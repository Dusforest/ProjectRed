using Elvenwood;
using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public class CloneAnimatorController : AbstractController
	{
		CloneController cloneController;
		Animator ani;
		int dir;

		private void Awake()
		{
			cloneController = GetComponentInParent<CloneController>();
			ani = GetComponent<Animator>();
		}


		public void MagicArrowShoot()
		{
			dir = ani.GetInteger("MagicAttackDir");

			if (dir == 2)
			{
				GameObject obj = Instantiate(cloneController.magicArrowPrefab, transform.position + new Vector3(0.2f, 0, 0) * cloneController.facingDir, Quaternion.Euler(0, 0, 90));
				obj.GetComponent<ArrowController>().InitArrowSpeed(dir, cloneController.magicArrowSpeed);
			}
			else if (dir == -2)
			{
				GameObject obj = Instantiate(cloneController.magicArrowPrefab, transform.position, Quaternion.Euler(0, 0, -90));
				obj.GetComponent<ArrowController>().InitArrowSpeed(dir, cloneController.magicArrowSpeed);
			}
			else
			{
				GameObject obj = Instantiate(cloneController.magicArrowPrefab, transform.position - new Vector3(0, 1.1f, 0), Quaternion.identity);
				obj.GetComponent<ArrowController>().InitArrowSpeed(cloneController.facingDir, cloneController.magicArrowSpeed);
			}
			//GameObject magicArrow = Instantiate(cloneController.magicArrowPrefab, transform.position, Quaternion.identity);
			//magicArrow.GetComponent<ArrowController>().InitArrowSpeed(ani.GetInteger("MagicAttackDir"), cloneController.magicArrowSpeed);
			this.GetModel<IPlayerModel>().Data.isCloneReleased.Value = false;
			cloneController.canRemoveInDisappear = false;
		}

		public void AttackOver()
		{
			cloneController.CloneDisappear();
		}
	}

}
