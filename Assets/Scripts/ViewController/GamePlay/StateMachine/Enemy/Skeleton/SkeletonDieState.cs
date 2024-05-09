using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public class SkeletonDieState : EnemyState
	{
		public SkeletonDieState(EnemyController enemyController, EnemyStateMachine stateMachine, string aniBoolName) : base(enemyController, stateMachine, aniBoolName)
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

			enemyController.SetVelocity();
			enemyController.sr.color = new Color(1, 1, 1, enemyController.sr.color.a - Time.deltaTime);
			if (enemyController.sr.color.a <= 0)
			{
				GameObject.Destroy(enemyController.gameObject);
			}
		}
	}
}

