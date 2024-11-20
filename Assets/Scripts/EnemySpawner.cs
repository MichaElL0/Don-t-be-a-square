using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] List<GameObject> spawnPoints = new();
	[SerializeField] List<GameObject> enemyList = new();
	public static List<GameObject> enemiesSpawnedList = new();

	public static int enemiesKilled = 0;
	private int maxEnemiesForWave;
	private int enemiesSpawned = 0;
	[HideInInspector] public float timeSinceWaveStarted = 0;
	//bool killedAllEnemies = false;

	[Header("Wave stats")]
	[SerializeField] float wave1SpawnTime;
	[SerializeField] float wave2SpawnTime;
	[SerializeField] float wave3SpawnTime;
	[SerializeField] float wave4SpawnTime;

	public void WaveStateSpawn(GameManager.Wave waveState)
	{
		switch (waveState)
		{
			case GameManager.Wave.Wave1:
				StartSpawningWave(0, false, wave1SpawnTime, Wave.Wave2);
				break;
			case GameManager.Wave.Wave2:
				StartSpawningWave(1, false, wave2SpawnTime, Wave.Wave3);
				break;
			case GameManager.Wave.Wave3:
				StartSpawningWave(2, false, wave3SpawnTime, Wave.Wave4);
				break;
			case GameManager.Wave.Wave4:
				StartSpawningWave(0, true, wave4SpawnTime, Wave.Wave4);
				GameManager.instance.isDoneWithLastWave = true;
				break;
			default:
				Debug.LogError("The wave is above wave 4 or 0");
				break;
		}
	}

	void StartSpawningWave(int enemyPos, bool isFinalWave, float waitTimeBtw, GameManager.Wave nextWave)
	{
		if (gameObject.activeInHierarchy) // Ensure the GameObject is active
		{
			Debug.Log("Starting wave spawning...");
			StartCoroutine(SpawnEnemyWave(enemyPos, isFinalWave, waitTimeBtw, nextWave));
		}
	}

	IEnumerator SpawnEnemyWave(int _enemyType, bool _isFinalWave, float _waitTime, GameManager.Wave nextWave)
	{
		maxEnemiesForWave = (int)GameManager.instance.wave;
		print(maxEnemiesForWave);
		for (int i = 1; i <= maxEnemiesForWave; i++)
		{
			yield return new WaitForSeconds(_waitTime);
			SpawnEnemy(_enemyType, _isFinalWave);
		}

		while (enemiesSpawnedList.Count >= 1)
		{
			yield return new WaitForSeconds(1);
			Debug.Log("Spawning... Time since wave started: " + timeSinceWaveStarted);
			timeSinceWaveStarted++;
		}
		GameManager.instance.wave = nextWave;
		GameManager.instance._waveState = GameManager.WaveState.Completed;
		enemiesSpawned = 0;
		enemiesKilled = 0;
		timeSinceWaveStarted = 0;
	}

	void SpawnEnemy(int enemy_Type, bool is_Final_Wave)
	{
		GameObject enemyPrefab = !is_Final_Wave ? enemyList[enemy_Type] : enemyList[Random.Range(0, 3)];
		GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position, Quaternion.identity);

		enemiesSpawnedList.Add(enemyInstance);
		enemiesSpawned++;
	}
}
