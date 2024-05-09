using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public class PlayerMagicAttackState : PlayerState
	{
		public float lastYInput;
		public PlayerMagicAttackState(PlayerController playerController, PlayerStateMachine stateMachine, string aniBoolName) : base(playerController, stateMachine, aniBoolName)
		{
		}

		public override void Enter()
		{
			base.Enter();
			lastYInput = InputManager.Instance.yInput;
			if (lastYInput == 0)
			{
				playerController.ani.SetInteger("MagicAttackDir", 1);
				playerController.SetVelocity(-playerController.facingDir * 10, 5, true);
			}
			else
			{
				playerController.ani.SetInteger("MagicAttackDir", (int)lastYInput * 2);
				playerController.SetVelocity();
			}


		}

		public override void Exit()
		{
			base.Exit();
		}

		public override void Update()
		{
			base.Update();
		}
	}
}


