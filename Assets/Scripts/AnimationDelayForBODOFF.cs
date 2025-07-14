using System.Collections;
using UnityEngine;

public class AnimationDelayForBODOFF : MonoBehaviour
{
	public float DelayForStart = 1f;

	public float StartRandomDelayMin = -1f;

	public float StartRandomDelayMax = -1f;

	public float StaticDelayWithoutAnim = -1f;

	public float RandomDelayWithoutAnimMin = -1f;

	public float RandomDelayWithoutAnimMax = -1f;

	public float OffsetX;

	public float OffsetY;

	private Vector3 StartPoint = Vector3.zero;

	private Animation anim;

	private Coroutine corut;

	private bool firstOpen;

	private void OnEnable()
	{
		anim = base.gameObject.GetComponent<Animation>();
		if (corut != null)
		{
			StopCoroutine(corut);
		}
		corut = StartCoroutine(DelayCorut());
		StartPoint = Vector3.zero;
		firstOpen = false;
	}

	private IEnumerator DelayCorut()
	{
		float t2 = (StartRandomDelayMin == -1f) ? DelayForStart : UnityEngine.Random.Range(StartRandomDelayMin, StartRandomDelayMax);
		while (t2 > 0f)
		{
			StartPoint = base.gameObject.transform.localPosition;
			t2 -= Time.deltaTime;
			yield return null;
		}
		anim.Play();
		while (true)
		{
			t2 = ((StaticDelayWithoutAnim == -1f) ? UnityEngine.Random.Range(RandomDelayWithoutAnimMin, RandomDelayWithoutAnimMax) : StaticDelayWithoutAnim);
			while (t2 > 0f)
			{
				t2 -= Time.deltaTime;
				yield return null;
			}
			base.gameObject.transform.localPosition = new Vector3(StartPoint.x + UnityEngine.Random.Range(0f - OffsetX, OffsetX), StartPoint.y + UnityEngine.Random.Range(0f - OffsetY, OffsetY), StartPoint.z);
			anim.Play();
			yield return null;
		}
	}

	private void OnDisable()
	{
		if (corut != null)
		{
			StopCoroutine(corut);
		}
	}
}
