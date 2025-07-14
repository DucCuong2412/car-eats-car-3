using System.Collections.Generic;
using UnityEngine;

public interface ISuspensionModule
{
	void Init(Rigidbody2D body, List<Rigidbody2D> wheels);

	void UpdateSettings();

	void SetFrictionCoeficient(float coefficient, float b);

	void ChangeDifficult(int dif);

	void Rotate(float direction);
}
