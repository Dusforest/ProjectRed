using System;
using Framework;
using Unity.VisualScripting;
using UnityEngine;

namespace Elvenwood
{
	public class MagicArrowPoint : AbstractController
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Player"))
			{
				this.SendCommand(new ObtainMagicArrowCommand());
				Destroy(gameObject);
			}
		}
	}
}