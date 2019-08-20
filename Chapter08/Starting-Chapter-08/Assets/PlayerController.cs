using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public string RunState, WalkState, IdleState, JumpState, DieState, ThrowState;
	bool isWalking, isRunning, isJumping, isIdle, isDie, forward,left,right,back;
	Animator mAnim;
	// Use this for initialization
	void Start () {
		mAnim = GetComponent<Animator>();
		isIdle = true;
	}
	
	// Update is called once per frame
	void Update () {
		//Down states
		if( Input.GetKeyDown(KeyCode.W))
		{
			if( !isRunning )
			{
				isWalking = true;
				isIdle = false;
				forward = true;
				mAnim.SetBool(WalkState, true);
				mAnim.SetBool(IdleState, false);
			}

		}
		if( Input.GetKeyDown(KeyCode.A))
		{
			if( !isRunning )
			{
				left = true;
				isWalking = true;
				isIdle = false;
				mAnim.SetBool(WalkState, true);
				mAnim.SetBool(IdleState, false);
			}
		}
		if( Input.GetKeyDown(KeyCode.S))
		{
			if( !isRunning )
			{
				back = true;
				isWalking = true;
				isIdle = false;
				mAnim.SetBool(WalkState, true);
				mAnim.SetBool(IdleState, false);
			}

		}
		if( Input.GetKeyDown(KeyCode.D))
		{
			if( !isRunning )
			{
				right = true;
				isWalking = true;
				isIdle = false;
				mAnim.SetBool(WalkState, true);
				mAnim.SetBool(IdleState, false);
			}
		}
		if( Input.GetKeyDown(KeyCode.LeftShift))
		{
			if( isWalking )
			{
				isRunning = true;
				mAnim.SetBool(RunState, true);
				mAnim.SetBool(WalkState, false);
			}
		}

		if( Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}

		if( Input.GetKeyDown(KeyCode.E))
		{
			Throw();
		}

		//up states

		if( Input.GetKeyUp(KeyCode.W) )
		{			
			forward = false;
			if( !left && !back && !right )
			{
				isWalking = false;
				isRunning = false;
				isIdle = true;
				mAnim.SetBool(WalkState, false);
				mAnim.SetBool(RunState, false);
				mAnim.SetBool(IdleState, true);
			}

		}
		if( Input.GetKeyUp(KeyCode.A))
		{
			left = false;
			if( !forward && !back && !right )
			{
				isWalking = false;
				isRunning = false;
				isIdle = true;
				mAnim.SetBool(WalkState, false);
				mAnim.SetBool(RunState, false);
				mAnim.SetBool(IdleState, true);
			}
		}
		if( Input.GetKeyUp(KeyCode.S))
		{
			back = false;
			if( !left && !forward && !right )
			{
				isWalking = false;
				isRunning = false;
				isIdle = true;
				mAnim.SetBool(WalkState, false);
				mAnim.SetBool(RunState, false);
				mAnim.SetBool(IdleState, true);
			}
		}
		if( Input.GetKeyUp(KeyCode.D))
		{
			right = false;
			if( !left && !back && !forward )
			{
				isWalking = false;
				isRunning = false;
				isIdle = true;
				mAnim.SetBool(WalkState, false);
				mAnim.SetBool(RunState, false);
				mAnim.SetBool(IdleState, true);
			}
		}

		if( Input.GetKeyUp(KeyCode.LeftShift))
		{
			if( isRunning && isWalking )
			{
				isRunning = false;
				mAnim.SetBool(RunState, false);
				mAnim.SetBool(WalkState, true);
			}
		}


	}

	public void Jump()
	{
		mAnim.SetBool(RunState, false);
		mAnim.SetBool(WalkState, false);
		mAnim.SetBool(IdleState, false);
		mAnim.SetBool(JumpState, true);
		StartCoroutine( ConsumeJump());
	}

	public void Throw()
	{
		mAnim.SetBool(RunState, false);
		mAnim.SetBool(WalkState, false);
		mAnim.SetBool(IdleState, false);
		mAnim.SetBool(ThrowState, true);
		StartCoroutine( ConsumeThrow());
	}

	IEnumerator ConsumeJump()
	{
		yield return new WaitForSeconds( 0.66f );
		mAnim.SetBool(JumpState, false);
		if( isRunning )
			mAnim.SetBool(RunState, true);
		else if( isWalking )
			mAnim.SetBool(WalkState, true);
		else if( isIdle )
			mAnim.SetBool(IdleState, true);
			
	}

	IEnumerator ConsumeThrow()
	{
		yield return new WaitForSeconds( 0.66f );
		mAnim.SetBool(ThrowState, false);
		if( isRunning )
			mAnim.SetBool(RunState, true);
		else if( isWalking )
			mAnim.SetBool(WalkState, true);
		else if( isIdle )
			mAnim.SetBool(IdleState, true);

	}
}
