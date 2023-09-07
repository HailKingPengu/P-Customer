using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager Instance;
    public bool debugMode;

    [SerializeField] private BuildingDataCollector buildingDataHub;

    [Header("Variable part 1")]
    public float happiness; 
    public int money; // money goes up overtime
    public float power;
    public float powerNeeded; // power thats needed to be sustainable
    public float happinessNeeded; // power thats needed to be sustainable
    public float pollution; // 

    [Header("Variable part 2")]
    public float sustainability; // sustainability is when power is over power needed
    public float rebellion; // rebellion goes up over time

    void Awake() {

        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }


    void Start()
    {
        
    }

    void Update()
    {
        Values currentValues = buildingDataHub.values;

        power = -currentValues.powerUse;
        happiness = currentValues.happiness;
        pollution = currentValues.pollution;

    }


}
