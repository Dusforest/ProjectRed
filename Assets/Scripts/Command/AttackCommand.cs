using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public class AttackCommand : AbstractCommand
	{
		public static readonly AttackCommand Single = new AttackCommand();

		protected override void OnExecute()
		{
			var attackSystem = this.GetSystem<IAttackSystem>();

			attackSystem.curAttack.AttackState.Value = AttackState.CoolDown;

			this.GetSystem<ITimeSystem>().AddDelayTask(attackSystem.curAttack.CoolDown, () =>
			{
				attackSystem.curAttack.AttackState.Value = AttackState.Idle;
			});
		}
	}
}

