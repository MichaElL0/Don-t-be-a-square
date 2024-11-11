using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using Random = UnityEngine.Random;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEditor.ShaderGraph.Internal;

public class EnemyClass : MonoBehaviour
{
	public float speed = 5f;
	public float swayAmmount = 1;
	GameObject target;
	ParticleSystem particleSystemOBJ;
	Rigidbody2D rb;

	public float swayAmplitude = 0.5f; // Distance of sway
	public float swayFrequency = 2f;

	public static event Action<GameObject> OnClickEnemy;

	private void OnEnable()
	{
		MiniGame.OnMiniGameQuit += KillEnemy;
		//MiniGame.OnMiniGameQuit += WaveSplash;
	}

	private void OnDisable()
	{
		MiniGame.OnMiniGameQuit -= KillEnemy;
		//MiniGame.OnMiniGameQuit -= WaveSplash;
	}

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Start is called before the first frame update
	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player");
		particleSystemOBJ = GetComponentInChildren<ParticleSystem>();
	}

	private void Update()
	{
		Vector3 direction = transform.InverseTransformPoint(target.transform.position);
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

		this.transform.Rotate(0, 0, angle);
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (target != null)
		{
			Vector2 direction = (target.transform.position - transform.position).normalized;
			Vector2 swayDirection = Vector2.Perpendicular(direction);
			float swayOffset = Mathf.Sin(Time.time * swayFrequency) * swayAmplitude;
			Vector2 moveDirection = direction * speed + swayDirection * swayOffset;
			rb.MovePosition((Vector2)transform.position + moveDirection * speed * Time.fixedDeltaTime);
		}
	}

	private void OnMouseDown()
	{
		if (!MiniGame.isInGame)
		{
			FindObjectOfType<AudioManager>().Play("Enemy click");
			OnClickEnemy?.Invoke(this.gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			target.GetComponent<PlayerScript>().TakeDamage();

			RemoveEnemy();
			DestroyEnemy();
		}
	}

	void KillEnemy(GameObject enemy)
	{
		if (enemy == gameObject)
		{
			FindObjectOfType<AudioManager>().Pitch("Enemy kill", Random.Range(0.7f, 0.9f));
			FindObjectOfType<AudioManager>().Play("Enemy kill");
			RemoveEnemy();
			DestroyEnemy();
		}
	}

	void DestroyEnemy()
	{
		transform.DetachChildren();
		particleSystemOBJ.Play();
		CameraShaker.Instance.ShakeOnce(3f, 3f, .2f, 1f);
		Destroy(gameObject);
	}

	void RemoveEnemy()
	{
		EnemySpawner.enemiesKilled++;
		EnemySpawner.enemiesSpawnedList.Remove(this.gameObject);
	}
}