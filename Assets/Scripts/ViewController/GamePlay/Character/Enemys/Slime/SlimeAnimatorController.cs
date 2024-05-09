using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public class SlimeAnimatorController : AbstractController
	{
		SlimeController slimeController;
		private void Awake()
		{
			slimeController = GetComponentInParent<SlimeController>();
		}

		public void AttackHit()
		{
			if (slimeController.attackHitCheck.Triggered)
			{
				PlayerManager.Instance.playerController.GetHurt(slimeController.gameObject.transform.position);
			}
		}

		public void AttackOver()
		{
			slimeController.stateMachine.ChangeState(slimeController.idleState);
		}
	}

}
