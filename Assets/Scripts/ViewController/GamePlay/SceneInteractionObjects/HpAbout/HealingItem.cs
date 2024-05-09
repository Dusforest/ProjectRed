using Framework;
using UnityEngine;
using UnityEngine.Serialization;

namespace Elvenwood.HealingItem
{
	public class HealingItem : AbstractController
	{
		[Tooltip("治疗量")] int healing = 1;
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Player"))
			{
				this.SendCommand(new HealingPlayerCommand(healing));
				Destroy(gameObject);
			}
		}
	}
}