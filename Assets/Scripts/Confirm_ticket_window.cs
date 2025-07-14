using System;
using UnityEngine;
using UnityEngine.UI;

public class Confirm_ticket_window : MonoBehaviour
{
	public Text descriptionText;

	public Button btn_close;

	public Button btn_yes;

	public Button btn_no;

	[HideInInspector]
	public string giftCosts;

	private bool isYes;

	private Action<bool> action;

	private string originText;

	public static void Show(Action<bool> callback, string giftPrice)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("GUI/Confirm_tickets_window")) as GameObject;
		Confirm_ticket_window component = gameObject.GetComponent<Confirm_ticket_window>();
		component.action = callback;
		component.giftCosts = giftPrice;
		Game.PushInnerState("ticket_window", component.Close);
	}

	private void Start()
	{
		btn_close.onClick.AddListener(Close);
		btn_no.onClick.AddListener(No);
		btn_yes.onClick.AddListener(Yes);
		if (originText == null)
		{
			originText = descriptionText.text;
		}
		descriptionText.text = originText.Replace("*", giftCosts);
	}

	private void OnDisable()
	{
		Game.PopInnerState("ticket_window", executeClose: false);
		if (action != null)
		{
			action(isYes);
		}
	}

	private void Close()
	{
		isYes = false;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void No()
	{
		isYes = false;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void Yes()
	{
		isYes = true;
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
