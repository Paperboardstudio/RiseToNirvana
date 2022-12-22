using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScene : MonoBehaviour
{
	/// <summary>
	/// Kinda hardcoded script for moving between scenes, the numbers can be found in the build option
	/// </summary>
	/// <param name="sceneNumber">The number of the scene in the build is the one we move to </param>
	public void MoveToPlay(int sceneNumber)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNumber);
	}
	public void QuitGame()
	{
		Application.Quit();
	}
}
