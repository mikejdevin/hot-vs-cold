using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCL2
{
    public enum TileType { invalid = -1 , empty = 0 , fire = 1 , 
        ice1 = 2 , ice2 = 3 , ice3 = 4 , maxice = 5}
    public enum tileTilt { vertical = 0, horizontal = 1 }

    public class Tile
    {

        public readonly int x, y;
        public static readonly int maxTemp = 50 ;

        public Board myBoard { get; set; }

        public Tile(TileType s = TileType.empty, int i = 0, int j = 0)
        {
            x = i;
            y = j;
            State = s;
            Damage = 0;
            Temp = 0;
        }

        public TileType State { get; set; }
        public double Damage { get; set; }
        public double Temp { get; set; }

        public void dealDamage()
        {
            Damage += Temp ;
        }
        public Boolean buildable() {
            switch (State)
            {
                case TileType.invalid:
                    return false;
                case TileType.empty:
                    return true;
                case TileType.fire:
                    return (Temp < maxTemp);
                case TileType.ice1:
                case TileType.ice2:
                case TileType.ice3:
                    return true;
                case TileType.maxice:
                    return false;
                default:
                    return false;
            }
        }

        public Boolean setFire()
        {
            int d;
            List<Tile> tlist;

            if (State != TileType.empty) return false;
            State = TileType.fire; 
            if (myBoard == null) return true;
            myBoard.fireList.Add(this);
            tlist = myBoard.GetTiles();
            foreach (Tile other in tlist)
                {
                    d = TileFactory.HexDistance(this, other);
                    other.Temp += Thermostat.CalcHeat(d);
                }
            return true;
        }

        public Boolean putIce()
        {
            int d;
            Boolean unheat =false;
            List<Tile> tlist;

            if (!buildable()) return false;
            State++;
            if (myBoard == null) return true;
            if (myBoard.fireList.Contains(this)) { 
                unheat = true ;
                myBoard.fireList.Remove(this);
            }
            tlist = myBoard.GetTiles();
            foreach (Tile other in tlist)
            {
                d = TileFactory.HexDistance(this, other);
                if (unheat)
                    other.Temp -= Thermostat.CalcHeat(d);
                other.Temp -= Thermostat.CalCold(d, State);
            }

            return true;
        }
    }

    public delegate Boolean BoardShape(int i, int j);

    public class Board
    {

        Dictionary<int, Tile> _mytiles;
        readonly int size;
        readonly Tile outOfBounds = new Tile(TileType.invalid);
        public List<Tile> fireList = new List<Tile>();


        public void Clear() {
            fireList = new List<Tile>();
            foreach(Tile t in _mytiles.Values)
            {
                t.State = TileType.empty;
                t.Temp = 0;
                t.Damage = 0;
            }

        }

        public Board(int s)
        {
            _mytiles = new Dictionary<int,Tile>();
            size = s ;

        }

        public Board(List<Tile> these)
        {
            _mytiles = new Dictionary<int,Tile>() ;
            size = these.Count;
            foreach (Tile t in these)
            {
                _mytiles[t.x + size * t.y] = t;
                t.myBoard = this;
            }
        }

        public Tile this[int i, int j]
        {
            get
            {
                Tile t;

                _mytiles.TryGetValue(i + size * j,out t);
                return t ?? outOfBounds;
            }
            set
            {
                _mytiles[i + size * j] = value;
            }
        }

        public List<Tile> GetTiles()
        {
            return _mytiles.Values.ToList<Tile>();
        }

        public List<Tile> GetNeighbors(Tile tl)
        {
            List<Tile> ns = new List<Tile>();
            int a, b ;
            Tile nt ;

            if ( tl.State == TileType.invalid ) 
                return ns;
            a = tl.x;
            b = tl.y;
            if (this[a, b] != tl )
                return ns;
            nt = this[++a,b]; //+,0
            if (nt.State != TileType.invalid)
                ns.Add(nt);
            nt = this[a, --b]; //+,-
            if (nt.State != TileType.invalid)
                ns.Add(nt);
            nt = this[--a, b]; //0,-
            if (nt.State != TileType.invalid)
                ns.Add(nt);
            nt = this[--a, ++b]; //-,0
            if (nt.State != TileType.invalid)
                ns.Add(nt);
            nt = this[a, ++b]; //-,+
            if (nt.State != TileType.invalid)
                ns.Add(nt);
            nt = this[++a, b]; //0,+
            if (nt.State != TileType.invalid)
                ns.Add(nt);
            return ns;
        }

        public Tuple<int, int,int,int> GetExtent(tileTilt tt)
        {
            int xmax, xmin, ymax, ymin;
            List<Tile> tlist = GetTiles();

            xmax = tlist.Max<Tile>(tl => (tt == tileTilt.vertical) ? 2 * tl.x + tl.y : 2*tl.x );
            xmin = tlist.Min<Tile>(tl => (tt == tileTilt.vertical) ? 2 * tl.x + tl.y : 2*tl.x );
            ymax = tlist.Max<Tile>(tl => (tt == tileTilt.horizontal) ? 2 * tl.y + tl.x : 2*tl.y );
            ymin = tlist.Min<Tile>(tl => (tt == tileTilt.horizontal) ? 2 * tl.y + tl.x : 2*tl.y );

            return new Tuple<int, int,int,int>(xmin,ymin,xmax,ymax); 
        }
        public Tuple<int, int, int, int> GetExtent()
        {
            int xmax, xmin, ymax, ymin;
            List<Tile> tlist = GetTiles();

            xmax = tlist.Max<Tile>(tl => tl.x);
            xmin = tlist.Min<Tile>(tl => tl.x);
            ymax = tlist.Max<Tile>(tl => tl.y);
            ymin = tlist.Min<Tile>(tl => tl.y);

            return new Tuple<int, int, int, int>(xmin, ymin, xmax, ymax);
        }

    }

    public static class TileFactory
    {
        public static List<Tile> CreateTiles(int m, BoardShape bs)
        {
            List<Tile> mylist = new List<Tile>();
            int i,j;

            for(i=0;i<m;i++)
                for(j=0;j<m;j++)
                    if (bs(i,j))
                        mylist.Add(new Tile(TileType.empty,i,j));
            return mylist;
        }

        public static Board CreateBoard(int m, BoardShape bs)
        {
            return new Board(CreateTiles(m, bs));
        }
   
        public static BoardShape HexBoard(int s)
        {
            return delegate(int i, int j)
            {
                if (i + j + 1 < s) return false;
                if (i + 2 > 2 * s) return false;
                if (j + 2 > 2 * s) return false;
                if (i + j +3> 3 * s) return false;
                return true;
            };
        }

        public static int HexDistance(int x1, int y1, int x2, int y2)
        {
            if (Math.Sign(x1 - x2) * Math.Sign(y1 - y2) >= 0) return Math.Abs(x1 + y1 - x2 - y2);
            if (Math.Abs(x1 - x2) >= Math.Abs(y1 - y2)) return Math.Abs(x1 - x2);
            return Math.Abs(y1 - y2);
        }
        public static int HexDistance(Tile t1, Tile t2)
        {
            return HexDistance(t1.x, t1.y, t2.x, t2.y);
        }

    }

    public static class Thermostat
    {
        public static double CalcHeat(int d)
        {
            return 100/(d+1);
        }

        public static double CalCold(int d, TileType tt)
        {
            if (d>2) return 0;
            switch (tt)
            {
                case TileType.ice1:
                    return (3 - d) * 10;
                case TileType.ice2:
                    return (3 - d) * 20;
                case TileType.ice3:
                    return (3 - d) * 40;
                case TileType.maxice:
                    return (4 - d) * 50;
                default:
                    return 0;
            }


            
        }


    }

    public class PaintBoard
    {
        public static Tuple<int,int>TileSize(Board b, int winx,int winy,tileTilt tt) {
            int difx,dify;
            Tuple<int, int, int, int> extent = b.GetExtent(tt) ;

            difx = extent.Item3 - extent.Item1 + 2;
            dify = extent.Item4 - extent.Item2 + 2;
            difx = (int)( winx / difx);
            dify = (int)( winy / dify);
            return new Tuple<int, int>(difx, dify);
 
        }


        public static List<Tuple<Tile,int,int>>Tilate(Board b,int winx,int winy,tileTilt tt) {
        
            List<Tuple<Tile,int,int>> mylist = new List<Tuple<Tile,int,int>>() ;
            Tuple<int, int, int, int> extent = b.GetExtent(tt) ;
            Tuple<int,int> ts = TileSize( b,  winx, winy, tt);
            List<Tile> tl = b.GetTiles();
            
            foreach(Tile t in tl) {
                int x = (tt==tileTilt.vertical) ? 2*t.x + t.y : 2*t.x ;  
                int y = (tt==tileTilt.horizontal) ? 2*t.y + t.x : 2*t.y ;  
                x -= extent.Item1 ;
                y -= extent.Item2 ;
                x = x* ts.Item1 ;
                y = y* ts.Item2 ;
                mylist.Add(new Tuple<Tile,int,int>(t,x,y));
            }
            
            return mylist;
        }

        public static Color colorFromTemp(double temp)
        {
           
            int shad;
            if (temp > 100) temp = 100;
            if (temp<-100) temp = -100;
            shad = (int)temp / 10;
            if (shad == 0) return Color.White;
            if (shad < 0) return Color.Blue;
            if (shad > 0) return Color.Red;
            return Color.White;
            //if (temp>100) temp = 100;
            //if (temp>0) 
            //    return Color.FromArgb(255,255,(int)(2.55*temp),(int)(2.55*temp));
            //if (temp<-100) temp = -100;
            //    return Color.FromArgb(255,225+(int)(2.55*temp),225+(int)(2.55*temp),255);
        }

        //public static Image StandardTiles(Board b,tileTilt tt)
        //{
        //    Bitmap scr;
        //    int x1, y1, x2, y2;
        //    var ex = b.GetExtent(tt);
            
        //    x1 = ex.Item1;
        //    y1 = ex.Item2;
        //    x2 = ex.Item3;
        //    y2 = ex.Item4;

        //    scr = new Bitmap(50 * (x2 - x1 + 3), 50 * (y2 - y1 + 1));

        //    return scr;
        //}


    }

}

