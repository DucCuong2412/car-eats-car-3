using CompleteProject;
using Smokoko.DebugModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerGarage : MonoBehaviour
{
	public GameObject armorgo;

	public GameObject speedgo;

	public GameObject turbogo;

	public GameObject damagego;

	public GameObject gadgetsgo;

	public GameObject bombgo;

	public MarkerGarage MG;

	public Canvas canvaNeedForCamera;

	public Animator animator;

	public GameObject preloader;

	public Button armor;

	public Button turbo;

	public Button bomb;

	public Button speed;

	public Button damage;

	public Button gadgets;

	public Button Back;

	public Button exit;

	public Button shop;

	public GameObject switches;

	public GameObject canva;

	public Text priceArmor;

	public Text priceWeapon;

	public Text priceTurbo;

	public Text priceSpeed;

	public Button btn_Easter;

	public Button btn_EasterPremium;

	public Button btn_UNLOCK;

	public Button btn_UNLOCKPremium;

	public Button btn_UNLOCKVelentine;

	public Button btn_UNLOCKVelentinePremium;

	public Button btn_GotoPolicopedia;

	public Button btn_GotoIncubator;

	public Button btn_ByuFrank;

	public Text unlockPrice;

	public Text unlockPricePremium;

	public Text unlockPricePremium2;

	public Text unlockPricePremium3;

	public GameObject btn_UNLOCK_RUBY;

	public List<GameObject> GoGate = new List<GameObject>();

	public List<Animator> AnimatorGate = new List<Animator>();

	public List<GameObject> lights = new List<GameObject>();

	public List<Car2DController> controllers_Cars = new List<Car2DController>();

	public ControllerGadget gadget;

	private int countT;

	public Button _btnArenaLoc;

	public Button _btnArenaUnLoc;

	public Button _btnArenaMoneyBuy;

	private int hash_categoryArmorON = Animator.StringToHash("categoryArmorON");

	private int hash_categoryTurboON = Animator.StringToHash("categoryTurboON");

	private int hash_categorySpeedON = Animator.StringToHash("categorySpeedON");

	private int hash_categoryBombsON = Animator.StringToHash("categoryBombsON");

	private int hash_categoryDamageON = Animator.StringToHash("categoryDamageON");

	private int hash_categoryGadgetsON = Animator.StringToHash("categoryGadgetsON");

	private int hash_garageON = Animator.StringToHash("garageON");

	private int hash_EMPemmiter_isOn = Animator.StringToHash("EMPemmiter_isOn");

	private int hash_missileLauncha_isOn = Animator.StringToHash("missileLauncha_isOn");

	private int hash_magnet_isOn = Animator.StringToHash("magnet_isOn");

	private int hash_bombGenerator_isOn = Animator.StringToHash("bombGenerator_isOn");

	private int hash_freezeRay_isOn = Animator.StringToHash("freezeRay_isOn");

	private int hash_car_isUnlocked = Animator.StringToHash("car_isUnlocked");

	private int hash_car_isActivated = Animator.StringToHash("car_isActivated");

	private int hash_isScrolled = Animator.StringToHash("isScrolled");

	private int hash_IsUnlockShop = Animator.StringToHash("IsUnlockShop");

	private int hash_isLocked = Animator.StringToHash("isLocked");

	private int hash_isChange = Animator.StringToHash("isChange");

	private int hash_isNextLocked = Animator.StringToHash("isNextLocked");

	public float zaderjkaForDelayPre = 0.5f;

	public ControllerStars CS;

	public List<GameObject> forexitGO;

	public List<Image> forexitImage;

	private Camera tempCamera;

	private Camera main;

	private PremiumShopNew _shopWindowModel;

	private Progress.Shop progressShop;

	private int _coins;

	private bool _isEnergyInfinite;

	public CounterController FueLabel;

	public Image FuelInfinytyIcon;

	public CounterController coinsLabel;

	public GameObject ShopCanvasTransparent;

	public int Coins
	{
		get
		{
			return _coins;
		}
		set
		{
			_coins = value;
		}
	}

	private void OnClicArmor()
	{
		armorgo.SetActive(value: false);
		Audio.Play("gui_button_02_sn");
		animator.SetBool(hash_categoryArmorON, value: true);
		ArmorPrice();
		StartCoroutine(armors());
	}

	private IEnumerator armors()
	{
		yield return new WaitForSeconds(zaderjkaForDelayPre);
		CS.ArmorPre();
	}

	private void OnClicTurbo()
	{
		turbogo.SetActive(value: false);
		Audio.Play("gui_button_02_sn");
		animator.SetBool(hash_categoryTurboON, value: true);
		controllers_Cars[Progress.shop.activeCar].animTurbo.On();
		TurboPrice();
		StartCoroutine(turbos());
	}

	private IEnumerator turbos()
	{
		yield return new WaitForSeconds(zaderjkaForDelayPre);
		CS.turboPre();
	}

	public void OnClicSpeed()
	{
		speedgo.SetActive(value: false);
		Audio.Play("gui_button_02_sn");
		animator.SetBool(hash_categorySpeedON, value: true);
		SpeedPrice();
		StartCoroutine(speeds());
	}

	private IEnumerator speeds()
	{
		yield return new WaitForSeconds(zaderjkaForDelayPre);
		CS.speedPre();
	}

	private void OnClicBomb()
	{
		bombgo.SetActive(value: false);
		Audio.Play("gui_button_02_sn");
		animator.SetBool(hash_categoryBombsON, value: true);
	}

	private void OnClicDamage()
	{
		damagego.SetActive(value: false);
		Audio.Play("gui_button_02_sn");
		animator.SetBool(hash_categoryDamageON, value: true);
		controllers_Cars[Progress.shop.activeCar].animWeapon.Damage();
		WeaponPrice();
		StartCoroutine(weapons());
	}

	private IEnumerator weapons()
	{
		yield return new WaitForSeconds(zaderjkaForDelayPre);
		CS.damagePre();
	}

	private void OnClicGadgets()
	{
		gadgetsgo.SetActive(value: false);
		Audio.Play("gui_button_02_sn");
		animator.SetBool(hash_categoryGadgetsON, value: true);
	}

	private void OnClicBack()
	{
		MG.up();
		Audio.Play("gui_button_02_sn");
		if (controllers_Cars[Progress.shop.activeCar].animGadgets.EMP.GetBool(hash_EMPemmiter_isOn))
		{
			controllers_Cars[Progress.shop.activeCar].animGadgets.EMP.SetBool(hash_EMPemmiter_isOn, value: false);
		}
		if (controllers_Cars[Progress.shop.activeCar].animGadgets.Missle.GetBool(hash_missileLauncha_isOn))
		{
			controllers_Cars[Progress.shop.activeCar].animGadgets.Missle.SetBool(hash_missileLauncha_isOn, value: false);
		}
		if (controllers_Cars[Progress.shop.activeCar].animGadgets.Magnet.GetBool(hash_magnet_isOn))
		{
			controllers_Cars[Progress.shop.activeCar].animGadgets.Magnet.SetBool(hash_magnet_isOn, value: false);
		}
		if (controllers_Cars[Progress.shop.activeCar].animGadgets.Rechrger.GetBool(hash_bombGenerator_isOn))
		{
			controllers_Cars[Progress.shop.activeCar].animGadgets.Rechrger.SetBool(hash_bombGenerator_isOn, value: false);
		}
		if (controllers_Cars[Progress.shop.activeCar].animGadgets.LedoLuch.GetBool(hash_freezeRay_isOn))
		{
			controllers_Cars[Progress.shop.activeCar].animGadgets.LedoLuch.SetBool(hash_freezeRay_isOn, value: false);
		}
		animator.SetBool(hash_categoryArmorON, value: false);
		animator.SetBool(hash_categoryTurboON, value: false);
		animator.SetBool(hash_categorySpeedON, value: false);
		animator.SetBool(hash_categoryBombsON, value: false);
		animator.SetBool(hash_categoryDamageON, value: false);
		animator.SetBool(hash_categoryGadgetsON, value: false);
		controllers_Cars[Progress.shop.activeCar].animTurbo.Off();
		controllers_Cars[Progress.shop.activeCar].animWeapon.OffDamage();
		foreach (GameObject item in CS.startBigSpeedAlpha)
		{
			item.SetActive(value: false);
		}
		foreach (GameObject item2 in CS.startBigArmorAlpha)
		{
			item2.SetActive(value: false);
		}
		foreach (GameObject item3 in CS.startBigDamageAlpha)
		{
			item3.SetActive(value: false);
		}
		foreach (GameObject item4 in CS.startBigTurboAlpha)
		{
			item4.SetActive(value: false);
		}
	}

	private void OnClicExit()
	{
		if (!animator.GetBool(hash_categoryArmorON) && !animator.GetBool(hash_categoryTurboON) && !animator.GetBool(hash_categorySpeedON) && !animator.GetBool(hash_categoryBombsON) && !animator.GetBool(hash_categoryDamageON) && !animator.GetBool(hash_categoryGadgetsON))
		{
			Audio.Play("gui_button_02_sn");
			StartCoroutine(exitcor());
		}
		else
		{
			OnClicBack();
		}
	}

	private IEnumerator exitcor()
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
		if (!animator.GetBool("isDrones"))
		{
			animator.SetBool(hash_garageON, value: false);
			foreach (GameObject item in forexitGO)
			{
				item.SetActive(value: false);
			}
			foreach (Image item2 in forexitImage)
			{
				item2.enabled = false;
			}
		}
		else
		{
			canva.SetActive(value: false);
			if (Progress.shop.shopinlevel)
			{
				Camera[] array = UnityEngine.Object.FindObjectsOfType<Camera>();
				Camera[] array2 = array;
				foreach (Camera camera in array2)
				{
					if (camera.name == "CameraUI")
					{
						camera.orthographicSize = 5f;
					}
				}
				Results_new_controller results_new_controller = UnityEngine.Object.FindObjectOfType<Results_new_controller>();
				if (results_new_controller != null)
				{
					results_new_controller.RubyText.count = Progress.shop.currency.ToString();
				}
				progressShop.shopinlevel = false;
				SceneManager.UnloadSceneAsync("garage_new");
				base.gameObject.SetActive(value: false);
			}
			else
			{
				if (Progress.shop.ArenaBrifOpenFromGarage)
				{
					Progress.levels.InUndeground = false;
				}
				if (Progress.levels.InUndeground)
				{
					Game.LoadLevel("scene_underground_map_new");
				}
				else
				{
					Game.LoadLevel("map_new");
				}
			}
		}
		while (switches.activeSelf)
		{
			yield return 0;
			lateup();
		}
	}

	private void OnClicShop()
	{
		Audio.Play("gui_button_02_sn");
		ShowBuyCanvasWindow(isCoins: true);
	}

	private IEnumerator testcor()
	{
		yield return 0;
		Progress.shop.Cars[1].bought = progressShop.BossDeath1;
		Progress.shop.Cars[2].bought = progressShop.BossDeath2;
		animator.SetBool(hash_car_isUnlocked, Progress.shop.Cars[Progress.shop.activeCar].bought);
		animator.SetBool(hash_car_isActivated, Progress.shop.Cars[Progress.shop.activeCar].equipped);
		animator.SetBool(hash_isScrolled, value: true);
		yield return new WaitForSeconds(0.4f);
		animator.SetBool(hash_isScrolled, value: false);
		btn_UNLOCK_RUBY.SetActive(value: true);
		if (Progress.shop.activeCar == 1 && !progressShop.showOpenGateCar2)
		{
			unlockPrice.text = ShopManagerPrice.instance.Price.CarByu1.ToString();
			yield return new WaitForSeconds(1f);
			progressShop.showOpenGateCar2 = true;
			ForUnlocksStart();
		}
		else if (progressShop.showOpenGateCar2)
		{
			unlockPrice.text = ShopManagerPrice.instance.Price.CarByu1.ToString();
			AnimatorGate[1].SetBool(hash_IsUnlockShop, value: true);
		}
		if (Progress.shop.activeCar == 2 && !progressShop.showOpenGateCar3)
		{
			yield return new WaitForSeconds(1f);
			unlockPrice.text = ShopManagerPrice.instance.Price.CarByu2.ToString();
			progressShop.showOpenGateCar3 = true;
			ForUnlocksStart();
		}
		else if (progressShop.showOpenGateCar3)
		{
			unlockPrice.text = ShopManagerPrice.instance.Price.CarByu2.ToString();
			AnimatorGate[2].SetBool(hash_IsUnlockShop, value: true);
		}
		if (Progress.shop.activeCar == 3 && !progressShop.showOpenGateCar4)
		{
			yield return new WaitForSeconds(1f);
			string price6 = Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar).metadata.localizedPrice.ToString();
			price6 = ((!string.IsNullOrEmpty(price6)) ? price6 : ShopManagerPrice.instance.Price.CarByu3);
			unlockPricePremium.text = price6;
			progressShop.showOpenGateCar4 = true;
			ForUnlocksStart();
			btn_UNLOCK_RUBY.SetActive(value: false);
		}
		else if (progressShop.showOpenGateCar4)
		{
			unlockPrice.text = ShopManagerPrice.instance.Price.CarByu3;
			AnimatorGate[3].SetBool(hash_IsUnlockShop, value: true);
		}
		if (Progress.shop.activeCar == 4 && !progressShop.showOpenGateCar5)
		{
			yield return new WaitForSeconds(1f);
			string price5 = Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar2).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar2).metadata.localizedPrice.ToString();
			price5 = ((!string.IsNullOrEmpty(price5)) ? price5 : ShopManagerPrice.instance.Price.CarByu4);
			unlockPricePremium2.text = price5;
			progressShop.showOpenGateCar5 = true;
			ForUnlocksStart();
			btn_UNLOCK_RUBY.SetActive(value: false);
		}
		else if (progressShop.showOpenGateCar5)
		{
			unlockPrice.text = ShopManagerPrice.instance.Price.CarByu4;
			GoGate[4].SetActive(value: false);
		}
		if (progressShop.Get1partForPoliceCar && progressShop.Get2partForPoliceCar && progressShop.Get3partForPoliceCar && progressShop.Get4partForPoliceCar)
		{
			GoGate[6].SetActive(value: false);
			AnimatorGate[6].SetBool(hash_IsUnlockShop, value: true);
			lights[6].SetActive(value: true);
		}
		if (progressShop.Get1partForPoliceCar2 && progressShop.Get2partForPoliceCar2 && progressShop.Get3partForPoliceCar2 && progressShop.Get4partForPoliceCar2)
		{
			GoGate[7].SetActive(value: false);
			AnimatorGate[7].SetBool(hash_IsUnlockShop, value: true);
			lights[7].SetActive(value: true);
		}
		if (progressShop.Get1partForPoliceCar3 && progressShop.Get2partForPoliceCar3 && progressShop.Get3partForPoliceCar3 && progressShop.Get4partForPoliceCar3)
		{
			GoGate[11].SetActive(value: false);
			AnimatorGate[11].SetBool(hash_IsUnlockShop, value: true);
			lights[11].SetActive(value: true);
		}
	}

	private void ForUnlocksStart()
	{
		Audio.PlayAsync("monstropedia_unlock");
		Audio.PlayAsync("open_car");
		AnimatorGate[progressShop.activeCar].SetBool(hash_isLocked, value: true);
		AnimatorGate[progressShop.activeCar].SetBool(hash_isChange, value: true);
		AnimatorGate[progressShop.activeCar].SetBool(hash_isNextLocked, value: true);
		Utilities.WaitForRealSeconds(1f);
		AnimatorGate[progressShop.activeCar].SetBool(hash_isLocked, value: false);
		AnimatorGate[progressShop.activeCar].SetBool(hash_isChange, value: false);
		lights[progressShop.activeCar].SetActive(value: true);
	}

	private void CLicCloseArena()
	{
		Progress.shop.ArenaBrifOpenFromGarage = true;
		OnClicExit();
	}

	private void CLicOpenArena()
	{
		AnalyticsManager.LogEvent(EventCategoty.purchase_cars, "car2", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
		ForUnlock();
	}

	private void CLicMoneyArena()
	{
		ByPremiumCar2();
	}

	private IEnumerator for5cars()
	{
		while (true)
		{
			controllers_Cars[5].animWeapon.Damage();
			yield return new WaitForSeconds(2f);
			controllers_Cars[5].animWeapon.OffDamage();
			yield return new WaitForSeconds(2f);
		}
	}

	private void OnEnable()
	{
		StartCoroutine(for5cars());
		if (Progress.shop.Cars.Length == 11)
		{
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
			Progress.shop.Cars[5].SPEED_STATS = ShopManagerStats.instance.Price.Car[5].Stock.SpeedStats;
			Progress.shop.Cars[5].ARMOR_STATS = ShopManagerStats.instance.Price.Car[5].Stock.ArmorStats;
			Progress.shop.Cars[5].WEAPON_STATS = ShopManagerStats.instance.Price.Car[5].Stock.WeaponStats;
			Progress.shop.Cars[5].TURBO_STATS = ShopManagerStats.instance.Price.Car[5].Stock.TurboStats;
			if (Progress.shop.Cars[0].Gadget_EMP_bounght)
			{
				Progress.shop.Cars[3].Gadget_EMP_bounght = true;
				Progress.shop.Cars[4].Gadget_EMP_bounght = true;
				Progress.shop.Cars[5].Gadget_EMP_bounght = true;
				Progress.shop.Cars[6].Gadget_EMP_bounght = true;
				Progress.shop.Cars[7].Gadget_EMP_bounght = true;
				Progress.shop.Cars[8].Gadget_EMP_bounght = true;
				Progress.shop.Cars[9].Gadget_EMP_bounght = true;
				Progress.shop.Cars[10].Gadget_EMP_bounght = true;
				Progress.shop.Cars[11].Gadget_EMP_bounght = true;
			}
			if (Progress.shop.Cars[0].Gadget_MISSLLE_bounght)
			{
				Progress.shop.Cars[3].Gadget_MISSLLE_bounght = true;
				Progress.shop.Cars[4].Gadget_MISSLLE_bounght = true;
				Progress.shop.Cars[5].Gadget_MISSLLE_bounght = true;
				Progress.shop.Cars[6].Gadget_MISSLLE_bounght = true;
				Progress.shop.Cars[7].Gadget_MISSLLE_bounght = true;
				Progress.shop.Cars[8].Gadget_MISSLLE_bounght = true;
				Progress.shop.Cars[9].Gadget_MISSLLE_bounght = true;
				Progress.shop.Cars[10].Gadget_MISSLLE_bounght = true;
				Progress.shop.Cars[11].Gadget_MISSLLE_bounght = true;
			}
			if (Progress.shop.Cars[0].Gadget_Magnet_bounght)
			{
				Progress.shop.Cars[3].Gadget_Magnet_bounght = true;
				Progress.shop.Cars[4].Gadget_Magnet_bounght = true;
				Progress.shop.Cars[5].Gadget_Magnet_bounght = true;
				Progress.shop.Cars[6].Gadget_Magnet_bounght = true;
				Progress.shop.Cars[7].Gadget_Magnet_bounght = true;
				Progress.shop.Cars[8].Gadget_Magnet_bounght = true;
				Progress.shop.Cars[9].Gadget_Magnet_bounght = true;
				Progress.shop.Cars[10].Gadget_Magnet_bounght = true;
				Progress.shop.Cars[11].Gadget_Magnet_bounght = true;
			}
			if (Progress.shop.Cars[0].Gadget_LEDOLUCH_bounght)
			{
				Progress.shop.Cars[3].Gadget_LEDOLUCH_bounght = true;
				Progress.shop.Cars[4].Gadget_LEDOLUCH_bounght = true;
				Progress.shop.Cars[5].Gadget_LEDOLUCH_bounght = true;
				Progress.shop.Cars[6].Gadget_LEDOLUCH_bounght = true;
				Progress.shop.Cars[7].Gadget_LEDOLUCH_bounght = true;
				Progress.shop.Cars[8].Gadget_LEDOLUCH_bounght = true;
				Progress.shop.Cars[9].Gadget_LEDOLUCH_bounght = true;
				Progress.shop.Cars[10].Gadget_LEDOLUCH_bounght = true;
				Progress.shop.Cars[11].Gadget_LEDOLUCH_bounght = true;
			}
			if (Progress.shop.Cars[0].Gadget_RECHARGER_bounght)
			{
				Progress.shop.Cars[3].Gadget_RECHARGER_bounght = true;
				Progress.shop.Cars[4].Gadget_RECHARGER_bounght = true;
				Progress.shop.Cars[5].Gadget_RECHARGER_bounght = true;
				Progress.shop.Cars[6].Gadget_RECHARGER_bounght = true;
				Progress.shop.Cars[7].Gadget_RECHARGER_bounght = true;
				Progress.shop.Cars[8].Gadget_RECHARGER_bounght = true;
				Progress.shop.Cars[9].Gadget_RECHARGER_bounght = true;
				Progress.shop.Cars[10].Gadget_RECHARGER_bounght = true;
				Progress.shop.Cars[11].Gadget_RECHARGER_bounght = true;
			}
		}
		_btnArenaLoc.onClick.AddListener(CLicCloseArena);
		_btnArenaUnLoc.onClick.AddListener(CLicOpenArena);
		_btnArenaMoneyBuy.onClick.AddListener(CLicMoneyArena);
		Input.multiTouchEnabled = false;
		if (!Audio.isBackgroundMusicPlaying)
		{
			Audio.PlayBackgroundMusic("music_interface");
		}
		StartCoroutine(testcor());
		armor.onClick.AddListener(OnClicArmor);
		turbo.onClick.AddListener(OnClicTurbo);
		speed.onClick.AddListener(OnClicSpeed);
		bomb.onClick.AddListener(OnClicBomb);
		Back.onClick.AddListener(OnClicBack);
		damage.onClick.AddListener(OnClicDamage);
		gadgets.onClick.AddListener(OnClicGadgets);
		exit.onClick.AddListener(OnClicExit);
		shop.onClick.AddListener(OnClicShop);
		btn_UNLOCK.onClick.AddListener(UNLOCK);
		btn_UNLOCKPremium.onClick.AddListener(UNLOCK);
		btn_UNLOCKVelentinePremium.onClick.AddListener(ByPremiumCar3);
		btn_UNLOCKVelentine.onClick.AddListener(Velentine);
		btn_Easter.onClick.AddListener(EASTER);
		btn_EasterPremium.onClick.AddListener(ByPremiumCarKroll);
		btn_GotoPolicopedia.onClick.AddListener(delegate
		{
			progressShop.LoadPolicePedia = true;
			btn_UNLOCK.gameObject.SetActive(value: false);
			if (progressShop.activeCar == 6)
			{
				Progress.shop.PolispediaChusenCarFromGarage = -1;
			}
			else if (progressShop.activeCar == 7)
			{
				Progress.shop.PolispediaChusenCarFromGarage = 1;
			}
			else if (progressShop.activeCar == 11)
			{
				Progress.shop.PolispediaChusenCarFromGarage = 2;
			}
			SceneManager.LoadScene("policepedia_new");
		});
		btn_GotoIncubator.onClick.AddListener(delegate
		{
			if (progressShop.activeCar == 9 || progressShop.activeCar == 10 || progressShop.activeCar == 12 || progressShop.activeCar == 13)
			{
				if (Progress.levels._pack[1]._level[7].isOpen)
				{
					Game.LoadLevel("scene_underground_map_new");
					Progress.levels.InUndeground = true;
					Progress.shop.TestFor9 = true;
				}
				else
				{
					Game.LoadLevel("map_new");
				}
			}
			else
			{
				SceneManager.LoadScene("scene_incubator");
			}
		});
		btn_ByuFrank.onClick.AddListener(ByPremiumCarFranc);
		progressShop = Progress.shop;
		_coins = progressShop.currency;
		coinsLabel.count = Progress.shop.currency.ToString();
		FueLabel.count = Progress.gameEnergy.energy.ToString();
		FueLabel.gameObject.SetActive(!Progress.gameEnergy.isInfinite);
		FuelInfinytyIcon.gameObject.SetActive(Progress.gameEnergy.isInfinite);
		for (int i = 0; i < GoGate.Count; i++)
		{
			GoGate[i].SetActive(!progressShop.Cars[i].equipped);
			lights[i].SetActive(progressShop.Cars[i].equipped);
		}
		controllers_Cars[Progress.shop.activeCar].animGadgets.EMP.SetBool(hash_EMPemmiter_isOn, value: false);
		controllers_Cars[Progress.shop.activeCar].animGadgets.Missle.SetBool(hash_missileLauncha_isOn, value: false);
		controllers_Cars[Progress.shop.activeCar].animGadgets.Magnet.SetBool(hash_magnet_isOn, value: false);
		controllers_Cars[Progress.shop.activeCar].animGadgets.Rechrger.SetBool(hash_bombGenerator_isOn, value: false);
		controllers_Cars[Progress.shop.activeCar].animGadgets.LedoLuch.SetBool(hash_freezeRay_isOn, value: false);
		StartCoroutine(preloderHide());
		if (!Progress.shop.shopinlevel)
		{
			return;
		}
		Camera[] array = UnityEngine.Object.FindObjectsOfType<Camera>();
		Camera[] array2 = array;
		foreach (Camera camera in array2)
		{
			if (camera.name == "CameraUI")
			{
				camera.orthographicSize = 220f;
				canvaNeedForCamera.worldCamera = camera;
			}
		}
		main = Camera.main;
		main.gameObject.SetActive(value: false);
	}

	private IEnumerator preloderHide()
	{
		yield return new WaitForSeconds(1f);
		for (int i = 0; i < progressShop.Cars.Length; i++)
		{
			if (controllers_Cars.Count > i)
			{
				controllers_Cars[i].animGadgets.EMP.SetBool(hash_EMPemmiter_isOn, value: false);
				controllers_Cars[i].animGadgets.Missle.SetBool(hash_missileLauncha_isOn, value: false);
				controllers_Cars[i].animGadgets.Magnet.SetBool(hash_magnet_isOn, value: false);
				controllers_Cars[i].animGadgets.Rechrger.SetBool(hash_bombGenerator_isOn, value: false);
				controllers_Cars[i].animGadgets.LedoLuch.SetBool(hash_freezeRay_isOn, value: false);
			}
		}
		if (progressShop.showOpenGateCar2)
		{
			unlockPrice.text = ShopManagerPrice.instance.Price.CarByu1.ToString();
			AnimatorGate[1].SetBool(hash_IsUnlockShop, value: true);
			lights[1].SetActive(progressShop.Cars[1].equipped);
		}
		if (progressShop.showOpenGateCar3)
		{
			unlockPrice.text = ShopManagerPrice.instance.Price.CarByu1.ToString();
			AnimatorGate[2].SetBool(hash_IsUnlockShop, value: true);
			lights[2].SetActive(progressShop.Cars[2].equipped);
		}
		PreloaderCanvas q = preloader.GetComponent<PreloaderCanvas>();
		yield return new WaitForSeconds(1f);
		if (q != null)
		{
			q.Hide();
		}
		preloader.SetActive(value: false);
	}

	private void Velentine()
	{
	}

	private void EASTER()
	{
		Progress.shop.EsterForMap = true;
		if (Progress.levels.InUndeground)
		{
			Game.LoadLevel("scene_underground_map_new");
		}
		else
		{
			Game.LoadLevel("map_new");
		}
	}

	private void UNLOCK()
	{
		if (Progress.shop.activeCar == 1)
		{
			if (ShopManagerPrice.instance.Price.CarByu1 <= Progress.shop.currency)
			{
				Progress.shop.currency -= ShopManagerPrice.instance.Price.CarByu1;
				coinsLabel.count = Progress.shop.currency.ToString();
				AnalyticsManager.LogEvent(EventCategoty.purchase_cars, "car2", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
				ForUnlock();
			}
			else
			{
				ShowBuyCanvasWindow(isCoins: true);
			}
		}
		else if (Progress.shop.activeCar == 2)
		{
			if (ShopManagerPrice.instance.Price.CarByu2 <= Progress.shop.currency)
			{
				Progress.shop.currency -= ShopManagerPrice.instance.Price.CarByu2;
				coinsLabel.count = Progress.shop.currency.ToString();
				AnalyticsManager.LogEvent(EventCategoty.purchase_cars, "car3", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
				ForUnlock();
			}
			else
			{
				ShowBuyCanvasWindow(isCoins: true);
			}
		}
		else if (Progress.shop.activeCar == 3)
		{
			ByPremiumCar();
		}
		GameCenter.OnPurchaseCars();
	}

	private void ByPremiumCar()
	{
		if (DebugFacade.isDebug)
		{
			ModalWindow.instance.Show("Debug buy Premium Car", new ButtonInfo("Buy", ByPC));
		}
		else
		{
			Purchaser.BuyProductID(Purchaser.PremiumCar, ByPC);
		}
	}

	private void ByPC()
	{
		AnalyticsManager.LogEvent(EventCategoty.premium_shop, "premium_car", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
		Progress.shop.BuyForRealMoney = true;
		ForUnlock();
	}

	private void ByPremiumCar2()
	{
		if (DebugFacade.isDebug)
		{
			ModalWindow.instance.Show("Debug buy Premium Car2", new ButtonInfo("Buy", ByPC2));
		}
		else
		{
			Purchaser.BuyProductID(Purchaser.PremiumCar2, ByPC2);
		}
	}

	private void ByPC2()
	{
		AnalyticsManager.LogEvent(EventCategoty.premium_shop, "jason", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
		Progress.shop.BuyForRealMoney = true;
		ForUnlock();
	}

	private void ByPremiumCarFranc()
	{
		if (progressShop.activeCar == 9)
		{
			if (DebugFacade.isDebug)
			{
				ModalWindow.instance.Show("Debug buy car9", new ButtonInfo("Buy", ByBug1));
			}
			else
			{
				Purchaser.BuyProductID(Purchaser.PremiumCar9, ByBug1);
			}
		}
		else if (progressShop.activeCar == 10)
		{
			if (DebugFacade.isDebug)
			{
				ModalWindow.instance.Show("Debug buy car10", new ButtonInfo("Buy", ByBug2));
			}
			else
			{
				Purchaser.BuyProductID(Purchaser.PremiumCar10, ByBug2);
			}
		}
		else if (progressShop.activeCar == 6)
		{
			if (DebugFacade.isDebug)
			{
				ModalWindow.instance.Show("Debug buy Franck", new ButtonInfo("Buy", Byfranc));
			}
			else
			{
				Purchaser.BuyProductID(Purchaser.PremiumCar4, Byfranc);
			}
		}
		else if (progressShop.activeCar == 7)
		{
			if (DebugFacade.isDebug)
			{
				ModalWindow.instance.Show("Debug buy CarCop", new ButtonInfo("Buy", ByCarCop));
			}
			else
			{
				Purchaser.BuyProductID(Purchaser.PremiumCar5, ByCarCop);
			}
		}
		else if (progressShop.activeCar == 11)
		{
			if (DebugFacade.isDebug)
			{
				ModalWindow.instance.Show("Debug buy ByCarTank", new ButtonInfo("Buy", ByCarTank));
			}
			else
			{
				Purchaser.BuyProductID(Purchaser.Tankominator, ByCarTank);
			}
		}
		else if (progressShop.activeCar == 12)
		{
			if (DebugFacade.isDebug)
			{
				ModalWindow.instance.Show("Debug buy Croco", new ButtonInfo("Buy", ByCroco));
			}
			else
			{
				Purchaser.BuyProductID(Purchaser.Croco, ByCroco);
			}
		}
		else if (progressShop.activeCar == 13)
		{
			if (DebugFacade.isDebug)
			{
				ModalWindow.instance.Show("Debug buy Cherepaha", new ButtonInfo("Buy", ByCherepaha));
			}
			else
			{
				Purchaser.BuyProductID(Purchaser.Cherepaha, ByCherepaha);
			}
		}
	}

	private void ByBug1()
	{
		AnalyticsManager.LogEvent(EventCategoty.premium_shop, "bug1", Progress.levels.Max_Active_Pack.ToString() + "_" + Progress.levels.Max_Active_Pack.ToString());
		Progress.shop.BuyForRealMoney = true;
		ForUnlock();
	}

	private void ByBug2()
	{
		AnalyticsManager.LogEvent(EventCategoty.premium_shop, "bug2", Progress.levels.Max_Active_Pack.ToString() + "_" + Progress.levels.Max_Active_Pack.ToString());
		Progress.shop.BuyForRealMoney = true;
		ForUnlock();
	}

	private void ByCroco()
	{
		AnalyticsManager.LogEvent(EventCategoty.premium_shop, "alligator", Progress.levels.Max_Active_Pack.ToString() + "_" + Progress.levels.Max_Active_Pack.ToString());
		Progress.shop.BuyForRealMoney = true;
		ForUnlock();
	}

	private void ByCherepaha()
	{
		AnalyticsManager.LogEvent(EventCategoty.premium_shop, "turtle", Progress.levels.Max_Active_Pack.ToString() + "_" + Progress.levels.Max_Active_Pack.ToString());
		Progress.shop.BuyForRealMoney = true;
		ForUnlock();
	}

	private void Byfranc()
	{
		AnalyticsManager.LogEvent(EventCategoty.premium_shop, "francopstein", Progress.levels.Max_Active_Pack.ToString() + "_" + Progress.levels.Max_Active_Pack.ToString());
		Progress.shop.BuyForRealMoney = true;
		ForUnlock();
	}

	private void ByCarCop()
	{
		AnalyticsManager.LogEvent(EventCategoty.premium_shop, "carocop", Progress.levels.Max_Active_Pack.ToString() + "_" + Progress.levels.Max_Active_Pack.ToString());
		Progress.shop.BuyForRealMoney = true;
		ForUnlock();
	}

	private void ByCarTank()
	{
		AnalyticsManager.LogEvent(EventCategoty.premium_shop, "tankominator", Progress.levels.Max_Active_Pack.ToString() + "_" + Progress.levels.Max_Active_Pack.ToString());
		Progress.shop.BuyForRealMoney = true;
		ForUnlock();
	}

	private void ByPremiumCarKroll()
	{
		if (DebugFacade.isDebug)
		{
			ModalWindow.instance.Show("Debug buy KROll", new ButtonInfo("Buy", ByPCKroll));
		}
		else
		{
			Purchaser.BuyProductID(Purchaser.PremiumCar6, ByPCKroll);
		}
	}

	private void ByPCKroll()
	{
		AnalyticsManager.LogEvent(EventCategoty.premium_shop, "rabbitster", Progress.levels.Max_Active_Pack.ToString() + "_" + Progress.levels.Max_Active_Pack.ToString());
		Progress.shop.BuyForRealMoney = true;
		ForUnlock();
	}

	private void ByPremiumCar3()
	{
		if (DebugFacade.isDebug)
		{
			ModalWindow.instance.Show("Debug buy Premium Car3", new ButtonInfo("Buy", ByPC3));
		}
		else
		{
			Purchaser.BuyProductID(Purchaser.PremiumCar3, ByPC3);
		}
	}

	private void ByPC3()
	{
		AnalyticsManager.LogEvent(EventCategoty.premium_shop, "beetlee", Progress.levels.Max_Active_Pack.ToString() + "_" + Progress.levels.Max_Active_Pack.ToString());
		Progress.shop.BuyForRealMoney = true;
		ForUnlock();
	}

	private void ForUnlock()
	{
		if (progressShop.activeCar == 6)
		{
			progressShop.Get1partForPoliceCar = true;
			progressShop.Get2partForPoliceCar = true;
			progressShop.Get3partForPoliceCar = true;
			progressShop.Get4partForPoliceCar = true;
			progressShop.CollKill1Car = 120;
			progressShop.CollKill2Car = 120;
			progressShop.CollKill3Car = 120;
			progressShop.CollKill4Car = 120;
		}
		if (progressShop.activeCar == 7)
		{
			progressShop.Get1partForPoliceCar2 = true;
			progressShop.Get2partForPoliceCar2 = true;
			progressShop.Get3partForPoliceCar2 = true;
			progressShop.Get4partForPoliceCar2 = true;
			progressShop.CollKill1Car2 = 320;
			progressShop.CollKill2Car2 = 320;
			progressShop.CollKill3Car2 = 320;
			progressShop.CollKill4Car2 = 320;
		}
		if (progressShop.activeCar == 11)
		{
			progressShop.Get1partForPoliceCar3 = true;
			progressShop.Get2partForPoliceCar3 = true;
			progressShop.Get3partForPoliceCar3 = true;
			progressShop.Get4partForPoliceCar3 = true;
			progressShop.CollKill1Car3 = 520;
			progressShop.CollKill2Car3 = 520;
			progressShop.CollKill3Car3 = 520;
			progressShop.CollKill4Car3 = 520;
		}
		Audio.Play("gui_button_02_sn");
		Audio.PlayAsync("monstropedia_unlock");
		Audio.PlayAsync("open_car");
		MG.up();
		AnimatorGate[progressShop.activeCar].SetBool(hash_isLocked, value: true);
		progressShop.Cars[progressShop.activeCar].equipped = true;
		animator.SetBool(hash_car_isActivated, value: true);
		AnimatorGate[progressShop.activeCar].SetBool(hash_isChange, value: true);
		AnimatorGate[progressShop.activeCar].SetBool(hash_isNextLocked, value: true);
		Utilities.WaitForRealSeconds(1f);
		AnimatorGate[progressShop.activeCar].SetBool(hash_isLocked, value: false);
		AnimatorGate[progressShop.activeCar].SetBool(hash_isChange, value: false);
		lights[progressShop.activeCar].SetActive(value: true);
	}

	private void OnDisable()
	{
		Input.multiTouchEnabled = true;
		armor.onClick.RemoveAllListeners();
		turbo.onClick.RemoveAllListeners();
		speed.onClick.RemoveAllListeners();
		bomb.onClick.RemoveAllListeners();
		Back.onClick.RemoveAllListeners();
		damage.onClick.RemoveAllListeners();
		gadgets.onClick.RemoveAllListeners();
		exit.onClick.RemoveAllListeners();
		shop.onClick.RemoveAllListeners();
		btn_GotoPolicopedia.onClick.RemoveAllListeners();
		btn_ByuFrank.onClick.RemoveAllListeners();
		btn_UNLOCK.onClick.RemoveAllListeners();
		btn_UNLOCKPremium.onClick.RemoveAllListeners();
		btn_UNLOCKVelentine.onClick.RemoveAllListeners();
		btn_UNLOCKVelentinePremium.onClick.RemoveAllListeners();
		btn_Easter.onClick.RemoveAllListeners();
		btn_EasterPremium.onClick.RemoveAllListeners();
		_btnArenaLoc.onClick.RemoveAllListeners();
		_btnArenaMoneyBuy.onClick.RemoveAllListeners();
		_btnArenaUnLoc.onClick.RemoveAllListeners();
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

	private int GetTimeNextPlayDay()
	{
		return (Progress.shop.EsterEndTime - DateTime.UtcNow).Days;
	}

	private int GetTimeNextPlayHours()
	{
		return (Progress.shop.EsterEndTime - DateTime.UtcNow).Hours;
	}

	private int GetTimeNextPlayMinutes()
	{
		return (Progress.shop.EsterEndTime - DateTime.UtcNow).Minutes;
	}

	private int GetTimeNextPlaySeconds()
	{
		return (Progress.shop.EsterEndTime - DateTime.UtcNow).Seconds;
	}

	private void Update()
	{
		if (Progress.shop.activeCar == 3)
		{
			_btnArenaUnLoc.transform.parent.gameObject.SetActive(value: false);
			btn_GotoPolicopedia.transform.parent.gameObject.SetActive(value: false);
			btn_UNLOCK.gameObject.SetActive(value: false);
			btn_UNLOCKVelentine.transform.parent.gameObject.SetActive(value: false);
			btn_UNLOCKVelentinePremium.transform.parent.gameObject.SetActive(value: false);
			btn_UNLOCKPremium.gameObject.SetActive(value: true);
			btn_Easter.gameObject.transform.parent.gameObject.SetActive(value: false);
		}
		else if (progressShop.activeCar == 4)
		{
			if (!progressShop.Car.equipped)
			{
				_btnArenaUnLoc.transform.parent.gameObject.SetActive(value: true);
				btn_GotoPolicopedia.transform.parent.gameObject.SetActive(value: false);
				btn_UNLOCK.gameObject.SetActive(value: false);
				btn_UNLOCKPremium.gameObject.SetActive(value: false);
				_btnArenaMoneyBuy.gameObject.SetActive(value: true);
				btn_UNLOCKVelentinePremium.transform.parent.gameObject.SetActive(value: false);
				btn_Easter.gameObject.transform.parent.gameObject.SetActive(value: false);
				btn_UNLOCKVelentine.transform.parent.gameObject.SetActive(value: false);
				if (progressShop.Key1 && progressShop.Key2 && progressShop.Key3)
				{
					_btnArenaUnLoc.gameObject.SetActive(value: true);
					_btnArenaLoc.gameObject.SetActive(value: false);
				}
				else
				{
					_btnArenaLoc.gameObject.SetActive(value: true);
					_btnArenaUnLoc.gameObject.SetActive(value: false);
				}
			}
			else
			{
				btn_GotoPolicopedia.transform.parent.gameObject.SetActive(value: false);
				_btnArenaUnLoc.transform.parent.gameObject.SetActive(value: false);
				btn_UNLOCKVelentinePremium.transform.parent.gameObject.SetActive(value: false);
			}
		}
		else if (progressShop.activeCar == 5)
		{
			if (!progressShop.Car.equipped)
			{
				_btnArenaUnLoc.transform.parent.gameObject.SetActive(value: false);
				btn_UNLOCK.gameObject.SetActive(value: false);
				btn_UNLOCKPremium.gameObject.SetActive(value: false);
				btn_Easter.gameObject.transform.parent.gameObject.SetActive(value: false);
				_btnArenaLoc.gameObject.SetActive(value: false);
				btn_GotoPolicopedia.transform.parent.gameObject.SetActive(value: false);
				_btnArenaUnLoc.gameObject.SetActive(value: false);
				bool active = false;
				btn_UNLOCKVelentine.gameObject.SetActive(active);
				btn_UNLOCKVelentinePremium.transform.parent.gameObject.SetActive(value: true);
				btn_UNLOCKVelentinePremium.gameObject.SetActive(!progressShop.Car.equipped);
			}
		}
		else if (progressShop.activeCar == 6 || progressShop.activeCar == 7 || progressShop.activeCar == 9 || progressShop.activeCar == 10 || progressShop.activeCar == 11 || progressShop.activeCar == 12 || progressShop.activeCar == 13)
		{
			if (!progressShop.Car.equipped)
			{
				btn_GotoPolicopedia.transform.parent.gameObject.SetActive(value: true);
				btn_GotoPolicopedia.gameObject.SetActive(value: false);
				btn_GotoIncubator.gameObject.SetActive(value: false);
				if (progressShop.activeCar == 6 || progressShop.activeCar == 7 || progressShop.activeCar == 11)
				{
					btn_GotoPolicopedia.gameObject.SetActive(value: true);
				}
				else
				{
					btn_GotoIncubator.gameObject.SetActive(value: true);
				}
				_btnArenaUnLoc.transform.parent.gameObject.SetActive(value: false);
				btn_UNLOCK.gameObject.SetActive(value: false);
				btn_UNLOCKPremium.gameObject.SetActive(value: false);
				btn_Easter.gameObject.transform.parent.gameObject.SetActive(value: false);
				_btnArenaMoneyBuy.gameObject.SetActive(value: false);
				btn_UNLOCKVelentinePremium.transform.parent.gameObject.SetActive(value: false);
				btn_UNLOCKVelentine.transform.parent.gameObject.SetActive(value: false);
			}
		}
		else if (progressShop.activeCar == 8)
		{
			bool flag = false;
			flag = ((GetTimeNextPlayMinutes() > 0 || GetTimeNextPlayHours() > 0 || GetTimeNextPlayDay() > 0 || GetTimeNextPlaySeconds() > 0) ? true : false);
			btn_Easter.gameObject.transform.parent.gameObject.SetActive(value: true);
			btn_Easter.gameObject.SetActive(flag);
			btn_EasterPremium.gameObject.SetActive(!flag);
			btn_GotoPolicopedia.transform.parent.gameObject.SetActive(value: false);
			_btnArenaUnLoc.transform.parent.gameObject.SetActive(value: false);
			btn_UNLOCK_RUBY.gameObject.SetActive(value: false);
			btn_UNLOCK.gameObject.SetActive(value: false);
			btn_UNLOCKPremium.gameObject.SetActive(value: false);
			_btnArenaMoneyBuy.gameObject.SetActive(value: false);
			btn_UNLOCKVelentinePremium.transform.parent.gameObject.SetActive(value: false);
			btn_UNLOCKVelentine.transform.parent.gameObject.SetActive(value: false);
		}
		else
		{
			_btnArenaUnLoc.transform.parent.gameObject.SetActive(value: false);
			btn_GotoPolicopedia.transform.parent.gameObject.SetActive(value: false);
			btn_Easter.gameObject.transform.parent.gameObject.SetActive(value: false);
			if (!animator.GetBool("isDrones"))
			{
				if (progressShop.Car.bought)
				{
					btn_UNLOCK.gameObject.SetActive(value: true);
					btn_UNLOCK.gameObject.transform.parent.gameObject.SetActive(value: true);
				}
				else
				{
					btn_UNLOCK.gameObject.transform.parent.gameObject.SetActive(value: false);
					btn_UNLOCK.gameObject.SetActive(value: false);
				}
				btn_UNLOCKPremium.gameObject.SetActive(value: false);
			}
			else
			{
				btn_UNLOCK.gameObject.transform.parent.gameObject.SetActive(value: false);
				btn_UNLOCK.gameObject.SetActive(value: false);
			}
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape) && !progressShop.TutorialGarage)
		{
			OnClicExit();
		}
		if (countT == 10)
		{
			SetCoinsLabel(progressShop.currency);
			SetEnergyLabel(Progress.gameEnergy.energy);
			countT = 0;
		}
		else
		{
			countT++;
		}
	}

	private void lateup()
	{
		if (main != null)
		{
			main.gameObject.SetActive(value: true);
		}
		if (switches.activeSelf)
		{
			return;
		}
		canva.SetActive(value: false);
		if (Progress.shop.shopinlevel)
		{
			Camera[] array = UnityEngine.Object.FindObjectsOfType<Camera>();
			Camera[] array2 = array;
			foreach (Camera camera in array2)
			{
				if (camera.name == "CameraUI")
				{
					camera.orthographicSize = 5f;
				}
			}
			progressShop.shopinlevel = false;
			SceneManager.UnloadSceneAsync("garage_new");
			base.gameObject.SetActive(value: false);
		}
		else if (Progress.levels.InUndeground)
		{
			Game.LoadLevel("scene_underground_map_new");
		}
		else
		{
			Game.LoadLevel("map_new");
		}
	}

	public void SpeedPrice()
	{
		if (Progress.shop.Cars[Progress.shop.activeCar].engineActLev == 0)
		{
			priceSpeed.text = ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Speed[0].price.ToString();
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].engineActLev == 1)
		{
			priceSpeed.text = ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Speed[1].price.ToString();
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].engineActLev == 2)
		{
			priceSpeed.text = ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Speed[2].price.ToString();
		}
	}

	public void WeaponPrice()
	{
		if (Progress.shop.Cars[Progress.shop.activeCar].weaponActLev == 0)
		{
			priceWeapon.text = ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Weapon[0].price.ToString();
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].weaponActLev == 1)
		{
			priceWeapon.text = ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Weapon[1].price.ToString();
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].weaponActLev == 2)
		{
			priceWeapon.text = ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Weapon[2].price.ToString();
		}
	}

	public void ArmorPrice()
	{
		if (Progress.shop.Cars[Progress.shop.activeCar].healthActLev == 0)
		{
			priceArmor.text = ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Armor[0].price.ToString();
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].healthActLev == 1)
		{
			priceArmor.text = ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Armor[1].price.ToString();
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].healthActLev == 2)
		{
			priceArmor.text = ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Armor[2].price.ToString();
		}
	}

	public void TurboPrice()
	{
		if (Progress.shop.Cars[Progress.shop.activeCar].turboActLev == 0)
		{
			priceTurbo.text = ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Turbo[0].price.ToString();
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].turboActLev == 1)
		{
			priceTurbo.text = ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Turbo[1].price.ToString();
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].turboActLev == 2)
		{
			priceTurbo.text = ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Turbo[2].price.ToString();
		}
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

	private void ShowGetCoinsAndFuel()
	{
		UnityEngine.Debug.Log("Minus currency !!!!!");
		_shopWindowModel.gameObject.SetActive(value: true);
	}

	private void SetShopCanvasTransparent(bool enable)
	{
		SetCanvasTransparent(enable);
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

	public void SetEnergyLabel(int count)
	{
		FueLabel.count = count.ToString();
	}

	public void SetCanvasTransparent(bool enable)
	{
		ShopCanvasTransparent.SetActive(enable);
	}

	public void SetCoinsLabel(int count)
	{
		coinsLabel.count = count.ToString();
	}

	public void SetFuelInfinytyIcon(bool enable)
	{
		FuelInfinytyIcon.gameObject.SetActive(enable);
		FueLabel.gameObject.SetActive(!enable);
	}
}
