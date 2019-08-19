using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour {

	Text Victory;
	int beetleCount;
	int cucumberCount = 1;	
	public AudioSource audioSource;
	public AudioClip victory;

	void Awake () {

		Victory = GetComponent<Text> ();
		Victory.text = "";
	}

	void Start () {
		audioSource = GetComponent<AudioSource> ();
	} 

	void Update () {

		beetleCount = BeetleManager.currentBeetleCount;

		if (beetleCount == 0) {
			Victory.text = ("You won!");
			audioSource.PlayOneShot (victory);
		}

		cucumberCount = CucumberManager.currentCucumberCount;

		if (cucumberCount == 0) {
			Victory.text = ("You Lost!");
		}
	}
}
