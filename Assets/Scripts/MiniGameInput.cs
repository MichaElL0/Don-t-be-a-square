using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameInput
{
	public bool isWPressed()
	{
		return Input.GetKeyDown(KeyCode.W);
	}

	public bool isSPressed()
	{
		return Input.GetKeyDown(KeyCode.S);
	}

	public bool isAPressed()
	{
		return Input.GetKeyDown(KeyCode.A);
	}

	public bool isDPressed()
	{
		return Input.GetKeyDown(KeyCode.D);
	}

}
