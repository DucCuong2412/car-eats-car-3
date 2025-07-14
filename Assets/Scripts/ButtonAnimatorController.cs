using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimatorController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler, IEventSystemHandler
{
	public float scaleCoofOver = 1.05f;

	public float scaleCoofClick = 0.95f;

	public bool isAnimation;

	public float timeAnimation = 1f;

	public float scaleCoofAnimation = 1.05f;

	private Vector3 _basicScale;

	public AnimationCurve Curve;

	private IEnumerator anim;

	private bool flag = true;

	private void OnEnable()
	{
		_basicScale = base.transform.localScale;
		if (isAnimation)
		{
			anim = AnimButton();
		}
		if (isAnimation)
		{
			StartCoroutine(anim);
		}
	}

	private void OnDisable()
	{
		base.transform.localScale = _basicScale;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (isAnimation)
		{
			flag = false;
		}
		base.transform.localScale = _basicScale * scaleCoofClick;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (isAnimation)
		{
			flag = true;
		}
		base.transform.localScale = _basicScale;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (isAnimation)
		{
			flag = true;
		}
		base.transform.localScale = _basicScale;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (isAnimation)
		{
			flag = false;
		}
		base.transform.localScale = _basicScale * scaleCoofOver;
	}

	private IEnumerator AnimButton()
	{
		while (true)
		{
			float time = 0f;
			while (flag && time < timeAnimation)
			{
				time += Time.deltaTime;
				float num = time / timeAnimation;
				base.transform.localScale = _basicScale * Curve.Evaluate(time);
				yield return 0;
			}
			yield return new WaitForSeconds(0.1f);
		}
	}
}
