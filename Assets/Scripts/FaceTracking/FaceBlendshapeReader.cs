using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceBlendshapeReader : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer m_SourceFaceSkinnedMesh;
    [SerializeField] private SkinnedMeshRenderer m_TargetFaceSkinnedMesh;

    private void CopyBlendShapeValues()
    {
        int blendShapeCount = m_SourceFaceSkinnedMesh.sharedMesh.blendShapeCount;

        for (int i = 0; i < blendShapeCount; i++)
        {
            //string blendShapeName = m_SourceFaceSkinnedMesh.sharedMesh.GetBlendShapeName(i);
            float blendShapeWeight = m_SourceFaceSkinnedMesh.GetBlendShapeWeight(i);
            m_TargetFaceSkinnedMesh.SetBlendShapeWeight(i, blendShapeWeight);
        }
    }

    private void Update()
    {
        CopyBlendShapeValues();
    }
}
