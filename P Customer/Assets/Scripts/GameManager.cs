using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager Instance;
    public bool debugMode;

    [SerializeField] private BuildingDataCollector buildingDataHub;

    [Header ("Rebellion timer")]
    RebellionTimer Timer;

    [Header("Variable part 1")]
    public float happiness;

    public float power;
    public float powerNeeded; // power thats needed to be sustainable
    public float happinessNeeded; // power thats needed to be sustainable
    public float pollution; // 

    [Header("Money increasing")]
    public int money; // money goes up overtime
    float moneyTime;
    public float moneyTimer;
    public int moneyAmountPerTime;


    [Header("Variable part 2")]
    public float sustainability; // sustainability is when power is over power needed
    public float rebellion; // rebellion goes up over time

    public enum Gamestate
    {
        Playing,
        gameOver,
        Title,
        endScreen1,
        endScreen2,
        endScreen3,
        endScreen4
    }

    public Gamestate currentState;
    
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
        Timer = GetComponent<RebellionTimer>();
    }

    void Update()
    {
        Values currentValues = buildingDataHub.values;

        power = -currentValues.powerUse;
        happiness = currentValues.happiness;
        pollution = currentValues.pollution;

        // RULE #3 REBELLION GOES UP OVER TIME
        rebellion = (Mathf.Round((Timer.currentRebellion)/3));


        switch (currentState)
        {
        case Gamestate.Playing:
                Playing();
        break;

            //case Gamestate.Playing:
            //    Debug.Log("I'm Gamign BATMAN");
            //    break;
            //case Gamestate.gameOver:
            //    Debug.Log("It's so Joever");
            //    break;
            //case Gamestate.Title:
            //    Debug.Log("We're so Barack");
            //    break;
        }

    }

    void gameOver()
    {
        Debug.Log("It's so Joever");
    }
    void Title()
    {
        Debug.Log("We're so Barack");
    }

    // the script that activates when we are playing
    void Playing()
    {
        // RULE #1 GET MONEY OVER TIME
        moneyTime += Time.deltaTime;
        if (moneyTime >= moneyTimer)
        {
            moneyTime -= moneyTimer;
            money += moneyAmountPerTime;
        }

        // RULE #2 GAME OVER WHEN REBELLION TOO MUCH
        if (Timer.gameOver)
        {
            currentState = Gamestate.gameOver;
        }
    }


}
