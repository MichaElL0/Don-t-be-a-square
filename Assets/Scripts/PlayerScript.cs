using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int playerLives = 3;
	public TMP_Text livesUI;

	public static event Action<GameObject> OnPlayerDamage;

	private void Start()
	{
		UpdateUI();
	}

	public void TakeDamage()
    {
        OnPlayerDamage?.Invoke(this.gameObject);
        playerLives--;
        UpdateUI();

		FindObjectOfType<AudioManager>().Play("Hit");

		if (playerLives <= 0)
        {
            GameManager.instance._waveState = GameManager.WaveState.End;
        }
    }

    public void UpdateUI()
    {
		livesUI.text = "LIVES: " + string.Concat(Enumerable.Repeat("O ", playerLives)); ;
	}
}
