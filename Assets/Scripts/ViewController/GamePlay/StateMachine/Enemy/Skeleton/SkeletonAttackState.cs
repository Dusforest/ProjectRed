using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public class SkeletonAttackState : EnemyState
	{
		public SkeletonAttackState(EnemyController enemyController, EnemyStateMachine stateMachine, string aniBoolName) : base(enemyController, stateMachine, aniBoolName)
		{
		}

		public override void Enter()
		{
			enemyController.SetVelocity(5 * enemyController.facingDir);
			base.Enter();
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


