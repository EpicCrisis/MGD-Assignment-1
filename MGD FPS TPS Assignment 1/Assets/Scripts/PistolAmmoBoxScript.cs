using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAmmoBoxScript : MonoBehaviour
{

	public float lifeTime = 12f;
	float lifeTimeCounter = 0f;

	public int ammoAmount = 16;

	GunScript gunScript;

	void Start ()
	{
		gunScript = FindObjectOfType<GunScript> ();
	}

	void Update ()
	{
		CheckLifeTime ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {

			gunScript.totalBullets += ammoAmount;

			lifeTimeCounter = 0f;

			gunScript.CheckPlayerAmmo ();

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
