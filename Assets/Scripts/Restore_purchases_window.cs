using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Restore_purchases_window : MonoBehaviour
{
	public Animator Anim;

	public GameObject textRestored;

	public GameObject textNotFound;

	public Button btnClose;

	private Action onClose;

	private int _isON = Animator.StringToHash("isON");

	public static void Show(bool restored, Action onClose = null)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("GUI/Restore_purchses_window")) as GameObject;
		Restore_purchases_window component = gameObject.GetComponent<Restore_purchases_window>();
		component.textRestored.SetActive(restored);
		component.textNotFound.SetActive(!restored);
		component.onClose = onClose;
		component.btnClose.onClick.AddListener(component.CloseWindow);
		Game.PushInnerState("restore_purch", component.CloseWindow);
	}

	private void CloseWindow()
	{
		Game.PopInnerState("restore_purch", executeClose: false);
		StartCoroutine(DelayClose());
	}

	private IEnumerator DelayClose()
	{
		Anim.SetBool(_isON, value: false);
		float t = 0.5f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return null;
		}
		UnityEngine.SceneManagement.SceneManager.LoadScene("Start_Preloader");
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void OnDisable()
	{
		if (onClose != null)
		{
			onClose();
		}
	}
}
