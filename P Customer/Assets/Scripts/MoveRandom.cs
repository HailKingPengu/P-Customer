using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveRandom : MonoBehaviour
{

    Rigidbody rb;
    Vector3 dir;
    Vector3 desiredPosition;
    public float force;
    [SerializeField] private bool canStop = true;
    [SerializeField] private bool followSidewalks = true;
    [SerializeField] private bool doRotate = false;
    float wait = 0;
    Vector3 dirChange = new Vector3(0f,0f,0f);

    float objectdistance = 0.5f;

    [SerializeField] private NavigatorMover mymover;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        desiredPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        if (Vector3.Distance(this.transform.position, desiredPosition) <= objectdistance)
        {
            // choose something new to do
            if (canStop)
            {
                if (Random.Range(0, 10) > 7)
                {
                    wait = Random.Range(20, 80);
                }
            }

            mymover = mymover.GetNewLocation();

            float difference = 0.2f;
            desiredPosition = getPosition(mymover);
            if (followSidewalks)
            {
                desiredPosition += new Vector3(Random.Range(-difference, difference), 0f, Random.Range(-difference, difference));
            }
            

        }
        if (doRotate)
        {
            Vector3 relativePos = desiredPosition - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
        }

        float dirChangeIntensity = 0.1f;
        dirChange.x += Random.Range(-dirChangeIntensity,dirChangeIntensity);
        dirChange.z += Random.Range(-dirChangeIntensity,dirChangeIntensity);
        dirChange.x = Mathf.Clamp(dirChange.x,-1f,1f);
        dirChange.z = Mathf.Clamp(dirChange.y,-1f,1f);

        dir = getDirection(desiredPosition)+dirChange;
    }



    private void FixedUpdate()
    {
        if(wait<=0){
            rb.AddForce(dir * force);
            Debug.DrawRay(transform.position, dir * objectdistance, Color.red);
        }else{
            wait--;
        }
    }

    Vector3 ChooseRandom ()
    {
        float r1 = Random.Range(-1f, 1f);
        float r2 = Random.Range(-1f, 1f);
        return (new Vector3(r1, 0f, r2));
    }

    Vector3 getDirection(Vector3 nav)
    {

        Vector3 dir = nav - this.transform.position;
        dir.Normalize();
        return new Vector3(dir.x,dir.y,dir.z);
    }

    Vector3 getPosition(NavigatorMover nav)
    {
        Vector3 p = nav.myposition;
        return new Vector3(p.x, this.transform.position.y, p.z);
    }


}
