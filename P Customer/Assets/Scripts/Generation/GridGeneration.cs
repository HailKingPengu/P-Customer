using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GridGeneration : MonoBehaviour
{

    //[SerializeField] private GameObject emptyPrefab; // 0
    //[SerializeField] private GameObject builtOnPrefab; // 1
    //[SerializeField] private GameObject forestPrefab; // 2
    //[SerializeField] private GameObject waterPrefab; // 3

    [SerializeField] private GameObject[] tilePrefabs;

    private int[,] tileState;

    [SerializeField] private int mapXSize;
    [SerializeField] private int mapYSize;

    void Start()
    {

        tileState = new int[mapXSize, mapYSize];

        for(int x = 0; x < mapXSize; x++)
        {
            for (int y = 0; y < mapYSize; y++)
            {
                tileState[x, y] = Random.Range(0, 4);


                GameObject newTile = Instantiate(tilePrefabs[tileState[x,y]]);

                newTile.transform.position = new Vector3(x, 0, y);
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
