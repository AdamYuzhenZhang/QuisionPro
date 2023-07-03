using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    [SerializeField] private GameObject[] m_Screens;
    private int m_ActiveIndex;

    private void DisableAllScreens()
    {
        foreach (GameObject m in m_Screens)
        {
            m.SetActive(false);
        }
    }

    private void TurnOnActiveScreen()
    {
        DisableAllScreens();
        m_Screens[m_ActiveIndex].SetActive(true);
    }

    public void ChangeToScreen(int index)
    {
        m_ActiveIndex = index;
        TurnOnActiveScreen();
    }

    private void Start()
    {
        ChangeToScreen(0);
    }
}
