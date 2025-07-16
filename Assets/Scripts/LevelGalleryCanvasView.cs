using CompleteProject;
using SmartLocalization;
using Smokoko.Social;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelGalleryCanvasView : LevelGalleryCanvasLogic
{
	public List<GameObject> CarsForLevel = new List<GameObject>();

	public FinalComics FC;

	public FuelAndCoinVideoOnMAp FACVOM;

	[Header("Scroll")]
	public RectTransform scrollView;

	public CounterController textStarts;

	public GameObject LeaveGame;

	public Button YesLeave;

	[Header("Top buttons")]
	public Button btnShop;

	public Button btnMonstropedia;

	public Button btnPolicepedia;

	public Button btnIncubator;

	public Button btnMonstropediaExit;

	public Button btnMonstropediaOut;

	public GameObject MostroWindow;

	public GameObject MonstropediaMarker;

	public GameObject MonstropediaMarkerIn;

	public GameObject PolicepediaMarkerIn;

	public GameObject IncubatorMarkerIn;

	[Header("Top Labels")]
	public CounterController txtRubies;

	//public CounterController txtEnergy;

	public GameObject InfEnergy;

	[Header("Buy Panel")]
	public GameObject buyLevelsWindow;

	public Button btnBuyNext;

	public Button btnBuyPack;

	public Button btnBuyClose;

	public Button btnBuyRealMoney;

	public Text txtBuyNextPrice;

	public Text txtBuyPackPrice;

	public Animator buylevel;

	private bool isAnimating;

	public Camera cameras;

	public Animation animfuel;

	public Text AnimfuelText;

	public GameObject ReclamaWikno;

	[Header("Rate US")]
	public float RateTimeBetwenStars = 0.15f;

	public GameObject RateObj;

	public Text RateCaption;

	public Text RateShauts;

	public Text RateRemindMe;

	public Button RateBtnClose;

	public Button RateBtnRate;

	public Button RateBtnNo;

	public Button RateBtnYes;

	public Button RateBtnStar1;

	public Button RateBtnStar2;

	public Button RateBtnStar3;

	public Button RateBtnStar4;

	public Button RateBtnStar5;

	public Animator RateOpenAnim;

	public Animator RateAfterCloseAnim;

	public Animator RateStar1Anim;

	public Animator RateStar2Anim;

	public Animator RateStar3Anim;

	public Animator RateStar4Anim;

	public Animator RateStar5Anim;

	public List<CellContainer> LGCL;

	private int LastStar = -1;

	private bool canPress = true;

	private int _isON = Animator.StringToHash("isON");

	private int _rateOFF = Animator.StringToHash("rateOFF");

	private string SoundStarClick = "actor_gadget_shield_sn";

	private string strShaut = "RATE US SHAUT ";

	private ControllerMonstropedia CM;

	public List<GameObject> carsInIsland = new List<GameObject>();

	public Animator ForPlaybtnAnim;

	public GameObject FBPriceNew;

	public GameObject DailyBonus;

	public PremiumShopNew _shopWindowModel;

	[CompilerGenerated]
	private static GameCenterWrapper.DelAvailableSaveFound _003C_003Ef__mg_0024cache0;

	private void IncubatorPress()
	{
		SceneManager.LoadScene("scene_incubator");
	}

	private void LeavesGame()
	{
		Application.Quit();
	}

	private void OpenRateUs()
	{
		RateObj.SetActive(value: true);
		RateBtnClose.onClick.AddListener(PressClose);
		RateBtnRate.onClick.AddListener(PressRate);
		RateBtnStar1.onClick.AddListener(PressStar1);
		RateBtnStar2.onClick.AddListener(PressStar2);
		RateBtnStar3.onClick.AddListener(PressStar3);
		RateBtnStar4.onClick.AddListener(PressStar4);
		RateBtnStar5.onClick.AddListener(PressStar5);
		canPress = true;
		LastStar = -1;
	}

	private void PressClose()
	{
		RateAfterCloseAnim.SetTrigger(_rateOFF);
		RateBtnNo.onClick.AddListener(PressNo);
		RateBtnYes.onClick.AddListener(PressYes);
		RateCaption.gameObject.SetActive(value: false);
		RateShauts.gameObject.SetActive(value: false);
		RateRemindMe.gameObject.SetActive(value: true);
	}

	public void ExitForReclamaWindow()
	{
		Progress.shop.ShowRelamaWindow = true;
	}

	private void PressRate()
	{
		if (LastStar == -1)
		{
			LastStar = 1;
		}
		AnalyticsManager.LogEvent(EventCategoty.rate, "rate", LastStar.ToString());
		Application.OpenURL(RateUsLinks.Instance.url_android);
		Progress.levels.RateUsRemind = false;
		StartCoroutine(CorutToClose());
	}

	private void PressYes()
	{
		GameCenter.OnRateGame(1);
		StartCoroutine(CorutToClose());
	}

	private void PressNo()
	{
		Progress.levels.RateUsRemind = false;
		StartCoroutine(CorutToClose());
	}

	private IEnumerator CorutToClose()
	{
		RateOpenAnim.SetBool(_isON, value: false);
		float t = 0.5f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return null;
		}
		RateObj.SetActive(value: false);
	}

	private void PressStar1()
	{
		Audio.Play(SoundStarClick);
		StartCoroutine(PressStar(1));
	}

	private void PressStar2()
	{
		Audio.Play(SoundStarClick);
		StartCoroutine(PressStar(2));
	}

	private void PressStar3()
	{
		Audio.Play(SoundStarClick);
		StartCoroutine(PressStar(3));
	}

	private void PressStar4()
	{
		Audio.Play(SoundStarClick);
		StartCoroutine(PressStar(4));
	}

	private void PressStar5()
	{
		Audio.Play(SoundStarClick);
		StartCoroutine(PressStar(5));
	}

	private IEnumerator PressStar(int index)
	{
		if (!canPress)
		{
			yield break;
		}
		canPress = false;
		float t10 = 0f;
		if (RateCaption.gameObject.activeSelf)
		{
			RateCaption.gameObject.SetActive(value: false);
			RateShauts.gameObject.SetActive(value: true);
		}
		RateShauts.text = LanguageManager.Instance.GetTextValue(strShaut + index.ToString());
		if (index > LastStar)
		{
			if (index >= 1 && LastStar < index)
			{
				while (t10 > 0f)
				{
					t10 -= Time.deltaTime;
					yield return null;
				}
				RateStar1Anim.SetBool(_isON, value: true);
			}
			if (index >= 2 && LastStar < index)
			{
				t10 = RateTimeBetwenStars;
				while (t10 > 0f)
				{
					t10 -= Time.deltaTime;
					yield return null;
				}
				RateStar2Anim.SetBool(_isON, value: true);
			}
			if (index >= 3 && LastStar < index)
			{
				t10 = RateTimeBetwenStars;
				while (t10 > 0f)
				{
					t10 -= Time.deltaTime;
					yield return null;
				}
				RateStar3Anim.SetBool(_isON, value: true);
			}
			if (index >= 4 && LastStar < index)
			{
				t10 = RateTimeBetwenStars;
				while (t10 > 0f)
				{
					t10 -= Time.deltaTime;
					yield return null;
				}
				RateStar4Anim.SetBool(_isON, value: true);
			}
			if (index >= 5 && LastStar < index)
			{
				t10 = RateTimeBetwenStars;
				while (t10 > 0f)
				{
					t10 -= Time.deltaTime;
					yield return null;
				}
				RateStar5Anim.SetBool(_isON, value: true);
			}
		}
		else if (index < LastStar)
		{
			if (index < 5 && LastStar > index)
			{
				t10 = RateTimeBetwenStars;
				while (t10 > 0f)
				{
					t10 -= Time.deltaTime;
					yield return null;
				}
				RateStar5Anim.SetBool(_isON, value: false);
			}
			if (index < 4 && LastStar > index)
			{
				t10 = RateTimeBetwenStars;
				while (t10 > 0f)
				{
					t10 -= Time.deltaTime;
					yield return null;
				}
				RateStar4Anim.SetBool(_isON, value: false);
			}
			if (index < 3 && LastStar > index)
			{
				t10 = RateTimeBetwenStars;
				while (t10 > 0f)
				{
					t10 -= Time.deltaTime;
					yield return null;
				}
				RateStar3Anim.SetBool(_isON, value: false);
			}
			if (index < 2 && LastStar > index)
			{
				t10 = RateTimeBetwenStars;
				while (t10 > 0f)
				{
					t10 -= Time.deltaTime;
					yield return null;
				}
				RateStar2Anim.SetBool(_isON, value: false);
			}
			if (index < 1 && LastStar > index)
			{
				t10 = RateTimeBetwenStars;
				while (t10 > 0f)
				{
					t10 -= Time.deltaTime;
					yield return null;
				}
				RateStar1Anim.SetBool(_isON, value: false);
			}
		}
		canPress = true;
		LastStar = index;
	}

	private void ChekCAr()
	{
		if (Progress.shop.activeCar >= 0)
		{
			if (!Progress.shop.Cars[Progress.shop.activeCar].equipped)
			{
				Progress.shop.activeCar = 0;
			}
		}
		else
		{
			Progress.shop.activeCar = 0;
		}
	}

	protected override void Awake()
	{
		int num = 0;
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j <= 12; j++)
			{
				num += Progress.levels.Pack(i).Level(j).oldticket;
			}
		}
		textStarts.count = num.ToString();
		Progress.levels.SendRetention();
		base.Awake();
		ChekCAr();
        btnBuyClose.onClick.AddListener(ButtonBuyClose);
        btnBuyNext.onClick.AddListener(ButtonBuyPack);
        btnBuyPack.onClick.AddListener(ButtonBuyAll);
        btnBuyRealMoney.onClick.AddListener(ButtonBuyRealMoney);
		YesLeave.onClick.AddListener(LeavesGame);
		btnMonstropedia.onClick.AddListener(btnMonstroIn);
		btnMonstropediaOut.onClick.AddListener(btnMonstro);
		btnPolicepedia.onClick.AddListener(btnPoliceIn);
		btnIncubator.onClick.AddListener(IncubatorPress);
		btnMonstropediaExit.onClick.AddListener(delegate
		{
			Game.OnStateChange(Game.gameState.Levels);
			MostroWindow.SetActive(value: false);
		});
		btnShop.onClick.AddListener(btnShops);
	}

	public void GCAchievements()
	{
		StartCoroutine(GCAch());
	}

	private IEnumerator GCAch()
	{
		yield return new WaitForSeconds(0.5f);
		GameCenter.ShowAchievements();
	}

	public void GCLeaderboards()
	{
		GameCenter.ShowLeaderBoards();
	}

	private void clic_fb()
	{
		SceneManager.LoadScene("fb_price", LoadSceneMode.Additive);
		FBLeaderboard.OnUserLoggedIn = (Action<bool>)Delegate.Combine(FBLeaderboard.OnUserLoggedIn, new Action<bool>(CLOSEBTN));
	}

	private void btnMonstro()
	{
		Game.OnStateChange(Game.gameState.OpenWindow);
		Audio.PlayAsync("gui_screen_on");
		MostroWindow.SetActive(value: true);
	}

	private void btnMonstroIn()
	{
		StartCoroutine(Monstro());
	}

	private void btnPoliceIn()
	{
		Progress.shop.LoadPolicePedia = true;
		base.ButtonPoliceClicked();
	}

	private IEnumerator Monstro()
	{
		base.ButtonMonstroClicked();
		while (CM == null)
		{
			CM = UnityEngine.Object.FindObjectOfType<ControllerMonstropedia>();
			yield return 0;
		}
	}

	public void btnShops()
	{
		StartCoroutine(shop());
	}

	private IEnumerator shop()
	{
		base.ButtonShopClicked();
		yield break;
	}

	private new void Update()
	{
		txtRubies.count = Progress.shop.currency.ToString();
		if (Game.currentState == Game.gameState.OpenWindow && UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			MostroWindow.SetActive(value: false);
			Game.OnStateChange(Game.gameState.Levels);
		}
		SetInfIco();
	}

	private void CLOSEBTN(bool a)
	{
		if (a)
		{
			Progress.shop.fb_price_ned = false;
		}
		FBLeaderboard.OnUserLoggedIn = (Action<bool>)Delegate.Remove(FBLeaderboard.OnUserLoggedIn, new Action<bool>(CLOSEBTN));
	}

	protected IEnumerator WaitAnConnetToGamecenter()
	{
		yield return null;
		if (Progress.shop.StartComixShow)
		{
			Game.InitGameCenter();
			GameCenter.Init();
			if (Progress.shop.foundProgress)
			{
				GameCenterWrapper.OnAvailableSaveFound = (GameCenterWrapper.DelAvailableSaveFound)Delegate.Combine(GameCenterWrapper.OnAvailableSaveFound, new GameCenterWrapper.DelAvailableSaveFound(Game.OnSavesLoaded));
			}
			yield return new WaitForSeconds(1f);
		}
	}

	protected override void OnEnable()
	{
		Input.multiTouchEnabled = false;
		Game.OnStateChange(Game.gameState.Levels);
		if (Progress.settings.LoginToGP)
		{
			StartCoroutine(WaitAnConnetToGamecenter());
		}
		foreach (GameObject item in carsInIsland)
		{
			item.SetActive(!Progress.shop.TutorialFin);
		}
		if (Progress.levels.active_pack == 3 && Progress.levels.Pack(3).Level(12).ticket > 0 && Progress.shop.TutorialFin && Progress.shop.BossDeath3)
		{
			FC.enabled = true;
		}
		if (Progress.levels.Max_Active_Level == 6 && Progress.levels.RateUsRemind)
		{
			if (Progress.levels.active_pack_last_openned == 1 && !Progress.levels.RateUsOpenned1)
			{
				Progress.levels.RateUsOpenned1 = true;
				OpenRateUs();
			}
			if (Progress.levels.active_pack_last_openned == 2 && !Progress.levels.RateUsOpenned2)
			{
				Progress.levels.RateUsOpenned2 = true;
				OpenRateUs();
			}
			if (Progress.levels.active_pack_last_openned == 3 && !Progress.levels.RateUsOpenned3)
			{
				Progress.levels.RateUsOpenned3 = true;
				OpenRateUs();
			}
		}
		StartCoroutine(openBeatfriend());
		base.OnEnable();
		StartCoroutine(for_Daily());
		SetInfIco();
		MonstropediaMarker.SetActive(value: false);
		PolicepediaMarkerIn.SetActive(value: false);
		MonstropediaMarkerIn.SetActive(value: false);
		IncubatorMarkerIn.SetActive(value: false);
		for (int i = 0; i < Progress.shop.MonstroLocks.Count; i++)
		{
			if (!Progress.shop.MonstroLocks[i] && Progress.shop.MonstroCanGetReward[i])
			{
				MonstropediaMarker.SetActive(value: true);
				MonstropediaMarkerIn.SetActive(value: true);
				break;
			}
		}
		if (Progress.shop.CollKill1Car >= ConfigForPolicePedia.instance.Car1.CollPart1 && !Progress.shop.Get1partForPoliceCar)
		{
			PolicepediaMarkerIn.SetActive(value: true);
			MonstropediaMarker.SetActive(value: true);
		}
		else if (Progress.shop.CollKill2Car >= ConfigForPolicePedia.instance.Car1.CollPart2 && !Progress.shop.Get2partForPoliceCar)
		{
			PolicepediaMarkerIn.SetActive(value: true);
			MonstropediaMarker.SetActive(value: true);
		}
		else if (Progress.shop.CollKill3Car >= ConfigForPolicePedia.instance.Car1.CollPart3 && !Progress.shop.Get3partForPoliceCar)
		{
			PolicepediaMarkerIn.SetActive(value: true);
			MonstropediaMarker.SetActive(value: true);
		}
		else if (Progress.shop.CollKill4Car >= ConfigForPolicePedia.instance.Car1.CollPart4 && !Progress.shop.Get4partForPoliceCar)
		{
			PolicepediaMarkerIn.SetActive(value: true);
			MonstropediaMarker.SetActive(value: true);
		}
		else if (Progress.shop.CollKill1Car2 >= ConfigForPolicePedia.instance.Car2.CollPart1 && !Progress.shop.Get1partForPoliceCar2)
		{
			PolicepediaMarkerIn.SetActive(value: true);
			MonstropediaMarker.SetActive(value: true);
		}
		else if (Progress.shop.CollKill2Car2 >= ConfigForPolicePedia.instance.Car2.CollPart2 && !Progress.shop.Get2partForPoliceCar2)
		{
			PolicepediaMarkerIn.SetActive(value: true);
			MonstropediaMarker.SetActive(value: true);
		}
		else if (Progress.shop.CollKill3Car2 >= ConfigForPolicePedia.instance.Car2.CollPart3 && !Progress.shop.Get3partForPoliceCar2)
		{
			PolicepediaMarkerIn.SetActive(value: true);
			MonstropediaMarker.SetActive(value: true);
		}
		else if (Progress.shop.CollKill4Car2 >= ConfigForPolicePedia.instance.Car2.CollPart4 && !Progress.shop.Get4partForPoliceCar2)
		{
			PolicepediaMarkerIn.SetActive(value: true);
			MonstropediaMarker.SetActive(value: true);
		}
		if (Progress.shop.Incubator_CurrentEggNum == -1)
		{
			int count = Progress.shop.Incubator_Eggs.Count;
			for (int j = 0; j < count; j++)
			{
				if (Progress.shop.Incubator_Eggs[j])
				{
					MonstropediaMarker.SetActive(value: true);
					IncubatorMarkerIn.SetActive(value: true);
					break;
				}
			}
		}
		else if (Progress.shop.Incubator_EvoProgressStep >= 0)
		{
			if (Progress.shop.Incubator_RubySetActive[Progress.shop.Incubator_EvoProgressStep])
			{
				if (!Progress.shop.Incubator_RubySetCompleat[Progress.shop.Incubator_EvoProgressStep])
				{
					MonstropediaMarker.SetActive(value: true);
					IncubatorMarkerIn.SetActive(value: true);
				}
			}
			else
			{
				switch (ConfigIncubator.instance.GerOrderRubyToUnlock()[Progress.shop.Incubator_EvoProgressStep])
				{
				case 1:
					if (Progress.shop.Incubator_CountRuby1 > 0)
					{
						MonstropediaMarker.SetActive(value: true);
						IncubatorMarkerIn.SetActive(value: true);
					}
					break;
				case 2:
					if (Progress.shop.Incubator_CountRuby2 > 0)
					{
						MonstropediaMarker.SetActive(value: true);
						IncubatorMarkerIn.SetActive(value: true);
					}
					break;
				case 3:
					if (Progress.shop.Incubator_CountRuby3 > 0)
					{
						MonstropediaMarker.SetActive(value: true);
						IncubatorMarkerIn.SetActive(value: true);
					}
					break;
				case 4:
					if (Progress.shop.Incubator_CountRuby4 > 0)
					{
						MonstropediaMarker.SetActive(value: true);
						IncubatorMarkerIn.SetActive(value: true);
					}
					break;
				}
			}
		}
		Progress.shop.LoadPolicePedia = false;
	}

	private IEnumerator openBeatfriend()
	{
		yield return new WaitForSeconds(1f);
	}

	private IEnumerator for_Daily()
	{
		if (Progress.levels.Max_Active_Pack == 1 && Progress.levels.Max_Active_Level == 3 && !Progress.shop.ShowRelamaWindow)
		{
			ReclamaWikno.SetActive(value: true);
			Progress.shop.ShowRelamaWindow = true;
		}
		while (!Progress.shop.StartComixShow)
		{
			yield return 0;
		}
		if (!Progress.shop.NeverShowPP)
		{
			while (Progress.shop.showPP)
			{
				yield return 0;
			}
		}
		if (Progress.shop.NeedForDB)
		{
			if (Progress.levels.dayEnded != 0)
			{
				DailyBonus.SetActive(value: true);
			}
			else if (Progress.levels.DAILYBONUS == 1)
			{
				DailyBonus.SetActive(value: true);
			}
			else
			{
				Progress.levels.DAILYBONUS++;
			}
		}
		else if (!FBLeaderboard.IsUserLoggedIn && Progress.shop.NeedForFB)
		{
			FBPriceNew.SetActive(value: true);
		}
	}

	protected override void OnDisable()
	{
		Input.multiTouchEnabled = true;
		base.OnDisable();
		YesLeave.onClick.RemoveAllListeners();
	}

	public void ButtonBuyRealMoney()
	{
		Audio.Play("gui_button_02_sn");
		ShowBuyCanvasWindow(isCoins: true);
		FACVOM.enabled = false;
		FACVOM._btnFirstVideoCoin.SetActive(value: false);
		//FACVOM._btnFirstVideoFuel.SetActive(value: false);
	}

	private void OnScroll(bool left)
	{
		if (left)
		{
			ScrollToLeft();
		}
		else
		{
			ScrollToRight();
		}
	}

	protected override void SetInAppPrices(CellContainer cell)
	{
		string id = Purchaser.UnlockWorld1;
		string text = PriceConfig.instance.levelsGallery.unlockWorld1DefaultPrice;
		Progress.shop.BuyPack = cell.Pack;
		switch (cell.Pack)
		{
		case 1:
			id = Purchaser.UnlockWorld1;
			text = PriceConfig.instance.levelsGallery.unlockWorld1DefaultPrice;
			break;
		case 2:
			id = Purchaser.UnlockWorld2;
			text = PriceConfig.instance.levelsGallery.unlockWorld2DefaultPrice;
			break;
		case 3:
			id = Purchaser.UnlockWorld3;
			text = PriceConfig.instance.levelsGallery.unlockWorld3DefaultPrice;
			break;
		}
		string text2 = Purchaser.m_StoreController.products.WithID(Purchaser.UnlockNext).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.UnlockNext).metadata.localizedPrice.ToString();
		text2 = ((!string.IsNullOrEmpty(text2)) ? text2 : PriceConfig.instance.levelsGallery.unlockNextLevelDefaultPrice);
		string text3 = Purchaser.m_StoreController.products.WithID(id).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(id).metadata.localizedPrice.ToString();
		text3 = ((!string.IsNullOrEmpty(text3)) ? text3 : text);
		txtBuyPackPrice.text = text2;
		txtBuyNextPrice.text = text3;
	}

	protected override void ScrollToLeft()
	{
		if (!isAnimating)
		{
			base.ScrollToLeft();
		}
	}

	protected override void ScrollToRight()
	{
		if (!isAnimating)
		{
			base.ScrollToRight();
		}
	}

	protected override void SetActiveCarIndex(int index)
	{
		base.SetActiveCarIndex(index);
	}

	protected override void SetRubiesCount(int rubies)
	{
		base.SetRubiesCount(rubies);
		txtRubies.count = rubies.ToString();
	}

	public override void SetInfIco()
	{
		if (!Progress.gameEnergy.isInfinite)
		{
			//InfEnergy.SetActive(value: false);
			//txtEnergy.gameObject.SetActive(value: true);
		}
		else
		{
			//InfEnergy.SetActive(value: true);
			//txtEnergy.gameObject.SetActive(value: false);
		}
	}

	protected override void SetEnergy(int energy)
	{
		base.SetEnergy(energy);
		//txtEnergy.count = energy.ToString();
	}

	protected override void ButtonBuyClose()
	{
		base.ButtonBuyClose();
		StartCoroutine(exitlevelbuy());
	}

	private IEnumerator exitlevelbuy()
	{
		buylevel.SetBool(_isON, value: false);
		yield return new WaitForSeconds(0.5f);
		buyLevelsWindow.SetActive(value: false);
	}

	protected override void ButtonBuyPack()
	{
		base.ButtonBuyPack();
	}

	protected override void ButtonBuyAll()
	{
		base.ButtonBuyAll();
	}

	protected override void OnPurchaseSuccess()
	{
		base.OnPurchaseSuccess();
		ButtonBuyClose();
	}

	protected override void SetActive(int pack, int level)
	{
		base.SetActive(pack, level);
		switch (pack)
		{
		case 1:
		{
			RectTransform rectTransform2 = scrollView;
			Vector3 localPosition4 = scrollView.localPosition;
			float x2 = localPosition4.x + 3380f;
			Vector3 localPosition5 = scrollView.localPosition;
			float y2 = localPosition5.y + 300f;
			Vector3 localPosition6 = scrollView.localPosition;
			rectTransform2.localPosition = new Vector3(x2, y2, localPosition6.z);
			return;
		}
		case 4:
			if (level == 9)
			{
				RectTransform rectTransform = scrollView;
				Vector3 localPosition = scrollView.localPosition;
				float x = localPosition.x + 2450f;
				Vector3 localPosition2 = scrollView.localPosition;
				float y = localPosition2.y + 300f;
				Vector3 localPosition3 = scrollView.localPosition;
				rectTransform.localPosition = new Vector3(x, y, localPosition3.z);
				return;
			}
			break;
		}
		if (pack != 1)
		{
			if (level >= 5)
			{
				RectTransform rectTransform3 = scrollView;
				Vector3 localPosition7 = scrollView.localPosition;
				float x3 = localPosition7.x + 3200f;
				Vector3 localPosition8 = scrollView.localPosition;
				float y3 = localPosition8.y + 300f;
				Vector3 localPosition9 = scrollView.localPosition;
				rectTransform3.localPosition = new Vector3(x3, y3, localPosition9.z);
			}
			else
			{
				RectTransform rectTransform4 = scrollView;
				Vector3 localPosition10 = scrollView.localPosition;
				float x4 = localPosition10.x + 3500f;
				Vector3 localPosition11 = scrollView.localPosition;
				float y4 = localPosition11.y + 300f;
				Vector3 localPosition12 = scrollView.localPosition;
				rectTransform4.localPosition = new Vector3(x4, y4, localPosition12.z);
			}
		}
	}

	protected override void OnClosedLevelClick(CellContainer cell)
	{
		base.OnClosedLevelClick(cell);
		buyLevelsWindow.SetActive(value: true);
	}

	protected override void OnAvailableClick(CellContainer cell)
	{
		base.OnAvailableClick(cell);
	}

	private IEnumerator animateTeleport(Action onComplete)
	{
		isAnimating = true;
		onComplete?.Invoke();
		isAnimating = false;
		yield break;
	}

	protected override void HideCar(CellContainer cell, Action onComplete)
	{
		if (cell.Pack == activePack)
		{
			StartCoroutine(animateTeleport(delegate
			{
				onComplete();
				base.HideCar(cell, null);
			}));
			return;
		}
		onComplete();
		base.HideCar(cell, null);
	}

	protected override void ShowCar(CellContainer cell, Action onComplete)
	{
		base.ShowCar(cell, onComplete);
		StartCoroutine(animateTeleport(delegate
		{
			onComplete();
		}));
	}

	protected override void TakeFuelAndGo()
	{
		base.TakeFuelAndGo();
		if (Progress.shop.EsterLevelPlay)
		{
			LoadRace();
		}
		else if (!Progress.shop.ArenaBrifOpen)
		{
			if (base.IsEnoughFuel)
			{
				StartCoroutine(AnimateFuel());
				return;
			}
			SetAllCellsInteractable(interactable: true);
			NotEnoughFuel();
		}
		else
		{
			StartCoroutine(AnimateFuel2());
		}
	}

	private IEnumerator AnimateFuel2()
	{
		for (float t = 0f; t < 2f; t += Time.deltaTime)
		{
			yield return 0;
		}
		LoadRace();
	}

	private IEnumerator AnimateFuel()
	{
		Audio.Play("fuel-1");
		//int fuel = GetFuelForRace();
		//AnimfuelText.text = "-" + fuel.ToString();
		//animfuel.Play();
		//animfuel["bodov_PAUSE_decreasFuel"].speed = 0.2f;
		////txtEnergy.count = GameEnergyLogic.GetEnergy.ToString();
		//while (animfuel.isPlaying)
		//{
		//	yield return 0;
		//}
		yield return new WaitForSeconds(0.5f);
        LoadRace();
	}

	protected override void NotEnoughFuel()
	{
		base.NotEnoughFuel();
	}

	private IEnumerator InitShopCanvasWindows()
	{
		SceneManager.LoadScene("premium_shop", LoadSceneMode.Additive);
		yield return 0;
		GameObject go = UnityEngine.Object.FindObjectOfType<PremiumShopNew>().gameObject;
		PremiumShopNew view = _shopWindowModel = go.GetComponent<PremiumShopNew>();
	}

	public void ShowBuyCanvasWindow(bool isCoins = false)
	{
		StartCoroutine(LoadBuyCanvasWindow(isCoins));
	}

	public IEnumerator LoadBuyCanvasWindow(bool isCoins = false)
	{
		if (_shopWindowModel == null)
		{
			yield return StartCoroutine(InitShopCanvasWindows());
		}
		ShowGetCoinsAndFuel();
	}

	public void ShowGetCoinsAndFuel()
	{
		_shopWindowModel.gameObject.SetActive(value: true);
	}

	private void SetShopCanvasTransparent(bool enable)
	{
		if (!enable)
		{
			GameObject gameObject = GameObject.Find("ShopCanvasWindows(Clone)");
			if (gameObject != null)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
			_shopWindowModel = null;
		}
	}
}
