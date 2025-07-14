using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShowerScript : MonoBehaviour
{
	public float _Time = 5f;

	public float Frequency = 3f;

	public Vector3 DeltaPos = new Vector3(0f, 13f);

	public float Radius = 20f;

	private Transform Car;

	private GameObject Meteor;

	[HideInInspector]
	public float time;

	public void Activate(Transform _car, List<Car2DAIController> _targets)
	{
		Car = _car;
		Audio.Play("gfx_earthquake_01_sn", Audio.soundVolume, loop: true, async: true);
		StartCoroutine(MeteorShower());
	}

	private IEnumerator MeteorShower()
	{
		time = _Time;
		float t = 0f;
		while (time > 0f)
		{
			if (t > 1f / Frequency)
			{
				SpawnMeteor();
				t = 0f;
			}
			time -= Time.deltaTime;
			t += Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}
		base.gameObject.SetActive(value: false);
	}

	private void SpawnMeteor()
	{
		Pool instance = Pool.instance;
		string name = "Meteor_" + UnityEngine.Random.Range(0, 3);
		Vector3 position = Car.position;
		float x = position.x + DeltaPos.x + UnityEngine.Random.Range(0f - Radius, Radius);
		Vector3 position2 = Car.position;
		instance.spawnAtPoint(name, new Vector2(x, position2.y + DeltaPos.y));
	}

	private void OnDisable()
	{
		Audio.Stop("gfx_earthquake_01_sn");
	}
}
