namespace GeneticSolver
{
    public class Utils
    {
        public static double FitnessFunction(Chromossome c)
        {
            double fitnessValue = 1 / c.totalAchievementTime;
            return fitnessValue;
        }

        public static bool IsValidChromossome(Chromossome c)
        {
            return !double.IsPositiveInfinity(c.totalAchievementTime);
        }
    }
}