using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car2DAnimatorSkiner : MonoBehaviour
{
	public class Call
	{
		public int progress;

		public string animname;

		public int boolname;
	}

	private Animator CarAnimator;

	private Animation SkinChanging;

	public List<Call> Calls = new List<Call>();

	private int _Shoot = Animator.StringToHash("Shoot");

	private static string str_TurboUpgrades = "TurboUpgrades";

	private int _UseTurbo = Animator.StringToHash("UseTurbo");

	private static string str_HullUpgrades = "HullUpgrades";

	private static string str_PreHullChange = "PreHullChange";

	private int _HullBack = Animator.StringToHash("HullBack");

	private int _ChangeHull = Animator.StringToHash("ChangeHull");

	private static string str_HullDamage = "HullDamage";

	private int _Damaged = Animator.StringToHash("Damaged");

	private static string str_EngineUpgrades = "EngineUpgrades";

	private int _ChangeEngine = Animator.StringToHash("ChangeEngine");

	private static string str_WheelsUpgrades = "WheelsUpgrades";

	private int _ChangeWheels = Animator.StringToHash("ChangeWheels");

	private int currHealthProgress = -1;

	private bool isWorking;

	public void Init()
	{
		CarAnimator = base.gameObject.GetComponentInChildren<Animator>();
		if (CarAnimator != null)
		{
			SkinChanging = CarAnimator.gameObject.GetComponentInChildren<Animation>();
		}
	}

	public void OnTurbo(bool Use)
	{
		CarAnimator.SetBool(_UseTurbo, Use);
	}

	public void OnShoot(bool Use)
	{
		CarAnimator.SetBool(_Shoot, Use);
	}

	public void ChangeTurbo(int progress, bool fast = false)
	{
		if (fast)
		{
			AddCall(progress, str_TurboUpgrades);
		}
		else
		{
			AddCall(progress, str_TurboUpgrades, _UseTurbo);
		}
	}

	public void ChangeHull(int progress, bool fast = false)
	{
		if (fast)
		{
			AddCall(progress, str_HullUpgrades);
			return;
		}
		AddCall((currHealthProgress == -1 || currHealthProgress <= progress) ? progress : (progress + 1), str_PreHullChange);
		AddCall(progress, str_HullUpgrades, (currHealthProgress == -1 || currHealthProgress <= progress) ? _ChangeHull : _HullBack);
		currHealthProgress = progress;
	}

	public void DamageHull(float damage)
	{
		if (base.gameObject.activeSelf)
		{
			int degreeDamage = GetDegreeDamage(damage);
			StartCoroutine(ChangeDetail(degreeDamage, str_HullDamage, _Damaged));
		}
	}

	private int GetDegreeDamage(float p)
	{
		if (p < 30f)
		{
			return 2;
		}
		if (p < 60f)
		{
			return 1;
		}
		return 0;
	}

	public void ChangeEngine(int progress, bool fast = false)
	{
		if (fast)
		{
			AddCall(progress, str_EngineUpgrades);
		}
		else
		{
			AddCall(progress, str_EngineUpgrades, _ChangeEngine);
		}
	}

	public void ChangeWheels(int progress, bool fast = false)
	{
		if (fast)
		{
			AddCall(progress, str_WheelsUpgrades);
		}
		else
		{
			AddCall(progress, str_WheelsUpgrades, _ChangeWheels);
		}
	}

	private void AddCall(int _progress, string animation, int boolname = -1)
	{
		if (!isWorking)
		{
			StartCoroutine(ChangeDetail(_progress, animation, boolname));
			return;
		}
		Call call = new Call();
		call.progress = _progress;
		call.animname = animation;
		call.boolname = boolname;
		Calls.Add(call);
	}

	private IEnumerator ChangeDetail(int _progress, string animation, int boolname = -1)
	{
		isWorking = true;
		if (boolname != -1)
		{
			CarAnimator.SetBool(boolname, !CarAnimator.GetBool(boolname));
			yield return Utilities.WaitForRealSeconds(0.2f);
		}
		else
		{
			yield return null;
		}
		SkinChanging.GetComponent<Animation>()[animation].speed = 1f;
		SkinChanging.GetComponent<Animation>()[animation].time = _progress;
		SkinChanging.Play(animation);
		SkinChanging.GetComponent<Animation>()[animation].speed = 0f;
		if (boolname != -1)
		{
			CarAnimator.SetBool(boolname, !CarAnimator.GetBool(boolname));
		}
		if (Calls.Count > 0)
		{
			StartCoroutine(ChangeDetail(Calls[0].progress, Calls[0].animname, Calls[0].boolname));
			Calls.Remove(Calls[0]);
		}
		else
		{
			isWorking = false;
		}
	}
}
