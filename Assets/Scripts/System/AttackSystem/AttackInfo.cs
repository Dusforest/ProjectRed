using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public enum AttackState
	{
		Idle,
		Shooting,
		CoolDown
	}
	public class AttackInfo
	{

		public BindableProperty<AttackState> AttackState;

		public BindableProperty<float> CoolDown;

	}
}

