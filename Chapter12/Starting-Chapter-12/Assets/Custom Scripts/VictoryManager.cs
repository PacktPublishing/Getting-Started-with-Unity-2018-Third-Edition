using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour {

	Text Victory;
	int beetleCount;
	int cucumberCount = 1;

	void Awake () {

		Victory = GetComponent<Text> ();
		Victory.text = "";
	}

	void Update () {

		beetleCount = BeetleManager.currentBeetleCount;

		if (beetleCount == 0) {
			Victory.text = ("You won!");
		}

		cucumberCount = CucumberManager.currentCucumberCount;

		if (cucumberCount == 0) {
			Victory.text = ("You Lost!");
		}
	}
}
