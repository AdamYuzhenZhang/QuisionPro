using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField] private MeshRenderer m_IconMesh;
    [SerializeField] private Material m_MatDefault;
    [SerializeField] private Material m_MatHovered;
    [SerializeField] private Material m_MatSelected;
    
    public void OnHover()
    {
        Debug.Log("On Hover Called");
        m_IconMesh.material = m_MatHovered;
    }
    
    public void OnHoverEnd()
    {
        Debug.Log("On Hover End Called");
        m_IconMesh.material = m_MatDefault;
    }

    

    public void OnSelected()
    {
        Debug.Log("On Selected Called");
        m_IconMesh.material = m_MatSelected;
    }

    public void OnDeselected()
    {
        Debug.Log("On Dis Selected Called");
        m_IconMesh.material = m_MatHovered;
    }
}
