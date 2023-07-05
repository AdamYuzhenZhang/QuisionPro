using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBehavior : StateMachineBehaviour
{
    private List<Transform> wayPoints = new List<Transform>();

    private CatMover m_CatMover;

    private bool m_Sit;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform wayPoint = GameObject.FindGameObjectWithTag("Waypoint").transform;
        foreach (Transform t in wayPoint)
        {
            wayPoints.Add(t);
        }

        m_CatMover = animator.GetComponent<CatMover>();
        //Debug.Log("Walking started");
        //Debug.Log("Target is");
        //Debug.Log(wayPoints[Random.Range(0, wayPoints.Count)].localPosition);
        bool result = false;
        int rand = Random.Range(0, wayPoints.Count);
        Transform randomWayPoint = wayPoints[rand];
        /*
        while (!result)
        {
            randomWayPoint = wayPoints[Random.Range(0, wayPoints.Count)];
            result = randomWayPoint.localPosition == animator.transform.localPosition;
        }
        */
        if (randomWayPoint.localPosition == animator.transform.localPosition)
        {
            if (rand > 0)
            {
                m_CatMover.MoveTo(wayPoints[rand - 1]);
            }
            else
            {
                m_CatMover.MoveTo(wayPoints[rand + 1]);
            }
        }
        else
        {
            m_CatMover.MoveTo(randomWayPoint);
        }
        
        //Transform randomWayPoint = wayPoints[Random.Range(0, wayPoints.Count)];
        //if (randomWayPoint.localPosition == animator.transform.localPosition)
        //{
        //    // play other animation instead
        //    animator.SetBool("startSitting", true);
        //    m_Sit = true;

        //}
        //else
        //{
        //    m_CatMover.MoveTo(randomWayPoint);
        //    m_Sit = false;
        //}
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_CatMover.Arrived())
        {
            animator.SetBool("isWalking", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
