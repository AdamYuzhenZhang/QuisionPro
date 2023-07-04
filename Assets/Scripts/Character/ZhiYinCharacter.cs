using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZhiYinCharacter : CharacterControllerBase
{
    [SerializeField] private GameObject m_LeftEyeClosed;
    [SerializeField] private GameObject m_RightEyeClosed;

    private bool m_LeftClosed;
    private bool m_RightClosed;

    [SerializeField] private float m_Threshold = 75f;
    
    protected override void UpdateFace(SkinnedMeshRenderer faceTracked)
    {
        // left eye 12
        float leftClosedVal = faceTracked.GetBlendShapeWeight(12);
        // right eye 13
        float rightClosedVal = faceTracked.GetBlendShapeWeight(13);
        
        if (leftClosedVal > m_Threshold) CloseLeftEye();
        else OpenLeftEye();

        if (rightClosedVal > m_Threshold) CloseRightEye();
        else OpenRightEye();
    }

    private void CloseLeftEye()
    {
        if (!m_LeftClosed)
        {
            m_LeftEye.gameObject.SetActive(false);
            m_LeftEyeClosed.SetActive(true);
            m_LeftClosed = true;
        }
    }

    private void OpenLeftEye()
    {
        if (m_LeftClosed)
        {
            m_LeftEye.gameObject.SetActive(true);
            m_LeftEyeClosed.SetActive(false);
            m_LeftClosed = false;
        }
    }
    
    private void CloseRightEye()
    {
        if (!m_RightClosed)
        {
            m_RightEye.gameObject.SetActive(false);
            m_RightEyeClosed.SetActive(true);
            m_RightClosed = true;
        }
    }

    private void OpenRightEye()
    {
        if (m_RightClosed)
        {
            m_RightEye.gameObject.SetActive(true);
            m_RightEyeClosed.SetActive(false);
            m_RightClosed = false;
        }
    }
}
