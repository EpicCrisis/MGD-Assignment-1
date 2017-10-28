using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetScript : MonoBehaviour
{

	public float health = 50.0f;

	public int scoreGain = 10;

	PlayerScript player;

	void Start ()
	{
		player = FindObjectOfType<PlayerScript> ();
	}

	public void TakeDamage (float amount)
	{
		health -= amount;
		if (health <= 0.0f) {
			Die ();
		}
	}

	void Die ()
	{
		FindObjectOfType<AudioManager> ().Play ("BloodEffect");

		player.CheckPlayerScore (scoreGain);

		Destroy (gameObject);
	}

}
