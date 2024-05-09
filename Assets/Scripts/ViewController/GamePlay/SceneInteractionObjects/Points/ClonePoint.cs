using Framework;
using UnityEngine;

namespace Elvenwood
{
	public class ClonePoint : AbstractController
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Player"))
			{
				this.SendCommand(new ObtainCloneSkillCommand());
				Destroy(gameObject);
			}
		}
	}
}