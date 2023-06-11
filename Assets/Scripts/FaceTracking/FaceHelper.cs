using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceHelper : MonoBehaviour
{
    [SerializeField] private OVRFaceExpressions m_FaceExpressions;

    private void Update()
    {
        float[] myArray = m_FaceExpressions.ToArray();
        
        Debug.Log(myArray.Length);
        
        string concatenatedString = string.Empty;
        for (int i = 0; i < myArray.Length; i++)
        {
            concatenatedString += myArray[i].ToString();
            if (i < myArray.Length - 1)
            {
                concatenatedString += ", ";
            }
        }

        Debug.Log(concatenatedString);
    }
}
