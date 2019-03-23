using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

public class MainMenuScript : MonoBehaviour
{
	
	public void Medicine()
	{
		SceneManager.LoadScene(1);
	}

	public void Student()
	{
		SceneManager.LoadScene(2);
	}

	public void Doctor()
	{
		SceneManager.LoadScene(3);
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void Home()
	{
		SceneManager.LoadScene(0);
	}

	public void Youtube()
	{
		Application.OpenURL("https://www.youtube.com/watch?v=nNdwe43bMKA");
	}

	public void Instagram()
	{
		Application.OpenURL("https://www.instagram.com/medicalfacts.uk/");
	}

	

}
