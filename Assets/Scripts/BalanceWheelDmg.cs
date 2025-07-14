using System.Collections;
using UnityEngine;

public class BalanceWheelDmg : MonoBehaviour
{
	public float dmg = 50f;

	public float TimeBetwin = 1f;

	private bool canDmg = true;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if ((other.tag == "CarMain" || other.tag == "CarMainChild") && canDmg)
		{
			StartCoroutine(Delay());
		}
	}

	private IEnumerator Delay()
	{
		canDmg = false;
		RaceLogic.instance.HitMainCar(dmg);
		float t = TimeBetwin;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return null;
		}
		canDmg = true;
	}
}
