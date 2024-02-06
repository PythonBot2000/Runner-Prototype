using UnityEngine;

public class Sliding : PlayerState
{
	private ObstacleCollisionHandler _collisionHandler;
	private bool _isCollided;

	public Sliding(PhysicsMovement physicsMovement, PlayerStateMachine playerStateMachine) : base(physicsMovement, playerStateMachine) 
	{
		_collisionHandler = physicsMovement.GetComponent<ObstacleCollisionHandler>();
	}

	private void SetIsCollidedTrue()
	{
		_isCollided = true;
	}

	public override void Enter()
	{
		base.Enter();
		_isCollided = false;
		_collisionHandler.CollidedWithObstacle += SetIsCollidedTrue;
		_physicsMovement.SpeedUpToMax();

		//Debug.Log("Состояние: обычное скольжение");
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();
		if (_isCollided)
		{
			_playerStateMachine.ChangeState<Invulnerable>();
		}
	}

	public override void Exit()
	{
		base.Exit();
		_collisionHandler.CollidedWithObstacle -= SetIsCollidedTrue;
	}
}
