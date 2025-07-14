using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkOnColision : MonoBehaviour
{
	public float TicksPerSecond = 0.5f;

	public GameObject Saw;

	public Rotate rot;

	private bool collision;

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag("CarMain") || coll.gameObject.CompareTag("CarMainChild") || coll.gameObject.CompareTag("Wheels"))
		{
			collision = true;
			Saw.transform.position = coll.contacts[0].point;
			if (!RaceLogic.instance.car.HealthModule.AnDeath)
			{
				Audio.PlayAsync("saw_work", 0.2f);
			}
			if (rot.speed < 0f)
			{
				Saw.transform.localRotation = RaceLogic.instance.car.transform.rotation;
			}
			else if (rot.speed > 0f)
			{
				Transform transform = Saw.transform;
				Vector3 eulerAngles = RaceLogic.instance.car.transform.rotation.eulerAngles;
				transform.localRotation = Quaternion.Euler(0f, 0f, eulerAngles.z + 180f);
			}
		}
	}

	private void OnCollisionStay2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag("CarMain") || coll.gameObject.CompareTag("CarMainChild") || coll.gameObject.CompareTag("Wheels"))
		{
			collision = true;
			Saw.transform.position = coll.contacts[0].point;
			if (!RaceLogic.instance.car.HealthModule.AnDeath)
			{
				Audio.PlayAsync("saw_work", 0.2f);
			}
			if (rot.speed < 0f)
			{
				Saw.transform.localRotation = RaceLogic.instance.car.transform.rotation;
			}
			else if (rot.speed > 0f)
			{
				Transform transform = Saw.transform;
				Vector3 eulerAngles = RaceLogic.instance.car.transform.rotation.eulerAngles;
				transform.localRotation = Quaternion.Euler(0f, 0f, eulerAngles.z + 180f);
			}
		}
	}

	private void OnCollisionExit2D(Collision2D coll)
	{
		if ((coll.gameObject.CompareTag("CarMain") || coll.gameObject.CompareTag("CarMainChild") || coll.gameObject.CompareTag("Wheels")) && base.gameObject.activeSelf)
		{
			collision = false;
		}
	}

	private IEnumerator exitSparkl(Collision2D coll)
	{
		yield return new WaitForSeconds(0.2f);
	}

	private void Update()
	{
		if (!RaceLogic.instance.chekSparkl)
		{
			collision = false;
		}
		if (collision && Time.timeScale != 0f)
		{
			StartCoroutine(Spark());
		}
	}

	private IEnumerator Spark()
	{
		List<GameObject> Sparks = new List<GameObject>();
		for (int i = 0; i < 3; i++)
		{
			Sparks.Add(Pool.Animate(Pool.Bonus.spark, Saw.transform));
		}
		for (int j = 0; j < Sparks.Count; j++)
		{
			Vector3 eulerAngles = Saw.transform.rotation.eulerAngles;
			float z = eulerAngles.z;
			Vector3 eulerAngles2 = Saw.transform.rotation.eulerAngles;
			float z2 = UnityEngine.Random.Range(z, eulerAngles2.z - 90f);
			Sparks[j].transform.rotation = Quaternion.Euler(0f, 0f, z2);
		}
		yield return new WaitForSeconds(TicksPerSecond);
	}
}
