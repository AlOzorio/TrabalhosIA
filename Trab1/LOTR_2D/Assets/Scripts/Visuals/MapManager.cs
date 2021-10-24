using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapManager : MonoBehaviour
{
    [SerializeField] TextAsset mapFile;
    [SerializeField] GameObject tilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        string realMap = mapFile.text.Replace('\n', ' ').Replace('\r', ' ');
        Grid grid = GetComponent<Astar>().grid;
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
                SetColor(realMap[index], tileInsntance);
                tileInsntance.transform.SetParent(gameObject.transform);
                index++;
            }
        }
    }

    public void RepaintMap(List<GridNode> fullpath)
    {
        for(int i = 0; i < fullpath.Count; i++)
        {
            Debug.Log(i);
            GameObject pathInstance = Instantiate(tilePrefab, new Vector3(fullpath[i].x + 0.5f, fullpath[i].y - 0.5f, 0f), Quaternion.identity);
            pathInstance.GetComponent<SpriteRenderer>().color = Color.red;
            pathInstance.GetComponent<SpriteRenderer>().sortingOrder = 1;;

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
        else if(terrain == '@')
        {
            tile.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            tile.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }
}
