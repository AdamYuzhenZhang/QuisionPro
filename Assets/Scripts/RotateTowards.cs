using UnityEngine;

public class RotateTowards : MonoBehaviour
{
    private void Update()
    {
        LookAtObject(Vector3.zero);
    }

    private void LookAtObject(Vector3 targetPos)
    {
        transform.LookAt(targetPos);
    }
}
