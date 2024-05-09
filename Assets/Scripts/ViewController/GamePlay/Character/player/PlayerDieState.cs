using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public class PlayerDieState : PlayerState
	{
		public PlayerDieState(PlayerController playerController, PlayerStateMachine stateMachine, string aniBoolName) : base(playerController, stateMachine, aniBoolName)
		{
		}

		public override void Enter()
		{
			base.Enter();
		}

		public override void Exit()
		{
			base.Exit();
		}

		public override void Update()
		{
			base.Update();
			playerController.SetVelocity();
		}
	}
}

