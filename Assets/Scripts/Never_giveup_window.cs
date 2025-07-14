using System;
using UnityEngine;
using UnityEngine.UI;

public class Never_giveup_window : MonoBehaviour
{
	private Action onCloseAction;

	public Button btn_close;

	public Button btn_getit;

	public static void Init(Action onClose)
	{
		Never_giveup_window component = (UnityEngine.Object.Instantiate(Resources.Load("GUI/Never_giveup_window")) as GameObject).GetComponent<Never_giveup_window>();
		component.onCloseAction = onClose;
		Game.PushInnerState("never_giveup", component.CloseWindow);
	}

	private void OnEnable()
	{
		btn_close.onClick.AddListener(CloseWindow);
		btn_getit.onClick.AddListener(OnGetIt);
	}

	private void OnDisable()
	{
		if (onCloseAction != null)
		{
			onCloseAction();
		}
	}

	private void CloseWindow()
	{
		Game.PopInnerState("never_giveup", executeClose: false);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void OnGetIt()
	{
		Progress.Levels levels = Progress.levels;
		levels.tickets += 3;
		Progress.levels = levels;
		Audio.Play("bns_health_man_01_sn");
		CloseWindow();
	}
}
