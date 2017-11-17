using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MultiagentRobots
{
    public class Robots
    {

        public int robAmount = 0;

        public enum Status
        {
            Unknown,
            Wall,
            Free,
            Visited,
        }

        public  Status[,] status = new Status[0, 0];

        public Robots(bool[,] wall, Point p, int v)
        {
            status = new Status[wall.GetLength(0), wall.GetLength(1)];
            status[p.X, p.Y] = Status.Free;


            robAmount = v;

            var rob = new SingleRobot[robAmount];
            for (int i = 0; i < robAmount; i++)
                rob[i] = new SingleRobot(p/*, wall*/);

            RoadCalculate(rob, wall);
        }

        public void RoadCalculate(SingleRobot[] rob, bool[,] wall)
        {
            //хранит непосещенные коридоры
            var freeCoridors = new List<Point>();
            freeCoridors.Add(rob[0].robPosition);

            while (freeCoridors.Count() != 0)
            {
                var operation = OperationAmount(freeCoridors.Count, robAmount);




                for (int i = 0; i < operation; i++)
                {
                    //if (rob[i].robCondition == SingleRobot.Condition.Free)
                    {
                        //добавить функцию вычисляющую ближайшую свободную клетку
                        
                        try
                        {
                            Point interesrPoint = freeCoridors[0];
                            CheckCell(interesrPoint, wall, freeCoridors);
                        }
                        catch { break; }
                    }
                }




                
            }


        }


        public int OperationAmount(int len, int freeR)
        {
            if (freeR <= len)
                return freeR;
            else return len;
        }

        public void CheckCell(Point interesrPoint, bool[,] wall, List<Point> freeCoridors)
        {
            if (interesrPoint.X - 1 >= 0)
                status[interesrPoint.X - 1, interesrPoint.Y] = SingleRobot.StatusCell(interesrPoint.X - 1, interesrPoint.Y, 
                    wall[interesrPoint.X - 1, interesrPoint.Y], freeCoridors, status);
            if (interesrPoint.X + 1 < wall.GetLength(0))
                status[interesrPoint.X + 1, interesrPoint.Y] = SingleRobot.StatusCell(interesrPoint.X + 1, interesrPoint.Y, 
                    wall[interesrPoint.X + 1, interesrPoint.Y], freeCoridors, status);
            if (interesrPoint.Y - 1 >= 0)
                status[interesrPoint.X, interesrPoint.Y - 1] = SingleRobot.StatusCell(interesrPoint.X, interesrPoint.Y - 1, 
                    wall[interesrPoint.X, interesrPoint.Y - 1], freeCoridors, status);
            if (interesrPoint.X + 1 < wall.GetLength(1))
                status[interesrPoint.X, interesrPoint.Y + 1] = SingleRobot.StatusCell(interesrPoint.X, interesrPoint.Y + 1, 
                    wall[interesrPoint.X, interesrPoint.Y + 1], freeCoridors, status);
            status[interesrPoint.X, interesrPoint.Y] = Status.Visited;
            freeCoridors.Remove(freeCoridors[0]);
        }
    }



    public class SingleRobot
    {
        //public enum Condition
        //{
        //    Free,
        //    Busy
        //}

        public enum Direction
        {
            None,
            Up,
            Down,
            Left,
            Right
        }

        //public Condition robCondition;
        public Direction robDirection;
        public List<Point> robotWay = new List<Point>();
        public int openCoridors = 0;
        public Point robPosition = new Point();

        public SingleRobot(Point p)
        {
            //robCondition = Condition.Free;
            robDirection = Direction.None;
            robPosition = p;
        }

        public static Robots.Status StatusCell(int a, int b, bool w, List<Point> freeCoridors, Robots.Status[,] status)
        {
            if ( status[a, b] == Robots.Status.Unknown)
            {
                if (w)
                {
                    freeCoridors.Add(new Point(a, b));
                    return Robots.Status.Free;
                }
                else
                    return Robots.Status.Wall;
            }
            else
                return status[a, b];
        }


    }
}
