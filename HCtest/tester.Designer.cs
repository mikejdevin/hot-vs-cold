namespace HCtest
{
    partial class app
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(app));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.downX = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.downY = new System.Windows.Forms.Label();
            this.upY = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.upX = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.distance = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.nearx = new System.Windows.Forms.Label();
            this.neary = new System.Windows.Forms.Label();
            this.tilecount = new System.Windows.Forms.Label();
            this.xy = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(30, 39);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(300, 300);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.beardown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bearup);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(347, 39);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(51, 20);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(404, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(347, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "currentsize";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(435, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "make";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(350, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "downX";
            // 
            // downX
            // 
            this.downX.AutoSize = true;
            this.downX.Location = new System.Drawing.Point(392, 107);
            this.downX.Name = "downX";
            this.downX.Size = new System.Drawing.Size(10, 13);
            this.downX.TabIndex = 6;
            this.downX.Text = "-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(350, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "downY";
            // 
            // downY
            // 
            this.downY.AutoSize = true;
            this.downY.Location = new System.Drawing.Point(392, 134);
            this.downY.Name = "downY";
            this.downY.Size = new System.Drawing.Size(10, 13);
            this.downY.TabIndex = 8;
            this.downY.Text = "-";
            // 
            // upY
            // 
            this.upY.AutoSize = true;
            this.upY.Location = new System.Drawing.Point(474, 134);
            this.upY.Name = "upY";
            this.upY.Size = new System.Drawing.Size(10, 13);
            this.upY.TabIndex = 12;
            this.upY.Text = "-";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(432, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "upY";
            // 
            // upX
            // 
            this.upX.AutoSize = true;
            this.upX.Location = new System.Drawing.Point(474, 107);
            this.upX.Name = "upX";
            this.upX.Size = new System.Drawing.Size(10, 13);
            this.upX.TabIndex = 10;
            this.upX.Text = "-";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(432, 108);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "upX";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(353, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "distance";
            // 
            // distance
            // 
            this.distance.AutoSize = true;
            this.distance.Location = new System.Drawing.Point(435, 163);
            this.distance.Name = "distance";
            this.distance.Size = new System.Drawing.Size(10, 13);
            this.distance.TabIndex = 14;
            this.distance.Text = "-";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(353, 191);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "i=";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(353, 213);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "j=";
            // 
            // nearx
            // 
            this.nearx.AutoSize = true;
            this.nearx.Location = new System.Drawing.Point(375, 190);
            this.nearx.Name = "nearx";
            this.nearx.Size = new System.Drawing.Size(33, 13);
            this.nearx.TabIndex = 17;
            this.nearx.Text = "nearx";
            // 
            // neary
            // 
            this.neary.AutoSize = true;
            this.neary.Location = new System.Drawing.Point(378, 213);
            this.neary.Name = "neary";
            this.neary.Size = new System.Drawing.Size(33, 13);
            this.neary.TabIndex = 18;
            this.neary.Text = "neary";
            // 
            // tilecount
            // 
            this.tilecount.AutoSize = true;
            this.tilecount.Location = new System.Drawing.Point(404, 80);
            this.tilecount.Name = "tilecount";
            this.tilecount.Size = new System.Drawing.Size(10, 13);
            this.tilecount.TabIndex = 19;
            this.tilecount.Text = "-";
            // 
            // xy
            // 
            this.xy.AutoSize = true;
            this.xy.Location = new System.Drawing.Point(350, 241);
            this.xy.Name = "xy";
            this.xy.Size = new System.Drawing.Size(20, 13);
            this.xy.TabIndex = 20;
            this.xy.Text = "x y";
            // 
            // app
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 477);
            this.Controls.Add(this.xy);
            this.Controls.Add(this.tilecount);
            this.Controls.Add(this.neary);
            this.Controls.Add(this.nearx);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.distance);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.upY);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.upX);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.downY);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.downX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "app";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label downX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label downY;
        private System.Windows.Forms.Label upY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label upX;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label distance;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label nearx;
        private System.Windows.Forms.Label neary;
        private System.Windows.Forms.Label tilecount;
        private System.Windows.Forms.Label xy;
    }
}

