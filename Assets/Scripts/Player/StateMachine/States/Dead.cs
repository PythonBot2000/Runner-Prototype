using UnityEngine;

public class Dead : PlayerState
{
	public Dead(PhysicsMovement physicsMovement, PlayerStateMachine playerStateMachine) : base(physicsMovement, playerStateMachine) { }

	public override void Enter()
	{
		_horizontalInput = _verticalInput = 0.0f;
		_physicsMovement.StopAllCoroutines();

		_physicsMovement.gameObject.SetActive(false);

		Debug.Log("Состояние: смэрть");
	}

	public override void HandleInput()
	{
		
	}

	public override void LogicUpdate()
	{

	}

	public override void PhysicsUpdate()
	{
		
	}

	public override void Exit()
	{

	}

}
