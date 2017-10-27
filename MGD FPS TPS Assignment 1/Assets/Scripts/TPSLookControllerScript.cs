using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

	public int totalBullets = 64;
	public int clipBullets = 64;
	public int currentBullets = 64;
	public int usedBullets = 2;

	bool toReload = false;

	public float reloadTime = 2.0f;
	float reloadCounter = 0.0f;

	bool doOnce = false;

	public Text ammoTextUI;

	void Start ()
	{
		initialPos = this.transform.position; //set initial position

		CheckPlayerAmmo ();
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

				AudioManager.instance.Play ("MP5");

				shootCounter = 0f;

				if (currentBullets >= usedBullets) {
					
					currentBullets -= usedBullets;

					muzzle1.Play ();

					muzzle2.Play ();

					CustomObjectPoolScript.Instance.Spawn ("Bullet", bulletHole1.position, bulletHole1.rotation);

					CustomObjectPoolScript.Instance.Spawn ("Bullet", bulletHole2.position, bulletHole2.rotation);

				} else {

					muzzle1.Play ();

					CustomObjectPoolScript.Instance.Spawn ("Bullet", bulletHole1.position, bulletHole1.rotation);

					currentBullets -= usedBullets / 2;
				}

				CheckPlayerAmmo ();

			}
		}
	}

	public void CheckReload ()
	{
		if (currentBullets <= 0) {

			toReload = true;

			if (totalBullets >= 1) {

				reloadCounter += Time.deltaTime;

				if (!doOnce) {
				
					AudioManager.instance.Play ("Reload1");

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

			CheckPlayerAmmo ();

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

	public void CheckPlayerAmmo ()
	{
		ammoTextUI.text = "AMMO: " + currentBullets + "/" + totalBullets;
	}
}
