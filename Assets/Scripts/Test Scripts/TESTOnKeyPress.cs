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
			TESTScoreTracking.updateUI += AddMissesScorePoints;
			TESTScoreTracking.updateUI();
			TESTScoreTracking.updateUI -= AddMissesScorePoints;
		}
		if (Input.GetKeyDown(KeyCode.I))
		{
			TESTScoreTracking.updateUI += AddCurrentScorePoints;
			TESTScoreTracking.updateUI();
			TESTScoreTracking.updateUI -= AddCurrentScorePoints;
		}
    }


	void AddMissesScorePoints()
	{
		score.missScore++;
		score.UpdateMissesScore();
		Debug.Log(score.missScore);
	}
	void AddCurrentScorePoints()
	{

		score.currentScore += 10;
		score.UpdateCurrentScore();
		Debug.Log(score.currentScore);
		score.CheckHighScore();
		score.UpdateHighScoreText();
	}

}
