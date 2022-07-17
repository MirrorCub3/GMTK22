using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Roll2 : StateMachineBehaviour
{
    Transform boss;
    BossScript bossScript;
    Transform target;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.gameObject.transform;
        bossScript = animator.gameObject.GetComponent<BossScript>();
        target = bossScript.rollTarget;
        target.position = GameObject.FindGameObjectWithTag("Player").transform.position; // targets the position of the player when the state was entered
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.position = Vector2.MoveTowards(boss.position, target.position, bossScript.rollSpeed2 * bossScript.speedMultiplier * Time.deltaTime);
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
