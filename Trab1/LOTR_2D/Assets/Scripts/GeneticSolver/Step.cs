using System;
using System.Collections.Generic;

namespace GeneticSolver
{
    public class Step
    {
        public char number; //{ get; set; } ?
        public int difficulty; //{ get; set; } ?
        public List<Hobbit> chosenHobbits = new List<Hobbit>(); //{ get; set; } ?
        public double achievementTime = Double.PositiveInfinity; //{ get; set; } ?

        public Step(char number, int difficulty)
        {
            this.number = number;
            this.difficulty = difficulty;
        }
    }
}