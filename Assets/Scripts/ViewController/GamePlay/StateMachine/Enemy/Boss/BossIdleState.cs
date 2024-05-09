using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Elvenwood
{
	public class BossIdleState : EnemyState
	{
		public BossIdleState(EnemyController enemyController, EnemyStateMachine stateMachine, string aniBoolName) : base(enemyController, stateMachine, aniBoolName)
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
				if (Random.Range(0, 3) == 0)
				{
					stateMachine.ChangeState((enemyController as BossController).skillState);
				}
				else
				{
					stateMachine.ChangeState((enemyController as BossController).moveState);
				}
			}

		}

		public override void Exit()
		{
			base.Exit();
		}

	}
}

