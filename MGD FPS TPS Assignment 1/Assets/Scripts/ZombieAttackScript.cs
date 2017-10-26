using UnityEngine;
using System.Collections;

public class ZombieAttackScript : MonoBehaviour
{

	public static ZombieAttackScript instance;

	public float attackSpeed = 2f;
	float attackSpeedCounter = 0f;

	public float attackDamage = 10f;

	PlayerScript player;

	public GameObject bloodEffect;

	public bool isAttacking = false;

	void Start ()
	{
		player = FindObjectOfType<PlayerScript> ();
	}

	void Update ()
	{

	}

	void AttackPlayer ()
	{
		if (!GameSettings.instance.isPaused) {
			
			attackSpeedCounter += Time.deltaTime;

			if (attackSpeedCounter >= attackSpeed) {

				AudioManager.instance.Play ("Hit1");

				GameObject bloodGO = Instantiate (bloodEffect, player.gameObject.transform.position, player.gameObject.transform.rotation);

				//player.TakeDamage (attackDamage);

				attackSpeedCounter = 0f;

				Destroy (bloodGO, 2.0f);

			}
		}
	}

	void OnTriggerStay (Collider other)
	{
		if (other.gameObject.tag == "Player") {

			AttackPlayer ();

			isAttacking = true;

		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "Player") {

			isAttacking = false;

		}
	}
}

