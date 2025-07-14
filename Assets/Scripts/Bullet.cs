using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private float moveSpeed;

	private float damage;

	private float angularSpeed;

	private float lifeTime;

	private Transform target;

	private LayerMask layerMask;

	private Car2DGun.BulletType bulletType;

	private bool isInited;

	private bool isHoming;

	private bool hasParticles;

	private float currentLifeTime;

	private Vector3 previosPosition;

	private float startingAngularSpeed;

	private float t;

	public GameObject dum;

	private GameObject particle;

	private bool firstBoom = true;

	[Header("DONT TOUCH THIS!!!!!!!")]
	public float force_for_mee = 400f;

	[Header("DONT TOUCH THIS TO!!!!!!!")]
	public Vector2 vector_for_mee = new Vector2(1.5f, 0f);

	[Header("DONT TOUCH THIS TO!!!!!!!")]
	public float delay_for_mee = 0.5f;

	private Transform tParticle;

	private Vector3 moveVector;

	private float firstZone = 2f;

	private float secondZone = 4f;

	public SpriteRenderer sprite;

	public SpriteRenderer fire;

	[Header("LedoLuch")]
	public float TimeZamozki;

	public void Init(bool isAhead, bool isHoming, bool hasParticles, Quaternion rotation, Transform target, float damage, float moveSpeed, float angularSpeed, float lifeTime, LayerMask layerMask, Car2DGun.BulletType bulletType, Pool.Explosion particleFly, float carVelocity, float StartPower, Transform positionStart)
	{
		base.transform.rotation = rotation;
		if (!isHoming)
		{
			Vector3 vector = target.transform.position - base.transform.position;
			float num = Mathf.Atan2(vector.y, vector.x);
			base.transform.eulerAngles = new Vector3(0f, 0f, num * 57.29578f);
		}
		this.isHoming = isHoming;
		this.hasParticles = hasParticles;
		this.target = target;
		this.damage = damage;
		this.angularSpeed = angularSpeed;
		this.lifeTime = lifeTime;
		this.moveSpeed = moveSpeed + ((carVelocity != -1f) ? carVelocity : 0f);
		this.bulletType = bulletType;
		this.layerMask = layerMask;
		currentLifeTime = 0f;
		startingAngularSpeed = angularSpeed;
		previosPosition = base.transform.position;
		StartCoroutine(initer(particleFly, StartPower, 0f, positionStart));
	}

	private IEnumerator initer(Pool.Explosion particleFly, float StartPower, float ttt, Transform pos)
	{
		isInited = false;
		if (TimeZamozki == 0f)
		{
			base.gameObject.transform.eulerAngles = new Vector3(0f, 0f, 90f);
		}
		if (!isHoming)
		{
			moveVector = target.position - base.transform.position;
		}
		if (hasParticles)
		{
			particle = Pool.Animate(Pool.Name(particleFly), dum.transform.position, "CarMain");
			tParticle = particle.transform;
		}
		t = 0.7f;
		if (TimeZamozki == 0f)
		{
			Audio.PlayAsync("gfx_rocket_01_sn", 0.5f);
			Rigidbody2D rb = base.transform.GetComponent<Rigidbody2D>();
			rb.AddForce(force_for_mee * (Vector2.up + vector_for_mee) * rb.mass);
			yield return new WaitForSeconds(delay_for_mee);
		}
		isInited = true;
	}

	private void FixedUpdate()
	{
		if (isInited)
		{
			if (tParticle != null)
			{
				tParticle.position = dum.transform.position;
			}
			if (!target.gameObject.activeSelf)
			{
				base.gameObject.SetActive(value: false);
			}
			currentLifeTime += Time.fixedDeltaTime;
			if (currentLifeTime >= lifeTime)
			{
				DestroyBullet(withDamage: false);
			}
			if (isHoming)
			{
				MoveHoming();
			}
			else
			{
				MoveRegular();
			}
			if (target == null || !target.gameObject.activeSelf)
			{
				base.gameObject.SetActive(value: false);
			}
		}
	}

	private void MoveRegular()
	{
		base.transform.position += moveVector.normalized * moveSpeed * Time.deltaTime;
		Vector3 eulerAngles = base.transform.rotation.eulerAngles;
		float z = eulerAngles.z;
		RaycastHit2D[] array = new RaycastHit2D[1];
		int num = Physics2D.LinecastNonAlloc(previosPosition, base.transform.position, array, layerMask.value);
		if (num > 0 && (bool)array[0].collider && array[0].collider.gameObject.activeSelf)
		{
			if (array[0].collider.gameObject.CompareTag("CarMain") || array[0].collider.gameObject.CompareTag("CarMainChild") || array[0].collider.gameObject.CompareTag("Wheels"))
			{
				RaceLogic.instance.OnBulletReachCar(bulletType);
			}
			DestroyBullet(withDamage: true);
		}
		previosPosition = base.transform.position;
	}

	private void MoveHoming()
	{
		Vector3 vector = target.position - base.transform.position;
		float magnitude = vector.magnitude;
		if (magnitude < firstZone)
		{
			angularSpeed = startingAngularSpeed * 3f;
		}
		else if (magnitude < secondZone)
		{
			angularSpeed = startingAngularSpeed * 2f;
		}
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.AngleAxis(Mathf.Atan2(vector.y, vector.x) * 57.29578f, Vector3.forward), Time.deltaTime * angularSpeed);
		Vector3 eulerAngles = base.transform.rotation.eulerAngles;
		float z = eulerAngles.z;
		base.transform.position += base.transform.right * moveSpeed * Time.deltaTime;
		RaycastHit2D[] array = new RaycastHit2D[1];
		int num = Physics2D.LinecastNonAlloc(previosPosition, base.transform.position, array, layerMask.value);
		if (num > 0 && (bool)array[0].collider && array[0].collider.gameObject.activeSelf)
		{
			if (array[0].collider.gameObject.CompareTag("CarMain") || array[0].collider.gameObject.CompareTag("CarMainChild") || array[0].collider.gameObject.CompareTag("Wheels"))
			{
				RaceLogic.instance.OnBulletReachCar(bulletType);
			}
			DestroyBullet(withDamage: true);
		}
		previosPosition = base.transform.position;
	}

	private void OnEnable()
	{
		if (RaceLogic.instance.AllInitedForPool)
		{
			if (sprite != null)
			{
				sprite.enabled = true;
			}
			if (fire != null)
			{
				fire.enabled = true;
			}
			firstBoom = true;
		}
	}

	private void DestroyBullet(bool withDamage)
	{
		if (RaceManager.instance.isStarted && firstBoom)
		{
			firstBoom = false;
			if (base.gameObject.activeInHierarchy)
			{
				StartCoroutine(DB(withDamage));
			}
		}
	}

	private IEnumerator DB(bool withDamage)
	{
		if (withDamage)
		{
			Audio.PlayAsync("exp_tnt_05_sn");
			Car2DAIController component = target.GetComponent<Car2DAIController>();
			if (component != null)
			{
				if (bulletType == Car2DGun.BulletType.Basic)
				{
					component.HealthModule.ChangeHealth(0f - damage);
				}
				else if (bulletType == Car2DGun.BulletType.Debuff)
				{
					component.ZS.zamorozka(TimeZamozki, 0f, component.gameObject, component);
					base.gameObject.SetActive(value: false);
				}
			}
			else
			{
				RaceLogic.instance.HitMainCar(0f - damage);
				if (base.gameObject.name == "projectile_03")
				{
					Pool.Animate("CFXM_GroundExplosion_Text", base.gameObject.transform.position);
				}
			}
		}
		if (TimeZamozki == 0f)
		{
			Pool.Animate(Pool.Explosion.exp26, target.transform.position, "CarMain");
		}
		if (sprite != null)
		{
			sprite.enabled = false;
		}
		if (fire != null)
		{
			fire.enabled = false;
		}
		yield return 0;
		StartCoroutine(end());
	}

	private IEnumerator end()
	{
		yield return new WaitForSeconds(0.1f);
		tParticle = null;
		if (particle != null)
		{
			particle.SetActive(value: false);
		}
		base.gameObject.SetActive(value: false);
	}
}
