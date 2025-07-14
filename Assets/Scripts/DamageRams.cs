using System.Collections;
using UnityEngine;

public class DamageRams : MonoBehaviour
{
	private static string CarMain = "CarMain";

	private static string CarMainChild = "CarMainChild";

	private static string actor_bite_01 = "actor_bite_01";

	private float eatTime;

	private bool can_eat = true;

	private Rigidbody2D r;

	public Animator RamAnimator;

	private int _isActive = Animator.StringToHash("isActive");

	public float eat_delay = 0.5f;

	public float EatDamage;

	public float DistansToActivation = 15f;

	private bool ramOpen;

	private void OnEnable()
	{
		ramOpen = false;
	}

	private void OnTriggerStay2D(Collider2D c)
	{
		Eating(c.gameObject);
	}

	public void Eating(GameObject g)
	{
		if ((!(g.tag != CarMain) || !(g.tag != CarMainChild)) && can_eat)
		{
			RaceLogic.instance.EatMainCar(EatDamage);
			can_eat = false;
			eatTime = eat_delay;
			if (base.gameObject.activeInHierarchy)
			{
				StartCoroutine(EatDelay());
			}
			Audio.PlayAsyncRandom(actor_bite_01, actor_bite_01);
		}
	}

	private IEnumerator EatDelay()
	{
		float t = eat_delay;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return null;
		}
		can_eat = true;
	}

	private void Update()
	{
		if (!(RaceLogic.instance.car != null))
		{
			return;
		}
		if (Vector2.Distance(RaceLogic.instance.car.transform.position, base.transform.position) <= DistansToActivation)
		{
			if (!ramOpen)
			{
				if (RamAnimator != null)
				{
					RamAnimator.SetBool(_isActive, value: true);
				}
				ramOpen = true;
			}
		}
		else if (ramOpen)
		{
			if (RamAnimator != null)
			{
				RamAnimator.SetBool(_isActive, value: false);
			}
			ramOpen = false;
		}
	}
}
