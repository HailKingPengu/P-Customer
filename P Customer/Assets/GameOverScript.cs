using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{

    [SerializeField] private GameObject worstEnding;
    [SerializeField] private GameObject badEnding;
    [SerializeField] private GameObject betterEnding;
    [SerializeField] private GameObject bestEnding;
    [SerializeField] private GameObject menuButton;

    // Start is called before the first frame update
    void Start()
    {
        worstEnding.SetActive(false);
        badEnding.SetActive(false);
        betterEnding.SetActive(false);
        bestEnding.SetActive(false);
        menuButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver(int ending)
    {

        Time.timeScale = 0;

        menuButton.SetActive(true);

        switch (ending)
        {
            case 0:
                worstEnding.SetActive(true); break;
            case 1:
                badEnding.SetActive(true); break;
            case 2:
                betterEnding.SetActive(true); break;
            case 3:
                bestEnding.SetActive(true); break;
        }
    }
}
