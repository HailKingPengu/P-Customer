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
    //[SerializeField] private GameObject roadPrefab; // 4

    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private GameObject[] buildingPrefabs;

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

                float randomizedWildness = wildness + Random.Range(-10, 10);

                if (y % 4 == 0 && wildness < 50)
                {

                    if (x == riverPosition)
                    {
                        //bridge
                        GameObject newTile = Instantiate(tilePrefabs[5], transform);

                        newTile.transform.position = new Vector3(x, 0, y);
                    }
                    else
                    {
                        //road
                        GameObject newTile = Instantiate(tilePrefabs[4], transform);

                        newTile.transform.position = new Vector3(x, 0, y);
                    }
                }
                else if (x == riverPosition)
                {
                    //river
                    GameObject newTile = Instantiate(tilePrefabs[3], transform);

                    newTile.transform.position = new Vector3(x, 0, y);
                }
                else if (x % 3 == 0 && wildness < 50)
                {
                    //road
                    GameObject newTile = Instantiate(tilePrefabs[4], transform);

                    newTile.transform.position = new Vector3(x, 0, y);
                }
                else if (randomizedWildness <= 40)
                {
                    //buildings
                    GameObject newTile = Instantiate(tilePrefabs[1], transform);

                    newTile.transform.position = new Vector3(x, 0, y);

                    SpawnBuilding(newTile, randomizedWildness);
                }
                else if (randomizedWildness <= 60)
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

        parentTile.transform.rotation = Quaternion.Euler(0, 90 * (int)Random.Range(0, 4), 0);

        float var = Random.Range(0.7f, 1f);

        Color buildingColor = new Color(
            var, var, var
        //Random.Range(0.7f, 1f),
        //Random.Range(0.7f, 1f),
        //Random.Range(0.7f, 1f)
        );

        for(int i = 0; i < (40 - wildness)/6; i++)
        {
            GameObject newFloor = Instantiate(buildingPrefabs[Random.Range(0,buildingPrefabs.Length)], parentTile.transform);

            newFloor.transform.position = new Vector3(parentTile.transform.position.x, 0.5f + 0.5f * i, parentTile.transform.position.z);
            newFloor.transform.localScale = new Vector3(0.7f, 0.7f, 1);
            newFloor.gameObject.GetComponent<MeshRenderer>().materials[0].color = buildingColor;
            newFloor.gameObject.AddComponent<floorScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
