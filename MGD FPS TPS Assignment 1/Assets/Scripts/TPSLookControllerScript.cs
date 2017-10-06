using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TPSLookControllerScript : MonoBehaviour, IDragHandler, IEndDragHandler
{
	public Transform character;
	Vector3 initialPos;
	Vector3 direction;

	public float rotSpeed = 3f;
	bool isDragging = false;


	void Start ()
	{
		initialPos = this.transform.position; //set initial position
	}


	void Update ()
	{
		if (isDragging) {
			direction = this.transform.position - initialPos;
		}
		direction.Normalize ();

		float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;

		character.rotation = Quaternion.Lerp (character.rotation, Quaternion.Euler (0f, -angle + 90f, 0f), Time.deltaTime * rotSpeed);
	}


	void LateUpdate ()
	{

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
}
