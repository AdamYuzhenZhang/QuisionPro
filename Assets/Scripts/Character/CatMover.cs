using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMover : MonoBehaviour
{
    private Vector3 m_TargetLocalPos;
    private Vector3 m_StartLocalPos;
    [SerializeField] private float speed = 1.0f;
    private float distance;
    private float startTime;
    private bool isMoving = false;
    
    private Quaternion startRotation;
    [SerializeField] float rotationSpeed = 1000f;

    private void Start()
    {
        startRotation = transform.localRotation;
    }

    void Update()
    {
        if (isMoving)
        {
            float journeyTime = distance / speed;
            float elapsedTime = Time.time - startTime;
            float t = elapsedTime / journeyTime;
            
            // Rotate towards the target
            Vector3 direction = m_TargetLocalPos - m_StartLocalPos;
            Quaternion targetRotation = Quaternion.LookRotation(direction, transform.up);
            Debug.Log(targetRotation.eulerAngles);
            transform.localRotation = Quaternion.Euler(0f,
                Quaternion.Slerp(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime).eulerAngles.y,
                0f);
            
            if (t < 1.0f)
            {
                transform.localPosition = Vector3.Lerp(m_StartLocalPos, m_TargetLocalPos, t);
            }
            else
            {
                transform.localPosition = m_TargetLocalPos;
                //transform.localRotation = startRotation;
                isMoving = false;
            }
        }
        else
        {
            if (Quaternion.Angle(transform.localRotation, startRotation) > 0f)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation, startRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    public void MoveTo(Transform target)
    {
        isMoving = true;
        startTime = Time.time;
        m_StartLocalPos = gameObject.transform.localPosition;
        m_TargetLocalPos = target.localPosition;
        distance = Vector3.Distance(m_StartLocalPos, m_TargetLocalPos);
        
        //Vector3 direction = m_TargetLocalPos - m_StartLocalPos;
        //Quaternion targetRotation = Quaternion.LookRotation(direction, transform.up);
        //transform.localRotation = targetRotation;

    }

    public bool Arrived()
    {
        return !isMoving;
    }
}
