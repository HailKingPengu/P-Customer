using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    private Vector3 startingPosition;
    public Vector3 targetPosition;

    [SerializeField] private float smoothing = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
    }

    public void ResetPosition()
    {
        targetPosition = startingPosition;
    }
}
