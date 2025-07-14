using System.Collections.Generic;
using UnityEngine;

public interface IEngineModule
{
	void Init(List<Rigidbody2D> wheels);

	void UpdateModuleValue(float coefficient);

	void ChangeDifficult(int dif);

	void Break(bool onoff);

	void Move(float direction, float multipler = 1f);
}
