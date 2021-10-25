using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
//using UnityEngine.Animations;

namespace GeneticSolver
{
    public class Chromossome // = Indivíduo
    {
        private List<Hobbit> Hobbits = new List<Hobbit>();
        public List<Step> chromossome = new List<Step>();

        private System.Random Randomizer = new System.Random();

        public Chromossome()
        {
            Hobbits.Add((new Hobbit("Frodo")));
            Hobbits.Add((new Hobbit("Sam")));
            Hobbits.Add((new Hobbit("Merry")));
            Hobbits.Add((new Hobbit("Pippin")));

            //Cria todos os passos necessários
            chromossome.Add(new Step('2', 10));
            chromossome.Add(new Step('3', 30));
            chromossome.Add(new Step('4', 60));
            chromossome.Add(new Step('5', 65));
            chromossome.Add(new Step('6', 70));
            chromossome.Add(new Step('7', 75));
            chromossome.Add(new Step('8', 80));
            chromossome.Add(new Step('9', 85));
            chromossome.Add(new Step('A', 90));
            chromossome.Add(new Step('B', 95));
            chromossome.Add(new Step('C', 100));
            chromossome.Add(new Step('D', 110));
            chromossome.Add(new Step('E', 120));
            chromossome.Add(new Step('F', 130));
            chromossome.Add(new Step('G', 140));
            chromossome.Add(new Step('H', 150));            

            for (int i = 0; i < Hobbits.Count(); i++)
            {
                //Distribui os hobbits em todas as etapas de forma que nao se tenha o mesmo hobbit em mais de 10 etapas diferentes
                List<int> possiblechromossome = new List<int>(Enumerable.Range(0, 15));
                while (Hobbits[i].energyPoints > 0)
                {
                    int choosedStep = Randomizer.Next(0, possiblechromossome.Count);
                    chromossome[choosedStep].chosenHobbits.Add(Hobbits[i]);
                    possiblechromossome.Remove(choosedStep);
                    Hobbits[i].energyPoints -= 1;
                }
            }

            for (int i = 0; i < chromossome.Count(); i++)
            {
                calculateAchievementTime(chromossome[i]);
            }
        }

        public void calculateAchievementTime(Step step)
        {
            float totalAgility = 0;
            for (int i = 0; i < step.chosenHobbits.Count(); i++)
            {
                totalAgility += step.chosenHobbits[i].agility;
            }
            step.achievementTime = step.difficulty / totalAgility;
        }
    }
}