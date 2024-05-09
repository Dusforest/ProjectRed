using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public class SkeletonAnimatorController : AbstractController
	{
		SkeletonController skeletonController;
		private void Awake()
		{
			skeletonController = GetComponentInParent<SkeletonController>();
		}

		public void AttackHit()
		{
			if (skeletonController.attackHitCheck.Triggered)
			{
				PlayerManager.Instance.playerController.GetHurt(skeletonController.gameObject.transform.position);
			}
		}

		public void AttackOver()
		{
			skeletonController.stateMachine.ChangeState(skeletonController.idleState);
		}
	}

}
