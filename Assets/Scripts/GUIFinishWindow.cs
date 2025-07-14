using AnimationOrTween;
using SmartLocalization;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GUIFinishWindow : MonoBehaviour
{
	private Action onNextLevelAction;

	private Action onLevelstAction;

	private Action OnAchivementsAction;

	private Action onShopAction;

	private Action onRestartAction;

	public GameObject btn_next;

	private bool nextLevelThroughGallery;

	[SerializeField]
	private ShopEffect shbtn;

	[SerializeField]
	private FortuneEffect ftbtn;

	[SerializeField]
	private Animation anim;

	[SerializeField]
	private GameObject cogs;

	[SerializeField]
	private Text level;

	[SerializeField]
	private Text collected;

	[SerializeField]
	private Text killed;

	[SerializeField]
	private GameObject bonusC;

	[SerializeField]
	private Text bonus;

	[SerializeField]
	private Text total;

	public Text FuelLabel;

	public Text CurrencyLabel;

	public GameObject FuelInfinityIcon;

	public GameObject FuelLabelAnim;

	public GameObject GUITransparent;

	public GameObject FuelAndCoinsPanel;

	public Text FuelLabelEachStartNextLevelBtn;

	public Text FuleLabelEachStartTop;

	private bool _isInfEnergy;

	private bool _isNextClicked;

	private bool _isRestartClicked;

	private string levelTranslate;

	public void OnNextLevel()
	{
		SetCogs(play: false);
		if (GameEnergyLogic.isEnoughForRace && !nextLevelThroughGallery)
		{
			StartCoroutine(StartFuelAnim());
		}
		else if (onNextLevelAction != null)
		{
			onNextLevelAction();
		}
	}

	public void OnRestart()
	{
		if (GameEnergyLogic.isEnoughForRace)
		{
			StartCoroutine(StartFuelAnim1());
		}
		else if (onRestartAction != null)
		{
			onRestartAction();
		}
	}

	private IEnumerator StartFuelAnim1()
	{
		_isRestartClicked = true;
		if (Progress.shop.endlessLevel)
		{
			FuelLabel.text = (Progress.gameEnergy.energy - PriceConfig.instance.energy.eachStart * 2).ToString();
		}
		else
		{
			FuelLabel.text = (Progress.gameEnergy.energy - PriceConfig.instance.energy.eachStart).ToString();
		}
		Audio.Play("fuel-1");
		yield return Utilities.WaitForRealSeconds(1.5f);
		_isRestartClicked = false;
		if (onRestartAction != null)
		{
			onRestartAction();
		}
	}

	private IEnumerator StartFuelAnim()
	{
		_isNextClicked = true;
		if (Progress.shop.endlessLevel)
		{
			FuelLabel.text = (Progress.gameEnergy.energy - PriceConfig.instance.energy.eachStart * 2).ToString();
		}
		else
		{
			FuelLabel.text = (Progress.gameEnergy.energy - PriceConfig.instance.energy.eachStart).ToString();
		}
		Audio.Play("fuel-1");
		yield return Utilities.WaitForRealSeconds(1.5f);
		_isNextClicked = false;
		if (onNextLevelAction != null)
		{
			onNextLevelAction();
		}
	}

	public void OnLevels()
	{
		if (!_isNextClicked && !_isRestartClicked)
		{
			SetCogs(play: false);
			if (onLevelstAction != null)
			{
				onLevelstAction();
			}
		}
	}

	public void OnAchivements()
	{
		if (!_isNextClicked && !_isRestartClicked)
		{
			SetCogs(play: false);
			if (OnAchivementsAction != null)
			{
				OnAchivementsAction();
			}
		}
	}

	public void OnShop()
	{
		if (!_isNextClicked && !_isRestartClicked)
		{
			SetCogs(play: false);
			if (onShopAction != null)
			{
				UnityEngine.Debug.Log("LEX RaceLogic.instance.car.gameObject.SetActive(false);");
				RaceLogic.instance.car.gameObject.SetActive(value: false);
				onShopAction();
			}
		}
	}

	public void Init(Action onNextLevelAction, Action onLevelsAction, Action OnAchivements, Action onShopAction, Action onRestartAction)
	{
		this.onRestartAction = onRestartAction;
		this.onNextLevelAction = onNextLevelAction;
		onLevelstAction = onLevelsAction;
		OnAchivementsAction = OnAchivements;
		this.onShopAction = onShopAction;
	}

	private void Update()
	{
		if (!_isNextClicked && !_isRestartClicked)
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

	public void Show(int levelnum, int collect, int kill, int bonu, bool _nextLevelThroughGallery = false)
	{
		nextLevelThroughGallery = _nextLevelThroughGallery;
		if (!Progress.shop.endlessLevel)
		{
			FuelLabelEachStartNextLevelBtn.text = $"-{PriceConfig.instance.energy.eachStart}";
		}
		base.gameObject.SetActive(value: true);
		GUITransparent.SetActive(value: true);
		ActiveAnimation activeAnimation = ActiveAnimation.Play(anim, Direction.Forward);
		EventDelegate.Callback call = delegate
		{
			GameBase.TogglePause(pause: true);
			StartCoroutine(CountTotal(collect, kill, bonu));
			FuelAndCoinsPanel.SetActive(value: true);
		};
		activeAnimation.onFinished.Add(new EventDelegate(call));
		StartCoroutine(ChangeLevellabel(levelnum));
		if (!Progress.shop.endlessLevel)
		{
			Text text = collected;
			string empty = string.Empty;
			total.text = empty;
			empty = empty;
			bonus.text = empty;
			empty = empty;
			killed.text = empty;
			text.text = empty;
			bonusC.SetActive(bonu > 0);
		}
		else
		{
			Text text2 = collected;
			string empty = string.Empty;
			total.text = empty;
			empty = empty;
			killed.text = empty;
			text2.text = empty;
		}
		Audio.Stop();
		SetCogs();
		if (Pool.instance.init)
		{
			StartCoroutine(Firework());
		}
	}

	public IEnumerator ChangeLevellabel(int num)
	{
		yield return null;
		if (levelTranslate == null)
		{
			levelTranslate = level.text;
		}
		if (Progress.shop.endlessLevel)
		{
			level.text = LanguageManager.Instance.GetTextValue("ARENA *").Replace("*", Progress.levels.active_pack.ToString());
		}
		else
		{
			level.text = LanguageManager.Instance.GetTextValue("LEVEL *").Replace("*", num.ToString());
		}
	}

	public void Hide()
	{
		GUITransparent.SetActive(value: false);
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

	public void SetCogs(bool play = true)
	{
		if (play)
		{
			TweenRotation[] componentsInChildren = cogs.GetComponentsInChildren<TweenRotation>();
			foreach (TweenRotation tweenRotation in componentsInChildren)
			{
				tweenRotation.enabled = true;
			}
			Audio.Play("gui_shop_cogweels_sn", 0.5f, loop: true);
		}
		else
		{
			TweenRotation[] componentsInChildren2 = cogs.GetComponentsInChildren<TweenRotation>();
			foreach (TweenRotation tweenRotation2 in componentsInChildren2)
			{
				tweenRotation2.enabled = false;
			}
			Audio.Stop("gui_shop_cogweels_sn");
		}
	}

	private IEnumerator Firework()
	{
		Audio.Play("gui_win_salut");
		for (int i = 0; i < 16; i++)
		{
			Pool.Animate(Pool.Explosion.exp30, base.transform.position + new Vector3(UnityEngine.Random.Range(-5.5f, 5.5f), UnityEngine.Random.Range(-5, 5)), "GUI");
			Pool.Animate(Pool.Explosion.exp31, base.transform.position + new Vector3(UnityEngine.Random.Range(-5.5f, 5.5f), UnityEngine.Random.Range(-5, 5)), "GUI");
			yield return StartCoroutine(Utilities.WaitForRealSecondsImpl(0.3f));
		}
	}

	private IEnumerator CountTotal(int collect, int kill, int bonu)
	{
		yield return StartCoroutine(RubiesLerp(collected, collect, 1f));
		yield return StartCoroutine(RubiesLerp(killed, kill, 1f));
		if (!Progress.shop.endlessLevel && bonu > 0)
		{
			yield return StartCoroutine(RubiesLerp(bonus, bonu, 1f));
		}
		if (Progress.fortune.SumPercentRuby != 0f)
		{
			yield return StartCoroutine(RubiesLerp(total, collect + kill + bonu, 1f));
		}
		else
		{
			yield return StartCoroutine(RubiesLerp(number: (float)(collect + kill + bonu) + (float)(collect + kill + bonu) * (0f * Progress.fortune.SumPercentRuby), l: total, time: 1f));
		}
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

	private void OnDestroy()
	{
		SetCogs(play: false);
	}
}
