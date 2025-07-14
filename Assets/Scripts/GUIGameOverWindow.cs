using AnimationOrTween;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GUIGameOverWindow : MonoBehaviour
{
	private Action onLevelsAction;

	private Action onRestartAction;

	private Action OnAchivementsAction;

	private Action onShopAction;

	[SerializeField]
	private Animation anim;

	[SerializeField]
	private ShopEffect shbtn;

	[SerializeField]
	private FortuneEffect ftbtn;

	[SerializeField]
	private Text level;

	[SerializeField]
	private Text collected;

	[SerializeField]
	private Text killed;

	[SerializeField]
	private Text total;

	public GameObject GUITransparent;

	public Text FuelLabel;

	public Text CurrencyLabel;

	public GameObject FuelInfinityIcon;

	public GameObject FuelAndCoinsPanel;

	public Text FuelLabelEachStartRestartBtn;

	private bool _isInfEnergy;

	private bool _isRestartClicked;

	private string levelTranslate;

	public void OnLevels()
	{
		if (!_isRestartClicked && onLevelsAction != null)
		{
			onLevelsAction();
		}
	}

	public void OnRestart()
	{
		if (GameEnergyLogic.isEnoughForRace)
		{
			StartCoroutine(StartFuelAnim());
		}
		else if (onRestartAction != null)
		{
			onRestartAction();
		}
	}

	private IEnumerator StartFuelAnim()
	{
		_isRestartClicked = true;
		FuelLabel.text = (Progress.gameEnergy.energy - PriceConfig.instance.energy.eachStart).ToString();
		Audio.Play("fuel-1");
		yield return Utilities.WaitForRealSeconds(1.5f);
		_isRestartClicked = false;
		if (onRestartAction != null)
		{
			onRestartAction();
		}
	}

	public void OnAchivements()
	{
		if (!_isRestartClicked && OnAchivementsAction != null)
		{
			OnAchivementsAction();
		}
	}

	public void OnShop()
	{
		if (!_isRestartClicked && onShopAction != null)
		{
			onShopAction();
		}
	}

	public void Init(Action onRestartAction, Action onLevelsAction, Action OnAchivements, Action onShopAction)
	{
		this.onRestartAction = onRestartAction;
		this.onLevelsAction = onLevelsAction;
		OnAchivementsAction = OnAchivements;
		this.onShopAction = onShopAction;
	}

	private void Update()
	{
		if (!_isRestartClicked)
		{
			CurrencyLabel.text = Progress.shop.currency.ToString();
			if (!Progress.gameEnergy.isInfinite)
			{
				FuelLabel.text = Progress.gameEnergy.energy.ToString();
			}
			else if (Progress.gameEnergy.isInfinite != _isInfEnergy)
			{
				_isInfEnergy = Progress.gameEnergy.isInfinite;
				FuelLabel.gameObject.SetActive(value: false);
				FuelInfinityIcon.SetActive(value: true);
			}
		}
	}

	public void Show(int levelnum, int collect, int kill)
	{
		FuelLabelEachStartRestartBtn.text = $"-{PriceConfig.instance.energy.eachStart}";
		base.gameObject.SetActive(value: true);
		GUITransparent.gameObject.SetActive(value: true);
		ActiveAnimation activeAnimation = ActiveAnimation.Play(anim, Direction.Forward);
		EventDelegate.Callback call = delegate
		{
			GameBase.TogglePause(pause: true);
			StartCoroutine(CountTotal(collect, kill));
			FuelAndCoinsPanel.SetActive(value: true);
		};
		activeAnimation.onFinished.Add(new EventDelegate(call));
		StartCoroutine(ChangeLevellabel(levelnum));
		Text text = collected;
		string empty = string.Empty;
		total.text = empty;
		empty = empty;
		killed.text = empty;
		text.text = empty;
		Audio.Play("gui_lose_screen");
		StartCoroutine(PlayWheelSound());
	}

	public IEnumerator ChangeLevellabel(int num)
	{
		yield return null;
		if (levelTranslate == null)
		{
			levelTranslate = level.text;
		}
		level.text = levelTranslate.Replace("*", num.ToString());
	}

	private IEnumerator PlayWheelSound()
	{
		float dt = 0f;
		while (dt < 1f)
		{
			dt += Time.unscaledDeltaTime;
			yield return null;
		}
		Audio.Play("gui_lose_sound2");
	}

	public void Hide()
	{
		GUITransparent.gameObject.SetActive(value: false);
		base.gameObject.SetActive(value: false);
	}

	public void SetShopButton()
	{
		shbtn.SetButton();
	}

	public void SetFortuneButton()
	{
		ftbtn.SetButton();
	}

	private IEnumerator CountTotal(int collect, int kill)
	{
		yield return StartCoroutine(RubiesLerp(collected, collect, 1f));
		yield return StartCoroutine(RubiesLerp(killed, kill, 1f));
		yield return StartCoroutine(RubiesLerp(total, collect + kill, 1f));
	}

	private IEnumerator RubiesLerp(Text l, float number, float time)
	{
		if (number > 0f)
		{
			Audio.PlayAsync("gui_scoring");
		}
		float buf = 0f;
		float t = 0f;
		while (t < 1f)
		{
			t += Time.unscaledDeltaTime / time;
			buf = Mathf.Lerp(buf, number, t);
			l.text = ((int)buf).ToString();
			yield return null;
		}
	}
}
