using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetlePatrol : MonoBehaviour {

	// Variables
	public static bool isDie, isEating, isAttacking = false;

	public float speed = 5;
	public float directionChangeInterval = 1;
	public float maxHeadingChange = 30;

	Animator beetleAnim;

	CharacterController controller;
	float heading;
	Vector3 targetRotation;

	// Use this for initialization
	void Start () {

		controller = GetComponent<CharacterController>();
		beetleAnim = GetComponent<Animator> ();

		// Set random initial rotation
		heading = Random.Range(0, 360);
		transform.eulerAngles = new Vector3(0, heading, 0);

		StartCoroutine(NewHeading());

	}

	// Update is called once per frame
	void Update () {

		if ((!isDie) & (!isEating) & (!isAttacking)) {
			transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
			var forward = transform.TransformDirection (Vector3.forward);
			controller.SimpleMove (forward * speed);
		}
	}
		
	IEnumerator NewHeading ()
	{
		while (true) {
			NewHeadingRoutine();
			yield return new WaitForSeconds(directionChangeInterval);
		}
	}
		
	void NewHeadingRoutine ()
	{
		var floor = transform.eulerAngles.y - maxHeadingChange;
		var ceil  = transform.eulerAngles.y + maxHeadingChange;
		heading = Random.Range(floor, ceil);
		targetRotation = new Vector3(0, heading, 0);
	}
}
