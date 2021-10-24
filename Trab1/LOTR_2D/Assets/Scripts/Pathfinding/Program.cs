using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Program : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Astar astar = GetComponent<Astar>();

        StartCoroutine(astar.ThereAndBackAgain());
    }
}
