using System;
using System.Collections;
using UnityEngine;

public class FollowSystem : MonoBehaviour
{
	private class _monoBehaviorr : MonoBehaviour
	{
		private static _monoBehaviorr _instance;

		public static _monoBehaviorr instance
		{
			get
			{
				if (_instance == null)
				{
					GameObject gameObject = new GameObject("_cameraCoroutine");
					_instance = gameObject.AddComponent<_monoBehaviorr>();
				}
				return _instance;
			}
		}
	}

	public class Values
	{
		public float ActivateSpeed = 13f;

		[HideInInspector]
		public float JumpHeight = 7f;

		[HideInInspector]
		public float targetX;

		[HideInInspector]
		public Rigidbody2D playerRB;

		[HideInInspector]
		public Transform player;

		[HideInInspector]
		public Camera MainCamera;

		[HideInInspector]
		public float SpeedX
		{
			get
			{
				if (player != null && playerRB == null)
				{
					playerRB = player.GetComponent<Rigidbody2D>();
				}
				return playerRB.velocity.magnitude;
			}
		}

		[HideInInspector]
		public bool JumpProcessing()
		{
			if (player == null)
			{
				return false;
			}
			Vector2 start = player.position;
			Vector3 position = player.position;
			float x = position.x;
			Vector3 position2 = player.position;
			float y = position2.y - JumpHeight;
			Vector3 position3 = player.position;
			return !Physics2D.Linecast(start, new Vector3(x, y, position3.z), 1 << LayerMask.NameToLayer("Ground"));
		}
	}

	[Serializable]
	public class Zoom : Values
	{
		public float CameraMoveSpeedZoom = 3f;

		public float ZoomOnSpeed = 16f;

		[HideInInspector]
		public bool Enabled;

		private float currentZoom;

		public bool OnCageZoom;

		public bool OnZoomBlock;

		public float OnCageZoomIter;

		public void Start(Transform _player, Camera _mainCam)
		{
			MainCamera = _mainCam;
			player = _player;
			Enabled = true;
			playerRB = null;
			playerRB = player.GetComponent<Rigidbody2D>();
		}

		public void ZoomTool()
		{
			if (player == null || !Enabled || OnZoomBlock)
			{
				return;
			}
			if (!OnCageZoom)
			{
				if (base.SpeedX > ActivateSpeed)
				{
					currentZoom = ZoomOnSpeed;
				}
				if (base.SpeedX < ActivateSpeed - 3f && !JumpProcessing())
				{
					currentZoom = ZoomOnSpeed - 4f;
				}
				if (JumpProcessing())
				{
					currentZoom = ZoomOnSpeed + 1f;
				}
				MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize, currentZoom, CameraMoveSpeedZoom * Time.deltaTime);
			}
			else if (OnCageZoomIter < 1f)
			{
				OnCageZoomIter += Time.deltaTime;
				MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize, 12f, OnCageZoomIter);
			}
		}
	}

	[Serializable]
	public class Offset : Values
	{
		[Range(0f, 1f)]
		public float xOffsetMax = 0.7f;

		public float CameraMoveSpeedOffset = 0.1f;

		[Range(0f, 1f)]
		public float MinYDistance = 0.3f;

		[Range(0f, 1f)]
		public float MaxYDistance = 0.6f;

		[HideInInspector]
		public bool Enabled;

		public float xOffset;

		public float yOffset;

		private float pixelHeight;

		private float pixelWidth;

		public float stabiliz = 1f;

		public void Start(Transform _player, Camera _mainCam)
		{
			MainCamera = _mainCam;
			player = _player;
			pixelWidth = MainCamera.pixelWidth;
			pixelHeight = MainCamera.pixelHeight;
			if (MaxYDistance < MinYDistance)
			{
				MaxYDistance = 0.5f;
				MinYDistance = 0.5f;
			}
			Vector3 position = player.position;
			yOffset = position.y;
			Enabled = true;
		}

		public IEnumerator stopCam()
		{
			stabiliz = 1f;
			float speed = 1f;
			while (stabiliz > 0f)
			{
				stabiliz -= Time.deltaTime * speed;
				yield return null;
			}
			stabiliz = 0f;
		}

		public IEnumerator startCam()
		{
			stabiliz = 0f;
			float speed = 1f;
			while (stabiliz < 1f)
			{
				stabiliz += Time.deltaTime * speed;
				yield return null;
			}
			stabiliz = 1f;
		}

		public void OffsetTool()
		{
			if (player == null || !Enabled)
			{
				return;
			}
			if (base.SpeedX > ActivateSpeed)
			{
				float a = xOffset;
				Vector3 vector = MainCamera.ScreenToWorldPoint(new Vector3(pixelWidth / 2f * xOffsetMax, 0f));
				float x = vector.x;
				Vector3 position = MainCamera.transform.position;
				xOffset = Mathf.Lerp(a, Mathf.Abs(x - position.x), Time.deltaTime);
				Vector3 position2 = player.position;
				float y = position2.y;
				Vector3 vector2 = MainCamera.ScreenToWorldPoint(new Vector3(0f, pixelHeight * MaxYDistance));
				if (y > vector2.y)
				{
					Vector3 position3 = player.position;
					float y2 = position3.y;
					Vector3 vector3 = MainCamera.ScreenToWorldPoint(new Vector3(0f, pixelHeight * MaxYDistance));
					float y3 = vector3.y;
					Vector3 position4 = MainCamera.transform.position;
					yOffset = y2 - (y3 - position4.y);
				}
				Vector3 position5 = player.position;
				float y4 = position5.y;
				Vector3 vector4 = MainCamera.ScreenToWorldPoint(new Vector3(0f, pixelHeight * MinYDistance));
				if (y4 < vector4.y)
				{
					Vector3 position6 = player.position;
					float y5 = position6.y;
					Vector3 vector5 = MainCamera.ScreenToWorldPoint(new Vector3(0f, pixelHeight * MinYDistance));
					float y6 = vector5.y;
					Vector3 position7 = MainCamera.transform.position;
					yOffset = y5 - (y6 - position7.y);
				}
			}
			else if (base.SpeedX < ActivateSpeed - 3f)
			{
				xOffset = Mathf.Lerp(xOffset, 0f, Time.deltaTime);
				float a2 = yOffset;
				Vector3 position8 = player.position;
				yOffset = Mathf.Lerp(a2, position8.y, Time.deltaTime);
			}
			if (stabiliz < 1f)
			{
				Vector3 position9 = player.position;
				base.targetX = position9.x + xOffset * stabiliz;
				float num = yOffset;
				Vector3 position10 = player.position;
				float num2 = (num - position10.y) * stabiliz;
				Vector3 position11 = player.position;
				yOffset = num2 + position11.y;
			}
			else
			{
				Vector3 position12 = player.position;
				base.targetX = position12.x + xOffset;
			}
			Transform transform = MainCamera.transform;
			float targetX = base.targetX;
			float y7 = yOffset;
			Vector3 position13 = MainCamera.transform.position;
			transform.position = new Vector3(targetX, y7, position13.z);
		}
	}
}
