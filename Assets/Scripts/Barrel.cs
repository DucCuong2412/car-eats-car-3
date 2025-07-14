using System;
using UnityEngine;

public class Barrel : MonoBehaviour
{
	public bool Enable;

	public float MaxValue;

	[SerializeField]
	private float _value;

	public float UsageValue = 1f;

	public bool Restore;

	public float RestoreTime;

	[HideInInspector]
	public Action callback;

	private bool isUse;

	public float Value
	{
		get
		{
			return _value;
		}
		set
		{
			_value = value;
			if (callback != null)
			{
				callback();
			}
		}
	}

	public void Use(bool _use)
	{
		isUse = _use;
	}

	public void Use(float _using)
	{
		Value = Mathf.Clamp(Value - _using, 0f, MaxValue);
	}

	private void Use()
	{
		Value = Mathf.Clamp(Value - Time.deltaTime * UsageValue, 0f, MaxValue);
	}

	public void Increase(float amount)
	{
		if (Value + MaxValue / 100f * amount <= MaxValue)
		{
			Value += MaxValue / 100f * amount;
		}
		else
		{
			Value = MaxValue;
		}
	}

	public void Increase(int count)
	{
		if (!Enable)
		{
			Enable = true;
		}
		Value = Mathf.Clamp(Value + (float)count, 0f, MaxValue);
	}

	public void Update()
	{
		if (Enable)
		{
			if (isUse)
			{
				Use();
			}
			else if (Restore)
			{
				Value = Mathf.Clamp(Value + MaxValue / RestoreTime * Time.deltaTime, 0f, MaxValue);
			}
		}
	}
}
