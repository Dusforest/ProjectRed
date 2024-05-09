using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Elvenwood
{
	public class EntityController : AbstractController
	{
		[Header("Entity")]
		[Tooltip("角色刚体"), HideInInspector] public Rigidbody2D rb;
		[Tooltip("动画"), HideInInspector] public Animator ani;
		[Tooltip("精灵"), HideInInspector] public SpriteRenderer sr;

		[Tooltip("地面检测器")] public TriggerCheck2D groundCheck;
		[Tooltip("墙壁检测器")] public TriggerCheck2D wallCheck;

		[Tooltip("角色移动速度")] public float moveSpeed;

		public int facingDir;
		public bool isFacingR;

		protected virtual void Awake()
		{
			facingDir = 1;
			isFacingR = true;
			rb = GetComponent<Rigidbody2D>();
			ani = GetComponentInChildren<Animator>();
			sr = GetComponentInChildren<SpriteRenderer>();
		}

		// Start is called before the first frame update
		protected virtual void Start()
		{

		}

		// Update is called once per frame
		protected virtual void Update()
		{

		}

		public virtual void SetVelocity(float xVelocity = 0, float yVelocity = 0, bool flipUnavailable = false)
		{
			rb.velocity = new Vector2(xVelocity, yVelocity);
			if (!flipUnavailable)
				FlipController();
		}

		public virtual void FlipController()
		{
			if (isFacingR && rb.velocity.x < 0 || !isFacingR && rb.velocity.x > 0)
				Flip();
		}

		public virtual void Flip()
		{
			facingDir = facingDir * -1;
			isFacingR = !isFacingR;
			transform.Rotate(0, 180, 0);
		}
	}
}

