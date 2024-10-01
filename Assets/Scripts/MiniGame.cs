using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class MiniGame : MonoBehaviour
{
    public static bool isInGame = false;
    public GameObject panel;
    public TMP_Text[] textLetters;

    private string[] letters = {"W", "S", "A", "D"};

	private void OnEnable()
	{
		EnemyClass.OnClickEnemy += EnterMiniGame;
        PlayerScript.OnPlayerDamage += QuitMiniGame;
	}

	public void EnterMiniGame()
    {
        isInGame = true;
        Debug.LogError("Start mini game!!!");
        panel.SetActive(true);
        Time.timeScale = 0.4f;
        foreach (var TMPLetter in textLetters)
        {
            TMPLetter.text = letters[Random.Range(0, 4)];
        }
    }

    public void QuitMiniGame()
    {
        isInGame = false;
        Debug.LogError("Quit mini game!!!");
    }


}
