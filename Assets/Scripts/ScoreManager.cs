using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
	public delegate void UpdateScore();
	public static UpdateScore updateScore;

	[SerializeField] TextMeshProUGUI highScoreText;
	[SerializeField] TextMeshProUGUI missesScoreText;
	[SerializeField] TextMeshProUGUI currentScoreText;

	public int missScore = 0;
	public int currentScore = 0;

	// Start is called before the first frame update
	void Start()
    {
		UpdateHighScoreText();
		UpdateCurrentScore();
		UpdateMissesScore();
    }

	/// <summary>
	/// Creates an int variable named "HighScore" that stores the highest value of "currentScore"
	/// Checks if the value of "currentScore" is bigger than the value of the "HighScore" variable
	/// Only stores the value of "HighScore" localy on the PC
	/// Can be dynamic by being call in the function that adds points to currentScore
	/// </summary>
	public void CheckHighScore()
	{
		if (currentScore > PlayerPrefs.GetInt("HighScore", 0))
		{
			PlayerPrefs.SetInt("HighScore", currentScore);
			UpdateHighScoreText();
		}
	}
	/// <summary>
	/// Updates the text of "highScoreText" and the value of "HighScore"
	/// Can be dynamic by being call in the function that adds points to currentScore
	/// </summary>
	public void UpdateHighScoreText()
	{
		highScoreText.text = $"HighScore: {PlayerPrefs.GetInt("HighScore", 0)}";
	}
	/// <summary>
	/// Updates the text of "currentScoreText"
	/// Can be dynamic by being call in the function that adds points to currentScore
	/// </summary>
	public void UpdateCurrentScore()
	{
		currentScoreText.text = "Current Score: " + currentScore.ToString();
	}
	/// <summary>
	/// Updates the text of "missesScoreText"
	/// Can be dynamic by being call in the function that adds points to missScore
	/// </summary>
	public void UpdateMissesScore()
	{
		missesScoreText.text = "Misses: " + missScore.ToString();
	}
}
