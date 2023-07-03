using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.PoseDetection.Debug;
using Unity.VisualScripting;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    [SerializeField] private GameObject[] m_Models;
    private int m_ActiveIndex;

    private void DisableAllModels()
    {
        foreach (GameObject m in m_Models)
        {
            m.SetActive(false);
        }
    }

    private void TurnOnActiveModel()
    {
        DisableAllModels();
        m_Models[m_ActiveIndex].SetActive(true);
    }

    public void ChangeToModel(int index)
    {
        m_ActiveIndex = index;
        TurnOnActiveModel();
    }

    private void Start()
    {
        ChangeToModel(0);
    }
}
