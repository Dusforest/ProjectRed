using Framework;
using System;
using System.Collections;
using UnityEngine;

namespace Elvenwood
{
	public class ArrowController : AbstractController
	{
		public bool isMagicArrow;
		[HideInInspector] public Rigidbody2D rb;

		public ParticleSystem destroyEffect;


		private void Awake()
		{
			rb = GetComponent<Rigidbody2D>();
		}
		private void Start()
		{
			if (isMagicArrow)
			{
				this.GetSystem<ITimeSystem>().AddDelayTask(1.5f, () =>
				{
#warning 这里有一个频繁使用的try-catch
					try
					{
						Destroy(gameObject);
					}
					catch (Exception e)
					{
						if (e is Exception)
						{

						}
					}
				});
			}
		}

		private void Update()
		{
			transform.right = rb.velocity;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
			{
				if (isMagicArrow)
				{
					print("魔法剑打到怪物了,伤害为" + (int)(this.GetModel<IPlayerModel>().Data.atk * 3f));
					collision.gameObject.GetComponent<EnemyStatistic>().GetHurt((int)(this.GetModel<IPlayerModel>().Data.atk * 3f));

					// destroyEffect.Play();
					Destroy(gameObject);
				}
				else
				{
					print("打到怪物了");
					collision.gameObject.GetComponent<EnemyStatistic>().GetHurt(this.GetModel<IPlayerModel>().Data.atk);
					gameObject.SetActive(false);
				}
			}
			else if (((1 << collision.gameObject.layer) & LayerMask.GetMask("Ground", "Mirror")) != 0)
			{
				gameObject.SetActive(false);
			}
		}

		void UnActiveSelf(GameObject obj)
		{
			obj.SetActive(false);
			print("unActive");
		}

		/// <summary>
		/// 初始化箭矢发射速度
		/// </summary>
		/// <param name="shootDir">发射方向，1右-1左2上-2下</param>
		public void InitArrowSpeed(int shootDir, float shootSpeed)
		{
			if (!isMagicArrow)
			{
				switch (shootDir)
				{
					case 1:
						rb.velocity = new Vector2(shootSpeed, 3);
						break;
					case -1:
						rb.velocity = new Vector2(-shootSpeed, 3);
						break;
					case 2:
						rb.velocity = new Vector2(0, shootSpeed);
						break;
					case -2:
						rb.velocity = new Vector2(0, -shootSpeed);
						break;
					default:
						Debug.LogWarning("Unknown Direction");
						break;
				}
			}
			else
			{
				switch (shootDir)
				{
					case 1:
						rb.velocity = new Vector2(shootSpeed, 0);
						break;
					case -1:
						rb.velocity = new Vector2(-shootSpeed, 0);
						break;
					case 2:
						rb.velocity = new Vector2(0, shootSpeed);
						break;
					case -2:
						rb.velocity = new Vector2(0, -shootSpeed);
						break;
					default:
						Debug.LogWarning("Unknown Direction");
						break;
				}
			}

		}
	}
}