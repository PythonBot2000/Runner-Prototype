using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
	[SerializeField] private ObstacleCollisionHandler collisionHandler;
	[SerializeField] private int _healthMaxPoints = 4;

	private int _healthPoints;

	public int HealthPoints => _healthPoints;
	public int HealthMaxPoints => _healthMaxPoints;

	public event UnityAction HealthChanged;
	public event UnityAction HealthDownToZero;

	private void OnEnable()
	{
		collisionHandler.CollidedWithObstacle += DecreaseHealth;
	}

	private void OnDisable()
	{
		collisionHandler.CollidedWithObstacle -= DecreaseHealth;
	}

	private void Awake()
	{
		_healthPoints = _healthMaxPoints;
	}

	private void DecreaseHealth()
	{
		if(_healthPoints > 0)
		{
			_healthPoints--;
			HealthChanged?.Invoke();
		}

		if(_healthPoints <= 0)
		{
			HealthDownToZero?.Invoke();
		}
	}

	private void IncreaseHealth()
	{
		if (_healthPoints < _healthMaxPoints)
		{
			_healthPoints++;
			HealthChanged?.Invoke();
		}
	}

	public void ResetHealth()
	{
		_healthPoints = _healthMaxPoints;
		HealthChanged?.Invoke();
	}
}
