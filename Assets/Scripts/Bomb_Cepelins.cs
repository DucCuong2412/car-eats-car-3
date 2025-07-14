using UnityEngine;

public class Bomb_Cepelins : MonoBehaviour
{
	private static string CarEnemy = "CarEnemy";

	public float damage;

	public float dist = 5f;

	private void OnEnable()
	{
		if (RaceLogic.instance.AllInitedForPool)
		{
			Rigidbody2D component = base.gameObject.GetComponent<Rigidbody2D>();
			component.velocity = Vector3.right * 30f;
		}
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (!RaceManager.instance.isStarted)
		{
			return;
		}
		Audio.Play("exp_tnt_05_sn");
		for (int i = 0; i < RaceLogic.instance.race.activeAIs.Count; i++)
		{
			Vector3 position = RaceLogic.instance.race.activeAIs[i].transform.position;
			float x = position.x;
			Vector3 position2 = base.transform.position;
			if (x > position2.x - dist)
			{
				Vector3 position3 = RaceLogic.instance.race.activeAIs[i].transform.position;
				float x2 = position3.x;
				Vector3 position4 = base.transform.position;
				if (x2 < position4.x + dist)
				{
					RaceLogic.instance.race.activeAIs[i].SetDamage(damage);
				}
			}
		}
		Pool.Animate(Pool.Explosion.exp25, base.transform.position);
		base.gameObject.SetActive(value: false);
	}
}
