using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Animations;

namespace GeneticSolver
{
    public class Chromossome // = Indivíduo
    {
        private List<Hobbit> Hobbits = new List<Hobbit>();
        public List<Step> Steps = new List<Step>();
        public double totalAchievementTime = double.PositiveInfinity;

        public Chromossome()
        {
            //Cria os hobbits
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
        }

        public void GenerateRandomChromossome(System.Random Randomizer)
        {
            for (int i = 0; i < Hobbits.Count(); i++)
            {
                //Distribui os hobbits em todas as etapas de forma que nao se tenha o mesmo hobbit em mais de 10 etapas diferentes
                //Problema: Garantir de maneira 100% aleatória que haja ao menos 1 Hobbit em cada etapa

                List<int> randomSteps = new List<int>(Enumerable.Range(0, 16));
                while (Hobbits[i].energyPoints > 0)
                {
                    int choosedStep = randomSteps[Randomizer.Next(randomSteps.Count)];
                    if (Steps[choosedStep].chosenHobbits.Count() < 4) {
                        Steps[choosedStep].chosenHobbits.Add(Hobbits[i]);
                        randomSteps.Remove(choosedStep);
                        Hobbits[i].energyPoints -= 1;
                    }
                }
            }
            
            for (int i = 0; i < Steps.Count(); i++)
            {
                calculateAchievementTime();
            }
        }

        public void ClearChromossome()
        {
            for (int i = 0; i < Steps.Count(); i++)
            {
                Steps[i].chosenHobbits.Clear();
            }

            for (int i = 0; i < Hobbits.Count(); i++)
            {
                Hobbits[i].energyPoints = 10;
            }
        }

        public void calculateAchievementTime()
        {
            totalAchievementTime = 0;
            for (int i = 0; i < Steps.Count(); i++)
            {
                float totalAgility = 0;
                for (int j = 0; j < Steps[i].chosenHobbits.Count(); j++)
                    totalAgility += Steps[i].chosenHobbits[j].agility;

                Steps[i].achievementTime = Steps[i].difficulty / totalAgility;
                totalAchievementTime += Steps[i].achievementTime;
            }
        }
    }
}