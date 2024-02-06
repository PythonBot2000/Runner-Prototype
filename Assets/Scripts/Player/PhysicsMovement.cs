using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
	[Header("Прямая скорость")]
	[SerializeField] private float _maxForwardSpeed = 70f;
	[SerializeField] private float _currentSpeed = 30f;
	[SerializeField] private float _startSpeed = 30f;
	[SerializeField] private float _forwardSpeedModifier = 1f;

	[Header("Время")]
	[SerializeField] private float _timeSinceStart = 0f;
	[SerializeField] private float _speedUpDuration = 60f;

	[Header("Боковая скорость")]
	[SerializeField] private float _sideSpeed = 10f;
	[SerializeField] private float _maxSideSpeed = 30f;
	[SerializeField] private float _directionSwitchRatio = 5f;

	private Transform _transform;
	private PlayerStateMachine _playerStateMachine;
	private Rigidbody _rigidbody;
	private SurfaceSlider _surfaceSlider;

	private Coroutine _speedUpToMax;
	private Coroutine _changeSpeedModifier;

	private void Awake()
	{
		_transform = GetComponent<Transform>();
		_rigidbody = GetComponent<Rigidbody>();
		_surfaceSlider = GetComponent<SurfaceSlider>();

		_playerStateMachine = new PlayerStateMachine(this);
		_playerStateMachine.Initialize<Sliding>();
	}

	private void Update()
	{
		_playerStateMachine.CurrentState.HandleInput();
		_playerStateMachine.CurrentState.LogicUpdate();
	}

	private void FixedUpdate()
	{
		_playerStateMachine.CurrentState.PhysicsUpdate();
	}

	public void ChangeSpeedModifier(float endModifier, float smoothTime)
	{
		if(smoothTime > 0f)
		{
			StartCoroutine(ChangeSpeedModifierCoroutine(endModifier, smoothTime));
			
		}
		else
		{
			_forwardSpeedModifier = endModifier;
		}
	}

	private IEnumerator ChangeSpeedModifierCoroutine(float endModifier, float smoothTime)
	{
		float startModifier = _forwardSpeedModifier;
		float elapsedTime = 0f;

		while (elapsedTime < smoothTime)
		{
			if(endModifier > startModifier) _forwardSpeedModifier = Mathf.Lerp(startModifier, endModifier, elapsedTime / smoothTime);
			else _forwardSpeedModifier = Mathf.Lerp(endModifier, startModifier, 1f - (elapsedTime / smoothTime));

			elapsedTime += Time.deltaTime;

			yield return null;
		}
		_forwardSpeedModifier = endModifier;
	}

	public void SpeedUpToMax()
	{
		if (_currentSpeed < _maxForwardSpeed) StartCoroutine(SpeedUpToMaxSpeedCoroutine());
	}

	private IEnumerator SpeedUpToMaxSpeedCoroutine()
	{
		float elapsedTime = _timeSinceStart;
		while (elapsedTime < _speedUpDuration)
		{
			_currentSpeed = Mathf.Lerp(_startSpeed, _maxForwardSpeed,
				elapsedTime / _speedUpDuration);

			elapsedTime += Time.deltaTime;
			_timeSinceStart += Time.deltaTime;

			yield return null;
		}
		_currentSpeed = _maxForwardSpeed;
	}

	public void Move(Vector3 direction)
	{
		direction = WallCheck(direction);

		Vector3 directionAlongSurface = _surfaceSlider.Project(new Vector3(0, 0, direction.z));

		Vector3 offset = directionAlongSurface * _currentSpeed * Time.fixedDeltaTime * _forwardSpeedModifier;
		_rigidbody.MovePosition(_rigidbody.position + offset);

		offset = new Vector3(direction.x, 0, 0) * _sideSpeed;

		if ((_rigidbody.velocity.x > 0 && offset.x < 0) || (_rigidbody.velocity.x < 0 && offset.x > 0))
		{
			offset *= _directionSwitchRatio;
		}

		if ((_rigidbody.velocity + offset).magnitude < _maxSideSpeed)
		{
			_rigidbody.AddForce(offset, ForceMode.VelocityChange);
		}
		else
		{
			if (offset.x >= 0)
				_rigidbody.velocity = new Vector3(_maxSideSpeed, 0, 0);
			else
				_rigidbody.velocity = new Vector3(-_maxSideSpeed, 0, 0);
		}
	}

	private Vector3 WallCheck(Vector3 direction)
	{
		Vector3 startPoint = _transform.position;
		Vector3 endPoint = _transform.right;

		if (Physics.Raycast(startPoint, endPoint, 1f, LayerMask.NameToLayer("Wall")))
		{
			if (direction.x > 0)
			{
				direction.x = 0;
			}
			if (_rigidbody.velocity.x > 0)
			{
				_rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, _rigidbody.velocity.z);
			}
		}
		if (Physics.Raycast(startPoint, -endPoint, 1f, LayerMask.NameToLayer("Wall")))
		{
			if (direction.x < 0)
			{
				direction.x = 0;
			}
			if (_rigidbody.velocity.x < 0)
			{
				_rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, _rigidbody.velocity.z);
			}
		}

		return direction;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawRay(_transform.position, _rigidbody.velocity);
	}
}
