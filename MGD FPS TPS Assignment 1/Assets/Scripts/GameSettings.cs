using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{

	public static GameSettings instance;

	//public float volume = 0.5f;
	public float brightness = 0.75f;

	public Image brightnessMask;

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
		
	}


	void Update ()
	{
		
	}

	public void SetVolume (float value)
	{
//		volume = value;
//		AudioManager.instance.GetComponent<SoundClass> ().volume = value;
	}

	public void SetBrightness (float value)
	{
		brightness = value;
		brightnessMask.color = new Color (0f, 0f, 0f, 1.0f - brightness);
	}
}
