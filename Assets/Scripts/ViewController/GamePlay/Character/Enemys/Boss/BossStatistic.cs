using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public class BossStatistic : EnemyStatistic
	{
		BossController bossController;
		protected override void Awake()
		{
			base.Awake();
			bossController = GetComponent<BossController>();
		}

		public override void GetHurt(int damagePoint)
		{
			base.GetHurt(damagePoint);
			if (curHp <= 0)
			{
				bossController.stateMachine.ChangeState(bossController.dieState);
			}
		}
	}
}
