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
        [SerializeField] public int function;

        public tutorialNext(string stext, int sfunction)
        {
            text = stext;
            function = sfunction;
        }

    }

    [SerializeField] private GameObject textPanel;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Vector3 targetPosition;

    [SerializeField] private Vector3 hiddenPos;
    [SerializeField] private Vector3 shownPos;
    [SerializeField] private float smoothing;

    [SerializeField] private GameObject targetBuilding;

    [Header("amongus")]
    [SerializeField] private tutorialNext[] tutorialText;

    private int currentText;

    private bool waitingOnTrigger;
    private int triggerType;

    //0 = mouse input, 1 = 

    // Start is called before the first frame update
    void Start()
    {
        textPanel.SetActive(true);

        targetPosition = textPanel.transform.position;
        shownPos = new Vector3(Screen.width / 2, 20, 0);
        hiddenPos = new Vector3(Screen.width/ 2, -100, 0);

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
                    if(Input.GetMouseButtonDown(0))
                    {
                        ShowNext(tutorialText[currentText]);
                        currentText++;
                    }
                    break;
            }
        }
    }

    private void ShowUntil(tutorialNext next)
    {

    }

    private void ShowNext(tutorialNext next)
    {
        targetPosition = hiddenPos;
        triggerType = next.function;
        waitingOnTrigger = false;

        Invoke("ShowPanel", 0.5f);
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
}
