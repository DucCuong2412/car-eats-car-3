using UnityEngine;

public class enableColl : MonoBehaviour
{
	public BoxCollider2D bc2d;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "CarMain" || other.tag == "CarMainChild")
		{
			bc2d.enabled = true;
		}
	}
}
