using System;
using System.Collections;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
	public GameObject bulletPrefab;

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

	public Animation ReloadAnim;

	public bool SpawnAfterAnimation;

	public bool NeedReloadAnimation;

	public string ShootAnimClipName;

	public string ReloadAnimClipName;

	public string SoundGunShot;

	[Header("Bullet")]
	public bool IsHoming = true;

	public bool HasParticles;

	public float BulletDamage;

	public float BulletMoveSpeed;

	public float CarVelocitySpeedBonusCoof = 0.005f;

	public float BulletAngularSpeed;

	public float BulletLifeTime;

	public LayerMask BulletLayerMask;

	public Car2DGun.BulletType BulletDamageType;

	public Pool.Explosion ParticleFly;

	public float StartPower;

	private int ammo;

	private float shootDelayTimer;

	private float rearmTimer;

	private bool isShooting;

	private bool isReloading;

	[HideInInspector]
	public Action<Transform> LauncherGadgetCallback;

	public bool IsShop;

	public Transform ShopTarget;

	private int _missileLauncha_isOn = Animator.StringToHash("missileLauncha_isOn");

	private int _freezeRay_isOn = Animator.StringToHash("freezeRay_isOn");

	public Animator animatorsssss;

	private Bullet bul;

	public bool CanShoot
	{
		get
		{
			if (ammo <= 0 || shootDelayTimer < ShootDelay)
			{
				return false;
			}
			Transform target = Target;
			if (target == null)
			{
				base.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
				return false;
			}
			return InRange(target);
		}
	}

	public virtual Transform Target
	{
		get
		{
			if (IsShop)
			{
				return ShopTarget;
			}
			float num = float.MaxValue;
			Transform result = null;
			if (RaceLogic.instance.race == null)
			{
				return null;
			}
			if (RaceLogic.instance.race.activeAIs == null)
			{
				return null;
			}
			foreach (Car2DAIController activeAI in RaceLogic.instance.race.activeAIs)
			{
				if (OnlyShootInFront)
				{
					Vector3 position = activeAI.transform.position;
					float x = position.x;
					Vector3 position2 = base.transform.position;
					if (x < position2.x && !activeAI.IsCivic)
					{
						continue;
					}
				}
				float magnitude = (activeAI.transform.position - base.transform.position).magnitude;
				if (magnitude < num && magnitude <= ShootDistanceMax)
				{
					if (activeAI.IsCivic)
					{
						return null;
					}
					result = activeAI.transform;
					num = magnitude;
				}
			}
			return result;
		}
	}

	public bool IsAhead => true;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
		isShooting = false;
	}

	private void Update()
	{
		if (BulletDamageType == Car2DGun.BulletType.Basic)
		{
			base.gameObject.transform.eulerAngles = new Vector3(0f, 0f, 90f);
		}
		else
		{
			base.gameObject.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
		}
		if (!IsShop)
		{
			if (isShooting)
			{
				return;
			}
			if (CanShoot)
			{
				Shoot();
				return;
			}
			if (ammo <= 0)
			{
				Rearm();
			}
		}
		shootDelayTimer += Time.deltaTime;
	}

	private bool InRange(Transform target)
	{
		if (target == null)
		{
			return false;
		}
		float magnitude = (target.position - base.transform.position).magnitude;
		return IsShop || (magnitude >= ShootDistanceMin && magnitude <= ShootDistanceMax);
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

	protected virtual void Shoot()
	{
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
		StartCoroutine(WaitForShoot());
	}

	private IEnumerator WaitForShoot()
	{
		isShooting = true;
		Transform target = Target;
		if (BulletDamageType == Car2DGun.BulletType.Basic)
		{
			animatorsssss.SetBool(_missileLauncha_isOn, value: true);
			yield return new WaitForSeconds(0.5f);
		}
		else if (BulletDamageType == Car2DGun.BulletType.Debuff)
		{
			animatorsssss.SetBool(_freezeRay_isOn, value: true);
			yield return new WaitForSeconds(1f);
		}
		if (ShootAnim != null && SpawnAfterAnimation)
		{
			while (ShootAnim.isPlaying)
			{
				yield return 0;
			}
		}
		GameObject bullet = null;
		if (!IsShop)
		{
			bullet = Pool.Animate(Pool.Name(BulletPrefab), ShootPoint.position, "CarMain");
			if (BulletPrefab != Pool.Bombs.freezeRay)
			{
				bullet.transform.eulerAngles = new Vector3(0f, 0f, 90f);
			}
		}
		else if (!IsShop)
		{
			bullet.transform.rotation = Quaternion.identity;
		}
		if (!IsShop)
		{
			bullet.SetActive(value: true);
			bullet.transform.position = ShootPoint.position;
			bul = bullet.GetComponent<Bullet>();
		}
		float carVelocity = 0f;
		if (!IsShop)
		{
			bul.Init(IsAhead, IsHoming, HasParticles, base.transform.localRotation, target, BulletDamage, BulletMoveSpeed, BulletAngularSpeed, BulletLifeTime, BulletLayerMask, BulletDamageType, ParticleFly, carVelocity, StartPower, ShootPoint);
		}
		if (LauncherGadgetCallback != null)
		{
			LauncherGadgetCallback(target);
		}
		shootDelayTimer = 0f;
		ammo--;
		if (ammo == 0)
		{
			rearmTimer = 0f;
		}
		isShooting = false;
		if (!IsShop)
		{
			DoSomethingWithBullet(bul);
		}
		if (NeedReloadAnimation && ReloadAnim != null)
		{
			if (string.IsNullOrEmpty(ReloadAnimClipName))
			{
				ReloadAnim.Play();
			}
			else
			{
				ReloadAnim.Play(ReloadAnimClipName);
			}
		}
		if (BulletDamageType == Car2DGun.BulletType.Basic)
		{
			animatorsssss.SetBool(_missileLauncha_isOn, value: false);
		}
		else if (BulletDamageType == Car2DGun.BulletType.Debuff)
		{
			animatorsssss.SetBool(_freezeRay_isOn, value: false);
		}
		AfterReload();
	}

	protected virtual void DoSomethingWithBullet(Bullet bullet)
	{
	}

	protected virtual void AfterReload()
	{
	}
}
