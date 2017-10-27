using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RayZombieScript : MonoBehaviour
{
	//float moveSpeed = 1f;

	public float zombieHealth = 50f;
	float tempHealth = 0f;

	PlayerScript player;

	NavMeshAgent navMeshAgent;

	RaycastHit hit;

	public GameObject bloodEffect;

	public float rayDamage = 10f;
	public float rayRange = 2f;
	public float rayAttackSpeed = 2f;

	float rayAttackSpeedCounter = 0f;

	public bool isAttacking = false;

	void Start ()
	{
		player = FindObjectOfType<PlayerScript> ();

		tempHealth = zombieHealth;

		navMeshAgent = this.GetComponent<NavMeshAgent> ();

		//StartCoroutine (RepathRoutine ());
	}

	void Update ()
	{
		if (!GameSettings.instance.isPaused) {

			//MoveTowardsPlayer ();

			player = FindObjectOfType<PlayerScript> ();

			LookAtPlayer ();

			RayCheckPlayer ();

		}
	}

	void LookAtPlayer ()
	{
		if (player != null) {

			Vector3 targetPosition = new Vector3 (player.transform.position.x, transform.position.y, player.transform.position.z);

			transform.LookAt (targetPosition);

			navMeshAgent.SetDestination (targetPosition);

		}
	}

	void MoveTowardsPlayer ()
	{
		if (player != null) {

			Vector3 targetPosition = new Vector3 (player.transform.position.x, transform.position.y, player.transform.position.z);

			transform.LookAt (targetPosition);

			if (!isAttacking) {

				//transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);

			} else {

				transform.Translate (Vector3.zero);

			}
		}
	}

	public void TakeDamage (float amount)
	{
		tempHealth -= amount;

		if (tempHealth <= 0.0f) {

			Die ();

		}
	}

	void Die ()
	{
		FindObjectOfType<AudioManager> ().Play ("BloodEffect");

		AudioManager.instance.Play ("ZombieDeath");

		CustomObjectPoolScript.Instance.Despawn (gameObject);

		tempHealth = zombieHealth;
	}

	IEnumerator RepathRoutine ()
	{
		if (!GameSettings.instance.isPaused) {

			while (true) {

				Vector3 targetPosition = new Vector3 (player.transform.position.x, transform.position.y, player.transform.position.z);

				yield return new WaitForSeconds (0.5f);

				navMeshAgent.SetDestination (targetPosition);

			}
		}
	}

	public void RayCheckPlayer ()
	{
		if (Physics.Raycast (gameObject.transform.position, gameObject.transform.forward, out hit, rayRange)) {

			if (hit.transform.tag == "Player") {

				PlayerScript target = hit.transform.GetComponent<PlayerScript> ();

				if (target != null) {

					rayAttackSpeedCounter += Time.deltaTime;

					if (rayAttackSpeedCounter >= rayAttackSpeed) {

						rayAttackSpeedCounter = 0.0f;

						FindObjectOfType<AudioManager> ().Play ("Hit1");

						GameObject bloodGO = Instantiate (bloodEffect, hit.point, Quaternion.LookRotation (hit.normal));

						target.TakeDamage (rayDamage);

						Destroy (bloodGO, 2.0f);
					}
				} else {

					rayAttackSpeedCounter = 0.0f;

				}

			}
		}
	}
}
