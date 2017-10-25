using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TPSLookControllerScript : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
	public Transform character;
	Vector3 initialPos;
	Vector3 direction;

	public float rotSpeed = 3f;
	bool isDragging = false;

	public Transform bulletHole1;
	public Transform bulletHole2;

	bool toShoot = false;

	public float shootRate = 1f;
	float shootCounter = 0f;

	public ParticleSystem muzzle1;
	public ParticleSystem muzzle2;

	public int totalBullets = 36;
	public int currentBullets = 36;

	bool toReload = false;

	public float reloadTime = 2.0f;
	float reloadCounter = 0.0f;

	bool doOnce = false;

	void Start ()
	{
		initialPos = this.transform.position; //set initial position
	}


	void Update ()
	{
		if (!GameSettings.instance.isPaused) {
			
			CheckInput ();

			CheckShoot ();

			CheckReload ();
		}
	}


	void LateUpdate ()
	{

	}

	void CheckInput ()
	{

		if (isDragging) {
			
			direction = this.transform.position - initialPos;

		}
		direction.Normalize ();

		float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg - 90f;

		character.rotation = Quaternion.Lerp (character.rotation, Quaternion.Euler (0f, -angle, 0f), Time.deltaTime * rotSpeed);
	}

	public void OnDrag (PointerEventData eventData)
	{
		this.transform.position = eventData.position; //make knob follow drag
		isDragging = true;
	}


	public void OnEndDrag (PointerEventData eventData)
	{
		this.transform.position = initialPos; //reset position of knob
		isDragging = false;
	}

	public void CheckShoot ()
	{
		if (toShoot && !toReload) {

			shootCounter += Time.deltaTime;

			if (shootCounter >= shootRate) {

				muzzle1.Play ();

				muzzle2.Play ();

				AudioManager.instance.Play ("MP5");

				CustomObjectPoolScript.Instance.Spawn ("Bullet", bulletHole1.position, bulletHole1.rotation);

				CustomObjectPoolScript.Instance.Spawn ("Bullet", bulletHole2.position, bulletHole2.rotation);

				shootCounter = 0f;

				currentBullets -= 1;

			}
		}
	}

	public void CheckReload ()
	{
		if (currentBullets <= 0) {

			toReload = true;

			reloadCounter += Time.deltaTime;

			if (!doOnce) {
				
				AudioManager.instance.Play ("Reload1");

				doOnce = true;

			}

		}
		if (reloadCounter >= reloadTime) {

			toReload = false;

			reloadCounter = 0f;

			currentBullets = totalBullets;

			doOnce = false;

		}
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		toShoot = true;
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		toShoot = false;
	}
}
