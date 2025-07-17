//using CompleteProject;
using SmartLocalization;
using Smokoko.DebugModule;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PremiumShopNew : MonoBehaviour
{
	public Text Fuel;

	public Image FuelInf;

	public Text Coin;

	public Animator AnimGloabal;

	public Animator AnimTopPanel;

	public Button _btnFuel;

	public Button _btnCoin;

	public Button _btnExit;

	public GameObject InfoReclama;

	[Header("Coin")]
	public GameObject _obgButtonVideoCoin;

	public GameObject _objNextVideoCoin;

	public Text _textNextVideoCoin;

	[Header("Fuel")]
	public GameObject _objtankFull;

	public GameObject _obgButtonVideoFuel;

	public GameObject _objNextVideoFuel;

	public Text _textNextVideoFuel;

	[Header("Button Fuel")]
	public Button _btnFirstVideoFuel;

	public Button _btnFuel2;

	public Button _btnFuel3;

	public Button _btnByInfFuel;

	[Header("Button Coins")]
	public Button _btnFirstVideoCoin;

	public Button _btnCoin2;

	public Button _btnCoin3;

	public Button _btnCoin4;

	public Button _btnCoin5;

	[Header("Price Fuel")]
	public Text FuelPrice1;

	public Text FuelPrice2;

	public Text FuelPrice3;

	[Header("Price Coin")]
	public Text CoinPrice1;

	public Text CoinPrice2;

	public Text CoinPrice3;

	public Text CoinPrice4;

	[Header("Coins Values")]
	public Text CoinsValues1;

	public Text CoinsValues2;

	public Text CoinsValues3;

	public Text CoinsValues4;

	public Text CoinsValues5;

	[Header("Fuel Values")]
	public Text FuelValues1;

	public Text FuelValues2;

	public Text FuelValues3;

	[Header("top  panel fuel")]
	public GameObject PlusOne;

	public Text timerTop;

	private int _videoRevardLoc1 = 100;

	private int _videoRevardLoc2 = 200;

	private int _videoRevardLoc3 = 300;

	private int _isFuelUnlim = Animator.StringToHash("isFuelUnlim");

	private int _rubiesON = Animator.StringToHash("rubiesON");

	private int _isON = Animator.StringToHash("isON");

	private bool videoForCoin;

	private bool videoForFuel;

	private static string str_plas = "+";

	private Action<Progress.GameEnergy> _energyCallback;

	private Progress.GameEnergy _gameEnergy;

	private void OnEnable()
	{
		StartCoroutine(forStart());
		Audio.Play("gui_screen_on");
		if (Progress.gameEnergy.isInfinite)
		{
			FuelInf.gameObject.SetActive(value: true);
			Fuel.gameObject.SetActive(value: false);
			Fuel.text = Progress.gameEnergy.energy.ToString();
			_btnByInfFuel.gameObject.SetActive(value: false);
			_objNextVideoFuel.SetActive(value: false);
			_obgButtonVideoFuel.SetActive(value: false);
			_objtankFull.SetActive(value: true);
			_btnFuel2.gameObject.SetActive(value: false);
			_btnFuel3.gameObject.SetActive(value: false);
		}
		else
		{
			FuelInf.gameObject.SetActive(value: false);
			Fuel.gameObject.SetActive(value: true);
			Fuel.text = Progress.gameEnergy.energy.ToString();
			Debug.Log(Progress.gameEnergy.energy+"                     ===================");	//xăng xe fuel
            _btnByInfFuel.gameObject.SetActive(value: true);
			_obgButtonVideoFuel.SetActive(value: true);
			_objtankFull.SetActive(value: false);
			_btnFuel2.gameObject.SetActive(value: true);
			_btnFuel3.gameObject.SetActive(value: true);
		}
		Actions();
		Coin.text = Progress.shop.currency.ToString();
		Debug.Log(Progress.shop.currency+"                     ===================");		///coin ruby
		
        _btnCoin.onClick.AddListener(BtnCoinClick);
		_btnFuel.onClick.AddListener(BtnFuelClick);
		_btnExit.onClick.AddListener(BtnExitClick);
		_btnFirstVideoFuel.onClick.AddListener(ShowVideoFuel);
		_btnFirstVideoCoin.onClick.AddListener(ShowVideoCoin);
		_btnByInfFuel.onClick.AddListener(BuyFuelInf);
		_btnFuel2.onClick.AddListener(ByMax);
		_btnFuel3.onClick.AddListener(ByColl);
		_btnCoin2.onClick.AddListener(delegate
		{
			OnBuyCoinsClick(2);
		});
		_btnCoin3.onClick.AddListener(delegate
		{
			OnBuyCoinsClick(3);
		});
		_btnCoin4.onClick.AddListener(delegate
		{
			OnBuyCoinsClick(4);
		});
		_btnCoin5.onClick.AddListener(delegate
		{
			OnBuyCoinsClick(5);
		});
		price();
		Values();
	}

	private void Actions()
	{
	}

	private IEnumerator forStart()
	{
		while (!AnimGloabal.gameObject.activeSelf)
		{
			yield return 0;
		}
		if (Progress.gameEnergy.isInfinite)
		{
			while (!AnimGloabal.isInitialized)
			{
				yield return 0;
			}
			AnimGloabal.SetBool(_isFuelUnlim, value: true);
			AnimGloabal.SetBool(_rubiesON, value: false);
		}
		else
		{
			AnimGloabal.SetBool(_isFuelUnlim, value: false);
		}
	}

	public void openFuel()
	{
		StartCoroutine(FuelOpen());
	}

	private IEnumerator FuelOpen()
	{
		while (!AnimGloabal.isInitialized)
		{
			yield return 0;
		}
		BtnFuelClick();
	}

	private void price()
	{
		//string text = Purchaser.m_StoreController.products.WithID(Purchaser.UnlimitedFuel).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.UnlimitedFuel).metadata.localizedPrice.ToString();
		//text = ((!string.IsNullOrEmpty(text)) ? text : PriceConfig.instance.energy.fuelPack5DefaultPrice);
		//FuelPrice3.text = text;
		//string text2 = Purchaser.m_StoreController.products.WithID(Purchaser.FuelUpTank).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.FuelUpTank).metadata.localizedPrice.ToString();
		//text2 = ((!string.IsNullOrEmpty(text2)) ? text2 : PriceConfig.instance.energy.fuelPackToMaxPrice);
		//FuelPrice1.text = text2;
		//string text3 = Purchaser.m_StoreController.products.WithID(Purchaser.FuelAddMore).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.FuelAddMore).metadata.localizedPrice.ToString();
		//text3 = ((!string.IsNullOrEmpty(text3)) ? text3 : PriceConfig.instance.energy.fuelPack100Price);
		//FuelPrice2.text = text3;
		//string text4 = Purchaser.m_StoreController.products.WithID(Purchaser.Rubies1).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.Rubies1).metadata.localizedPrice.ToString();
		//string text5 = Purchaser.m_StoreController.products.WithID(Purchaser.Rubies2).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.Rubies2).metadata.localizedPrice.ToString();
		//string text6 = Purchaser.m_StoreController.products.WithID(Purchaser.Rubies3).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.Rubies3).metadata.localizedPrice.ToString();
		//string text7 = Purchaser.m_StoreController.products.WithID(Purchaser.Rubies4).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.Rubies4).metadata.localizedPrice.ToString();
		//text4 = ((!string.IsNullOrEmpty(text4)) ? text4 : PriceConfig.instance.currency.coinsPack2DefaultPrice.ToString());
		//text5 = ((!string.IsNullOrEmpty(text5)) ? text5 : PriceConfig.instance.currency.coinsPack3DefaultPrice.ToString());
		//text6 = ((!string.IsNullOrEmpty(text6)) ? text6 : PriceConfig.instance.currency.coinsPack4DefaultPrice.ToString());
		//text7 = ((!string.IsNullOrEmpty(text7)) ? text7 : PriceConfig.instance.currency.coinsPack5DefaultPrice.ToString());
		//CoinPrice1.text = text4;
		//CoinPrice2.text = text5;
		//CoinPrice3.text = text6;
		//CoinPrice4.text = text7;
	}

	private void Values()
	{
		StartCoroutine(FORVALUES());
	}

	private IEnumerator FORVALUES()
	{
		yield return 0;
		if (Progress.levels.active_pack == 1)
		{
			CoinsValues1.text = "+" + _videoRevardLoc1.ToString();
		}
		else if (Progress.levels.active_pack == 2)
		{
			CoinsValues1.text = "+" + _videoRevardLoc2.ToString();
		}
		else if (Progress.levels.active_pack == 3)
		{
			CoinsValues1.text = "+" + _videoRevardLoc3.ToString();
		}
		CoinsValues2.text = str_plas + PriceConfig.instance.currency.coinsPack2.ToString();
		CoinsValues3.text = str_plas + PriceConfig.instance.currency.coinsPack3.ToString();
		CoinsValues4.text = str_plas + PriceConfig.instance.currency.coinsPack4.ToString();
		CoinsValues5.text = str_plas + PriceConfig.instance.currency.coinsPack5.ToString();
		FuelValues1.text = LanguageManager.Instance.GetTextValue("FREE");
		string temp = LanguageManager.Instance.GetTextValue("LARGE FUEL TANK");
		FuelValues2.text = temp;
		FuelValues3.text = str_plas + PriceConfig.instance.energy.fuelPack3.ToString();
	}

	private void OnDisable()
	{
		_btnCoin.onClick.RemoveAllListeners();
		_btnFuel.onClick.RemoveAllListeners();
		_btnExit.onClick.RemoveAllListeners();
		_btnFirstVideoFuel.onClick.RemoveAllListeners();
		_btnFirstVideoCoin.onClick.RemoveAllListeners();
		_btnByInfFuel.onClick.RemoveAllListeners();
		_btnFuel2.onClick.RemoveAllListeners();
		_btnFuel3.onClick.RemoveAllListeners();
		_btnCoin2.onClick.RemoveAllListeners();
		_btnCoin3.onClick.RemoveAllListeners();
		_btnCoin4.onClick.RemoveAllListeners();
		_btnCoin5.onClick.RemoveAllListeners();
	}

	private void BtnFuelClick()
	{
		AnimGloabal.SetBool(_rubiesON, value: false);
	}

	private void BtnCoinClick()
	{
		AnimGloabal.SetBool(_rubiesON, value: true);
	}

	private void BtnExitClick()
	{
		StartCoroutine(Exit());
	}

	private IEnumerator checkInternetConnection(Action<bool> action)
	{
		WWW www = new WWW("http://google.com");
		yield return www;
		if (www.error != null)
		{
			action(obj: false);
		}
		else
		{
			action(obj: true);
		}
	}

	private void ShowVideoCoin()
	{
		_btnFirstVideoCoin.interactable = false;
		videoForCoin = true;
		videoForFuel = false;
		NewWrapperForAppodeal.instance.ShowVideo(delegate(bool sucess)
		{
			if (sucess)
			{
				SucsesCoin();
			}
			else
			{
				NoSucses();
			}
		}, delegate
		{
			NOvideo();
		}, delegate
		{
			NoSucses();
		});
	}

	private void SucsesCoin()
	{
		if (Progress.levels.active_pack == 1)
		{
			StartCoroutine(RubiesLerp(_videoRevardLoc1, fuel: false));
		}
		else if (Progress.levels.active_pack == 2)
		{
			StartCoroutine(RubiesLerp(_videoRevardLoc2, fuel: false));
		}
		else if (Progress.levels.active_pack == 3)
		{
			StartCoroutine(RubiesLerp(_videoRevardLoc3, fuel: false));
		}
		Progress.shop.timerForRuby = DateTime.Now.ToString();
		AnalyticsManager.LogEvent(EventCategoty.video, "ads_watched", "free coins");
		_btnFirstVideoCoin.interactable = true;
		_btnFirstVideoFuel.interactable = true;
		Progress.shop.CollReclamForRuby++;
	}

	private void NoSucses()
	{
		_btnFirstVideoCoin.interactable = true;
		_btnFirstVideoFuel.interactable = true;
	}

	private void NOvideo()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("noVideoBox", typeof(GameObject))) as GameObject;
		_btnFirstVideoFuel.interactable = true;
		_btnFirstVideoCoin.interactable = true;
	}

	private void ShowVideoFuel()
	{
		videoForCoin = false;
		videoForFuel = true;
		NewWrapperForAppodeal.instance.ShowVideo(delegate(bool sucess)
		{
			if (sucess)
			{
				SucsesFuel();
			}
			else
			{
				NoSucses();
			}
		}, delegate
		{
			NOvideo();
		}, delegate
		{
			NoSucses();
		});
	}

	private void SucsesFuel()
	{
		Progress.shop.timerForFuel = DateTime.Now.ToString();
		StartCoroutine(RubiesLerp(PriceConfig.instance.energy.fuelPack1, fuel: true));
		AnalyticsManager.LogEvent(EventCategoty.video, "ads_watched", "free fuel");
		_btnFirstVideoFuel.interactable = true;
		_btnFirstVideoCoin.interactable = true;
		Progress.shop.CollReclamForFuel++;
	}

	private IEnumerator RubiesLerp(int time, bool fuel)
	{
		int t = 0;
		while (t < time)
		{
			if (fuel)
			{
				t++;
				GameEnergyLogic.AddFuel();
			}
			else
			{
				t += 10;
				Progress.shop.currency += 10;
			}
			yield return null;
		}
	}

	private IEnumerator Exit()
	{
		AnimTopPanel.SetBool(_isON, value: false);
		yield return new WaitForSeconds(0.8f);
		LevelGalleryCanvasView temp = UnityEngine.Object.FindObjectOfType<LevelGalleryCanvasView>();
		if (temp != null)
		{
			temp.FACVOM.enabled = true;
		}
		base.gameObject.SetActive(value: false);
	}

	private void Update()
	{
		InfoReclama.SetActive(!Progress.shop.BuyForRealMoney);
		if (GameEnergyLogic.instance.Energy >= PriceConfig.instance.energy.maxValue)
		{
			timerTop.text = string.Empty;
		}
		if (timerTop.text != string.Empty)
		{
			PlusOne.SetActive(value: true);
			timerTop.text = GameEnergyLogic.instance.FuelTimer;
		}
		else
		{
			PlusOne.SetActive(value: false);
		}
		Fuel.text = Progress.gameEnergy.energy.ToString();
		Coin.text = Progress.shop.currency.ToString();
		ruby();
		fuel();
		if (Progress.gameEnergy.energy >= PriceConfig.instance.energy.maxValue)
		{
			_objNextVideoFuel.SetActive(value: false);
			_obgButtonVideoFuel.SetActive(value: false);
			_objtankFull.SetActive(value: true);
		}
		else
		{
			_objtankFull.SetActive(value: false);
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			BtnExitClick();
		}
	}

	public void OnBuyCoinsClick(int index)
	{
		if (DebugFacade.isDebug)
		{
			ModalWindow.instance.Show("Buy coins pack " + index.ToString(), new ButtonInfo("Buy", delegate
			{
				OnMoneytierBought(index);
			}));
		}
		else
		{
			BuyCoinsForRealMoney(index);
		}
	}

	private void BuyCoinsForRealMoney(int index)
	{
		//switch (index)
		//{
		//case 2:
		//	Purchaser.BuyProductID(Purchaser.Rubies1, null, OnMoneytierBought);
		//	break;
		//case 3:
		//	Purchaser.BuyProductID(Purchaser.Rubies2, null, OnMoneytierBought);
		//	break;
		//case 4:
		//	Purchaser.BuyProductID(Purchaser.Rubies3, null, OnMoneytierBought);
		//	break;
		//case 5:
		//	Purchaser.BuyProductID(Purchaser.Rubies4, null, OnMoneytierBought);
		//	break;
		//}
	}

	private void OnMoneytierBought(int current)
	{
		Audio.PlayAsync("shop_purchase");
		Audio.PlayAsync("gui_scoring");
		Progress.shop.BuyForRealMoney = true;
		switch (current)
		{
		case 2:
			Progress.shop.currency += PriceConfig.instance.currency.coinsPack2;
			AnalyticsManager.LogEvent(EventCategoty.premium_shop, "rubies_pack1", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
			break;
		case 3:
			Progress.shop.currency += PriceConfig.instance.currency.coinsPack3;
			AnalyticsManager.LogEvent(EventCategoty.premium_shop, "rubies_pack2", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
			break;
		case 4:
			Progress.shop.currency += PriceConfig.instance.currency.coinsPack4;
			AnalyticsManager.LogEvent(EventCategoty.premium_shop, "rubies_pack3", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
			break;
		case 5:
			Progress.shop.currency += PriceConfig.instance.currency.coinsPack5;
			AnalyticsManager.LogEvent(EventCategoty.premium_shop, "rubies_pack4", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
			break;
		}
		Fuel.text = Progress.gameEnergy.energy.ToString();
		Coin.text = Progress.shop.currency.ToString();
		Progress.review.atLeastOnePurchase = true;
	}

	private void ruby()
	{
		if (Progress.shop.timerForRuby != null)
		{
			if (Progress.shop.CollReclamForRuby == 3)
			{
				if (Progress.shop.timerForRuby == string.Empty)
				{
					Progress.shop.timerForRuby = DateTime.MinValue.ToString();
				}
				TimeSpan timeSpan = DateTime.Now - DateTime.Parse(Progress.shop.timerForRuby);
				double num = (double)PriceConfig.instance.currency.timeForRuby - timeSpan.TotalSeconds;
				int num2 = (int)(num % 3600.0) / 60;
				int num3 = (int)(num % 60.0);
				string text = string.Format("{0}:{1}", (num2 >= 10) ? num2.ToString() : ("0" + num2.ToString()), (num3 >= 10) ? num3.ToString() : ("0" + num3.ToString()));
				if (timeSpan.TotalSeconds > (double)PriceConfig.instance.currency.timeForRuby)
				{
					_objNextVideoCoin.SetActive(value: false);
					_obgButtonVideoCoin.SetActive(value: true);
					Progress.shop.CollReclamForRuby = 0;
				}
				else
				{
					_objNextVideoCoin.SetActive(value: true);
					_obgButtonVideoCoin.SetActive(value: false);
					_textNextVideoCoin.text = text;
				}
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
			if (Progress.shop.CollReclamForFuel == 3)
			{
				if (Progress.shop.timerForFuel == string.Empty)
				{
					Progress.shop.timerForFuel = DateTime.MinValue.ToString();
				}
				TimeSpan timeSpan = DateTime.Now - DateTime.Parse(Progress.shop.timerForFuel);
				double num = (double)PriceConfig.instance.currency.timeForFuel - timeSpan.TotalSeconds;
				int num2 = (int)(num % 3600.0) / 60;
				int num3 = (int)(num % 60.0);
				string text = string.Format("{0}:{1}", (num2 >= 10) ? num2.ToString() : ("0" + num2.ToString()), (num3 >= 10) ? num3.ToString() : ("0" + num3.ToString()));
				if (timeSpan.TotalSeconds > (double)PriceConfig.instance.currency.timeForFuel)
				{
					_objNextVideoFuel.SetActive(value: false);
					_obgButtonVideoFuel.SetActive(value: true);
					Progress.shop.CollReclamForFuel = 0;
				}
				else
				{
					_objNextVideoFuel.SetActive(value: true);
					_obgButtonVideoFuel.SetActive(value: false);
					_textNextVideoFuel.text = text;
				}
			}
		}
		else
		{
			Progress.shop.timerForFuel = DateTime.MinValue.ToString();
		}
	}

	private void BuyFuelInf()
	{
		if (DebugFacade.isDebug)
		{
			ModalWindow.instance.Show("Debug buy unlimited fuel", new ButtonInfo("Buy", OnFuelBought));
		}
		else
		{
			//Purchaser.BuyProductID(Purchaser.UnlimitedFuel, OnFuelBought);
		}
	}

	private void OnFuelBought()
	{
		Progress.shop.BuyForRealMoney = true;
		AnalyticsManager.LogEvent(EventCategoty.premium_shop, "unlimited_fuel", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
		Audio.PlayAsync("fuel");
		Progress.gameEnergy.isInfinite = true;
		Progress.review.atLeastOnePurchase = true;
		FuelInf.gameObject.SetActive(value: true);
		Fuel.gameObject.SetActive(value: false);
		_btnByInfFuel.gameObject.SetActive(value: false);
		_objNextVideoFuel.SetActive(value: false);
		_obgButtonVideoFuel.SetActive(value: false);
		_objtankFull.SetActive(value: true);
		_btnFuel2.gameObject.SetActive(value: false);
		_btnFuel3.gameObject.SetActive(value: false);
		AnimGloabal.SetBool(_isFuelUnlim, value: true);
	}

	private void ByMax()
	{
		if (DebugFacade.isDebug)
		{
			ModalWindow.instance.Show("Debug buy +" + PriceConfig.instance.energy.fuelPack2 + " to max fuel", new ButtonInfo("Buy", ByFueltoMax));
		}
		else
		{
			//Purchaser.BuyProductID(Purchaser.FuelUpTank, ByFueltoMax);
		}
	}

	private void ByColl()
	{
		if (DebugFacade.isDebug)
		{
			ModalWindow.instance.Show("Debug buy +" + PriceConfig.instance.energy.fuelPack3 + " to coll fuel", new ButtonInfo("Buy", ByCollFuel));
		}
		else
		{
			//Purchaser.BuyProductID(Purchaser.FuelAddMore, ByCollFuel);
		}
	}

	private void ByFueltoMax()
	{
		Progress.shop.BuyForRealMoney = true;
		AnalyticsManager.LogEvent(EventCategoty.premium_shop, "tank_upgrade", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
		Audio.PlayAsync("fuel");
		PriceConfig.instance.energy.maxValue += PriceConfig.instance.energy.fuelPack2;
		Fuel.text = Progress.gameEnergy.energy.ToString();
		Coin.text = Progress.shop.currency.ToString();
	}

	private void ByCollFuel()
	{
		Progress.shop.BuyForRealMoney = true;
		AnalyticsManager.LogEvent(EventCategoty.premium_shop, "fuel_truck", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
		Audio.PlayAsync("fuel");
		GameEnergyLogic.AddFuel(PriceConfig.instance.energy.fuelPack3);
		Fuel.text = Progress.gameEnergy.energy.ToString();
		Coin.text = Progress.shop.currency.ToString();
	}
}
