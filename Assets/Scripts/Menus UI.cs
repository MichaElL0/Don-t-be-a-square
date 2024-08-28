using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenusUI : MonoBehaviour
{
	[SerializeField] private AudioMixer audioMixer;
	[SerializeField] private Slider slider;

	public void StartButton()
	{
		SceneManager.LoadScene(1);
	}

	public void ExitButton()
	{
		Application.Quit();
	}

	public void ChangeVolume()
	{
		float volume = slider.value;
		audioMixer.SetFloat("Volume", Mathf.Log10(volume)*20);
	}
}
