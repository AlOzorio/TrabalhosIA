using System;
using System.Collections.Generic;
using System.Linq;
using GeneticSolver;
using UnityEngine;

public class GeneticSolverScript : MonoBehaviour
{
    public static Chromossome bestChromossome;
    private static System.Random Randomizer = new System.Random();
    public double bestFitness = 0;
    
    public void Solve()
    {
        int Iteracoes = 0;

        List<Chromossome> Population = new List<Chromossome>();
        Debug.Log("Cálculo da melhor tmepo para raelizar as etapas:");
        
        while (Iteracoes < 1000)
        {
            Chromossome c = new Chromossome(Randomizer);
            if (Utils.isValidChromossome(c))
            {
                Population.Add(c);
                Iteracoes++;
                if (Utils.FitnessFunction(c) > bestFitness)
                {
                    bestFitness = Utils.FitnessFunction(c);
                    bestChromossome = c;
                    Debug.Log("Novo melhor tempo: " + 1/Utils.FitnessFunction(c)+ " minutos");
                }
            }
        }
        
        Debug.Log("Fim");
        Debug.Log("Melhor tempo: " + 1/bestFitness + " minutos");
        
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
