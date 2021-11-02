using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Program : MonoBehaviour
{   
    private Astar astar;
    private GeneticSolverScript gss;
    System.Random Randomizer = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        astar = GetComponent<Astar>();
        gss = GetComponent<GeneticSolverScript>();
        GetPathfindingTime();
    }

    public void GetPathfindingTime()
    {
        StartCoroutine(astar.ThereAndBackAgain());
    }

    public void GetGeneticSolverTime()
    {
        StartCoroutine(gss.Solve(Randomizer));
    }

    public void ExibitTotalTime()
    {
        double totalTime = astar.GetTravelTime() + gss.GetBestTime();
        Debug.Log("Tempo total gasto na viagem: " + totalTime + " minutos");
    }
}
