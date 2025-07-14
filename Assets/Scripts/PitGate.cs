using System.Collections;
using UnityEngine;

public class PitGate : MonoBehaviour
{
	public tk2dSlicedSprite HP;

	public PitController controller;

	public Collider2D box;

	private float healthPoint = 1f;

	private float StartDelay = 0.5f;

	private bool canDmg = true;

	private void Start()
	{
		healthPoint = controller.HPGate;
		StartDelay = controller.DmgDelay;
	}

	public void SetDmg(float dmg = 25f)
	{
		if (Progress.shop.activeCar == 3 || Progress.shop.activeCar == 4)
		{
			dmg = 100f;
		}
		if (canDmg)
		{
			StartCoroutine(delayDamage(dmg));
		}
	}

	private IEnumerator delayDamage(float dmg)
	{
		healthPoint -= dmg;
		for (int i = 0; i < 2; i++)
		{
			Pool.Scrap(Pool.Scraps.pitDoor, base.transform.position, UnityEngine.Random.Range(0, 360), 5f);
		}
		Pool.Scrap(Pool.Scraps.pitDoor1, base.transform.position, UnityEngine.Random.Range(0, 360), 5f);
		HP.scale = new Vector3(100f / controller.HPGate * healthPoint / 100f, 1f, 1f);
		canDmg = false;
		float t = StartDelay;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return null;
		}
		if (healthPoint <= 0f)
		{
			controller.GateDeath();
			base.gameObject.SetActive(value: false);
			box.gameObject.layer = LayerMask.NameToLayer("ObjectsBox");
			box.isTrigger = true;
		}
		else
		{
			canDmg = true;
		}
	}
}
