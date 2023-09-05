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
    [SerializeField] private GameObject buildingPrefab;

    private int[,] tileState;
    private float[,] builtOnDegree;

    [SerializeField] private int mapXSize;
    [SerializeField] private int mapYSize;

    [SerializeField] private Vector2 centerPoint;

    [SerializeField] private int riverPosition;

    void Start()
    {

        float maxBuiltOnDegree = 0;


        float maxDistance = mapXSize;


        tileState = new int[mapXSize, mapYSize];

        builtOnDegree = new float[mapXSize, mapYSize];

        for (int x = 0; x < mapXSize; x++)
        {
            for (int y = 0; y < mapYSize; y++)
            {
                //tileState[x, y] = Random.Range(0, 4);

                builtOnDegree[x, y] = Vector2.Distance(new Vector2(x, y), centerPoint) / mapXSize;

                if(builtOnDegree[x, y] > maxBuiltOnDegree)
                {
                    maxBuiltOnDegree = builtOnDegree[x, y];
                }
            }
        }



        for (int x = 0; x < mapXSize; x++)
        {
            for (int y = 0; y < mapYSize; y++)
            {

                float wildness = builtOnDegree[x, y];

                wildness *= 100 / maxBuiltOnDegree;

                wildness += Random.Range(-10, 10);

                if (x == riverPosition)
                {
                    //river
                    GameObject newTile = Instantiate(tilePrefabs[3], transform);

                    newTile.transform.position = new Vector3(x, 0, y);
                } 
                else if (wildness <= 40)
                {
                    //buildings
                    GameObject newTile = Instantiate(tilePrefabs[1], transform);

                    newTile.transform.position = new Vector3(x, 0, y);

                    SpawnBuilding(newTile, wildness);
                }
                else if (wildness <= 70)
                {
                    //grass
                    GameObject newTile = Instantiate(tilePrefabs[0], transform);

                    newTile.transform.position = new Vector3(x, 0, y);
                }
                else
                {
                    //forest
                    GameObject newTile = Instantiate(tilePrefabs[2], transform);

                    newTile.transform.position = new Vector3(x, 0, y);
                }
            }
        }

    }

    private void SpawnBuilding(GameObject parentTile, float wildness)
    {
        for(int i = 0; i < (40 - wildness)/6; i++)
        {
            GameObject newFloor = Instantiate(buildingPrefab, parentTile.transform);

            newFloor.transform.position = new Vector3(parentTile.transform.position.x, 0.75f + 0.5f * i, parentTile.transform.position.z);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
