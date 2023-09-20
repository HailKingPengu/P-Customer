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
    public List<GameObject> Paths = new List<GameObject>();

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

            if(NorthPath!=null){Paths.Add(NorthPath);}
            if(SouthPath!=null){Paths.Add(SouthPath);}
            if(EastPath!=null){Paths.Add(EastPath);}
            if(WestPath!=null){Paths.Add(WestPath);}

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



    public NavigatorMover GetNewLocation(GameObject prevNavMover)
    {
        GameObject returnal = null;
        int tries = 0;



            if(Paths.Count>1){
                while(returnal==null){
                    int r = Random.Range(0, Paths.Count);
                    Debug.Log(r);
                    returnal = Paths[r];
                    if(GameObject.ReferenceEquals( returnal, prevNavMover)){returnal = null;};
                }
            } else
            if(Paths.Count==1) {
                returnal = Paths[0];
            }

            if (returnal == null) { returnal = transform.gameObject; }


        return (returnal.GetComponent<NavigatorMover>() );
    }

    public GameObject getObject() {
        return gameObject;
    }
}
