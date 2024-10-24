using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using EZCameraShake;

public class MiniGame : MonoBehaviour
{
	public static bool isInGame = false;
	public GameObject panel;

	MiniGameInput input;

	public static event Action<GameObject> OnMiniGameQuit;

	GameObject nowEnemy;

	public TMP_Text[] textLetters;
	private string[] letters = { "W", "S", "A", "D" };
	public string[] letterCombination = new string[4];
	private int currentIndex = 0;
	float defaultPitch = 0.7f;
	float pitch;

	private void Start()
	{
		pitch = defaultPitch;
	}

	private void OnEnable()
	{
		EnemyClass.OnClickEnemy += EnterMiniGame;
		PlayerScript.OnPlayerDamage += QuitMiniGame;
		input = new MiniGameInput();
	}

	private void OnDisable()
	{
		EnemyClass.OnClickEnemy -= EnterMiniGame;
		PlayerScript.OnPlayerDamage -= QuitMiniGame;
	}

	void Update()
	{
		if (isInGame)
		{
			if (currentIndex < letterCombination.Length)
			{
				
				if (Input.GetKeyDown(letterCombination[currentIndex].ToLower()))
				{
					FindObjectOfType<AudioManager>().Play("Correct");
					FindObjectOfType<AudioManager>().Pitch("Correct", pitch);

					CameraShaker.Instance.ShakeOnce(2, 2, 0.1f, 0.3f);
					pitch += 0.1f;

					currentIndex++;
				}
			}
			else
			{
				pitch = defaultPitch;
				QuitMiniGame(nowEnemy);
			}
		}
	}

	public void EnterMiniGame(GameObject enemy)
	{
		if (enemy == null)
		{
			Debug.LogWarning("Enemy is already destroyed.");
			return; 
		}

		nowEnemy = enemy;
		isInGame = true;
		Debug.LogError("Start mini game!!!");
		panel.SetActive(true);
		Time.timeScale = 0.4f;

		GenerateRandomLetters();
	}

	public void QuitMiniGame(GameObject enemy)
	{
		if (enemy == null)
		{
			Debug.LogWarning("Enemy is already destroyed.");
			return;
		}

		isInGame = false;
		panel.SetActive(false);
		Time.timeScale = 1;

		OnMiniGameQuit?.Invoke(enemy);
		nowEnemy = null;

		Debug.LogError("Quit mini game!!!");
	}

	void GenerateRandomLetters()
	{
		currentIndex = 0;
		for (int i = 0; i < 4; i++)
		{
			textLetters[i].text = letters[Random.Range(0, 4)];
			letterCombination[i] = textLetters[i].text;
		}
	}
}
