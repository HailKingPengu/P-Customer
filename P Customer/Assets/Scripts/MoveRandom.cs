using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRandom : MonoBehaviour
{

    Rigidbody rb;
    Vector3 dir;
    public float force;
    int timer = 30;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dir = ChooseRandom();


    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(dir * force);
    }

    private void FixedUpdate()
    {
        timer--;
        if (timer <= 0)
        {
            dir = ChooseRandom();
            timer = 30;
        }
    }

    Vector3 ChooseRandom ()
    {
        float r1 = Random.Range(-1f, 1f);
        float r2 = Random.Range(-1f, 1f);
        return (new Vector3(r1, 0f, r2));
    }
}
