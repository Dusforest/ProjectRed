using Elvenwood;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
	public PlayerIdleState(PlayerController playerController, PlayerStateMachine stateMachine, string aniBoolName) : base(playerController, stateMachine, aniBoolName)
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
		if (xInput != 0)
		{
			if (playerController.wallCheck.Triggered)
			{
				if ((xInput == -1 && playerController.isFacingR) || (xInput == 1 && !playerController.isFacingR))
				{
					stateMachine.ChangeState(playerController.moveState);
				}
			}
			else
			{
				stateMachine.ChangeState(playerController.moveState);
			}

		}
	}
}

