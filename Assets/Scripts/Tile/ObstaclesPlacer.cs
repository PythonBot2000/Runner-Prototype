using System.Collections.Generic;
using UnityEngine;

public class ObstaclesPlacer : MonoBehaviour
{
	[SerializeField] private List<GameObject> _obstaclePrefabs;
	[SerializeField] private GameObject _obstacleLayer;
	[SerializeField] private Vector2Int _gridSize;
	[SerializeField] private float _obstacleOffsetX;
	[SerializeField] private float _obstacleOffsetZ;
	[SerializeField] private int _obstacleCount;

	private Obstacle[,] _grid;

	private void Start()
	{
		_grid = new Obstacle[_gridSize.x, _gridSize.y];

		for(int i = 0; i < _obstacleCount; i++)
		{
			int obstaclePositionX = Random.Range(0, _gridSize.x);
			int obstaclePositionZ = Random.Range(0, _gridSize.y);

			GameObject obstacle = _obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Count)];
			Obstacle temp = obstacle.GetComponent<Obstacle>();

			if ((obstaclePositionX <= _gridSize.x) &&
				(obstaclePositionZ <= _gridSize.y))
			{
				if (!IsPlaceTaken(temp, obstaclePositionX, obstaclePositionZ))
				{
					Instantiate(obstacle, new Vector3(obstaclePositionX + _obstacleOffsetX + transform.position.x, 1f,
						obstaclePositionZ + _obstacleOffsetZ + transform.position.z), Quaternion.identity, _obstacleLayer.transform);

					PlaceObstacle(temp, obstaclePositionX, obstaclePositionZ);
				}
			}
			
		}
	}

	private bool IsPlaceTaken(Obstacle obstacle, int placeX, int placeZ)
	{
		for (int x = -obstacle.Size.x / 2; x < obstacle.Size.x / 2; x++)
		{
			if(x + placeX < 0 || x + placeX >= _gridSize.x) continue;

			for (int z = -obstacle.Size.z / 2; z < obstacle.Size.z / 2; z++)
			{
				if (z + placeZ < 0 || z + placeZ >= _gridSize.y) continue;

				if (_grid[placeX + x, placeZ + z] != null) return true;
			}
		}

		return false;
	}

	private void PlaceObstacle(Obstacle obstacle, int placeX, int placeZ)
	{
		for (int x = -obstacle.Size.x / 2; x < obstacle.Size.x / 2; x++)
		{
			if (x + placeX < 0 || x + placeX >= _gridSize.x) continue;

			for (int z = -obstacle.Size.z / 2; z < obstacle.Size.z / 2; z++)
			{
				if (z + placeZ < 0 || z + placeZ >= _gridSize.y) continue;

				_grid[placeX + x, placeZ + z] = obstacle;
			}
		}
	}
}
