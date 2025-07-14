using System.Collections.Generic;
using UnityEngine;

public class TEST2 : MonoBehaviour
{
	public ObjectActor OA;

	public List<Rigidbody2D> barells = new List<Rigidbody2D>();

	private bool chek = true;

	public bool isBoom = true;

	private const string nameSpiderWeb = "obj_spiderWeb_boom";

	private void Update()
	{
		if (OA.Health <= 0 && chek)
		{
			chek = !chek;
			if (base.transform.name.Contains("obj_spiderWeb_boom"))
			{
				Audio.Play("spider_web", 1f);
			}
			if (isBoom)
			{
				Pool.Animate(Pool.Explosion.exp25, base.transform.position);
				Pool.Animate(Pool.Explosion.exp25, base.transform.position);
				Pool.Animate(Pool.Explosion.exp25, base.transform.position);
			}
			foreach (Rigidbody2D barell in barells)
			{
				if (barell != null)
				{
					barell.gameObject.SetActive(value: true);
					barell.isKinematic = false;
					barell.AddForce(new Vector2(UnityEngine.Random.Range(0, 60), 25f), ForceMode2D.Impulse);
				}
			}
		}
	}
}
