using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Elvenwood
{
	public class SkeletonController : EnemyController
	{
		#region State
		public EnemyStateMachine stateMachine;

		public SkeletonIdleState idleState;

		public SkeletonMoveState moveState;

		public SkeletonBattleState battleState;

		public SkeletonAttackState attackState;

		public SkeletonDieState dieState;
		#endregion

		protected override void Awake()
		{
			base.Awake();

			sr.GetComponent<SpriteRenderer>();

			stateMachine = new EnemyStateMachine();
			idleState = new SkeletonIdleState(this, stateMachine, "IsIdle");
			moveState = new SkeletonMoveState(this, stateMachine, "IsMove");
			battleState = new SkeletonBattleState(this, stateMachine, "IsMove");
			attackState = new SkeletonAttackState(this, stateMachine, "IsAttack");
			dieState = new SkeletonDieState(this, stateMachine, "IsDie");
		}

		protected override void Start()
		{
			base.Start();
			stateMachine.Init(idleState);
		}

		protected override void Update()
		{
			base.Update();
			stateMachine.currentState.Update();
		}
	}
}

