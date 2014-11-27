using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HC3
{
 
	//Here is a comment.  testingin testing testing
    public enum TileType { invalid = -1 , empty = 0 , fire = 1 , 
        ice1 = 2 , ice2 = 3 , ice3 = 4 , maxice = 5}
    public enum tileTilt { vertical = 0, horizontal = 1 }
    public delegate Boolean BoardShape(int i, int j);
    public delegate double HeatField(int i,int j);



    public class Tile
    {
        public readonly int x, y;
        public Board myboard;

        public TileType State { get; set; }
        public double Temp { get; set; }
        public double Damage { get; set; }

        public Tile(int i, int j, TileType ty = TileType.empty)
        {
            x = i;
            y = j;
            State = ty;
        }

        public void DealDamage()
        {
            Damage += Temp;
        }
        public Boolean buildable()
        {
            switch (State)
            {
                case TileType.invalid:
                    return false;
                case TileType.empty:
                    return true;
                case TileType.fire:
                    return (Temp < Thermostat.BuildTemp);
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
        public Boolean SetFire()
        {
            return myboard.SetFire(x, y);
        }
        public Boolean PutIce()
        {
            return myboard.PutIce(x, y);
        }
        public List<Tile> GetNeighbors()
        {
            return myboard.GetNeighbors(this);
        }

    }

    public class Board
    {
        List<Tile> _mytiles = new List<Tile>();
        List<Tile> fireList = new List<Tile>();
        Tile[,] _board;
        public readonly int size;
        readonly Tile outOfBounds ;

        public Board(List<Tile> tldr,int sz)
        {
            outOfBounds = new Tile(-1,-1,TileType.invalid);
            outOfBounds.myboard = this;
            _mytiles = tldr;
            size = sz;
            _board = new Tile[sz, sz];
            foreach (Tile tl in tldr)
            {
                _board[tl.x, tl.y] = tl;
                tl.myboard = this;
            }
        }

        public List<Tile> GetFires() {
            return fireList.ToList<Tile>();
        }
        public List<Tile> GetTiles()
        {
            return _mytiles.ToList<Tile>();
        }

        public Tile this[int i, int j]
        {
            get
            {
                if ((i < 0)||(j<0)||(i>size-1)||(j>size-1)) return outOfBounds;
                return _board[i, j] ?? outOfBounds;
            }
        }

        public List<Tile> GetNeighbors(Tile tl)
        {
            List<Tile> ns = new List<Tile>();
            int a, b;
            Tile nt;

            if (tl.State == TileType.invalid)
                return ns;
            a = tl.x;
            b = tl.y;
            if (this[a, b] != tl)
                return ns;
            nt = this[a+1, b]; 
            if (nt.State != TileType.invalid)
                ns.Add(nt);
            nt = this[a, b+1]; 
            if (nt.State != TileType.invalid)
                ns.Add(nt);
            nt = this[a-1, b]; 
            if (nt.State != TileType.invalid)
                ns.Add(nt);
            nt = this[a, b-1]; 
            if (nt.State != TileType.invalid)
                ns.Add(nt);
            nt = this[a-1, b+1]; 
            if (nt.State != TileType.invalid)
                ns.Add(nt);
            nt = this[a+1, b-1]; 
            if (nt.State != TileType.invalid)
                ns.Add(nt);
            return ns;
        }

        public Tuple<int, int, int, int> GetExtent(tileTilt tt)
        {
            int xmax, xmin, ymax, ymin;
            List<Tile> tlist = GetTiles();

            xmax = tlist.Max<Tile>(tl => (tt == tileTilt.vertical) ? 2 * tl.x + tl.y : 2 * tl.x);
            xmin = tlist.Min<Tile>(tl => (tt == tileTilt.vertical) ? 2 * tl.x + tl.y : 2 * tl.x);
            ymax = tlist.Max<Tile>(tl => (tt == tileTilt.horizontal) ? 2 * tl.y + tl.x : 2 * tl.y);
            ymin = tlist.Min<Tile>(tl => (tt == tileTilt.horizontal) ? 2 * tl.y + tl.x : 2 * tl.y);

            return new Tuple<int, int, int, int>(xmin, ymin, xmax, ymax);
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

        public void Clear()
        {
            fireList = new List<Tile>();
            foreach (Tile t in _mytiles)
            {
                t.State = TileType.empty;
                t.Temp = 0;
                t.Damage = 0;
            }

        }
        public void DamageTick()
        {
            foreach (Tile tl in _mytiles)
            {
                tl.DealDamage();
            }
        }
        public Boolean SetFire(int i, int j)
        {
            Tile tl = this[i, j];
            int d;

            if (tl == outOfBounds) return false;
            if (tl.State != TileType.empty) return false;
            tl.State = TileType.fire;
            fireList.Add(tl);
            foreach (Tile other in _mytiles)
            {
                d = Thermostat.HexDistance(tl, other);
                other.Temp += Thermostat.CalcHeat(d);
            }
            return true;
        }
        public Boolean PutIce(int i,int j)
        {
            int d;
            Boolean unheat =false;
            Tile tl = this[i, j];

            if (!tl.buildable()) return false;
            tl.State++;
            if (fireList.Contains(tl)) { 
                unheat = true ;
                fireList.Remove(tl);
            }
            foreach (Tile other in _mytiles)
            {
                d = Thermostat.HexDistance(tl, other);
                if (unheat)
                    other.Temp -= Thermostat.CalcHeat(d);
                other.Temp -= Thermostat.CalCold(d, tl.State);
            }

            return true;
        }

        public void Turn()
        {
            DamageTick();
            Thermostat.SpreadFire(this);

        }

    }

    public static class TileFactory
    {
        public static List<Tile> CreateTiles(int m, BoardShape bs)
        {
            List<Tile> mylist = new List<Tile>();
            int i, j;

            for (i = 0; i < m; i++)
                for (j = 0; j < m; j++)
                    if (bs(i, j))
                        mylist.Add(new Tile(i, j));
            return mylist;
        }

        public static Board CreateBoard(int m, BoardShape bs)
        {
            return new Board(CreateTiles(m, bs), m);
        }

        public static BoardShape HexBoard(int s)
        {
            return delegate(int i, int j)
            {
                if (i + j + 1 < s) return false;
                if (i + 2 > 2 * s) return false;
                if (j + 2 > 2 * s) return false;
                if (i + j + 3 > 3 * s) return false;
                return true;
            };
        }

        public static BoardShape fancy()
        {
            BoardShape bs = HexBoard(3);

            return delegate(int i, int j)
            {
                if (bs(i,j)) return true ;
                if (bs(i-2,j)) return true ;
                return false;
            };

        }

    }

    public static class Thermostat
    {
        public const int BuildTemp = 25 ;
        
        public static double CalcHeat(int d)
        {
            return 100 / (d + 1);
        }

        public static double CalCold(int d, TileType tt)
        {
            if (d > 2) return 0;
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
        public static void SpreadFire(Board bd)
        {
            HashSet<Tile> ns = new HashSet<Tile>();
            Random r = new Random();

            foreach (Tile t in bd.GetFires())
                foreach (Tile l in bd.GetNeighbors(t))
                    if (l.State==TileType.empty)
                        if (r.Next(10)==0) 
                            l.SetFire();
        }

    }

    public class PaintBoard
    {
        public static Tuple<int, int> TileSize(Board b, int winx, int winy, tileTilt tt)
        {
            int difx, dify;
            Tuple<int, int, int, int> extent = b.GetExtent(tt);

            difx = extent.Item3 - extent.Item1 + 2;
            dify = extent.Item4 - extent.Item2 + 2;
            difx = (int)(winx / difx);
            dify = (int)(winy / dify);
            return new Tuple<int, int>(difx, dify);

        }


        public static List<Tuple<Tile, int, int>> Tilate(Board b, int winx, int winy, tileTilt tt)
        {

            List<Tuple<Tile, int, int>> mylist = new List<Tuple<Tile, int, int>>();
            Tuple<int, int, int, int> extent = b.GetExtent(tt);
            Tuple<int, int> ts = TileSize(b, winx, winy, tt);
            List<Tile> tl = b.GetTiles();

            foreach (Tile t in tl)
            {
                int x = (tt == tileTilt.vertical) ? 2 * t.x + t.y : 2 * t.x;
                int y = (tt == tileTilt.horizontal) ? 2 * t.y + t.x : 2 * t.y;
                x -= extent.Item1;
                y -= extent.Item2;
                x = x * ts.Item1;
                y = y * ts.Item2;
                mylist.Add(new Tuple<Tile, int, int>(t, x, y));
            }

            return mylist;
        }

        public static Color colorFromTemp(double temp)
        {
            if (temp > 100) return Color.Red;
            if (temp < -100) return Color.Blue;
            if (temp > 0) return Color.FromArgb( 2*(int)temp+55,0, 0);
            return Color.FromArgb(0, 0, 55+2*(int)temp);        
        
        
        }
    }


}
