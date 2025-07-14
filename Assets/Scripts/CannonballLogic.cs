using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballLogic : MonoBehaviour
{
	private class Object
	{
		public Action<Transform, Vector2> action;

		public Transform transform;

		public Vector2 position;

		public Transform hitTransform;

		public Vector2 positionInPreviusFrame;

		public Vector2 positionInPreviusPreviusFrame;

		public float gravity;

		public float lifeTime;

		public float time;

		public Vector2 force;

		public float iter;

		public Object(Transform t)
		{
			transform = t;
		}
	}

	private static CannonballLogic _instance;

	private List<Object> activeObjects = new List<Object>();

	private bool isUpdate;

	public static CannonballLogic instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject gameObject = new GameObject("_CannonLogicUpdate");
				_instance = gameObject.AddComponent<CannonballLogic>();
			}
			return _instance;
		}
		set
		{
			_instance = value;
		}
	}

	public virtual GameObject fireBullets(Action<Transform, Vector2> action, GameObject go, Vector2 startPosition, Vector2 dir, float force, float lifeTime, float gravity = 0f, float time = 1f)
	{
		Object @object = new Object((!go) ? null : go.transform);
		@object.time = time;
		@object.action = action;
		@object.gravity = gravity;
		@object.lifeTime = lifeTime;
		@object.force = dir * force;
		@object.iter = gravity * 0.1f;
		@object.position = startPosition;
		@object.positionInPreviusFrame = startPosition;
		@object.positionInPreviusPreviusFrame = startPosition;
		if (@object.transform != null)
		{
			@object.transform.position = startPosition;
			@object.transform.gameObject.SetActive(value: true);
		}
		activeObjects.Add(@object);
		if (@object != null)
		{
			if (!isUpdate)
			{
				StartCoroutine(update());
			}
			return (!(@object.transform != null)) ? null : @object.transform.gameObject;
		}
		return null;
	}

	public IEnumerator update()
	{
		isUpdate = true;
		while (true)
		{
			for (int i = 0; i < activeObjects.Count; i++)
			{
				Object obj = activeObjects[i];
				obj.position += obj.force * Time.deltaTime;
				obj.force.y -= obj.iter;
				obj.iter += obj.gravity;
				obj.time += Time.deltaTime;
				RaycastHit2D raycastHit2D = Physics2D.Linecast(obj.positionInPreviusFrame, obj.position);
				if ((bool)raycastHit2D.transform || obj.time >= obj.lifeTime)
				{
					obj.time = obj.lifeTime;
					obj.hitTransform = raycastHit2D.transform;
					obj.position = raycastHit2D.point;
					if (obj.transform != null)
					{
						obj.transform.position = obj.position;
						obj.transform.gameObject.SetActive(value: false);
					}
					Transform transform = raycastHit2D.transform;
					if (transform != null)
					{
						while (transform.transform.name.Contains("Wheel"))
						{
							if (transform.parent != null)
							{
								transform = transform.parent;
								continue;
							}
							yield break;
						}
						if (obj.action != null)
						{
							obj.action(transform, obj.position);
						}
					}
					activeObjects.RemoveAt(i);
					i--;
				}
				else
				{
					obj.positionInPreviusPreviusFrame = obj.positionInPreviusFrame;
					obj.positionInPreviusFrame = obj.position;
					if (obj.transform != null)
					{
						obj.transform.position = obj.position;
					}
				}
			}
			if (activeObjects.Count <= 0)
			{
				break;
			}
			yield return new WaitForFixedUpdate();
		}
		isUpdate = false;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		//foreach (UnityEngine.Object activeObject in activeObjects)
		//{
		//	Gizmos.DrawLine(activeObject.positionInPreviusPreviusFrame, activeObject.positionInPreviusFrame);
		//}
	}
}
