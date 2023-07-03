using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class EyeInteractable : MonoBehaviour
{
    public bool IsHovered { get; set; }
    [SerializeField] private bool hovered;
    public bool IsSelected { get; set; }
    [SerializeField] private bool selected;
    
    [SerializeField] private UnityEvent<GameObject> OnObjectHover;
    [SerializeField] private UnityEvent<GameObject> OnObjectHoverEnded;
    [SerializeField] private UnityEvent<GameObject> OnObjectSelected;
    [SerializeField] private UnityEvent<GameObject> OnObjectDiselected;
    [SerializeField] private GameObject m_Model;

    public void Select(bool state)
    {
        IsSelected = state;
    }
    
    private void Update()
    {
        if (IsHovered && !hovered)
        {
            // just hovered
            hovered = true;
            OnObjectHover?.Invoke(gameObject);
        } 
        else if (!IsHovered && hovered)
        {
            // just hover ended
            hovered = false;
            OnObjectHoverEnded?.Invoke(gameObject);
        }

        if (IsSelected && !selected)
        {
            selected = true;
            OnObjectSelected?.Invoke(gameObject);
        }
        else if (!IsSelected && selected)
        {
            selected = false;
            OnObjectDiselected?.Invoke(gameObject);
        }
    }
}
