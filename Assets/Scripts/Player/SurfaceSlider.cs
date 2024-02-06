using UnityEngine;

public class SurfaceSlider : MonoBehaviour
{
    private Vector3 _normal;

	private void Awake()
	{
		_normal = transform.up;
	}

	public Vector3 Project(Vector3 forward)
    {
        return forward - Vector3.Dot(forward, _normal) * _normal;
    }

 //   private void OnCollisionEnter(Collision collision)
	//{
	//	NormalProvider normalProvider = collision.gameObject.GetComponent<NormalProvider>();

	//	if (normalProvider is not null && normalProvider.CanProvideNormal)
	//	{
	//		_normal = collision.GetContact(0).normal;
	//	}
	//	else
	//	{
	//		_normal = transform.up;
	//	}
 //   }

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + _normal * 3);
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + Project(transform.forward));
		Gizmos.color = Color.black;
		Gizmos.DrawLine(transform.position, transform.position + Project(transform.right));
		Gizmos.DrawLine(transform.position, transform.position + Project(-transform.right));
	}
}
