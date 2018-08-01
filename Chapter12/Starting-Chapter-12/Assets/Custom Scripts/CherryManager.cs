using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CherryManager : MonoBehaviour {

	Text Cherries_Count;

	void Awake () {

		Cherries_Count = GetComponent<Text> ();

	}


	void Update () {
		
		Cherries_Count.text = CucumberManManager.currentCherryCount.ToString ();

	}
}
