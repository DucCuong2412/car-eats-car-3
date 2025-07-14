using System.Collections;
using UnityEngine;

public class EMP : MonoBehaviour
{
	public Animator anim;

	private const int maxCount = 50;

	private int layerExplosion;

	private RaycastHit2D[] hits = new RaycastHit2D[50];

	public float Reload;

	public GameObject target;

	public float Radius;

	public float Power;

	public float Damage;

	public BombLogic.types types;

	public bool garage;

	private int _EMPemmiter_isOn = Animator.StringToHash("EMPemmiter_isOn");

	private float i;

	private int exp3FragmentDamage = 10;

	public float shockWaveDamage = 10f;

	public float shockWaveDuration = 10f;

	private bool isMainCarDamaged;

	public void Awake()
	{
		layerExplosion = LayerMask.GetMask("EnemyCar");
	}

	private void Update()
	{
		if (!garage)
		{
			if (i > Reload)
			{
				StartCoroutine(booms());
				i = 0f;
			}
			else
			{
				i += Time.deltaTime;
			}
		}
	}

	private IEnumerator booms()
	{
		anim.SetBool(_EMPemmiter_isOn, value: true);
		yield return new WaitForSeconds(3f);
		Explosion(target.transform.position, Radius, Power, Damage, types);
		anim.SetBool(_EMPemmiter_isOn, value: false);
	}

	private IEnumerator sled(GameObject boom)
	{
		float ij = 0f;
		while (ij < 3f)
		{
			ij += Time.deltaTime;
			yield return 0;
			boom.transform.position = target.transform.position;
		}
	}

	public void Explosion(Vector3 pos, float radius, float power, float damage, BombLogic.types types = BombLogic.types.DEFAULT)
	{
		int num = Physics2D.CircleCastNonAlloc(pos, radius, Vector2.zero, hits, 0f, layerExplosion);
		for (int i = 0; i < num; i++)
		{
			Rigidbody2D component = hits[i].collider.GetComponent<Rigidbody2D>();
			float value = Vector2.Distance(hits[i].transform.position, pos);
			float num2 = Get1_0FromDistance(value, radius);
			Vector2 force = -hits[i].normal * power * num2;
			if (types == BombLogic.types.REDUSER && hits[i].transform.name.Contains("Enemy"))
			{
				hits[i].transform.gameObject.SetActive(value: false);
			}
			if ((bool)component)
			{
				component.AddForce(force);
				component.BroadcastMessage("OnHit", damage * num2, SendMessageOptions.DontRequireReceiver);
				component.SendMessageUpwards("ChangeHealth", 0f - damage * num2, SendMessageOptions.DontRequireReceiver);
				expl3(component.gameObject.transform);
			}
		}
	}

	private float Get1_0FromDistance(float value, float maxDistance)
	{
		float num = Mathf.Clamp(value, 0f, maxDistance);
		return 1f - num / maxDistance;
	}

	private void expl3(Transform _hitTransform)
	{
		setFragmentDamage(_hitTransform, exp3FragmentDamage);
		if (!_hitTransform)
		{
			return;
		}
		if (_hitTransform.gameObject.tag == "CarEnemy")
		{
			Transform parent = _hitTransform.parent.parent.parent;
			Car2DAIController component = parent.GetComponent<Car2DAIController>();
			if (component != null)
			{
				StartCoroutine(shockWave(component));
			}
		}
		if (isMainCarDamaged && _hitTransform.gameObject.tag == "CarMain" && _hitTransform.GetComponent<Car2DController>() != null)
		{
			StartCoroutine(shockWave(_hitTransform.GetComponent<Car2DController>()));
		}
	}

	private void setFragmentDamage(Transform t, int damage)
	{
		if (t != null && (isMainCarDamaged || (!(t.tag == "CarMain") && !(t.tag == "CarMainChild"))))
		{
			t.SendMessageUpwards("ChangeHealth", -damage, SendMessageOptions.DontRequireReceiver);
		}
	}

	private IEnumerator shockWave(Car2DAIController hitC)
	{
		GameObject eff = Pool.Animate(Pool.Explosion.exp2, hitC.transform, "GUI");
		if (hitC != null && hitC.gameObject.activeSelf)
		{
			hitC.EngineModule.moduleEnabled = false;
			hitC.EngineModule.Break(onoff: true);
		}
		float damage = shockWaveDamage;
		float duration = shockWaveDuration;
		for (int i = 0; i < 10; i++)
		{
			if (!hitC.gameObject.activeSelf)
			{
				break;
			}
			int dam = (int)(0f - ((!(damage / 10f < 1f)) ? (damage / 10f) : 1f));
			hitC.HealthModule.ChangeHealth(dam);
			yield return new WaitForSeconds(duration / 10f);
		}
		eff.GetComponent<ParticleSystem>().Stop(withChildren: true);
		if (hitC.gameObject.activeSelf)
		{
			hitC.EngineModule.Break(onoff: false);
			hitC.EngineModule.moduleEnabled = true;
		}
	}

	private IEnumerator shockWave(Car2DController hitC)
	{
		GameObject eff = Pool.Animate(Pool.Explosion.exp2, hitC.transform, "GUI");
		if (hitC != null && hitC.gameObject.activeSelf)
		{
			hitC.EngineModule.moduleEnabled = false;
			hitC.EngineModule.Break(onoff: true);
		}
		float damage = shockWaveDamage;
		float duration = shockWaveDuration;
		for (int i = 0; i < 10; i++)
		{
			if (!hitC.gameObject.activeSelf)
			{
				break;
			}
			int dam = (int)(0f - ((!(damage / 10f < 1f)) ? (damage / 10f) : 1f));
			hitC.HealthModule.ChangeHealth(dam);
			yield return new WaitForSeconds(duration / 10f);
		}
		eff.GetComponent<ParticleSystem>().Stop(withChildren: true);
		if (hitC.gameObject.activeSelf)
		{
			hitC.EngineModule.Break(onoff: false);
			hitC.EngineModule.moduleEnabled = true;
		}
	}
}
