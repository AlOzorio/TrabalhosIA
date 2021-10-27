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
    double fitnessThreshold = 0.0024; //Máximo 0.0024..alguma coisa.. (Menos que isso TRAVA O PROGRAMA)
    int maxIterations = 100000;
    string stopCondition = "maxIterations"; //ou "fitnessThreshold"
    System.Random Randomizer = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        astar = GetComponent<Astar>();
        gss = GetComponent<GeneticSolverScript>();
        StartCoroutine(astar.ThereAndBackAgain());
        gss.Solve(maxPopulationSize, maxAllowedSurvivors, Randomizer, stopCondition, maxIterations: maxIterations);
    }

    public void GetTime()
    {
        gss.Solve(maxPopulationSize, maxAllowedSurvivors, Randomizer, stopCondition, fitnessThreshold, maxIterations);
        double totalTime = gss.bestTime + (double)astar.travelTime;
        Debug.Log("Tempo total gasto na viagem: " + totalTime + " minutos");
    }
}
