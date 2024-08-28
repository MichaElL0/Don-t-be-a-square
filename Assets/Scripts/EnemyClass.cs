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

    public static event Action OnClickEnemy;

	private void OnEnable()
	{
        //OnClickEnemy += miniGameScript.EnterMiniGame;
	}

	private void OnDisable()
	{
        //OnClickEnemy -= miniGameScript.EnterMiniGame;
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
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

	private void OnMouseDown()
	{
        OnClickEnemy?.Invoke();
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        print(target);
        target.GetComponent<PlayerScript>().TakeDamage();
        Destroy(gameObject);
	}

    void KillEnemy()
    {
		FindObjectOfType<AudioManager>().Play("Enemy kill");
		EnemySpawner.enemiesKilled++;
		EnemySpawner.enemiesSpawnedList.Remove(this.gameObject);
        transform.DetachChildren();
        particleSystemOBJ.Play();
        Destroy(this.gameObject);
	}

    
}
