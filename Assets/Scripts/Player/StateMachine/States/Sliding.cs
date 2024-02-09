using UnityEngine;

public class Sliding : PlayerState
{
	private ObstacleCollisionHandler _collisionHandler;
	private Health _health;
	private bool _isCollided;
	private bool _isDead;

	public Sliding(PhysicsMovement physicsMovement, PlayerStateMachine playerStateMachine) : base(physicsMovement, playerStateMachine) 
	{
		_collisionHandler = physicsMovement.GetComponent<ObstacleCollisionHandler>();
		_health = physicsMovement.GetComponent<Health>();
	}

	private void IsCollided()
	{
		_isCollided = true;
	}
	private void IsDead()
	{
		_isDead = true;
	}

	public override void Enter()
	{
		base.Enter();
		_isCollided = false;
		_isDead = false;
		_collisionHandler.CollidedWithObstacle += IsCollided;
		_health.HealthDownToZero += IsDead;
		_physicsMovement.SpeedUpToMax();

		Debug.Log("Состояние: обычное скольжение");
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();
		if (_isDead)
		{
			_playerStateMachine.ChangeState<Dead>();
		}
		else if (_isCollided)
		{
			_playerStateMachine.ChangeState<Invulnerable>();
		}
		
	}

	public override void Exit()
	{
		base.Exit();
		_collisionHandler.CollidedWithObstacle -= IsDead;
		_health.HealthDownToZero -= IsDead;
	}
}
