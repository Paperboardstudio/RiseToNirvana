using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedGameState : State<KeyboardInputs>
{
	public static PausedGameState i { get; private set; }
	private void Awake()
	{
		i = this;
	}
	KeyboardInputs gc;
	public override void Enter(KeyboardInputs owner)
	{
		gc = owner;
	}
	public override void Execute()
	{
		//KeyboardInputs.i.HandleUpdate();

		if (Input.GetKeyDown(KeyCode.Return))
		{
			gc.StateMachine.Push(GameMenuState.i);
		}
	}
}
