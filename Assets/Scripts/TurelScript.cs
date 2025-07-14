using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurelScript : MonoBehaviour
{
	public bool FixAngleByBody;

	public float Damage = 150f;

	public float ForvardAngle = 180f;

	public float BackAngle = 180f;

	public float AngularSpeed = 5f;

	public float FireTime = 15f;

	public float Force = 45f;

	public Transform Turel;

	public Transform Target;

	public Pool.Bombs Wheezbang;

	public Pool.Explosion Effect;

	public string soundShoot = string.Empty;

	private Vector3 dir = new Vector3(1f, 0f, 0f);

	private float angle;

	private Animation _anim;

	private List<Transform> Shots = new List<Transform>();

	[Header("Daily")]
	public Animation Shoot2Anim;

	[Header("LedoLuch")]
	public float TimeZamozki;

	public Animator luch;

	private int _freezeRay_isOn = Animator.StringToHash("freezeRay_isOn");

	private Car2DWeaponModuleBase _turel;

	private float timeToShoot;

	private int inter;

	private float max;

	private float time;

	private GameObject target;

	private Car2DAIController ai;

	private float startHealth;

	private float startDamage;

	private static string gfx_freeze_02 = "gfx_freeze_02_sn";

	private Animation ShootAnim
	{
		get
		{
			if (_anim == null)
			{
				_anim = base.gameObject.GetComponent<Animation>();
			}
			return _anim;
		}
	}

	private Car2DWeaponModuleBase TurelModule
	{
		get
		{
			if (_turel == null)
			{
				_turel = base.gameObject.AddComponent<Car2DWeaponModuleBase>();
				_turel.Init(base.gameObject.GetComponentInParent<Car2DController>().transform);
				_turel.moduleEnabled = true;
				_turel._Barrel.MaxValue = 15f;
				_turel._Barrel.Value = 15f;
				_turel.Distance = 25f;
				TurelLogic.instance.onHit += OnHit;
			}
			return _turel;
		}
	}

	private void OnEnable()
	{
		if (!UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Contains("Levels"))
		{
			StartCoroutine(CheckTarget());
			if (Effect != 0)
			{
				StartCoroutine(ShotEffect());
			}
		}
	}

	private IEnumerator CheckTarget()
	{
		while (base.gameObject.activeSelf)
		{
			if (TurelModule.Target != null && TurelModule.Target.gameObject.activeSelf)
			{
				Shoot();
			}
			if (!FixAngleByBody)
			{
				Turn(Mathf.Sign(dir.x));
				Aim();
			}
			yield return new WaitForFixedUpdate();
		}
	}

	private IEnumerator ShotEffect()
	{
		while (base.gameObject.activeSelf)
		{
			for (int i = 0; i < Shots.Count; i++)
			{
				if (Shots[i].gameObject.activeSelf)
				{
					Pool.Animate(Effect, Shots[i].position + new Vector3(UnityEngine.Random.Range(0f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f), 0f));
				}
				else
				{
					Shots.Remove(Shots[i]);
				}
			}
			yield return new WaitForSeconds(0.01f);
		}
	}

	private void Shoot()
	{
		if (timeToShoot >= FireTime)
		{
			if (Shoot2Anim != null)
			{
				StartCoroutine(iAnimShoot());
			}
			Vector3 a = Target.position - Turel.position;
			float magnitude = a.magnitude;
			Vector3 v = a / magnitude;
			GameObject @object = Pool.instance.GetObject(Pool.Name(Wheezbang));
			@object.transform.rotation = Turel.rotation;
			if (!Shots.Contains(@object.transform))
			{
				Shots.Add(@object.transform);
			}
			TurelLogic.instance.fireBullets(@object, Target.position, v, Force, 10f);
			timeToShoot = 0f;
			if (soundShoot.Length > 0)
			{
				Audio.PlayAsync(soundShoot);
			}
			if (Wheezbang != Pool.Bombs.freezeRay)
			{
				return;
			}
			for (int i = 0; i < RaceLogic.instance.race.activeAIs.Count; i++)
			{
				if (!RaceLogic.instance.race.activeAIs[i].IsCivic)
				{
					Vector3 position = Target.position;
					float x = position.x;
					Vector3 position2 = RaceLogic.instance.race.activeAIs[i].gameObject.transform.position;
					float num = x - position2.x;
					if (max > num)
					{
						max = num;
						inter = i;
					}
					if (inter <= RaceLogic.instance.race.activeAIs.Count)
					{
						RaceLogic.instance.race.activeAIs[inter].freeze = true;
						GlacierScript component = Pool.instance.spawnAtPoint(Pool.Name(Pool.Bombs.ice), RaceLogic.instance.race.activeAIs[inter].transform).GetComponent<GlacierScript>();
						RaceLogic.instance.race.activeAIs[inter].HealthModule._Barrel.Value = 1f;
						RaceLogic.instance.race.activeAIs[inter].EatDamage = 0f;
						RaceLogic.instance.race.activeAIs[inter].EngineModule.Break(onoff: true);
						StartCoroutine(Shoots());
					}
				}
			}
		}
		else
		{
			timeToShoot += Time.deltaTime;
			if (ShootAnim != null)
			{
				ShootAnim.Play();
			}
		}
	}

	private IEnumerator Shoots()
	{
		if (!RaceLogic.instance.race.activeAIs[inter].IsCivic)
		{
			luch.SetBool(_freezeRay_isOn, value: true);
			yield return new WaitForSeconds(2f);
			if (inter < RaceLogic.instance.race.activeAIs.Count)
			{
				zamorozka(TimeZamozki, Damage, RaceLogic.instance.race.activeAIs[inter].gameObject, RaceLogic.instance.race.activeAIs[inter]);
			}
			yield return new WaitForSeconds(1f);
			luch.SetBool(_freezeRay_isOn, value: false);
		}
	}

	private IEnumerator iAnimShoot()
	{
		Shoot2Anim.Play("gun_shoot");
		while (Shoot2Anim.isPlaying)
		{
			yield return null;
		}
		Shoot2Anim.Play("gun_reload");
		while (Shoot2Anim.isPlaying)
		{
			yield return null;
		}
	}

	private void Aim()
	{
		if (TurelModule.Target != null && TurelModule.Target.gameObject.activeSelf)
		{
			dir = TurelModule.Target.position - Turel.position;
		}
		else
		{
			dir = new Vector3(1f, 0f, 0f);
		}
		angle = Mathf.Atan2(dir.y, dir.x) * 57.29578f;
		Turel.rotation = Quaternion.Slerp(Turel.rotation, Quaternion.AngleAxis((!(angle < 90f) || !(angle > -90f)) ? (Mathf.Sign(angle) * Mathf.Clamp(Mathf.Abs(angle), 180f - BackAngle / 2f, 180f)) : Mathf.Clamp(angle, (0f - ForvardAngle) / 2f, ForvardAngle / 2f), Vector3.forward), Time.deltaTime * AngularSpeed);
	}

	private void Turn(float _dir)
	{
		if (_dir > 0f)
		{
			Transform turel = Turel;
			Vector3 localScale = Turel.localScale;
			float x = localScale.x;
			Vector3 localScale2 = Turel.localScale;
			float y = Mathf.Abs(localScale2.y);
			Vector3 localScale3 = Turel.localScale;
			turel.localScale = new Vector3(x, y, localScale3.z);
		}
		else
		{
			Transform turel2 = Turel;
			Vector3 localScale4 = Turel.localScale;
			float x2 = localScale4.x;
			Vector3 localScale5 = Turel.localScale;
			float y2 = 0f - Mathf.Abs(localScale5.y);
			Vector3 localScale6 = Turel.localScale;
			turel2.localScale = new Vector3(x2, y2, localScale6.z);
		}
	}

	private void OnHit(Transform sh, Transform hitTransform, Vector2 hitPoint)
	{
		if ((bool)hitTransform && Shots.Contains(sh))
		{
			hitTransform.gameObject.SendMessageUpwards("ChangeHealth", 0f - Damage, SendMessageOptions.DontRequireReceiver);
			Shots.Remove(sh);
		}
	}

	public void zamorozka(float _time, float damage, GameObject _target, Car2DAIController _ai)
	{
		time = _time;
		target = _target;
		ai = _ai;
		ai.freeze = true;
		Audio.Play(gfx_freeze_02, 1f);
	}

	private IEnumerator Freeze(Car2DAIController _ai)
	{
		startHealth = _ai.HealthModule._Barrel.Value;
		startDamage = _ai.EatDamage;
		_ai.HealthModule._Barrel.Value = 1f;
		_ai.EatDamage = 0f;
		_ai.EngineModule.Break(onoff: true);
		_ai.freeze = true;
		float t = 0f;
		while (t <= time)
		{
			t += Time.deltaTime;
			base.transform.rotation = target.transform.rotation;
			yield return null;
		}
		_ai.HealthModule._Barrel.Value = startHealth;
		_ai.EatDamage = startDamage;
		if (!target.activeSelf)
		{
			Kill(_ai.HealthModule._Barrel.Value + 1f, _ai);
		}
	}

	private void Kill(float _damage, Car2DAIController _ai)
	{
		_ai.EngineModule.Break(onoff: false);
		_ai.Death();
		for (int i = 0; i < 10; i++)
		{
			Pool.Scrap(Pool.Scraps.ice, target.transform.position, UnityEngine.Random.Range(0, 360), 5f);
		}
	}
}
