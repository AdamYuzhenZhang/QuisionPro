using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    [SerializeField]
    private bool detectHorizontalSwipe = true;
    [SerializeField]
    private bool detectVerticalSwipe = true;

    public enum SwipeDirection
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    public event System.Action<SwipeDirection> OnSwipe;

    void Update()
    {
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDownPosition = touch.position;
                DetectDirection();
            }
        }
    }

    private void DetectDirection()
    {
        if (SwipeDistanceCheckMet())
        {
            if (detectHorizontalSwipe && IsHorizontalSwipe())
            {
                SwipeDirection direction = (fingerDownPosition.x - fingerUpPosition.x > 0) ? SwipeDirection.Right : SwipeDirection.Left;
                TriggerSwipeEvent(direction);
            }
            else if (detectVerticalSwipe && IsVerticalSwipe())
            {
                SwipeDirection direction = (fingerDownPosition.y - fingerUpPosition.y > 0) ? SwipeDirection.Up : SwipeDirection.Down;
                TriggerSwipeEvent(direction);
            }
        }
    }

    private bool IsHorizontalSwipe()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x) > Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    private bool IsVerticalSwipe()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x) < Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > 50 || HorizontalMovementDistance() > 50;
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }

    private void TriggerSwipeEvent(SwipeDirection direction)
    {
        if (OnSwipe != null)
            OnSwipe.Invoke(direction);
    }
}
