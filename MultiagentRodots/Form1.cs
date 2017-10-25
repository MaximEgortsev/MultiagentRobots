using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiagentRodots
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            Pen p = new Pen(Color.Red);
            for (int i = 0; i < numericUpDown_coloms.Value; i++)
            {
                g.DrawLine(p, new Point((pictureBox1.Width / 10 * (i + 1)), 0), new Point((pictureBox1.Width / 10 * (i + 1)), pictureBox1.Height));
            }
            for (int i = 0; i < numericUpDown_Rows.Value; i++)
            {
                g.DrawLine(p, new Point(0, (pictureBox1.Height / 10 * (i + 1))), new Point(pictureBox1.Width, (pictureBox1.Height / 10 * (i + 1))));
            }
        }

    }
}
