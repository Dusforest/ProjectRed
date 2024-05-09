using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public class InputManager : AbstractController
	{
		PlayerController playerController => PlayerManager.Instance.playerController;

		PlayerStateMachine stateMachine => playerController.stateMachine;

		public static InputManager Instance { get; private set; }

		private List<InputEntity> inputs;
		private List<HoldEntity> holdInputs;

		public float xInput { get; private set; }
		public float yInput { get; private set; }


		private void Awake()
		{
			if (Instance != null)
				Destroy(Instance.gameObject);
			Instance = this;
		}

		private void Start()
		{
			inputs = new List<InputEntity>();
			holdInputs = new List<HoldEntity>();

			inputs.Add(Jump());
			inputs.Add(Attack());
			inputs.Add(MagicAttack());
			inputs.Add(CloneSkill());
			inputs.Add(OpenMenu());
		}

		private void Update()
		{
			xInput = Input.GetAxisRaw("Horizontal");
			yInput = Input.GetAxisRaw("Vertical");

			for (int i = 0; i < inputs.Count; i++)
			{
				if (Input.GetKeyDown(inputs[i].keyCode))
				{
					inputs[i].KeyAction();
				}
			}
			for (int i = 0; i < holdInputs.Count; i++)
			{
				if (Input.GetKey(holdInputs[i].keyCode))
				{
					holdInputs[i].curruntHoldTime += Time.deltaTime;
					if (holdInputs[i].curruntHoldTime > holdInputs[i].needHoldTime)
					{
						holdInputs[i].KeyAction();
					}
				}
				else
				{
					holdInputs[i].curruntHoldTime = 0;
				}
			}
		}

		InputEntity Jump() => new InputEntity("Jump", KeyCode.K, () =>
		{
			print(stateMachine.currentState);
			if (stateMachine.currentState is PlayerGroundState)
			{
				playerController.jumpState.canStartJump = false;
				stateMachine.ChangeState(playerController.jumpState);
			}
		});

		InputEntity Attack() => new InputEntity("Attack", KeyCode.J, () =>
		{

			if (stateMachine.currentState is PlayerGroundState)
			{
				stateMachine.ChangeState(playerController.attackState);
			}

		});

		InputEntity MagicAttack() => new InputEntity("MagicAttack", KeyCode.U, () =>
		{
			if (stateMachine.currentState is PlayerGroundState && this.GetModel<IPlayerModel>().Data.hasMagicArrow)
			{
				stateMachine.ChangeState(playerController.magicAttackState);
				playerController.CloneMagicAttack();
			}
		});

		InputEntity CloneSkill() => new InputEntity("CloneSkill", KeyCode.I, () =>
		{
			if (this.GetModel<IPlayerModel>().Data.hasCloneSkill)
				playerController.UseCloneSkill();
		});

		InputEntity OpenMenu() => new InputEntity("OpenMenu", KeyCode.Escape, () =>
		{
			if (UIManager.Instance.optionUI.activeSelf)
			{
				UIManager.Instance.OnHideOption();
			}
			else
			{
				UIManager.Instance.OnShowOption();
			}

		});
	}
}

