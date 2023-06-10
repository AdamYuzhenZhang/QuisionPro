using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The projection code is based on TheParallaxView project
// https://github.com/algomystic/TheParallaxView

public class Projection : MonoBehaviour
{
    [SerializeField] private Camera m_ViewerCam;
    [SerializeField] private GameObject m_TargetObject;
    private float left, right, bottom, top, near, far;
    private void Update()
    {
        // look opposite direction of device cam
        Quaternion q = m_TargetObject.transform.rotation * Quaternion.Euler(Vector3.up * 180);
        m_ViewerCam.transform.rotation = q;

        Vector3 deviceCamPos = m_ViewerCam.transform.worldToLocalMatrix.MultiplyPoint( m_TargetObject.transform.position ); // find device camera in rendering camera's view space
        Vector3 fwd = m_ViewerCam.transform.worldToLocalMatrix.MultiplyVector (m_TargetObject.transform.forward); // normal of plane defined by device camera
        Plane device_plane = new Plane( fwd, deviceCamPos);

        Vector3 close = device_plane.ClosestPointOnPlane (Vector3.zero);
        near = close.magnitude;
        
        // landscape iPhone X, measures in meters
        left = deviceCamPos.x - 0.005f;
        right = deviceCamPos.x + 0.135f;
        top = deviceCamPos.y + 0.024f;
        bottom = deviceCamPos.y - 0.042f;
        
        far = 10f; // may need bigger for bigger scenes, max 10 metres for now

        Vector3 topLeft = new Vector3 (left, top, near);
        Vector3 topRight = new Vector3 (right, top, near);
        Vector3 bottomLeft = new Vector3 (left, bottom, near);
        Vector3 bottomRight = new Vector3 (right, bottom, near);
        
        // move near to 0.01 (1 cm from eye)
        float scale_factor = 0.01f / near;
        near *= scale_factor;
        left *= scale_factor;
        right *= scale_factor;
        top *= scale_factor;
        bottom *= scale_factor;

        Matrix4x4 m = PerspectiveOffCenter(left, right, bottom, top, near, far);
        m_ViewerCam.projectionMatrix = m;
    }
    
    static Matrix4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far)
    {
        float x = 2.0F * near / (right - left);
        float y = 2.0F * near / (top - bottom);
        float a = (right + left) / (right - left);
        float b = (top + bottom) / (top - bottom);
        float c = -(far + near) / (far - near);
        float d = -(2.0F * far * near) / (far - near);
        float e = -1.0F;
        Matrix4x4 m = new Matrix4x4();
        m[0, 0] = x;
        m[0, 1] = 0;
        m[0, 2] = a;
        m[0, 3] = 0;
        m[1, 0] = 0;
        m[1, 1] = y;
        m[1, 2] = b;
        m[1, 3] = 0;
        m[2, 0] = 0;
        m[2, 1] = 0;
        m[2, 2] = c;
        m[2, 3] = d;
        m[3, 0] = 0;
        m[3, 1] = 0;
        m[3, 2] = e;
        m[3, 3] = 0;
        return m;
    }
}
