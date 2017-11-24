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
            CreateCircleDiagram((int)numericUpDown1.Value);
            CreateColumDiagramm();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = CreateText((int)numericUpDown1.Value);
            CreateCircleDiagram((int)numericUpDown1.Value);
        }

        /// <summary>
        /// вывод количества открыты коридоров на экран
        /// </summary>
        /// <param name="val">количество роботов</param>
        /// <returns></returns>
        private string CreateText(int val)
        {
            string str = "В результате прохождения лабиринта, роботы открыли следующее количество коридоров:\n";
            for (int i = 0; i < val; i++)
                str += "робот" + Convert.ToString(i) + " - " +
                    Convert.ToString(statistic.robOpenCoridors[val - 1, i]) + ";\n";

            return str;
        }

        /// <summary>
        /// оздает круговую диаграмму отображающую количество открытых коридоров каждым роботом
        /// </summary>
        /// <param name="val"></param>
        private void CreateCircleDiagram(int val)
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

        /// <summary>
        /// создаем столбчатую диаграмму
        /// </summary>
        private void CreateColumDiagramm()
        {
            pictureBox2.Image = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            var gr = Graphics.FromImage(pictureBox2.Image);
            //левый отступ
            int lIndent = 50;
            //отступ сверху
            int tIndent = 30;
            Font drawFont = new Font("Arial", 12);
            //надписи такт и робот
            gr.DrawString("tact", drawFont, new SolidBrush(Color.Black), 0, 0);
            gr.DrawString("robot", drawFont, new SolidBrush(Color.Black), pictureBox2.Width - lIndent, pictureBox2.Height - tIndent);
            //линии для столбчатой диаграммы
            gr.DrawLine(new Pen(Color.Black), lIndent, tIndent, lIndent, pictureBox2.Size.Height - tIndent);
            gr.DrawLine(new Pen(Color.Black), lIndent, pictureBox2.Size.Height - tIndent,
                pictureBox2.Size.Width - lIndent, pictureBox2.Size.Height - tIndent);
            //количество роботов
            gr.DrawString(Convert.ToString(0), drawFont, new SolidBrush(Color.Black),
                    10, pictureBox2.Height - tIndent);

            int grafWidth = pictureBox2.Size.Width - lIndent * 2;
            int grafHeight = pictureBox2.Size.Height - tIndent * 2;

            var w = (int)grafWidth / (statistic.robAmount.Length + 1);
            double max = statistic.steps.Max() + 10;
            var t = (int)grafHeight / max;

            var widhtColom = 20;
            //отображаем количество тактов для каждого количества роботов
            for (int i = 0; i < statistic.robAmount.Length; i++)
            {
                gr.DrawString(Convert.ToString(i + 1), drawFont, new SolidBrush(Color.Black), lIndent + w * (i + 1), grafHeight + tIndent);
                gr.DrawString(Convert.ToString(statistic.steps[i]), drawFont, new SolidBrush(Color.Black),
                    lIndent + w * (i + 1), pictureBox2.Height - tIndent - (int)(t * statistic.steps[i]) - 20);
                gr.FillRectangle(new SolidBrush(Color.Red), lIndent + w * (i + 1), pictureBox2.Height - tIndent - (int)(t * statistic.steps[i]),
                    widhtColom, (int)(t * statistic.steps[i]));
            }
        }

    }
}
