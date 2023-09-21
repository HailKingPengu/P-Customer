using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class fxtxtBehavior : MonoBehaviour
{
    [SerializeField] private float size;
    [SerializeField] private float sizeDecreasePercentage;

    [SerializeField] private Vector3 velocity;

    [SerializeField] private float timeToLive;
    [SerializeField] private float startTime;

    [SerializeField] private TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(size,size,size);

        startTime = Time.time;
    }

    //public void initialize(Vector3 pos)
    //{
    //    transform.localScale = new Vector3(size, size, size);

    //    startTime = Time.time;
    //}

    // Update is called once per frame
    void Update()
    {
        size = Mathf.Lerp(size, 0, sizeDecreasePercentage * (Time.deltaTime * 60));
        transform.localScale = new Vector3(size, size, size);
        if (size <= 0.01f) { Destroy(gameObject); };

        transform.position += velocity;

        text.color = new Color(0.8f,0.1f,0.1f,1f-((Time.time - startTime)/timeToLive));

        if(timeToLive > Time.time + startTime)
        {
            Destroy(gameObject);
        }
    }
}
