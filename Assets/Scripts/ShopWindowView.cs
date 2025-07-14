using SmartLocalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopWindowView : MonoBehaviour, IShopWindowView
{
	[SerializeField]
	private GameObject _mainPanel;

	[SerializeField]
	private GameObject _getCoinsContent;

	[SerializeField]
	private GameObject _getFuelContent;

	[SerializeField]
	private Button _showGetCoinsBtn;

	[SerializeField]
	private Button _showGetFuelBtn;

	[SerializeField]
	private Button _closeBtn;

	[SerializeField]
	private Text _coinsBenefit3;

	[SerializeField]
	private Text _coinsBenefit4;

	[SerializeField]
	private Text _coinsBenefit5;

	[SerializeField]
	private List<Text> _coinsValues = new List<Text>();

	[SerializeField]
	private List<Button> _buyCoinsBtns = new List<Button>();

	[SerializeField]
	private List<Button> _buyFuelBtns = new List<Button>();

	[SerializeField]
	private Button _fuelTimerBtn;

	[SerializeField]
	private Button _closeVideoWindowBtn;

	[SerializeField]
	private List<Text> _fuelValues = new List<Text>();

	[SerializeField]
	private List<Text> _fuelPrices = new List<Text>();

	[SerializeField]
	private Text _unlimFuelText;

	[SerializeField]
	private Transform _fuelArraow;

	[SerializeField]
	private Image _fuelIndicator;

	[SerializeField]
	private Image _fuelGlow;

	[SerializeField]
	private Image _fuelInfinityIcon;

	[SerializeField]
	private GameObject _fuelIndicatorIcon;

	[SerializeField]
	private GameObject _fuelJerrycanIcon;

	[SerializeField]
	private Text _fuelText;

	[SerializeField]
	private Text _coinsText;

	[SerializeField]
	private GameObject _videoWindow;

	[SerializeField]
	private List<TweenRotation> _fuelWheelsList = new List<TweenRotation>();

	[SerializeField]
	private List<TweenRotation> _currencyWheelsList = new List<TweenRotation>();

	private bool _restoreFuel;

	private float _fuelArrowPos = 50f;

	private IShopWindowController controller;

	public Text coinFree;

	private float _duration = 0.4f;

	private float _alpha;

	private float _counter;

	public void Init(IShopWindowController controller)
	{
		this.controller = controller;
		AttachButtonEvents();
	}

	public void ShowCoins()
	{
		StartCoroutine(SetCurrencyWheels());
		_restoreFuel = false;
		_mainPanel.SetActive(value: true);
		_getCoinsContent.SetActive(value: true);
		_getFuelContent.SetActive(value: false);
		RefreshCoinsPrices();
	}

	public void ShowFuel(bool isFull)
	{
		StartCoroutine(SetFuelWheels());
		_mainPanel.SetActive(value: true);
		_getFuelContent.SetActive(value: true);
		_getCoinsContent.SetActive(value: false);
		SetFirstFuelItem(isFull);
	}

	public void ShowMainPanel(bool isEnergyFull, int currency, bool isCoinsPanel)
	{
		SetMainPanel(direction: true, isEnergyFull, isCoinsPanel);
		StartCoroutine(SetFuelWheels());
		StartCoroutine(SetCurrencyWheels());
	}

	public void HideMainPanel()
	{
		_restoreFuel = false;
		SetMainPanel(direction: false, isEnergyFull: false, isCoinsPanel: false);
	}

	public void SetFirstFuelItem(bool isFull)
	{
		if (isFull)
		{
			_restoreFuel = false;
			SetFuelText(GameEnergyLogic.GetEnergy);
			_fuelTimerBtn.gameObject.SetActive(value: false);
			_buyFuelBtns[0].gameObject.SetActive(value: true);
			_fuelIndicatorIcon.SetActive(value: false);
			_fuelJerrycanIcon.SetActive(value: true);
		}
		else
		{
			_buyFuelBtns[0].gameObject.SetActive(value: false);
			_fuelTimerBtn.gameObject.SetActive(value: true);
			_fuelJerrycanIcon.SetActive(value: false);
			_fuelIndicatorIcon.SetActive(value: true);
			_restoreFuel = true;
		}
	}

	public void SetVideoWindow(bool turn)
	{
		_videoWindow.SetActive(turn);
	}

	public void SetInfinityIcon(bool turn)
	{
		_fuelInfinityIcon.gameObject.SetActive(turn);
		_fuelText.gameObject.SetActive(!turn);
		TurnOffBuyFuelButtons();
		SetFirstFuelItem(isFull: true);
		_fuelPrices[4].gameObject.SetActive(value: false);
		_unlimFuelText.gameObject.SetActive(value: true);
		foreach (Button buyFuelBtn in _buyFuelBtns)
		{
			ButtonPressTween component = buyFuelBtn.GetComponent<ButtonPressTween>();
			if (component != null)
			{
				component.enabled = false;
			}
		}
	}

	public void SetFuelPricesAndValues()
	{
		_fuelValues[0].text = $"+{PriceConfig.instance.energy.fuelPack1}";
		_fuelValues[1].text = $"+{PriceConfig.instance.energy.fuelPack2}";
		_fuelValues[2].text = $"+{PriceConfig.instance.energy.fuelPack3}";
		_fuelValues[3].text = $"+{PriceConfig.instance.energy.fuelPack4}";
		_fuelPrices[0].text = $"{PriceConfig.instance.energy.fuelPack1Price}";
		_fuelPrices[1].text = $"{PriceConfig.instance.energy.fuelPack2Price}";
		_fuelPrices[2].text = $"{PriceConfig.instance.energy.fuelPack3Price}";
		_fuelPrices[3].text = $"{PriceConfig.instance.energy.fuelPack4Price}";
		string price = InAppManager.instance.GetPrice(InAppSettings.Purchases.UnlimitedFuel);
		price = ((!string.IsNullOrEmpty(price)) ? price : PriceConfig.instance.energy.fuelPack5DefaultPrice);
		_fuelPrices[4].text = price;
	}

	public void SetFuelText(int count)
	{
		if ((bool)_fuelText)
		{
			_fuelText.text = count.ToString();
		}
	}

	public void SetCurrencyText(int count)
	{
		_coinsText.text = count.ToString();
	}

	private void Update()
	{
		SetFuelText(GameEnergyLogic.GetEnergy);
		SetCurrencyText(Progress.shop.currency);
		UpdateFuelTimer();
		UpdateFuelIndicators();
		ruby();
		fuel();
	}

	private void ruby()
	{
		if (Progress.shop.timerForRuby != null)
		{
			TimeSpan timeSpan = DateTime.Now - DateTime.Parse(Progress.shop.timerForRuby);
			double num = (double)PriceConfig.instance.currency.timeForRuby - timeSpan.TotalSeconds;
			int num2 = (int)(num % 3600.0) / 60;
			int num3 = (int)(num % 60.0);
			string text = string.Format("{0}:{1}", (num2 >= 10) ? num2.ToString() : ("0" + num2.ToString()), (num3 >= 10) ? num3.ToString() : ("0" + num3.ToString()));
			if (timeSpan.TotalSeconds > (double)PriceConfig.instance.currency.timeForRuby)
			{
				coinFree.text = LanguageManager.Instance.GetTextValue("VIDEO");
				_buyCoinsBtns[0].interactable = true;
			}
			else
			{
				_buyCoinsBtns[0].interactable = false;
				coinFree.text = text;
			}
		}
		else
		{
			Progress.shop.timerForRuby = DateTime.MinValue.ToString();
		}
	}

	private void fuel()
	{
		if (Progress.shop.timerForFuel != null)
		{
			TimeSpan timeSpan = DateTime.Now - DateTime.Parse(Progress.shop.timerForFuel);
			double num = (double)PriceConfig.instance.currency.timeForFuel - timeSpan.TotalSeconds;
			int num2 = (int)(num % 3600.0) / 60;
			int num3 = (int)(num % 60.0);
			string text = string.Format("{0}:{1}", (num2 >= 10) ? num2.ToString() : ("0" + num2.ToString()), (num3 >= 10) ? num3.ToString() : ("0" + num3.ToString()));
			if (timeSpan.TotalSeconds > (double)PriceConfig.instance.currency.timeForFuel)
			{
				_fuelPrices[1].text = LanguageManager.Instance.GetTextValue("VIDEO");
				_buyFuelBtns[1].interactable = true;
			}
			else
			{
				_buyFuelBtns[1].interactable = false;
				_fuelPrices[1].text = text;
			}
		}
		else
		{
			Progress.shop.timerForFuel = DateTime.MinValue.ToString();
		}
	}

	private void SetMainPanel(bool direction, bool isEnergyFull, bool isCoinsPanel = true)
	{
		if (!direction)
		{
			_mainPanel.SetActive(value: false);
			return;
		}
		if (isCoinsPanel)
		{
			ShowCoins();
			return;
		}
		ShowFuel(isEnergyFull);
		SetFuelPricesAndValues();
	}

	private void RefreshCoinsPrices()
	{
		_coinsValues[0].text = PriceConfig.instance.currency.coinsPack1.ToString();
		_coinsValues[1].text = PriceConfig.instance.currency.coinsPack2.ToString();
		_coinsValues[2].text = PriceConfig.instance.currency.coinsPack3.ToString();
		_coinsValues[3].text = PriceConfig.instance.currency.coinsPack4.ToString();
		_coinsValues[4].text = PriceConfig.instance.currency.coinsPack5.ToString();
		_coinsBenefit3.text = PriceConfig.instance.currency.coinsPack3Benefit;
		_coinsBenefit4.text = PriceConfig.instance.currency.coinsPack4Benefit;
		_coinsBenefit5.text = PriceConfig.instance.currency.coinsPack5Benefit;
		string price = InAppManager.instance.GetPrice(InAppSettings.Purchases.Rubies1);
		string price2 = InAppManager.instance.GetPrice(InAppSettings.Purchases.Rubies2);
		string price3 = InAppManager.instance.GetPrice(InAppSettings.Purchases.Rubies3);
		string price4 = InAppManager.instance.GetPrice(InAppSettings.Purchases.Rubies4);
		price = ((!string.IsNullOrEmpty(price)) ? price : PriceConfig.instance.currency.coinsPack2DefaultPrice.ToString());
		price2 = ((!string.IsNullOrEmpty(price2)) ? price2 : PriceConfig.instance.currency.coinsPack3DefaultPrice.ToString());
		price3 = ((!string.IsNullOrEmpty(price3)) ? price3 : PriceConfig.instance.currency.coinsPack4DefaultPrice.ToString());
		price4 = ((!string.IsNullOrEmpty(price4)) ? price4 : PriceConfig.instance.currency.coinsPack5DefaultPrice.ToString());
		_buyCoinsBtns[1].GetComponentInChildren<Text>().text = price;
		_buyCoinsBtns[2].GetComponentInChildren<Text>().text = price2;
		_buyCoinsBtns[3].GetComponentInChildren<Text>().text = price3;
		_buyCoinsBtns[4].GetComponentInChildren<Text>().text = price4;
	}

	private void AttachButtonEvents()
	{
		Button.ButtonClickedEvent onClick = _showGetCoinsBtn.onClick;
		IShopWindowController shopWindowController = controller;
		onClick.AddListener(shopWindowController.OnCoinsButtonClick);
		Button.ButtonClickedEvent onClick2 = _showGetFuelBtn.onClick;
		IShopWindowController shopWindowController2 = controller;
		onClick2.AddListener(shopWindowController2.OnFuelButtonClick);
		Button.ButtonClickedEvent onClick3 = _closeBtn.onClick;
		IShopWindowController shopWindowController3 = controller;
		onClick3.AddListener(shopWindowController3.OnCloseButtonClick);
		_buyCoinsBtns[0].onClick.AddListener(delegate
		{
			controller.OnBuyCoinsClick(_buyCoinsBtns[0].name);
		});
		_buyCoinsBtns[1].onClick.AddListener(delegate
		{
			controller.OnBuyCoinsClick(_buyCoinsBtns[1].name);
		});
		_buyCoinsBtns[2].onClick.AddListener(delegate
		{
			controller.OnBuyCoinsClick(_buyCoinsBtns[2].name);
		});
		_buyCoinsBtns[3].onClick.AddListener(delegate
		{
			controller.OnBuyCoinsClick(_buyCoinsBtns[3].name);
		});
		_buyCoinsBtns[4].onClick.AddListener(delegate
		{
			controller.OnBuyCoinsClick(_buyCoinsBtns[4].name);
		});
		_buyFuelBtns[0].onClick.AddListener(delegate
		{
			controller.OnBuyFuelClick(_buyFuelBtns[0].name);
		});
		_buyFuelBtns[1].onClick.AddListener(delegate
		{
			controller.OnBuyFuelClick(_buyFuelBtns[1].name);
		});
		_buyFuelBtns[2].onClick.AddListener(delegate
		{
			controller.OnBuyFuelClick(_buyFuelBtns[2].name);
		});
		_buyFuelBtns[3].onClick.AddListener(delegate
		{
			controller.OnBuyFuelClick(_buyFuelBtns[3].name);
		});
		_buyFuelBtns[4].onClick.AddListener(delegate
		{
			controller.OnBuyFuelClick(_buyFuelBtns[4].name);
		});
		Button.ButtonClickedEvent onClick4 = _closeVideoWindowBtn.onClick;
		IShopWindowController shopWindowController4 = controller;
		onClick4.AddListener(shopWindowController4.OnCloseVideoWindowButtonClick);
	}

	private void TurnOffBuyFuelButtons()
	{
		for (int i = 0; i < _buyFuelBtns.Count; i++)
		{
			_buyFuelBtns[i].interactable = false;
		}
	}

	private void UpdateFuelTimer()
	{
		if (_restoreFuel)
		{
			_fuelTimerBtn.GetComponentInChildren<Text>().text = GameEnergyLogic.NextFuelTimer;
		}
	}

	private void UpdateFuelIndicators()
	{
		if (_restoreFuel)
		{
			if (GameEnergyLogic.isEnergyFull)
			{
				_restoreFuel = false;
				SetFirstFuelItem(isFull: true);
				return;
			}
			float num = (float)GameEnergyLogic.GetTotalTime;
			float getRestoreTime = GameEnergyLogic.GetRestoreTime;
			_fuelIndicator.fillAmount = 1f * num / getRestoreTime;
			_fuelArraow.localRotation = Quaternion.Euler(0f, 0f, _fuelArrowPos - num / getRestoreTime * 100f);
			_counter += 0.02f;
			_alpha = Mathf.PingPong(_counter, _duration) / _duration;
			Image fuelGlow = _fuelGlow;
			Color color = _fuelGlow.color;
			float r = color.r;
			Color color2 = _fuelGlow.color;
			float g = color2.g;
			Color color3 = _fuelGlow.color;
			fuelGlow.color = new Color(r, g, color3.b, _alpha);
			SetFuelText(GameEnergyLogic.GetEnergy);
		}
	}

	private IEnumerator SetFuelWheels()
	{
		Audio.PlayAsync("gui_shop_cogweels_sn2");
		for (int i = 0; i < _fuelWheelsList.Count; i++)
		{
			_fuelWheelsList[i].enabled = true;
			_fuelWheelsList[i].Play(forward: true);
		}
		yield return Utilities.WaitForRealSeconds(0.6f);
		for (int j = 0; j < _fuelWheelsList.Count; j++)
		{
			_fuelWheelsList[j].enabled = false;
		}
	}

	private IEnumerator SetCurrencyWheels()
	{
		Audio.PlayAsync("gui_shop_cogweels_sn2");
		for (int i = 0; i < _currencyWheelsList.Count; i++)
		{
			_currencyWheelsList[i].enabled = true;
			_currencyWheelsList[i].Play(forward: true);
		}
		yield return Utilities.WaitForRealSeconds(0.6f);
		for (int j = 0; j < _currencyWheelsList.Count; j++)
		{
			_currencyWheelsList[j].enabled = false;
		}
	}
}
