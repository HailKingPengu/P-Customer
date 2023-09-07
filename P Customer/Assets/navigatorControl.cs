using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navigatorControl : MonoBehaviour
{
    public GameObject[] navpoints;
    bool hasupdated = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!hasupdated)
        {
            UpdateNavpoints();
            hasupdated = true;
        }
    }

     void UpdateNavpoints()
    {
        navpoints = GameObject.FindGameObjectsWithTag("navpoint");
    }
}
