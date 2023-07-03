using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeballCharacter : CharacterControllerBase
{
    protected override void UpdateEyes(Transform leftEyeTracked, Transform rightEyeTracked)
    {
        base.UpdateEyes(leftEyeTracked, rightEyeTracked);
        m_LeftEye.localPosition = leftEyeTracked.localPosition;
        m_RightEye.localPosition = rightEyeTracked.localPosition;
        Debug.Log("EyeballCharacterEyesCalled");
    }

    protected override void UpdateCharacterLocal(Transform leftEyeTracked, Transform rightEyeTracked, SkinnedMeshRenderer faceTracked)
    {
        UpdateEyes(leftEyeTracked, rightEyeTracked);
        Debug.Log("EyeballCharacter Update Called");

    }
}
