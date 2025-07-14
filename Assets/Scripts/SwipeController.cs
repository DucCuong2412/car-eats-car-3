using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Graphics))]
public class SwipeController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerClickHandler, IEventSystemHandler
{
	public delegate void DelOnClick(Vector2 pos);

	public DelOnClick OnClick = delegate
	{
	};

	public DelOnClick OnTap = delegate
	{
	};

	public Camera cam;

	public float zoomSpeed = 0.01f;

	public float minSize = 4f;

	public float SwipeDivisor = 100f;

	public bool VerticalMoves;

	public GameObject TestObj;

	private List<PointerEventData> pointers = new List<PointerEventData>();

	public void OnPointerDown(PointerEventData eventData)
	{
		pointers.Add(eventData);
		OnTap(eventData.pressPosition);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		pointers.Remove(eventData);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (pointers.Count != 1 && pointers.Count != 2)
		{
		}
	}

	public void ZoomChange(int num, float koef = 0.25f)
	{
		if (num != -1)
		{
			koef = 0f - koef;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		OnClick(eventData.pressPosition);
	}
}
