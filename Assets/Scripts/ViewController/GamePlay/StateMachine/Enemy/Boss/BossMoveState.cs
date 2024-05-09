using Elvenwood;
using System.Diagnostics;
using Unity.VisualScripting.FullSerializer;


public class BossMoveState : SlimeGroundState
{
	public BossMoveState(EnemyController enemyController, EnemyStateMachine stateMachine, string aniBoolName) : base(enemyController, stateMachine, aniBoolName)
	{
	}

	public override void Enter()
	{
		base.Enter();
	}
	public override void Update()
	{
		base.Update();
		enemyController.SetVelocity(enemyController.moveSpeed * enemyController.facingDir, enemyController.rb.velocity.y);

		if (!enemyController.groundCheck.Triggered || enemyController.wallCheck.Triggered)
		{
			enemyController.Flip();
			stateMachine.ChangeState((enemyController as BossController).idleState);
		}

		if (enemyController.playerDetect.Triggered)
		{
			stateMachine.ChangeState((enemyController as BossController).battleState);
		}
	}

	public override void Exit()
	{
		base.Exit();
		enemyController.ani.speed = 1;
	}

}

