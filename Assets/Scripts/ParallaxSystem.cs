using System;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxSystem : MonoBehaviour
{
	[Serializable]
	public class DecorPart
	{
		public GameObject[] Decor;

		public Vector2 DecorSpeed = new Vector2(0f, 0f);

		[HideInInspector]
		public float distanceLeft;

		[HideInInspector]
		public float distanceRight;
	}

	[Serializable]
	public class ParallaxPart
	{
		public Vector2 Speed = new Vector2(0f, 0f);

		public Renderer Parallax;
	}

	private DecorPart[] LocationComponent;

	public List<ParallaxPart> ParallaxParts = new List<ParallaxPart>();

	private Vector3 lastPosition;

	private Transform mainCameraTransform;

	private Camera mainCamera;

	private float speedX;

	private float speedY;

	private int location;

	public static void Create(Transform _camera, int _location)
	{
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Parallax"));
		ParallaxSystem component = gameObject.GetComponent<ParallaxSystem>();
		if (_location < 1 || _location > 5)
		{
			_location = 1;
		}
		if (Progress.shop.TestFor9)
		{
			_location = 6;
		}
		if (Progress.shop.Undeground2)
		{
			_location = 7;
		}
		component.location = _location;
		component.mainCameraTransform = _camera;
		component.mainCamera = _camera.GetComponent<Camera>();
	}

	public void Create()
	{
		base.transform.position = mainCameraTransform.position;
		base.transform.parent = mainCameraTransform;
		if (!(Resources.Load("decor_" + location) == null))
		{
			if (Progress.shop.EsterLevelPlay)
			{
				location = 4;
			}
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("decor_" + location));
			gameObject.transform.parent = base.transform;
			gameObject.transform.position = base.transform.position + new Vector3(0f, 0f, 10f);
			LocationComponent = gameObject.GetComponent<ParallaxInformation>().LocationComponent.ToArray();
			int num = LocationComponent.Length;
			for (int i = 0; i < num; i++)
			{
				GetSideDistances(LocationComponent[i]);
				AddDecorCopy(LocationComponent[i]);
			}
		}
	}

	public void GetSideDistances(DecorPart _decor)
	{
		MeshRenderer[] componentsInChildren = _decor.Decor[0].GetComponentsInChildren<MeshRenderer>();
		foreach (MeshRenderer meshRenderer in componentsInChildren)
		{
			Vector3 position = meshRenderer.gameObject.transform.position;
			float x = position.x;
			Vector3 position2 = _decor.Decor[0].transform.position;
			if (x <= position2.x)
			{
				Vector3 position3 = _decor.Decor[0].transform.position;
				float x2 = position3.x;
				Vector3 position4 = meshRenderer.gameObject.transform.position;
				float num = x2 - position4.x;
				Vector3 size = meshRenderer.bounds.size;
				if (0f - (num + size.x / 2f) < _decor.distanceLeft)
				{
					Vector3 position5 = _decor.Decor[0].transform.position;
					float x3 = position5.x;
					Vector3 position6 = meshRenderer.gameObject.transform.position;
					float num2 = x3 - position6.x;
					Vector3 size2 = meshRenderer.bounds.size;
					_decor.distanceLeft = 0f - (num2 + size2.x / 2f);
				}
			}
			Vector3 position7 = meshRenderer.gameObject.transform.position;
			float x4 = position7.x;
			Vector3 position8 = _decor.Decor[0].transform.position;
			float num3 = x4 - position8.x;
			Vector3 size3 = meshRenderer.bounds.size;
			if (num3 + size3.x / 2f > _decor.distanceRight)
			{
				Vector3 position9 = meshRenderer.gameObject.transform.position;
				float x5 = position9.x;
				Vector3 position10 = _decor.Decor[0].transform.position;
				float num4 = x5 - position10.x;
				Vector3 size4 = meshRenderer.bounds.size;
				_decor.distanceRight = num4 + size4.x / 2f;
			}
		}
	}

	public void AddDecorCopy(DecorPart _decor)
	{
		_decor.Decor[1] = UnityEngine.Object.Instantiate(_decor.Decor[0]);
		_decor.Decor[1].transform.parent = _decor.Decor[0].transform.parent;
		_decor.Decor[1].transform.localScale = _decor.Decor[0].transform.localScale;
		Transform transform = _decor.Decor[1].transform;
		Vector3 position = _decor.Decor[0].transform.position;
		float x = position.x + (_decor.distanceRight + Mathf.Abs(_decor.distanceLeft));
		Vector3 position2 = _decor.Decor[0].transform.position;
		transform.position = new Vector2(x, position2.y);
	}

	public void Start()
	{
		Create();
	}

	private void OnRenderObject()
	{
		if (Time.timeScale != 0f)
		{
			UpdateCameInf();
			UpdateOffset();
			UpdatePos();
		}
	}

	private void UpdateCameInf()
	{
		Vector3 position = mainCameraTransform.position;
		speedX = position.x - lastPosition.x;
		Vector3 position2 = mainCameraTransform.position;
		speedY = position2.y - lastPosition.y;
		lastPosition = mainCameraTransform.position;
	}

	private void UpdateOffset()
	{
		Vector3 vector = mainCamera.ScreenToWorldPoint(Vector2.zero);
		float x = vector.x;
		Vector3 vector2 = mainCamera.ScreenToWorldPoint(new Vector2(mainCamera.pixelWidth, 0f));
		float x2 = vector2.x;
		int num = LocationComponent.Length;
		for (int i = 0; i < num; i++)
		{
			DecorPart decorPart = LocationComponent[i];
			int num2 = decorPart.Decor.Length;
			for (int j = 0; j < num2; j++)
			{
				Transform transform = decorPart.Decor[j].transform;
				Vector3 position = transform.position - Vector3.right * speedX * decorPart.DecorSpeed.x;
				if (position.x + decorPart.distanceRight < x)
				{
					Vector3 position2 = decorPart.Decor[num2 - (j + 1)].transform.position;
					position = new Vector3(position2.x + (Mathf.Abs(decorPart.distanceLeft) + Mathf.Abs(decorPart.distanceRight)) - speedX * decorPart.DecorSpeed.x - 0.5f, position.y, 0.5f);
				}
				else if (position.x + decorPart.distanceLeft > x2)
				{
					Vector3 position3 = decorPart.Decor[num2 - (j + 1)].transform.position;
					position = new Vector3(position3.x - (LocationComponent[i].distanceRight + Mathf.Abs(LocationComponent[i].distanceLeft)) - speedX * LocationComponent[i].DecorSpeed.x + 0.5f, position.y, 0.5f);
				}
				transform.position = position;
			}
		}
	}

	private void UpdatePos()
	{
		int num = LocationComponent.Length;
		for (int i = 0; i < num; i++)
		{
			DecorPart decorPart = LocationComponent[i];
			int num2 = decorPart.Decor.Length;
			Vector3 vector = Vector3.up * decorPart.DecorSpeed.y * speedY;
			for (int j = 0; j < num2; j++)
			{
				decorPart.Decor[j].transform.position -= vector;
			}
		}
	}
}
