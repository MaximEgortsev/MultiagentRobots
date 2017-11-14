using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MultiagentRodots
{
    public class Maze 
    {
        public Maze(decimal coloms, decimal rows)
        {
            walls = new bool[(int)coloms, (int)rows];
            status = new int[(int)coloms, (int)rows];
        }
        /// <summary>
        /// if true - wall
        /// </summary>
        public bool[,] walls;
        /// <summary>
        /// if true - robot can go
        /// </summary>
        public bool[,] correctWalls;
        public int[,] status;

        public enum Status
        {
            Unknown,
            Wall,
            Free,
            Visited,
        }


        //определяем все клетки которые робот не может посетить
        public void FindBusyCell(int widthMaze, int heightMaze, Point startPoint)
        {
            correctWalls = new bool[widthMaze, heightMaze];
            //отображает возможное нахождение робота при прохождении алгоритма
            var robPosition = new bool[widthMaze, heightMaze];
            correctWalls[startPoint.X, startPoint.Y] = true;
            robPosition[startPoint.X, startPoint.Y] = true;
            //показывает что не все клетки с возможным нахождением робота рассмотрены
            bool flag = true;
            //если есть клетки, в которых может быть робот продолжать алгоритм
            while (flag)
            {
                flag = false;
                bool step = true;

                for (int i = 0; i < widthMaze; i++)
                    for (int j = 0; j < heightMaze; j++)
                    {
                        if(robPosition[i, j] && step)
                        {
                            //проверяет верхнюю, нижнюю правую и левую клетку относительно возможного расположения
                            if (i - 1 >= 0)
                                robPosition[i - 1, j] = CheckCell(i - 1, j, robPosition[i - 1, j]);
                            if (i + 1 <= widthMaze)
                                robPosition[i + 1, j] = CheckCell(i + 1, j, robPosition[i + 1, j]);
                            if (j - 1 >= 0)
                                robPosition[i, j - 1] = CheckCell(i, j - 1, robPosition[i, j - 1]);
                            if (j + 1 <= heightMaze)
                                robPosition[i, j + 1] = CheckCell(i, j + 1, robPosition[i, j + 1]);
                            robPosition[i, j] = false;
                            //переменная для того, чтобы смотреть только одно возможное положение за цикл
                            step = false;
                        }
                    }
                //если не все клетки проверены продолжаем алгоритм
                foreach (var e in robPosition)
                    if (e) flag = true;

                step = true;

            }

        }

        //проверяет есть ли в соседней клетки препятствие
        public bool CheckCell(int a, int b, bool rb)
        {
            //если нет стены и клетка еще не посещена 
            if (!walls[a, b] && !correctWalls[a, b] && !rb)
                {
                    //говорим что робот может проехать
                    correctWalls[a, b] = true;
                    return true;
                }
            return rb;
        }










    }

    


}
