using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDataCollector : MonoBehaviour
{

    //public for now
    public BuildingManager[,] buildingManagersGrid;

    public BuildingManager[] buildingManagers;

    [Header("yo, thats stuff")] 
    [SerializeField] private float powerUse;

    [SerializeField] private float happiness;

    [SerializeField] private float pollution;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        powerUse = 0;
        pollution = 0;

        float totalHappiness = 0;

        foreach (BuildingManager bm in buildingManagers)
        {
            float[] buildingData = bm.FetchData();

            powerUse += buildingData[0];
            totalHappiness += buildingData[1];
            pollution += buildingData[2];
        }

        happiness = totalHappiness / buildingManagers.Length;

    }
}
