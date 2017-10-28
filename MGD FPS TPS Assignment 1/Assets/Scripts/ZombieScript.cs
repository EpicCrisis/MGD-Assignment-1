using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieScript : MonoBehaviour
{
	//float moveSpeed = 1f;

	public float zombieHealth = 50f;
	float tempHealth = 0f;

	public int scoreGain = 10;

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

		player.CheckPlayerScore (scoreGain);

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
