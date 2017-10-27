using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoxScript : MonoBehaviour
{

	public float lifeTime = 12f;
	float lifeTimeCounter = 0f;

	public float healAmount = 20f;

	PlayerScript player;

	void Start ()
	{
		player = FindObjectOfType<PlayerScript> ();
	}

	void Update ()
	{
		CheckLifeTime ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {

			player.tempHealth += healAmount;

			lifeTimeCounter = 0f;

			player.CheckPlayerHealth ();

			AudioManager.instance.Play ("Heal");

			CustomObjectPoolScript.Instance.Despawn (gameObject);

		}
	}

	void CheckLifeTime ()
	{
		lifeTimeCounter += Time.deltaTime;

		if (lifeTimeCounter >= lifeTime) {

			lifeTimeCounter = 0f;

			CustomObjectPoolScript.Instance.Despawn (gameObject);

		}
	}
}
