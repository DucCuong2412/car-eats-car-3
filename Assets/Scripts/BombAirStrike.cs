using UnityEngine;

public class BombAirStrike : MonoBehaviour
{
	public float Damage = 300f;

	public float Radius = 5f;

	public float Power = 150f;

	private const int maxCount = 50;

	private GameObject Exp;

	private RaycastHit2D[] hits = new RaycastHit2D[50];

	private int layerExplosion;

	public void Awake()
	{
		layerExplosion = LayerMask.GetMask("Car", "Wheel");
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		Audio.Play("gfx_earthquake_02_sn", Audio.soundVolume, loop: false);
		Explosion(base.transform.position, Radius, Power, Damage);
		Pool.Animate(Pool.Explosion.exp26, base.transform.position);
		base.gameObject.SetActive(value: false);
	}

	public void Explosion(Vector3 pos, float radius, float power, float damage, BombLogic.types types = BombLogic.types.DEFAULT)
	{
		int num = Physics2D.CircleCastNonAlloc(pos, radius, Vector2.down, hits, 0f, layerExplosion);
		for (int i = 0; i < num; i++)
		{
			Rigidbody2D component = hits[i].collider.GetComponent<Rigidbody2D>();
			float value = Vector2.Distance(hits[i].transform.position, pos);
			float num2 = Get1_0FromDistance(value, radius);
			Vector2 force = -hits[i].normal * power * num2;
			if (component != null)
			{
				component.AddForce(force);
				component.BroadcastMessage("OnHit", damage * num2, SendMessageOptions.DontRequireReceiver);
				component.SendMessageUpwards("ChangeHealth", 0f - damage * num2, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	private float Get1_0FromDistance(float value, float maxDistance)
	{
		float num = Mathf.Clamp(value, 0f, maxDistance);
		return 1f - num / maxDistance;
	}
}
