using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Program : MonoBehaviour
{   
    private Astar astar;
    private GeneticSolverScript gss;
    
    //Variáveis de configuração do algoritmo genético
    int maxPopulationSize = 1000;
    int maxAllowedSurvivors = 10;
    double fitnessThreshold = 0.01;
    System.Random Randomizer = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        astar = GetComponent<Astar>();
        gss = GetComponent<GeneticSolverScript>();
        //StartCoroutine(astar.ThereAndBackAgain());
        gss.Solve(maxPopulationSize, maxAllowedSurvivors, fitnessThreshold, Randomizer);
    }

    public void GetTime()
    {
        gss.Solve(maxPopulationSize, maxAllowedSurvivors, fitnessThreshold, Randomizer);
        double totalTime = gss.bestTime + (double)astar.travelTime;
        Debug.Log("Tempo total gasto na viagem: " + totalTime + " minutos");
    }
}
