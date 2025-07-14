using System;

public interface IHealthModule
{
	void Init(Action<float> _action = null, Action callback = null, bool myCar = false);

	void UpdateModuleValue(float hpsetings);

	void IncreaseHealth(float addhealth);

	void ChangeHealth(float dmg);
}
