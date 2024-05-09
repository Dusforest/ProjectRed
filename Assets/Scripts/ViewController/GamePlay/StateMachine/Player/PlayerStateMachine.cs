using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
	public PlayerState currentState { get; private set; }

	public void Init(PlayerState startState)
	{
		currentState = startState;
		currentState.Enter();
	}

	public void ChangeState(PlayerState newState)
	{
		currentState.Exit();
		currentState = newState;
		currentState.Enter();
	}

	public void CheckState()
	{
		Debug.Log(currentState);
	}
}
