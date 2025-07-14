using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
	public Transform target;

	public float delay = 10000f;

	public float distance = 40f;

	public float maxX;

	public float minX;

	private float height;

	private float width;

	private Camera cam;

	private Camera camGC;

	private void Start()
	{
		minX = EasyHill2DManager.Instance().getMinX();
		maxX = EasyHill2DManager.Instance().getMaxX();
		cam = Camera.main;
		height = 2f * cam.orthographicSize;
		width = height * cam.aspect;
		camGC = null;
		camGC = GetComponent<Camera>();
	}

	private void FixedUpdate()
	{
		base.transform.position = Vector3.Lerp(base.transform.position, target.position + new Vector3(0f, 0f, 0f - distance), Time.deltaTime * delay);
		if (camGC == null)
		{
			camGC = GetComponent<Camera>();
		}
		Vector3 position = base.transform.position;
		if (position.y < height / 2f)
		{
			Transform transform = base.transform;
			Vector3 position2 = base.transform.position;
			float x = position2.x;
			float orthographicSize = camGC.orthographicSize;
			Vector3 position3 = base.transform.position;
			transform.position = new Vector3(x, orthographicSize, position3.z);
		}
		Vector3 position4 = base.transform.position;
		if (position4.x < minX + width / 2f)
		{
			Transform transform2 = base.transform;
			float x2 = minX + width / 2f;
			Vector3 position5 = base.transform.position;
			float y = position5.y;
			Vector3 position6 = base.transform.position;
			transform2.position = new Vector3(x2, y, position6.z);
		}
		Vector3 position7 = base.transform.position;
		if (position7.x > maxX - width / 2f)
		{
			Transform transform3 = base.transform;
			float x3 = maxX - width / 2f;
			Vector3 position8 = base.transform.position;
			float y2 = position8.y;
			Vector3 position9 = base.transform.position;
			transform3.position = new Vector3(x3, y2, position9.z);
		}
	}
}
