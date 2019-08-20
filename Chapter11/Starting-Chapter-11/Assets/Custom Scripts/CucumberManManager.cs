using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CucumberManManager : MonoBehaviour {

	public static int currentCherryCount;
	public int tempCurrentCherryCount;
	public bool collectingCherries;
	PointsManager _ptsManager;


	void Awake () {

		currentCherryCount = 0;
		tempCurrentCherryCount = 0;
		collectingCherries = false;

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

}
