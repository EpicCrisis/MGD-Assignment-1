using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{

	public float health = 50.0f;

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

		Destroy (gameObject);
	}

}
