using UnityEngine;
using UnityEngine.Events;

public class ObstacleCollisionHandler : MonoBehaviour
{
	private float _lastObstacleCollisonTime = 0f;
	private float _delay = 2f;

	public event UnityAction CollidedWithObstacle;

	private void OnTriggerEnter(Collider other)
	{
		GameObject hittedObject = other.gameObject;
		if ((hittedObject.tag == "Obstacle") && (Time.time - _lastObstacleCollisonTime > _delay))
		{
			Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Obstacle"), LayerMask.NameToLayer("Player"), true);
			Destroy(hittedObject);

			CollidedWithObstacle?.Invoke();
			_lastObstacleCollisonTime = Time.time;
		}
		else if(hittedObject.tag == "Coin")
		{
			Destroy(hittedObject);
		}
	}

	private void Update()
	{
		if (Time.time - _lastObstacleCollisonTime > _delay)
		{
			Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Obstacle"), LayerMask.NameToLayer("Player"), false);
		}
	}
}
