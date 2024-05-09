using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Elvenwood
{
	public class SlimeController : EnemyController
	{
		#region State
		public EnemyStateMachine stateMachine;

		public SlimeIdleState idleState;

		public SlimeMoveState moveState;

		public SlimeBattleState battleState;

		public SlimeAttackState attackState;

		public SlimeDieState dieState;
		#endregion

		protected override void Awake()
		{
			base.Awake();

			sr.GetComponent<SpriteRenderer>();

			stateMachine = new EnemyStateMachine();
			idleState = new SlimeIdleState(this, stateMachine, "IsIdle");
			moveState = new SlimeMoveState(this, stateMachine, "IsMove");
			battleState = new SlimeBattleState(this, stateMachine, "IsMove");
			attackState = new SlimeAttackState(this, stateMachine, "IsAttack");
			dieState = new SlimeDieState(this, stateMachine, "IsDie");
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

