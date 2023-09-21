using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class RebelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject rebelObject;

    [SerializeField] private List<string> defaultCritic;
    [SerializeField] private List<string> happinessCritic;
    [SerializeField] private List<string> powerCritic;
    [SerializeField] private List<string> tutorialCritic;

    [SerializeField] private TMP_Text textmesh;

    [SerializeField] private AudioSource protestSounds;
    private enum thingsToBeMadAbout { 
        pollution,
        happiness,
        power,
        tutorial
    }
    [SerializeField] private thingsToBeMadAbout theThingIAmMadAbout;

    GameManager manager;

    float timer;
    [SerializeField] private float timeTillUpdate;

    public string madAboutString;
    float percentageOfProtestors = 0;

    float rebelsCreated = 0;

    public bool inTutorial = true;

    [SerializeField] private bool manuallySpawnProtestor;

   // Start is called before the first frame update
   void Start()
    {
        manager = GameManager.Instance;
        CreateRebel();
        UpdateMadChoice();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeTillUpdate)
        {
            timer -= timeTillUpdate;
            UpdateMadChoice();
        }

        if (manuallySpawnProtestor)
        {
            CreateRebel();
            manuallySpawnProtestor = false;
        }
    }

    void UpdateMadChoice()
    {
        if (inTutorial)
        {
            theThingIAmMadAbout = thingsToBeMadAbout.tutorial;
        }
        else
        if (manager.powerUse > manager.powerProduction)
        {

            theThingIAmMadAbout = thingsToBeMadAbout.power;

        }
        else
        if (manager.happiness < 50)
        {

            theThingIAmMadAbout = thingsToBeMadAbout.happiness;

        }
        else
        {

            theThingIAmMadAbout = thingsToBeMadAbout.pollution;

        }


        switch (theThingIAmMadAbout)
        {
            case thingsToBeMadAbout.tutorial:
                chooseRandomString(tutorialCritic);
                break;
            case thingsToBeMadAbout.power:
                chooseRandomString(powerCritic);
                break;
            case thingsToBeMadAbout.happiness:
                chooseRandomString(happinessCritic);
                break;
            case thingsToBeMadAbout.pollution:
                chooseRandomString(defaultCritic);
                break;
        }

        RebellionTimer timer = manager.GetComponent<RebellionTimer>();

        float newProtestorPercent = (timer.currentRebellion / timer.maxRebellion);

        if(newProtestorPercent*10f > rebelsCreated)
        {
            CreateRebel();
        }


        protestSounds.volume = newProtestorPercent * 0.8f + 0.2f;
    }

    void chooseRandomString(List<string> stringList)
    {
        int index = Mathf.RoundToInt(Random.Range(0,stringList.Count));
        madAboutString = stringList[index];
        textmesh.text = madAboutString;
    }

    public void CreateRebel()
    {
         GameObject newObject = Instantiate(rebelObject, transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)), transform.rotation * Quaternion.Euler(0, Random.Range(-180, 180), 0));
         rebelsCreated++;
    }

     void OnMouseEnter()
    {
        textmesh.gameObject.SetActive(true);
    }
     void OnMouseExit()
    {
        textmesh.gameObject.SetActive(false);
    }
}
