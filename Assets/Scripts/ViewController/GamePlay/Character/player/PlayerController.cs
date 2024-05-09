using Framework;
using UnityEngine;
using UnityEngine.Serialization;

namespace Elvenwood
{
	public class PlayerController : EntityController
	{
		public Animator Ani;
		public bool isAlive;
		public bool isInvincible;

		[Tooltip("跳跃初始速度")] public float jumpVelocity;

		public float fallMultiplier = 2.5f;

		#region States

		public PlayerStateMachine stateMachine { get; private set; }

		public PlayerIdleState idleState;

		public PlayerMoveState moveState;

		public PlayerJumpState jumpState;

		public PlayerFallState fallState;

		public PlayerAttackState attackState;

		public PlayerMagicAttackState magicAttackState;

		public PlayerStunnedState stunnedState;

		public PlayerDieState dieState;

		#endregion
		#region Attack
		[Header("Attack Info")]
		[SerializeField] GameObject arrowPrefab;
		[SerializeField] Transform arrowPoolParent;
		public float arrowSpeed;
		public ObjectPool arrowPool;
		public float comboWindow;
		public float invicibleTime;
		#endregion
		#region Skills
		[Header("Skill")]
		public GameObject magicArrowPrefab;
		public float magicArrowSpeed;

		public GameObject clonePrefab;
		public bool isCloneAlreadyExist;
		public float cloneMoveSpeed;
		public float cloneDuration;
		public float cloneDisappearTime;
		public CloneController cloneController;
		#endregion


		protected override void Awake()
		{
			base.Awake();
			ani = Ani;
			arrowPool = new ObjectPool(arrowPrefab, 10, arrowPoolParent);

			stateMachine = new PlayerStateMachine();
			idleState = new PlayerIdleState(this, stateMachine, "IsIdle");
			moveState = new PlayerMoveState(this, stateMachine, "IsMove");
			jumpState = new PlayerJumpState(this, stateMachine, "IsJump");
			fallState = new PlayerFallState(this, stateMachine, "IsJump");
			attackState = new PlayerAttackState(this, stateMachine, "IsAttack");
			magicAttackState = new PlayerMagicAttackState(this, stateMachine, "IsMagicAttack");
			stunnedState = new PlayerStunnedState(this, stateMachine, "IsHurt");
			dieState = new PlayerDieState(this, stateMachine, "IsDie");

			isCloneAlreadyExist = false;
		}

		protected override void Start()
		{
			base.Start();
			GameStart();
		}

		protected override void Update()
		{
			base.Update();
			stateMachine.currentState.Update();
		}

		public void GameStart()
		{
			isAlive = true;
			stateMachine.Init(idleState);
			isInvincible = false;
		}

		#region Skills
		public void UseCloneSkill()
		{
			if (stateMachine.currentState is PlayerGroundState)
			{
				if (!this.GetModel<IPlayerModel>().Data.isCloneReleased)
				{
					this.GetModel<IPlayerModel>().Data.isCloneReleased.Value = true;
					GameObject clone = Instantiate(clonePrefab, transform.position, Quaternion.identity);
					cloneController = clone.GetComponent<CloneController>();
					cloneController.InitClone(cloneMoveSpeed, facingDir, cloneDuration, cloneDisappearTime, magicArrowPrefab, magicArrowSpeed);
				}
				else
				{
					transform.position = cloneController.gameObject.transform.position;
					Destroy(cloneController.gameObject);
					cloneController = null;
					this.GetModel<IPlayerModel>().Data.isCloneReleased.Value = false;
				}
			}
		}

		public void CloneMagicAttack()
		{
			if (!this.GetModel<IPlayerModel>().Data.isCloneReleased) return;
			int yInput = (int)InputManager.Instance.yInput;
			if (yInput == 0) cloneController.MagicAttack(1);
			else cloneController.MagicAttack(yInput * 2);
		}
		#endregion
		#region Damage

		public void GetHurt(Vector3 enemyPosition)
		{
			if (isInvincible) return;
			print("挨揍了");

			if (transform.position.x - enemyPosition.x > 0) SetVelocity(5, 0, true);
			else SetVelocity(-5, 0, true);

			stateMachine.ChangeState(stunnedState);
			isInvincible = true;
			sr.color = Color.yellow;
			this.GetSystem<ITimeSystem>().AddDelayTask(invicibleTime, () =>
			{
				isInvincible = false;
				sr.color = Color.white;
			});

			this.GetModel<IPlayerModel>().Data.curHp.Value -= 1;
			if (this.GetModel<IPlayerModel>().Data.curHp <= 0)
			{
				Die();
			}
		}

		private void Die()
		{
			SetVelocity();
			rb.isKinematic = true;
			this.GetComponent<Collider2D>().enabled = false;
			stateMachine.ChangeState(dieState);
			isAlive = false;
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.layer == 8)
			{
				GetHurt(collision.gameObject.transform.position);
			}
		}
		#endregion

	}
}