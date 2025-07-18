//using CompleteProject;
using SA.Common.Data;
using SmartLocalization;
using Smokoko.DebugModule;
using Smokoko.Social;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
	private enum RestoreState
	{
		SuccessNoItems,
		SuccessRestored
	}

	public GameObject Setting;

	public GameObject msgBox;

	public Text CaptionMsg;

	public Text TextMsg;

	[Header("Animators")]
	public Animator global;

	public Animator reset;

	public Animator language_credits;

	public Animator FBanim;

	public Animator topPanel;

	public Animator TopIcons;

	public Animator TopName;

	[Header("AnimatorsCogs")]
	public Animator resetCog;

	public Animator language_creditsCog;

	public Animator FbanimCog;

	[Header("Buttons")]
	public Button credits;

	public Button FbLogin;

	public Button FbLogout;

	public Button restore;

	public Button language;

	public Button resetProgres;

	public Button resetProgresYes;

	public Button resetProgresNo;

	public Button Exit;

	public Button Back;

	public Button OpenSetting;

	public Toggle Music;

	public Toggle Sound;

	public Button Cheats;

	public Text Version;

	public FacebookFriendsLevelLogic FFLL;

	private int _credits_isON = Animator.StringToHash("credits_isON");

	private int _isLogedIn = Animator.StringToHash("isLogedIn");

	private int _languages_isON = Animator.StringToHash("languages_isON");

	private int _isConfirmON = Animator.StringToHash("isConfirmON");

	private int _isProgresEmpty = Animator.StringToHash("isProgresEmpty");

	private int _Test = Animator.StringToHash("Test");

	private int _settings_isON = Animator.StringToHash("settings_isON");

	private GameObject cahs;

	public Button btnGDPR;

	private object region;

	private int tapsToDebug = 15;

	public GameObject FBConect;

	private void OnEnable()
	{
		if (btnGDPR != null)
		{
			StartCoroutine(checkRegion());
		}
		//Purchaser.ForRestoreIos = null;
		//Purchaser.ForRestoreIos = (Action<bool>)Delegate.Combine(Purchaser.ForRestoreIos, new Action<bool>(ActionsForrestore));
		global.SetTrigger(_Test);
		credits.onClick.AddListener(creditsClic);
		FbLogin.onClick.AddListener(FbLoginClic);
		FbLogout.onClick.AddListener(FbLogoutClic);
		restore.onClick.AddListener(restoreClic);
		language.onClick.AddListener(languageClic);
		resetProgres.onClick.AddListener(resetProgresClic);
		resetProgresYes.onClick.AddListener(resetProgresYesClic);
		resetProgresNo.onClick.AddListener(resetProgresNoClic);
		Exit.onClick.AddListener(Off);
		Back.onClick.AddListener(BackClic);
		Cheats.onClick.AddListener(OnButtonCheatClick);
		Music.onValueChanged.AddListener(delegate(bool active)
		{
			OnButtonMusicClick(!active);
		});
		Sound.onValueChanged.AddListener(delegate(bool active)
		{
			OnButtonSoundClick(!active);
		});
		Music.isOn = !Progress.settings.isMusic;
		Sound.isOn = !Progress.settings.isSound;
		Version.text = "v." + Application.version;
		if (btnGDPR != null)
		{
			btnGDPR.onClick.AddListener(ShowWindow);
		}
	}

	private void ShowWindow()
	{
		if (cahs != null)
		{
			cahs.SetActive(value: true);
			return;
		}
		SceneManager.LoadScene("PreSplash", LoadSceneMode.Additive);
		StartCoroutine(finds());
	}

	private IEnumerator finds()
	{
		yield return new WaitForSeconds(0.5f);
		cahs = UnityEngine.Object.FindObjectOfType<GDPR_Controller>()._GdprWindow;
	}

	private IEnumerator checkRegion()
	{
		WWW www = new WWW("http://ip-api.com/json");
		yield return www;
		if (www.error != null)
		{
			btnGDPR.gameObject.SetActive(value: false);
			yield break;
		}
		Dictionary<string, object> dictionary = Json.Deserialize(www.text) as Dictionary<string, object>;
		dictionary.TryGetValue("countryCode", out region);
		UnityEngine.Debug.Log(region.ToString());
		if (region.ToString().Contains("BE") || region.ToString().Contains("EL") || region.ToString().Contains("LT") || region.ToString().Contains("PT") || region.ToString().Contains("BG") || region.ToString().Contains("ES") || region.ToString().Contains("LU") || region.ToString().Contains("RO") || region.ToString().Contains("CZ") || region.ToString().Contains("FR") || region.ToString().Contains("HU") || region.ToString().Contains("SI") || region.ToString().Contains("DK") || region.ToString().Contains("HR") || region.ToString().Contains("NO") || region.ToString().Contains("MT") || region.ToString().Contains("SK") || region.ToString().Contains("DE") || region.ToString().Contains("IT") || region.ToString().Contains("NL") || region.ToString().Contains("FI") || region.ToString().Contains("EE") || region.ToString().Contains("CY") || region.ToString().Contains("AT") || region.ToString().Contains("SE") || region.ToString().Contains("IE") || region.ToString().Contains("LV") || region.ToString().Contains("PL") || region.ToString().Contains("CH") || region.ToString().Contains("LI") || region.ToString().Contains("IS") || region.ToString().Contains("GB"))
		{
			btnGDPR.gameObject.SetActive(value: true);
		}
		else
		{
			btnGDPR.gameObject.SetActive(value: false);
		}
	}

	private void ActionsForrestore(bool sucsess)
	{
		msgBox.SetActive(value: true);
		CaptionMsg.text = LanguageManager.Instance.GetTextValue("RESTORE PURCHASES");
		if (sucsess)
		{
			TextMsg.text = LanguageManager.Instance.GetTextValue("YOUR PREVIOUSLY PURCHASD PRODUCTS HAVE BEEN RESTORED");
		}
		else
		{
			TextMsg.text = LanguageManager.Instance.GetTextValue("CAN'T FIND PURCHASED PRODUCTS");
		}
	}

	public void CloseMSG()
	{
		SceneManager.LoadScene("Start_Preloader");
	}

	private IEnumerator for_start_fb()
	{
		yield return new WaitForSeconds(0.1f);
	}

	private void OnButtonCheatClick()
	{
		if (--tapsToDebug == 0)
		{
			OpenDebug();
		}
	}

	public void PP()
	{
		string privacyPolicy = PriceConfig.instance.PrivasyPolicy.PrivacyPolicy;
		Application.OpenURL(privacyPolicy);
	}

	public void TOU()
	{
		string tempOfUse = PriceConfig.instance.PrivasyPolicy.TempOfUse;
		Application.OpenURL(tempOfUse);
	}

	private void OpenDebug()
	{
		GameCheats @object = new GameObject("_cheats").AddComponent<GameCheats>();
		DebugFacade.Init();
		DebugFacade.CustomAction = @object.Show;
		BackClic();
		StartCoroutine(Exits());
	}

	private void OnButtonMusicClick(bool enabled)
	{
		Progress.Settings settings = Progress.settings;
		Audio.musicEnabled = (settings.isMusic = enabled);
		Progress.settings = settings;
		Progress.Save(Progress.SaveType.Settings);
	}

	private void OnButtonSoundClick(bool enabled)
	{
		Progress.Settings settings = Progress.settings;
		Audio.soundEnabled = (settings.isSound = enabled);
		Progress.settings = settings;
		Progress.Save(Progress.SaveType.Settings);
	}

	private void OnDisable()
	{
		credits.onClick.RemoveAllListeners();
		FbLogin.onClick.RemoveAllListeners();
		FbLogout.onClick.RemoveAllListeners();
		restore.onClick.RemoveAllListeners();
		language.onClick.RemoveAllListeners();
		resetProgres.onClick.RemoveAllListeners();
		resetProgresYes.onClick.RemoveAllListeners();
		resetProgresNo.onClick.RemoveAllListeners();
		Exit.onClick.RemoveAllListeners();
		Back.onClick.RemoveAllListeners();
		Cheats.onClick.RemoveAllListeners();
	}

	private void creditsClic()
	{
		Audio.PlayAsync("gui_shop_cogweels_sn2");
		language_credits.SetBool(_credits_isON, value: true);
		language_creditsCog.SetBool(_credits_isON, value: true);
		TopIcons.SetBool(_credits_isON, value: true);
		topPanel.SetBool(_credits_isON, value: true);
		TopName.SetBool(_credits_isON, value: true);
	}

	private void FbLoginClic()
	{
		Audio.PlayAsync("gui_button_02_sn");
		if (Progress.shop.forFB)
		{
			FBConect.SetActive(value: true);
			return;
		}
		FBLeaderboard.LogIn();
		StartCoroutine(for_FB());
	}

	private IEnumerator for_FB()
	{
		yield return new WaitForSeconds(1f);
		StartCoroutine(FFLL.WaitUserImage());
	}

	private void Update()
	{
		if (Setting.activeSelf)
		{
			OpenSetting.interactable = false;
		}
		else
		{
			OpenSetting.interactable = true;
		}
		if (Setting.activeSelf && FBanim.gameObject.activeInHierarchy)
		{
			FBanim.SetBool(_isLogedIn, value: false);
			FbanimCog.SetBool(_isLogedIn, value: false);
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			Off();
		}
	}

	private void FbLogoutClic()
	{
		Audio.PlayAsync("gui_button_02_sn");
		FBanim.SetBool(_isLogedIn, value: false);
		FbanimCog.SetBool(_isLogedIn, value: false);
		foreach (CellContainer container in FFLL.containers)
		{
			container.FacebookFriendContainer.Off();
		}
		FBLeaderboard.LogOut();
	}

	private void restoreClic()
	{
		Audio.PlayAsync("gui_button_02_sn");
		//Purchaser.RestorePurchases();
	}

	private void languageClic()
	{
		Audio.PlayAsync("gui_shop_cogweels_sn2");
		language_credits.SetBool(_languages_isON, value: true);
		language_creditsCog.SetBool(_languages_isON, value: true);
		TopIcons.SetBool(_languages_isON, value: true);
		topPanel.SetBool(_languages_isON, value: true);
		TopName.SetBool(_languages_isON, value: true);
	}

	private void resetProgresClic()
	{
		Audio.PlayAsync("gui_shop_cogweels_sn2");
		reset.SetBool(_isConfirmON, value: true);
		reset.SetBool(_isProgresEmpty, value: false);
		resetCog.SetBool(_isConfirmON, value: true);
		resetCog.SetBool(_isProgresEmpty, value: false);
	}

	private void resetProgresNoClic()
	{
		Audio.PlayAsync("gui_shop_cogweels_sn2");
		reset.SetBool(_isConfirmON, value: false);
		reset.SetBool(_isProgresEmpty, value: false);
		resetCog.SetBool(_isConfirmON, value: false);
		resetCog.SetBool(_isProgresEmpty, value: false);
	}

	private void resetProgresYesClic()
	{
		PriceConfig.instance.energy.maxValue = 40;
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
		Progress.Reset();
		GameEnergyLogic.Reset();
		FBanim.SetBool(_isLogedIn, value: false);
		FbanimCog.SetBool(_isLogedIn, value: false);
		foreach (CellContainer container in FFLL.containers)
		{
			container.FacebookFriendContainer.Off();
		}
		FBLeaderboard.LogOut();
		for (int i = 0; i < Progress.shop.Cars.Length; i++)
		{
			if (Progress.shop.Cars[i].ARMOR_STATS == 0)
			{
				Progress.shop.Cars[i].ARMOR_STATS = ShopManagerStats.instance.Price.Car[i].Stock.ArmorStats;
			}
			if (Progress.shop.Cars[i].SPEED_STATS == 0)
			{
				Progress.shop.Cars[i].SPEED_STATS = ShopManagerStats.instance.Price.Car[i].Stock.SpeedStats;
			}
			if (Progress.shop.Cars[i].TURBO_STATS == 0)
			{
				Progress.shop.Cars[i].TURBO_STATS = ShopManagerStats.instance.Price.Car[i].Stock.TurboStats;
			}
			if (Progress.shop.Cars[i].WEAPON_STATS == 0)
			{
				Progress.shop.Cars[i].WEAPON_STATS = ShopManagerStats.instance.Price.Car[i].Stock.WeaponStats;
			}
		}
		Progress.shop.Cars[3].bought = true;
		Progress.shop.Cars[3].weaponActLev = 3;
		Progress.shop.Cars[3].wheelsActLev = 3;
		Progress.shop.Cars[3].engineActLev = 3;
		Progress.shop.Cars[3].healthActLev = 3;
		Progress.shop.Cars[3].turboActLev = 3;
		Progress.shop.Cars[4].bought = true;
		Progress.shop.Cars[4].weaponActLev = 3;
		Progress.shop.Cars[4].wheelsActLev = 3;
		Progress.shop.Cars[4].engineActLev = 3;
		Progress.shop.Cars[4].healthActLev = 3;
		Progress.shop.Cars[4].turboActLev = 3;
		Progress.shop.Cars[5].bought = true;
		Progress.shop.Cars[5].weaponActLev = 3;
		Progress.shop.Cars[5].wheelsActLev = 3;
		Progress.shop.Cars[5].engineActLev = 3;
		Progress.shop.Cars[5].healthActLev = 3;
		Progress.shop.Cars[5].turboActLev = 3;
		Progress.shop.Cars[0].bought = true;
		Progress.shop.Cars[0].equipped = true;
		Audio.musicEnabled = Progress.settings.isMusic;
		Audio.soundEnabled = Progress.settings.isSound;
		Progress.shop.NeverShowPP = true;
		Progress.Save(Progress.SaveType.Settings);
		UnityEngine.Debug.Log("CloudProgresssave DeleteSpanshotByName start ");
		UnityEngine.Debug.Log("CloudProgresssave DeleteSpanshotByName finish");
		UnityEngine.Debug.Log("CloudProgresssave clear for reset game start");
		GameCenterWrapper.SaveGameSave();
		UnityEngine.Debug.Log("CloudProgresssave clear for reset game finish");
		LocalizationManager.instance.SetDefaultLanguage();
		LocalizationObjectReferences[] array = UnityEngine.Object.FindObjectsOfType<LocalizationObjectReferences>();
		foreach (LocalizationObjectReferences localizationObjectReferences in array)
		{
			localizationObjectReferences.RefreshLanguageLables();
		}
		UnityEngine.SceneManagement.SceneManager.LoadScene("Start_Preloader");
	}

	public void On()
	{
		Setting.SetActive(value: true);
		Audio.PlayAsync("gui_screen_on");
		Game.OnStateChange(Game.gameState.OpenWindow);
		global.SetBool(_settings_isON, value: true);
		Music.isOn = !Progress.settings.isMusic;
		Sound.isOn = !Progress.settings.isSound;
		StartCoroutine(for_start_fb());
	}

	private void Off()
	{
		StartCoroutine(Exits());
	}

	private IEnumerator Exits()
	{
		Audio.PlayAsync("gui_button_02_sn");
		global.SetBool(_settings_isON, value: false);
		yield return new WaitForSeconds(1.9f);
		Setting.SetActive(value: false);
	}

	private void BackClic()
	{
		Audio.PlayAsync("gui_shop_cogweels_sn2");
		language_credits.SetBool(_credits_isON, value: false);
		language_credits.SetBool(_languages_isON, value: false);
		TopName.SetBool(_credits_isON, value: false);
		TopName.SetBool(_languages_isON, value: false);
		TopIcons.SetBool(_credits_isON, value: false);
		TopIcons.SetBool(_languages_isON, value: false);
		topPanel.SetBool(_credits_isON, value: false);
		topPanel.SetBool(_languages_isON, value: false);
		language_creditsCog.SetBool(_credits_isON, value: false);
		language_creditsCog.SetBool(_languages_isON, value: false);
	}

	private void RestoredPurchases(List<string> skus)
	{
		Progress.Shop shop = Progress.shop;
		Progress.Levels levels = Progress.levels;
		Progress.GameEnergy gameEnergy = Progress.gameEnergy;
		foreach (string sku in skus)
		{
			if (sku == InAppSettings.instance.UnlockWorld1)
			{
				levels.Pack(1).isOpen = true;
				for (int i = 1; i <= 9; i++)
				{
					levels.Pack(1).Level(i).isOpen = true;
				}
			}
			if (sku == InAppSettings.instance.UnlockWorld2)
			{
				levels.Pack(2).isOpen = true;
				for (int j = 1; j <= 9; j++)
				{
					levels.Pack(2).Level(j).isOpen = true;
				}
			}
			if (sku == InAppSettings.instance.UnlockWorld3)
			{
				levels.Pack(3).isOpen = true;
				for (int k = 1; k <= 9; k++)
				{
					levels.Pack(3).Level(k).isOpen = true;
				}
			}
			if (sku == InAppSettings.instance.UnlimitedFuel)
			{
				gameEnergy.isInfinite = true;
				GameEnergyLogic.Reset();
			}
			if (sku == InAppSettings.instance.UnlockNext)
			{
				levels.Pack(1).isOpen = true;
				for (int l = 1; l <= 13; l++)
				{
					levels.Pack(1).Level(l).isOpen = true;
				}
				levels.Pack(2).isOpen = true;
				for (int m = 1; m <= 13; m++)
				{
					levels.Pack(2).Level(m).isOpen = true;
				}
				levels.Pack(3).isOpen = true;
				for (int n = 1; n <= 13; n++)
				{
					levels.Pack(3).Level(n).isOpen = true;
				}
			}
			if (sku == InAppSettings.instance.PremiumCar)
			{
				int num = shop.Cars.Length;
				shop.Cars[3].equipped = true;
				shop.Cars[3].bought = true;
			}
			if (sku == InAppSettings.instance.DroneBee)
			{
				shop.dronBeeBuy = true;
			}
			if (sku == InAppSettings.instance.DroneBomber)
			{
				shop.dronBombsBuy = true;
			}
		}
		Progress.shop = shop;
		Progress.levels = levels;
		Progress.gameEnergy = gameEnergy;
		if (skus.Count > 0)
		{
			Progress.review.atLeastOnePurchase = true;
		}
		OnRestoreFinished((skus.Count > 0) ? RestoreState.SuccessRestored : RestoreState.SuccessNoItems);
	}

	private void OnRestoreFinished(RestoreState state)
	{
		Restore_purchases_window.Show(state == RestoreState.SuccessRestored);
	}
}
