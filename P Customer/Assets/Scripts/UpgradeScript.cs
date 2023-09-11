using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeScript : MonoBehaviour
{

    [SerializeField] private GameObject upgradeMenu;
    private RaycastHit lastHit;

    private floorScript lastFloorScript;

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

            bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
            Console.WriteLine(isOverUI);


            //if (!EventSystem.current.IsPointerOverGameObject())
            //{
            //    hit = new RaycastHit();
            //}

            //if (hit.transform != null)
            //{
            //    Debug.Log(hit.transform);
            //}

            if (hit.transform.GetComponentInParent<floorScript>() != null && !isOverUI)
            {
                lastHit = hit;
                lastFloorScript = hit.transform.GetComponentInParent<floorScript>();

                lastFloorScript.Hovered();
                //if(Input.GetMouseButton(0))
                //{
                //    //hit.transform.GetComponentInParent<floorScript>().Upgrade(1);
                //}
            }
            else
            {
                lastHit = new RaycastHit();
                lastFloorScript = null;
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();

            Console.

            if (lastHit.transform.GetComponentInParent<floorScript>() != null && !isOverUI)
            {
                upgradeMenu.transform.position = Camera.main.WorldToScreenPoint(lastHit.transform.position);
                upgradeMenu.SetActive(true);
            }
            else if (isOverUI)
            {
                return;
            }
            else
            {
                upgradeMenu.SetActive(false);
            }
        }
    }

    public void upgradeCurrent(int level)
    {
        if(lastFloorScript != null)
        {
            lastFloorScript.Upgrade(level);
        }
    }
}
