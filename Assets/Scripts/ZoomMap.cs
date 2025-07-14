using UnityEngine;
using UnityEngine.UI;

public class ZoomMap : MonoBehaviour
{
	public float perspectiveZoomSpeed = 0.5f;

	public float orthoZoomSpeed = 0.5f;

	public Camera camera;

	public CameraControllerZoom CCZ;

	public ScrollRect SR;

	public float min;

	public float max;

	public float speed;

	public void zoomPlus()
	{
		CCZ.ZoomChange(SR, (0f - orthoZoomSpeed) * speed, CCZ.gameObject, min, max);
	}

	public void zoomMinus()
	{
		CCZ.ZoomChange(SR, orthoZoomSpeed * speed, CCZ.gameObject, min, max);
	}

	private void Update()
	{
		if (UnityEngine.Input.touchCount >= 2)
		{
			SR.enabled = false;
			Touch touch = UnityEngine.Input.GetTouch(0);
			Touch touch2 = UnityEngine.Input.GetTouch(1);
			Vector2 a = touch.position - touch.deltaPosition;
			Vector2 b = touch2.position - touch2.deltaPosition;
			float magnitude = (a - b).magnitude;
			float magnitude2 = (touch.position - touch2.position).magnitude;
			float num = magnitude - magnitude2;
			if (camera.orthographic)
			{
				CCZ.ZoomChange(SR, orthoZoomSpeed * num, CCZ.gameObject, min, max);
				return;
			}
			camera.fieldOfView += num * perspectiveZoomSpeed;
			camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
		}
		else if (UnityEngine.Input.touchCount <= 1)
		{
			SR.enabled = true;
		}
	}
}
