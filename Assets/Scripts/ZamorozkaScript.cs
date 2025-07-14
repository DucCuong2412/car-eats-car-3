using System.Collections;
using UnityEngine;

public class ZamorozkaScript : MonoBehaviour
{
	private float time;

	private GameObject targets;

	private Car2DAIController ai;

	private float startHealth;

	private float startDamage;

	private static string gfx_freeze_02 = "gfx_freeze_02_sn";

	public void zamorozka(float _time, float damage, GameObject _target, Car2DAIController _ai)
	{
		time = _time;
		targets = _target;
		ai = _ai;
		ai.freeze = true;
		RaceLogic.instance.CounterEmemys++;
		Audio.Play(gfx_freeze_02, 1f);
		StartCoroutine(Freeze());
	}

	private IEnumerator Freeze()
	{
		Pool.instance.spawnAtPoint(Pool.Name(Pool.Bombs.ice), ai.gameObject.transform).GetComponent<GlacierScript>();
		startHealth = ai.HealthModule._Barrel.Value;
		startDamage = ai.EatDamage;
		ai.HealthModule._Barrel.Value = 1f;
		ai.EatDamage = 0f;
		ai.EngineModule.Break(onoff: true);
		ai.freeze = true;
		float t = 0f;
		while (t <= time)
		{
			t += Time.deltaTime;
			base.transform.rotation = targets.transform.rotation;
			yield return null;
		}
		ai.HealthModule._Barrel.Value = startHealth;
		ai.EatDamage = startDamage;
		if (targets.activeSelf)
		{
			Kill(ai.HealthModule._Barrel.MaxValue + 1f);
		}
	}

	private void Kill(float _damage)
	{
		ai.EngineModule.Break(onoff: false);
		ai.HealthModule.ChangeHealth(0f - _damage);
		for (int i = 0; i < 10; i++)
		{
			Pool.Scrap(Pool.Scraps.ice, targets.transform.position, UnityEngine.Random.Range(0, 360), 5f);
		}
	}
}
