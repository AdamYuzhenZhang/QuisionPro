using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestSceneInitializer : MonoBehaviour
{
    [SerializeField] private TextMeshPro m_IPText;

    IEnumerator SetIPText()
    {
        yield return new WaitForSeconds(2f);
        m_IPText.text = FindObjectOfType<NetworkController>().GetLocalIPAddress();
    }

    private void Start()
    {
        //StartCoroutine(SetIPText());
        m_IPText.text = FindObjectOfType<NetworkController>().GetLocalIPAddress();
    }
}
