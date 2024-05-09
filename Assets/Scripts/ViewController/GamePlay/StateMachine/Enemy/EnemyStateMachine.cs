using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public class EnemyStateMachine
	{
		public EnemyState currentState { get; private set; }

		public void Init(EnemyState startState)
		{
			currentState = startState;
			currentState.Enter();
		}

		public void ChangeState(EnemyState newState)
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
}

