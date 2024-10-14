using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class MiniGame : MonoBehaviour
{
    public static bool isInGame = false;
    public GameObject panel;

	MiniGameInput input;

    public TMP_Text[] textLetters;
    private string[] letters = {"W", "S", "A", "D"};
    public string[] letterCombination = new string[4];
    private int currentIndex = 0;

	private void OnEnable()
	{
		EnemyClass.OnClickEnemy += EnterMiniGame;
        PlayerScript.OnPlayerDamage += QuitMiniGame;
        input = new MiniGameInput();
	}

	void Update()
	{
        //----------------
		if (isInGame)
        {
            if(currentIndex < letterCombination.Length)
            {
				if (Input.GetKeyDown(letterCombination[currentIndex].ToLower()))
				{
					print("THIS! : " + letterCombination[currentIndex]);

                    currentIndex++;
				}
			}
            else
            {
				print("You've done it!");
                QuitMiniGame();
			}

        }
	}

	public void EnterMiniGame()
    {
        isInGame = true;
        Debug.LogError("Start mini game!!!");
        panel.SetActive(true);
        Time.timeScale = 0.4f;

        GenerateRandomLetters();
    }

    public void QuitMiniGame()
    {
        isInGame = false;
        panel.SetActive(false);
        Time.timeScale = 1;
        Debug.LogError("Quit mini game!!!");
    }

    void GenerateRandomLetters()
    {
        currentIndex = 0;
		for (int i = 0; i < 4; i++)
		{
            textLetters[i].text = letters[Random.Range(0, 4)];

            letterCombination[i] = textLetters[i].text;
		}
	}

    
}
