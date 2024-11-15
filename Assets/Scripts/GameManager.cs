using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public EnemySpawner enemySpawner;

	public static event Action<WaveState> OnWaveStateChanged;

	[HideInInspector] public Wave wave;

	[HideInInspector] public bool isDoneWithLastWave = false;

	public PlayerScript playerScript;

	[Header("Wave setup")]
	public Wave startingWave;
	public WaveState startingWaveState;

	private WaveState waveState;
	public WaveState _waveState
	{
		get { return waveState; }
		set
		{
			if (waveState != value)
			{
				waveState = value;
				Debug.Log("State is changed to: " + value);
				OnWaveStateChanged?.Invoke(waveState);
			}
		}
	}

	[Header("UI elements")]
	public TMP_Text waveCountUI;
	public TMP_Text completedMessageUI;
	public GameObject completedMessagePanel;
	public TMP_Text countdown;
	public Animator animatorCountdown;


	[Header("")]
	public List<string> randomCompleteMessage = new List<string>();

	public enum Wave
	{
		Wave1 = 8,
		Wave2 = 9,
		Wave3 = 11,
		Wave4 = 13,
		//
	}

	public enum WaveState
	{
		Przed,
		Waiting,
		Spawning,
		Completed,
		End
	}

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		instance = this;
	}

	private void OnEnable()
	{
		OnWaveStateChanged += HandleWaveStateChanged;
	}

	private void OnDisable()
	{
		OnWaveStateChanged -= HandleWaveStateChanged;
	}

	void Start()
	{
		wave = startingWave;
		_waveState = startingWaveState;
	}

	private void HandleWaveStateChanged(WaveState newState)
	{
		switch (newState)
		{
			case WaveState.Waiting:
				Debug.Log("Prepare for another wave! Wave: " + wave);
				WaveWaiting();
				break;
			case WaveState.Spawning:
				if(completedMessagePanel.activeSelf)
				{
					completedMessagePanel.SetActive(false);
				}
				enemySpawner.WaveStateSpawn(wave);
				waveCountUI.text = "Wave: " + wave.ToString().Substring(4);
				break;
			case WaveState.Completed:
				if(isDoneWithLastWave)
				{
					_waveState = WaveState.End;
				}
				else
				{
					WaveCompleted();	
				}
				break;
			case WaveState.End:
				End();
				break;
			default:
				Debug.LogError("No such wave state!");
				break;
		}
	}

	public void WaveCompleted()
	{
		completedMessageUI.text = "You completed wave " + (Int32.Parse(wave.ToString().Substring(4)) - 1) + " in " + enemySpawner.timeSinceWaveStarted + "s time! " + randomCompleteMessage[Random.Range(0, randomCompleteMessage.Count)] + "\n\nReady for another wave? \r\nWave in: ";
		completedMessagePanel.SetActive(true);
		if(playerScript.playerLives < 3)
		{
			playerScript.playerLives++;
			playerScript.UpdateUI();
		}
		_waveState = WaveState.Waiting;
	}

	public void WaveWaiting()
	{
		StartCoroutine(WaitToChangeState(WaveState.Spawning));
	}

	IEnumerator WaitToChangeState(WaveState state)
	{
		int count = 5;
		while (count >= 1)
		{
			countdown.text = count.ToString();
			if(count != 1)
				FindObjectOfType<AudioManager>().Play("Count");
			else
				FindObjectOfType<AudioManager>().Play("Count final");
			animatorCountdown.SetTrigger("Count");
			Debug.Log(count--);
			yield return new WaitForSeconds(1);
		}
		count = 5;
		_waveState = state;
	}

	public void End()
	{
		SceneManager.LoadScene("End");
	}
}
