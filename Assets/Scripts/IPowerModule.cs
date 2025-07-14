using System;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerModule
{
	void Init(List<Rigidbody2D> body, Action callback = null);

	void UpdateModuleValue(float coefficient, float power);

	void ChangeDifficult(int dif);

	void UsePower(bool _isUse);
}
