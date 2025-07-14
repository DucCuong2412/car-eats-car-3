using System;
using System.Collections;
using UnityEngine;

public class Car2DHealthModule : Car2DHealthModuleBase
{
	public Action boosterCallback;

	private float _healthBoost;

	public bool AnDeath;

	public float HealthBoost
	{
		get
		{
			return _healthBoost;
		}
		set
		{
			_healthBoost = value;
			if (boosterCallback != null)
			{
				boosterCallback();
			}
		}
	}

	public override void ChangeHealth(float dmg)
	{
		if (AnDeath)
		{
			dmg = 0f;
		}
		if (HealthBoost > 0f)
		{
			HealthBoost += dmg;
		}
		else
		{
			base.ChangeHealth(dmg);
		}
	}

	public override void IncreaseHealth(float addhealth)
	{
		if (AnDeath)
		{
			addhealth = 0f;
		}
		if (HealthBoost <= 0f)
		{
			base.IncreaseHealth(addhealth);
		}
	}

	public IEnumerator LerpFloat(float value)
	{
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime * 3f;
			if (!AnDeath)
			{
				HealthBoost = Mathf.Lerp(0f, value, t);
			}
			yield return null;
		}
	}
}
