using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiagentRodots
{
    public class Maze 
    {
        public Maze(decimal coloms, decimal rows)
        {
            walls = new bool[(int)coloms, (int)rows];
            status = new int[(int)coloms, (int)rows];
        }
        public bool[,] walls;
        public int[,] status;

        public enum Status
        {
            Unknown,
            Wall,
            Free,
            Visited,
        }

        


    }

    


}
