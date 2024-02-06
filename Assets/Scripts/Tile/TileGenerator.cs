using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private Transform _player;
	[SerializeField] private int _chunksCount = 2;

	[Header("Настройка тайлов")]
	[SerializeField] private GameObject _randomTile;

	private Queue<GameObject> _chunkQueue;
	private float _nextSpawnPosition;
	private float _spawnStep;

	private void Awake()
	{
		_chunkQueue = new Queue<GameObject>();
		_spawnStep = 300f;
		_nextSpawnPosition = -_spawnStep;
	}

	private void Start()
	{
		for (int i = 0; i < _chunksCount; i++)
		{
			CreateNextChunk();
		}
	}

	void Update()
	{
		if(_player.position.z + _spawnStep * (_chunksCount - 1) > _nextSpawnPosition)
		{
			CreateNextChunk();
			DeletePreviousChunk();
		}
	}

	private void DeletePreviousChunk()
	{
		GameObject previousChunk = _chunkQueue.Dequeue();
		Destroy(previousChunk.gameObject);
	}

	void CreateNextChunk()
	{
		GameObject nextChunk = Instantiate(_randomTile, new Vector3(0, 0, _nextSpawnPosition), new Quaternion());
		_chunkQueue.Enqueue(nextChunk);
		_nextSpawnPosition += _spawnStep;
	}
}
