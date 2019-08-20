using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeetleManager : MonoBehaviour {

	public int currentBeetleCount;
	Text Beetle_Count;
	public GameObject[] beetles;


	void Awake () {

		Beetle_Count = GetComponent<Text> ();
		currentBeetleCount = 0;
	}


	void Update () {

		beetles = GameObject.FindGameObjectsWithTag ("Beetle");
		Beetle_Count.text = beetles.Length.ToString();
	}
}