using UnityEngine;
using UnityEngine.EventSystems;

public class LevelGalleryPackScroll : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
{
	public delegate void DelOnScroll(bool left);

	public DelOnScroll OnNeedToScroll = delegate
	{
	};

	private PointerEventData pointer;

	public void OnDrag(PointerEventData eventData)
	{
		if (pointer != null && pointer == eventData)
		{
			Vector2 position = pointer.position;
			float x = position.x;
			Vector2 pressPosition = pointer.pressPosition;
			float num = x - pressPosition.x;
			if (Mathf.Abs(num) > (float)Screen.width * 0.2f)
			{
				OnNeedToScroll(num > 0f);
				pointer = null;
			}
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		pointer = eventData;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		pointer = null;
	}
}
