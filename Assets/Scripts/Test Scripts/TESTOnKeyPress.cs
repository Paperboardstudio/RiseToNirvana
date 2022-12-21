using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTOnKeyPress : MonoBehaviour
{
	public ScoreManager score;
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.P))
		{
			ScoreManager.updateScore += AddMissesScorePoints;
			ScoreManager.updateScore();
			ScoreManager.updateScore -= AddMissesScorePoints;
			Time.timeScale = 1f;
		}
		if (Input.GetKeyDown(KeyCode.I))
		{
			ScoreManager.updateScore += AddCurrentScorePoints;
			ScoreManager.updateScore();
			ScoreManager.updateScore -= AddCurrentScorePoints;
			Time.timeScale = 0f;
		}
    }


	void AddMissesScorePoints()
	{
		score.missScore++;
		//score.UpdateMissesScore();
		Debug.Log(score.missScore);
	}
	void AddCurrentScorePoints()
	{

		score.currentScore += 10;
		//score.UpdateCurrentScore();
		Debug.Log(score.currentScore);
		score.CheckHighScore();
		score.UpdateUI();
	}

}
