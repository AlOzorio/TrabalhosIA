using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Program : MonoBehaviour
{   
    private Astar astar;
    private GeneticSolverScript gss;

    // Start is called before the first frame update
    void Start()
    {
        astar = GetComponent<Astar>();
        gss = GetComponent<GeneticSolverScript>();

        StartCoroutine(astar.ThereAndBackAgain());
    }

    public void GetTime()
    {
        gss.Solve();
        double totalTime = gss.bestTime + (double)astar.travelTime;
        Debug.Log("Tempo total gasto na viagem: " + totalTime);
    }
}
