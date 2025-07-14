using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurelAIScript : MonoBehaviour
{
	public Animator GunAnimator;

	private int _isActive = Animator.StringToHash("isActive");

	public bool FixAngleByBody;

	public float Damage = 50f;

	public float ForvardAngle = 180f;

	public float BackAngle = 180f;

	public float AngularSpeed = 5f;

	public float FireTime = 0.25f;

	public float Force = 100f;

	public float MaxDistance = 15f;

	public Transform Turel;

	public Transform Target;

	public Pool.Bombs Wheezbang;

	public Pool.Explosion Effect;

	public string soundShoot = string.Empty;

	private Vector3 dir = new Vector3(1f, 0f, 0f);

	private float angle;

	private Animation _anim;

	private List<Transform> Shots = new List<Transform>();

	private Transform _targetCar;

	private bool gunOpen;

	private float timeToShoot;

	public Transform TargetCar
	{
		get
		{
			if (_targetCar == null)
			{
				_targetCar = RaceManager.instance.car;
			}
			return _targetCar;
		}
	}

	private void OnEnable()
	{
		if (!UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Contains("Levels"))
		{
			gunOpen = false;
			TurelLogic.instance.onHit += OnHit;
			StartCoroutine(CheckTarget());
			if (Effect != 0)
			{
				StartCoroutine(ShotEffect());
			}
			Target = TargetCar;
		}
	}

	private IEnumerator CheckTarget()
	{
		while (base.gameObject.activeSelf)
		{
			if (TargetCar != null && TargetCar.gameObject.activeSelf && Vector2.Distance(TargetCar.position, base.transform.position) <= MaxDistance)
			{
				if (!gunOpen)
				{
					gunOpen = true;
					if (GunAnimator != null)
					{
						GunAnimator.SetBool(_isActive, value: true);
					}
				}
				Shoot();
			}
			else if (gunOpen)
			{
				gunOpen = false;
				if (GunAnimator != null)
				{
					GunAnimator.SetBool(_isActive, value: false);
				}
			}
			if (!FixAngleByBody)
			{
				Turn(Mathf.Sign(dir.x));
				Aim();
			}
			yield return null;
		}
	}

	private void Aim()
	{
		if (TargetCar != null && TargetCar.gameObject.activeSelf)
		{
			dir = TargetCar.position - Turel.position;
		}
		else
		{
			dir = new Vector3(1f, 0f, 0f);
		}
		angle = Mathf.Atan2(dir.y, dir.x) * 57.29578f;
		float num = Mathf.Clamp(angle, (0f - ForvardAngle) / 2f, ForvardAngle / 2f);
		float num2 = Mathf.Sign(angle) * Mathf.Clamp(Mathf.Abs(angle), 180f - BackAngle / 2f, 180f);
		Turel.rotation = Quaternion.Slerp(Turel.rotation, Quaternion.AngleAxis((!(angle < 90f) || !(angle > -90f)) ? num2 : num, Vector3.forward), Time.deltaTime * AngularSpeed);
		UnityEngine.Debug.Log("Turel.rotation " + Turel.rotation + " | dir " + dir + " | angle " + angle + " | qw1 " + num + " | qw2 " + num2);
	}

	private void Turn(float _dir)
	{
		if (_dir > 0f)
		{
			Transform turel = Turel;
			Vector3 localScale = Turel.localScale;
			float x = localScale.x;
			Vector3 localScale2 = Turel.localScale;
			float y = Mathf.Abs(localScale2.y);
			Vector3 localScale3 = Turel.localScale;
			turel.localScale = new Vector3(x, y, localScale3.z);
		}
		else
		{
			Transform turel2 = Turel;
			Vector3 localScale4 = Turel.localScale;
			float x2 = localScale4.x;
			Vector3 localScale5 = Turel.localScale;
			float y2 = 0f - Mathf.Abs(localScale5.y);
			Vector3 localScale6 = Turel.localScale;
			turel2.localScale = new Vector3(x2, y2, localScale6.z);
		}
	}

	private IEnumerator ShotEffect()
	{
		while (base.gameObject.activeSelf)
		{
			for (int i = 0; i < Shots.Count; i++)
			{
				if (Shots[i].gameObject.activeSelf)
				{
					Pool.Animate(Effect, Shots[i].position + new Vector3(UnityEngine.Random.Range(0f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f), 0f));
				}
			}
			yield return new WaitForSeconds(0.01f);
		}
	}

	private void Shoot()
	{
		if (timeToShoot >= FireTime)
		{
			Vector3 a = Target.position - Turel.position;
			float magnitude = a.magnitude;
			Vector3 v = a / magnitude;
			GameObject @object = Pool.instance.GetObject(Pool.Name(Wheezbang));
			@object.transform.rotation = Turel.rotation;
			if (!Shots.Contains(@object.transform))
			{
				Shots.Add(@object.transform);
			}
			TurelLogic.instance.fireBullets(@object, Target.position, v, Force, 10f, 0f, ai: true);
			timeToShoot = 0f;
			if (soundShoot.Length > 0)
			{
				Audio.PlayAsync(soundShoot);
			}
			if (GunAnimator != null)
			{
				GunAnimator.SetTrigger("attack");
			}
		}
		else
		{
			timeToShoot += Time.deltaTime;
		}
	}

	private void OnHit(Transform sh, Transform hitTransform, Vector2 hitPoint)
	{
		if ((bool)hitTransform && Shots.Contains(sh))
		{
			hitTransform.gameObject.SendMessageUpwards("ChangeHealth", 0f - Damage, SendMessageOptions.DontRequireReceiver);
			Shots.Remove(sh);
		}
	}

	public void OnDisable()
	{
		TurelLogic.instance.onHit -= OnHit;
	}
}
