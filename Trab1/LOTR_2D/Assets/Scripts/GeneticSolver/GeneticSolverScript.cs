﻿using System;
using System.Collections.Generic;
using System.Linq;
using GeneticSolver;
using UnityEngine;

public class GeneticSolverScript : MonoBehaviour
{
    List<Chromossome> Population = new List<Chromossome>();
    List<Chromossome> Survivors = new List<Chromossome>();
    public static Chromossome bestChromossome;
    public double bestFitness = 0;
    public double bestTime;
    private int maxPopulationSize;
    private int maxAllowedSurvivors;
    private System.Random Randomizer;


    public void Solve(int maxPopulationSize, int maxAllowedSurvivors, double fitnessThreshold, System.Random Randomizer)
    {
        this.maxPopulationSize = maxPopulationSize;
        this.maxAllowedSurvivors = maxAllowedSurvivors;
        this.Randomizer = Randomizer;

        Debug.Log("Cálculo do melhor tempo para realizar as etapas:");
        GenerateFirstPopulation();
        
        while (bestFitness < fitnessThreshold)
        {
            SelectBestChromossomes();
            CrossOver();
        }
        
        bestTime = Utils.ReverseFitnessToTime(bestFitness);
        Debug.Log("Fim");
        Debug.Log("Melhor tempo: " + bestTime + " minutos");
        
        showBestChromossome();
    }


    //Gerador da primeira população
    private void GenerateFirstPopulation()
    {
        while (Population.Count() < maxPopulationSize)
        {
            Chromossome c = new Chromossome();
            c.GenerateRandomChromossome(Randomizer);
            if (Utils.IsValidChromossome(c))
            {
                Population.Add(c);
                CompareBestChromossome(c);
            }
        }
    }


    //Atualizador do melhor cromossomo
    private void CompareBestChromossome(Chromossome c)
    {
        if (Utils.FitnessFunction(c) > bestFitness)
        {
            bestFitness = Utils.FitnessFunction(c);
            bestChromossome = c;
            Debug.Log("Novo melhor tempo: " + c.totalAchievementTime + " minutos");
        }
    }


    //Selecionador dos melhores indivíduos
    private void SelectBestChromossomes()
    {
        Population = Population.OrderByDescending(c => Utils.FitnessFunction(c)).ToList();
        for (int i = 0; i < maxAllowedSurvivors; i++)
        {
            Survivors.Add(Population[i]);
        }
        
        Population.Clear();
        Population = Survivors.ToList();
    }


    //Cross-over
    private void CrossOver()
    {
        //---Depois multiplicar maxPopulationSize por um fator < 1 para sobrar espaço para a mutação---
        while (Population.Count() < maxPopulationSize)
        {
            List<Chromossome> Fathers = new List<Chromossome>();
            List<Chromossome> Mothers = new List<Chromossome>();

            //Caso haja um número ímpar de sobreviventes, adiciona mais um cromossomo
            if (maxAllowedSurvivors % 2 != 0)
            {
                Chromossome ExtraChomossome = new Chromossome();
                ExtraChomossome.GenerateRandomChromossome(Randomizer);
                Survivors.Add(ExtraChomossome);
            }

            //Divide os sobreviventes em duas metades
            for (int i = 0; i < Survivors.Count(); i++)
            {
                if (i < Survivors.Count() / 2)
                    Fathers.Add(Survivors[i]);
                else
                    Mothers.Add((Survivors[i]));
            }

            //Troca os genes intercaladamente e gera possíveis novos cromossomos
            for (int i = 0; i < Survivors.Count() / 2; i++)
            {
                Chromossome newChromossomeSon = new Chromossome();
                Chromossome newChromossomeDaughter = new Chromossome();
                for (int j = 0; j < Survivors[i].Steps.Count(); j++)
                {
                    if (j % 2 == 0)
                    {
                        (Fathers[i].Steps[j], Mothers[i].Steps[j]) = (Mothers[i].Steps[j], Fathers[i].Steps[j]);
                    }
                }

                newChromossomeSon = Fathers[i];
                newChromossomeDaughter = Mothers[i];
                
                newChromossomeSon.calculateAchievementTime();
                newChromossomeDaughter.calculateAchievementTime();

                if (Utils.IsValidChromossome(newChromossomeSon))
                {
                    Population.Add(newChromossomeSon);
                    CompareBestChromossome(newChromossomeSon);
                }

                if (Utils.IsValidChromossome(newChromossomeDaughter))
                {
                    Population.Add(newChromossomeDaughter);
                    CompareBestChromossome(newChromossomeSon);
                }
            }
            Survivors.Clear();
        }
    }


    //Exibidor do melhor cromossomo
    public void showBestChromossome()
    {
        for (int i = 0; i < bestChromossome.Steps.Count(); i++)
        {
            string message = "Os hobbits da etapa " + bestChromossome.Steps[i].number + " foram: ";
            for (int j = 0; j < bestChromossome.Steps[i].chosenHobbits.Count(); j++)
            {
                message += bestChromossome.Steps[i].chosenHobbits[j].name + " ";
            }
            Debug.Log(message);
        }
    }

    
}
