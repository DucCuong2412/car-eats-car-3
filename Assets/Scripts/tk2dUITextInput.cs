using System;
using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("2D Toolkit/UI/tk2dUITextInput")]
public class tk2dUITextInput : MonoBehaviour
{
	public tk2dUIItem selectionBtn;

	public tk2dTextMesh inputLabel;

	public tk2dTextMesh emptyDisplayLabel;

	public GameObject unSelectedStateGO;

	public GameObject selectedStateGO;

	public GameObject cursor;

	public float fieldLength = 1f;

	public int maxCharacterLength = 30;

	public string emptyDisplayText;

	public bool isPasswordField;

	public string passwordChar = "*";

	[SerializeField]
	[HideInInspector]
	private tk2dUILayout layoutItem;

	private bool isSelected;

	private bool wasStartedCalled;

	private bool wasOnAnyPressEventAttached;

	private TouchScreenKeyboard keyboard;

	private bool listenForKeyboardText;

	private bool isDisplayTextShown;

	public Action<tk2dUITextInput> OnTextChange;

	public string SendMessageOnTextChangeMethodName = string.Empty;

	private string text = string.Empty;

	public tk2dUILayout LayoutItem
	{
		get
		{
			return layoutItem;
		}
		set
		{
			if (layoutItem != value)
			{
				if (layoutItem != null)
				{
					layoutItem.OnReshape -= LayoutReshaped;
				}
				layoutItem = value;
				if (layoutItem != null)
				{
					layoutItem.OnReshape += LayoutReshaped;
				}
			}
		}
	}

	public GameObject SendMessageTarget
	{
		get
		{
			if (selectionBtn != null)
			{
				return selectionBtn.sendMessageTarget;
			}
			return null;
		}
		set
		{
			if (selectionBtn != null && selectionBtn.sendMessageTarget != value)
			{
				selectionBtn.sendMessageTarget = value;
			}
		}
	}

	public bool IsFocus => isSelected;

	public string Text
	{
		get
		{
			return text;
		}
		set
		{
			if (text != value)
			{
				text = value;
				if (text.Length > maxCharacterLength)
				{
					text = text.Substring(0, maxCharacterLength);
				}
				FormatTextForDisplay(text);
				if (isSelected)
				{
					SetCursorPosition();
				}
			}
		}
	}

	private void Awake()
	{
		SetState();
		ShowDisplayText();
	}

	private void Start()
	{
		wasStartedCalled = true;
		if (tk2dUIManager.Instance__NoCreate != null)
		{
			tk2dUIManager.Instance.OnAnyPress += AnyPress;
		}
		wasOnAnyPressEventAttached = true;
	}

	private void OnEnable()
	{
		if (wasStartedCalled && !wasOnAnyPressEventAttached && tk2dUIManager.Instance__NoCreate != null)
		{
			tk2dUIManager.Instance.OnAnyPress += AnyPress;
		}
		if (layoutItem != null)
		{
			layoutItem.OnReshape += LayoutReshaped;
		}
		selectionBtn.OnClick += InputSelected;
	}

	private void OnDisable()
	{
		if (tk2dUIManager.Instance__NoCreate != null)
		{
			tk2dUIManager.Instance.OnAnyPress -= AnyPress;
			if (listenForKeyboardText)
			{
				tk2dUIManager.Instance.OnInputUpdate -= ListenForKeyboardTextUpdate;
			}
		}
		wasOnAnyPressEventAttached = false;
		selectionBtn.OnClick -= InputSelected;
		listenForKeyboardText = false;
		if (layoutItem != null)
		{
			layoutItem.OnReshape -= LayoutReshaped;
		}
	}

	public void SetFocus()
	{
		SetFocus(focus: true);
	}

	public void SetFocus(bool focus)
	{
		if (!IsFocus && focus)
		{
			InputSelected();
		}
		else if (IsFocus && !focus)
		{
			InputDeselected();
		}
	}

	private void FormatTextForDisplay(string modifiedText)
	{
		if (isPasswordField)
		{
			int length = modifiedText.Length;
			char paddingChar = (passwordChar.Length <= 0) ? '*' : passwordChar[0];
			modifiedText = string.Empty;
			modifiedText = modifiedText.PadRight(length, paddingChar);
		}
		inputLabel.text = modifiedText;
		inputLabel.Commit();
		while (true)
		{
			Vector3 extents = inputLabel.GetComponent<Renderer>().bounds.extents;
			if (!(extents.x * 2f > fieldLength))
			{
				break;
			}
			modifiedText = modifiedText.Substring(1, modifiedText.Length - 1);
			inputLabel.text = modifiedText;
			inputLabel.Commit();
		}
		if (modifiedText.Length == 0 && !listenForKeyboardText)
		{
			ShowDisplayText();
		}
		else
		{
			HideDisplayText();
		}
	}

	private void ListenForKeyboardTextUpdate()
	{
		bool flag = false;
		string arg = text;
		string inputString = Input.inputString;
		foreach (char c in inputString)
		{
			if (c == "\b"[0])
			{
				if (text.Length != 0)
				{
					arg = text.Substring(0, text.Length - 1);
					flag = true;
				}
			}
			else if (c != "\n"[0] && c != "\r"[0] && c != '\t' && c != '\u001b')
			{
				arg += c;
				flag = true;
			}
		}
		if (flag)
		{
			Text = arg;
			if (OnTextChange != null)
			{
				OnTextChange(this);
			}
			if (SendMessageTarget != null && SendMessageOnTextChangeMethodName.Length > 0)
			{
				SendMessageTarget.SendMessage(SendMessageOnTextChangeMethodName, this, SendMessageOptions.RequireReceiver);
			}
		}
	}

	private void InputSelected()
	{
		if (text.Length == 0)
		{
			HideDisplayText();
		}
		isSelected = true;
		if (!listenForKeyboardText)
		{
			tk2dUIManager.Instance.OnInputUpdate += ListenForKeyboardTextUpdate;
		}
		listenForKeyboardText = true;
		SetState();
		SetCursorPosition();
		if (Application.platform != RuntimePlatform.WindowsEditor && Application.platform != 0)
		{
			TouchScreenKeyboard.hideInput = false;
			keyboard = TouchScreenKeyboard.Open(text, TouchScreenKeyboardType.Default, autocorrection: false, multiline: false, secure: false, alert: false);
			StartCoroutine(TouchScreenKeyboardLoop());
		}
	}

	private IEnumerator TouchScreenKeyboardLoop()
	{
		while (keyboard != null && !keyboard.done && keyboard.active)
		{
			Text = keyboard.text;
			yield return null;
		}
		if (keyboard != null)
		{
			Text = keyboard.text;
		}
		if (isSelected)
		{
			InputDeselected();
		}
	}

	private void InputDeselected()
	{
		if (text.Length == 0)
		{
			ShowDisplayText();
		}
		isSelected = false;
		if (listenForKeyboardText)
		{
			tk2dUIManager.Instance.OnInputUpdate -= ListenForKeyboardTextUpdate;
		}
		listenForKeyboardText = false;
		SetState();
		if (keyboard != null && !keyboard.done)
		{
			keyboard.active = false;
		}
		keyboard = null;
	}

	private void AnyPress()
	{
		if (isSelected && tk2dUIManager.Instance.PressedUIItem != selectionBtn)
		{
			InputDeselected();
		}
	}

	private void SetState()
	{
		tk2dUIBaseItemControl.ChangeGameObjectActiveStateWithNullCheck(unSelectedStateGO, !isSelected);
		tk2dUIBaseItemControl.ChangeGameObjectActiveStateWithNullCheck(selectedStateGO, isSelected);
		tk2dUIBaseItemControl.ChangeGameObjectActiveState(cursor, isSelected);
	}

	private void SetCursorPosition()
	{
		float num = 1f;
		float num2 = 0.002f;
		if (inputLabel.anchor == TextAnchor.MiddleLeft || inputLabel.anchor == TextAnchor.LowerLeft || inputLabel.anchor == TextAnchor.UpperLeft)
		{
			num = 2f;
		}
		else if (inputLabel.anchor == TextAnchor.MiddleRight || inputLabel.anchor == TextAnchor.LowerRight || inputLabel.anchor == TextAnchor.UpperRight)
		{
			num = -2f;
			num2 = 0.012f;
		}
		if (text.EndsWith(" "))
		{
			tk2dFontChar tk2dFontChar = (!inputLabel.font.useDictionary) ? inputLabel.font.chars[32] : inputLabel.font.charDict[32];
			float num3 = num2;
			float advance = tk2dFontChar.advance;
			Vector3 scale = inputLabel.scale;
			num2 = num3 + advance * scale.x / 2f;
		}
		Transform transform = cursor.transform;
		Vector3 localPosition = inputLabel.transform.localPosition;
		float x = localPosition.x;
		Vector3 extents = inputLabel.GetComponent<Renderer>().bounds.extents;
		float x2 = x + (extents.x + num2) * num;
		Vector3 localPosition2 = cursor.transform.localPosition;
		float y = localPosition2.y;
		Vector3 localPosition3 = cursor.transform.localPosition;
		transform.localPosition = new Vector3(x2, y, localPosition3.z);
	}

	private void ShowDisplayText()
	{
		if (!isDisplayTextShown)
		{
			isDisplayTextShown = true;
			if (emptyDisplayLabel != null)
			{
				emptyDisplayLabel.text = emptyDisplayText;
				emptyDisplayLabel.Commit();
				tk2dUIBaseItemControl.ChangeGameObjectActiveState(emptyDisplayLabel.gameObject, isActive: true);
			}
			tk2dUIBaseItemControl.ChangeGameObjectActiveState(inputLabel.gameObject, isActive: false);
		}
	}

	private void HideDisplayText()
	{
		if (isDisplayTextShown)
		{
			isDisplayTextShown = false;
			tk2dUIBaseItemControl.ChangeGameObjectActiveStateWithNullCheck(emptyDisplayLabel.gameObject, isActive: false);
			tk2dUIBaseItemControl.ChangeGameObjectActiveState(inputLabel.gameObject, isActive: true);
		}
	}

	private void LayoutReshaped(Vector3 dMin, Vector3 dMax)
	{
		fieldLength += dMax.x - dMin.x;
		string text = this.text;
		this.text = string.Empty;
		Text = text;
	}
}
