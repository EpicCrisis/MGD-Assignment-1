using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxScript : MonoBehaviour
{

	public float lifeTime = 12f;
	float lifeTimeCounter = 0f;

	public int ammoAmount = 128;

	TPSLookControllerScript lookScript;

	void Start ()
	{
		lookScript = FindObjectOfType<TPSLookControllerScript> ();
	}

	void Update ()
	{
		CheckLifeTime ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {

			lookScript.totalBullets += ammoAmount;

			lifeTimeCounter = 0f;

			lookScript.CheckPlayerAmmo ();

			AudioManager.instance.Play ("Ammo");

			CustomObjectPoolScript.Instance.Despawn (gameObject);

		}
	}

	void CheckLifeTime ()
	{
		lifeTimeCounter += Time.deltaTime;

		if (lifeTimeCounter >= lifeTime) {

			CustomObjectPoolScript.Instance.Despawn (gameObject);

			lifeTimeCounter = 0f;

		}
	}
}
