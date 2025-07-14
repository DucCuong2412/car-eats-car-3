using System.Collections;
using UnityEngine;

public class Acid_ControlledDmg : MonoBehaviour
{
	public float DmgPerFrame = 0.5f;

	private Animator anim;

	private void OnEnable()
	{
		anim = GetComponentInChildren<Animator>();
		if (anim != null)
		{
			StartCoroutine(upd());
		}
	}

	private IEnumerator upd()
	{
		while (true)
		{
			anim.SetBool("isAttack", value: true);
			yield return new WaitForSeconds(2f);
			anim.SetBool("isAttack", value: false);
			yield return new WaitForSeconds(2f);
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "CarMain" || other.tag == "CarMainChild")
		{
			RaceLogic.instance.HitMainCar(DmgPerFrame);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if ((other.tag == "CarMain" || other.tag == "CarMainChild") && anim != null)
		{
			Audio.PlayAsync("par", 1f);
		}
	}
}
