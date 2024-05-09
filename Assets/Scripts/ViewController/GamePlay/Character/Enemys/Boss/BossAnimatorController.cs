using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public class BossAnimatorController : AbstractController
	{
		BossController bossController;
		private void Awake()
		{
			bossController = GetComponentInParent<BossController>();
		}

		public void AttackHit()
		{
			bossController.SetVelocity();
			if (bossController.attackHitCheck.Triggered)
			{
				PlayerManager.Instance.playerController.GetHurt(bossController.gameObject.transform.position);
			}
		}

		public void AttackOver()
		{
			bossController.stateMachine.ChangeState(bossController.idleState);
		}

		public void SkillHit()
		{
			print("释放技能");
			CreateSkillPrefab();
			this.GetSystem<ITimeSystem>().AddDelayTask(0.5f, CreateSkillPrefab);
			this.GetSystem<ITimeSystem>().AddDelayTask(1f, CreateSkillPrefab);

		}

		public void CreateSkillPrefab()
		{
			print("释放技能预制体");
			GameObject obj = Instantiate(bossController.bossSkillAttackPrefab);
			obj.transform.position = PlayerManager.Instance.playerController.transform.position + new Vector3(0, 4);
		}
	}
}
