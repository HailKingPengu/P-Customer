using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fxtxtBehavior : MonoBehaviour
{
    [SerializeField] private float size;
    [SerializeField] private float sizeDecreasePercentage;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(size,size,size);
    }

    // Update is called once per frame
    void Update()
    {
        size = Mathf.Lerp(size,0,sizeDecreasePercentage*(Time.deltaTime*60));
        transform.localScale = new Vector3(size,size,size);
        if(size<=0.01f){Destroy(gameObject);};
    }
}
