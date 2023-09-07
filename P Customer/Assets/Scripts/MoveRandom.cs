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
    int timer = 30;
    bool imready = false;

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

            mymover = mymover.GetNewLocation();

        }
        desiredPosition = getPosition(mymover);
        dir = getDirection(mymover);
    }



    private void FixedUpdate()
    {
        rb.AddForce(dir * force);
        Debug.DrawRay(transform.position, dir * objectdistance, Color.red);
        Debug.DrawRay(transform.position, dir * objectdistance, Color.red);
    }

    Vector3 ChooseRandom ()
    {
        float r1 = Random.Range(-1f, 1f);
        float r2 = Random.Range(-1f, 1f);
        return (new Vector3(r1, 0f, r2));
    }

    Vector3 getDirection(NavigatorMover nav)
    {

        Vector3 dir = nav.myposition - this.transform.position;
        dir.Normalize();
        return new Vector3(dir.x,dir.y,dir.z);
    }

    Vector3 getPosition(NavigatorMover nav)
    {
        Vector3 p = nav.myposition;
        return new Vector3(p.x, this.transform.position.y, p.z);
    }


}
