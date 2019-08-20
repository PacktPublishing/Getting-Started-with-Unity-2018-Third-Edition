using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CucumberManager : MonoBehaviour {

	public static int currentCucumberCount;
	Text Cucumber_Count;
	public GameObject[] cucumbers;

	void Awake () {

		Cucumber_Count = GetComponent<Text> ();
		currentCucumberCount = 1;
	}
	

	void Update () {

		cucumbers = GameObject.FindGameObjectsWithTag ("Cucumber");
		Cucumber_Count.text = cucumbers.Length.ToString();
		currentCucumberCount = cucumbers.Length;
	}
}
