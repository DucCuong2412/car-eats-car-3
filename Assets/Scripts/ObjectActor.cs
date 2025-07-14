using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObjectActor : MonoBehaviour
{
	public enum enCollSound
	{
		none,
		actor_creature_01_sn,
		actor_creature_02_sn,
		actor_creature_03_sn,
		crash_metal_01_sn,
		crash_metal_02_sn,
		warm
	}

	public enum enCrashSound
	{
		none,
		crash_metal_01_sn,
		crash_metal_02_sn,
		crash_rock_04_sn,
		crash_wood_01_sn,
		crash_wood_02_sn,
		glass_green_kolby
	}

	[Serializable]
	public class Scrap
	{
		public Pool.Scraps scrap;

		public int num = 2;
	}

	[Serializable]
	public class ScrapDynamicObject
	{
		public Pool.ScrapDynamic scrap;

		public int num;

		public ScrapDynamicObject Clone()
		{
			return (ScrapDynamicObject)MemberwiseClone();
		}
	}

	[Serializable]
	public class Explosion
	{
		public Pool.Explosion exp;

		public int num = 2;
	}

	public bool DestroyObj;

	public bool Active;

	[Range(0f, 1000f)]
	public int Health = 1000;

	[Range(0f, 1000f)]
	public float Damage;

	public bool _immortal;

	public enCollSound CollisionSound;

	public enCrashSound CrashSound;

	public Rigidbody2D Rb2d;

	public List<Scrap> scraps = new List<Scrap>();

	public List<ScrapDynamicObject> scrapsDynamicObject = new List<ScrapDynamicObject>();

	public List<Explosion> explosions = new List<Explosion>();

	public Shadow.ShadowType shadowType = Shadow.ShadowType.SmartSize;

	public float shadowSize = 0.5f;

	[Header("DONT TOUCH THIS")]
	public float TestDamage;

	private void Update()
	{
		if (DestroyObj)
		{
			ObjectDestroy();
			DestroyObj = false;
		}
		if (Active)
		{
			base.gameObject.SetActive(value: true);
			Active = false;
		}
		if (Rb2d != null && !_immortal)
		{
			if (Rb2d.mass <= 0.5f)
			{
				_immortal = true;
			}
			else
			{
				_immortal = false;
			}
		}
	}

	private void SetSoudnColl(enCollSound snd)
	{
		if (snd != 0)
		{
			Audio.PlayAsync(snd.ToString(), 1f);
		}
	}

	private void SetSoudnExp(enCrashSound snd)
	{
		if (snd != 0)
		{
			Audio.PlayAsync(snd.ToString(), 1f);
		}
	}

	private void OnEnable()
	{
		Rb2d = base.gameObject.GetComponent<Rigidbody2D>();
		if ((float)Health <= 0.5f)
		{
			_immortal = true;
		}
		else
		{
			_immortal = false;
		}
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag("CarMain") || coll.gameObject.CompareTag("CarMainChild") || coll.gameObject.CompareTag("Wheels"))
		{
			OnHit(50f);
		}
		if (coll.gameObject.CompareTag("CarMain") || coll.gameObject.CompareTag("CarMainChild") || coll.gameObject.CompareTag("Wheels"))
		{
			if (Progress.shop.ArenaNew)
			{
				if (RaceLogic.instance.gui.interface_PositionBar.Distance > 5000f)
				{
					TestDamage = Damage + Damage * (RaceLogic.instance.gui.interface_PositionBar.Distance / 1000f);
					RaceLogic.instance.HitMainCar(TestDamage);
				}
				else
				{
					RaceLogic.instance.HitMainCar(Damage);
				}
			}
			else
			{
				RaceLogic.instance.HitMainCar(Damage);
			}
		}
		if (coll.gameObject.CompareTag("CarEnemy"))
		{
			coll.gameObject.SendMessageUpwards("ChangeHealth", 0f - Damage, SendMessageOptions.DontRequireReceiver);
		}
		SetSoudnColl(CollisionSound);
	}

	private void OnTriggrEnter(Collider2D coll)
	{
		if (coll.CompareTag("EnemyCar") && coll.GetComponent<EatSensor>() != null)
		{
			OnHit(coll.GetComponentInParent<Car2DAIController>().EatDamage);
		}
	}

	public void OnHit(float magnitude)
	{
		if (!_immortal)
		{
			Health -= (int)magnitude;
			HealthCheck();
		}
	}

	private void HealthCheck()
	{
		if (Health <= 0)
		{
			Health = 0;
			ObjectDestroy();
		}
	}

	private void ObjectDestroy()
	{
		if (scraps.Count > 0)
		{
			for (int i = 0; i < scraps.Count; i++)
			{
				for (int j = 0; j < scraps[i].num; j++)
				{
					Pool.Scrap(scraps[i].scrap, base.transform.position, UnityEngine.Random.Range(-1, 361), 2f, 2f);
				}
			}
		}
		Pool.ScrapDynamicDeco(this);
		base.gameObject.SetActive(value: false);
		if (RaceManager.instance != null && RaceManager.instance.isStarted)
		{
			SetSoudnExp(CrashSound);
		}
	}
}
