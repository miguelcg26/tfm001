﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump2_ABS : StateMachineBehaviour {

	Rigidbody rb;
	public float impulse;
	public float jumpForce;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		rb = GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ();
		rb.AddForce (-rb.transform.up * (impulse * 1000));
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//rb = GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ();
	//rb.AddForce (-rb.transform.up * impulse);
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		rb = GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ();
		rb.AddForce (rb.transform.up * jumpForce + rb.transform.forward * jumpForce, ForceMode.Impulse);
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}