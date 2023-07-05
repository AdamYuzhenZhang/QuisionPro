using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : StateMachineBehaviour
{
    private float m_Timer;
    private float m_MaxTime;
    [SerializeField] private float m_Min = 3f;
    [SerializeField] private float m_Max = 5f;
    [SerializeField] private int m_WalkingPossibility = 3;
    private bool m_NextSet = false;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Timer = 0f;
        m_MaxTime = Random.Range(m_Min, m_Max);
        m_NextSet = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        m_Timer += Time.deltaTime;
        if (m_Timer > m_MaxTime)
        {

            int randomValue = Random.Range(0, m_WalkingPossibility);
            //Debug.Log(randomValue);
            //Debug.Log(randomValue == 0);
            if (randomValue == 0 && !m_NextSet)
            {
                Debug.Log("sitting");
                animator.SetBool("startSitting", true);
                
                m_NextSet = true;
            }
            else if (!m_NextSet)
            {                
                Debug.Log("walking");
                animator.SetBool("isWalking", true);
                m_NextSet = true;
            }
        }
    
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

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
