using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

	public Transform target;
	public float distance = 10f;

	void Start ()
	{
		
	}

	void Update ()
	{
		gameObject.transform.position = new Vector3 (target.position.x, target.position.y + distance, target.position.z - distance);
	}
}
