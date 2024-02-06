using UnityEngine;

public class Obstacle : MonoBehaviour
{
	public Vector3Int Size;

	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(0f, 1f, 0f, .5f);
		Gizmos.DrawCube(transform.position, Size);
	}
}
