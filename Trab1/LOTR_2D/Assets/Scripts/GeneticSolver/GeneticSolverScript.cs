using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeneticSolver;
using UnityEngine;
using UnityEngine.Assertions;
using Debug = UnityEngine.Debug;

public class GeneticSolverScript : MonoBehaviour
{
    List<Chromossome> Population = new List<Chromossome>();
    List<Chromossome> Survivors = new List<Chromossome>();
    public static Chromossome bestChromossome;
    private double bestFitness = 0;
    private double bestTime = Double.PositiveInfinity;
    private System.Random Randomizer;
    private int iterationNumber = 0;
    private float LogWaitTime = 1;
    private Stopwatch chronometer = new Stopwatch();
    
    //Variáveis de configuração do algoritmo genético
    [SerializeField] private int maxPopulationSize; //1000
    [SerializeField] private int maxAllowedSurvivors; //10
    [SerializeField] private string stopCondition; //"maxIterations" ou "fitnessThreshold"
    [SerializeField] private double fitnessThreshold; //Máximo 0.0024..alguma coisa.. (Mais que isso TRAVA O PROGRAMA)
    [SerializeField] private long maxWaitingTime; //20
    [SerializeField] private int maxIterations; //100000
    [SerializeField] private float mutationChance; //0.05f
    

    public IEnumerator Solve(System.Random Randomizer)
    {
        this.Randomizer = Randomizer;
        Debug.Log("Cálculo do melhor tempo para realizar as etapas:");
        yield return new WaitForSeconds(LogWaitTime);

        //Validação dos parâmetros gerais
        if (maxPopulationSize <= 0)
        {
            Debug.Log("ERRO - Tamanho da população inválido");
            UnityEditor.EditorApplication.ExitPlaymode();
        }

        if (maxAllowedSurvivors <= 0 || maxAllowedSurvivors >= maxPopulationSize)
        {
            Debug.Log("ERRO - Tamanho da população de sobreviventes inválido");
            UnityEditor.EditorApplication.ExitPlaymode();
        }

        if (mutationChance < 0 || mutationChance >= 1)
        {
            Debug.Log("ERRO - Chance de mutação inválida");
            UnityEditor.EditorApplication.ExitPlaymode();
        }
            
        chronometer.Start();
        GenerateFirstPopulation();

        if (stopCondition == "fitnessThreshold")
        {
            if (fitnessThreshold > 0 && maxWaitingTime > 0)
            {
                while (bestFitness < fitnessThreshold)
                {
                    if (chronometer.ElapsedMilliseconds < maxWaitingTime * 1000)
                    {
                        SelectBestChromossomes();
                        CrossOver();
                        Mutation();
                    }
                    else
                    {
                        Debug.Log("Tempo máximo de " + maxWaitingTime + " segundos expirado");
                        break;
                    }
                }
            }
            else
            {
                if (fitnessThreshold <= 0)
                    Debug.Log("ERRO - Defina um limiar para a função fitness válido");
                if (maxWaitingTime <= 0)
                    Debug.Log("ERRO - Defina um tempo máximo de expiração válido");
                UnityEditor.EditorApplication.ExitPlaymode();
            }
        }
        else if (stopCondition == "maxIterations")
        {
            if (maxIterations > 0)
            {
                while (iterationNumber < maxIterations)
                {
                    SelectBestChromossomes();
                    CrossOver();
                    Mutation();
                }
            }
            else
            {
                Debug.Log("ERRO - Defina um número máximo de iterações válido");
                UnityEditor.EditorApplication.ExitPlaymode();
            }
        }
        else
        {
            Debug.Log("ERRO - Defina uma condição de parada válida");
            UnityEditor.EditorApplication.ExitPlaymode();
        }
        chronometer.Stop();
        bestTime = Utils.ReverseFitnessToTime(bestFitness);
        Debug.Log("Fim (" + iterationNumber + " iterações)");
        Debug.Log("Melhor tempo: " + bestTime + " minutos");
        GetComponent<Program>().Invoke("ExibitTotalTime", 0);
        showBestChromossome();
    }


    //Gerador da primeira população
    private void GenerateFirstPopulation()
    {
        while (Population.Count() < maxPopulationSize)
            addValidChromossome();
    }

    
    //Testa vários cromossomos até adicionar um cromossomo válido
    public void addValidChromossome()
    {
        Chromossome newChromossome = new Chromossome();
        while (!Utils.IsValidChromossome(newChromossome))
        {
            newChromossome.ClearChromossome();
            newChromossome.GenerateRandomChromossome(Randomizer);
            iterationNumber++;
        }
        Population.Add(newChromossome);
        CompareBestChromossome(newChromossome);
    }
    

    //Atualizador do melhor cromossomo
    private void CompareBestChromossome(Chromossome c)
    {
        if (Utils.FitnessFunction(c) > bestFitness)
        {
            chronometer.Stop();
            chronometer.Start();
            bestFitness = Utils.FitnessFunction(c);
            bestChromossome = c;
            //---Tentar printar novo melhor tempo "ao vivo"---
            Debug.Log("Novo melhor tempo: " + c.totalAchievementTime + " minutos");
        }
    }


    //Selecionador dos melhores indivíduos
    private void SelectBestChromossomes()
    {
        Survivors.Clear();
        Population = Population.OrderByDescending(c => Utils.FitnessFunction(c)).ToList();
        for (int i = 0; i < maxAllowedSurvivors; i++)
        {
            Survivors.Add(Population[i]);
        }
        Population.Clear();
        Population = Survivors.ToList();
    }

    
    //Mutação
    private void Mutation()
    // ----- Acho que está caindo em loop a mutação -----
    {
        for (int i = 0; i < Population.Count(); i++)
        {
            if (Randomizer.Next(0, 100) / 100 <= mutationChance) //5% de chance padrão
            {
                //Inverte a ordem
                for (int j = 0; j < Population[i].Steps.Count(); j++)
                {
                    (Population[i].Steps[j].chosenHobbits) = (Population[i].Steps[15-j].chosenHobbits);
                }
                
                Population[i].calculateAchievementTime();
                
                //Adiciona o cromossomo mutado se for válido
                if (Utils.IsValidChromossome(Population[i]))
                    CompareBestChromossome(Population[i]);
                
                //Caso contrário adiciona-se um cromossomo válido substituto para manter o mesmo número da população
                else
                {
                    Population.Remove(Population[i]);
                    addValidChromossome();
                }
                iterationNumber++;
            }
        }
    }
    

    //Cross-over
    private void CrossOver()
    {
        //---Depois multiplicar maxPopulationSize por um fator < 1 para sobrar espaço para a mutação---
        while (Population.Count() < maxPopulationSize)
        {
            List<Chromossome> Fathers = new List<Chromossome>();
            List<Chromossome> Mothers = new List<Chromossome>();

            //Caso haja um número ímpar de sobreviventes, adiciona um cromossomo válido
            if (maxAllowedSurvivors % 2 != 0)
                addValidChromossome();

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
                        (Fathers[i].Steps[j].chosenHobbits, Mothers[i].Steps[j].chosenHobbits) = (Mothers[i].Steps[j].chosenHobbits, Fathers[i].Steps[j].chosenHobbits);
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
            iterationNumber++;
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
    

    public double GetBestTime()
    {
        return bestTime;
    }
    
    public double GetBestFitness()
    {
        return bestFitness;
    }
    
}
