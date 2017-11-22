using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiagentRobots
{
    public partial class Form3 : Form
    {
        public Statistic statistic;
        public Form3(Statistic st)
        {
            InitializeComponent();
            statistic = st;
            richTextBox1.Text = CreateText((int)numericUpDown1.Value);
            CreateDiagram((int)numericUpDown1.Value);
        }

         

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = CreateText((int)numericUpDown1.Value);
            CreateDiagram((int)numericUpDown1.Value);
        }


        private string CreateText(int val)
        {
            string str = "В результате прохождения лабиринта, роботы открыли следующее количество коридоров:\n";
            for (int i = 0; i < val; i++)
                str += "робот" + Convert.ToString(i) + " - " +
                    Convert.ToString(statistic.robOpenCoridors[val - 1, i]) + ";\n";

            return str;
        }

        private void CreateDiagram(int val)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            var g = Graphics.FromImage(pictureBox1.Image);

            SolidBrush[] br = new SolidBrush[5];
            br[0] = new SolidBrush(Color.Red);
            br[1] = new SolidBrush(Color.Yellow);
            br[2] = new SolidBrush(Color.Blue);
            br[3] = new SolidBrush(Color.Green);
            br[4] = new SolidBrush(Color.DarkGoldenrod);
            
            Rectangle rect = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);

            float m = (float)360 / statistic.robOpenCoridors[0, 0];
            float lastPos = 0;
            for (int i = 0; i < val; i++)
            {
                var add = m * statistic.robOpenCoridors[val-1, i];
                g.FillPie(br[i], rect, lastPos, add);
                lastPos += add;
            }
        }

    }
}
