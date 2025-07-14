using UnityEngine;

public class Colliders : MonoBehaviour
{
	public PolygonCollider2D polygon;

	public CircleCollider2D circle;

	public EdgeCollider2D edge;

	public BoxCollider2D box;

	public CollectibleItem CI;

	private void OnEnable()
	{
		if (CI != null)
		{
			CI.enabled = true;
		}
	}
}
