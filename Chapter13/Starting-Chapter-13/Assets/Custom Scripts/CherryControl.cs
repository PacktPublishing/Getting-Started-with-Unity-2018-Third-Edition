using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryControl : MonoBehaviour {

	public Rigidbody cherry;
	public float throwDistance = 2000000000f;
	public float time2Die = 4.0f;

	GameObject cucumberHand;

	void Update () {

		int count = CucumberManManager.currentCherryCount;

		if (Input.GetKeyDown (KeyCode.E)) {
			
			if (count >= 1) {

				ThrowACherry ();
			}

		}
		
	}

	public void ThrowACherry () {


		Rigidbody cherryClone = (Rigidbody)Instantiate (cherry, transform.position, transform.rotation);
		cherryClone.gameObject.SetActive (true);
		cherryClone.useGravity = true;
		cherryClone.constraints = RigidbodyConstraints.None;
		cherryClone.AddForce(transform.forward * throwDistance);
		Destroy (cherryClone.gameObject, time2Die);

		CucumberManManager.currentCherryCount = CucumberManManager.currentCherryCount - 1;
	
	}


	public void ReleaseCherry() {

	}
}
