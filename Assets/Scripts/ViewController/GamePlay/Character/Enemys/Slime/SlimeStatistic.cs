using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public class SlimeStatistic : EnemyStatistic
	{
		SlimeController slimeController;
		protected override void Awake()
		{
			base.Awake();
			slimeController = GetComponent<SlimeController>();
		}

		public override void GetHurt(int damagePoint)
		{
			base.GetHurt(damagePoint);
			if (curHp <= 0)
			{
				slimeController.stateMachine.ChangeState(slimeController.dieState);
			}
		}
	}
}

