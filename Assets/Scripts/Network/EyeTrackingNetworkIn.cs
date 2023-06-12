using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Grabs the transforms of the tracked eyeball and asigns it to the networked prefabs
/// </summary>
public class EyeTrackingNetworkIn : MonoBehaviour
{
    // Tracked eyes
    [SerializeField] private Transform m_LeftEye;
    [SerializeField] private Transform m_RightEye;

    // Networked eyes
    private GameObject m_LeftEyeNetworked;
    private GameObject m_RightEyeNetworked;

    private bool m_EyesFound;

    private void GetNetworkedEyes()
    {
        m_LeftEyeNetworked = GameObject.FindGameObjectWithTag("LeftEyeNetworked");
        m_RightEyeNetworked = GameObject.FindGameObjectWithTag("RightEyeNetworked");
        if (m_LeftEyeNetworked && m_RightEyeNetworked) m_EyesFound = true;
    }

    private void Update()
    {
        // update transform;
        if (m_EyesFound)
        {
            m_LeftEyeNetworked.transform.rotation = m_LeftEye.rotation;
            m_LeftEyeNetworked.transform.position = m_LeftEye.position;
            
            m_RightEyeNetworked.transform.rotation = m_RightEye.rotation;
            m_RightEyeNetworked.transform.position = m_RightEye.position;
        }
    }

    private void Start()
    {
        GetNetworkedEyes();
    }
}
