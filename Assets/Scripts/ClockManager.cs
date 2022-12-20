using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockManager : MonoBehaviour
{
	//how many real life seconds does it take for 1 hour
	private const float REAL_SECONDS_PER_DAY = 30f;

	[SerializeField] TextMeshProUGUI timeText;
	private float day;

	private void Update()
	{
		//to pause timer set Time.timeScale = 0f and to resume 1f
		day += Time.deltaTime / REAL_SECONDS_PER_DAY;
		
		float dayNormalized = day % 1f;
		//how many hours are in a day
		float hoursPerDay = 8f;
		float startingHour = 8f;
		//sets the hours in string form
		string hourString = Mathf.Floor((dayNormalized * hoursPerDay) + startingHour).ToString("00");
		//minutes in day
		float minutesPerDay = 6f;
		//sets minutes in string form
		string minuteString = Mathf.Floor(((dayNormalized * hoursPerDay) % 1f) * minutesPerDay).ToString("0");
		timeText.text = hourString + ":" + minuteString + "0";
	}
}
