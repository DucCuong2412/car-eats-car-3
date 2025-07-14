using System.Collections;
using UnityEngine;

public class FuelCarBomb : MonoBehaviour
{
	public float damage = 5f;

	public GameObject Go;

	public Rigidbody2D gorb2d;

	public ParticleSystem ps1;

	public float pser1;

	public ParticleSystem ps2;

	public float pser2;

	public ParticleSystem ps3;

	public float pser3;

	public float lifeTyme = 3f;

	private static string CarMain = "CarMain";

	private static string CarMainChild = "CarMainChild";

	private static string Wheels = "Wheels";

	private void OnEnable()
	{
		if (RaceLogic.instance.AllInitedForPool)
		{
			StartCoroutine(startobj());
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == CarMain || collision.tag == CarMainChild || collision.tag == Wheels)
		{
			RaceLogic.instance.EatMainCar(damage);
		}
	}

	private IEnumerator startobj()
	{
		ps1.emissionRate = pser1;
		ps2.emissionRate = pser2;
		ps3.emissionRate = pser3;
		Go.SetActive(value: true);
		float t2 = lifeTyme - 2f;
		while (t2 > 0f)
		{
			t2 -= Time.deltaTime;
			yield return null;
		}
		StartCoroutine(patr(ps1));
		StartCoroutine(patr(ps2));
		StartCoroutine(patr(ps3));
		t2 = 2f;
		while (t2 > 0f)
		{
			t2 -= Time.deltaTime;
			yield return null;
		}
		base.gameObject.transform.parent.gameObject.SetActive(value: false);
	}

	private IEnumerator patr(ParticleSystem ps)
	{
		while (ps.emissionRate > 0f)
		{
			ps.emissionRate -= 0.5f;
			yield return 0;
		}
	}
}
