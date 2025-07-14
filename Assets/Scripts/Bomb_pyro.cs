using UnityEngine;

public class Bomb_pyro : MonoBehaviour
{
	public BombLogic.types type;

	public float radius;

	public float impulse;

	public float maxDamage;

	public float lifeTime;

	public bool onCollisionEnter = true;

	public int fragmentCount;

	public Pool.Explosion fragmentSprite;

	public int fragmentSpeed;

	public float fragmentGravityScale = 0.05f;

	[Range(0f, 1f)]
	public float fragmentCloudWidth = 1f;

	public int fragmentLifeTime;

	public int fragmentDamage;

	public float efectDamage;

	public float efectDuration;

	public bool mainCarCollision;

	public int exp2FragmentDamage = 10;

	public float fireDamage = 10f;

	public float fireDuration = 10f;

	private bool isMainCarDamaged;

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (onCollisionEnter)
		{
			OnCollision();
			base.gameObject.SetActive(value: false);
		}
	}

	private void OnCollision()
	{
		Pool.Animate(Pool.Explosion.exp28, base.transform.position);
		isMainCarDamaged = mainCarCollision;
		setFragments();
	}

	private void setFragments()
	{
		for (int i = 0; i < fragmentCount; i++)
		{
			GameObject gameObject = Pool.instance.spawnAtPoint(Pool.Name(fragmentSprite), base.transform.position);
			Vector2 vector = new Vector2(UnityEngine.Random.Range(0f - fragmentCloudWidth, fragmentCloudWidth), UnityEngine.Random.Range(0.35f, 1f));
		}
	}
}
