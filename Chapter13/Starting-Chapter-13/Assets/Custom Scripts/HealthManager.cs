using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

	public static int currentHealth;
	public Slider healthBar;

	void Awake () {
		healthBar = GetComponent<Slider> ();
		currentHealth = 100;
	}

	public void ReduceHealth () {
		currentHealth = currentHealth - 1;
		healthBar.value = currentHealth;
	}

	void Update () {
		healthBar.value = currentHealth;
	}
}
