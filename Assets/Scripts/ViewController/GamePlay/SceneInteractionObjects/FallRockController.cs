using Elvenwood;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRockController : MonoBehaviour
{
	// Start is called before the first frame update
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
		{
			collision.gameObject.GetComponent<EnemyStatistic>().GetHurt(99999);
		}
	}
}
