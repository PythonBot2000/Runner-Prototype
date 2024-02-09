using UnityEngine;

public class Invulnerable : PlayerState
{
	private float _slowdownTime = 2f;
	private float _timePast = 0f;

	private MaterialController _materialController;

	public Invulnerable(PhysicsMovement physicsMovement, PlayerStateMachine playerStateMachine) : base(physicsMovement, playerStateMachine) 
	{
		_materialController = physicsMovement.GetComponent<MaterialController>();
	}

	public override void Enter()
	{
		base.Enter();
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Obstacle"), LayerMask.NameToLayer("Player"), true);

		_physicsMovement.StopAllCoroutines();

		_physicsMovement.ChangeSpeedModifier(0.5f, 0f);
		_physicsMovement.ChangeSpeedModifier(1f, _slowdownTime);

		Color color = _materialController.Color;
		color.a = 0.3f;
		_materialController.Flash(color);

		Debug.Log("—осто€ние: неу€звимость");
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();
		if (_timePast > _slowdownTime) 
		{
			_playerStateMachine.ChangeState<Sliding>();
		}
		_timePast += Time.deltaTime;
	}

	public override void Exit()
	{
		base.Exit();
		_timePast = 0f;
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Obstacle"), LayerMask.NameToLayer("Player"), false);
	}
}
