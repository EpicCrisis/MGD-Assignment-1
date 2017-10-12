using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

	// To create a class that stores all audio variables.//
	// Each sound item is called via string name whenever a code to call is executed.//
	// Each sound has independent variables that effect volume and pitch, etc.//

	public static AudioManager instance;

	public SoundClass[] sounds;

	void Awake ()
	{

		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
			return;
		}

		DontDestroyOnLoad (gameObject);

		foreach (SoundClass sound in sounds) {
			
			sound.source = gameObject.AddComponent<AudioSource> ();

			sound.source.clip = sound.clip;
			sound.source.volume = sound.volume;
			sound.source.pitch = sound.pitch;
			sound.source.loop = sound.loop;
		}
	}

	void Start ()
	{
		Play ("BGM");
	}


	void Update ()
	{
		
	}

	public void Play (string name)
	{
		SoundClass s = Array.Find (sounds, sound => sound.name == name);
		if (s == null) {
			Debug.LogWarning ("Sound with name: " + name + " is not found!");
			return;
		}
		s.source.Play ();
	}

	// To play sound.//
	// FindObjectOfType<AudioManager> ().Play ();//
}
