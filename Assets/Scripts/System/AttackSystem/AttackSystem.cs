using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public interface IAttackSystem : ISystem
	{
		AttackInfo curAttack { get; set; }
	}

	public class AttackSystem : AbstractSystem, IAttackSystem
	{
		public AttackInfo curAttack { get; set; }

		protected override void OnInit()
		{
			curAttack = new AttackInfo()
			{
				AttackState = new BindableProperty<AttackState>(AttackState.Idle),
				CoolDown = new BindableProperty<float>(1f)
			};
		}

	}

}

