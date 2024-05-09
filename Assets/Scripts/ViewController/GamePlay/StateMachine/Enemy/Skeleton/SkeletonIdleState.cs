using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Elvenwood
{
	public class SkeletonIdleState : EnemyState
	{
		public SkeletonIdleState(EnemyController enemyController, EnemyStateMachine stateMachine, string aniBoolName) : base(enemyController, stateMachine, aniBoolName)
		{
		}

		public override void Enter()
		{
			base.Enter();
			stateTimer = enemyController.idleTime;
			enemyController.SetVelocity();
		}
		public override void Update()
		{
			base.Update();
			if (stateTimer <= 0)
			{
				stateMachine.ChangeState((enemyController as SkeletonController).moveState);
			}

		}

		public override void Exit()
		{
			base.Exit();
		}

	}
}

