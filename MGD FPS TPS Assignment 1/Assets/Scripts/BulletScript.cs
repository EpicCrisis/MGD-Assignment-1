using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

	public float bulletSpeed = 100f;

	public float lifeTime = 10f;
	float lifeTimeCounter = 0f;

	void Start ()
	{
		
	}

	void Update ()
	{
		if (!GameSettings.instance.isPaused) {
			
			BulletMove ();

		}
	}

	void BulletMove ()
	{

		lifeTimeCounter += Time.deltaTime;

		transform.position += transform.up * bulletSpeed;

		//transform.Translate (Vector3.forward * bulletSpeed * Time.deltaTime, Space.Self);

		//rb.AddForce (transform.up * bulletSpeed);

		if (lifeTimeCounter >= lifeTime) {

			lifeTimeCounter = 0f;

			CustomObjectPoolScript.Instance.Despawn (gameObject);

		}
	}

	void OnTriggerStay (Collider other)
	{

		if (other.gameObject.layer == 8) {

			lifeTimeCounter = 0f;

			CustomObjectPoolScript.Instance.Despawn (gameObject);

		}
	}
}
