using Elvenwood;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
	int attackCombo;
	float lastAttackTime;

	public PlayerAttackState(PlayerController playerController, PlayerStateMachine stateMachine, string aniBoolName) : base(playerController, stateMachine, aniBoolName)
	{
	}

	public override void Enter()
	{
		base.Enter();
		if (Time.time - lastAttackTime >= playerController.comboWindow) attackCombo = 0;
		playerController.ani.SetInteger("Combo", attackCombo);
	}

	public override void Exit()
	{
		base.Exit();
		attackCombo = (attackCombo + 1) % 2;
		lastAttackTime = Time.time;
	}

	public override void Update()
	{
		base.Update();
		playerController.SetVelocity();
	}
}
