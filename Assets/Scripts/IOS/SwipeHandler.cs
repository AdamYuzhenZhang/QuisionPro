using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeHandler : MonoBehaviour
{
    [SerializeField] private SwipeManager m_SwipeManager;
    [SerializeField] private MainCharacterController m_CharacterController;
    [SerializeField] private ModelController m_ModelController;

    private void OnEnable()
    {
        if (m_SwipeManager) m_SwipeManager.OnSwipe += HandleSwipe;
    }

    private void OnDisable()
    {
        if (m_SwipeManager) m_SwipeManager.OnSwipe -= HandleSwipe;
    }

    private void HandleSwipe(SwipeManager.SwipeDirection direction)
    {
        switch (direction)
        {
            case SwipeManager.SwipeDirection.Up:
                // Do something for an upward swipe
                Debug.Log("Upward swipe detected!");
                m_ModelController.NextModel();
                break;
            case SwipeManager.SwipeDirection.Down:
                // Do something for a downward swipe
                Debug.Log("Downward swipe detected!");
                m_ModelController.PreviousModel();
                break;
            case SwipeManager.SwipeDirection.Left:
                // Do something for a left swipe
                Debug.Log("Left swipe detected!");
                m_CharacterController.PreviousCharacter();
                break;
            case SwipeManager.SwipeDirection.Right:
                // Do something for a right swipe
                Debug.Log("Right swipe detected!");
                m_CharacterController.NextCharacter();
                break;
            default:
                break;
        }
    }
}
