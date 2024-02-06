using UnityEngine;

public abstract class PlayerState
{
	protected PhysicsMovement _physicsMovement;
	protected PlayerStateMachine _playerStateMachine;

	protected float _horizontalInput;
	protected float _verticalInput;

	public PlayerState(PhysicsMovement physicsMovement, PlayerStateMachine playerStateMachine)
	{
		_physicsMovement = physicsMovement;
		_playerStateMachine = playerStateMachine;
	}

	public virtual void Enter()
	{
		_horizontalInput = _verticalInput = 0.0f;
	}

	public virtual void HandleInput()
	{
		_horizontalInput = Input.GetAxis("Vertical");
		_verticalInput = Input.GetAxis("Horizontal");
	}

	public virtual void LogicUpdate()
	{

	}

	public virtual void PhysicsUpdate()
	{
		_physicsMovement.Move(new Vector3(_verticalInput, 0, _horizontalInput));
	}

	public virtual void Exit()
	{

	}
}
