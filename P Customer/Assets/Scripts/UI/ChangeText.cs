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
        getPower
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
        Invoke(chosenfunction.ToString(),1f);
        textmesh.text = headertext+text+footertext;
    }


    void getMoney() {  
        text = gameManager.money.ToString();
    }
    void getHappiness() {  
        text = gameManager.happiness.ToString();
    }
    void getPower() {  
        text = gameManager.power.ToString();
    }
}
