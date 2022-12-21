using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuState : State<KeyboardInputs>
{
    public static GameMenuState i { get; private set; }
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
        if (Input.GetKeyDown(KeyCode.X))
        {
            gc.StateMachine.Pop();
        }
    }
}
