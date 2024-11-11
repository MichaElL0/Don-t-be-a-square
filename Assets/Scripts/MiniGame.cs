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

	public GameObject letterParticle;

	public GameObject playerObject;

	public LayerMask enemyMask;
	public float waveBlastRadius = 5f;



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

					textLetters[currentIndex].GetComponent<Animator>().SetTrigger("Correct");

					Instantiate(letterParticle, textLetters[currentIndex].gameObject.transform.position, Quaternion.identity);

					CameraShaker.Instance.ShakeOnce(2, 2, 0.1f, 0.3f);
					pitch += 0.1f;

					currentIndex++;
				}

				else if (Input.anyKeyDown && !input.IsMouseClick())
				{
					//Wrong KEY
					
					if(Time.timeScale <= 1f)
					{
						Time.timeScale += 0.07f;
						FindObjectOfType<AudioManager>().Play("Wrong");
					}
					else
					{
						Time.timeScale = 1f;
					}	
					
				}
			}
			else
			{
				QuitMiniGame(nowEnemy);
				StartCoroutine(WaveSplash());
			}
		}
	}

	public void EnterMiniGame(GameObject enemy)
	{
		if (enemy is null)
		{
			Debug.LogWarning("Enemy is already destroyed.");
			return;
		}

		nowEnemy = enemy;
		isInGame = true;
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

		foreach (var letter in textLetters)
		{
			letter.GetComponent<Animator>().SetTrigger("Reset");
		}

		pitch = defaultPitch;
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

	public IEnumerator WaveSplash()
	{
		foreach (var enemy in EnemySpawner.enemiesSpawnedList)
		{
			Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();

			print("Add force to this enemy: " + enemy.name);

			Vector2 pushDirection = (enemy.transform.position - playerObject.transform.position).normalized;
			enemyRb.AddForce(pushDirection * 2, ForceMode2D.Impulse);

			yield return new WaitForSeconds(0.3f);  
			enemyRb.velocity = Vector2.zero;
			//yield return null;
		}
	}
}


