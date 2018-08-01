using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

	public static CameraFollower instance;

	public float movementSmoothness = 1f;
	public float rotationSmoothness = 1f;

	public GameObject FollowTarget;

	public bool canFollow = true;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if( FollowTarget == null || !canFollow )
			return;

		transform.position = Vector3.Lerp( transform.position, FollowTarget.transform.position, Time.deltaTime *movementSmoothness );
		transform.rotation = Quaternion.Slerp( transform.rotation, FollowTarget.transform.rotation, Time.deltaTime * rotationSmoothness );
		
	}
}
