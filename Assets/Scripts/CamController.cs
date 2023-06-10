using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] private Camera m_ViewerCam;
    [SerializeField] private Camera m_ARCam;
    [SerializeField] private Camera m_SpectatorCam;

    public void SwitchToViewerCam()
    {
        m_SpectatorCam.enabled = false;
        m_ARCam.enabled = false;
        m_ViewerCam.enabled = true;
    }
    public void SwitchToARCam()
    {
        m_SpectatorCam.enabled = false;
        m_ViewerCam.enabled = false;
        m_ARCam.enabled = true;
    }

    public void SwitchToSpectatorCam()
    {
        m_ARCam.enabled = false;
        m_ViewerCam.enabled = false;
        m_SpectatorCam.enabled = true;
    }
}
