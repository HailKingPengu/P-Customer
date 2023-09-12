using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class floorScript : ValuesScript
{

    private GameObject model;
    [SerializeField] private ParticleSystem UpgradeParticles;

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

            currentLevel = level;

            if (UpgradeParticles != null)
            {
                UpgradeParticles.Emit(80);
            }
        }
    }

}
