using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

	RaycastHit hit;
	public Camera fpsCam;

	public float damage = 10.0f;
	public int range = 100;

	public float impactForce = 100.0f;

	public float shootDelay = 1.0f;
	float shootTimer = 0.0f;

	public ParticleSystem muzzle;
	public GameObject bloodEffect;

	void Start ()
	{
		
	}

	void Update ()
	{
		CheckEnemyTarget ();
	}

	void CheckEnemyTarget ()
	{
		if (Physics.Raycast (fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) {
			
//			Debug.Log (hit.transform.name);

			TargetScript target = hit.transform.GetComponent<TargetScript> (); // Will store info when object with this script is hit.//

			if (target != null) {

				shootTimer += Time.deltaTime;

				if (shootTimer >= shootDelay) {

					shootTimer = 0.0f;

					if (hit.rigidbody != null) {
						hit.rigidbody.AddForce (-hit.normal * impactForce);
					}

					GameObject bloodGO = Instantiate (bloodEffect, hit.point, Quaternion.LookRotation (hit.normal));

					muzzle.Play ();

					FindObjectOfType<AudioManager> ().Play ("Gunshot");

					target.TakeDamage (damage);

					Destroy (bloodGO, 2.0f);
				}
			} else {
				
				shootTimer = 0.0f;

			}

		} else {
			
//			Debug.LogWarning ("Nothing");
			return;
		}

//		if (hit.transform.gameObject.tag == "Enemy") {
//			
//			Debug.Log ("Hit The Enemy");
//
//		} else {
//			
//			Debug.LogWarning ("No Enemy Detected");
//			return;
//		}
	}
}
