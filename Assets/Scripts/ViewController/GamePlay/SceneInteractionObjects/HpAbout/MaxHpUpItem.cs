using Framework;
using UnityEngine;

namespace Elvenwood.HealingItem
{
	public class MaxHpUpItem : AbstractController
	{
		[Tooltip("治疗量")]
		public int healing;
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Player"))
			{
				this.SendCommand(new MaxHpUpCommand());
				Destroy(gameObject);
			}
		}
	}
}