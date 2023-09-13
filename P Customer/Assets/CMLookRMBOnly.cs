using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMLookRMBOnly : MonoBehaviour {

    [SerializeField] private float scale;
    [SerializeField] private CinemachineFreeLook cmFreelook;

    void Start() {
        CinemachineCore.GetInputAxis = GetAxisCustom;
    }

    void Update()
    {

        //if(Input.mouseScrollDelta.y > 0)
        //{
        //    scale += 0.05f;
        //}
        //else if (Input.mouseScrollDelta.y < 0)
        //{
        //    scale -= 0.05f;
        //}

        scale = Mathf.Clamp(1 - 0.1f * Input.mouseScrollDelta.y, 0.3f, 3f);

        //Debug.Log("y = " + Input.mouseScrollDelta.y);
        //Debug.Log("x = " + Input.mouseScrollDelta.x);

        for (int i = 0; i < cmFreelook.m_Orbits.Length; i++)
        {
            cmFreelook.m_Orbits[i].m_Radius *= scale;
            cmFreelook.m_Orbits[i].m_Height *= scale;
        }
    }

    public float GetAxisCustom(string axisName)
    {
        if (axisName == "Mouse X")
        {
            if (Input.GetMouseButton(2))
            {
                return UnityEngine.Input.GetAxis("Mouse X");
            }
            else
            {
                return 0;
            }
        }
        else if (axisName == "Mouse Y")
        {
            if (Input.GetMouseButton(2))
            {
                return UnityEngine.Input.GetAxis("Mouse Y");
            }
            else
            {
                return 0;
            }
        }
        return UnityEngine.Input.GetAxis(axisName);
    }
}