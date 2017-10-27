using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
	public float playerHealth = 100f;
	public float tempHealth = 0f;

	public GameObject lookPanel;
	public GameObject movePanel;
	public GameObject restartPanel;

	public Text healthTextUI;

	void Start ()
	{
		gameObject.SetActive (true);

		tempHealth = playerHealth;

		CheckPlayerHealth ();
	}

	void Update ()
	{
		
	}

	public void TakeDamage (float amount)
	{
		tempHealth -= amount;

		CheckPlayerHealth ();

		if (tempHealth <= 0.0f) {
			
			Die ();

		}
	}

	// Show the restart button.
	void Die ()
	{
		FindObjectOfType<AudioManager> ().Play ("BloodEffect");

		AudioManager.instance.Play ("ManDeath");

		GameSettings.instance.PauseGame (true);

		lookPanel.SetActive (false);

		movePanel.SetActive (false);

		restartPanel.SetActive (true);

		//tempHealth = playerHealth;
	}

	public void CheckPlayerHealth ()
	{
		if (tempHealth >= playerHealth) {

			tempHealth = playerHealth;

		}

		healthTextUI.text = "HEALTH: " + tempHealth.ToString ();
	}
}
