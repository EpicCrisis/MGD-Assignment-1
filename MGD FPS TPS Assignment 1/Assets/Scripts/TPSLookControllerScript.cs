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

	public Transform bulletHole;

	bool toShoot = false;

	public float shootRate = 1f;
	float shootCounter = 0f;

	public ParticleSystem muzzle;

	void Start ()
	{
		initialPos = this.transform.position; //set initial position
	}


	void Update ()
	{
		if (!GameSettings.instance.isPaused) {
			
			CheckInput ();

			CheckShoot ();

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
		if (toShoot) {

			shootCounter += Time.deltaTime;

			if (shootCounter >= shootRate) {

				muzzle.Play ();

				AudioManager.instance.Play ("MP5");

				CustomObjectPoolScript.Instance.Spawn ("Bullet", bulletHole.position, bulletHole.rotation);

				shootCounter = 0f;

			}
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
