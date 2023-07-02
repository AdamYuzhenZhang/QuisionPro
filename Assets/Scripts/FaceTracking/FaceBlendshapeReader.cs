using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceBlendshapeReader : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer m_SourceFaceSkinnedMesh;
    [SerializeField] private SkinnedMeshRenderer m_NetworkedFaceSkinnedMesh;

    private bool m_FaceFound;
    
    private void CopyBlendShapeValues()
    {
        int blendShapeCount = m_SourceFaceSkinnedMesh.sharedMesh.blendShapeCount;

        for (int i = 0; i < blendShapeCount; i++)
        {
            //string blendShapeName = m_SourceFaceSkinnedMesh.sharedMesh.GetBlendShapeName(i);
            float blendShapeWeight = m_SourceFaceSkinnedMesh.GetBlendShapeWeight(i);
            m_NetworkedFaceSkinnedMesh.SetBlendShapeWeight(i, blendShapeWeight);
        }
    }

    private void GetNetworkedFace()
    {
        GameObject face = GameObject.FindGameObjectWithTag("RightEyeNetworked");
        m_NetworkedFaceSkinnedMesh = face.GetComponent<SkinnedMeshRenderer>();
        if (m_NetworkedFaceSkinnedMesh) m_FaceFound = true;
    }

    private void Update()
    {
        if (m_FaceFound) CopyBlendShapeValues();
    }

    private void Start()
    {
        GetNetworkedFace();
    }
}
