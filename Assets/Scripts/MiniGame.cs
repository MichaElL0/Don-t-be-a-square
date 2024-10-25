using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using EZCameraShake;
using System.Linq;

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
		int wCount = 0;
		int sCount = 0;
		int aCount = 0;
		int dCount = 0;
		for (int i = 0; i < 4; i++)
		{
			bool letterAssigned = false;
			textLetters[i].text = letters[Random.Range(0, 4)];
			while (!letterAssigned)
			{
				string randomLetter = letters[Random.Range(0, 4)];

				switch (randomLetter)
				{
					case "W":
						if (wCount < 2)
						{
							wCount++;
							textLetters[i].text = "W";
							letterAssigned = true;
						}
						break;
					case "S":
						if (sCount < 2)
						{
							sCount++;
							textLetters[i].text = "S";
							letterAssigned = true;
						}
						break;
					case "A":
						if (aCount < 2)
						{
							aCount++;
							textLetters[i].text = "A";
							letterAssigned = true;
						}
						break;
					case "D":
						if (dCount < 2)
						{
							dCount++;
							textLetters[i].text = "D";
							letterAssigned = true;
						}
						break;
				}
			}

			letterCombination[i] = textLetters[i].text;
			
		}
	}
}


