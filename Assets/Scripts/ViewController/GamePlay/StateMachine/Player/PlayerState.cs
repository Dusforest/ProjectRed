using UnityEngine;
using Elvenwood;

public class PlayerState
{
	protected PlayerController playerController;
	protected PlayerStateMachine stateMachine;
	protected string aniBoolName;
	protected Rigidbody2D rb;
	public float xInput => InputManager.Instance.xInput;

	protected float stateTimer;

	public PlayerState(PlayerController playerController, PlayerStateMachine stateMachine, string aniBoolName)
	{
		this.playerController = playerController;
		this.stateMachine = stateMachine;
		this.aniBoolName = aniBoolName;
		rb = playerController.rb;
	}

	public virtual void Enter()
	{
		playerController.ani.SetBool(aniBoolName, true);
	}

	public virtual void Update()
	{
		if (stateTimer > 0) stateTimer -= Time.deltaTime;
		playerController.ani.SetFloat("yVelocity", rb.velocity.y);
	}

	public virtual void Exit()
	{
		playerController.ani.SetBool(aniBoolName, false);
	}

}
