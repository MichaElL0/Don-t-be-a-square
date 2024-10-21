using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
	public float speed = 5f;
	public float swayAmmount = 1;
	GameObject target;
	ParticleSystem particleSystemOBJ;

	public static event Action<GameObject> OnClickEnemy;

	private void OnEnable()
	{
		MiniGame.OnMiniGameQuit += KillEnemy;
	}

	private void OnDisable()
	{
		MiniGame.OnMiniGameQuit -= KillEnemy;
	}

	// Start is called before the first frame update
	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player");
		particleSystemOBJ = GetComponentInChildren<ParticleSystem>();
	}

	// Update is called once per frame
	void Update()
	{
		if (target != null)
		{
			transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
		}
	}

	private void OnMouseDown()
	{
		if (!MiniGame.isInGame)
		{
			OnClickEnemy?.Invoke(this.gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			target.GetComponent<PlayerScript>().TakeDamage();
			DestroyEnemy();
		}
	}

	void DestroyEnemy()
	{
		transform.DetachChildren();
		particleSystemOBJ.Play();
		Destroy(gameObject);
	}

	void KillEnemy(GameObject enemy)
	{
		if (enemy == gameObject)
		{
			FindObjectOfType<AudioManager>().Play("Enemy kill");
			EnemySpawner.enemiesKilled++;
			EnemySpawner.enemiesSpawnedList.Remove(enemy);

			DestroyEnemy();
		}
	}
}



//void KillEnemy(GameObject enemy)
//{
//	FindObjectOfType<AudioManager>().Play("Enemy kill");
//	EnemySpawner.enemiesKilled++;
//	EnemySpawner.enemiesSpawnedList.Remove(enemy);
//	transform.DetachChildren();
//	particleSystemOBJ.Play();
//	Destroy(this.gameObject);
//}