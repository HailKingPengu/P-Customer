using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{

    //public struct tutorialText(){




    [SerializeField] private GameObject textPanel;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Vector3 targetPosition;

    [SerializeField] private Vector3 hiddenPos;
    [SerializeField] private Vector3 shownPos;
    [SerializeField] private float smoothing;

    [SerializeField] private GameObject targetBuilding;

    [SerializeField] private string[] tutorialText;

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
                        //ShowNext();
                    }
                    break;
            }
        }
    }

    private void ShowUntil(string text, int action)
    {
        switch(action)
        {
            case 0:
                targetPosition = shownPos; 
                break;
        }
    }

    private void ShowNext(string text, int action)
    {

    }

    private void ShowPanel()
    {
        targetPosition = shownPos;
    }

    private void HidePanel()
    {
        targetPosition = hiddenPos;
    }
}
