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
        /// <summary>
        /// количество роботов
        /// </summary>
        public static int agentsAmount = 0;
        /// <summary>
        /// непосещенные коридоры
        /// </summary>
        public static List<Point> freeCoridors = new List<Point>();
        
        public enum Status
        {
            Unknown,
            Wall,
            Free,
            Visited,
            Goal
        }

        /// <summary>
        /// массив со стасусами каждой клетки в лабиринте
        /// </summary>
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

        /// <summary>
        ///расчитывает один шаг для каждого робота
        /// </summary>
        /// <param name="w"> массив стен</param>
        /// <param name="agents"> массив роботов</param>
        /// <param name="freeRob">список свобоных роботов</param>
        public static void RoadCalc(bool[,] w, SingleAgent[] agents, List<int> freeRob)
        {
            //расчет количества роботов для выполнения данного шага (в зависимости от свободных клеток и количества роботов)
            var operation = Math.Min(agentsAmount, freeCoridors.Count);
            //выполняется при первом заходе в лабиринт
            if (freeCoridors[0] == agents[0].startRobPos)
            {
                //Point coridor = freeCoridors[0];
                //agents[0].startRobPos = coridor;
                //SingleAgent.DeterminateStatusNextCells(coridor, w);
                //freeCoridors.Remove(coridor);
                //Point coridor = freeCoridors[0];
                //присваиваем первому роботу начальную позицию - вход в лабиринт
                agents[0].startRobPos = freeCoridors[0];
                SingleAgent.DeterminateStatusNextCells(freeCoridors[0], w);
                //удаляем посещенный коридор
                freeCoridors.RemoveAt(0);
            }
            //расчет шага после захода в лабиринт      
            else 
                for (int i = 0; i < operation; i++)
                    RoadCalcInMaze(freeRob, agents, w);

        }

        /// <summary>
        /// расчет шага для каждого робота, если они уже зашли в лабиринт
        /// </summary>
        /// <param name="freeRob">список свободных роботов</param>
        /// <param name="agents">список роботов</param>
        /// <param name="w">массив стен</param>
        public static void RoadCalcInMaze(List<int> freeRob, SingleAgent[] agents, bool[,] w)
        {
            //список с расстояниями от роботов до целевых точек
            var lenghtRobWay = new List<int>();
            //ищем кратчайшее растояие от каждого свободного робота до любой целевой точки
            for (int j = 0; j < freeRob.Count(); j++)
            {
                agents[freeRob[j]].endRobPos = SingleAgent.CalcNextRobPosition(agents[freeRob[j]]);
                lenghtRobWay.Add(agents[freeRob[j]].wayLenght);
            }
            //определяем индекс робота с минимальной ценой пути
            var index = lenghtRobWay.IndexOf(lenghtRobWay.Min());
            //перемещаем робота с следующую клетку
            agents[freeRob[index]].startRobPos = agents[freeRob[index]].endRobPos;
            //определяем статус соседних клеток
            SingleAgent.DeterminateStatusNextCells(agents[freeRob[index]].startRobPos, w);
            //удаляем посещенный коридор и робота выполнившего задачу из списков
            freeCoridors.Remove(agents[freeRob[index]].startRobPos);
            freeRob.RemoveAt(index);
        }
        
    }



    public class SingleAgent
    {
        /// <summary>
        /// записб пути пройденного робота во вермя прохождения лабиринта
        /// </summary>
        public List<Point> robotWay = new List<Point>();
        /// <summary>
        /// количество открытых коридоров
        /// </summary>
        public int openCoridors = 0;
        /// <summary>
        /// позиция робота
        /// </summary>
        public Point startRobPos = new Point();
        /// <summary>
        /// позиция, в которую робот должен переместиться
        /// </summary>
        public Point endRobPos = new Point();
        /// <summary>
        /// длина пути до ближайшей целевой точки
        /// </summary>
        public int wayLenght = 0;

        public SingleAgent(Point p)
        {
            startRobPos = p;
            endRobPos = startRobPos;
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
            weight[ag.startRobPos.X, ag.startRobPos.Y] = 1;
            var startPoint = CalcAllPosibleMove(weight, ag);
            ag.wayLenght = weight[startPoint.X, startPoint.Y];
            return FindPosToMove(weight, startPoint);
        }

        /// <summary>
        /// вычисляем все возможные перемещения робота до целевой точки
        /// целеыая точка - ближайшая точка со статусом Free
        /// </summary>
        public static Point CalcAllPosibleMove(int[,] w, SingleAgent ag)
        {
            ag.wayLenght = 1;
            Point goalPoint;
            //пока робот не достиг целевой точки 
            while(true)
            {
                for(int i = 0; i < w.GetLength(0); i++)
                    for(int j = 0; j < w.GetLength(1); j++)
                    {
                        if(w[i, j] == ag.wayLenght)
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
                ag.wayLenght++;
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
            return goalPoint;
        }

    }

}
