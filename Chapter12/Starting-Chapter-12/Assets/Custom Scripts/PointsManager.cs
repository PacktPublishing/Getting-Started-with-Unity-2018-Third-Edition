using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour {

	public static int currentScore;
	Text score;

	void Awake () {
		score = GetComponent<Text> ();
		currentScore = 0;
		
	}
	
	void Update () {

		score.text = currentScore.ToString ();
		
	}
}
