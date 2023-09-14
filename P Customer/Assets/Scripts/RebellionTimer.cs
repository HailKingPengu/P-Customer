using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebellionTimer : MonoBehaviour
{
    public float currentRebellion;
    public int maxRebellion;
    public bool gameOver;
    public float rebellionModifier;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        maxRebellion = 400;
        currentRebellion = 0;
        rebellionModifier = 5f;
    }

    private void FixedUpdate()
    {
        currentRebellion += (Time.deltaTime * rebellionModifier);
        //Debug.Log(Mathf.Round(currentRebellion/3));
        if(currentRebellion >= maxRebellion)
        {
            //gameOver = true;
        }
        if (gameOver)
        {
            //Debug.Log("Time's up, dumbass");
        }
    }
}
