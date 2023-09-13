using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMLookRMBOnly : MonoBehaviour {

    [SerializeField] private float scale = 3;
    [SerializeField] private float smoothScale = 3;
    [SerializeField] private float smoothing = 0.1f;
    private CinemachineFreeLook.Orbit[] orbits;

    [SerializeField] private CinemachineFreeLook cmFreelook;

    void Start()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;

        orbits = new CinemachineFreeLook.Orbit[cmFreelook.m_Orbits.Length];

        for (int i = 0; i < cmFreelook.m_Orbits.Length; i++)
        {
            orbits[i] = cmFreelook.m_Orbits[i];
        }
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

        scale -= 0.1f * Input.mouseScrollDelta.y;
        scale = Mathf.Clamp(scale, 0.3f, 3f);

        smoothScale = Mathf.Lerp(smoothScale, Mathf.Clamp(scale, 0.3f, 3f), smoothing);

        //Debug.Log("y = " + Input.mouseScrollDelta.y);
        //Debug.Log("x = " + Input.mouseScrollDelta.x);

        for (int i = 0; i < cmFreelook.m_Orbits.Length; i++)
        {
            cmFreelook.m_Orbits[i].m_Radius = orbits[i].m_Radius * smoothScale;
            cmFreelook.m_Orbits[i].m_Height = orbits[i].m_Height * smoothScale;
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