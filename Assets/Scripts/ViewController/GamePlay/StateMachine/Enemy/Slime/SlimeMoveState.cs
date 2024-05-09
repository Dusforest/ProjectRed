using Elvenwood;
using System.Diagnostics;
using Unity.VisualScripting.FullSerializer;


public class SlimeMoveState : SlimeGroundState
{
	public SlimeMoveState(EnemyController enemyController, EnemyStateMachine stateMachine, string aniBoolName) : base(enemyController, stateMachine, aniBoolName)
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
			stateMachine.ChangeState((enemyController as SlimeController).idleState);
		}

		if (enemyController.isRangeLimit && enemyController.IsOutofRange(enemyController.transform.position))
		{
			enemyController.Flip();
			stateMachine.ChangeState((enemyController as SkeletonController).idleState);
		}

		if (enemyController.playerDetect.Triggered)
		{
			stateMachine.ChangeState((enemyController as SlimeController).battleState);
		}
	}

	public override void Exit()
	{
		base.Exit();
		enemyController.ani.speed = 1;
	}

}

