using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlertPopup : MonoBehaviour
{

    [SerializeField] private TMP_Text text;
    [SerializeField] private float disappearIn;
    [SerializeField] private float yInside;
    [SerializeField] private float yOutside;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        disappearIn -= Time.deltaTime;

        if (disappearIn < 0)
        {
            transform.localPosition = new Vector3(0, yOutside, 0);
            //Debug.Log("HUH");
        }
        else
        {
            transform.localPosition = new Vector3(0, yInside, 0);
            //Debug.Log("WHAT");
        }
    }

    public void Popup(string message, float duration)
    {
        text.text = message;
        disappearIn = duration;
    }
}
