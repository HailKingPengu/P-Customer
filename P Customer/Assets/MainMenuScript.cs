using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement           ;  

public class MainMenuScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1.0f;
    }
}
