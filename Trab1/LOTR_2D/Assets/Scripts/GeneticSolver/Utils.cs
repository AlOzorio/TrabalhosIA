using System;

namespace GeneticSolver
{
    public class Utils
    {
        public static float FitnessFunction(Chromossome c)
        {
            float tempoTotal = 0;
            float fitnessValue;
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