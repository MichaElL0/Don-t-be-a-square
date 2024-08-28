using UnityEngine;
using UnityEngine.Events;

public class Effects : MonoBehaviour
{
	[Header("Effects")]
	public UnityEvent Effect;
	//public RectTransform react;

	private Vector2 startPosition;
	private float startTime;

	private void Start()
	{
		//startPosition = react.anchoredPosition;
		startTime = Time.time;
	}

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

	public void WaveEffect(float speed)
	{
		float elapsedTime = Time.time - 1;
		float newX = startPosition.x + elapsedTime * 1 * 10;
		float sineAmmount = startPosition.y + 10 * Mathf.Sin((elapsedTime * 1) + (Time.time * 1));
		//react.anchoredPosition = new Vector2(newX, sineAmmount);
	}
}
