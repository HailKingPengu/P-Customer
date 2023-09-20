using System.Collections;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GridGeneration : MonoBehaviour
{

    [System.Serializable]
    public struct BuildingArray
    {
        [SerializeField] public GameObject[] floor;
    }

    public struct PowerPlantPlacements
    {
        public int x;
        public int y;
        public GameObject linkedGameObject;

        public PowerPlantPlacements(int x, int y, GameObject linkedGameObject)
        {
            this.x = x;
            this.y = y;
            this.linkedGameObject = linkedGameObject;
        }
    }

    public BuildingArray[] buildingPrefabs;
    public BuildingArray[] buildingRoofPrefabs;
    public BuildingArray[] industryPrefabs;
    public BuildingArray[] industryRoofPrefabs;

    [SerializeField] public GameObject buildingManagerPrefab;

    //[SerializeField] private GameObject emptyPrefab; // 0
    //[SerializeField] private GameObject builtOnPrefab; // 1
    //[SerializeField] private GameObject forestPrefab; // 2
    //[SerializeField] private GameObject waterPrefab; // 3
    //[SerializeField] private GameObject roadPrefab; // 4

    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private GameObject[] roadPrefabs;
    //[SerializeField] private GameObject[][] buildingPrefabs;
    //[SerializeField] private GameObject[] industryPrefabs;
    //[SerializeField] private GameObject[] industryRoofPrefabs;
    [SerializeField] private GameObject powerPlantPrefab;
    [SerializeField] private int numPowerPlants;
    private int builtPowerPlants;
    //always [locationNumber][0 = x - 1 = y]
    private List<PowerPlantPlacements> potentialPowerPlantLocations;

    private int[,] tileState;
    private float[,] builtOnDegree;

    [SerializeField] private int mapXSize;
    [SerializeField] private int mapYSize;

    [SerializeField] private Vector2 centerPoint;

    [SerializeField] private int riverPosition;

    [SerializeField] private BuildingDataCollector buildingDataHub;

    void Start()
    {

        float maxBuiltOnDegree = 0;

        bool powerPlantBuilt = false;

        float maxDistance = mapXSize;

        buildingDataHub.buildingManagersGrid = new BuildingManager[mapXSize, mapYSize];


        tileState = new int[mapXSize, mapYSize];

        builtOnDegree = new float[mapXSize, mapYSize];

        potentialPowerPlantLocations = new List<PowerPlantPlacements>();

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


        List<BuildingManager> bmList = new List<BuildingManager>();


        for (int x = 0; x < mapXSize; x++)
        {
            for (int y = 0; y < mapYSize; y++)
            {

                float wildness = builtOnDegree[x, y];

                wildness *= 100 / maxBuiltOnDegree;

                float randomizedWildness = wildness + Random.Range(-10, 10);

                if (Vector3.Distance(centerPoint, new Vector3(x, y, 0)) > (mapXSize / 2))
                {

                }
                else if (x % 3 == 0 && y % 4 == 0 && x != riverPosition && wildness < 50)
                {
                    //road
                    GameObject newTile = Instantiate(roadPrefabs[0], transform);

                    newTile.transform.rotation = Quaternion.Euler(-90, 90, 0);
                    newTile.transform.position = new Vector3(x, 0, y);

                    tileState[x, y] = 4;
                }
                else if (y % 4 == 0 && wildness < 50)
                {

                    tileState[x, y] = 4;

                    if (x == riverPosition)
                    {
                        //bridge
                        GameObject newTile = Instantiate(tilePrefabs[5], transform);

                        newTile.transform.position = new Vector3(x, 0, y);
                    }
                    else
                    {
                        //road
                        GameObject newTile = Instantiate(roadPrefabs[Random.Range(1,3)], transform);

                        newTile.transform.position = new Vector3(x, 0, y);
                    }
                }
                else if (x == riverPosition)
                {
                    //river
                    GameObject newTile = Instantiate(tilePrefabs[3], transform);

                    newTile.transform.position = new Vector3(x, 0, y);

                    tileState[x, y] = 3;
                }
                else if (x % 3 == 0 && wildness < 50)
                {
                    //road
                    GameObject newTile = Instantiate(roadPrefabs[Random.Range(1, 3)], transform);

                    newTile.transform.rotation = Quaternion.Euler(-90, 90, 0);
                    newTile.transform.position = new Vector3(x, 0, y);

                    tileState[x, y] = 4;
                }
                else if (randomizedWildness <= 40)
                {
                    if (x < riverPosition)
                    {
                        //buildings
                        GameObject newTile = Instantiate(tilePrefabs[1], transform);

                        newTile.transform.position = new Vector3(x, 0, y);

                        SpawnBuilding(buildingPrefabs, buildingRoofPrefabs, newTile, randomizedWildness, x, y, 6, bmList);

                        tileState[x, y] = 1;
                    }
                    else
                    {

                        GameObject newTile = Instantiate(tilePrefabs[1], transform);

                        newTile.transform.position = new Vector3(x, 0, y);

                        if (powerPlantBuilt == false && tileState[x - 1, y] == 1 && tileState[x - 1, y - 1] == 1 && tileState[x - 1, y - 1] == 1 && x - 1 != riverPosition)
                        {


                            potentialPowerPlantLocations.Add(new PowerPlantPlacements(x, y, newTile));

                            //if (buildingDataHub.buildingManagersGrid[x - 1, y] != null)
                            //{
                            //    buildingDataHub.buildingManagersGrid[x - 1, y].transform.gameObject.SetActive(false);

                            //    Debug.Log(buildingDataHub.buildingManagersGrid[x - 1, y].transform.gameObject);
                            //}
                            //if (buildingDataHub.buildingManagersGrid[x - 1, y - 1] != null)
                            //{
                            //    buildingDataHub.buildingManagersGrid[x - 1, y - 1].transform.gameObject.SetActive(false);
                            //}
                            //if (buildingDataHub.buildingManagersGrid[x, y - 1] != null)
                            //{
                            //    buildingDataHub.buildingManagersGrid[x, y - 1].transform.gameObject.SetActive(false);
                            //}

                            //powerPlantBuilt = true;

                            //GameObject newTile = Instantiate(tilePrefabs[1], transform);
                            //newTile.transform.position = new Vector3(x, 0, y);

                            //SpawnSingleBuilding(powerPlantPrefab, newTile, x, y, bmList);

                            //tileState[x, y] = 1;

                            //return;
                            //Debug.Log("DO YOU WORK????");
                        }
                        //else
                        {

                            //industry
                            //GameObject newTile = Instantiate(tilePrefabs[1], transform);

                            //newTile.transform.position = new Vector3(x, 0, y);

                            SpawnBuilding(industryPrefabs, industryRoofPrefabs, newTile, randomizedWildness, x, y, 12, bmList);

                            tileState[x, y] = 1;

                        }
                    }
                }
                else if (randomizedWildness <= 60)
                {
                    //grass
                    GameObject newTile = Instantiate(tilePrefabs[0], transform);

                    newTile.transform.position = new Vector3(x, 0, y);

                    tileState[x, y] = 0;
                }
                else
                {
                    //forest
                    GameObject newTile = Instantiate(tilePrefabs[2], transform);

                    newTile.transform.position = new Vector3(x, 0, y);
                    newTile.transform.rotation = Quaternion.Euler(0, 90 * (int)Random.Range(0, 4), 0);

                    tileState[x, y] = 2;
                }
            }
        }


        SpawnPowerPlants(bmList);

        buildingDataHub.buildingManagers = bmList.ToArray();

    }

    private void SpawnPowerPlants(List<BuildingManager> bmList)
    {
        for(int i = 0; i < numPowerPlants; i++)
        {
            //Debug.Log("spawning!");

            PowerPlantPlacements spawnLocation = potentialPowerPlantLocations[Random.Range(0, potentialPowerPlantLocations.Count)];
            int x = spawnLocation.x;
            int y = spawnLocation.y;

            if (buildingDataHub.buildingManagersGrid[x - 1, y] != null)
            {
                buildingDataHub.buildingManagersGrid[x - 1, y].transform.gameObject.SetActive(false);
            }

            if (buildingDataHub.buildingManagersGrid[x - 1, y - 1] != null)
            {
                buildingDataHub.buildingManagersGrid[x - 1, y - 1].transform.gameObject.SetActive(false);
            }
            if (buildingDataHub.buildingManagersGrid[x, y - 1] != null)
            {
                buildingDataHub.buildingManagersGrid[x, y - 1].transform.gameObject.SetActive(false);
            }
            if (buildingDataHub.buildingManagersGrid[x, y] != null)
            {
                buildingDataHub.buildingManagersGrid[x, y].transform.gameObject.SetActive(false);
            }

            for (int j = 0; j < potentialPowerPlantLocations.Count; j++)
            {
                if (potentialPowerPlantLocations[i].x == x && potentialPowerPlantLocations[i].y == y + 1)
                {
                    Debug.Log("dumbass!X");
                    potentialPowerPlantLocations.RemoveAt(i);
                }
            }
            for (int j = 0; j < potentialPowerPlantLocations.Count; j++)
            {
                if (potentialPowerPlantLocations[i].x == x && potentialPowerPlantLocations[i].y == y - 1)
                {
                    Debug.Log("dumbass!Y");
                    potentialPowerPlantLocations.RemoveAt(i);
                }
            }
            for (int j = 0; j < potentialPowerPlantLocations.Count; j++)
            {
                if (potentialPowerPlantLocations[i].x == x + 1 && potentialPowerPlantLocations[i].y == y)
                {
                    Debug.Log("dumbass!X");
                    potentialPowerPlantLocations.RemoveAt(i);
                }
            }
            for (int j = 0; j < potentialPowerPlantLocations.Count; j++)
            {
                if (potentialPowerPlantLocations[i].x == x - 1 && potentialPowerPlantLocations[i].y == y)
                {
                    Debug.Log("dumbass!Y");
                    potentialPowerPlantLocations.RemoveAt(i);
                }
            }

            SpawnSingleBuilding(powerPlantPrefab, spawnLocation.linkedGameObject, x, y, bmList);

            tileState[x, y] = 1;

            potentialPowerPlantLocations.Remove(spawnLocation);
        }
    }

    private GameObject InitializeBuildingManager(int buildingType, Transform parent)
    {
        GameObject buildingManager = Instantiate(buildingPrefabs[buildingType].floor[0], parent);

        buildingManager.transform.parent = buildingDataHub.transform;
        buildingManager.transform.rotation = Quaternion.Euler(0, 90 * (int)Random.Range(0, 4), 0);

        return buildingManager;
    }

    private GameObject InitializeSingleBuildingManager(Transform parent)
    {
        GameObject buildingManager = Instantiate(buildingManagerPrefab, parent);

        buildingManager.transform.parent = buildingDataHub.transform;
        //buildingManager.transform.rotation = Quaternion.Euler(0, 90 * (int)Random.Range(0, 4), 0);

        return buildingManager;
    }

    private void SpawnBuilding(BuildingArray[] buildingTypes, BuildingArray[] buildingRoofTypes, GameObject parentTile, float wildness, int x, int y, int floorFac, List<BuildingManager> bmList)
    {

        int buildingVariant = Random.Range(0, buildingTypes.Length);


        GameObject buildingManager = InitializeBuildingManager(buildingVariant, parentTile.transform);
        BuildingManager connectedManager = buildingManager.GetComponent<BuildingManager>();


        float var = Random.Range(0.7f, 1f);
        Color buildingColor = new Color(var, var, var);


        buildingDataHub.buildingManagersGrid[x, y] = connectedManager;
        bmList.Add(connectedManager);
        connectedManager.floors = new floorScript[(int)(((40 - wildness) / floorFac)/2) + 2];


        for (int i = 0; i < connectedManager.floors.Length - 1; i++)
        {

            GameObject newFloor = Instantiate(buildingTypes[buildingVariant].floor[Random.Range(1, buildingTypes[buildingVariant].floor.Length)], connectedManager.transform);


            newFloor.transform.position = new Vector3(parentTile.transform.position.x, 1f + 1f * i, parentTile.transform.position.z);
            newFloor.transform.localScale = new Vector3(0.7f, 1, 0.7f);


            connectedManager.floors[i] = newFloor.gameObject.GetComponent<floorScript>();
        }

        //roof
        GameObject newRoof = Instantiate(buildingRoofTypes[buildingVariant].floor[Random.Range(0, buildingRoofTypes[buildingVariant].floor.Length)], connectedManager.transform);


        newRoof.transform.position = new Vector3(parentTile.transform.position.x, 0.5f + 1f * (connectedManager.floors.Length - 1), parentTile.transform.position.z);
        newRoof.transform.localScale = new Vector3(0.7f, 1, 0.7f);


        connectedManager.floors[connectedManager.floors.Length - 1] = newRoof.gameObject.GetComponent<floorScript>();
    }

    private void SpawnSingleBuilding(GameObject building, GameObject parentTile, int x, int y, List<BuildingManager> bmList)
    {

        GameObject buildingManager = InitializeSingleBuildingManager(parentTile.transform);
        BuildingManager connectedManager = buildingManager.GetComponent<BuildingManager>();


        buildingDataHub.buildingManagersGrid[x, y] = connectedManager;
        bmList.Add(connectedManager);
        connectedManager.floors = new floorScript[1];



        GameObject newBuilding = Instantiate(building, connectedManager.transform);


        newBuilding.transform.position = new Vector3(parentTile.transform.position.x, 0.5f, parentTile.transform.position.z);
        newBuilding.transform.localScale = new Vector3(0.7f, 1, 0.7f);


        connectedManager.floors[0] = newBuilding.gameObject.GetComponent<floorScript>();

    }

    //private void SpawnIndustryBuilding(GameObject parentTile, float wildness, int x, int y, List<BuildingManager> bmList)
    //{

    //    int buildingType = Random.Range(0, buildingPrefabs.Length);

    //    parentTile.transform.rotation = Quaternion.Euler(0, 90 * (int)Random.Range(0, 4), 0);

    //    GameObject buildingManager = Instantiate(industryPrefabs[buildingType].floor[Random.Range(1, buildingPrefabs[buildingType].floor.Length)], parentTile.transform);
    //    BuildingManager connectedManager = buildingManager.GetComponent<BuildingManager>();
    //    buildingDataHub.buildingManagersGrid[x, y] = connectedManager;

    //    float var = Random.Range(0f, 0.3f);

    //    Color buildingColor = new Color(
    //        Random.Range(0.3f, 0.5f), var, var
    //    );

    //    for (int i = 0; i < (40 - wildness) / 10; i++)
    //    {
    //        GameObject newFloor = Instantiate(industryPrefabs[Random.Range(0, industryPrefabs.Length)], parentTile.transform);

    //        newFloor.transform.position = new Vector3(parentTile.transform.position.x, 0.5f + 0.5f * i, parentTile.transform.position.z);
    //        newFloor.transform.localScale = new Vector3(0.7f, 0.7f, 1);
    //        newFloor.gameObject.GetComponent<MeshRenderer>().materials[0].color = buildingColor;
    //        newFloor.gameObject.AddComponent<floorScript>();
    //    }

    //    GameObject roof = Instantiate(industryRoofPrefabs[Random.Range(0, industryRoofPrefabs.Length)], parentTile.transform);

    //    roof.transform.position = new Vector3(parentTile.transform.position.x, 0.5f + 0.5f * (((int)(40 - wildness) / 10) + 1), parentTile.transform.position.z);
    //    roof.transform.localScale = new Vector3(0.7f, 0.7f, 1);
    //    roof.gameObject.GetComponent<MeshRenderer>().materials[1].color = buildingColor;
    //    roof.gameObject.AddComponent<floorScript>();

    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
