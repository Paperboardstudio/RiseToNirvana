using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class KeyboardInputs : MonoBehaviour
{
	public PlayerInput PlayerInputs;
	RiseToNirvana Controls;
	string oldcontrol;
	public void Awake()
	{
		Controls = new RiseToNirvana();
	}
	public void OnEnable()
	{
		Controls.Enable();
	
		Controls.Player.Newaction.performed += ctx => CheckKboardInputs();
	}
	public Staircase stair;

	public void OnDisable()
	{
		Controls.Disable();
	}
	/// <summary>
	/// Check for inputaction according to your current inputsystem.
	/// </summary>
	void CheckKboardInputs()
	{
		InputSystem.onAnyButtonPress
			.CallOnce(ctrl => oldcontrol = ctrl.displayName.ToString().ToLower());
		//Debug.Log(Keyboard.current[(Key)16].wasPressedThisFrame);
		if (stair == null)
		{
			stair = FindObjectOfType<Staircase>();
		}

		
		string newkey = stair.GetStep();
		Debug.Log(newkey);
		Debug.Log(oldcontrol);
		if (oldcontrol.Equals(newkey))
		{
			stair.RegenerateCharacters();
			stair.DestroyCurrentStep();
		}
		/*
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
	*/
	}
}