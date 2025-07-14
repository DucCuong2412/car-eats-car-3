using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Car2DPowerModuleBase : Car2DModuleBase, IPowerModule
{
	public ForceMode2D _forceMode;

	public float Power;

	public float Angle;

	public Barrel _barrel;

	public int Dir = 1;

	[HideInInspector]
	public bool isUsed;

	[HideInInspector]
	public bool DoubleTurbo;

	private Rigidbody2D TempRB;

	private float Speed;

	private List<Rigidbody2D> Body = new List<Rigidbody2D>();

	private int Difficult = 1;

	private float MinSpeedToUseAI = 6f;

	private bool IsConfigured;

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

	private Vector3 PowerDirection
	{
		get
		{
			if (TempRB == null)
			{
				int count = Body.Count;
				for (int i = 0; i < count; i++)
				{
					if (Body[i].tag == "CarMain")
					{
						TempRB = Body[i];
					}
				}
			}
			Vector3 result = Vector3.zero;
			if (Body[0] == null)
			{
				result = Vector3.zero;
			}
			else if (TempRB != null)
			{
				Vector3 eulerAngles = TempRB.transform.rotation.eulerAngles;
				if (eulerAngles.z > 70f)
				{
					float angle = _angle;
					Vector3 eulerAngles2 = TempRB.transform.rotation.eulerAngles;
					float x = Mathf.Sin((float)Math.PI / 180f * (angle - eulerAngles2.z));
					float angle2 = _angle;
					Vector3 eulerAngles3 = TempRB.transform.rotation.eulerAngles;
					Vector3 vector = new Vector3(x, Mathf.Cos((float)Math.PI / 180f * (angle2 - eulerAngles3.z)), 0f);
					result = vector.normalized * Dir;
				}
				else
				{
					result = new Vector3(_angle, _angle, 0f).normalized * Dir;
				}
			}
			return result;
		}
	}

	private float _angle => Angle * 30f;

	public override void onModuleEnable()
	{
		if (isAI)
		{
			StartCoroutine(AIPower());
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

	public virtual void Init(List<Rigidbody2D> bodyes, Action callback = null)
	{
		Body = bodyes;
		_Barrel.callback = callback;
		base.moduleInited = true;
		TempRB = null;
	}

	public void UpdateModuleValue(float coefficient, float power)
	{
		_Barrel.MaxValue = coefficient * 100f;
		_Barrel.Value = coefficient * 100f;
		Power = power;
	}

	public virtual void UsePower(bool _isUse)
	{
		if (!base.moduleEnabled || !base.moduleInited)
		{
			return;
		}
		isUsed = false;
		if (_isUse)
		{
			if (Body == null)
			{
				UnityEngine.Debug.LogWarning(GetType() + " no rigidBody!");
			}
			else if (Math.Abs(_Barrel.Value) < 0.05f)
			{
				_Barrel.Value = 0f;
			}
			else
			{
				isUsed = _isUse;
			}
		}
	}

	public virtual void Update()
	{
		if (isUsed)
		{
			UsePowerImmediately();
		}
		_Barrel.Use(isUsed);
	}

	public virtual void UsePowerImmediately()
	{
		if (Time.timeScale != 0f)
		{
			for (int i = 0; i < Body.Count; i++)
			{
				Body[i].AddForce(PowerDirection * Power * Body[i].mass * Time.deltaTime, _forceMode);
			}
		}
	}

	public virtual void Increase(float cost)
	{
		_Barrel.Increase(cost);
	}

	public void InitAI(List<Rigidbody2D> bodyes, Action callback = null, int diff = 1)
	{
		_Barrel.callback = callback;
		isAI = true;
		Body = bodyes;
		Difficult = diff;
		base.moduleInited = true;
		StartCoroutine(AIPower());
	}

	public void ChangeDifficult(int dif = 1)
	{
		switch (dif)
		{
		case 1:
			Power += Power / 100f * 10f;
			_Barrel.MaxValue += _Barrel.MaxValue / 100f * 10f;
			MinSpeedToUseAI += MinSpeedToUseAI / 100f * 10f;
			if (_Barrel.UsageValue > _Barrel.MaxValue)
			{
				_Barrel.UsageValue = _Barrel.MaxValue;
			}
			if (_Barrel.Restore)
			{
				_Barrel.RestoreTime -= _Barrel.RestoreTime / 100f * 20f;
			}
			break;
		case 2:
			Power += Power / 100f * 20f;
			_Barrel.MaxValue += _Barrel.MaxValue / 100f * 20f;
			MinSpeedToUseAI += MinSpeedToUseAI / 100f * 20f;
			if (_Barrel.Restore)
			{
				_Barrel.RestoreTime -= _Barrel.RestoreTime / 100f * 20f;
			}
			break;
		}
		IsConfigured = true;
	}

	public IEnumerator AIPower()
	{
		if (!IsConfigured)
		{
			ChangeDifficult(Difficult);
		}
		bool used = false;
		while (base.moduleEnabled)
		{
			if (base.moduleEnabled && _Barrel.Value != 0f)
			{
				if (_Barrel.Value >= _Barrel.MaxValue && !used)
				{
					used = true;
					UsePower(_isUse: true);
				}
				else if (_Barrel.Value > _Barrel.UsageValue && used)
				{
					UsePower(_isUse: true);
				}
				else if (_Barrel.Value < _Barrel.UsageValue)
				{
					UsePower(_isUse: false);
					used = false;
				}
			}
			yield return new WaitForFixedUpdate();
		}
	}
}
