using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Types : MonoBehaviour
{
    public List<Vector2> checkpoints;
    public static Dictionary<char, int> tileTypeToInt = new Dictionary<char, int>() {{'.', 1}, {'R', 5},
    {'V', 10}, {'M', 200}, {'P', Int32.MaxValue}, {'#', Int32.MaxValue}, {'\0', 0}};

    public List<Vector4> color;
}
