using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestEyeTrackingRay : MonoBehaviour
{
    [SerializeField] private float m_RayDistance = 2f;
    [SerializeField] private LayerMask m_IncludedLayers;
    //private List<EyeInteractable> m_EyeInteractables = new List<EyeInteractable>();
    private Dictionary<int, EyeInteractable> m_EyeInteractables = new Dictionary<int, EyeInteractable>();
    private EyeInteractable m_LastInteractable;
    [SerializeField] private OVRHand m_ActiveHand;
    private bool m_CanSelect;

    private void Start()
    {
        if (m_ActiveHand) m_CanSelect = true;
    }

    private void Update()
    {
        SelectionStarted();
    }

    
    private void FixedUpdate()
    {
        if (IsPinching()) return;
        RaycastHit hit;
        Vector3 rayCastDirection = transform.TransformDirection(Vector3.forward) * m_RayDistance;
        if (Physics.Raycast(transform.position, rayCastDirection, out hit, Mathf.Infinity, m_IncludedLayers))
        {
            OnHoverExit();
            if (!m_EyeInteractables.TryGetValue(hit.transform.gameObject.GetHashCode(), out EyeInteractable eyeInteractable))
            {
                eyeInteractable = hit.transform.GetComponent<EyeInteractable>();
                m_EyeInteractables.Add(hit.transform.gameObject.GetHashCode(), eyeInteractable);
            }
            eyeInteractable.IsHovered = true;
            m_LastInteractable = eyeInteractable;
        }
        else
        {
            OnHoverExit();
            m_LastInteractable = null;
        }
    }


    private bool IsPinching() => (m_CanSelect && m_ActiveHand.GetFingerIsPinching(OVRHand.HandFinger.Index));

    private void OnDestroy()
    {
        m_EyeInteractables.Clear();
    }

    private void OnHoverExit()
    {
        foreach (var i in m_EyeInteractables)
        {
            i.Value.IsHovered = false;
        }
    }

    private void SelectionStarted()
    {
        if (IsPinching())
        {
            m_LastInteractable?.Select(true);
        }
        else
        {
            m_LastInteractable?.Select(false);
        }
    }
}
