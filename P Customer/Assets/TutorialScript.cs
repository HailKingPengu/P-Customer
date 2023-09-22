using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialScript : MonoBehaviour
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

    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private Transform protestLocation;
    [SerializeField] private Transform industryLocation;

    [SerializeField] private UpgradeScript upgradeScript;

    [SerializeField] private RebelSpawner rebelSpawner;
    [SerializeField] private ClockScript clock;

    //0 = mouse input, 1 = 

    // Start is called before the first frame update
    void Start()
    {
        textPanel.SetActive(true);

        targetPosition = textPanel.transform.position;
        shownPos = new Vector3(Screen.width / 2, 20, 0);
        hiddenPos = new Vector3(Screen.width/ 2, -150, 0);

        //Invoke("ShowPanel", 1f);
        //Invoke("HidePanel", 2f);

        waitingOnTrigger = true;
        ShowPanel();
    }

    // Update is called once per frame
    void Update()
    {
        textPanel.transform.position = Vector3.Lerp(textPanel.transform.position, targetPosition, smoothing);

        if(waitingOnTrigger)
        {
            switch(triggerType)
            {
                case 0:

                    nextText.text = "press space >";

                    if (Input.GetKey(KeyCode.Space))
                    {
                        ShowNext(tutorialText[currentText]);
                        currentText++;
                    }
                break;
                case 1:

                    nextText.text = "select a building >";

                    if (upgradeScript.isSelected)
                    {
                        ShowNext(tutorialText[currentText]);
                        currentText++;
                    }
                    break;
                case 2:

                    nextText.text = "upgrade a floor >";

                    if (upgradeScript.hasUpgraded)
                    {
                        ShowNext(tutorialText[currentText]);
                        currentText++;
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
        text.text = tutorialText[currentText].text;
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
                cameraMove.targetPosition = protestLocation.position;
                //Debug.Log("JEEEEEEZ");
                break;
            case 2:
                cameraMove.ResetPosition();
                upgradeScript.scriptActivated = true;
                break;
            case 3:
                rebelSpawner.inTutorial = false;
                break;
            case 4:
                cameraMove.targetPosition = industryLocation.position;
                break;
            case 5:
                clock.isCounting = true;
                break;
            case 6:
                upgradeScript.scriptActivated = false;
                break;
        }
    }
}
