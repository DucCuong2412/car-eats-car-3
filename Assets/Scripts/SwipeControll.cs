using System.Collections.Generic;
using UnityEngine;

public class SwipeControll : MonoBehaviour
{
	private Vector3 fp;

	private Vector3 lp;

	private float dragDistance;

	private List<Vector3> touchPositions = new List<Vector3>();

	private float number;

	private Vector2 worldStartPoint;

	private Vector2 worldStartPointe;

	private void Start()
	{
		Input.simulateMouseWithTouches = true;
		dragDistance = Screen.height * 20 / 100;
	}

	private void Update()
	{
		float axis = UnityEngine.Input.GetAxis("Mouse ScrollWheel");
		if (axis > 0f)
		{
			UnityEngine.Debug.Log(" scroll up");
		}
		else if (axis < 0f)
		{
			UnityEngine.Debug.Log(" scroll down");
		}
		if (UnityEngine.Input.touchCount == 1)
		{
			Touch touch = UnityEngine.Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began)
			{
				worldStartPoint = getWorldPoint(touch.position);
			}
			if (touch.phase == TouchPhase.Moved)
			{
				Vector2 vector = getWorldPoint(touch.position) - worldStartPoint;
				Camera.main.transform.Translate(0f - vector.x, 0f - vector.y, 0f);
			}
		}
	}

	private Vector2 getWorldPoint(Vector2 screenPoint)
	{
		Physics.Raycast(Camera.main.ScreenPointToRay(screenPoint), out RaycastHit hitInfo);
		return hitInfo.point;
	}
}
