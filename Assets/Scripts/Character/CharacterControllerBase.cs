using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for controlling characters
/// </summary>
public class CharacterControllerBase : MonoBehaviour
{
    // the set of elements for this character
    [SerializeField] protected Transform m_LeftEye;
    [SerializeField] protected Transform m_RightEye;
    [SerializeField] protected SkinnedMeshRenderer m_Face;
    
    /// <summary>
    /// Updates the positions of the eyes
    /// Default way is just apply the local rotations
    /// </summary>
    protected virtual void UpdateEyes(Transform leftEyeTracked, Transform rightEyeTracked)
    {
        m_LeftEye.localRotation = leftEyeTracked.localRotation;
        m_RightEye.localRotation = rightEyeTracked.localRotation;
    }

    /// <summary>
    /// Updates the blend shape values of the face
    /// Default way is copy all values
    /// </summary>
    protected virtual void UpdateFace(SkinnedMeshRenderer faceTracked)
    {
        int blendShapeCount = faceTracked.sharedMesh.blendShapeCount;
        for (int i = 0; i < blendShapeCount; i++)
        {
            float blendShapeValue = faceTracked.GetBlendShapeWeight(i);
            m_Face.SetBlendShapeWeight(i, blendShapeValue);
        }
    }

    protected virtual void UpdateCharacterLocal(Transform leftEyeTracked, Transform rightEyeTracked,
        SkinnedMeshRenderer faceTracked)
    {
        UpdateEyes(leftEyeTracked, rightEyeTracked);
        UpdateFace(faceTracked);
    }
    
    /// <summary>
    /// Calling this every frame to update this character
    /// </summary>
    public virtual void UpdateCharacter(Transform leftEyeTracked, Transform rightEyeTracked,
        SkinnedMeshRenderer faceTracked)
    {
        UpdateCharacterLocal(leftEyeTracked, rightEyeTracked, faceTracked);
    }
}
