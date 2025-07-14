using UnityEngine;

public class OnOffColl : MonoBehaviour
{
	public CircleCollider2D cc2d;

	public BoxCollider2D bc2d;

	public EdgeCollider2D ec2d;

	public PolygonCollider2D pc2d;

	private void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.tag == "CarMain" || coll.tag == "CarMainChild")
		{
			if (cc2d != null)
			{
				cc2d.enabled = false;
			}
			if (bc2d != null)
			{
				bc2d.enabled = false;
			}
			if (ec2d != null)
			{
				ec2d.enabled = false;
			}
			if (pc2d != null)
			{
				pc2d.enabled = false;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.tag == "CarMain" || coll.tag == "CarMainChild")
		{
			if (cc2d != null)
			{
				cc2d.enabled = true;
			}
			if (bc2d != null)
			{
				bc2d.enabled = true;
			}
			if (ec2d != null)
			{
				ec2d.enabled = true;
			}
			if (pc2d != null)
			{
				pc2d.enabled = true;
			}
		}
	}
}
