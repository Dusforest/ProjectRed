using System;
using UnityEngine;

namespace Elvenwood
{
	public class TriggerCheck2D : MonoBehaviour
	{
		[Tooltip("检测目标图层")] public LayerMask targetLayers;

		private Collider2D mCollider2D;

		private void Start()
		{
			mCollider2D = GetComponent<Collider2D>();
		}

		public bool Triggered => mCollider2D.IsTouchingLayers(targetLayers);
	}
}