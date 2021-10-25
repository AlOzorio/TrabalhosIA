using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace GeneticSolver
{
    public class Step
    {
        public char number; //{ get; set; } ?
        public int difficulty; //{ get; set; } ?
        public List<Hobbit> chosenHobbits = new List<Hobbit>(); //{ get; set; } ?
        public float achievementTime; //{ get; set; } ?

        public Step(char number, int difficulty)
        {
            this.number = number;
            this.difficulty = difficulty;
        }
    }
}