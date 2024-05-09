using Elvenwood;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
	public bool canStartJump;
	public PlayerJumpState(PlayerController playerController, PlayerStateMachine stateMachine, string aniBoolName) : base(playerController, stateMachine, aniBoolName)
	{
		canStartJump = false;
	}

	public override void Enter()
	{
		base.Enter();
		playerController.SetVelocity();
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void Update()
	{
		base.Update();

		if (canStartJump)
		{
			Debug.Log("我跳了");
			playerController.SetVelocity(rb.velocity.x, playerController.jumpVelocity);
			canStartJump = false;
		}

		if (rb.velocity.y < 0)
			stateMachine.ChangeState(playerController.fallState);
		if (!playerController.groundCheck.Triggered)
			playerController.SetVelocity(xInput * playerController.moveSpeed * 0.8f, rb.velocity.y);
	}
}
