using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HC3;
using System.Diagnostics;

namespace test2
{
    public partial class Form1 : Form
    {
        BoardShape bs;
        Board bd;
        List<Tuple<Tile,int, int>> corners;
        Tuple<int, int> squareSize;
        Bitmap mp = new Bitmap(700, 600);
        Graphics gm;
        Brush bush = new SolidBrush(Color.BurlyWood);
        Image bearImg, fireImg, ice1Img, Ice2Img, ice3Img, maxIceImg;
        

        public Form1()
        {
            gm = Graphics.FromImage(mp);
            bs = TileFactory.fancy();
            bd = TileFactory.CreateBoard(22,bs);
            corners = PaintBoard.Tilate(bd, 700, 600, tileTilt.vertical);
            squareSize = PaintBoard.TileSize(bd, 700, 600, tileTilt.vertical);
            gm.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            gm.FillRectangle(bush, 0, 0, 700, 600);
            InitializeComponent();
       }

        void resetboard()
        {
            bd.Clear();
            bd.SetFire(4, 3);

        }

        void renderman()
        {
            int x, y ,wx,wy;

            Stopwatch tim = new Stopwatch() ;
            Rectangle rekt;
            


            tim.Start();
            foreach (var thing in corners)
            {
                ((SolidBrush)bush).Color = PaintBoard.colorFromTemp(thing.Item1.Temp);
                x = thing.Item2;
                y = thing.Item3;
                wx = squareSize.Item1;
                wy = squareSize.Item2;
                rekt = new Rectangle(x, y, 2*wx, 2*wy);
                gm.FillRectangle(bush, rekt);
                switch (thing.Item1.State)
                {
                    case TileType.invalid:

                        break;
                    case TileType.empty:
                        break;
                    case TileType.fire:

                        break;
                    case TileType.ice1:
                        break;
                    case TileType.ice2:
                        break;
                    case TileType.ice3:
                        break;
                    case TileType.maxice:
                        break;
                    default:
                        break;
                }

            }
            pictureBox1.Image = mp;
            tim.Stop();
            label1.Text = label1.Text + "\n" + tim.ElapsedMilliseconds.ToString();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void reset_Click(object sender, EventArgs e)
        {
            bd.Clear();
            bd[10, 10].SetFire();
            renderman();
        }

        private void ice_Click(object sender, EventArgs e)
        {

        }

        private void turn_Click(object sender, EventArgs e)
        {
            Stopwatch tim = new Stopwatch();

            tim.Start();
            bd.Turn();
            tim.Stop();
            label1.Text = tim.ElapsedMilliseconds.ToString();
            renderman();
        }
    }
}
