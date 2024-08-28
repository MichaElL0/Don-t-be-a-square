using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    int playerLives = 3;
	public TMP_Text livesUI;

	public static event Action OnPlayerDamage;


    public void TakeDamage()
    {
        OnPlayerDamage?.Invoke();
        playerLives--;
        livesUI.text = "LIVES: " + "O O O".Substring(0, playerLives+1);
		FindObjectOfType<AudioManager>().Play("Hit");
		if (playerLives <= 0)
        {
            GameManager.instance._waveState = GameManager.WaveState.End;
        }
    }
}
