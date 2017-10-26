using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	public float playerHealth = 100f;
	float tempHealth = 0f;

	void Start ()
	{
		gameObject.SetActive (true);

		tempHealth = playerHealth;
	}

	void Update ()
	{
		
	}

	public void TakeDamage (float amount)
	{
		tempHealth -= amount;

		if (tempHealth <= 0.0f) {
			
			Die ();

		}
	}

	// Show the restart button.
	void Die ()
	{
		FindObjectOfType<AudioManager> ().Play ("BloodEffect");

		tempHealth = playerHealth;
	}
}
