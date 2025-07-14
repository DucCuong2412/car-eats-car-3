using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePoolExample : PoolBase
{
	public enum Explosion
	{
		exp1,
		exp2
	}

	public enum Scrups
	{
		scrup1,
		scrup2,
		scrup3,
		scrup4,
		scrup5,
		scrup6,
		scrup7
	}

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
		}
	}

	private static ParticlePoolExample _instance;

	private List<ScrapObject> activeScraps = new List<ScrapObject>();

	private bool isUpdate;

	public static ParticlePoolExample instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject gameObject = new GameObject("ParticlePoolExample");
				_instance = gameObject.AddComponent<ParticlePoolExample>();
				_instance.CreatePool(gameObject);
			}
			return _instance;
		}
		set
		{
			_instance = value;
		}
	}

	public void Init()
	{
		Add("ParticlePoolExampleParticle", 5);
	}

	public GameObject Animate(Explosion exp, Vector3 position, string layer = null)
	{
		return spawnAtPoint(getParticleName(exp), position);
	}

	public GameObject Animate(Scrups exp, Vector3 position, string layer = null)
	{
		return spawnAtPoint(getScrapName(exp), position);
	}

	public GameObject Scrup(Scrups scrap, Vector2 startPosition, float angle, float force, float lifeTime, bool rotate = true, float gravity = 0.1f, string layer = null)
	{
		GameObject @object = GetObject(getScrapName(scrap));
		return _animateScrap(@object, startPosition, angle, force, lifeTime, rotate, gravity);
	}

	public string getParticleName(Explosion exp)
	{
		switch (exp)
		{
		case Explosion.exp1:
			return "ParticlePoolExampleParticle";
		case Explosion.exp2:
			return "TurboParticle";
		default:
			return null;
		}
	}

	public string getScrapName(Scrups scrup)
	{
		switch (scrup)
		{
		case Scrups.scrup1:
			return "ParticlePoolExampleParticle";
		case Scrups.scrup2:
			return "effect1_4";
		case Scrups.scrup3:
			return "Spark";
		case Scrups.scrup4:
			return "effect3_2";
		case Scrups.scrup5:
			return "Sprite0";
		case Scrups.scrup6:
			return "playrturbo";
		case Scrups.scrup7:
			return "enemyturbo";
		default:
			return null;
		}
	}

	public virtual GameObject _animateScrap(GameObject obj, Vector2 startPosition, float angle, float force, float lifeTime, bool rotate = true, float gravity = 0.1f)
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
			scrapObject.force.x *= Random.Range(1f, 3f);
			scrapObject.force.y *= Random.Range(1f, 3f);
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

	public IEnumerator updateScraps()
	{
		isUpdate = true;
		while (true)
		{
			for (int i = 0; i < activeScraps.Count; i++)
			{
				ScrapObject scrapObject = activeScraps[i];
				scrapObject.transform.position = scrapObject.transform.position + scrapObject.force * Time.deltaTime;
				if (scrapObject.rotate)
				{
					Transform transform = scrapObject.transform;
					Vector3 axis = new Vector3(0f, 0f, 1f);
					Quaternion rotation = scrapObject.transform.rotation;
					transform.Rotate(axis, rotation.z + scrapObject.force.x);
				}
				scrapObject.force.y -= 0.05f + scrapObject.iter;
				scrapObject.iter += scrapObject.gravity * 0.1f;
				scrapObject.time += Time.deltaTime;
				if (scrapObject.time >= scrapObject.lifeTime)
				{
					scrapObject.transform.gameObject.SetActive(value: false);
					activeScraps.RemoveAt(i);
					i--;
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
}
