using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public class BossSkillAttackAnimatorController : AbstractController
	{
		public TriggerCheck2D attackHitCheck;

		private void Awake()
		{
			attackHitCheck = GetComponentInChildren<TriggerCheck2D>();
		}

		public void AttackHit()
		{
			if (attackHitCheck.Triggered)
			{
				PlayerManager.Instance.playerController.GetHurt(transform.position);
			}
		}

		public void AttackOver()
		{
			Destroy(gameObject);
		}
	}

}
