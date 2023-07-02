using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeRotationAssigner : MonoBehaviour
{
    [SerializeField] private Transform m_LeftEyeSource;
    [SerializeField] private Transform m_LeftEyeTarget;

    private void Update()
    {
        m_LeftEyeTarget.localRotation = m_LeftEyeSource.localRotation;
    }
}
