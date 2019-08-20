using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CucumberManManager : MonoBehaviour {

	public static int currentCherryCount;
	public int livesRemaining;
	public int tempCurrentCherryCount;
	public bool collectingCherries;
	PointsManager _ptsManager;
	public Animator anim;

	public Transform SpawnPad1;
	public Transform SpawnPad2;
	public Transform SpawnPad3;

	void Awake () {

		currentCherryCount = 0;
		tempCurrentCherryCount = 0;
		collectingCherries = false;
		livesRemaining = 3;
	}

	void Update () {

		if (collectingCherries) {
			if (tempCurrentCherryCount >= 60) {
				currentCherryCount = currentCherryCount + 1;
				tempCurrentCherryCount = 0;

				_ptsManager = GameObject.Find ("Score_Value").GetComponent<PointsManager>();
				PointsManager.currentScore = PointsManager.currentScore + 5;

			} else {
				tempCurrentCherryCount = tempCurrentCherryCount + 1;
			}
		}
			
		if (livesRemaining == 2) {
			Destroy (GameObject.Find ("Life3"));
			anim = GetComponent<Animator> ();
			anim.Play ("CM_Die");

			StartCoroutine ("ReSpawnCucumberMan");

		}
		if (livesRemaining == 1) {
			Destroy (GameObject.Find ("Life2"));
			anim = GetComponent<Animator> ();
			anim.Play ("CM_Die");

			StartCoroutine ("ReSpawnCucumberMan");
		}
		if (livesRemaining == 0) {
			Destroy (GameObject.Find ("Life1"));
			anim = GetComponent<Animator> ();
			anim.Play ("CM_Die");
		}
	}

	void OnTriggerEnter(Collider theObject) {
		if (theObject.gameObject.CompareTag ("CherryTree")) {

			collectingCherries = true; 
			currentCherryCount = currentCherryCount + 1;
			}
	}

	void OnTriggerExit(Collider theObject) {
		if (theObject.gameObject.CompareTag ("CherryTree")) {
			collectingCherries = false;
		}
	}

	IEnumerator ReSpawnCucumberMan() {

		int randomNumber = Random.Range (1, 4);

		if (randomNumber == 1) {
			yield return new WaitForSecondsRealtime (4);
			this.transform.position = SpawnPad1.transform.position;
		} else if (randomNumber == 2) {
			yield return new WaitForSecondsRealtime (4);
			this.transform.position = SpawnPad2.transform.position;
		} else {
			yield return new WaitForSecondsRealtime (4);
			this.transform.position = SpawnPad3.transform.position;
		}
			
		anim.Play ("CM_Idle");
	}

}
