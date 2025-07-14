using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunAnimController : MonoBehaviour
{
	public Animation anim;

	public TurelAIScript TAS;

	public float reload;

	public float DistansToActivation;

	private bool show = true;

	private bool hide = true;

	private string on;

	private string off;

	private string activ;

	private string deactiv;

	private string fire;

	private List<string> states = new List<string>();

	private float timer;

	private void OnEnable()
	{
		IEnumerator enumerator = anim.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				AnimationState animationState = (AnimationState)enumerator.Current;
				if (animationState.name.Contains("_off"))
				{
					off = animationState.name;
				}
				if (animationState.name.Contains("_on"))
				{
					on = animationState.name;
				}
				if (animationState.name.Contains("_activate"))
				{
					activ = animationState.name;
				}
				if (animationState.name.Contains("_fire"))
				{
					fire = animationState.name;
				}
				if (animationState.name.Contains("_disactivate"))
				{
					deactiv = animationState.name;
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		StartCoroutine(animation());
	}

	private IEnumerator animation()
	{
		while (true)
		{
			if (TAS.TargetCar != null)
			{
				Vector3 position = TAS.TargetCar.position;
				float x = position.x;
				Vector3 position2 = TAS.gameObject.transform.position;
				if (Mathf.Abs(x - position2.x) < DistansToActivation)
				{
					if (show)
					{
						show = false;
						hide = true;
						anim.Play(activ);
					}
					anim.Play(fire);
					yield return new WaitForSeconds(1f);
					while (timer < reload - 1f)
					{
						yield return 0;
						timer += Time.deltaTime;
						anim.Play(on);
					}
					timer = 0f;
				}
				else
				{
					if (hide)
					{
						hide = false;
						anim.Play(deactiv);
						anim.Play(off);
					}
					show = true;
					anim.Play(off);
				}
			}
			yield return 0;
		}
	}
}
