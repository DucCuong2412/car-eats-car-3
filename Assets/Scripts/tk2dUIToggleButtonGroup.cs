using System;
using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/tk2dUIToggleButtonGroup")]
public class tk2dUIToggleButtonGroup : MonoBehaviour
{
	[SerializeField]
	private tk2dUIToggleButton[] toggleBtns;

	public GameObject sendMessageTarget;

	[SerializeField]
	private int selectedIndex;

	private tk2dUIToggleButton selectedToggleButton;

	public string SendMessageOnChangeMethodName = string.Empty;

	public tk2dUIToggleButton[] ToggleBtns => toggleBtns;

	public int SelectedIndex
	{
		get
		{
			return selectedIndex;
		}
		set
		{
			if (selectedIndex != value)
			{
				selectedIndex = value;
				SetToggleButtonUsingSelectedIndex();
			}
		}
	}

	public tk2dUIToggleButton SelectedToggleButton
	{
		get
		{
			return selectedToggleButton;
		}
		set
		{
			ButtonToggle(value);
		}
	}

	public event Action<tk2dUIToggleButtonGroup> OnChange;

	protected virtual void Awake()
	{
		Setup();
	}

	protected void Setup()
	{
		tk2dUIToggleButton[] array = toggleBtns;
		foreach (tk2dUIToggleButton tk2dUIToggleButton in array)
		{
			if (tk2dUIToggleButton != null)
			{
				tk2dUIToggleButton.IsInToggleGroup = true;
				tk2dUIToggleButton.IsOn = false;
				tk2dUIToggleButton.OnToggle += ButtonToggle;
			}
		}
		SetToggleButtonUsingSelectedIndex();
	}

	public void AddNewToggleButtons(tk2dUIToggleButton[] newToggleBtns)
	{
		ClearExistingToggleBtns();
		toggleBtns = newToggleBtns;
		Setup();
	}

	private void ClearExistingToggleBtns()
	{
		if (toggleBtns != null && toggleBtns.Length > 0)
		{
			tk2dUIToggleButton[] array = toggleBtns;
			foreach (tk2dUIToggleButton tk2dUIToggleButton in array)
			{
				tk2dUIToggleButton.IsInToggleGroup = false;
				tk2dUIToggleButton.OnToggle -= ButtonToggle;
				tk2dUIToggleButton.IsOn = false;
			}
		}
	}

	private void SetToggleButtonUsingSelectedIndex()
	{
		tk2dUIToggleButton tk2dUIToggleButton = null;
		if (selectedIndex >= 0 && selectedIndex < toggleBtns.Length)
		{
			tk2dUIToggleButton = toggleBtns[selectedIndex];
			tk2dUIToggleButton.IsOn = true;
		}
		else
		{
			tk2dUIToggleButton = null;
			selectedIndex = -1;
			ButtonToggle(tk2dUIToggleButton);
		}
	}

	private void ButtonToggle(tk2dUIToggleButton toggleButton)
	{
		if (!(toggleButton == null) && !toggleButton.IsOn)
		{
			return;
		}
		tk2dUIToggleButton[] array = toggleBtns;
		foreach (tk2dUIToggleButton tk2dUIToggleButton in array)
		{
			if (tk2dUIToggleButton != toggleButton)
			{
				tk2dUIToggleButton.IsOn = false;
			}
		}
		if (toggleButton != selectedToggleButton)
		{
			selectedToggleButton = toggleButton;
			SetSelectedIndexFromSelectedToggleButton();
			if (this.OnChange != null)
			{
				this.OnChange(this);
			}
			if (sendMessageTarget != null && SendMessageOnChangeMethodName.Length > 0)
			{
				sendMessageTarget.SendMessage(SendMessageOnChangeMethodName, this, SendMessageOptions.RequireReceiver);
			}
		}
	}

	private void SetSelectedIndexFromSelectedToggleButton()
	{
		selectedIndex = -1;
		int num = 0;
		while (true)
		{
			if (num < toggleBtns.Length)
			{
				tk2dUIToggleButton x = toggleBtns[num];
				if (x == selectedToggleButton)
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		selectedIndex = num;
	}
}
