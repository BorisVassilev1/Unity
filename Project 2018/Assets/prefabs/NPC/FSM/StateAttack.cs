using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : StateMachineBehaviour {

    GameObject projectile;
    GameObject agent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<FSMAgent>().InvokeRepeating("Fire",1.5f,1.5f);
        projectile = animator.gameObject.GetComponent<FSMAgent>().projectile;
        agent = animator.gameObject;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<FSMAgent>().CancelInvoke("Fire");
    }

    public void Fire()
    {
        GameObject go = Instantiate(projectile, agent.transform.position, Quaternion.identity);
        go.GetComponent<Rigidbody>().AddForce(agent.transform.forward * 500);
    }
}
