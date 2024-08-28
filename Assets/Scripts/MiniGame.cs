using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    public GameObject panel;

	private void OnEnable()
	{
		EnemyClass.OnClickEnemy += EnterMiniGame;
        PlayerScript.OnPlayerDamage += QuitMiniGame;
	}

	public void EnterMiniGame()
    {
        Debug.LogError("Start mini game!!!");
        panel.SetActive(true);
        Time.timeScale = 0.4f;
    }

    public void QuitMiniGame()
    {
        Debug.LogError("Quit mini game!!!");
    }


}
