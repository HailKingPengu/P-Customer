using System;
using UnityEngine;

public class UpgradeScript : MonoBehaviour
{

    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject upgradeMenu;
    private RaycastHit lastHit;

    private floorScript lastFloorScript;
    private floorScript targetedFloorScript;

    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera buildingCam;
    [SerializeField] private GameObject overlayEffect;

    private LayerMask usedMask;
    private LayerMask defaultMask;
    private LayerMask buildingMask;

    //[SerializeField] private GameObject upgradeLevel1;
    //[SerializeField] private TMP_Text level1Text;
    //[SerializeField] private GameObject upgradeLevel2;
    //[SerializeField] private TMP_Text level2Text;
    //[SerializeField] private GameObject upgradeLevel3;
    //[SerializeField] private TextMeshPro level3Text;

    // Start is called before the first frame update
    void Start()
    {
        defaultMask = LayerMask.GetMask("Default");
        buildingMask = LayerMask.GetMask("SelectedBuilding");

        usedMask = defaultMask;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, usedMask))
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

            if (hit.transform.GetComponentInParent<floorScript>() == null)
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

                    if (targetedFloorScript != lastFloorScript && targetedFloorScript != null)
                    {
                        ShowBuilding(false, targetedFloorScript);
                    }

                    targetedFloorScript = lastFloorScript;




                    upgradeMenu.transform.position = Camera.main.WorldToScreenPoint(lastHit.transform.position);
                    upgradeMenu.SetActive(true);
                    ShowBuilding(true, targetedFloorScript);
                    usedMask = buildingMask;


                    Values lVal1 = lastFloorScript.valuesArray[1];

                    for (int i = 0; i < lastFloorScript.valuesArray.Length; i++)
                    {

                    }

                    //level1Text.text = 
                    //"power use:" + lVal1.powerUse + "\nhappiness:" + lVal1.happiness + "\npollution:" + lVal1.pollution + "\n\ncost:" + lastFloorScript.cost[lastFloorScript.currentLevel];

                }
                else
                {
                    upgradeMenu.SetActive(false);
                    ShowBuilding(false, targetedFloorScript);
                    usedMask = defaultMask;
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
        if (lastFloorScript != null)
        {
            if (gameManager.money >= lastFloorScript.cost[level] && level != lastFloorScript.currentLevel)
            {
                lastFloorScript.Upgrade(level);
                lastFloorScript = lastFloorScript.transform.GetComponent<floorScript>();

                gameManager.money -= lastFloorScript.cost[level];

                ShowBuilding(true, targetedFloorScript);
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
            Debug.Log("no floor script");
        }
    }

    public void upgradeBuilding(int level)
    {
        if (lastFloorScript != null)
        {

            Transform building = lastFloorScript.transform.parent;
            floorScript[] floors = new floorScript[building.childCount];

            int upgradeCost = 0;

            for (int i = 0; i < building.childCount; i++)
            {
                floors[i] = building.GetChild(i).GetComponent<floorScript>();
                if (floors[i].currentLevel != level)
                {
                    upgradeCost += floors[i].cost[level];
                }
            }


            if (gameManager.money >= upgradeCost)
            {

                for(int i = 0; i < building.childCount; i++)
                {
                    if(floors[i].currentLevel != level)
                    {
                        floors[i].UpgradeAfter(level, i * 0.1f);
                    }
                }


                lastFloorScript = lastFloorScript.transform.GetComponent<floorScript>();

                gameManager.money -= upgradeCost;

                ShowBuilding(true, targetedFloorScript);
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
            Debug.Log("no floor script");
        }
    }

    private void ShowBuilding(bool show, floorScript floorScr)
    {

        Transform building = floorScr.transform.parent;

        if (show)
        {
            for (int i = 0; i < building.childCount; i++)
            {
                Transform floor = building.GetChild(i);

                for (int j = 0; j < floor.childCount; j++)
                {
                    floor.GetChild(j).gameObject.layer = 8;
                }
            }

            overlayEffect.SetActive(true);

        }
        else
        {
            for (int i = 0; i < building.childCount; i++)
            {
                Transform floor = building.GetChild(i);

                for (int j = 0; j < floor.childCount; j++)
                {
                    floor.GetChild(j).gameObject.layer = 0;
                }
            }

            overlayEffect.SetActive(false);
        }
    }
}
