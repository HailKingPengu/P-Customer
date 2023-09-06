using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class floorScript : MonoBehaviour
{

    public GameObject[] levelModels;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.7f, 0.7f, 1f), 0.1f);
    }

    public void Hovered()
    {
        transform.localScale = new Vector3(1f, 1, 1f);
    }
}
