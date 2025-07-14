using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class ButtonPressTween : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
{
	public static ButtonPressTween current;

	public Vector2 pressedScale = new Vector2(0.95f, 0.95f);

	public float scaleDuration = 0.05f;

	public bool sound = true;

	[HideInInspector]
	public List<EventDelegate> onClick = new List<EventDelegate>();

	private RectTransform scaleTarget;

	private Vector3 mScale;

	private bool mStarted;

	private void Start()
	{
		if (!mStarted)
		{
			mStarted = true;
			if (scaleTarget == null)
			{
				scaleTarget = base.transform.GetComponent<RectTransform>();
			}
			mScale = scaleTarget.localScale;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		OnClick();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		OnPress(isPressed: true);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		OnPress(isPressed: false);
	}

	private void OnClick()
	{
		if (!(current != null))
		{
			current = this;
			EventDelegate.Execute(onClick);
			if (sound)
			{
				Audio.PlayAsync("gui_button_02_sn");
			}
			current = null;
		}
	}

	private void OnPress(bool isPressed)
	{
		if (current != null)
		{
			return;
		}
		current = this;
		if (base.enabled)
		{
			if (!mStarted)
			{
				Start();
			}
			TweenScale.Begin(scaleTarget.gameObject, scaleDuration, (!isPressed) ? mScale : Vector3.Scale(mScale, new Vector3(pressedScale.x, pressedScale.y, 1f))).method = UITweener.Method.EaseInOut;
		}
		current = null;
	}
}
