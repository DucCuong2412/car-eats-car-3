using System;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : PoolBase
{
	private class ShadowObject
	{
		private Transform _transform;

		public GameObject gameObject;

		public Transform shadow;

		public Shadow.ShadowType shadowType;

		public bool active;

		private float _size = 1f;

		public tk2dSprite sprite;

		public Transform transform
		{
			get
			{
				return _transform;
			}
			set
			{
				_transform = value;
				gameObject = value.gameObject;
			}
		}

		public float size
		{
			get
			{
				return _size;
			}
			set
			{
				_size = value;
			}
		}

		public ShadowObject(Transform _shadow, float _size = 1f, Shadow.ShadowType _type = Shadow.ShadowType.None)
		{
			shadow = _shadow;
			shadow.gameObject.SetActive(value: true);
			size = _size;
			shadowType = _type;
			sprite = shadow.gameObject.GetComponent<tk2dSprite>();
		}

		public void Disable()
		{
			active = false;
			shadow.gameObject.SetActive(value: false);
		}

		public void Enable()
		{
			shadow.gameObject.SetActive(value: true);
			if (shadowType == Shadow.ShadowType.FixedSize)
			{
				sprite.scale = new Vector3(size * 5f, size * 5f);
			}
			active = true;
		}
	}

	private static ShadowPool _instance;

	private RaycastHit2D[] hh1 = new RaycastHit2D[1];

	private RaycastHit2D[] hh2 = new RaycastHit2D[1];

	private int layerCast;

	private List<ShadowObject> objectsWithShadow = new List<ShadowObject>();

	private Stack<ShadowObject> shadowStack = new Stack<ShadowObject>();

	private bool isStarted;

	private const string untaggedTag = "Untagged";

	public static ShadowPool instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject gameObject = new GameObject("ShadowPool");
				_instance = gameObject.AddComponent<ShadowPool>();
				_instance.CreatePool(gameObject);
			}
			return _instance;
		}
	}

	public static bool IsStarted => instance.isStarted;

	private void OnEnable()
	{
		layerCast = 1 << LayerMask.NameToLayer("Ground");
	}

	private Transform GetShadow()
	{
		GameObject @object = GetObject("Shadow");
		return @object.transform;
	}

	private void OnDestroy()
	{
		objectsWithShadow.Clear();
		shadowStack.Clear();
		_instance = null;
	}

	public static void Init(int count = 30)
	{
		instance.Add("Shadow", count);
		List<ShadowObject> list = new List<ShadowObject>();
		for (int i = 0; i < count; i++)
		{
			ShadowObject item = new ShadowObject(instance.GetShadow());
			list.Add(item);
		}
		for (int j = 0; j < count; j++)
		{
			instance.shadowStack.Push(list[j]);
			instance.shadowStack.Peek().Disable();
		}
		instance.StartShadows();
	}

	public Transform CastShadow(Transform trans, float size, Shadow.ShadowType type)
	{
		if (shadowStack.Count == 0)
		{
			shadowStack.Push(new ShadowObject(instance.GetShadow()));
			shadowStack.Peek().Disable();
		}
		ShadowObject shadowObject;
		for (int i = 0; i < objectsWithShadow.Count; i++)
		{
			if (objectsWithShadow[i].transform == trans)
			{
				shadowObject = objectsWithShadow[i];
				objectsWithShadow.RemoveAt(i);
				shadowObject.Disable();
				shadowStack.Push(shadowObject);
			}
		}
		shadowObject = shadowStack.Pop();
		shadowObject.transform = trans;
		shadowObject.size = size;
		shadowObject.shadowType = type;
		objectsWithShadow.Add(shadowObject);
		return shadowObject.shadow;
	}

	private void StartShadows()
	{
		isStarted = true;
	}

	private void Update()
	{
		if (!isStarted)
		{
			return;
		}
		Vector2 from = default(Vector2);
		for (int i = 0; i < objectsWithShadow.Count; i++)
		{
			ShadowObject shadowObject = objectsWithShadow[i];
			if (shadowObject.transform == null)
			{
				objectsWithShadow.Remove(shadowObject);
				shadowObject.Disable();
				shadowStack.Push(shadowObject);
				i--;
				continue;
			}
			if (!shadowObject.gameObject.activeSelf)
			{
				objectsWithShadow.Remove(shadowObject);
				shadowObject.Disable();
				shadowStack.Push(shadowObject);
				i--;
				continue;
			}
			Vector3 position = shadowObject.transform.position;
			Vector3 position2 = shadowObject.shadow.position;
			int num = Physics2D.RaycastNonAlloc(position - Vector3.right + Vector3.up, -Vector2.up, hh1, 8f, layerCast, -1f, 1f);
			int num2 = Physics2D.RaycastNonAlloc(position + Vector3.right + Vector3.up, -Vector2.up, hh2, 8f, layerCast, -1f, 1f);
			if (num > 0 && num2 > 0 && !hh1[0].collider.CompareTag("Untagged") && !hh2[0].collider.CompareTag("Untagged"))
			{
				RaycastHit2D raycastHit2D = hh1[0];
				RaycastHit2D raycastHit2D2 = hh2[0];
				Vector2 point = raycastHit2D2.point;
				float x = point.x;
				Vector2 point2 = raycastHit2D.point;
				float x2 = x - point2.x;
				Vector2 point3 = raycastHit2D2.point;
				float y = point3.y;
				Vector2 point4 = raycastHit2D.point;
				from = new Vector2(x2, y - point4.y);
				float num3 = Vector2.Angle(from, Vector2.right);
				if (from.y < 0f)
				{
					num3 = 360f - num3;
				}
				float x3 = position.x;
				Vector2 point5 = raycastHit2D.point;
				float num4 = (x3 - point5.x) * Mathf.Tan((float)Math.PI / 180f * num3);
				Transform shadow = shadowObject.shadow;
				float x4 = position.x;
				Vector2 point6 = raycastHit2D.point;
				shadow.position = new Vector3(x4, point6.y + num4, position2.z);
				shadowObject.shadow.eulerAngles = new Vector3(0f, 0f, num3);
				float num5 = 0.6f - (position.y - position2.y) / 30f;
				shadowObject.sprite.color = new Color(0f, 0f, 0f, num5);
				if (shadowObject.shadowType == Shadow.ShadowType.SmartSize && (bool)shadowObject.gameObject.GetComponent<Renderer>())
				{
					tk2dSprite sprite = shadowObject.sprite;
					Vector3 size = shadowObject.gameObject.GetComponent<Renderer>().bounds.size;
					sprite.scale = new Vector3(size.x, 4f * shadowObject.size * (num5 + 0.6f));
				}
				if (!shadowObject.active)
				{
					shadowObject.Enable();
				}
			}
			else
			{
				shadowObject.sprite.color = new Color(0f, 0f, 0f, 0f);
			}
		}
	}
}
