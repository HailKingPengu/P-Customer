using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager Instance;
    public bool debugMode;

    [Header("Variable part 1")]
    public int happiness; 
    public int money; // money goes up overtime
    public int power;
    public int powerNeeded; // power thats needed to be sustainable
    public int happinessNeeded; // power thats needed to be sustainable
    public int pollution; // 

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

    }


}
