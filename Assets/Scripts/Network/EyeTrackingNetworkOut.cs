using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Grabs the transform of the networked eyes and apply it to the active eye
/// </summary>
public class EyeTrackingNetworkOut : MonoBehaviour
{
    // Active eyes
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
            m_LeftEye.localRotation = m_LeftEyeNetworked.transform.rotation;
            m_LeftEye.localPosition = m_LeftEyeNetworked.transform.position;
            
            m_RightEye.localRotation = m_RightEyeNetworked.transform.rotation;
            m_RightEye.localPosition = m_RightEyeNetworked.transform.position;
        }
        else
        {
            GetNetworkedEyes();
        }
    }
    
    private void Start()
    {
        GetNetworkedEyes();
    }
}
