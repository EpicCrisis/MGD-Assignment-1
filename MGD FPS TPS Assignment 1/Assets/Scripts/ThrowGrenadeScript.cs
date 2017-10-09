using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThrowGrenadeScript : MonoBehaviour, IPointerDownHandler
{

	public Transform pointOfThrow;
	public GameObject grenadeToSpawn;

	public float throwForce = 100.0f;

	public bool onCooldown = false;

	public float throwDelay = 3.0f;
	float throwTimer = 0.0f;

	void Awake ()
	{
		
	}

	void Start ()
	{
	}


	void Update ()
	{
		ActivateGrenadeCooldown ();
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		if (!onCooldown) {
			
			Debug.Log ("Throw Grenade");
			GameObject grenadeGO = Instantiate (grenadeToSpawn, pointOfThrow.transform.position, pointOfThrow.rotation);

			grenadeGO.GetComponent<Rigidbody> ().AddForce (pointOfThrow.forward * throwForce);

			onCooldown = true;

		} else {
			return;
		}
	}

	void ActivateGrenadeCooldown ()
	{
		if (onCooldown) {

			throwTimer += Time.deltaTime;

			if (throwTimer >= throwDelay) {

				throwTimer = 0.0f;

				onCooldown = false;
			}
		}
	}
}
