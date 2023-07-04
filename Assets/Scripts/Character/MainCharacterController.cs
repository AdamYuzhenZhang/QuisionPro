using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Changes the player character
/// </summary>
public class MainCharacterController : MonoBehaviour
{
    // Elements with synchronized data
    [SerializeField] private Transform m_LeftEyeTracked;
    [SerializeField] private Transform m_RightEyeTracked;    
    [SerializeField] private SkinnedMeshRenderer m_FaceTracked;

    // All characters
    [SerializeField] private CharacterControllerBase[] m_CharacterControllers;
    // The active character
    private int m_ActiveIndex;
    private CharacterControllerBase m_ActiveCharacterController;

    private void DisableAllCharacters()
    {
        foreach (CharacterControllerBase c in m_CharacterControllers)
        {
            c.gameObject.SetActive(false);
        }
        m_ActiveCharacterController = null;
    }

    private void UpdateActiveCharacter()
    {
        DisableAllCharacters();
        m_ActiveCharacterController = m_CharacterControllers[m_ActiveIndex];
        m_ActiveCharacterController.gameObject.SetActive(true);
    }

    public void ChangeCharacter(int index)
    {
        m_ActiveIndex = index;
        UpdateActiveCharacter();
    }

    private void UpdateActiveCharacterValues()
    {
        if (m_ActiveCharacterController)
            m_ActiveCharacterController.UpdateCharacter(m_LeftEyeTracked, m_RightEyeTracked, m_FaceTracked);
    }

    private void Update()
    {
        UpdateActiveCharacterValues();
    }

    public void NextCharacter()
    {
        if (m_ActiveIndex < m_CharacterControllers.Length - 1)
        {
            m_ActiveIndex++;
            UpdateActiveCharacter();
        }
    }
    public void PreviousCharacter()
    {
        if (m_ActiveIndex > 0)
        {
            m_ActiveIndex--;
            UpdateActiveCharacter();
        }
    }

    private void Start()
    {
        ChangeCharacter(0);
    }
}
