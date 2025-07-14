using System.Collections;
using UnityEngine;

public class animatorChangeStateAfterTime : MonoBehaviour
{
	public float time;

	public string nameBool;

	public bool StateBool;

	public Animator anim;

	private void OnEnable()
	{
		if (RaceLogic.instance.AllInitedForPool)
		{
			StartCoroutine(Rotina());
		}
	}

	private IEnumerator Rotina()
	{
		yield return new WaitForSeconds(time);
		anim.SetBool(nameBool, StateBool);
	}
}
