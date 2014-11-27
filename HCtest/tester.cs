using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using hotvcoldlib;

namespace HCtest
{
    public partial class app : Form
    {
        board myboard = new board(2) ;
        int size = 2;
        List<Tile> myTiles;
        readonly double root3 = Math.Sqrt(3);

        public app()
        {
            InitializeComponent();
            label2.Text = size.ToString();
            myTiles = myboard.getTiles();
        }

        private void beardown(object sender, MouseEventArgs e)
        {
            downX.Text = e.X.ToString();
            downY.Text = e.Y.ToString();
            showNearest(e.X, e.Y);
       
        }

        private void bearup(object sender, MouseEventArgs e)
        {
            upX.Text = e.X.ToString();
            upY.Text = e.Y.ToString();
            showNearest(e.X,e.Y);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            size =(int)numericUpDown1.Value;
            myboard = new board(size);
            label2.Text = size.ToString();
            myTiles = myboard.getTiles();
            tilecount.Text = myTiles.Count.ToString();
            pictureBox1.Image = HexMaker.MakeThat(size);
        }

        private void showNearest(double x ,double y)
        {
            Tuple<Tile,double> q = myboard.nearestTile(x/300,y/300);
            nearx.Text = q.Item1.x.ToString();
            neary.Text = q.Item1.y.ToString();
            distance.Text = (300* q.Item2).ToString();
        }
   
    
    
    
    }
}
