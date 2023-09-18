using System.Collections;
using System.Collections.Generic;
using TMPro;
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


    public float happinessNeeded; // power thats needed to be sustainable
    public float pollution; // 

    [Header("Money increasing")]
    public int money; // money goes up overtime
    float moneyTime;
    public float moneyTimer;
    public int moneyAmountPerTime;
    float moneyIncrementor;
    public float happinessMoneyMultiplier;

    [Header("Power Needs and Gains")]
    public float power;
    public float powerProduction;
    public float powerUse;
    public float powerNeeded; // power thats needed to be sustainable
    float powerDeficiency;
    public float powerDeficiencyHappinessInfluence;
    public float happinessRageThreshold;
    float rageThreshold;

    [Header("Variable part 2")]
    public float sustainability; // sustainability is when power is over power needed
    public float rebellion; // rebellion goes up over time

    [Header("other affectors")]
    public float anger;
    [SerializeField] private float angerReduction = 0.0001f;
    public float pollutionAnger;
    [SerializeField] private float pollutionResistance = 0.0003f;

    [Header("affected UI")]
    [SerializeField] private AlertPopup alertText;
    [SerializeField] private GameObject gameOverScreen;


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

        power = currentValues.powerUse;
        powerProduction = buildingDataHub.powerProduction;
        powerUse = buildingDataHub.powerUse;

        happiness = currentValues.happiness;
        pollution = currentValues.pollution;

        // RULE #3 REBELLION GOES UP OVER TIME
        rebellion = (Mathf.Round((Timer.currentRebellion)/3));

        //pollution resistance
        //pollutionAnger = (pollution - 500) * (pollutionResistance * ((Time.time * Time.time) / 450));

        //happiness -= pollutionAnger;

        //outrage for insufficient power
        //if (power < 0)
        //{
        //    anger += Time.deltaTime;
        //}

        //anger = Mathf.Lerp(anger, 0, angerReduction);

        //happiness -= anger;


        if (power < 0)
        {
            alertText.Popup("Your city has run out of power!", 0.1f);
        }

        if(happiness < 0)
        {
            gameOverScreen.SetActive(true);
        }

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

        MoneyUpdateBehavior();

        PowerUpdateBehavior();

        HappinessUpdateBehavior();


        // RULE #2 GAME OVER WHEN REBELLION TOO MUCH
        if (Timer.gameOver)
        {
            currentState = Gamestate.gameOver;
        }
    }

    
    void MoneyUpdateBehavior ()
    {
        moneyIncrementor = moneyAmountPerTime * ((happiness / happinessNeeded) * happinessMoneyMultiplier);
        
        // RULE #1 GET MONEY OVER TIME
        moneyTime += Time.deltaTime;
        if (moneyTime >= moneyTimer)
        {
            moneyTime -= moneyTimer;
            money += Mathf.RoundToInt(moneyIncrementor);
        }
    }

    void PowerUpdateBehavior ()
    {
        powerDeficiency = Mathf.Max(powerUse - powerProduction, 0f);
    }

    void HappinessUpdateBehavior ()
    {
        happiness = happiness - (powerDeficiency / powerDeficiencyHappinessInfluence);

        rageThreshold = Mathf.Max(happinessRageThreshold - happiness, 0);

        Timer.rebellionModifier = 1 + rageThreshold;
    }
}
