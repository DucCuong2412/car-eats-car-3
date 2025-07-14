using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Car2DWeaponModuleBase : Car2DModuleBase
{
	public Transform Body;

	public float Distance = 40f;

	public float StartPower = 400f;

	public float PauseTime = 1f;

	public Barrel _barrel;

	public Action<Transform> ShootCallback;

	public List<Transform> Targets = new List<Transform>();

	private Transform newTarget;

	private Transform MainCar;

	private bool isAI;

	public Barrel _Barrel
	{
		get
		{
			if (_barrel == null)
			{
				_barrel = base.gameObject.AddComponent<Barrel>();
			}
			return _barrel;
		}
	}

	public Transform Target
	{
		get
		{
			newTarget = null;
			for (int i = 0; i < Targets.Count; i++)
			{
				if (Math.Abs(Vector2.Distance(base.transform.position, Targets[i].position)) <= Distance && (newTarget == null || Math.Abs(Vector2.Distance(base.transform.position, Targets[i].position)) < Math.Abs(Vector2.Distance(base.transform.position, newTarget.position))))
				{
					newTarget = Targets[i];
				}
			}
			return newTarget;
		}
	}

	public override void onModuleEnable()
	{
		if (isAI)
		{
			StartCoroutine(AI());
		}
		_Barrel.Enable = true;
	}

	public override void onModuleDisable()
	{
		_Barrel.Enable = false;
	}

	public override void onModuleInited()
	{
	}

	public void Init(Transform _muzzle, Action callback = null, Action<Transform> shootCallback = null)
	{
		_Barrel.callback = callback;
		ShootCallback = shootCallback;
		Body = _muzzle;
		base.moduleInited = true;
	}

	public void Increase(int _count)
	{
		_Barrel.Increase(_count);
	}

	public void AddTarget(Transform _target)
	{
		Targets.Add(_target);
	}

	public void RemoveTarget(Transform _target)
	{
		Targets.Remove(_target);
	}

	private IEnumerator FireVolley(Rigidbody2D[] _items, int _shotPesSec, Vector2 muzzle, Vector2 _dir, float? _force = default(float?))
	{
		foreach (Rigidbody2D i in _items)
		{
			Fire(i, muzzle, _dir, _force);
			yield return new WaitForSeconds(1f / (float)_shotPesSec);
		}
	}

	public void Fire(Rigidbody2D[] items, int shotPesSec, Vector2 muzzle, Vector2 dir, float? force = default(float?))
	{
		StartCoroutine(FireVolley(items, shotPesSec, muzzle, dir, force));
	}

	public void Fire(Rigidbody2D item, Vector2 muzzle, Vector2 dir, float? _force = default(float?))
	{
		if (base.moduleInited && base.moduleEnabled && !(_Barrel.Value <= 0f))
		{
			float d = (!_force.HasValue) ? StartPower : _force.Value;
			item.transform.position = muzzle;
			if (!item.gameObject.activeSelf)
			{
				item.gameObject.SetActive(value: true);
			}
			Rigidbody2D component = Body.GetComponent<Rigidbody2D>();
			if ((bool)component)
			{
				item.velocity = component.velocity;
			}
			item.AddForce(dir * d);
			ShootCallback(item.transform);
			_Barrel.Use(1f);
		}
	}

	public void InitAI(Transform _body, int rocketType = 0, float radius = 0f, Transform _car = null, Action callback = null)
	{
		if (rocketType < 0)
		{
			_Barrel.Value = 0f;
			return;
		}
		_Barrel.callback = callback;
		Body = _body;
		Targets.Add(_car);
		MainCar = _car;
		isAI = true;
		base.moduleInited = true;
		StartCoroutine(AI());
	}

	public IEnumerator AI()
	{
		while (base.moduleEnabled)
		{
			if (!(MainCar != null) || base.moduleEnabled)
			{
			}
			yield return null;
		}
	}
}
