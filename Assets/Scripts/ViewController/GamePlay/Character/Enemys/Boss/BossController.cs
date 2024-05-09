using Elvenwood;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public class BossController : EnemyController
	{
		#region State
		public EnemyStateMachine stateMachine;

		public BossIdleState idleState;

		public BossMoveState moveState;

		public BossBattleState battleState;

		public BossAttackState attackState;

		public BossSkillkState skillState;

		public BossDieState dieState;
		#endregion

		public GameObject bossSkillAttackPrefab;

		protected override void Awake()
		{
			base.Awake();

			sr.GetComponent<SpriteRenderer>();

			stateMachine = new EnemyStateMachine();
			idleState = new BossIdleState(this, stateMachine, "IsIdle");
			moveState = new BossMoveState(this, stateMachine, "IsMove");
			battleState = new BossBattleState(this, stateMachine, "IsMove");
			attackState = new BossAttackState(this, stateMachine, "IsAttack");
			skillState = new BossSkillkState(this, stateMachine, "IsSkill");
			dieState = new BossDieState(this, stateMachine, "IsDie");
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

