using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurelLogic : MonoBehaviour
{
	private class Object
	{
		public Transform transform;

		public Transform hitTransform;

		public Vector3 previusFramePosition;

		public float gravity;

		public float lifeTime;

		public float time;

		public Vector3 force;

		public float iter;

		public bool ai;

		public Object(Transform t)
		{
			transform = t;
		}
	}

	public delegate void onHitDelegate(Transform bullet, Transform hitTransform, Vector2 hitPoint);

	private static TurelLogic _instance;

	private List<Object> activeObjects = new List<Object>();

	private bool isUpdate;

	public static TurelLogic instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject gameObject = new GameObject("_TurelLogicUpdate");
				_instance = gameObject.AddComponent<TurelLogic>();
			}
			return _instance;
		}
		set
		{
			_instance = value;
		}
	}

	public event onHitDelegate onHit;

	public virtual GameObject fireBullets(GameObject _obj, Vector2 startPosition, Vector2 dir, float force, float lifeTime, float gravity = 0f, bool ai = false)
	{
		Object @object = new Object(_obj.transform);
		if (!@object.transform.gameObject.activeSelf)
		{
			@object.gravity = gravity;
			@object.lifeTime = lifeTime;
			@object.force = dir * force;
			@object.iter = gravity * 0.1f;
			@object.transform.position = startPosition;
			@object.previusFramePosition = startPosition;
			@object.ai = ai;
			@object.transform.gameObject.SetActive(value: true);
			activeObjects.Add(@object);
		}
		if (@object != null)
		{
			if (!isUpdate)
			{
				StartCoroutine(update());
			}
			return @object.transform.gameObject;
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
				obj.transform.position += obj.force * Time.deltaTime;
				obj.force.y -= obj.iter;
				obj.iter += obj.gravity;
				obj.time += Time.deltaTime;
				RaycastHit2D raycastHit2D = obj.ai ? Physics2D.Linecast(obj.previusFramePosition, obj.transform.position, 1 << LayerMask.NameToLayer("Car")) : Physics2D.Linecast(obj.previusFramePosition, obj.transform.position, 1 << LayerMask.NameToLayer("EnemyCar"));
				if ((bool)raycastHit2D.transform || obj.time >= obj.lifeTime)
				{
					obj.time = obj.lifeTime;
					obj.hitTransform = raycastHit2D.transform;
					obj.transform.position = raycastHit2D.point;
					if (this.onHit != null)
					{
						this.onHit(obj.transform, raycastHit2D.transform, obj.transform.position);
					}
					obj.transform.gameObject.SetActive(value: false);
					activeObjects.RemoveAt(i);
					i--;
				}
				else
				{
					obj.previusFramePosition = obj.transform.position;
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
}
