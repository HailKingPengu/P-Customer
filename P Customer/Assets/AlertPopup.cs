using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlertPopup : MonoBehaviour
{

    [SerializeField] private TMP_Text text;
    [SerializeField] private float disappearIn;
    [SerializeField] private Vector3 inside;
    [SerializeField] private Vector3 outside;

    [SerializeField] private RectTransform tf;

    // Start is called before the first frame update
    void Start()
    {
        inside = tf.anchoredPosition;
        outside = new Vector3(100000, 100000, 100000);
    }

    // Update is called once per frame
    void Update()
    {

        disappearIn -= Time.deltaTime;

        if (disappearIn < 0)
        {
            tf.anchoredPosition = outside;
            //Debug.Log("HUH");
        }
        else
        {
            tf.anchoredPosition = inside;
            //Debug.Log("WHAT");
        }
    }

    public void Popup(string message, float duration)
    {
        text.text = message;
        disappearIn = duration;
    }
}
