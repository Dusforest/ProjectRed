using UnityEngine;

namespace Elvenwood
{
	public class SlimeGroundState : EnemyState
	{
		public SlimeGroundState(EnemyController enemyController, EnemyStateMachine stateMachine, string aniBoolName) : base(enemyController, stateMachine, aniBoolName)
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
			//	if (enemyController.IsPlayerDetected() && enemyController.IsGroundDetected() && CanAttack())
			//		stateMachine.ChangeState((enemyController as SlimeLogic).battleState);
			//}
		}
	}
}

