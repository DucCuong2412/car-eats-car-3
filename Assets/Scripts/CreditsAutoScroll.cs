using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class CreditsAutoScroll : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
{
	private ScrollRect scroll;

	private bool isTouched;

	private void OnEnable()
	{
		scroll = GetComponent<ScrollRect>();
		scroll.verticalNormalizedPosition = 1f;
	}

	private void Update()
	{
		if (scroll.verticalNormalizedPosition > 0f && !isTouched)
		{
			scroll.verticalNormalizedPosition -= 0.001f;
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		isTouched = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		isTouched = false;
	}
}
