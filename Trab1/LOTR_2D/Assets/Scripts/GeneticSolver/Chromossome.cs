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
        public double totalAchievementTime = 0;

        public Chromossome(System.Random Randomizer)
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
            
            /* --- Debugger para conferir número de cada hobbit em um cromossomo (APAGAR DEPOIS)---
            
            int cont_Frodo = 0;
            int cont_Sam = 0;
            int cont_Merry = 0;
            int cont_Pippin = 0;

            for (int i = 0; i < Steps.Count(); i++)
            {
                for (int j = 0; j < Steps[i].chosenHobbits.Count(); j++)
                {
                    if (Steps[i].chosenHobbits[j].name == "Frodo")
                    {
                        cont_Frodo++;
                    }
                    else if (Steps[i].chosenHobbits[j].name == "Sam")
                    {
                        cont_Sam++;
                    }
                    else if (Steps[i].chosenHobbits[j].name == "Merry")
                    {
                        cont_Merry++;
                    }
                    else if (Steps[i].chosenHobbits[j].name == "Pippin")
                    {
                        cont_Pippin++;
                    }
                }
            } 
            
            Console.WriteLine("Numero de Frodo's: " + cont_Frodo);
            Console.WriteLine("Numero de Sam's: " + cont_Sam);
            Console.WriteLine("Numero de Merry's: " + cont_Merry);
            Console.WriteLine("Numero de Pippin's: " + cont_Pippin);
            
            */

            for (int i = 0; i < Steps.Count(); i++)
            {
                calculateAchievementTime();
            }
        }

        public void calculateAchievementTime()
        {
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