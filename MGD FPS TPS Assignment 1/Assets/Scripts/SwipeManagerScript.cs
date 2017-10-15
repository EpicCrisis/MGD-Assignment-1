using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwipeDirection
{
	NONE = 0,

	UP,
	DOWN,
	LEFT,
	RIGHT,

	TOTAL
}

public class SwipeManagerScript : MonoBehaviour
{

	//First detect the moment finger taps on screen, then start a timer. //
	//Timer counts down on how long until finger leaves the screen. //
	//During that interval, distance is gauge on how far the finger has moved over the x and y axis within the time. //

	public static SwipeManagerScript instance;

	public SwipeDirection swipeDirection;

	Vector2 fingerStartPosition = Vector2.zero;

	float fingerStartTime = 0.0f;

	bool isSwipe = false;
	bool isSwipeDown = false;

	public float minSwipeDistance = 100.0f;
	public float maxSwipeTime = 0.5f;

	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
			return;
		}

		DontDestroyOnLoad (gameObject);
	}

	void Start ()
	{
		swipeDirection = SwipeDirection.NONE;
	}


	void Update ()
	{
		CheckSwipe ();
	}

	void CheckSwipe ()
	{
		isSwipeDown = false;

		if (Input.touchCount > 0) {

			foreach (Touch _touch in Input.touches) {

				switch (_touch.phase) {

				case TouchPhase.Began:
					
					if (!isSwipe) {

						isSwipe = true;
						fingerStartTime = Time.time;
						fingerStartPosition = _touch.position;
						swipeDirection = SwipeDirection.NONE;

					}
					break;

				case TouchPhase.Canceled:

					if (isSwipe) {

						isSwipe = false;

					}
					break;

				case TouchPhase.Ended:

					float gestureTime = Time.time - fingerStartTime;
					float gestureDistance = Vector2.Distance (_touch.position, fingerStartPosition);

					if (isSwipe && gestureTime < maxSwipeTime && gestureDistance > minSwipeDistance) {

						Vector2 direction = _touch.position - fingerStartPosition;
						Vector2 swipeType = Vector2.zero;

						if (Mathf.Abs (direction.x) > Mathf.Abs (direction.y)) {

							swipeType = Vector2.right * Mathf.Sign (direction.x);

						} else {

							swipeType = Vector2.up * Mathf.Sign (direction.y);

						}

						if (swipeType.x != 0) {

							if (swipeType.x > 0) {

								swipeDirection = SwipeDirection.RIGHT;

							} else {

								swipeDirection = SwipeDirection.LEFT;

							}

							isSwipeDown = true;
							isSwipe = false;

						} else if (swipeType.y != 0) {

							if (swipeType.y > 0) {

								swipeDirection = SwipeDirection.UP;

							} else {

								swipeDirection = SwipeDirection.DOWN;

							}

							isSwipeDown = true;
							isSwipe = false;
						}

					} else {

						isSwipe = false;

					}
					break;

				}
			}
		}
	}

	public SwipeDirection GetSwipe ()
	{
		if (!isSwipeDown) {
			return SwipeDirection.NONE;
		} else {
			return swipeDirection;
		}
	}
}
