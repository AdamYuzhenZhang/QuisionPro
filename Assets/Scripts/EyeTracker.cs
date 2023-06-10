//using System;
//using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

// Referenced Dilmer Valecillos's implementation
// https://www.youtube.com/watch?v=kIcvAi60qlI

[RequireComponent(typeof(ARFace))]
public class EyeTracker : MonoBehaviour
{
    [SerializeField] private GameObject m_EyePrefab;
    private GameObject m_LeftEye;
    private GameObject m_RightEye;
    private ARFace m_ARFace;
    //private XRFaceSubsystem m_XRFaceSubSystem;
    //[SerializeField] private TextMeshProUGUI m_DebugText;

    //private SceneController m_SceneController;

    private void Awake()
    {
        m_ARFace = GetComponent<ARFace>();
        m_ARFace = GetComponent<ARFace>();
    }

    private void OnEnable()
    {
        ARFaceManager faceManager = FindObjectOfType<ARFaceManager>();
        if (faceManager && faceManager.subsystem != null &&
            faceManager.subsystem.subsystemDescriptor.supportsEyeTracking)
        {
            m_ARFace.updated += OnUpdated;
            //if (m_DebugText) m_DebugText.text = "Eye tracking supported";
        }
        else
        {
            //if (m_DebugText) m_DebugText.text = "Eye tracking not supported";
        }
    }

    private void OnDisable()
    {
        m_ARFace.updated -= OnUpdated;
        SetVisibility(false);
    }

    private void OnUpdated(ARFaceUpdatedEventArgs eventArgs)
    {
        if (m_ARFace.leftEye && !m_LeftEye)
        {
            m_LeftEye = Instantiate(m_EyePrefab, m_ARFace.leftEye);
            m_LeftEye.SetActive(false);
            SceneController sceneController = FindObjectOfType<SceneController>();
            if (sceneController) sceneController.LeftEye = m_LeftEye;
        }
        if (m_ARFace.rightEye && !m_RightEye)
        {
            m_RightEye = Instantiate(m_EyePrefab, m_ARFace.rightEye);
            m_RightEye.SetActive(false);
            SceneController sceneController = FindObjectOfType<SceneController>();
            if (sceneController) sceneController.RightEye = m_RightEye;
        }

        bool visible = (m_ARFace.trackingState == TrackingState.Tracking) &&
                       (ARSession.state > ARSessionState.Ready);
        SetVisibility(visible);
    }

    private void SetVisibility(bool isVisible)
    {
        if (m_LeftEye && m_RightEye)
        {
            m_LeftEye.SetActive(isVisible);
            m_RightEye.SetActive(isVisible);
        }
    }
}
