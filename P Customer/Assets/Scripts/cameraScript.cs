using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {

            //if (hit.transform != null)
            //{
            //    Debug.Log(hit.transform);
            //}

            if (hit.transform.GetComponentInParent<floorScript>() != null)
            {
                hit.transform.GetComponentInParent<floorScript>().Hovered();
                if(Input.GetMouseButton(0))
                {
                    hit.transform.GetComponentInParent<floorScript>().Upgrade();
                }
            }
        }
    }
}
