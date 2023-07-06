using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CheckBox : MonoBehaviour
{
    [SerializeField] private UnityEvent ToggleOnEvent;
    [SerializeField] private UnityEvent ToggleOffEvent;
    [SerializeField] private Sprite m_OnImg;
    [SerializeField] private Sprite m_OffImg;
    private Button m_Button;
    private Image m_Image;

    [SerializeField] private bool m_IsTrue;

    public void Toggle()
    {
        if (m_IsTrue)
        {
            ToggleOffEvent?.Invoke();
            m_IsTrue = false;
            m_Image.sprite = m_OffImg;
        }
        else
        {
            ToggleOnEvent?.Invoke();
            m_IsTrue = true;
            m_Image.sprite = m_OnImg;

        }
    }

    private void Start()
    {
        m_Image = GetComponent<Image>();
        m_Button = GetComponent<Button>();
        if (m_IsTrue)
        {
            ToggleOnEvent?.Invoke();
            m_Image.sprite = m_OnImg;

        }
        else
        {
            ToggleOffEvent?.Invoke();
            m_Image.sprite = m_OffImg;

        }
        m_Button.onClick.AddListener(Toggle);
    }
}
