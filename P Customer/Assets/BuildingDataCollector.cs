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
    public float powerProduction;
    public float powerUse;

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

        powerProduction = 0;
        powerUse = 0;

        values.powerUse = 0;
        values.pollution = 0;

        float totalHappiness = 0;

        foreach (BuildingManager bm in buildingManagers)
        {
            Values buildingData = bm.FetchData();

            //values.powerUse += buildingData.powerUse;
            totalHappiness += buildingData.happiness;
            values.pollution += buildingData.pollution;

            if (buildingData.powerUse < 0) { powerProduction -= buildingData.powerUse; }
            if (buildingData.powerUse >= 0) { powerUse += buildingData.powerUse; }
        }

        values.powerUse = powerProduction - powerUse;

        //Debug.Log(powerProduction + " - " + powerUse + " - " + values.powerUse);

        values.happiness = totalHappiness / buildingManagers.Length;

    }
}
