using System.Collections;
using UnityEngine;

public class GlacierScript : MonoBehaviour
{
	private float time;

	private float damage = 400f;

	private GameObject target;

	private Car2DAIController ai;

	private float startHealth;

	private float startDamage;

	private static string gfx_freeze_02 = "gfx_freeze_02_sn";

	private static string CarMain = "CarMain";

	private static string CarMainChild = "CarMainChild";

	public void Init(float _time, float damage, GameObject _target, Car2DAIController _ai)
	{
		time = _time;
		target = _target;
		ai = _ai;
		Audio.Play(gfx_freeze_02, Audio.soundVolume, loop: false);
		StartCoroutine(Freeze());
	}

	private IEnumerator Freeze()
	{
		startHealth = ai.HealthModule._Barrel.Value;
		startDamage = ai.EatDamage;
		ai.HealthModule._Barrel.Value = 1f;
		ai.EatDamage = 0f;
		ai.EngineModule.Break(onoff: true);
		float t = 0f;
		while (t <= time)
		{
			t += Time.deltaTime;
			base.transform.rotation = target.transform.rotation;
			if (!target.activeSelf)
			{
				for (int i = 0; i < 10; i++)
				{
					base.gameObject.SetActive(value: false);
				}
			}
			yield return null;
		}
		ai.HealthModule._Barrel.Value = startHealth;
		ai.EatDamage = startDamage;
		Kill(damage);
	}

	public void OnTriggerEnter2D(Collider2D coll)
	{
		if ((coll.gameObject.tag == CarMain || coll.gameObject.tag == CarMainChild) && ai != null)
		{
			Kill(ai.HealthModule._Barrel.Value + 1f);
		}
	}

	private void Kill(float _damage)
	{
		ai.EngineModule.Break(onoff: false);
		ai.HealthModule.ChangeHealth(0f - _damage);
		for (int i = 0; i < 10; i++)
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
