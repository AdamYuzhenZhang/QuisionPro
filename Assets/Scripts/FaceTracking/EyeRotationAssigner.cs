using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeRotationAssigner : MonoBehaviour
{
    [SerializeField] private Transform m_LeftEyeSource;
    [SerializeField] private Transform m_LeftEyeTarget;
    [SerializeField] private Transform m_RightEyeSource;
    [SerializeField] private Transform m_RightEyeTarget;

    private void Update()
    {
        m_LeftEyeTarget.localRotation = m_LeftEyeSource.localRotation;
        m_RightEyeTarget.localRotation = m_RightEyeSource.localRotation;
    }
}
