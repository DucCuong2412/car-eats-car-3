using AnimationOrTween;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GUIBoosters : MonoBehaviour
{
	public Animation ShowAnim;

	public Text HealthRestoreLabel;

	public Image HealthRestoreRubieSprite;

	public GameObject tintRed;

	public GameObject tintGreen;

	public GameObject ReviveGameObject;

	public Animation redAnim;

	public Button _BTN;

	public Text countReviwe;

	private Action onTurboAction;

	private Action onHealthAction;

	private Action onHealthRestoreAction;

	public CounterController Timer_for_revive;

	private string tTime = string.Empty;

	private string tTimeNull = "0";

	private string tTimeNNN = "00.00";

	private float TimeToDethNum = 0f;

	private static string str_gui_boosters_activation = "gui_boosters_activation";

	private static string str_revive = "revive";

	private IEnumerator timerBoosts;

	private IEnumerator timerRestore;

	private IEnumerator timerRestoreFORME;

	[HideInInspector]
	public bool startRace;

	private float t;

	private bool tutorialHealthTapped;

	private bool tutorialTurboTapped;

	private bool tutorialReviveTapped;

	private IEnumerator scaleCoroutine;

	private IEnumerator FixThisFackingButton()
	{
		for (int i = 0; i < 4; i++)
		{
			yield return 0;
		}
	}

	public void ShowPreRaceBoosts(float time, int rubinsCount, int turboBonus, int turboPrice, bool isTurboShow, Action onTurboAction, int healthBonus, int healthPrice, bool isHealthShow, Action onHealthAction, Action onHideCallback = null)
	{
		base.gameObject.SetActive(value: true);
		if (isTurboShow || isHealthShow)
		{
			if (Progress.levels.active_level != 1 || Progress.levels.active_pack != 1)
			{
				StartCoroutine(FixThisFackingButton());
				tintGreen.SetActive(value: false);
				tintRed.SetActive(value: false);
			}
			this.onTurboAction = onTurboAction;
			this.onHealthAction = onHealthAction;
			if (timerRestore != null)
			{
				StopCoroutine(timerRestore);
				timerRestore = null;
			}
			if (timerBoosts != null)
			{
				StopCoroutine(timerBoosts);
			}
		}
	}

	public void RefreshPreRaceBoosts(int funds, int turboBonus, bool isTurboShow, int healthBonus, bool isHealthShow, bool isTurboPurchased, bool isHealthPurchased)
	{
	}

	private IEnumerator animFor()
	{
		while (true)
		{
			ActiveAnimation a = ActiveAnimation.Play(redAnim, "flash", Direction.Forward);
			if (a != null)
			{
				while (a.isPlaying)
				{
					yield return 0;
				}
				continue;
			}
			break;
		}
	}

	public void ShowRestoreBoost(int rubinsCount, int restoreBonus, int price, float time, Action onClick, Action onHideCallback = null)
	{
        //base.gameObject.SetActive(value: true);
        //tintRed.SetActive(value: true);
        //tintGreen.SetActive(value: false);
        //StartCoroutine(animFor());
        //onHealthRestoreAction = onClick;
        //SetBooster(restoreBonus, activate: true, HealthRestoreLabel, purchased: false, price, HealthRestoreRubieSprite);
        //timerRestore = TimerForRevive(time, delegate
        //{
        //	onHideCallback();
        //	timerRestore = null;
        //});
        //StartCoroutine(timerRestore);
        //timerRestoreFORME = TimeToDeathCorut();
        //StartCoroutine(timerRestoreFORME);
        RaceLogic.instance.GameOver(); // nhảy thẳng sang UI chết
    }

	private IEnumerator TimeToDeathCorut()
	{
		Timer_for_revive.gameObject.SetActive(value: true);
		float t = TimeToDethNum;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			if (t < 0.3f)
			{
				_BTN.interactable = false;
			}
			SetCageTime(TimeToDethNum, t);
			yield return null;
		}
		RaceLogic.instance.GameOver();
		RaceLogic.instance.gui.CageBut.gameObject.SetActive(value: false);
	}

	private void SetCageTime(float max, float time)
	{
		tTime = time.ToString();
		if ((int)time < 10)
		{
			tTime = tTimeNull + tTime;
		}
		if (time <= 0f)
		{
			tTime = tTimeNNN;
		}
		Timer_for_revive.count = tTime;
	}

	public void RefreshRestoreBoost(int funds, int restoreBonus, bool isHealthRestore, bool isHealthRestorePurchased)
	{
		SetBooster(restoreBonus, isHealthRestore, HealthRestoreLabel, isHealthRestorePurchased, 0, HealthRestoreRubieSprite);
		ReviveGameObject.SetActive(value: false);
		tintRed.SetActive(value: false);
		tintGreen.SetActive(value: true);
		Timer_for_revive.gameObject.SetActive(value: false);
	}

	public void HideRestoreBoost()
	{
		ReviveGameObject.SetActive(value: false);
		tintRed.SetActive(value: false);
		tintGreen.SetActive(value: false);
		if (timerRestore != null)
		{
			StopCoroutine(timerRestore);
		}
	}

	public void onTurboClick()
	{
		if (onTurboAction != null)
		{
			onTurboAction();
		}
		tutorialTurboTapped = true;
		Audio.PlayAsync(str_gui_boosters_activation);
	}

	public void onHealthClick()
	{
		if (onHealthAction != null)
		{
			onHealthAction();
		}
		tutorialHealthTapped = true;
		Audio.PlayAsync(str_gui_boosters_activation);
	}

	public void onHealthRestoreClick()
	{
		if (onHealthRestoreAction != null)
		{
			onHealthRestoreAction();
			if (timerRestoreFORME != null)
			{
				StopCoroutine(timerRestoreFORME);
				timerRestoreFORME = null;
			}
		}
		tutorialReviveTapped = true;
		t = 0f;
		Audio.PlayAsync(str_revive);
	}

	private void SetBooster(int count, bool activate, Text label, bool purchased = false, int price = 0, Image rubin = null)
	{
		label.transform.parent.parent.parent.gameObject.SetActive(value: true);
		if (activate && count > 0 && !purchased)
		{
			label.transform.parent.parent.parent.GetComponent<Button>().interactable = true;
		}
		else if (activate && !purchased)
		{
			label.transform.parent.parent.parent.GetComponent<Button>().interactable = true;
		}
		else if (!purchased)
		{
			label.transform.parent.parent.parent.GetComponent<Button>().interactable = true;
		}
		else
		{
			label.transform.parent.parent.parent.GetComponent<Button>().interactable = false;
		}
	}

	private IEnumerator TimerForRevive(float time, Action callback = null)
	{
		ActiveAnimation anim = ActiveAnimation.Play(ShowAnim, Direction.Forward);
		if (timerBoosts != null && PlayerPrefs.GetInt("tutorial_boosters") == 1)
		{
			PlayerPrefs.SetInt("tutorial_boosters", 2);
			while (anim.isPlaying)
			{
				yield return null;
			}
			while (!tutorialHealthTapped || !tutorialTurboTapped)
			{
				yield return null;
			}
			PlayerPrefs.SetInt("tutorial_boosters", 2);
			ActiveAnimation.Play(ShowAnim, Direction.Reverse);
			callback?.Invoke();
			yield break;
		}
		if (timerRestore != null && PlayerPrefs.GetInt("tutorial_boosters") == 2 && PlayerPrefs.GetInt("tutorial_revive") == 1)
		{
			PlayerPrefs.SetInt("tutorial_revive", 2);
			while (!tutorialReviveTapped)
			{
				yield return null;
			}
			ActiveAnimation.Play(ShowAnim, Direction.Reverse);
			callback?.Invoke();
			t = 0f;
			yield break;
		}
		t = time;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return null;
		}
		float j = 2f;
		while (j > 0f)
		{
			j -= Time.deltaTime;
			yield return 0;
		}
		ActiveAnimation.Play(ShowAnim, Direction.Reverse);
		callback?.Invoke();
		for (int i = 0; i < 10; i++)
		{
			yield return 0;
		}
		base.gameObject.SetActive(value: false);
	}

	public void forGoButton()
	{
		startRace = true;
	}

	private void Update()
	{
		countReviwe.text = Progress.shop.restoreBoost.ToString();
	}

	private IEnumerator ScaleTutorial(GameObject go)
	{
		float scale = 1f;
		Vector3 localScale = go.transform.localScale;
		float minScale = localScale.x;
		float maxScale = minScale + 0.05f;
		bool up = true;
		while (true)
		{
			if ((!(scale < maxScale) || !up) && (!(scale > minScale) || up))
			{
				up = !up;
				continue;
			}
			yield return null;
			scale += 0.0025f * (float)(up ? 1 : (-1));
			go.transform.localScale = Vector3.one;
		}
	}

	private void ResetScale(GameObject go)
	{
		go.transform.localScale = Vector3.one;
	}
}
