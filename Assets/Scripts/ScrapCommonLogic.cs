using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapCommonLogic : MonoBehaviour
{
	private class ScrapObject
	{
		public Transform transform;

		public float gravity;

		public float lifeTime;

		public bool rotate;

		public float time;

		public Vector3 force;

		public float iter;

		public ScrapObject(Transform t)
		{
			transform = t;
			Transform obj = transform;
			Vector3 position = t.position;
			float x = position.x;
			Vector3 position2 = t.position;
			obj.position = new Vector3(x, position2.y, 0f);
		}

		public ScrapObject(Transform t, float lifeTime)
		{
			transform = t;
			this.lifeTime = lifeTime;
		}
	}

	private static ScrapCommonLogic _instance;

	private List<ScrapObject> activeScraps = new List<ScrapObject>();

	private List<ScrapObject> activePhysicsScraps = new List<ScrapObject>();

	private bool isUpdate;

	private bool isUpdatePhys;

	public static ScrapCommonLogic instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject gameObject = new GameObject("_scrapListUpdate");
				_instance = gameObject.AddComponent<ScrapCommonLogic>();
			}
			return _instance;
		}
		set
		{
			_instance = value;
		}
	}

	public virtual GameObject animateScrap(GameObject obj, Vector2 startPosition, float angle, float force, float lifeTime, bool rotate = true, float gravity = 0.1f)
	{
		ScrapObject scrapObject = new ScrapObject(obj.transform);
		if (!scrapObject.transform.gameObject.activeSelf)
		{
			scrapObject.gravity = gravity;
			scrapObject.lifeTime = lifeTime;
			scrapObject.time = 0f;
			scrapObject.rotate = rotate;
			scrapObject.force = Quaternion.AngleAxis(angle, Vector3.forward) * new Vector3(force, force, 1f);
			ref Vector3 force2 = ref scrapObject.force;
			Vector3 position = scrapObject.transform.position;
			force2.z = position.z;
			scrapObject.force.x *= UnityEngine.Random.Range(1f, 3f);
			scrapObject.force.y *= UnityEngine.Random.Range(1f, 3f);
			scrapObject.iter = gravity * 0.1f;
			scrapObject.transform.localPosition = startPosition;
			scrapObject.transform.gameObject.SetActive(value: true);
			activeScraps.Add(scrapObject);
		}
		if (scrapObject != null)
		{
			if (!isUpdate)
			{
				StartCoroutine(updateScraps());
			}
			return scrapObject.transform.gameObject;
		}
		return null;
	}

	public virtual GameObject animateScrap(GameObject obj, Vector2 startPosition, Vector2 force, float lifeTime)
	{
		obj.transform.position = startPosition;
		obj.SetActive(value: true);
		obj.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
		ScrapObject scrapObject = new ScrapObject(obj.transform, lifeTime);
		activePhysicsScraps.Add(scrapObject);
		if (scrapObject != null)
		{
			if (!isUpdatePhys)
			{
				StartCoroutine(updatePhysicsScraps());
			}
			return scrapObject.transform.gameObject;
		}
		return null;
	}

	public IEnumerator updateScraps()
	{
		isUpdate = true;
		while (true)
		{
			for (int i = 0; i < activeScraps.Count; i++)
			{
				ScrapObject scrapObject = activeScraps[i];
				if (scrapObject != null)
				{
					scrapObject.transform.position = scrapObject.transform.position + scrapObject.force * Time.deltaTime;
					if (scrapObject.rotate)
					{
						Transform transform = scrapObject.transform;
						Vector3 axis = new Vector3(0f, 0f, 1f);
						Quaternion rotation = scrapObject.transform.rotation;
						transform.Rotate(axis, rotation.z + scrapObject.force.x);
					}
					scrapObject.force.y -= scrapObject.iter;
					scrapObject.iter += scrapObject.gravity * 0.1f;
					scrapObject.time += Time.deltaTime;
					if (scrapObject.time >= scrapObject.lifeTime)
					{
						scrapObject.transform.gameObject.SetActive(value: false);
						activeScraps.RemoveAt(i);
						i--;
					}
				}
			}
			if (activeScraps.Count <= 0)
			{
				break;
			}
			yield return new WaitForFixedUpdate();
		}
		isUpdate = false;
	}

	public IEnumerator updatePhysicsScraps()
	{
		isUpdatePhys = true;
		while (true)
		{
			for (int i = 0; i < activePhysicsScraps.Count; i++)
			{
				ScrapObject scrapObject = activePhysicsScraps[i];
				scrapObject.time += Time.unscaledDeltaTime;
				if (scrapObject.time >= scrapObject.lifeTime)
				{
					scrapObject.transform.gameObject.SetActive(value: false);
					activePhysicsScraps.RemoveAt(i);
					i--;
				}
			}
			if (activePhysicsScraps.Count <= 0)
			{
				break;
			}
			yield return new WaitForFixedUpdate();
		}
		isUpdatePhys = false;
	}
}
