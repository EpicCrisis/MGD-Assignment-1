using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

	public float bulletSpeed = 100f;

	public float lifeTime = 10f;
	float lifeTimeCounter = 0f;

	public float bulletDamage = 10f;

	public GameObject bloodEffect;

	Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
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

		//transform.position += transform.up * bulletSpeed;

		rb.velocity = transform.up * bulletSpeed;

		//transform.Translate (Vector3.forward * bulletSpeed * Time.deltaTime, Space.Self);

		//rb.AddForce (transform.up * bulletSpeed);

		if (lifeTimeCounter >= lifeTime) {

			lifeTimeCounter = 0f;

			CustomObjectPoolScript.Instance.Despawn (gameObject);

		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Zombie") {

			AudioManager.instance.Play ("Hit2");

			other.GetComponent<ZombieScript> ().TakeDamage (bulletDamage);

			lifeTimeCounter = 0f;

			CustomObjectPoolScript.Instance.Despawn (gameObject);

			GameObject bloodGO = Instantiate (bloodEffect, other.gameObject.transform.position, other.gameObject.transform.rotation);

			Destroy (bloodGO, 2.0f);

		}

		if (other.gameObject.layer == 8) {

			lifeTimeCounter = 0f;

			CustomObjectPoolScript.Instance.Despawn (gameObject);

		}
	}
}
