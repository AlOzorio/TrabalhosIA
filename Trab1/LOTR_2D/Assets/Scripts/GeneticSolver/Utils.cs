namespace GeneticSolver
{
    public class Utils
    {
        public static double FitnessFunction(Chromossome c)
        {
            double fitnessValue = 1 / c.totalAchievementTime;
            return fitnessValue;
        }

        public static double ReverseFitnessToTime(double fitness)
        {
            return 1 / fitness;
        }

        public static bool IsValidChromossome(Chromossome c)
        {
            if (!double.IsPositiveInfinity(c.totalAchievementTime))
            {
                int cont_Frodo = 0;
                int cont_Sam = 0;
                int cont_Merry = 0;
                int cont_Pippin = 0;

                for (int i = 0; i < c.Steps.Count; i++)
                {
                    for (int j = 0; j < c.Steps[i].chosenHobbits.Count; j++)
                    {
                        if (c.Steps[i].chosenHobbits[j].name == "Frodo")
                        {
                            cont_Frodo++;
                        }
                        else if (c.Steps[i].chosenHobbits[j].name == "Sam")
                        {
                            cont_Sam++;
                        }
                        else if (c.Steps[i].chosenHobbits[j].name == "Merry")
                        {
                            cont_Merry++;
                        }
                        else if (c.Steps[i].chosenHobbits[j].name == "Pippin")
                        {
                            cont_Pippin++;
                        }
                    }
                }

                if (cont_Frodo == 10 && cont_Merry == 10 && cont_Pippin == 10 & cont_Sam == 10)
                    return true;
            }
            return false;
        }
    }
}