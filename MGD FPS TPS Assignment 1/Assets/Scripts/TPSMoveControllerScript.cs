using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TPSMoveControllerScript : MonoBehaviour, IDragHandler, IEndDragHandler
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

		character.Translate (new Vector3 (direction.x, 0f, direction.y) * Time.deltaTime * rotSpeed, Space.World);
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
		direction = Vector3.zero; //reset direction to stop rotation
		isDragging = false;
	}
}
