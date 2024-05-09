using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public class BossRoomTrigger : MonoBehaviour
	{
		[SerializeField] GameObject boss;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
			{
				boss.SetActive(true);
				Destroy(gameObject);
			}
		}
	}

}
