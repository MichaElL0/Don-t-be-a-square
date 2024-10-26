using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameInput
{
	public bool IsMouseClick()
	{
		return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2);
	}

}
