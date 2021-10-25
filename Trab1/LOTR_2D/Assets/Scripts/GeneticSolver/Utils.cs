using System;

namespace GeneticSolver
{
    public class Utils
    {
        public static double FitnessFunction(Chromossome c)
        {
            double tempoTotal = 0;
            double fitnessValue;
            for (int i = 0; i < c.chromossome.Count; i++)
            {
                tempoTotal += c.chromossome[i].achievementTime;
            }

            fitnessValue = 1 / tempoTotal;
            Console.WriteLine("Tempo total: ",tempoTotal);
            return fitnessValue;
        }
    }
}