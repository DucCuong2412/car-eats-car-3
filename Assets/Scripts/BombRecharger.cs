using System;
using System.Collections;
using UnityEngine;

public class BombRecharger : MonoBehaviour
{
	public float time = 3.3f;

	private Animation _anim;

	public Action callback;

	public Animation Anim
	{
		get
		{
			if (_anim == null)
			{
				_anim = base.gameObject.GetComponent<Animation>();
			}
			return _anim;
		}
	}

	public void OnEnable()
	{
		StartCoroutine(PlayAnim());
	}

	public void Callback()
	{
		if (RaceLogic.instance.car != null)
		{
			RaceLogic.instance.car.BombModule.Increase(1);
		}
	}

	public IEnumerator PlayAnim()
	{
		yield return new WaitForSeconds(time);
		Callback();
		StartCoroutine(PlayAnim());
	}
}
