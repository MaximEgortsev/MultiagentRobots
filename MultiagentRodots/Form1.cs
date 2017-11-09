﻿using System;
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

        public int leftIndent;
        public int topIndent;
        public int cellSize;
        public int step = 0;
        public Graphics g;
        public Point startPoint = new Point();
        Maze maze;

        private void button1_Click(object sender, EventArgs e)
        {
            if (step == 0)
            {
                step = 1;
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                g = Graphics.FromImage(pictureBox1.Image);
                //создается экземпляр класса лабиринт
                maze = new Maze(numericUpDown_coloms.Value, numericUpDown_Rows.Value);
                //колонок больше чем рядов?
                bool colomsMoreRows = numericUpDown_coloms.Value >= numericUpDown_Rows.Value;

                cellSize = CellSize(colomsMoreRows);
                DrawSetka(cellSize, colomsMoreRows, g);

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
                if (posX != -1 && posY != -1 &&
                    (posX == 0 || posY == 0 || posX == numericUpDown_coloms.Value - 1 || posY == numericUpDown_Rows.Value - 1))
                {
                    PaintOverCell(new SolidBrush(Color.Green), posX, posY);
                    startPoint = new Point(posX, posY);
                    step = 3;
                }
            }

        }
        
        //отрисовка сетки
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

        //расчет размеров клетки
        private int CellSize(bool colomsMoreRows)
        {
            if (colomsMoreRows)
                return Convert.ToInt32(pictureBox1.Width / numericUpDown_coloms.Value);
            else
                return Convert.ToInt32(pictureBox1.Height / numericUpDown_Rows.Value);
        }

        //определение позиции курсора относительно сетки
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

        //закрашиваем нужную кдлетку в нужный цвет
        private void PaintOverCell(SolidBrush br, int posX, int posY)
        {
            g.FillRectangle(br, leftIndent + cellSize * posX + 2, topIndent + cellSize * posY +2, cellSize - 2, cellSize - 2);
            pictureBox1.Invalidate();
            maze.walls[posX, posY] = !maze.walls[posX, posY];
        }

        //закрашиваем контур
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (step == 1)
            {
                step = 2;
            }
        }
    }
}
