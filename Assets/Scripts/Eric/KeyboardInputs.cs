using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using System;

public enum GameState { FreeRoam, Paused }
public class KeyboardInputs : MonoBehaviour
{
	public static KeyboardInputs i;

	[Header("Prefabs")]
	public GameObject PauseMenu;
	public GameObject MapMenu;
	public DialogueTrigger TriggerDialogue;

	RiseToNirvana Controls;

	[Header("References to script")]
	public Staircase stair;
	public ScoreManager score;

	public StateMachine<KeyboardInputs> StateMachine { get; private set; } //State machine is missing stuff but it works just for checking pausing
	public GameState state { get; private set; }

	public event Action<bool> OnGamePaused;		// Subscribe the event here to the function

	[Header("Debug")]
	public bool DebugMode = false;

	public void Awake()
	{
		i = this;
		Controls = new RiseToNirvana();
	}
	void Start()
	{
		if (stair == null)
		{
			stair = FindObjectOfType<Staircase>();
		}
		if (score == null)
		{
			score = FindObjectOfType<ScoreManager>();
		}

		StateMachine = new StateMachine<KeyboardInputs>(this);
		StateMachine.ChangeState(FreeRoamState.i);


		MapMenu.GetComponentInParent<MapUI>().Init(stair);

		//TriggerDialogue.TutorialTrigger();
		//EventManager.onCutScene += PausedGame;
	}
	public void OnEnable()
	{
		Controls.Enable();
		Controls.Player.OnPause.performed += ctx => PausedGame();
		
		Controls.Player.Newaction.performed += ctx => InputSystem.onAnyButtonPress.CallOnce(CheckKboardInputs);

	}

	private void CheckKboardInputs(InputControl oldcontrol)
	{
		if(state != GameState.Paused)
		{
			string newkey = stair.GetStep();
			string passedKey = oldcontrol.displayName.ToString().ToLower();			
			Time.timeScale = 1f;

			if (newkey.Equals(passedKey))
			{
				AddScoreDelegate();
				stair.DestroyCurrentStep();
			}
			else {
				AddMissDelegate();
			}

			if (DebugMode)
				Debug.Log("Stairs key :" + newkey + " Typed Key: "+passedKey);
		}
	}
	void AddMissDelegate()
	{
		ScoreManager.updateScore += AddMissesScorePoints;
		ScoreManager.updateScore();
		ScoreManager.updateScore -= AddMissesScorePoints;
	}
	void AddScoreDelegate()
	{
		ScoreManager.updateScore += AddCurrentScorePoints;
		ScoreManager.updateScore();
		ScoreManager.updateScore -= AddCurrentScorePoints;
	}
	void AddMissesScorePoints()
	{
		score.missScore++;
		score.UpdateUI();
	}
	void AddCurrentScorePoints()
	{
		score.currentScore += 10;
		score.CheckHighScore();
		score.UpdateUI();
	}
	public void OnDisable()
	{
		Controls.Disable();
	}
	private void Update()
	{
		// StateMachine.Execute(); This belongs here but we did it kinda wrong and update the StateMachine manually
	}


	public void PausedGame()
	{
		if(state != GameState.Paused)
		{
			StateMachine.ChangeState(PausedGameState.i);
			state = GameState.Paused;

			PauseMenu.gameObject.SetActive(true);
		}
		else
		{
			ResumeGame();
		}

		if (DebugMode)
			Debug.Log("Paused Game " + state);
	}

	/// <summary>
	/// Called in PauseMenu, Continue Button
	/// </summary>
	public void ResumeGame()
	{
		state = GameState.FreeRoam;
		StateMachine.ChangeState(FreeRoamState.i);

		PauseMenu.gameObject.SetActive(false);

		if (DebugMode)
			Debug.Log("Paused Game " + state);
	}

	/*
	/// <summary>
	/// Check for inputaction according to your current inputsystem.
	/// Curently using it here but it should be done in the GameController
	/// </summary>
	void CheckKboardInputs()
	{
		//InputSystem.onAnyButtonPress.CallOnce(ctrl => oldcontrol = ctrl.displayName.ToString().ToLower());
		//Debug.Log(Keyboard.current[(Key)16].wasPressedThisFrame);

		InputSystem.onAnyButtonPress += OnAnyButtonPress;

		string newkey = stair.GetStep();
		Debug.Log(newkey);
		Debug.Log(oldcontrol);
		if (newkey.Equals(oldcontrol))
		{
			stair.DestroyCurrentStep();
		}

		if (PlayerInputs.actions["onType"].WasPressedThisFrame())
			Debug.Log("R");
	
		Keyboard kboard = Keyboard.current;

		if (kboard.anyKey.wasPressedThisFrame)
		{
			foreach (KeyControl k in kboard.allKeys)
			{
				if (k.wasPressedThisFrame)
				{
					Debug.Log((int)k.keyCode + "   path = " + k.path);
					break;
				}
			}
		}

	}
*/
	/// <summary>
	/// Called in Pause Menu, Exit Button
	/// </summary>
	public void ExitGame()
	{
		Application.Quit();
	}
	void OnGUI()
	{
		var style = new GUIStyle();
		style.fontSize = 24;

		GUILayout.Label("STATE STACK", style);

		foreach (var state in StateMachine.StateStack)
		{
			GUILayout.Label(state.GetType().ToString(), style);
		}

	}

	public void PauseDialogue()
	{
		state = GameState.Paused;
	}
}
