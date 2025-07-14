using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GUIControllsButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IEventSystemHandler
{
	public enum Type
	{
		Normal = 1,
		Pressed,
		Disabled
	}

	[HideInInspector]
	public Type type = Type.Normal;

	public GameObject buttonContainer;

	public Image buttonIcon;

	public Action on;

	public Action off;

	private Vector3 pressedScale = new Vector3(0.9f, 0.9f);

	private bool isLateDisable;

	private List<EventDelegate> onPress1 = new List<EventDelegate>();

	private List<EventDelegate> onRelease1 = new List<EventDelegate>();

	private List<EventDelegate> onDragOver1 = new List<EventDelegate>();

	private List<EventDelegate> onDragOut1 = new List<EventDelegate>();

	public void OnPointerDown(PointerEventData eventData)
	{
		onPress();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		onRelease();
	}

	public void onPress()
	{
		if (type != Type.Pressed && type != Type.Disabled)
		{
			Set(Type.Pressed);
		}
	}

	public void onRelease()
	{
		if (type != Type.Normal && type != Type.Disabled)
		{
			if (isLateDisable)
			{
				isLateDisable = false;
				Dis();
			}
			else
			{
				Set(Type.Normal);
			}
		}
	}

	public void onDragOver()
	{
		if (type != Type.Pressed && type != Type.Disabled)
		{
			Set(Type.Pressed);
		}
	}

	public void onDragOut()
	{
		if (type != Type.Normal && type != Type.Disabled)
		{
			if (isLateDisable)
			{
				isLateDisable = false;
				Dis();
			}
			else
			{
				Set(Type.Normal);
			}
		}
	}

	public void Set(Type btype)
	{
		switch (btype)
		{
		case Type.Normal:
			isLateDisable = false;
			type = Type.Normal;
			buttonContainer.transform.localScale = Vector3.one;
			off();
			break;
		case Type.Pressed:
			type = Type.Pressed;
			buttonContainer.transform.localScale = pressedScale;
			on();
			break;
		case Type.Disabled:
			if (type == Type.Pressed && !isLateDisable)
			{
				isLateDisable = true;
			}
			else
			{
				Dis();
			}
			break;
		}
	}

	public void Show()
	{
		base.gameObject.SetActive(value: true);
	}

	public void Hide()
	{
		base.gameObject.SetActive(value: false);
	}

	private void Dis()
	{
		type = Type.Disabled;
		buttonContainer.transform.localScale = Vector3.one;
	}

	private void Start()
	{
		EventDelegate item = new EventDelegate(this, "onPress");
		EventDelegate item2 = new EventDelegate(this, "onRelease");
		EventDelegate item3 = new EventDelegate(this, "onDragOver");
		EventDelegate item4 = new EventDelegate(this, "onDragOut");
		onPress1.Add(item);
		onRelease1.Add(item2);
		onDragOver1.Add(item3);
		onDragOut1.Add(item4);
	}

	private void OnPress(bool pressed)
	{
		EventDelegate.Execute((!pressed) ? onRelease1 : onPress1);
	}

	private void OnDragOver(GameObject go)
	{
		EventDelegate.Execute(onDragOver1);
	}

	private void OnDragOut(GameObject go)
	{
		EventDelegate.Execute(onDragOut1);
	}
}
