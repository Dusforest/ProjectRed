using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public class SkeletonBattleState : EnemyState
	{
		public SkeletonBattleState(EnemyController enemyController, EnemyStateMachine stateMachine, string aniBoolName) : base(enemyController, stateMachine, aniBoolName)
		{
		}

		public override void Enter()
		{
			base.Enter();
			enemyController.ani.speed = 1.5f;
		}

		public override void Exit()
		{
			base.Exit();
			enemyController.ani.speed = 1;
		}

		public override void Update()
		{
			base.Update();
			enemyController.SetVelocity(enemyController.moveSpeed * enemyController.facingDir * 1.5f, enemyController.rb.velocity.y);
			if (!enemyController.playerDetect.Triggered)
			{
				stateMachine.ChangeState((enemyController as SkeletonController).idleState);
			}

			if (enemyController.isRangeLimit && enemyController.IsOutofRange(enemyController.transform.position))
			{
				enemyController.Flip();
				stateMachine.ChangeState((enemyController as SkeletonController).idleState);
			}

			if (enemyController.attackRangeDetect.Triggered)
			{
				stateMachine.ChangeState((enemyController as SkeletonController).attackState);
			}
		}
	}
}

