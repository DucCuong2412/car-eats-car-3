using System.Collections;
using UnityEngine;

public class Car2DGun : MonoBehaviour
{
	public enum BulletType
	{
		Basic,
		Slow,
		Debuff
	}

	public GameObject bulletPrefab;

	public Car2DAIController aiController;

	[Header("Gun")]
	public bool OnlyShootInFront;

	public float ShootDistanceMin;

	public float ShootDistanceMax;

	public float RearmTime;

	public int MaxAmmo;

	public float ShootDelay;

	public Transform ShootPoint;

	public Pool.Bombs BulletPrefab;

	[Header("Optional")]
	public Animation ShootAnim;

	public string ShootAnimClipName;

	public string SoundGunName;

	[Header("Bullet")]
	public bool IsHoming = true;

	public bool HasParticles;

	public float BulletDamage;

	public float BulletMoveSpeed;

	public float BulletAngularSpeed;

	public float BulletLifeTime;

	public LayerMask BulletLayerMask;

	public BulletType BulletDamageType;

	public Pool.Explosion ParticleFly;

	private int ammo;

	private float shootDelayTimer;

	private float rearmTimer;

	public Transform target;

	public bool CanShoot => ammo > 0 && shootDelayTimer > ShootDelay;

	public Transform Target
	{
		get
		{
			return target;
		}
		set
		{
			target = value;
		}
	}

	public bool IsAhead => aiController.constructor.isAhead;

	private void OnEnable()
	{
		StartCoroutine(WaitForTarget());
	}

	private IEnumerator WaitForTarget()
	{
		while (!RaceLogic.instance || !RaceLogic.instance.car)
		{
			yield return 0;
		}
		target = RaceLogic.instance.car.transform;
	}

	private void Update()
	{
		target = RaceLogic.instance.car.transform;
		if (CanShoot && InRange() && (!OnlyShootInFront || (OnlyShootInFront && TargetIsInFront())))
		{
			Shoot();
			return;
		}
		if (ammo == 0)
		{
			Rearm();
		}
		shootDelayTimer += Time.deltaTime;
	}

	private bool InRange()
	{
		float magnitude = (target.position - base.transform.position).magnitude;
		return magnitude >= ShootDistanceMin && magnitude <= ShootDistanceMax;
	}

	private bool TargetIsInFront()
	{
		return true;
	}

	private void Rearm()
	{
		rearmTimer += Time.deltaTime;
		if (rearmTimer >= RearmTime)
		{
			ammo = MaxAmmo;
		}
	}

	private void OnDrawGizmos()
	{
	}

	private void Shoot()
	{
		if (!string.IsNullOrEmpty(SoundGunName))
		{
			Audio.PlayAsync(SoundGunName);
		}
		if (ShootAnim != null)
		{
			if (string.IsNullOrEmpty(ShootAnimClipName))
			{
				ShootAnim.Play();
			}
			else
			{
				ShootAnim.Play(ShootAnimClipName);
			}
		}
		GameObject gameObject = (!(bulletPrefab != null)) ? Pool.Animate(Pool.Name(BulletPrefab), ShootPoint.position) : UnityEngine.Object.Instantiate(bulletPrefab);
		gameObject.SetActive(value: true);
		gameObject.transform.position = ShootPoint.position;
		Bullet component = gameObject.GetComponent<Bullet>();
		component.Init(IsAhead, IsHoming, HasParticles, base.transform.localRotation, target, BulletDamage, BulletMoveSpeed, BulletAngularSpeed, BulletLifeTime, BulletLayerMask, BulletDamageType, ParticleFly, -1f, 0f, ShootPoint);
		shootDelayTimer = 0f;
		ammo--;
		if (ammo == 0)
		{
			rearmTimer = 0f;
		}
	}
}
