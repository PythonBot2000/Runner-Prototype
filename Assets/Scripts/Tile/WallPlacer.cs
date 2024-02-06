using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlacer : MonoBehaviour
{
    [SerializeField] private List<GameObject> _wallPrefabs = new List<GameObject>();
	[SerializeField] private GameObject _rightAnchor;
	[SerializeField] private GameObject _leftAnchor;

	private void Start()
	{
		SetWallModel(_rightAnchor.transform);
		SetWallModel(_leftAnchor.transform);
	}

	private void SetWallModel(Transform anchor)
	{
		Instantiate(_wallPrefabs[Random.Range(0, _wallPrefabs.Count)], anchor.position, anchor.rotation, anchor.transform);
	}
}
