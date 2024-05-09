using Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Elvenwood
{
	public class EnemyStatistic : AbstractController
	{
		public int maxHp;
		[SerializeField] protected int curHp;
		protected EnemyStateMachine stateMachine;

		protected virtual void Awake()
		{
			curHp = maxHp;
		}

		public virtual void GetHurt(int damagePoint)
		{
			if (curHp > 0) curHp -= damagePoint;
		}
	}

}
