using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NavigatorMover : MonoBehaviour
{
    public GameObject[] NavObjects;
    public float rayLength = 2f;
    public bool findNav = true;
    [SerializeField] private LayerMask layermask;

    private int delay = 10;

    public bool ready = false;

    public GameObject NorthPath; // 1,0,0
    public GameObject SouthPath; // -1,0,0
    public GameObject EastPath; // 0,0,-1
    public GameObject WestPath; // 0,0,1

    public Vector3 myposition;
    // Start is called before the first frame update
    void Start()
    {
        myposition = transform.position;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        delay-=1;
        if (delay<=0 && findNav)
        {
            NorthPath = HitOtherNavs(new Vector3(1f, 0f, 0f));
            SouthPath = HitOtherNavs(new Vector3(-1f, 0f, 0f));
            EastPath = HitOtherNavs(new Vector3(0f, 0f, 1f));
            WestPath = HitOtherNavs(new Vector3(0f, 0f, -1f));

            findNav = false;
            ready = true;
        }
    }


    GameObject HitOtherNavs(Vector3 direction)
    {
        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, direction, out hit, rayLength, layermask))
        {
                Debug.DrawRay(transform.position, direction * hit.distance, Color.yellow);
                return (hit.transform.gameObject);
        }
        return (null);
    }



    public NavigatorMover GetNewLocation()
    {
        GameObject returnal = null;
        int tries = 0;

        if (NorthPath == null && SouthPath == null && EastPath == null && WestPath == null) { Debug.Log("HEEELPEF"); }

        while (returnal == null && tries < 100) {
            tries++;
            int r = Random.Range(0, 4);

            switch (r) {
                case 0:
                    returnal = NorthPath;
                   break;
                case 1:
                    returnal = SouthPath;
                    break;
                case 2:
                    returnal = EastPath;
                    break;
                case 3:
                    returnal= WestPath;
                    break;
            }
        }
        if (returnal == null) { returnal = transform.gameObject; }

        return (returnal.GetComponent<NavigatorMover>() );
    }
}
