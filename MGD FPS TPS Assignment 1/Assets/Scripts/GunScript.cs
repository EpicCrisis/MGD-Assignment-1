using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{

	RaycastHit hit;
	public Camera fpsCam;

	public float damage = 10.0f;
	public int range = 100;

	public float impactForce = 100.0f;

	public float shootDelay = 1.0f;
	float shootTimer = 0.0f;

	bool shellFall = false;

	public float shellDelay = 1.0f;
	float shellTimer = 0.0f;

	public ParticleSystem muzzle;
	public GameObject bloodEffect;

	public int totalBullets = 32;
	public int clipBullets = 8;
	public int currentBullets = 8;
	public int usedBullets = 1;

	bool toReload = false;

	public float reloadTime = 1.5f;
	float reloadCounter = 0.0f;

	bool doOnce = false;

	public Text ammoTextUI;

	void Start ()
	{
		CheckPlayerAmmo ();
	}

	void Update ()
	{
		if (!GameSettings.instance.isPaused) {

			CheckEnemyTarget ();

			CheckShell ();

			CheckReload ();

		}
	}

	void CheckEnemyTarget ()
	{
		if (Physics.Raycast (fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) {
			
//			Debug.Log (hit.transform.name);

			if (hit.transform.tag == "Target") {
				
				TargetScript target = hit.transform.GetComponent<TargetScript> (); // Will store info when object with this script is hit.//

				if (target != null && !toReload) {

					shootTimer += Time.deltaTime;

					if (shootTimer >= shootDelay) {

						shootTimer = 0.0f;

						if (hit.rigidbody != null) {
							hit.rigidbody.AddForce (-hit.normal * impactForce);
						}

						GameObject bloodGO = Instantiate (bloodEffect, hit.point, Quaternion.LookRotation (hit.normal));

						muzzle.Play ();

						FindObjectOfType<AudioManager> ().Play ("Gunshot");

						shellFall = true;

						target.TakeDamage (damage);

						Destroy (bloodGO, 2.0f);

						currentBullets -= usedBullets;

						CheckPlayerAmmo ();
					}
				} else {

					shootTimer = 0.0f;

				}

			} else if (hit.transform.tag == "Zombie") {
				
				RayZombieScript target = hit.transform.GetComponent<RayZombieScript> ();

				if (target != null && !toReload) {

					shootTimer += Time.deltaTime;

					if (shootTimer >= shootDelay) {

						shootTimer = 0.0f;

						if (hit.rigidbody != null) {
							hit.rigidbody.AddForce (-hit.normal * impactForce);
						}

						GameObject bloodGO = Instantiate (bloodEffect, hit.point, Quaternion.LookRotation (hit.normal));

						muzzle.Play ();

						FindObjectOfType<AudioManager> ().Play ("Gunshot");

						shellFall = true;

						target.TakeDamage (damage);

						Destroy (bloodGO, 2.0f);

						currentBullets -= usedBullets;

						CheckPlayerAmmo ();
					}
				} else {

					shootTimer = 0.0f;

				}
			}
		} else {
			
			//Debug.LogWarning ("Nothing");
			return;
		}

		/*
		if (hit.transform.gameObject.tag == "Enemy") {
			
			Debug.Log ("Hit The Enemy");

		} else {
			
			Debug.LogWarning ("No Enemy Detected");
			return;
		}
		*/

	}

	void CheckShell ()
	{
		if (shellFall) {
			
			shellTimer += Time.deltaTime;

			if (shellTimer >= shellDelay) {

				shellTimer = 0f;

				shellFall = false;

				AudioManager.instance.Play ("Shell");

			}
		}
	}

	void CheckReload ()
	{
		if (currentBullets <= 0) {

			toReload = true;

			if (totalBullets >= 1) {

				reloadCounter += Time.deltaTime;

				if (!doOnce) {

					AudioManager.instance.Play ("Reload2");

					doOnce = true;

				}

			}

		}

		if (reloadCounter >= reloadTime) {

			toReload = false;

			reloadCounter = 0f;

			if (totalBullets >= clipBullets) {

				currentBullets += clipBullets;

				totalBullets -= clipBullets;

			} else {

				currentBullets += totalBullets;

				totalBullets -= totalBullets;
			}

			doOnce = false;

		}
	}

	public void CheckPlayerAmmo ()
	{
		ammoTextUI.text = "AMMO: " + currentBullets + "/" + totalBullets;
	}
}
