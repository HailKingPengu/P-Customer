using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeScript : MonoBehaviour
{

    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject upgradeMenu;
    private RaycastHit lastHit;

    private floorScript lastFloorScript;

    [SerializeField] private GameObject upgradeLevel1;
    [SerializeField] private TMP_Text level1Text;
    [SerializeField] private GameObject upgradeLevel2;
    [SerializeField] private TMP_Text level2Text;
    //[SerializeField] private GameObject upgradeLevel3;
    //[SerializeField] private TextMeshPro level3Text;

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
            //if (Input.GetMouseButtonDown(0))
            //{
            if (hit.transform.GetComponentInParent<floorScript>() != null && !isOverUI)
            {
                lastHit = hit;


                hit.transform.GetComponentInParent<floorScript>().Hovered();
                //if(Input.GetMouseButton(0))
                //{
                //    //hit.transform.GetComponentInParent<floorScript>().Upgrade(1);
                //}
            }
            else if (isOverUI)
            {
                //return;
            }
            
            if(hit.transform.GetComponentInParent<floorScript>() == null)
            {
                lastHit = new RaycastHit();
                //lastFloorScript = null;
            }
            //}
        }

        //Debug.Log(lastHit.transform);

    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {

            bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
            //lastFloorScript = lastHit.transform.GetComponentInParent<floorScript>();

            //Debug.Log(lastHit.transform.GetComponentInParent<floorScript>());
            //Debug.Log(lastHit.transform.parent);

            if (!isOverUI)
            {

                if (lastHit.transform != null)
                {
                    lastFloorScript = lastHit.transform.GetComponentInParent<floorScript>();


                    upgradeMenu.transform.position = Camera.main.WorldToScreenPoint(lastHit.transform.position);
                    upgradeMenu.SetActive(true);


                    Values lVal1 = lastFloorScript.valuesArray[1];

                    level1Text.text = 
                    "power use:" + lVal1.powerUse + "\nhappiness:" + lVal1.happiness + "\npollution:" + lVal1.pollution + "\n\ncost:" + lastFloorScript.cost[lastFloorScript.currentLevel];

                }
                else
                {
                    upgradeMenu.SetActive(false);
                }
            }
            //else if(lastFloorScript == null && !isOverUI)
            //{
            //    lastFloorScript = lastHit.transform.GetComponentInParent<floorScript>();
            //}
            //if (lastFloorScript != null && !isOverUI)
            //{
            //    upgradeMenu.transform.position = Camera.main.WorldToScreenPoint(lastHit.transform.position);
            //    upgradeMenu.SetActive(true);
            //}
            else if (isOverUI)
            {
                //return;
            }

            //if (!isOverUI && lastHit.transform.GetComponentInParent<floorScript>() == null)
            //{

            //}
        }
    }

    public void upgradeCurrent(int level)
    {
        if(lastFloorScript != null)
        {
            if (gameManager.money >= lastFloorScript.cost[level] && level != lastFloorScript.currentLevel)
            {
                lastFloorScript.Upgrade(level);
                lastFloorScript = lastFloorScript.transform.GetComponent<floorScript>();

                gameManager.money -= lastFloorScript.cost[level];
            }
            else
            {
                //Debug.Log(gameManager.money + "" + lastFloorScript.cost[level]);
                //Debug.Log("BROKE");
            }
        }
        else
        {
            Debug.Log(lastFloorScript);
            Debug.Log("IT WENT WROOOONG");
        }
    }
}
