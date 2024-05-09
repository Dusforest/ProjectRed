using Elvenwood;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
	public PlayerMoveState(PlayerController playerController, PlayerStateMachine stateMachine, string aniBoolName) : base(playerController, stateMachine, aniBoolName)
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

		playerController.SetVelocity(xInput * playerController.moveSpeed, rb.velocity.y);
		if (xInput == 0 || playerController.wallCheck.Triggered)
		{
			stateMachine.ChangeState(playerController.idleState);
		}
	}
}
