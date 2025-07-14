using System;
using System.Collections;
using UnityEngine;

public class Car2DTurboModule : Car2DPowerModuleBase
{
	[HideInInspector]
	public Action boosterCallback;

	public float _turboBoost;

	public bool boostUsed;

	public float TurboBoost
	{
		get
		{
			return _turboBoost;
		}
		set
		{
			_turboBoost = value;
			if (boosterCallback != null)
			{
				boosterCallback();
			}
		}
	}

	public override void UsePower(bool _isUse)
	{
		if (base.moduleEnabled)
		{
			if (TurboBoost > 0f)
			{
				boostUsed = _isUse;
				return;
			}
			boostUsed = false;
			base.UsePower(_isUse);
		}
	}

	public override void Increase(float cost)
	{
		if (TurboBoost <= 0f)
		{
			base.Increase(cost);
		}
	}

	public override void Update()
	{
		if (boostUsed)
		{
			UsePowerImmediately();
			TurboBoost -= base._Barrel.UsageValue * Time.deltaTime;
		}
		else
		{
			base.Update();
		}
	}

	public IEnumerator LerpFloat(float value)
	{
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime * 3f;
			TurboBoost = Mathf.Lerp(0f, value, t);
			yield return null;
		}
	}
}
