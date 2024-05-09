using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public class CloneController : AbstractController
	{
		Rigidbody2D rb;
		[SerializeField] Animator ani;
		SpriteRenderer[] sr;
		float cloneMoveSpeed;
		float cloneDisappearTime;
		[HideInInspector] public int facingDir;


		bool isMoving; //当使用魔法箭技能时设置为false使得移动速度变为0
		bool isDisappearing;  //当分身寿命结束开始消失时设置为true

		/*放完魔法箭的分身其实已经不算在分身内，此时它也消失但是不希望在消失时把PlayerModule里的数据做更改
		 * 此时把canRemoveInDisAppear设置为true即可。
		 */
		[HideInInspector] public bool canRemoveInDisappear;

		[HideInInspector] public GameObject magicArrowPrefab;
		[HideInInspector] public float magicArrowSpeed;

		private void Awake()
		{
			rb = GetComponentInChildren<Rigidbody2D>();
			ani = GetComponentInChildren<Animator>();
			sr = GetComponentsInChildren<SpriteRenderer>();
		}

		// Update is called once per frame
		void Update()
		{
			if (isDisappearing)
			{
				for (int i = 0; i < sr.Length; i++)
				{
					sr[i].color = new Color(1, 1, 1, sr[i].color.a - (Time.deltaTime * cloneDisappearTime));
				}

				if (sr[0].color.a <= 0)
				{
					if (canRemoveInDisappear) this.GetModel<IPlayerModel>().Data.isCloneReleased.Value = false;
					Destroy(gameObject);
				}
			}

			if (isMoving) rb.velocity = new Vector2(cloneMoveSpeed * facingDir, rb.velocity.y);
		}

		public void InitClone(float cloneMoveSpeed, int facingDir, float cloneDuration, float cloneDisappearTime,
			GameObject magicArrowPrefab, float magicArrowSpeed)
		{

			this.cloneMoveSpeed = cloneMoveSpeed;
			this.facingDir = facingDir;
			this.GetSystem<ITimeSystem>().AddDelayTask(cloneDuration, CloneDisappear);
			this.cloneDisappearTime = cloneDisappearTime;
			this.magicArrowPrefab = magicArrowPrefab;
			this.magicArrowSpeed = magicArrowSpeed;

			isMoving = true;
			isDisappearing = false;
			canRemoveInDisappear = true;
			ani.SetBool("IsMove", true);
			if (facingDir == -1) transform.Rotate(0, 180, 0);
		}

		public void CloneDisappear()
		{
			isDisappearing = true;
		}

		public void MagicAttack(int attackDir)
		{
			isMoving = false;
			ani.SetBool("IsMove", false);
			ani.SetBool("IsMagicAttack", true);
			ani.SetInteger("MagicAttackDir", attackDir);
			if (attackDir == 1) rb.velocity = new Vector2(-facingDir * 10, 5);
			else rb.velocity = Vector2.zero;
		}
	}
}

