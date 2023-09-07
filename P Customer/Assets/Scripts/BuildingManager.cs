using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{

    public floorScript[] floors;



    [Header("Engineer variables")]
    public GameObject[] levelModels;

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
        
    }

    public void fetchData()
    {
        //powerUse, happiness, pollution

        powerUse = 0;
        pollution = 0;

        float totalHappiness = 0;

        foreach(floorScript floor in floors)
        {
            float[] floorData = floor.FetchData();

            powerUse += floorData[0];
            totalHappiness += floorData[1];
            pollution += floorData[2];
        }

        happiness = totalHappiness / floors.Length;
    }
}
