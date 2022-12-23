using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using System;

public enum GameState { FreeRoam, Paused , Dialog }
public class KeyboardInputs : MonoBehaviour
{
	public static KeyboardInputs i;

	[Header("Prefabs")]
	[field: SerializeField] GameObject PauseMenu;
	[field: SerializeField] GameObject MapMenu;
	[field: SerializeField] GameObject TutorialMenu;

	public DialogueTrigger TriggerDialogue;
	RiseToNirvana Controls;

	[Header("References to script")]
	[field: SerializeField] private PlayerController playerController;
	public Staircase stair;
	public ScoreManager score;

	public StateMachine<KeyboardInputs> StateMachine { get; private set; } //State machine is missing stuff but it works just for checking pausing
	public GameState state { get; private set; }
	public bool SavedKey = false; // Hacky way of preventing miss score after dialogue

	public event Action<bool> OnGamePaused;		// Subscribe the event here to the function

	[Header("Debug")]
	public bool DebugMode = false;

	[Header("Dialogues")]
	[field: SerializeField] List<Dialog> dialog;

	public void Awake()
	{
		i = this;
		Controls = new RiseToNirvana();
	}

	void Start()
	{
		if (stair == null)
			stair = FindObjectOfType<Staircase>();
		
		if (score == null)
			score = FindObjectOfType<ScoreManager>();
		
		if (playerController == null)
			playerController = FindObjectOfType<PlayerController>();

		DialogueMan.Instance.OnShowDialog += ShowDialog;
		DialogueMan.Instance.OnCloseDialog += CloseDialog;

		StateMachine = new StateMachine<KeyboardInputs>(this);
		StateMachine.ChangeState(FreeRoamState.i);

		MapMenu.GetComponentInParent<MapUI>().Init(stair);

		TutorialMenu.SetActive(true);
		//TriggerDialogue.TutorialTrigger();
		//EventManager.onCutScene += PausedGame;
	}

	void ShowDialog()
	{
		state = GameState.Dialog;
	}

	void CloseDialog()
	{
		if(state == GameState.Dialog)
		{
			state = GameState.FreeRoam;
		}
	}

	public void OnEnable()
	{
		Controls.Enable();
		Controls.Player.OnPause.performed += ctx => PausedGame();

		Controls.Player.Newaction.performed += ctx => InputSystem.onAnyButtonPress.CallOnce(CheckKboardInputs);

		//Controls.Player.Newaction.performed += OnNewactionPerformed;
		//Controls.Player.Newaction.performed += Pressed;
	}

	/*
	public void OnNewactionPerformed(InputAction.CallbackContext ctx)
	{
		InputControl oldcontrol;
		string o = InputSystem.onAnyButtonPress.CallOnce(ctx).ToString();
	}
	*/

	private void CheckKboardInputs(InputControl oldcontrol)
	{
		if (TutorialMenu.activeInHierarchy)
		{
			TutorialMenu.SetActive(false);
			//Start intro dialog
			KeyboardInputs.i.Interact(0);
		}

		if (state == GameState.Dialog)
		{
			playerController.IsWalking(0);
			DialogueMan.Instance.HandleUpdate();
		}

		if (state != GameState.Paused && state != GameState.Dialog)
		{
			string newkey = stair.GetStep();
			string passedKey = oldcontrol.displayName.ToString().ToLower();			
			Time.timeScale = 1f;

			if (newkey.Equals(passedKey))
			{
				SavedKey = false;
				// Walking animation 0 is stop, 1 walking
				playerController.IsWalking(1);
				
				AddScoreDelegate();
				stair.DestroyCurrentStep();
			}
			else if(!passedKey.Equals("esc") && !SavedKey)
			{
				playerController.IsWalking(0);
				AddMissDelegate();
			}

			if (DebugMode)
				Debug.Log("Stairs key :" + newkey + " Typed Key: "+passedKey);
		}
	}

	public void AddMissDelegate()
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
		score.currentScore -= 1;
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
		if (DebugMode)
		{
			var style = new GUIStyle();
			style.fontSize = 24;

			GUILayout.Label("STATE STACK", style);

			foreach (var state in StateMachine.StateStack)
			{
				GUILayout.Label(state.GetType().ToString(), style);
			}
		}
	}

	/// <summary>
	/// ASSIGN THE DIALOGUES IN THE INSPECTOR
	/// event for interacting with the dialogues
	/// </summary>
	/// <param name="eventNumber">the dialogue number we want to play (check inspector)</param>
	public void Interact(int eventNumber)
	{
		StartCoroutine(DialogueMan.Instance.ShowDialog(dialog[eventNumber]));
	}

	// HMMM
	public void PauseDialogue()
	{
		state = GameState.Paused;
	}
	
}
