using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FPSLookControllerScript : MonoBehaviour, IDragHandler, IEndDragHandler
{

	Vector3 initialPos;
	Vector3 direction;

	public Transform xAxis;
	public Transform yAxis;
	//public Transform cameraTrans;

	float clampX;
	public Transform targetXRotation;

	public float rotSpeed = 30f;


	void Start ()
	{
		initialPos = this.transform.position; //set initial position
	}


	void Update ()
	{
		direction = this.transform.position - initialPos;
		//direction.Normalize ();
	}


	void LateUpdate ()
	{
		//rotate in y axis
		yAxis.Rotate (Vector3.up * direction.normalized.x * Time.deltaTime * rotSpeed);

		//rotate in x axis
		clampX = Mathf.Clamp (direction.y, -30f, 30f); //clamp float x value

		targetXRotation.localEulerAngles = new Vector3 (-clampX, 0f, 0f);
		xAxis.rotation = Quaternion.Lerp (xAxis.rotation, targetXRotation.rotation, Time.deltaTime * rotSpeed);

	}


	public void OnDrag (PointerEventData eventData)
	{
		this.transform.position = eventData.position; //make knob follow drag
	}


	public void OnEndDrag (PointerEventData eventData)
	{
		this.transform.position = initialPos; //reset position of knob
		direction = Vector3.zero; //reset direction to stop rotation
	}
}
