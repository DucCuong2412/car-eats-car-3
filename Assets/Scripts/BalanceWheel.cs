using UnityEngine;

public class BalanceWheel : MonoBehaviour
{
	public Rigidbody2D Rb;

	public GameObject pos;

	public HingeJoint2D HingeJ;

	private void Start()
	{
		if (HingeJ != null)
		{
			HingeJoint2D hingeJ = HingeJ;
			Vector3 position = pos.transform.position;
			float x = position.x;
			Vector3 position2 = pos.transform.position;
			hingeJ.connectedAnchor = new Vector2(x, position2.y);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "CarMain" || other.tag == "CarMainChild")
		{
			Rb.isKinematic = false;
			if (HingeJ == null)
			{
				Audio.Play("finish_cave");
			}
			base.gameObject.SetActive(value: false);
		}
	}
}
