using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{

	public float timeToExplode = 3.0f;
	float explodeTimer = 0.0f;

	public GameObject explodeEffects;

	public float power = 10.0f;
	public float radius = 100.0f;
	public float upForce = 1.0f;
	public float explodeDamage = 25.0f;

	void Awake ()
	{
		
	}

	void Start ()
	{
		
	}

	void Update ()
	{
		if (!GameSettings.instance.isPaused) {

			CheckExplode ();

		}
	}

	void CheckExplode ()
	{
		explodeTimer += Time.deltaTime;

		if (explodeTimer >= timeToExplode) {

			explodeTimer = 0.0f;

			FindObjectOfType<AudioManager> ().Play ("Explosion");

			Explode ();

		}
	}

	void Explode ()
	{
		GameObject explodeGO = Instantiate (explodeEffects, transform.position, Quaternion.identity);

		Vector3 explosionPosition = gameObject.transform.position;

		Collider[] colliders = Physics.OverlapSphere (explosionPosition, radius);

		foreach (Collider hit in colliders) {
			
			Rigidbody rb = hit.GetComponent<Rigidbody> ();

			TargetScript target = hit.GetComponent<TargetScript> ();

			RayZombieScript rayZombie = hit.GetComponent<RayZombieScript> ();

			if (rb != null) {
				rb.AddExplosionForce (power, explosionPosition, radius, upForce, ForceMode.Impulse);
			}

			if (target != null) {
				target.TakeDamage (explodeDamage);
			}

			if (rayZombie != null) {
				rayZombie.TakeDamage (explodeDamage);
			}
		}

		Destroy (gameObject);

		Destroy (explodeGO, 2.0f);

	}
}
