using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RebelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject rebelObject;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0;i < 10;i++) {
            CreateRebel();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateRebel()
    {
        Debug.Log("HELP");
         GameObject newObject = Instantiate(rebelObject, transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)), transform.rotation);

    }
}
