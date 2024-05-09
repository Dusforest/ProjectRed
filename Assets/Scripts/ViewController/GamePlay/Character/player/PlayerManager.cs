using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public class PlayerManager : AbstractController
	{
		public static PlayerManager Instance { get; private set; }

		public PlayerController playerController;

		private void Awake()
		{
			if (Instance != null)
				Destroy(Instance.gameObject);
			Instance = this;
		}
	}
}

