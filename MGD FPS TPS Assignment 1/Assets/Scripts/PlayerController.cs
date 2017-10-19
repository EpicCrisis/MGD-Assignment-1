using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	//Uses an empty game object as a ground checker, checks distance from ground. //
	//Custom gravity and velocity, not using Rigidbody. //

	public float jumpHeight = 5.0f;
	public float dashDistance = 5.0f;
	public float groundDistance = 0.1f;
	public float gravity = -29.81f;

	public bool isGrounded = false;

	public Vector3 damping;

	public Transform groundChecker;

	public LayerMask Ground;

	public GameObject dashEffects;
	public GameObject jumpEffects;

	CharacterController controller;

	Vector3 velocity;

	void Start ()
	{
		controller = GetComponent<CharacterController> ();
	}


	void Update ()
	{
		if (!GameSettings.instance.isPaused) {
			
			CheckPhysics ();
			CheckGrounded ();
			ToDash ();

		}
	}

	void ToDash ()
	{
		if (SwipeScript.instance.IsSwiping (_SwipeDirection.UP)) {

			AudioManager.instance.Play ("Dash");

			//transform.rotation = Quaternion.Euler (transform.rotation.x, 0.0f, transform.rotation.z);
			//velocity += Vector3.Scale (transform.forward, dashDistance * new Vector3 ((Mathf.Log (1f / (Time.deltaTime * damping.x + 1)) / -Time.deltaTime), 0f, (Mathf.Log (1f / (Time.deltaTime * damping.z + 1)) / -Time.deltaTime)));

			GameObject dashGO = Instantiate (dashEffects, new Vector3 (transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
			dashGO.transform.parent = gameObject.transform;

			velocity += Vector3.forward + (dashDistance * new Vector3 (0f, 0f, (Mathf.Log (1f / (Time.deltaTime * damping.z + 1)) / -Time.deltaTime)));

			Destroy (dashGO, 2.0f);

		} else if (SwipeScript.instance.IsSwiping (_SwipeDirection.DOWN)) {

			AudioManager.instance.Play ("Dash");

			GameObject dashGO = Instantiate (dashEffects, new Vector3 (transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
			dashGO.transform.parent = gameObject.transform;

			velocity += Vector3.forward + (dashDistance * new Vector3 (0f, 0f, -(Mathf.Log (1f / (Time.deltaTime * damping.z + 1)) / -Time.deltaTime)));

			Destroy (dashGO, 2.0f);

		} else if (SwipeScript.instance.IsSwiping (_SwipeDirection.LEFT)) {

			AudioManager.instance.Play ("Dash");

			GameObject dashGO = Instantiate (dashEffects, new Vector3 (transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
			dashGO.transform.parent = gameObject.transform;

			velocity += Vector3.forward + (dashDistance * new Vector3 (-(Mathf.Log (1f / (Time.deltaTime * damping.x + 1)) / -Time.deltaTime), 0f, 0f));

			Destroy (dashGO, 2.0f);

		} else if (SwipeScript.instance.IsSwiping (_SwipeDirection.RIGHT)) {

			AudioManager.instance.Play ("Dash");

			GameObject dashGO = Instantiate (dashEffects, new Vector3 (transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
			dashGO.transform.parent = gameObject.transform;

			velocity += Vector3.forward + (dashDistance * new Vector3 ((Mathf.Log (1f / (Time.deltaTime * damping.x + 1)) / -Time.deltaTime), 0f, 0f));

			Destroy (dashGO, 2.0f);

		}
	}

	public void ToJump ()
	{
		if (!GameSettings.instance.isPaused) {
			
			if (isGrounded) {

				AudioManager.instance.Play ("Jump");

				velocity.y += Mathf.Sqrt (jumpHeight * -2f * gravity);

				GameObject jumpGO = Instantiate (jumpEffects, transform.position, Quaternion.identity);

				Destroy (jumpGO, 2.0f);

			}
		}
	}

	public void CheckGrounded ()
	{
		isGrounded = Physics.CheckSphere (groundChecker.position, groundDistance, Ground, QueryTriggerInteraction.Ignore);

		if (isGrounded && velocity.y < 0f) {

			velocity.y = 0f;

		}
	}

	public void CheckPhysics ()
	{
		velocity.y += gravity * Time.deltaTime;

		velocity.x /= 1 + damping.x * Time.deltaTime;
		velocity.y /= 1 + damping.y * Time.deltaTime;
		velocity.z /= 1 + damping.z * Time.deltaTime;

		controller.Move (velocity * Time.deltaTime);
	}
}
