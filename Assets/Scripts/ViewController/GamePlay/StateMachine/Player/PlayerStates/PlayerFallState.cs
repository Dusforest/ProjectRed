using Elvenwood;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerState
{
	public PlayerFallState(PlayerController playerController, PlayerStateMachine stateMachine, string aniBoolName) : base(playerController, stateMachine, aniBoolName)
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
		if (playerController.groundCheck.Triggered && Mathf.Abs( playerController.rb.velocity.y) <=0.02 )
		{
			stateMachine.ChangeState(playerController.idleState);
		}
		rb.velocity += Vector2.up * (Physics2D.gravity.y * (playerController.fallMultiplier - 1) * Time.deltaTime);
		playerController.SetVelocity(xInput * playerController.moveSpeed * 0.8f, rb.velocity.y);
	}
}
