using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

	float horizontal, vertical;
	Rigidbody m_Rigidbody;

	public float JumpPower;
	public float MoveSpeed, RunSpeed;

	private float currentJumpPower = 0;
	private float currentMoveSpeed = 0;
	// Use this for initialization
	void Start () {
		m_Rigidbody = GetComponent<Rigidbody>();
		currentMoveSpeed = MoveSpeed;
	//	m_Cam = Camera.main.transform;
		m_Animator = GetComponent<Animator>();
	}

	void Update()
	{
		
		CheckGroundStatus();
		horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
		vertical = CrossPlatformInputManager.GetAxis("Vertical");
//		Debug.Log( horizontal + " " + vertical);
		if( Input.GetKeyDown(KeyCode.Space) && m_IsGrounded )
		{
			m_Rigidbody.AddForce(0,JumpPower,0);
		}

		if( Input.GetKeyDown(KeyCode.LeftShift) && m_IsGrounded )
		{
			currentMoveSpeed = RunSpeed;
		}

		if( Input.GetKeyUp(KeyCode.LeftShift))
		{
			currentMoveSpeed = MoveSpeed;
		}
	}
	float m_TurnAmount;
	float m_ForwardAmount;
	[SerializeField] float m_StationaryTurnSpeed = 180;
	[SerializeField] float m_MovingTurnSpeed = 360;

	public Transform m_Cam;                  // A reference to the main camera in the scenes transform
	private Vector3 m_CamForward;  
	private Vector3 m_Move;
	private bool m_Jump;   
	// Update is called once per frame
	void FixedUpdate () {
		
		if (m_Cam != null)
		{
			// calculate camera relative direction to move:
			m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
			m_Move = vertical*m_CamForward + horizontal*m_Cam.right;
		}
		else
		{
			// we use world-relative directions in the case of no main camera
			m_Move = vertical*Vector3.forward + horizontal*Vector3.right;
		}
		if( m_Move.magnitude > 0 )
			Move(m_Move);
//		m_Rigidbody.velocity = new Vector3( horizontal,currentJumpPower,vertical) * currentMoveSpeed;
//		m_TurnAmount = Mathf.Atan2(horizontal, vertical);
//		m_ForwardAmount = vertical;
//		ApplyExtraTurnRotation();
	}
	void ApplyExtraTurnRotation()
	{
		// help the character turn faster (this is in addition to root rotation in the animation)
		float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
		transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
	}
	[SerializeField] float m_GroundCheckDistance = 0.1f;
	bool m_IsGrounded;
	Vector3 m_GroundNormal;

	void CheckGroundStatus()
	{
		RaycastHit hitInfo;
		#if UNITY_EDITOR
		// helper to visualise the ground check ray in the scene view
		Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
		#endif
		// 0.1f is a small offset to start the ray from inside the character
		// it is also good to note that the transform position in the sample assets is at the base of the character
		if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
		{
			m_GroundNormal = hitInfo.normal;
			m_IsGrounded = true;

		}
		else
		{
			m_IsGrounded = false;
			m_GroundNormal = Vector3.up;

		}
	}

	public void Move(Vector3 move)
	{
		if (move.magnitude > 1f) move.Normalize();
		move = transform.InverseTransformDirection(move);
		CheckGroundStatus();
		move = Vector3.ProjectOnPlane(move, m_GroundNormal);
		m_TurnAmount = Mathf.Atan2(move.x, move.z);
		m_ForwardAmount = move.z;
		m_Rigidbody.velocity = transform.forward * currentMoveSpeed;
			ApplyExtraTurnRotation();
	}
	Animator m_Animator;
	[SerializeField] float m_MoveSpeedMultiplier = 1f;
	public void OnAnimatorMove()
	{
		// we implement this function to override the default root motion.
		// this allows us to modify the positional speed before it's applied.
		if (m_IsGrounded && Time.deltaTime > 0)
		{
			Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

			// we preserve the existing y part of the current velocity.
			v.y = m_Rigidbody.velocity.y;
			m_Rigidbody.velocity = v;
		}
	}
}
