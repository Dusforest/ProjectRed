using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public class EnemyController : EntityController
	{
		public TriggerCheck2D playerDetect;
		public TriggerCheck2D attackRangeDetect;
		public TriggerCheck2D attackHitCheck;

		public float idleTime;
		public bool isRangeLimit;
		public float rangeLimitDisLeft;
		public float rangeLimitDisRight;
		[HideInInspector] public Vector3 startPoint;

		protected override void Awake()
		{
			base.Awake();
			startPoint = transform.position;
		}

		protected override void Start()
		{
			base.Start();
		}

		protected override void Update()
		{
			base.Update();
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			if (isRangeLimit) Gizmos.DrawLine(transform.position - new Vector3(rangeLimitDisLeft, 0, 0), transform.position + new Vector3(rangeLimitDisRight, 0, 0));
		}

		public bool IsOutofRange(Vector3 curruntPosition)
		{
			if (curruntPosition.x - (startPoint.x + rangeLimitDisRight) > 0 && isFacingR)
			{
				return true;
			}

			else if ((startPoint.x - rangeLimitDisLeft) - curruntPosition.x > 0 && !isFacingR)
			{
				return true;
			}

			return false;
		}
	}
}

