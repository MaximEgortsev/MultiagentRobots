using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace MultiagentRobots
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }        
        
        /// <summary>
        /// левы отступ в picBox
        /// </summary>
        public int leftIndent;
        /// <summary>
        /// верхний отступ в picBox
        /// </summary>
        public int topIndent;
        /// <summary>
        /// размер клетки
        /// </summary>
        public int cellSize;
        public int step = 0;
        public Graphics g;

        Maze maze;
        SingleAgent[] agent;
        

        /// <summary>
        /// задаем размеры лабиринта, кол-во роботов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (step == 0)
            {
                numericUpDown_coloms.Enabled = false;
                numericUpDown_Robots.Enabled = false;
                numericUpDown_Rows.Enabled = false;
                step = 1;
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                g = Graphics.FromImage(pictureBox1.Image);
                //создается экземпляр класса лабиринт
                maze = new Maze(numericUpDown_coloms.Value, numericUpDown_Rows.Value);
                //колонок больше чем рядов?
                bool colomsMoreRows = numericUpDown_coloms.Value >= numericUpDown_Rows.Value;

                cellSize = CellSize(colomsMoreRows);
                DrawSetka(cellSize, colomsMoreRows, g);
                button1.Enabled = false;
            }

        }

        /// <summary>
        /// после расстановки препятствий, далее выбор точки входа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (step == 1)
            {
                step = 2;
                button2.Enabled = false;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //положение курсора относительно сетки
            int posX = PositionInMaze(e.X, leftIndent, numericUpDown_coloms.Value);
            int posY = PositionInMaze(e.Y, topIndent, numericUpDown_Rows.Value);

            if (step == 1)
            {
                //если нажали на клетку закрашиваем ее
                if (posX != -1 && posY != -1 &&
                    !(posX == 0 || posY == 0 || posX == numericUpDown_coloms.Value - 1 || posY == numericUpDown_Rows.Value - 1))
                {
                    if (maze.walls[posX, posY] == false)
                        PaintOverCell(new SolidBrush(Color.Black), posX, posY);
                    else
                        PaintOverCell(new SolidBrush(Color.White), posX, posY);
                }
            }
            if (step == 2)
            {
                //выбор входа в лабиринт, может быть только крайняя клетка
                if (posX != -1 && posY != -1 &&
                    (posX == 0 || posY == 0 || posX == numericUpDown_coloms.Value - 1 || posY == numericUpDown_Rows.Value - 1))
                {
                    PaintOverCell(new SolidBrush(Color.Green), posX, posY);
                    step = 3;
                    ColorizeMaze(posX, posY);
                    CreateRobots(posX, posY, (int)numericUpDown_Robots.Value);
                    maze.startpoint = new Point(posX, posY);
                    timer1.Enabled = true;
                }
            }

        }
        
        /// <summary>
        /// отрисовка сетки
        /// </summary>
        /// <param name="cellSize"></param>
        /// <param name="colomsMoreRows"></param>
        /// <param name="g"></param>
        public void DrawSetka(int cellSize, bool colomsMoreRows, Graphics g)
        {
            //расчет отступов сетки от краев picBox
            leftIndent = (int)((pictureBox1.Width - cellSize * numericUpDown_coloms.Value) / 2);
            topIndent = (int)((pictureBox1.Height - cellSize * numericUpDown_Rows.Value) / 2);
            //закрашиваем рабочую область
            g.FillRectangle(new SolidBrush(Color.White), leftIndent, topIndent, cellSize * (int)numericUpDown_coloms.Value, cellSize * (int)numericUpDown_Rows.Value);

            //отрисовка колонок
            for (int i = 0; i <= numericUpDown_coloms.Value; i++)
            {
                var p1 = new Point(leftIndent + cellSize * i, topIndent);
                var p2 = new Point(leftIndent + cellSize * i, pictureBox1.Height - topIndent);
                g.DrawLine(new Pen(Color.Red), p1, p2);
            }
            //отрисовка рядов
            for (int i = 0; i <= numericUpDown_Rows.Value; i++)
            {
                var p1 = new Point(leftIndent, topIndent + cellSize * i);
                var p2 = new Point(pictureBox1.Height - leftIndent, topIndent + cellSize * i);
                g.DrawLine(new Pen(Color.Red), p1, p2);
            }

            PaintConture();

        }

        /// <summary>
        ///расчет размеров клетки
        /// </summary>
        /// <param name="colomsMoreRows"></param>
        /// <returns></returns>
        private int CellSize(bool colomsMoreRows)
        {
            if (colomsMoreRows)
                return Convert.ToInt32(pictureBox1.Width / numericUpDown_coloms.Value);
            else
                return Convert.ToInt32(pictureBox1.Height / numericUpDown_Rows.Value);
        }

        /// <summary>
        /// определение позиции курсора относительно сетки
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="indent"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private int PositionInMaze(int pos, int indent, decimal value)
        {
            //проверка что курсор внутри рабочей области
            if (pos > indent && pos < pictureBox1.Width - indent)
            {
                for (int i = 0; i < value; i++)
                    //определение конкретной клетки
                    if (pos > (indent + cellSize * i) && pos < (indent + cellSize * (i+1)))
                        return i;
            }
            return -1;
        }

        /// <summary>
        /// закрашиваем нужную кдлетку в нужный цвет
        /// </summary>
        /// <param name="br">цвет</param>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        private void PaintOverCell(SolidBrush br, int posX, int posY)
        {
            g.FillRectangle(br, leftIndent + cellSize * posX + 2, topIndent + cellSize * posY +2, cellSize - 2, cellSize - 2);
            pictureBox1.Invalidate();
            maze.walls[posX, posY] = !maze.walls[posX, posY];
        }

        /// <summary>
        /// закрашиваем контур
        /// </summary>
        private void PaintConture()
        {
            for (int i = 0; i < numericUpDown_coloms.Value; i++)
                for (int j = 0; j < numericUpDown_Rows.Value; j++)
                {
                    if (i == 0 || j == 0 || i == numericUpDown_coloms.Value - 1 || j == numericUpDown_Rows.Value - 1)
                    {
                        PaintOverCell(new SolidBrush(Color.Black), i, j);
                        maze.walls[i, j] = true;
                    }
                }
        }

        /// <summary>
        /// закрашиваем клетки которые нельзя посетить
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        private void ColorizeMaze(int posX, int posY)
        {
            maze.FindBusyCell((int)numericUpDown_coloms.Value, (int)numericUpDown_Rows.Value, new Point(posX, posY));
            for (int i = 0; i < (int)numericUpDown_coloms.Value; i++)
                for (int j = 0; j < (int)numericUpDown_Rows.Value; j++)
                {
                    if (!maze.correctWalls[i, j])
                        PaintOverCell(new SolidBrush(Color.Black), i, j);
                }
        }
        
        /// <summary>
        /// создаем роботов
        /// </summary>
        /// <param name="posX">стартовая позиция х</param>
        /// <param name="posY">стартовая позиция у</param>
        /// <param name="count">кол-во роботов</param>
        private void CreateRobots(int posX, int posY, int count)
        {
            var multAgent = new Agents(maze.correctWalls, new Point(posX, posY), count);

            agent = new SingleAgent[count];
            for (int i = 0; i < count; i++)
                agent[i] = new SingleAgent(new Point(posX, posY));
        }

        /// <summary>
        /// отрисовка передвижения роботов
        /// розовый - свободные клетки
        /// красные - посещенные клетки
        /// желтые - обнаруженные стены
        /// </summary>
        private void VizualizationRobMove()
        {
            MoveOneStep();
            for (int i = 0; i < (int)numericUpDown_coloms.Value; i++)
                for (int j = 0; j < (int)numericUpDown_Rows.Value; j++)
                {
                    if (Agents.status[i, j] == Agents.Status.Visited)
                        PaintOverCell(new SolidBrush(Color.Pink), i, j);
                    if (Agents.status[i, j] == Agents.Status.Free)
                        PaintOverCell(new SolidBrush(Color.Red), i, j);
                    if (Agents.status[i, j] == Agents.Status.Wall)
                        PaintOverCell(new SolidBrush(Color.Yellow), i, j);
                }
        }

        /// <summary>
        /// рассчет одного такта
        /// </summary>
        private void MoveOneStep()
        {
            agent[0].takt++;
            //незадействованные роботы
            var freeRobots = new List<int>();
            for (int i = 0; i < agent.Count(); i++)
                freeRobots.Add(i);

            Agents.RoadCalc(maze.correctWalls, agent, freeRobots);
        }
       
        private void timer1_Tick(object sender, EventArgs e)
        {
            var stat = new Statistic((int)numericUpDown_Robots.Maximum); 
            if (Agents.freeCoridors.Count() != 0)
            {
                //agent[0].takt++;
                VizualizationRobMove();
                DrawAgent();
            }
            else
            {
                timer1.Enabled = false;

                stat.func1(agent);
                
                CalcWayForAllVar(stat);
                Statistics(stat);
                // var stWindow = new StatWindow(stat);
                var Form3 = new Form3(stat);
                Form3.Visible = true;
            }
                       
        }

        /// <summary>
        /// отрисовка роботов
        /// </summary>
        private void DrawAgent()
        {
            for (int i = 0; i < agent.Count(); i++)
            {
                var br = new SolidBrush(Color.FromArgb(i * 40 + 15, i * 40 + 15, i * 40 + 15));
                float corX = leftIndent + cellSize * agent[i].startRobPos.X + cellSize / agent.Count() * i + 2;
                float corY = topIndent + cellSize * agent[i].startRobPos.Y + cellSize / agent.Count() * i + 2;
                float wid = cellSize / agent.Count() - 2;
                float heig = cellSize / agent.Count()- 2;
                g.FillEllipse(br, corX, corY, wid, heig);
            }
            
            pictureBox1.Invalidate();

        }

        /// <summary>
        /// 
        /// </summary>
        private void Statistics(Statistic st)
        {
            Form Form2 = new Form();
            Form2.Size = new Size(500, 300);
            Form2.Visible = true;

            PictureBox picBox = new PictureBox();
            picBox.Dock = DockStyle.Fill;
            picBox.BackColor = Color.White;

            Form2.Controls.Add(picBox);

            picBox.Image = new Bitmap(picBox.Width, picBox.Height);
            var gr = Graphics.FromImage(picBox.Image);

            //
            string str1 = "tact";
            string str2 = "robot";
            Font drawFont = new Font("Arial", 16);
            gr.DrawString(str1, drawFont, new SolidBrush(Color.Black), 0, 0);
            gr.DrawString(str2, drawFont, new SolidBrush(Color.Black), 430, 240);

            int lIndent = 50;
            int tIndent = 30;

            gr.DrawLine(new Pen(Color.Black), lIndent, tIndent, lIndent, picBox.Size.Height - tIndent);
            gr.DrawLine(new Pen(Color.Black), lIndent, picBox.Size.Height - tIndent,
                picBox.Size.Width - lIndent, picBox.Size.Height - tIndent);


            int grafWidth = picBox.Size.Width - lIndent * 2;
            int grafHeight = picBox.Size.Height - tIndent * 2;

            var w = (int)grafWidth / (st.robAmount.Length + 1);
            double max = st.steps.Max() + 10;
            var t = (int)grafHeight / max;
            //for (int i = 1; i <= st.robAmount.Length; i++) 
            //    gr.DrawString(Convert.ToString(i), drawFont, new SolidBrush(Color.Black), lIndent + w * i, grafHeight + tIndent);

            var widhtColom = 20;

            for (int i = 0; i < st.robAmount.Length; i++)
            {
                gr.DrawString(Convert.ToString(i+1), drawFont, new SolidBrush(Color.Black), lIndent + w * (i+1), grafHeight + tIndent);
                gr.DrawString(Convert.ToString(st.steps[i]), drawFont, new SolidBrush(Color.Black),
                    lIndent + w * (i + 1), picBox.Height - tIndent - (int)(t * st.steps[i]) - 20);
                gr.FillRectangle(new SolidBrush(Color.Red), lIndent + w * (i + 1), picBox.Height - tIndent - (int)(t * st.steps[i]), 
                    widhtColom, (int)(t * st.steps[i]));
            }

            var mark = (max / 10 + 1) * 10;
            gr.DrawString(Convert.ToString(mark), drawFont, new SolidBrush(Color.Black),
                    10, picBox.Height - tIndent - (int)(t * mark));
            gr.DrawString(Convert.ToString(0), drawFont, new SolidBrush(Color.Black),
                    10, picBox.Height - tIndent);




        }

        /// <summary>
        /// рассчет прохождения лабиринта роботами от 1 до n
        /// без визуализации
        /// </summary>
        /// <param name="st"></param>
        private void CalcWayForAllVar(Statistic st)
        {
            for (int i = 1; i <= (int)numericUpDown_Robots.Maximum; i++)
            {
                if (i == (int)numericUpDown_Robots.Value)
                    continue;
                CreateRobots(maze.startpoint.X, maze.startpoint.Y, i);

                while (Agents.freeCoridors.Count() != 0)
                    MoveOneStep();

                st.func1(agent);
            }
        }



    }
}
