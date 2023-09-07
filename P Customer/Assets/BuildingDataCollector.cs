using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDataCollector : MonoBehaviour
{

    //public for now
    public BuildingManager[,] buildingManagers;

    public BuildingManager[] buildingManagersArray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(buildingManagers.GetLength(0));
        //Debug.Log(buildingManagers.GetLength(1));

        foreach (BuildingManager bm in buildingManagersArray)
        {
            bm.fetchData();
        }
    }
}
