using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
	//float moveSpeed = 1f;

	public float zombieHealth = 50f;
	float tempHealth = 0f;

	PlayerScript player;

	ZombieAttackScript zombieAttack;

	NavMeshAgent navMeshAgent;

	void Start ()
	{
		player = FindObjectOfType<PlayerScript> ();

		zombieAttack = GetComponentInChildren<ZombieAttackScript> ();

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

			if (!zombieAttack.isAttacking) {

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
}
