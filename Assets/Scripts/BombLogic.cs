using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLogic : BombCommonLogic
{
	public enum types
	{
		DEFAULT,
		FRAGMENT,
		FRAGMENT_FIRE,
		FRAGMENT_ELECTRO,
		CLUSTER,
		GRAVITY,
		REDUSER
	}

	public class Object
	{
		public float time;

		public Transform transform;

		public types type;

		public float lifeTime = 1f;

		public float radius = 15f;

		public float impulse = 20000f;

		public float damage = 10f;

		public int fragmentCount;

		public Pool.Scraps fragmentSprite;

		public int fragmentSpeed;

		public float fragmentGravityScale = 0.05f;

		public float fragmentCloudWidth = 1f;

		public int fragmentLifeTime;

		public int fragmentDamage;

		public float efectDamage;

		public float efectDuration;

		public bool mainCarCollision;

		public Object(Transform t)
		{
			time = 0f;
			transform = t;
		}
	}

	private class cc
	{
		public Transform fire;

		public Transform target;

		public float dist;

		public float angle;

		public float time;
	}

	private static BombLogic _instance;

	private List<Object> activeObjects = new List<Object>();

	private bool isUpdate;

	private bool isMainCarDamaged;

	private int exp1FragmentDamage = 10;

	private int exp2FragmentDamage = 10;

	private float fireDamage = 10f;

	private float fireDuration = 10f;

	private int exp3FragmentDamage = 10;

	private float shockWaveDamage = 10f;

	private float shockWaveDuration = 10f;

	private int exp4FragmentDamage = 10;

	private int exp5FragmentDamage = 10;

	private List<cc> lis = new List<cc>();

	public static BombLogic instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject gameObject = new GameObject("_bombListUpdate");
				_instance = gameObject.AddComponent<BombLogic>();
			}
			return _instance;
		}
		set
		{
			_instance = value;
		}
	}

	private void explosion(Object _obj)
	{
		Explosion(_obj.transform.position, _obj.radius, _obj.impulse, _obj.damage, _obj.type);
		Audio.Stop("gfx_bomb_01_sn");
		switch (_obj.type)
		{
		case types.DEFAULT:
			Pool.Animate(Pool.Explosion.exp25, _obj.transform.position);
			break;
		case types.FRAGMENT:
			Pool.Animate(Pool.Explosion.exp25, _obj.transform.position);
			exp1FragmentDamage = _obj.fragmentDamage;
			isMainCarDamaged = _obj.mainCarCollision;
			setFragments(_obj, expl1);
			break;
		case types.FRAGMENT_FIRE:
			Pool.Animate(Pool.Explosion.exp28, _obj.transform.position);
			fireDamage = _obj.efectDamage;
			fireDuration = _obj.efectDuration;
			exp2FragmentDamage = _obj.fragmentDamage;
			isMainCarDamaged = _obj.mainCarCollision;
			setFragments(_obj, expl2);
			break;
		case types.FRAGMENT_ELECTRO:
			Pool.Animate(Pool.Explosion.exp9, _obj.transform.position);
			shockWaveDamage = _obj.efectDamage;
			shockWaveDuration = _obj.efectDuration;
			exp3FragmentDamage = _obj.fragmentDamage;
			isMainCarDamaged = _obj.mainCarCollision;
			setFragments(_obj, expl3);
			Audio.PlayAsync("bullet_electrick");
			break;
		case types.CLUSTER:
			Pool.Animate(Pool.Explosion.exp25, _obj.transform.position);
			exp4FragmentDamage = _obj.fragmentDamage;
			isMainCarDamaged = _obj.mainCarCollision;
			setFragments(_obj, expl4);
			break;
		case types.GRAVITY:
			exp5FragmentDamage = _obj.fragmentDamage;
			setFragments(_obj, expl5);
			Pool.Animate(Pool.Explosion.exp29, _obj.transform.position);
			break;
		case types.REDUSER:
			Pool.Animate(Pool.Explosion.exp25, _obj.transform.position);
			exp1FragmentDamage = _obj.fragmentDamage;
			isMainCarDamaged = _obj.mainCarCollision;
			break;
		}
	}

	public GameObject bomb(Transform obj, float dmg = -1f)
	{
		Object @object = new Object(obj.transform);
		BombItem component = obj.GetComponent<BombItem>();
		if ((bool)component)
		{
			@object.type = component.parameters.type;
			@object.lifeTime = component.parameters.lifeTime;
			@object.radius = component.parameters.radius;
			@object.impulse = component.parameters.impulse;
			if (Progress.settings.x2damage)
			{
				@object.damage = component.parameters.maxDamage * 2f;
			}
			else if (Progress.settings.ReduceDamage)
			{
				@object.damage = component.parameters.maxDamage / 2f;
			}
			else
			{
				@object.damage = component.parameters.maxDamage;
			}
			@object.fragmentCount = component.parameters.fragmentCount;
			@object.fragmentSprite = component.parameters.fragmentSprite;
			@object.fragmentSpeed = component.parameters.fragmentSpeed;
			@object.fragmentLifeTime = component.parameters.fragmentLifeTime;
			@object.fragmentDamage = component.parameters.fragmentDamage;
			@object.fragmentGravityScale = component.parameters.fragmentGravityScale;
			@object.fragmentCloudWidth = component.parameters.fragmentCloudWidth;
			@object.efectDamage = component.parameters.efectDamage;
			@object.efectDuration = component.parameters.efectDuration;
			@object.mainCarCollision = component.parameters.mainCarCollision;
		}
		else
		{
			UnityEngine.Debug.Log("Use default bomb settings, because BombItem component does'n finded!");
		}
		if (dmg != -1f)
		{
			@object.damage = dmg;
		}
		if (@object != null)
		{
			activeObjects.Add(@object);
			if (!isUpdate)
			{
				StartCoroutine(update());
			}
			return @object.transform.gameObject;
		}
		return null;
	}

	private void setFragments(Object _obj, Action<Transform, Vector2> act)
	{
		for (int i = 0; i < _obj.fragmentCount; i++)
		{
			GameObject @object = Pool.instance.GetObject(Pool.Name(_obj.fragmentSprite));
			Vector2 dir = new Vector2(UnityEngine.Random.Range(0f - _obj.fragmentCloudWidth, _obj.fragmentCloudWidth), UnityEngine.Random.Range(0.35f, 1f));
			CannonballLogic.instance.fireBullets(act, @object, _obj.transform.position, dir, _obj.fragmentSpeed, _obj.fragmentLifeTime, _obj.fragmentGravityScale);
		}
	}

	private void setFragmentDamage(Transform t, int damage)
	{
		if (t != null && (isMainCarDamaged || (!(t.tag == "CarMain") && !(t.tag == "CarMainChild") && !(t.tag == "Wheels") && !(t.tag == "Untagged"))))
		{
			t.SendMessageUpwards("ChangeHealth", -damage, SendMessageOptions.DontRequireReceiver);
		}
	}

	private void expl1(Transform _hitTransform, Vector2 _hitPoint)
	{
		setFragmentDamage(_hitTransform, exp1FragmentDamage);
		Pool.Animate(Pool.Explosion.exp7, _hitPoint);
	}

	private void expl2(Transform _hitTransform, Vector2 _hitPoint)
	{
		setFragmentDamage(_hitTransform, exp2FragmentDamage);
		if ((bool)_hitTransform)
		{
			GameObject gameObject = Pool.Animate(Pool.Explosion.exp7, _hitPoint, "GUI");
			cc cc = new cc();
			cc.fire = gameObject.transform;
			cc.target = _hitTransform;
			cc.dist = Vector3.Distance(_hitTransform.position, _hitPoint);
			cc cc2 = cc;
			Vector3 position = _hitTransform.position;
			float y = position.y - _hitPoint.y;
			Vector3 position2 = _hitTransform.position;
			cc2.angle = Mathf.Atan2(y, position2.x - _hitPoint.x);
			StartCoroutine(cc22(cc));
		}
	}

	private void expl3(Transform _hitTransform, Vector2 _hitPoint)
	{
		setFragmentDamage(_hitTransform, exp3FragmentDamage);
		if ((bool)_hitTransform)
		{
			Car2DAIController component = _hitTransform.GetComponent<Car2DAIController>();
			if (_hitTransform.gameObject.tag == "CarEnemy" && component != null)
			{
				StartCoroutine(ZmenshuvachAI(component));
			}
			Car2DController component2 = _hitTransform.GetComponent<Car2DController>();
			if (isMainCarDamaged && _hitTransform.gameObject.tag == "CarMain" && component2 != null)
			{
				StartCoroutine(ZmenshuvachMain(component2));
			}
		}
	}

	private void expl4(Transform _hitTransform, Vector2 _hitPoint)
	{
		setFragmentDamage(_hitTransform, exp4FragmentDamage);
		Pool.Animate(Pool.Explosion.exp25, _hitPoint);
	}

	private void expl5(Transform _hitTransform, Vector2 _hitPoint)
	{
		setFragmentDamage(_hitTransform, exp5FragmentDamage);
		Pool.Animate(Pool.Explosion.exp25, _hitPoint);
	}

	private void expl6(Transform _hitTransform, Vector2 _hitPoint)
	{
		if ((bool)_hitTransform)
		{
			Car2DAIController component = _hitTransform.GetComponent<Car2DAIController>();
			if (_hitTransform.gameObject.tag == "CarEnemy" && component != null)
			{
				StartCoroutine(ZmenshuvachAI(component));
			}
			Car2DController component2 = _hitTransform.GetComponent<Car2DController>();
			if (isMainCarDamaged && _hitTransform.gameObject.tag == "CarMain" && component2 != null)
			{
				StartCoroutine(ZmenshuvachMain(component2));
			}
		}
	}

	private IEnumerator cc22(cc _c)
	{
		lis.Add(_c);
		Transform temp_parrent = _c.fire.parent;
		float damage = fireDamage;
		float duration = fireDuration;
		if (_c.target.tag != "Wheels")
		{
			_c.fire.SetParent(_c.target);
		}
		for (int i = 0; i < 7; i++)
		{
			yield return new WaitForSeconds(duration / 7f);
			if (!(_c.target == null) && (isMainCarDamaged || (!(_c.target.tag == "CarMain") && !(_c.target.tag == "CarMainChild") && !(_c.target.tag == "Wheels"))) && !(_c.target.tag != "CarMain") && !(_c.target.tag != "CarMainChild") && !(_c.target.tag != "Wheels"))
			{
				_c.target.SendMessageUpwards("ChangeHealth", (0f - damage) / 7f, SendMessageOptions.DontRequireReceiver);
				if (!_c.target.gameObject.activeSelf)
				{
					_c.fire.gameObject.SetActive(value: false);
					_c.fire.SetParent(temp_parrent);
				}
			}
		}
		lis.Remove(_c);
		_c.fire.GetComponent<ParticleSystem>().Stop(withChildren: true);
		_c.fire.gameObject.SetActive(value: false);
		_c.fire.SetParent(temp_parrent);
	}

	private void UpdateFire()
	{
		for (int i = 0; i < lis.Count; i++)
		{
			if (!(lis[i].target == null))
			{
				Vector3 eulerAngles = lis[i].target.eulerAngles;
				float num = eulerAngles.z * (float)Math.PI / 180f - (float)Math.PI;
				Vector3 position = lis[i].target.position;
				float x = position.x + Mathf.Cos(num + lis[i].angle) * lis[i].dist;
				Vector3 position2 = lis[i].target.position;
				float y = position2.y + Mathf.Sin(num + lis[i].angle) * lis[i].dist;
				lis[i].fire.position = new Vector3(x, y);
			}
		}
	}

	private IEnumerator ZmenshuvachAI(Car2DAIController hitC)
	{
		if (hitC != null && hitC.gameObject.activeSelf)
		{
			hitC.gameObject.SetActive(value: false);
			yield return null;
		}
	}

	private IEnumerator ZmenshuvachMain(Car2DController hitC)
	{
		yield return null;
	}

	private IEnumerator shockWave(Car2DAIController hitC)
	{
		GameObject eff = Pool.Animate(Pool.Explosion.exp2, hitC.transform, "GUI");
		if (hitC != null && hitC.gameObject.activeSelf)
		{
			hitC.EngineModule.moduleEnabled = false;
			hitC.EngineModule.Break(onoff: true);
		}
		float damage = shockWaveDamage;
		float duration = shockWaveDuration;
		for (int i = 0; i < 10; i++)
		{
			if (!hitC.gameObject.activeSelf)
			{
				break;
			}
			int dam = (int)(0f - ((!(damage / 10f < 1f)) ? (damage / 10f) : 1f));
			hitC.HealthModule.ChangeHealth(dam);
			yield return new WaitForSeconds(duration / 10f);
		}
		eff.GetComponent<ParticleSystem>().Stop(withChildren: true);
		if (hitC.gameObject.activeSelf)
		{
			hitC.EngineModule.Break(onoff: false);
			hitC.EngineModule.moduleEnabled = true;
		}
	}

	private IEnumerator shockWave(Car2DController hitC)
	{
		GameObject eff = Pool.Animate(Pool.Explosion.exp2, hitC.transform, "GUI");
		if (hitC != null && hitC.gameObject.activeSelf)
		{
			hitC.EngineModule.moduleEnabled = false;
			hitC.EngineModule.Break(onoff: true);
		}
		float damage = shockWaveDamage;
		float duration = shockWaveDuration;
		for (int i = 0; i < 10; i++)
		{
			if (!hitC.gameObject.activeSelf)
			{
				break;
			}
			int dam = (int)(0f - ((!(damage / 10f < 1f)) ? (damage / 10f) : 1f));
			hitC.HealthModule.ChangeHealth(dam);
			yield return new WaitForSeconds(duration / 10f);
		}
		eff.GetComponent<ParticleSystem>().Stop(withChildren: true);
		if (hitC.gameObject.activeSelf)
		{
			hitC.EngineModule.Break(onoff: false);
			hitC.EngineModule.moduleEnabled = true;
		}
	}

	public void OnCollision(Transform tr)
	{
		List<Object> list = new List<Object>();
		list = activeObjects;
		Audio.PlayAsync("exp_tnt_05_sn");
		int num = 0;
		Object @object;
		while (true)
		{
			if (num < list.Count)
			{
				@object = list[num];
				if (@object.transform.Equals(tr))
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		if (!tr.CompareTag("CarMain") && !tr.CompareTag("CarMainChild"))
		{
			@object.time = @object.lifeTime;
		}
	}

	private void Update()
	{
		UpdateFire();
	}

	private IEnumerator update()
	{
		isUpdate = true;
		while (true)
		{
			for (int i = 0; i < activeObjects.Count; i++)
			{
				Object tempObject = activeObjects[i];
				tempObject.time += Time.deltaTime;
				if (tempObject.time >= tempObject.lifeTime)
				{
					tempObject.transform.gameObject.SetActive(value: false);
					activeObjects.RemoveAt(i);
					explosion(tempObject);
					i--;
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
		Gizmos.color = Color.yellow;
		foreach (Object activeObject in activeObjects)
		{
			Gizmos.DrawWireSphere(activeObject.transform.position, activeObject.radius);
		}
	}
}
