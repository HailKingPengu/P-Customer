using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{

    public ValuesScript[] floors;



    [Header("Engineer variables")]
    public GameObject[] levelModels;

    //[SerializeField] private float totalPowerUse;

    //[SerializeField] private float totalHappiness;

    //[SerializeField] private float totalPollution;

    [SerializeField] public Values values;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Values FetchData()
    {
        //powerUse, happiness, pollution

        values.powerUse = 0;
        values.pollution = 0;
        values.moneyGeneration = 0;

        float totalHappiness = 0;

        foreach(ValuesScript floor in floors)
        {
            Values floorData = floor.FetchData();

            values.powerUse += floorData.powerUse;
            totalHappiness += floorData.happiness;
            values.pollution += floorData.pollution;
            values.moneyGeneration += floorData.moneyGeneration;
        }

        values.happiness = totalHappiness / floors.Length;

        return values;
    }
}
