using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
	float seconds;
	public Text timerText;

	public void startTimer(float s)
	{
		seconds = s;
		Update ();
	}

	public void Update () 
	{
		seconds -= Time.deltaTime;
		if (timerText != null)
		{
			timerText.text = seconds.ToString("f0");
		}
	}
}