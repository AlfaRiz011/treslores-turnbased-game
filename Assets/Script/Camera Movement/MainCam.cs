using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MainCam : MonoBehaviour
{
    // Start is called before the first frame update
    public CinemachineBrain brain;
    public bool SetBool = false;

     void Update()
    {
        brain = GetComponent<CinemachineBrain>();
        if (SetBool == false)
        {
            brain.m_DefaultBlend.m_Time = 1.0f;
        }
        else if (SetBool == true)
        {

            brain.m_DefaultBlend.m_Time = 0.0f;
        }
    }
}
