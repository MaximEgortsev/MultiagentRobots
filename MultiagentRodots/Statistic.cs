using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiagentRobots
{
    public class Statistic
    {

        /// <summary>
        /// количество роботов
        /// </summary>
        public int[] robAmount;
        /// <summary>
        /// количество открытых коридоров
        /// </summary>
        public int[,] robOpenCoridors;
        /// <summary>
        /// количество тактов прохождения лабиринта
        /// </summary>
        public int[] steps;

        public Statistic(int amount)
        {
            robAmount = new int[amount];
            for (int i = 0; i < amount; i++)
                robAmount[i] = i+1;
            steps = new int[amount];
            robOpenCoridors = new int[amount, amount];
        }
        
        public  void func1(SingleAgent[] ag)
        {
            for(int i = 0; i < ag.Count(); i++)
            {
                robOpenCoridors[ag.Count() - 1, i] = ag[i].openCoridors;
            }
            steps[ag.Count() - 1] = ag[0].takt;
        }


    }
}
