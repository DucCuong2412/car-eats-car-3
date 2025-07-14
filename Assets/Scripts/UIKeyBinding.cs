using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Key Binding")]
public class UIKeyBinding : MonoBehaviour
{
	public enum Action
	{
		PressAndClick,
		Select,
		All
	}

	public enum Modifier
	{
		None,
		Shift,
		Control,
		Alt
	}

	public KeyCode keyCode;

	public Modifier modifier;

	public Action action;

	private bool mIgnoreUp;

	private bool mIsInput;

	private bool mPress;

	protected virtual void Start()
	{
		UIInput component = GetComponent<UIInput>();
		mIsInput = (component != null);
		if (component != null)
		{
			EventDelegate.Add(component.onSubmit, OnSubmit);
		}
	}

	protected virtual void OnSubmit()
	{
		if (UICamera.currentKey == keyCode && IsModifierActive())
		{
			mIgnoreUp = true;
		}
	}

	protected virtual bool IsModifierActive()
	{
		if (modifier == Modifier.None)
		{
			return true;
		}
		if (modifier == Modifier.Alt)
		{
			if (UnityEngine.Input.GetKey(KeyCode.LeftAlt) || UnityEngine.Input.GetKey(KeyCode.RightAlt))
			{
				return true;
			}
		}
		else if (modifier == Modifier.Control)
		{
			if (UnityEngine.Input.GetKey(KeyCode.LeftControl) || UnityEngine.Input.GetKey(KeyCode.RightControl))
			{
				return true;
			}
		}
		else if (modifier == Modifier.Shift && (UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift)))
		{
			return true;
		}
		return false;
	}

	protected virtual void Update()
	{
		if (UICamera.inputHasFocus || keyCode == KeyCode.None || !IsModifierActive())
		{
			return;
		}
		bool keyDown = UnityEngine.Input.GetKeyDown(keyCode);
		bool keyUp = UnityEngine.Input.GetKeyUp(keyCode);
		if (keyDown)
		{
			mPress = true;
		}
		if (action == Action.PressAndClick || action == Action.All)
		{
			UICamera.currentTouch = UICamera.controller;
			UICamera.currentScheme = UICamera.ControlScheme.Mouse;
			UICamera.currentTouch.current = base.gameObject;
			if (keyDown)
			{
				OnBindingPress(pressed: true);
			}
			if (mPress && keyUp)
			{
				OnBindingPress(pressed: false);
				OnBindingClick();
			}
			UICamera.currentTouch.current = null;
		}
		if ((action == Action.Select || action == Action.All) && keyUp)
		{
			if (mIsInput)
			{
				if (!mIgnoreUp && !UICamera.inputHasFocus && mPress)
				{
					UICamera.selectedObject = base.gameObject;
				}
				mIgnoreUp = false;
			}
			else if (mPress)
			{
				UICamera.selectedObject = base.gameObject;
			}
		}
		if (keyUp)
		{
			mPress = false;
		}
	}

	protected virtual void OnBindingPress(bool pressed)
	{
		UICamera.Notify(base.gameObject, "OnPress", pressed);
	}

	protected virtual void OnBindingClick()
	{
		UICamera.Notify(base.gameObject, "OnClick", null);
	}
}
