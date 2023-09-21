using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTipsScript : MonoBehaviour
{

    [System.Serializable]
    struct tutorialNext
    {
    
        [SerializeField] public string text;
        [SerializeField] public int trigger;
        [SerializeField] public int action;

        public tutorialNext(string stext, int strigger, int saction)
        {
            text = stext;
            trigger = strigger;
            action = saction;
        }

    }

    [SerializeField] private GameObject textPanel;
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text nextText;
    [SerializeField] private Vector3 targetPosition;

    [SerializeField] private Vector3 hiddenPos;
    [SerializeField] private Vector3 shownPos;
    [SerializeField] private float smoothing;

    [SerializeField] private GameObject targetBuilding;

    [Header("amongus")]
    [SerializeField] private tutorialNext[] tutorialText;

    [SerializeField] private int currentText;

    [SerializeField] private bool waitingOnTrigger;
    [SerializeField] private int triggerType;

    [SerializeField] private float sustainableOslo;
    [SerializeField] private float sustainableRotterdam;
    [SerializeField] private float sustainableHongKong;
    [SerializeField] private float sustainableDetroit;

    public int sustainableGotmessage = 0;
    public float newSustainability;

    private GameManager gameManager;


    //0 = mouse input, 1 = 

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        textPanel.SetActive(true);

        targetPosition = textPanel.transform.position;
        shownPos = new Vector3(Screen.width / 2, 20, 0);
        hiddenPos = new Vector3(Screen.width/ 2, -150, 0);

        //Invoke("ShowPanel", 1f);
        //Invoke("HidePanel", 2f);

        waitingOnTrigger = true;
        HidePanel();
    }

    // Update is called once per frame
    void Update()
    {
        textPanel.transform.position = Vector3.Lerp(textPanel.transform.position, targetPosition, smoothing);

        CheckForUpdate();

        if(waitingOnTrigger)
        {
            switch(triggerType)
            {
                case 0:

                    nextText.text = "press space >";

                    if (Input.GetKey(KeyCode.Space))
                    {
                        //ShowNext(tutorialText[currentText]);
                        //currentText++;
                        HidePanel();
                    }
                break;

            }
        }
        else
        {
            nextText.text = " ";
        }
    }

    private void ShowUntil(tutorialNext next)
    {
        
    }

    private void ShowNext(tutorialNext next)
    {
        targetPosition = hiddenPos;
        triggerType = next.trigger;
        waitingOnTrigger = false;

        Invoke("ShowPanel", 0.5f);

        if (next.action != 0)
        {
            Action(next.action);
            //Debug.Log("JEEEEEEZ????");
        }
    }

    private void ShowPanel()
    {
        //text.text = tutorialText[currentText].text;
        targetPosition = shownPos;
        waitingOnTrigger = true;
    }

    private void HidePanel()
    {
        targetPosition = hiddenPos;
    }

    private void Action(int action)
    {
        switch(action)
        {
            case 1:
                // zoom in on protestors
                //cameraMove.targetPosition = protestLocation.position;
                //Debug.Log("JEEEEEEZ");
                break;
            case 2:
                //cameraMove.ResetPosition();
                //upgradeScript.scriptActivated = true;
                break;
            case 3:
                //rebelSpawner.inTutorial = false;
                break;
        }
    }

    void CheckForUpdate () {
        newSustainability = gameManager.sustainability*100f;

        switch(sustainableGotmessage){
            case 0:
                if(newSustainability>sustainableDetroit){
                    NewSustain("Your city just passed Detroit in sustainability!");
                    sustainableGotmessage++;
                    Debug.Log("HI");
                }
            break;
            case 1:
                if(newSustainability>sustainableHongKong){
                    NewSustain("Good news! Your city is now more sustainable than Hong Kong!");
                    sustainableGotmessage++;
                }
            break;
            case 2:
                if(newSustainability>sustainableRotterdam){
                    NewSustain("What an achievement! Your city is even more sustainable than Rotterdam!");
                    sustainableGotmessage++;
                }
            break;
            case 3:
                if(newSustainability>sustainableOslo){
                    NewSustain("Your city is now so sustainable, that it passed Oslo! That is amazing!");
                    sustainableGotmessage++;
                }
            break;
            case 4:

            break;
        }



    }

    void NewSustain(string newText) {
        text.text = newText;
        ShowPanel();
    }
}
