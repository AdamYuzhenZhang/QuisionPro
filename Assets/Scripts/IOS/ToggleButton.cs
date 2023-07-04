using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] private GameObject m_Target;
    public void Toggle()
    {
        m_Target.SetActive(!m_Target.activeSelf);
    }

    private void Start()
    {
        m_Target.SetActive(false);
    }
}
