using UnityEngine;

public class SphereController : MonoBehaviour
{
	private Rigidbody2D rb;

	public void Update()
	{
		if (rb == null)
		{
			rb = base.gameObject.GetComponent<Rigidbody2D>();
		}
		rb.AddForce(Vector2.right * UnityEngine.Input.GetAxisRaw("Horizontal") * 10000f);
		rb.AddForce(Vector2.up * UnityEngine.Input.GetAxisRaw("Vertical") * 100000f);
	}
}
