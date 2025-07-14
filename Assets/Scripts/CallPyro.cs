using UnityEngine;

public class CallPyro : MonoBehaviour
{
	private GameObject pyro;

	private bool chek;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if ((collision.tag == "CarMain" || collision.tag == "CarMainChild" || collision.tag == "Wheels") && !chek)
		{
			chek = true;
			pyro = Pool.GameOBJECT(Pool.Bonus.pyro, base.gameObject.transform.position + new Vector3(40f, 40f, 0f));
			ToGround();
			base.gameObject.SetActive(value: false);
		}
	}

	private void ToGround()
	{
		Vector2 origin = pyro.transform.position + 10f * Vector3.up - Vector3.right;
		RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, -Vector2.up, float.MaxValue, 1 << LayerMask.NameToLayer("Ground"));
		Vector2 origin2 = pyro.transform.position + 10f * Vector3.up + Vector3.right;
		RaycastHit2D raycastHit2D2 = Physics2D.Raycast(origin2, -Vector2.up, float.MaxValue, 1 << LayerMask.NameToLayer("Ground"));
		Vector2 point = raycastHit2D2.point;
		float x = point.x;
		Vector2 point2 = raycastHit2D.point;
		float x2 = x - point2.x;
		Vector2 point3 = raycastHit2D2.point;
		float y = point3.y;
		Vector2 point4 = raycastHit2D.point;
		Vector2 from = new Vector2(x2, y - point4.y);
		float num = Vector2.Angle(from, Vector2.right);
		if (from.y < 0f)
		{
			num = 360f - num;
		}
		Vector2 point5 = raycastHit2D.point;
		float y2 = point5.y;
		Vector2 point6 = raycastHit2D2.point;
		float num2 = Mathf.Max(y2, point6.y) + 4f;
		Transform transform = pyro.transform;
		Vector3 position = pyro.transform.position;
		float x3 = position.x;
		float y3 = num2;
		Vector3 position2 = pyro.transform.position;
		transform.position = new Vector3(x3, y3, position2.z);
		pyro.transform.eulerAngles = Vector3.forward * num;
	}
}
