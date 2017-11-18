using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MultiagentRobots
{
    public class Agents
    {
        public static int agentsAmount = 0;
        public static List<Point> freeCoridors = new List<Point>();
        

        public enum Status
        {
            Unknown,
            Wall,
            Free,
            Visited,
        }

        public static Status[,] status = new Status[0, 0];

        public Agents(bool[,] wall, Point p, int v)
        {
            status = new Status[wall.GetLength(0), wall.GetLength(1)];
            status[p.X, p.Y] = Status.Free;
            
            agentsAmount = v;

            //var agent = new SingleAgent[agentsAmount];
            //for (int i = 0; i < agentsAmount; i++)
            //    agent[i] = new SingleAgent(p);
            
            freeCoridors.Add(p);


            


        }


        public static void RoadCalc(bool[,] w, SingleAgent[] agents)
        {
            var operation = Math.Min(agentsAmount, freeCoridors.Count);
            Point coridor;

            for(int i = 0; i < operation; i++)
            {
                if (freeCoridors[0] == agents[0].robPosition)
                    coridor = freeCoridors[0];
                else
                    coridor = SingleAgent.CalcNextRobPosition(agents[i]);
                agents[i].robPosition = coridor;
                SingleAgent.DeterminateStatusNextCells(coridor, w);
                freeCoridors.Remove(coridor);
            }
        }

        
        

    }



    public class SingleAgent
    {
        public List<Point> robotWay = new List<Point>();
        public int openCoridors = 0;
        public Point robPosition = new Point();

        public SingleAgent(Point p)
        {
            robPosition = p;
        }


        /// <summary>
        /// определение статуса соседних клеток от положения робота
        /// </summary>
        /// <param name="coridor"> начальная точка</param>
        /// <param name="w">массив стен</param>
        public static void DeterminateStatusNextCells(Point coridor, bool[,] w)
        {
            if (coridor.X - 1 >= 0)
                StatusCell(coridor.X - 1, coridor.Y, w[coridor.X - 1, coridor.Y]);
            if (coridor.X + 1 < w.GetLength(0))
                StatusCell(coridor.X + 1, coridor.Y, w[coridor.X + 1, coridor.Y]);
            if (coridor.Y - 1 >= 0)
                StatusCell(coridor.X, coridor.Y - 1, w[coridor.X, coridor.Y - 1]);
            if (coridor.Y + 1 < w.GetLength(1))
                StatusCell(coridor.X, coridor.Y + 1, w[coridor.X, coridor.Y + 1]);
            Agents.status[coridor.X, coridor.Y] = Agents.Status.Visited;
        }
        
        /// <summary>
        /// определение статуса конкретной клетки
        /// </summary>
        /// <param name="a">координата клетки по х</param>
        /// <param name="b">координата клетки по у</param>
        /// <param name="w">массив стен</param>
        public static void StatusCell(int a, int b, bool w)
        {
            if (Agents.status[a, b] == Agents.Status.Unknown)
            {
                if (w)
                {
                    Agents.freeCoridors.Add(new Point(a, b));
                    Agents.status[a, b] = Agents.Status.Free;
                }
                else
                    Agents.status[a, b] = Agents.Status.Wall;
            }
        }        
        
        /// <summary>
        /// вычисляем точку в которую должен пойти робот на следующем шаге
        /// </summary>
        /// <param name="ag"></param>
        /// <returns></returns>
        public static Point CalcNextRobPosition(SingleAgent ag)
        {
            var weight = new int[Agents.status.GetLength(0), Agents.status.GetLength(1)];
            weight[ag.robPosition.X, ag.robPosition.Y] = 1;
            var startPoint = CalcAllPosibleMove(weight);
            return FindPosToMove(weight, startPoint);
        }

        /// <summary>
        /// вычисляем все возможные перемещения робота до целевой точки
        /// целеыая точка - ближайшая точка со статусом Free
        /// </summary>
        public static Point CalcAllPosibleMove(int[,] w)
        {
            int check = 1;
            Point goalPoint;

            while(true)
            {
                for(int i = 0; i < w.GetLength(0); i++)
                    for(int j = 0; j < w.GetLength(1); j++)
                    {
                        if(w[i, j] == check)
                        {
                            if (i - 1 >= 0)
                            {
                                goalPoint = CheckNextPosiblePos(w, i, j, -1, 0);
                                if (goalPoint != new Point(i, j))
                                    return goalPoint;
                            }
                                
                            if (i + 1 < w.GetLength(0))
                            {
                                goalPoint = CheckNextPosiblePos(w, i, j, 1, 0);
                                if (goalPoint != new Point(i, j))
                                    return goalPoint;
                            }                              
                            if (j - 1 >= 0)
                            {
                                goalPoint = CheckNextPosiblePos(w, i, j, 0, -1);
                                if (goalPoint != new Point(i, j))
                                    return goalPoint;
                            }

                            if (j + 1 < w.GetLength(1))
                            {
                                goalPoint = CheckNextPosiblePos(w, i, j, 0, 1);
                                if (goalPoint != new Point(i, j))
                                    return goalPoint;
                            }
                        }
                    }
                check++;
            }
        }

        /// <summary>
        /// проверяем можно ли пройти в соседнюю клетку
        /// если можно пройти то изменяем соответствующее значение в массиве w на check+1
        /// если найдена целевая точка возвращаем ее, в противном случае возвращаем предыдущую точку
        /// </summary>
        public static Point CheckNextPosiblePos(int[,] w, int a, int b, int rA, int rB)
        {
            if (Agents.status[a + rA, b + rB] == Agents.Status.Free) 
            {
                w[a + rA, b + rB] = w[a, b] + 1;
                return new Point(a + rA, b + rB);
            }
            else if(Agents.status[a + rA, b + rB] == Agents.Status.Visited && w[a + rA, b + rB] == 0)
                w[a + rA, b + rB] = w[a, b] + 1;
            return new Point(a, b);


        }

        /// <summary>
        /// находит точку в которую должен переместиться робот по пути к целевой точке
        /// </summary>
        /// <returns></returns>
        public static Point FindPosToMove(int[,] w, Point sPoint)
        {
            int check = w[sPoint.X, sPoint.Y];
            Point goalPoint = new Point();
            while(check != 1)
            {
                check--;
                goalPoint = new Point(sPoint.X, sPoint.Y);
                if(sPoint.X - 1 >= 0 && w[sPoint.X -1, sPoint.Y] == w[sPoint.X, sPoint.Y] - 1)
                {
                    sPoint = new Point(sPoint.X - 1, sPoint.Y);
                    continue;
                }
                if (sPoint.X + 1 < w.GetLength(0) && w[sPoint.X + 1, sPoint.Y] == w[sPoint.X, sPoint.Y] - 1)
                {
                    sPoint = new Point(sPoint.X + 1, sPoint.Y);
                    continue;
                }
                if (sPoint.Y - 1 >= 0 && w[sPoint.X, sPoint.Y - 1] == w[sPoint.X, sPoint.Y] - 1)
                {
                    sPoint = new Point(sPoint.X, sPoint.Y - 1);
                    continue;
                }
                if (sPoint.Y + 1 < w.GetLength(1) && w[sPoint.X, sPoint.Y + 1] == w[sPoint.X, sPoint.Y] - 1)
                {
                    sPoint = new Point(sPoint.X, sPoint.Y + 1);
                    continue;
                }
            }
            //return sPoint;
            return goalPoint;
        }

    }


}
