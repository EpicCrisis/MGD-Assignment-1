using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

	public string FPSLevel;
	public string TPSLevel;

	public GameObject[] menuPanels;
	public Slider[] sliders;

	void Awake ()
	{
		
	}

	void Start ()
	{
//		SetupVolume (sliders [0]);
		SetupBrightness (sliders [0]);
	}


	void Update ()
	{
		
	}

	public void StartFPSScene ()
	{
		SceneManager.LoadScene (FPSLevel);
	}

	public void StartTPSScene ()
	{
		SceneManager.LoadScene (TPSLevel);
	}

	//Finds the menu enum in the public list, then proceed to set active.//

	public void OpenMenu (int menu)
	{
		menuPanels [menu].SetActive (true);
	}

	public void CloseMenu (int menu)
	{
		menuPanels [menu].SetActive (false);
	}

	public void ExitGame ()
	{
		Application.Quit ();
	}

	//Finds the slider object in the public list, and change the instance of the audio manager.//

	public void SetupVolume (Slider sliderObject)
	{
//		sliderObject.GetComponent<Slider> ().value = AudioManager.instance.GetComponent<SoundClass> ().volume;
//		ChangeVolume (sliderObject);
	}

	public void ChangeVolume (Slider sliderObject)
	{
//		GameSettings.instance.SetVolume (sliderObject.GetComponent<Slider> ().value);	
	}

	public void SetupBrightness (Slider sliderObject)
	{
		sliderObject.value = GameSettings.instance.brightness;
		ChangeBrightness (sliderObject);
	}

	public void ChangeBrightness (Slider sliderObject)
	{
		GameSettings.instance.SetBrightness (sliderObject.value);
	}
}
