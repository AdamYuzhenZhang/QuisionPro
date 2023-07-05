using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiauBehavior : StateMachineBehaviour
{
    private float m_Timer;
    private float m_MaxTime;
    [SerializeField] private float m_Min = 1f;
    [SerializeField] private float m_Max = 3f;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("miauAgain", false);
        m_Timer = 0f;
        m_MaxTime = Random.Range(m_Min, m_Max);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Timer += Time.deltaTime;
        if (m_Timer > m_MaxTime)
        {
            int randomValue = Random.Range(0, 5);
            if (randomValue == 0) animator.SetBool("standUp", true);
            else animator.SetBool("miauAgain", true);
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
