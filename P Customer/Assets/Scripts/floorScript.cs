using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class floorScript : ValuesScript
{

    private GameObject model;
    [SerializeField] private ParticleSystem UpgradeParticles;
    [SerializeField] private AudioSource UpgradeSound;

    private int upgradeTo;

    public bool isSelected;

    

    //private BuildingManager connectedManager;

    // Start is called before the first frame update
    void Start()
    {
        model = Instantiate(levelModels[0], transform);
        ParticleSystem instantiatedParticles = Instantiate(UpgradeParticles, transform);
        UpgradeParticles = instantiatedParticles;
        UpgradeParticles.Stop();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.7f, 1f, 0.7f), 0.1f);
    }


    public void Hovered()
    {
        transform.localScale = new Vector3(1f, 1, 1f);
    }

    public void Upgrade(int level)
    {
        if (level < levelModels.Length && currentLevel != level)
        {
            Destroy(model);
            model = Instantiate(levelModels[level], transform);

            UpgradeSound.pitch = 1f;
            UpgradeSound.Play();

            currentLevel = level;

            if (UpgradeParticles != null)
            {
                UpgradeParticles.Emit(80);
                transform.localScale = new Vector3(1f, 1, 1f);
            }
        }
    }

    public void UpgradeAfter(int level, float delay)
    {
        upgradeTo = level;
        UpgradeSound.pitch = 1f+(delay*2f);
        Invoke("UpgradeDelay", delay);

        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    transform.GetChild(i).gameObject.layer = 8;
        //}
    }

    private void UpgradeDelay()
    {
        if (upgradeTo < levelModels.Length && currentLevel != upgradeTo)
        {
            Destroy(model);
            model = Instantiate(levelModels[upgradeTo], transform);

            if(isSelected)
            {
                model.layer = 8;
            }

            currentLevel = upgradeTo;

            UpgradeSound.pitch += 0.1f;
            UpgradeSound.Play();

            if (UpgradeParticles != null)
            {
                UpgradeParticles.Emit(80);
                transform.localScale = new Vector3(1f, 1, 1f);
            }
        }
    }

}
