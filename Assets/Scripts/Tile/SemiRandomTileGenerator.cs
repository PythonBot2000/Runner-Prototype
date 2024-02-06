using System.Collections.Generic;
using UnityEngine;

public class SemiRandomTileGenerator : MonoBehaviour
{
	[SerializeField] private Transform _player;
	[SerializeField] private int _chunksCount = 5;

	[Header("Настройка тайлов")]
	[SerializeField] private GameObject _randomTile;
	[SerializeField] private GameObject[] _easyTiles;
	[SerializeField] private GameObject[] _mediumTiles;
	[SerializeField] private GameObject[] _hardTiles;
	[SerializeField] private GameObject[] _intersectionTiles;

	private Queue<GameObject> _chunkQueue;
	private float _nextSpawnPosition;
	private float _positionToSpawn;
	private bool _isNextIntersection;

	private GameObject _nextTile;

	private void Awake()
	{
		_chunkQueue = new Queue<GameObject>();
		_nextSpawnPosition = 0f;
		_positionToSpawn = 200f * (_chunksCount - 2);
		_isNextIntersection = false;
		_nextTile = _randomTile;
	}

	private void Start()
	{
		for (int i = 0; i < _chunksCount; i++)
		{
			CreateNextTile();
		}
	}

	void Update()
	{
		if (_player.position.z + _positionToSpawn > _nextSpawnPosition)
		{
			CreateNextTile();
			DeletePreviousTile();
		}
	}

	private void DeletePreviousTile()
	{
		GameObject previousChunk = _chunkQueue.Dequeue();
		Destroy(previousChunk.gameObject);
	}

	private void CreateNextTile()
	{
		_nextTile = Instantiate(_nextTile, new Vector3(0, 0, _nextSpawnPosition), Quaternion.identity);
		_chunkQueue.Enqueue(_nextTile);
		Tile tile = _nextTile.GetComponent<Tile>();
		_nextSpawnPosition += tile.Size / 2;

		_isNextIntersection = !_isNextIntersection;
		_nextTile = GetRandomTile();
		tile = _nextTile.GetComponent<Tile>();
		_nextSpawnPosition += tile.Size / 2;
	}

	private GameObject GetRandomTile()
	{
		if (_isNextIntersection)
		{
			int num = Random.Range(0, _intersectionTiles.Length);
			return _intersectionTiles[num];
		}
		else
		{
			float chance = Random.value * 100;
			return chance switch
			{
				< 25 => _easyTiles[0],
				< 50 => _mediumTiles[0],
				< 75 => _hardTiles[0],
				_ => _randomTile
			};
		}
		
	}
}
