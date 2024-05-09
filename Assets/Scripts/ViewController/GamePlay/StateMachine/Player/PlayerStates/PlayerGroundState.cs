using Elvenwood;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
	public PlayerGroundState(PlayerController playerController, PlayerStateMachine stateMachine, string aniBoolName) : base(playerController, stateMachine, aniBoolName)
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
		if (!playerController.groundCheck.Triggered)
			stateMachine.ChangeState(playerController.fallState);
	}
}
