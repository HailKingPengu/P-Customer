using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeText : MonoBehaviour
{
    private TMPro.TextMeshProUGUI textmesh;
    private GameManager gameManager;

    public enum Functions{
        getMoney,
        getHappiness,
        getPower,
        getPowerProduction,
        getPowerUse,
        getRebellion,
        getPollution,
        getPollutionUnhappiness,
        getPowerlessUnhappiness,
        getSustainability,
        getMoneyChange
    }

    public Functions chosenfunction;


    public string headertext;
    public string footertext;
    private string text;
    
    // Start is called before the first frame update
    void Start()
    {
        textmesh = GetComponent<TMPro.TextMeshProUGUI>(); 
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        Invoke(chosenfunction.ToString(),0f);
        textmesh.text = headertext+text+footertext;
    }


    void getMoney() {  
        text = gameManager.money.ToString();
    }
    void getHappiness() {  
        text = Mathf.Round(gameManager.happiness).ToString();
    }
    void getPower() {  
        text = Mathf.Round(gameManager.power).ToString();
    }
    void getPowerProduction()
    {
        text = gameManager.powerProduction.ToString();
    }
    void getPowerUse()
    {
        text = gameManager.powerUse.ToString();
    }
    void getRebellion()
    {
        text = gameManager.rebellion.ToString();
    }
    void getPollution()
    {
        text = gameManager.pollution.ToString();
    }
    void getPollutionUnhappiness()
    {
        text = gameManager.pollutionAnger.ToString();
    }
    void getPowerlessUnhappiness()
    {
        text = gameManager.anger.ToString();
    }
    void getSustainability()
    {
        text = gameManager.anger.ToString();
    }
    void getMoneyChange()
    {

        int moneyChange = gameManager.moneyChange;

        if (moneyChange < 0)
        {
            text = moneyChange.ToString();
        }
        if (moneyChange == 0)
        {
            text = "-";
        }
        if (moneyChange > 0)
        {
            text = "+" + moneyChange.ToString();
        }

    }
}
