using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MultiagentRodots
{
    public class Robot
    {

        public Point robPosition = new Point();
        public Condition robCondition;

        public enum Condition
        {
            Free,
            Busy
        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }


        public Robot()
        {
            robCondition = Condition.Free;
        }
    }
}
