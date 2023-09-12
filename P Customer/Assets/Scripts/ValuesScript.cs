using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValuesScript : MonoBehaviour
{

    [Header("Engineer variables")]

    [SerializeField] public int[] cost;

    public GameObject[] levelModels;

    public int currentLevel;


    [Header("Designer variables")]
    [Header("position in array applies to prefab level")]

    public Values[] valuesArray;



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

        //Values values = new Values;

        return valuesArray[currentLevel];
    }
}
