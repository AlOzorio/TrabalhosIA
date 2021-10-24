using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int w;
    public int h;
    private int[,] gridArray;
    public GridNode[,] nodes;
    public float cellSize;

    [SerializeField] TextAsset mapFile;
    [SerializeField] GameObject tilePrefab;

    public void GenerateGrid(int w, int h, float cellSize)
    {
        this.w = w;
        this.h = h;
        this.cellSize = cellSize;

        gridArray = new int[w,h];
        nodes = new GridNode[w, h];

        for(int x = 0; x < gridArray.GetLength(0) ; x++)
        {
            for(int y = gridArray.GetLength(1); y > 0; y--)
            {
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y - 1), Color.white, Mathf.Infinity);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, Mathf.Infinity);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, 0), GetWorldPosition(w,0), Color.white, Mathf.Infinity);
        Debug.DrawLine(GetWorldPosition(w, h), GetWorldPosition(w,0), Color.white, Mathf.Infinity);

        PaintMap();
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }

    public List<GridNode> GetNeighbours(GridNode node)
    {
        List<GridNode> neighbours = new List<GridNode>();
        
        if(node.y + 1 < h && nodes[node.x, node.y + 1].type != '#' && nodes[node.x, node.y + 1].type != 'P')
        {
            neighbours.Add(nodes[node.x, node.y + 1]);
        }

        if(node.y - 1 >= 0 && nodes[node.x, node.y - 1].type != '#' && nodes[node.x, node.y - 1].type != 'P')
        {
            neighbours.Add(nodes[node.x, node.y - 1]);
        }

        if(node.x + 1 < w && nodes[node.x + 1, node.y].type != '#' && nodes[node.x + 1, node.y].type != 'P')
        {
            neighbours.Add(nodes[node.x + 1, node.y]);
        }

        if(node.x - 1 >= 0 && nodes[node.x - 1, node.y].type != '#' && nodes[node.x - 1, node.y].type != 'P')
        {
            neighbours.Add(nodes[node.x - 1, node.y]);
        }

        return neighbours;
    }

    private void PaintMap()
    {
        string realMap = mapFile.text.Replace('\n', ' ').Replace('\r', ' ');
        int index = 0;

        for(int y = 70; y > 0; y--)
        {
            for(int x = 0; x < 200; x++)
            {
                if(realMap[index] == ' ')
                {
                    index+=2;
                }
                GameObject tileInsntance = Instantiate(tilePrefab, new Vector3(x + 0.5f, y - 0.5f, 0f), Quaternion.identity);
                nodes[x, y - 1] = new GridNode(x, y - 1, realMap[index]);
                nodes[x, y - 1].mapTile = tileInsntance;
                SetColor(realMap[index], tileInsntance);
                tileInsntance.transform.SetParent(gameObject.transform);
                index++;
            }
        }
    }

    private void SetColor(char terrain, GameObject tile)
    {
        if(terrain == '#' || terrain == 'P')
        {
            tile.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else if(terrain == '.')
        {
            tile.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else if(terrain == 'R')
        {
            tile.GetComponent<SpriteRenderer>().color = Color.grey;
        }
        else if(terrain == 'V')
        {
            tile.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if(terrain == 'M')
        {
            tile.GetComponent<SpriteRenderer>().color = Color.black;
        }
        else
        {
            tile.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }
}
