using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Car2DEngineModuleBase : Car2DModuleBase, IEngineModule
{
	public float Speed;

	public float MaxSpeed;

	public float Torque;

	public bool IsConvoi;

	public List<Rigidbody2D> Wheels;

	private float convoiMultipler = 1f;

	private int Difficult = 1;

	private bool IsConfigured;

	private bool isAI;

	public int Dir = 1;

	public override void onModuleEnable()
	{
		if (isAI)
		{
			StartCoroutine(AIEngine());
		}
	}

	public override void onModuleDisable()
	{
	}

	public override void onModuleInited()
	{
		if (isAI)
		{
			StartCoroutine(AIEngine());
		}
	}

	public void Init(List<Rigidbody2D> wheels)
	{
		Wheels = wheels;
		base.moduleInited = true;
	}

	public void UpdateModuleValue(float coefficient)
	{
		MaxSpeed = coefficient;
	}

	public void Break(bool onoff)
	{
		if (base.moduleInited)
		{
			for (int i = 0; i < Wheels.Count; i++)
			{
				Wheels[i].fixedAngle = onoff;
			}
		}
	}

	public void Move(float direction, float multipler = 1f)
	{
		if (!base.moduleInited || !base.moduleEnabled)
		{
			return;
		}
		Speed = Math.Abs(Wheels[0].angularVelocity);
		if (IsConvoi && Speed < 300f)
		{
			convoiMultipler = 1.5f;
		}
		else
		{
			convoiMultipler = 1f;
		}
		for (int i = 0; i < Wheels.Count; i++)
		{
			if ((Wheels[i].angularVelocity < MaxSpeed && direction < 0f) || (Wheels[i].angularVelocity > 0f - MaxSpeed && direction > 0f))
			{
				Wheels[i].AddTorque(Torque * (0f - direction) * 100f * multipler * convoiMultipler);
			}
		}
	}

	public void InitAI(List<Rigidbody2D> wheels, int diff = 1, bool _isConvoi = false)
	{
		isAI = true;
		Wheels = wheels;
		IsConvoi = _isConvoi;
		base.moduleInited = true;
	}

	public void ChangeDifficult(int dif = 1)
	{
		switch (dif)
		{
		case 1:
			MaxSpeed += MaxSpeed / 100f * 10f;
			Torque += Torque / 100f * 10f;
			break;
		case 2:
			MaxSpeed += MaxSpeed / 100f * 20f;
			Torque += Torque / 100f * 20f;
			break;
		}
		IsConfigured = true;
	}

	public IEnumerator AIEngine()
	{
		if (!IsConfigured)
		{
			ChangeDifficult(Difficult);
		}
		while (base.moduleEnabled)
		{
			Move(Dir);
			yield return null;
		}
	}
}
