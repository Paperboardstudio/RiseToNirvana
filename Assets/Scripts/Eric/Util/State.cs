using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<T> : MonoBehaviour
{
    // When it enters the state
    public virtual void Enter(T owner) { }
    // Execute logic of the state
    public virtual void Execute() { }
    // When we exit a state
    public virtual void Exit() { }

}
