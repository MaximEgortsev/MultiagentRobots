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

            for(int i = 0; i < operation; i++)
            {
                agents[i].robPosition = freeCoridors[0];
                SingleAgent.CheckNextCells(freeCoridors[0], w);
                freeCoridors.Remove(freeCoridors[0]);
            }

            


        }
        

    }



    public class SingleAgent
    {
        public enum Direction
        {
            None,
            Up,
            Down,
            Left,
            Right
        }

        public Direction robDirection;
        public List<Point> robotWay = new List<Point>();
        public int openCoridors = 0;
        public Point robPosition = new Point();

        public SingleAgent(Point p)
        {
            robDirection = Direction.None;
            robPosition = p;
        }


        public static void CheckNextCells(Point coridor, bool[,] w/*, SingleAgent ag*/)
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




    }



}
