using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Effects : MonoBehaviour
{
	[Header("Effects")]
	public UnityEvent Effect;

	// Update is called once per frame
	void Update()
    {
		Effect?.Invoke();
	}

	public void SpinEffect(float speed)
	{
		float sineAmmount = 1 * Mathf.Sin(Time.time * speed);
		transform.localScale = new Vector3(sineAmmount, 1, 0);
	}

	public void SwayEffect(float speed)
	{
		float angle = 1 * Mathf.Sin(Time.time * speed);
		transform.localRotation = Quaternion.Euler(0, 0, angle);
	}
}
