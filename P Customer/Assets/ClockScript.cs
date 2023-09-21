using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClockScript : MonoBehaviour
{

    [SerializeField] private TMP_Text clockText;

    private string[] months = new string[]{ "jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec" };
    [SerializeField] float targetTime = 300;
    float StartTime;
    [SerializeField] float numYears;

    public bool targetReached;

    //1980 - 2030

    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        float currentTime = Time.time - StartTime;
        float currentYear = currentTime / (targetTime / numYears);
        float currentMonth = (currentYear % 1) * 12;

        clockText.text = "year: " + (1980 + (int)currentYear).ToString();

        if(currentTime >= targetTime) targetReached = true;
    }
}
