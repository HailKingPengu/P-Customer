using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BuildingDataCollector : MonoBehaviour
{

    //public for now
    public BuildingManager[,] buildingManagersGrid;

    public BuildingManager[] buildingManagers;

    [Header("yo, thats stuff")]

    [SerializeField] public Values values;

    //[SerializeField] private float powerUse;

    //[SerializeField] private float happiness;

    //[SerializeField] private float pollution;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        values.powerUse = 0;
        values.pollution = 0;

        float totalHappiness = 0;

        foreach (BuildingManager bm in buildingManagers)
        {
            Values buildingData = bm.FetchData();

            values.powerUse += buildingData.powerUse;
            totalHappiness += buildingData.happiness;
            values.pollution += buildingData.pollution;
        }

        values.happiness = totalHappiness / buildingManagers.Length;

    }
}
