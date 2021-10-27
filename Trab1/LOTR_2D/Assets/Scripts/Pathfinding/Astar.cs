using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    public Grid grid;
    private int colorIndex;

    [SerializeField] private float waitTime;
    public int travelTime;
    
    private void Awake()
    {
        colorIndex = 0;
        grid = GetComponent<Grid>();
        grid.GenerateGrid(grid.w, grid.h, grid.cellSize);
    }

    public IEnumerator ThereAndBackAgain()
    {
        List<Vector2> checkpoints = GetComponent<Types>().checkpoints;
        for(int i = 0; i < 17; i++)
        {
            yield return new WaitForSeconds(waitTime);
            FromSourceToDestiny(grid.nodes[(int)checkpoints[i][0], (int)checkpoints[i][1]], grid.nodes[(int)checkpoints[i + 1][0], (int)checkpoints[i + 1][1]]);
        }
        GetComponent<Program>().Invoke("GetTime", 0);
    }

    private void FromSourceToDestiny(GridNode start, GridNode end)
    {
        List<GridNode> open = new List<GridNode>();
        List<GridNode> closed = new List<GridNode>();

        open.Add(start);

        while(open.Count > 0)
        {
            GridNode current = open[0];
            for(int i = 1; i < open.Count; i++)
            {
                if(open[i].GetFCost() < current.GetFCost() || open[i].GetFCost() == current.GetFCost() && open[i].h < current.h)
                {
                    current = open[i];
                }
            }

            open.Remove(current);
            closed.Add(current);

            if(current == end)
            {
                GetPath(start, end);
                return;
            }

            foreach(GridNode neighbour in grid.GetNeighbours(current))
            {
                if(closed.Contains(neighbour))
                {
                    continue;
                }
                
                int newCost = current.g + GetNodeDistance(current, neighbour);

                if(Types.tileTypeToInt.ContainsKey(neighbour.type))
                {
                    newCost += Types.tileTypeToInt[neighbour.type];
                }
                
                if(newCost < neighbour.g || !open.Contains(neighbour))
                {
                    neighbour.g = newCost;
                    neighbour.h = GetNodeDistance(neighbour, end);
                    neighbour.parent = current;

                    if(open.Contains(neighbour) == false)
                    {
                        open.Add(neighbour);
                    }
                } 
            }
        }
    }

    private void GetPath(GridNode start, GridNode end)
    {
        List<GridNode> fullPath = new List<GridNode>();
        GridNode current = end;

        while(current != start)
        {
            fullPath.Add(current);
            current = current.parent;
        }

        fullPath.Reverse();
        
        for(int i = 0; i < fullPath.Count; i++)
        {
            fullPath[i].mapTile.GetComponent<SpriteRenderer>().color = GetComponent<Types>().color[colorIndex];

            if(Types.tileTypeToInt.ContainsKey(fullPath[i].type))
            {
                travelTime += Types.tileTypeToInt[fullPath[i].type];
            }
        }

        colorIndex++;
    }

    private int GetNodeDistance(GridNode origin, GridNode destiny)
    {
        int distX = Mathf.Abs(origin.x - destiny.x);
        int distY = Mathf.Abs(origin.y - destiny.y);

        return distX + distY;
    }
}
