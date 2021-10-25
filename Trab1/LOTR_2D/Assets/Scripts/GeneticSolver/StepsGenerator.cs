using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Animations;

namespace GeneticSolver
{
    public class StepsGenerator // = Indivíduo
    {
        private List<Hobbit> Hobbits;
        public List<Step> Steps;

        private System.Random Randomizer = new System.Random();

        public StepsGenerator()
        {
            Hobbits.Add((new Hobbit("Frodo")));
            Hobbits.Add((new Hobbit("Sam")));
            Hobbits.Add((new Hobbit("Merry")));
            Hobbits.Add((new Hobbit("Pippin")));

            //Cria todos os passos necessários
            Steps.Add(new Step('2', 10));
            Steps.Add(new Step('3', 30));
            Steps.Add(new Step('4', 60));
            Steps.Add(new Step('5', 65));
            Steps.Add(new Step('6', 70));
            Steps.Add(new Step('7', 75));
            Steps.Add(new Step('8', 80));
            Steps.Add(new Step('9', 85));
            Steps.Add(new Step('A', 90));
            Steps.Add(new Step('B', 95));
            Steps.Add(new Step('C', 100));
            Steps.Add(new Step('D', 110));
            Steps.Add(new Step('E', 120));
            Steps.Add(new Step('F', 130));
            Steps.Add(new Step('G', 140));
            Steps.Add(new Step('H', 150));            

            for (int i = 0; i < Hobbits.Count(); i++)
            {
                //Distribui os hobbits em todas as etapas de forma que nao se tenha o mesmo hobbit em mais de 10 etapas diferentes
                List<int> possibleSteps = new List<int>(Enumerable.Range(0, 15));
                while (Hobbits[i].energyPoints > 0)
                {
                    int choosedStep = Randomizer.Next(0, possibleSteps.Count);
                    Steps[choosedStep].chosenHobbits.Add(Hobbits[i]);
                    possibleSteps.Remove(choosedStep);
                    Hobbits[i].energyPoints -= 1;
                }
            }
        }
    }
}