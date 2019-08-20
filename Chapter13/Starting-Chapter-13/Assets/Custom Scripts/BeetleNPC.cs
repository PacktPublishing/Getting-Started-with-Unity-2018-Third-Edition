using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleNPC : MonoBehaviour {

    Animator animator;
	public GameObject cucumberToDestroy;
	public bool cherryHit = false;
	public float smoothTime = 3.0f; 
	public Vector3 smoothVelocity = Vector3.zero;
	public PointsManager _ptsManager;
	public HealthManager _healthManager;

	public AudioSource audioSource;
	public AudioClip eating;
	public AudioClip attack;
	public AudioClip die;

	void Start () {
        animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource> ();
	}

	void Update () {
		if (cherryHit) {

			var cm = GameObject.Find ("CucumberMan");
			var tf = cm.transform;
			this.gameObject.transform.LookAt (tf);

			// move towards Cucumber Man
			animator.Play ("Standing Run");

			transform.position = Vector3.SmoothDamp (transform.position, tf.position, 
				ref smoothVelocity, smoothTime);
		}
	}

    // Collision Detection Test
    void OnCollisionEnter(Collision col) 
    {
		if (col.gameObject.CompareTag ("Player")) {

			_healthManager = GameObject.Find ("Health_Slider").GetComponent<HealthManager> ();
			_healthManager.ReduceHealth();

			if (!cherryHit) {

				BeetlePatrol.isAttacking = true;

				var cm = GameObject.Find ("CucumberMan");
				var tf = cm.transform;
				this.gameObject.transform.LookAt (tf);

				animator.Play ("Attacking on Ground");
				StartCoroutine ("DestroySelfOnGround");
			} else {
				animator.Play ("Standing Attack");
				StartCoroutine ("DestroySelfStanding");
			}
		}
    }

	// Detect Collision with Cucumber
	void OnTriggerEnter(Collider theObject) {
		if (theObject.gameObject.CompareTag ("Cucumber")) {
			cucumberToDestroy = theObject.gameObject;
			BeetlePatrol.isEating = true;
			animator.Play ("Eating on Ground");
			audioSource.PlayOneShot (eating);
			StartCoroutine ("DestroyCucumber");
		} else if (theObject.gameObject.CompareTag ("Cherry")) {
			_ptsManager = GameObject.Find ("Score_Value").GetComponent<PointsManager> ();
			PointsManager.currentScore = PointsManager.currentScore + 10; 
			BeetlePatrol.isAttacking = true;
			cherryHit = true;
			animator.Play ("Stand");
			audioSource.PlayOneShot (attack);
		}
	}


	IEnumerator DestroyCucumber() {
		
		yield return new WaitForSecondsRealtime (4);
		Destroy (cucumberToDestroy.gameObject);
		BeetlePatrol.isEating = false;
	}
		
	IEnumerator DestroySelfOnGround() {

		yield return new WaitForSecondsRealtime (4);
		animator.Play ("Die on Ground");
		audioSource.PlayOneShot (die);
		Destroy (this.gameObject, 4);
	}

	IEnumerator DestroySelfStanding() {

		yield return new WaitForSecondsRealtime (4);
		animator.Play ("Die Standing");
		audioSource.PlayOneShot (die);
		Destroy (this.gameObject, 4);
		cherryHit = false;
	}

}
