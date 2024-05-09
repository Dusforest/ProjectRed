using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public class SkeletonStatistic : EnemyStatistic
	{
		SkeletonController skeletonController;
		protected override void Awake()
		{
			base.Awake();
			skeletonController = GetComponent<SkeletonController>();
		}

		public override void GetHurt(int damagePoint)
		{
			base.GetHurt(damagePoint);
			if (curHp <= 0)
			{
				skeletonController.stateMachine.ChangeState(skeletonController.dieState);
			}
		}
	}
}

