using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public class EnemyState
	{
		protected EnemyController enemyController;
		protected EnemyStateMachine stateMachine;
		protected string aniBoolName;
		protected Rigidbody2D rb;

		protected float stateTimer;

		public EnemyState(EnemyController enemyController, EnemyStateMachine stateMachine, string aniBoolName)
		{
			this.enemyController = enemyController;
			this.stateMachine = stateMachine;
			this.aniBoolName = aniBoolName;
			rb = enemyController.rb;
		}

		public virtual void Enter()
		{
			enemyController.ani.SetBool(aniBoolName, true);
		}

		public virtual void Update()
		{
			if (stateTimer > 0) stateTimer -= Time.deltaTime;
		}

		public virtual void Exit()
		{
			enemyController.ani.SetBool(aniBoolName, false);
		}
	}
}

