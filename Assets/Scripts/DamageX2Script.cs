using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageX2Script : MonoBehaviour
{
	public float time;

	private Transform Car;

	public void Activate(Transform _car, List<Car2DAIController> _targets, bool x2damage)
	{
		Car = _car;
		if (time > Progress.settings.timerForx2damage)
		{
			Progress.settings.timerForx2damage = time;
		}
		else
		{
			time = Progress.settings.timerForx2damage;
		}
		StartCoroutine(Active(x2damage));
	}

	private IEnumerator Active(bool x2damage)
	{
		while (time > 0f)
		{
			if (x2damage)
			{
				Progress.settings.x2damage = true;
				Progress.settings.ReduceDamage = false;
			}
			else
			{
				Progress.settings.x2damage = false;
				Progress.settings.ReduceDamage = true;
			}
			time -= Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}
		Progress.settings.x2damage = false;
		Progress.settings.ReduceDamage = false;
		base.gameObject.SetActive(value: false);
	}
}
