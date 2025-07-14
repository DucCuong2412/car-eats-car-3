using System;
using System.Collections;
using UnityEngine;

public class turel : MonoBehaviour
{
	private float rotationSpeed = 3f;

	private float rotateDirection;

	public GameObject OnOff;

	public Animation OnOffanim;

	private string on;

	private string off;

	private string activ;

	private string deactiv;

	private string fire;

	private void OnEnable()
	{
		OnOffanim = OnOff.GetComponentInChildren<Animation>();
		IEnumerator enumerator = OnOffanim.GetEnumerator();
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
	}

	private void LateUpdate()
	{
		if (RaceLogic.instance.car == null)
		{
			return;
		}
		Vector3 position = RaceLogic.instance.car.transform.position;
		float y = position.y;
		Vector3 position2 = base.transform.position;
		if (y < position2.y)
		{
			if (base.name.Contains("_f"))
			{
				rotateDirection = -1f;
			}
			else if (base.name.Contains("_b"))
			{
				rotateDirection = 1f;
			}
		}
		else
		{
			float y2 = position.y;
			Vector3 position3 = base.transform.position;
			if (y2 > position3.y)
			{
				if (base.name.Contains("_f"))
				{
					rotateDirection = 1f;
				}
				else if (base.name.Contains("_b"))
				{
					rotateDirection = -1f;
				}
			}
			else
			{
				rotateDirection = 0f;
			}
		}
		Quaternion localRotation = base.transform.localRotation;
		if ((double)localRotation.z > -0.166)
		{
			Quaternion localRotation2 = base.transform.localRotation;
			if ((double)localRotation2.z < 0.166)
			{
				base.transform.localRotation *= Quaternion.Euler(0f, 0f, rotationSpeed * rotateDirection);
			}
			else
			{
				base.transform.localRotation = Quaternion.Euler(0f, 0f, 19f);
			}
		}
		else
		{
			base.transform.localRotation = Quaternion.Euler(0f, 0f, -19f);
		}
		float y3 = position.y;
		Vector3 position4 = base.transform.position;
		if (!(y3 - position4.y > 3f))
		{
			float y4 = position.y;
			Vector3 position5 = base.transform.position;
			if (!(y4 - position5.y < -3f))
			{
				OnOffanim.Play(on);
				return;
			}
		}
		Animation onOffanim = OnOffanim;
		IEnumerator enumerator = onOffanim.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				AnimationState animationState = (AnimationState)enumerator.Current;
				string name = animationState.name;
				onOffanim[name].time = 0f;
				onOffanim.Sample();
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
		OnOffanim.Play(off);
	}
}
