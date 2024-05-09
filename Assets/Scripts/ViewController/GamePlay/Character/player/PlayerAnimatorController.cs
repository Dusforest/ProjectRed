using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public class PlayerAnimatorController : AbstractController
	{
		PlayerController playerController;

		private void Start()
		{
			playerController = PlayerManager.Instance.playerController;
		}

		public void ArrowShoot()
		{
			GameObject arrow = playerController.arrowPool.GetObject();
			arrow.transform.position = playerController.transform.position + new Vector3(0, 2f);
			arrow.SetActive(true);
			arrow.GetComponent<ArrowController>().InitArrowSpeed(playerController.facingDir, playerController.arrowSpeed);
		}

		public void ArrowShoot2()
		{
			GameObject arrow = playerController.arrowPool.GetObject();
			arrow.transform.position = playerController.transform.position + new Vector3(0, 1.5f);
			arrow.SetActive(true);
			arrow.GetComponent<ArrowController>().InitArrowSpeed(playerController.facingDir, playerController.arrowSpeed);
		}

		public void MagicArrowShoot()
		{
			int lastYInput = (int)(playerController.stateMachine.currentState as PlayerMagicAttackState).lastYInput;

			if (lastYInput == 1)
			{
				GameObject magicArrow = Instantiate(playerController.magicArrowPrefab, transform.position + new Vector3(0.2f, 0, 0) * playerController.facingDir, Quaternion.Euler(0, 0, 90));
				magicArrow.GetComponent<ArrowController>().InitArrowSpeed(lastYInput * 2, playerController.magicArrowSpeed);
			}
			else if (lastYInput == -1)
			{
				GameObject magicArrow = Instantiate(playerController.magicArrowPrefab, transform.position, Quaternion.Euler(0, 0, -90));
				magicArrow.GetComponent<ArrowController>().InitArrowSpeed(lastYInput * 2, playerController.magicArrowSpeed);
			}
			else
			{
				GameObject magicArrow = Instantiate(playerController.magicArrowPrefab, transform.position - new Vector3(0, 1.1f, 0), Quaternion.identity);
				magicArrow.GetComponent<ArrowController>().InitArrowSpeed(playerController.facingDir, playerController.magicArrowSpeed);
			}
		}



		public void AttackOver()
		{
			playerController.stateMachine.ChangeState(playerController.idleState);
		}

		public void StartJump()
		{
			playerController.jumpState.canStartJump = true;
		}

		public void Reborn()
		{
			playerController.isAlive = true;
			playerController.rb.isKinematic = false;
			playerController.GetComponent<Collider2D>().enabled = true;
			playerController.stateMachine.ChangeState(playerController.idleState);
			this.GetModel<IPlayerModel>().Data.curHp = this.GetModel<IPlayerModel>().Data.maxHp;
		}
	}
}


