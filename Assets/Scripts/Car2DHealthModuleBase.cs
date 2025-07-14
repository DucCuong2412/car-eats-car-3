using System;
using UnityEngine;

[Serializable]
public class Car2DHealthModuleBase : Car2DModuleBase, IHealthModule
{
	public Barrel _barrel;

	public Action<float> DetailDamage;

	public float BaseValue = 100f;

	private int i;

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

	public override void onModuleEnable()
	{
		_Barrel.Enable = true;
		ChangeHealth(0f);
	}

	public override void onModuleDisable()
	{
		_Barrel.Enable = false;
	}

	public override void onModuleInited()
	{
	}

	public virtual void Init(Action<float> _action = null, Action callback = null, bool myCar = false)
	{
		DetailDamage = _action;
		_Barrel.callback = callback;
		_Barrel.MaxValue = BaseValue;
		_Barrel.Value = BaseValue;
		base.moduleInited = true;
	}

	public void UpdateModuleValue(float hpsetings)
	{
		_Barrel.MaxValue = hpsetings;
		_Barrel.Value = hpsetings;
		BaseValue = hpsetings;
		OnHealthChange();
	}

	public virtual void OnHealthChange()
	{
		if (base.moduleEnabled && _Barrel.Value < 0.05f)
		{
			_Barrel.Value = 0f;
		}
	}

	public virtual void IncreaseHealth(float addhealth)
	{
		ChangeHealth(_Barrel.MaxValue / 100f * addhealth);
	}

	public virtual void ChangeHealth(float dmg)
	{
		if (base.moduleInited && base.moduleEnabled)
		{
			_Barrel.Value = Mathf.Clamp(_Barrel.Value + dmg, 0f, _Barrel.MaxValue);
			CheckElementHealth();
			OnHealthChange();
		}
	}

	private void Update()
	{
		if (i == 20)
		{
			CheckElementHealth();
			i = 0;
		}
		else
		{
			i++;
		}
	}

	public void CheckElementHealth()
	{
		if (DetailDamage != null)
		{
			float obj = _Barrel.Value / _Barrel.MaxValue * 100f;
			DetailDamage(obj);
		}
	}
}
