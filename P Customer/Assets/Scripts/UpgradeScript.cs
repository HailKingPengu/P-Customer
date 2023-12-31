using System;
using TMPro;
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
    [SerializeField] private GameObject target;

    private LayerMask usedMask;
    private LayerMask defaultMask;
    private LayerMask buildingMask;

    [SerializeField] private float menuOffset;
    [SerializeField] private float distanceFac;


    [SerializeField] private GameObject buildingStats;
    [SerializeField] private TMP_Text buildingStatsText;

    [SerializeField] private GameObject upgradeLevel1;
    [SerializeField] private TMP_Text level1Text;
    [SerializeField] private GameObject upgradeLevel2;
    [SerializeField] private TMP_Text level2Text;

    [SerializeField] private TMP_Text PrizeLevel1;
    [SerializeField] private TMP_Text PrizeLevel2;
    [SerializeField] private TMP_Text PrizeLevel1Building;
    [SerializeField] private TMP_Text PrizeLevel2Building;

    [SerializeField] private TMP_Text RoofLevel1;
    [SerializeField] private TMP_Text RoofLevel2;

    [SerializeField] private TMP_Text PowerLevel1;
    [SerializeField] private TMP_Text PowerLevel2;

    [SerializeField] private TMP_Text IndustryLevel1;
    [SerializeField] private TMP_Text IndustryLevel2;
    //[SerializeField] private GameObject upgradeLevel3;
    //[SerializeField] private TextMeshPro level3Text;

    [SerializeField] private AlertPopup alertPopup;

    [SerializeField] private CameraMove cameraMove;


    [SerializeField] private GameObject buildingMenu;
    [SerializeField] private GameObject powerMenu;
    [SerializeField] private GameObject industryMenu;

    [SerializeField] private GameObject effectsText;


    [SerializeField] private AudioSource soundUpgrade;
    [SerializeField] private AudioSource soundDenied;
    [SerializeField] private AudioSource soundSelect;
    private float soundLastSelectedSomething = 0;
    private float soundSelectReset = 0.8f;

    public bool isSelected;
    public bool hasUpgraded;
    public bool scriptActivated = false;

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

        if (scriptActivated)
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
            else
            {
                lastHit = new RaycastHit();
            }
        }

        //Debug.Log(lastHit.transform);

    }

    private void Update()
    {
        // resetting pitch
        if(soundLastSelectedSomething>0){
            soundLastSelectedSomething -= Time.deltaTime;
            if(soundLastSelectedSomething<=0){
                soundSelect.pitch = 1f;
                soundLastSelectedSomething = 0;
            }
        }

        if (scriptActivated) {

            if (Input.GetMouseButtonDown(0))
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

                        PlaySelectSound();

                        upgradeMenu.transform.position = Camera.main.WorldToScreenPoint(lastHit.transform.position);
                        upgradeMenu.SetActive(true);
                        buildingStats.SetActive(true);
                        ShowBuilding(true, targetedFloorScript);
                        usedMask = buildingMask;

                        Values lVal0 = lastFloorScript.valuesArray[0];
                        Values lVal1 = new Values();
                        Values lVal2 = new Values();

                        if (lastFloorScript.valuesArray.Length >= 2)
                        {
                            upgradeLevel1.SetActive(true);
                            lVal1 = lastFloorScript.valuesArray[1];
                        }
                        else
                        {
                            upgradeLevel1.SetActive(false);
                        }
                        if (lastFloorScript.valuesArray.Length >= 3)
                        {
                            upgradeLevel2.SetActive(true);
                            lVal2 = lastFloorScript.valuesArray[2];
                        }
                        else
                        {
                            upgradeLevel2.SetActive(false);
                        }
                        for (int i = 0; i < lastFloorScript.valuesArray.Length; i++)
                        {

                        }

                        if (lVal0.powerUse > 45)
                        {
                            buildingMenu.SetActive(false);
                            industryMenu.SetActive(true);
                            powerMenu.SetActive(false);

                            //BuildingManager buildingManager = lastFloorScript.GetComponentInParent<BuildingManager>();

                            IndustryLevel1.text = lastFloorScript.cost[1].ToString() + "$";
                            IndustryLevel2.text = lastFloorScript.cost[2].ToString() + "$";
                        }
                        else if (lVal0.powerUse >= 0)
                        {

                            buildingMenu.SetActive(true);
                            industryMenu.SetActive(false);
                            powerMenu.SetActive(false);

                            BuildingManager buildingManager = lastFloorScript.GetComponentInParent<BuildingManager>();

                            PrizeLevel1.text = lastFloorScript.cost[1].ToString() + "$";
                            PrizeLevel2.text = lastFloorScript.cost[2].ToString() + "$";
                            PrizeLevel1Building.text = (lastFloorScript.cost[1] * (buildingManager.floors.Length - 1)).ToString() + "$";
                            PrizeLevel2Building.text = (lastFloorScript.cost[2] * (buildingManager.floors.Length - 1)).ToString() + "$";

                            RoofLevel1.text = (buildingManager.floors[buildingManager.floors.Length - 1].cost[1]).ToString() + "$";
                            RoofLevel2.text = (buildingManager.floors[buildingManager.floors.Length - 1].cost[2]).ToString() + "$";

                            //level1Text.text =
                            //"power use:" + (lVal1.powerUse - lVal0.powerUse) + "\nhappiness:" + (lVal1.happiness - lVal0.happiness) + "\npollution:" + (lVal1.pollution - lVal0.pollution) + "\n\ncost:" + lastFloorScript.cost[1];


                            //level2Text.text =
                            //"power use:" + (lVal2.powerUse - lVal0.powerUse) + "\nhappiness:" + (lVal2.happiness - lVal0.happiness) + "\npollution:" + (lVal2.pollution - lVal0.pollution) + "\n\ncost:" + lastFloorScript.cost[2];
                        }
                        else
                        {
                            buildingMenu.SetActive(false);
                            industryMenu.SetActive(false);
                            powerMenu.SetActive(true);

                            //BuildingManager buildingManager = lastFloorScript.GetComponentInParent<BuildingManager>();

                            PowerLevel1.text = lastFloorScript.cost[1].ToString() + "$";
                            PowerLevel2.text = lastFloorScript.cost[2].ToString() + "$";

                            //level1Text.text =
                            //"power production:" + (-lVal1.powerUse) + "\nhappiness:" + (lVal1.happiness - lVal0.happiness) + "\npollution:" + (lVal1.pollution - lVal0.pollution) + "\n\ncost:" + lastFloorScript.cost[1];


                            //level2Text.text =
                            //"power production:" + (-lVal2.powerUse) + "\nhappiness:" + (lVal2.happiness - lVal0.happiness) + "\npollution:" + (lVal2.pollution - lVal0.pollution) + "\n\ncost:" + lastFloorScript.cost[2];
                        }



                    }
                    else
                    {
                        upgradeMenu.SetActive(false);
                        buildingStats.SetActive(false);
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
            }

            //if (!isOverUI && lastHit.transform.GetComponentInParent<floorScript>() == null)
        }

        if (lastFloorScript!=null && lastFloorScript.transform != null)
        {
            upgradeMenu.transform.position = Camera.main.WorldToScreenPoint(lastFloorScript.transform.position);
            upgradeMenu.transform.position -= new Vector3(menuOffset + distanceFac * Mathf.Sqrt(Vector3.Distance(mainCam.transform.position, lastFloorScript.transform.position)), 0, 0);

            buildingStats.transform.position = Camera.main.WorldToScreenPoint(lastFloorScript.transform.position);
            buildingStats.transform.position -= new Vector3(-menuOffset - distanceFac * Mathf.Sqrt(Vector3.Distance(mainCam.transform.position, lastFloorScript.transform.position)), 0, 0);

            Values bVal = lastFloorScript.transform.parent.GetComponent<BuildingManager>().values;

            buildingStatsText.text = "power use:" + bVal.powerUse + "\nhappiness:" + bVal.happiness + "\npollution:" + bVal.pollution + "\nfloors: " + lastFloorScript.transform.parent.childCount;
        }
    }

    public void upgradeCurrent(int level)
    {
        if (lastFloorScript != null)
        {
            if (gameManager.money >= lastFloorScript.cost[level])
            {
                if (level != lastFloorScript.currentLevel)
                {

                    hasUpgraded = true;

                    lastFloorScript.Upgrade(level);
                    lastFloorScript = lastFloorScript.transform.GetComponent<floorScript>();


                    gameManager.money -= lastFloorScript.cost[level];
                    CreateEffectsText("-$" + lastFloorScript.cost[level].ToString());

                    ShowBuilding(true, targetedFloorScript);
                }
                else
                {
                    soundDenied.Play();
                }
            }
            else
            {
                alertPopup.Popup("You don't have enough money to do this.", 3f);
                soundDenied.Play();
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

    public void upgradeRoof(int level)
    {
        if (lastFloorScript != null)
        {

            Transform building = lastFloorScript.transform.parent;
            floorScript roofScript = building.GetChild(building.childCount - 1).GetComponent<floorScript>();

            if (gameManager.money >= roofScript.cost[level])
            {
                if (level != roofScript.currentLevel)
                {
                    //hasUpgraded = true;

                    roofScript.Upgrade(level);
                    roofScript = roofScript.transform.GetComponent<floorScript>();


                    gameManager.money -= roofScript.cost[level];
                    CreateEffectsText("-$" + roofScript.cost[level].ToString());

                    ShowBuilding(true, roofScript);
                }
                else
                {
                    soundDenied.Play();
                }
            }
            else
            {
                alertPopup.Popup("You don't have enough money to do this.", 3f);
                soundDenied.Play();

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

            for (int i = 0; i < building.childCount - 1; i++)
            {
                floors[i] = building.GetChild(i).GetComponent<floorScript>();
                if (floors[i].currentLevel != level)
                {
                    upgradeCost += floors[i].cost[level];
                }
            }


            if (gameManager.money >= upgradeCost)
            {

                for(int i = 0; i < building.childCount - 1; i++)
                {
                    if(floors[i].currentLevel != level)
                    {
                        floors[i].UpgradeAfter(level, i * 0.1f);
                    }
                }

                hasUpgraded = true;


                lastFloorScript = lastFloorScript.transform.GetComponent<floorScript>();

                gameManager.money -= upgradeCost;
                CreateEffectsText("-$"+upgradeCost.ToString());

                ShowBuilding(true, targetedFloorScript);
            }
            else
            {
                alertPopup.Popup("You don't have enough money to do this.", 3f);
                soundDenied.Play();

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
                    floor.GetComponent<floorScript>().isSelected = true;
                }
            }

            isSelected = true;
            cameraMove.targetPosition = new Vector3(floorScr.transform.position.x, 1, floorScr.transform.position.z);
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
                    floor.GetComponent<floorScript>().isSelected = false;
                }
            }

            isSelected = false;
            cameraMove.ResetPosition();
            overlayEffect.SetActive(false);
        }
    }

    void PlaySelectSound() {
        soundSelect.Play();
        soundSelect.pitch = Mathf.Min(soundSelect.pitch+0.1f,2f);
        soundLastSelectedSomething = soundSelectReset;
    }

    void CreateEffectsText(String str){

        var effectObject = Instantiate(effectsText);
        effectObject.transform.parent = gameObject.transform;
        effectObject.GetComponent<TMP_Text>().text = str;
        //effectObject.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition;
        effectObject.transform.position = Input.mousePosition;
    }
}
