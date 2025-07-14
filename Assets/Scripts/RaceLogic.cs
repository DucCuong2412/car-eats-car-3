using Smokoko.Social;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceLogic : MonoBehaviour
{
	public enum enItem
	{
		Rubin,
		Health,
		Nitro,
		Rocket,
		Flip,
		Cloud,
		Metor,
		Freeze,
		Enigma,
		Ticket,
		DamageX2,
		BombCar,
		Zeppelin,
		Copter,
		Police,
		ReduceDamage
	}

	public enum enItemRuby
	{
		Rubin,
		none
	}

	private delegate void RubinsCollect(int rubinsCount, bool saveShop = false);

	private static RaceLogic _instance;

	public int level;

	public int pack;

	public RaceManager race;

	public Car2DController car;

	public GameObject Convoi;

	private int carNum;

	public GuiContainer gui;

	public CarFollow follow;

	private int CarDeathOnLevel;

	private int EnemiesKilledPerDT;

	private int EnemiesKilledPerDTCivic;

	private bool _noVideoShowed;

	public bool BossDeath;

	public bool AllInitedForPool;

	private List<int> UsegColorsIndexs = new List<int>();

	private bool PauseVideo;

	private bool PauseActivate;

	private bool PauseDeActivate;

	public bool isRestoreBoostWasShowed;

	public bool deathInMarker;

	public float goleftonmarker;

	private bool isCoinsBuyMenuShowed;

	public bool FortuneVideo;

	public bool rewiveVideo;

	private Action Fort_video_suc;

	private Action Fort_video_nosuc;

	private Action Fort_video_novideo;

	private PreloaderCanvas preloader;

	private GameObject Dron;

	private GameObject Dron2;

	private GameObject Dron3;

	private bool chek = true;

	private int hash_IsON = Animator.StringToHash("isON");

	private int hash_NeedForExit = Animator.StringToHash("NeedForExit");

	public GameObject startsss;

	public Coroutine TimeToDieCorut;

	public float TimeDieSpecMissions;

	public float TimeDieSpecMissionsForMee;

	private List<Car2DController> Cars = new List<Car2DController>();

	private IEnumerator OnDieCoroutine;

	public bool chekSparkl = true;

	private Results_lost_new_controller R_L_N_C;

	private Results_new_controller Res_New_Contr;

	private IEnumerator checkingEnemies;

	private List<Action> NonInitedAI = new List<Action>();

	public List<GameObject> bomb_lip = new List<GameObject>();

	public int CounterEmemys;

	private int LastCounterEmemys;

	public int CurrentDifLevel;

	public int MaxEnemysInLevel;

	public static RaceLogic instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (RaceLogic)UnityEngine.Object.FindObjectOfType(typeof(RaceLogic));
				if (_instance == null)
				{
					GameObject gameObject = new GameObject();
					gameObject.name = "_raceLogic";
					_instance = gameObject.AddComponent<RaceLogic>();
				}
			}
			return _instance;
		}
	}

	private event RubinsCollect OnRubinsCollect;

	private event RubinsCollect OnRubinsAICollect;

	private event RubinsCollect OnRubinsBonusCollect;

	public int GerCivilColorsIndex()
	{
		if (UsegColorsIndexs.Count >= DifficultyConfig.instance.ColorsAll.Count)
		{
			UsegColorsIndexs.Clear();
		}
		int num = -1;
		for (int i = 0; i < 5; i++)
		{
			if (num != -1)
			{
				break;
			}
			num = UnityEngine.Random.Range(0, DifficultyConfig.instance.ColorsAll.Count);
			for (int j = 0; j < UsegColorsIndexs.Count; j++)
			{
				if (num == UsegColorsIndexs[j])
				{
					num = -1;
					break;
				}
			}
		}
		if (num == -1)
		{
			num = UnityEngine.Random.Range(0, DifficultyConfig.instance.ColorsAll.Count);
		}
		UsegColorsIndexs.Add(num);
		return num;
	}

	public void badgeTutorial()
	{
		StartCoroutine(forTutorialBadge());
	}

	public IEnumerator forTutorialBadge()
	{
		yield return new WaitForSeconds(1f);
		instance.gui.THUD.Shade.SetActive(value: true);
		instance.gui.THUD.Badge.SetActive(value: true);
		instance.gui.THUD.badges.interactable = false;
		instance.gui.THUD.bonuses.interactable = false;
		instance.gui.THUD.i = 0;
		Progress.shop.TutorialBadgeNeed = false;
		Time.timeScale = 0f;
	}

	public void OnFinish1()
	{
		if (Audio.IsSoundPlaying("gfx_turbo_01_sn"))
		{
			Audio.Stop("gfx_turbo_01_sn");
		}
		if (Convoi != null)
		{
			UnityEngine.Debug.Log("Convoi finish");
			car.HealthModule._barrel.Enable = false;
			car.HealthModule._barrel.enabled = false;
		}
		OnFinish();
		Convoi = null;
		if (TimeToDieCorut != null)
		{
			StopCoroutine(TimeToDieCorut);
		}
	}

	public void Init(RaceManager race, Car2DController car, GuiContainer gui, CarFollow follow, int level, int pack)
	{
		this.race = race;
		this.car = car;
		this.car.HealthModule.AnDeath = false;
		this.gui = gui;
		this.follow = follow;
		this.level = level;
		this.pack = pack;
		Start1();
	}

	public void OnBulletReachCar(Car2DGun.BulletType bulletType)
	{
		car.OnEnemyBullet(bulletType, gui);
	}

	private void Start1()
	{
		Progress.shop.Car.id = Progress.shop.activeCar;
		Progress.shop.Car.bombActLev = Progress.shop.Cars[Progress.shop.activeCar].bombActLev;
		Progress.shop.Car.healthActLev = Progress.shop.Cars[Progress.shop.activeCar].healthActLev;
		Progress.shop.Car.turboActLev = Progress.shop.Cars[Progress.shop.activeCar].turboActLev;
		Progress.shop.Car.engineActLev = Progress.shop.Cars[Progress.shop.activeCar].engineActLev;
		Progress.shop.Car.weaponActLev = Progress.shop.Cars[Progress.shop.activeCar].weaponActLev;
		car.gameObject.SetActive(value: true);
		car.Init(Progress.shop.Car);
		carNum = Progress.shop.activeCar;
		InitGUI();
		if (car != null)
		{
			car.OnDie += OnDie;
		}
		OnRubinsCollect += SetRubins;
		OnRubinsAICollect += SetRubinsAI;
		OnRubinsBonusCollect += SetRubinsBonus;
		SuperBonusesManager.instance.Init(Camera.main.transform, race.activeAIs);
		StartRace();
		CounterEmemys = 0;
		LastCounterEmemys = -1;
		CurrentDifLevel = 0;
		if (Progress.shop.bossLevel)
		{
			gui.StrtCorutFunc();
		}
		_noVideoShowed = false;
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (!pauseStatus)
		{
			if (Game.currentState == Game.gameState.Revive)
			{
				Sucsessss();
			}
			if (!PauseVideo && Game.currentState == Game.gameState.Race)
			{
				PauseRace();
			}
		}
	}

	private void InitGUI()
	{
		car.SetCallbackControll(delegate(bool b)
		{
			gui.interface_Controlls.SetTurboButtonState(b);
		}, delegate(bool b)
		{
			gui.interface_Controlls.SetBombButtonState(b);
		});
		gui.interface_BoostTimer.Init(gui.interface_ColorTint);
		gui.interface_CarInfoBars.ChangeRocketCount(car.BombModule._Barrel.Value);
		car.OnHeathChanged += delegate(float v)
		{
			gui.interface_CarInfoBars.ChangeHealthBar(v);
		};
		car.OnTurboChanged += delegate(float v)
		{
			gui.interface_CarInfoBars.ChangeTurboBar(v);
		};
		car.OnWeaponChanged += delegate(float v)
		{
			gui.interface_CarInfoBars.ChangeRocketCount(v);
		};
		car.OnBoostHealthChanged += delegate(float v)
		{
			gui.interface_CarInfoBars.ChangeBoostHealthBar(v);
		};
		car.OnBoostTurboChanged += delegate(float v)
		{
			gui.interface_CarInfoBars.ChangeBoostTurboBar(v);
		};
		gui.interface_Controlls.InitControlls(delegate
		{
			car.rotationUse = -1f;
		}, delegate
		{
			car.rotationUse = 1f;
		}, delegate
		{
			car.rotationUse = 0f;
		}, delegate
		{
			car.turboUse = true;
		}, delegate
		{
			car.turboUse = false;
		}, delegate
		{
			car.Fire();
		}, PauseRace);
	}

	public void PauseRace()
	{
		if (Game.currentState != Game.gameState.Finish && !PauseActivate)
		{
			PauseActivate = true;
			PauseDeActivate = true;
			StartCoroutine(delayPauseStart());
			Game.OnStateChange(Game.gameState.PausedRace);
			gui.windows_pause.Show(Utilities.LevelNumberGlobal(level, pack).ToString(), delegate
			{
				ResumeRace();
			}, delegate
			{
				if (Progress.shop.EsterLevelPlay)
				{
					Progress.shop.EsterX2TimeActivate = false;
				}
				StartCoroutine(delayPauseRestart());
			}, delegate
			{
				if (Progress.shop.EsterLevelPlay)
				{
					Progress.shop.EsterX2TimeActivate = false;
				}
				if (Progress.levels.InUndeground)
				{
					Game.LoadLevel("scene_underground_map_new");
				}
				else
				{
					Game.LoadLevel("map_new");
				}
			}, delegate
			{
				Audio.soundEnabled = !Audio.soundEnabled;
				Progress.settings.isSound = Audio.soundEnabled;
				Progress.Save(Progress.SaveType.Settings);
			}, delegate
			{
				Audio.musicEnabled = !Audio.musicEnabled;
				if (Audio.musicEnabled)
				{
					Audio.PauseBackgroundMusic();
				}
				Progress.settings.isMusic = Audio.musicEnabled;
				Progress.Save(Progress.SaveType.Settings);
			}, Progress.settings.isSound, Progress.settings.isMusic);
		}
	}

	private IEnumerator delayPauseRestart()
	{
		gui.EndCage();
		if (Progress.shop.endlessLevel)
		{
			PriceConfig.instance.energy.eachStart = 10;
		}
		if (!GameEnergyLogic.isEnoughForRace)
		{
			GUI_shop.instance.ShowBuyCanvasWindow();
			yield break;
		}
		Audio.Play("fuel-1");
		gui.interface_CarInfoBars.rocketsLabel.StopCheng = true;
		gui.interface_CarInfoBars.rubiesLabel.StopCheng = true;
		car.StopUnstopCar(stop: true);
		car.HealthModule.AnDeath = true;
		PauseDeActivate = true;
		gui.windows_pause.PlayAnimHide();
		float t2 = 0f;
		while (t2 > 0f)
		{
			t2 -= 0.018f;
			yield return null;
		}
		Game.OnStateChange(Game.gameState.PrevState);
		t2 = 2f;
		while (t2 > 0f)
		{
			t2 -= 0.018f;
			yield return null;
		}
		PauseActivate = false;
		PauseDeActivate = false;
		if (Progress.shop.endlessLevel)
		{
			PriceConfig.instance.energy.eachStart = 10;
		}
		if (Progress.shop.EsterLevelPlay)
		{
			gui.windows_pause.Hide();
			gui.interface_Controlls.Show();
			gui.interface_CarInfoBars.Show();
			if (Progress.shop.endlessLevel)
			{
				GameEnergyLogic.instance.energyConfig.eachStart = 10;
			}
			gui.interface_Boosters.HideRestoreBoost();
			car.HealthModule.AnDeath = true;
			Restart();
		}
		else if (!GameEnergyLogic.isEnoughForRace)
		{
			GUI_shop.instance.ShowBuyCanvasWindow();
		}
		else
		{
			gui.windows_pause.Hide();
			gui.interface_Controlls.Show();
			gui.interface_CarInfoBars.Show();
			if (Progress.shop.endlessLevel)
			{
				GameEnergyLogic.instance.energyConfig.eachStart = 10;
			}
			gui.interface_Boosters.HideRestoreBoost();
			car.HealthModule.AnDeath = true;
			Restart();
		}
	}

	private IEnumerator delayPauseStart()
	{
		float t = 1f;
		while (t > 0f)
		{
			t -= 0.018f;
			yield return null;
		}
		PauseDeActivate = false;
	}

	public void ResumeRace()
	{
		if (!PauseDeActivate)
		{
			StartCoroutine(delayPauseOff());
		}
	}

	private IEnumerator delayPauseOff()
	{
		PauseDeActivate = true;
		gui.windows_pause.PlayAnimHide();
		float t2 = 1.5f;
		while (t2 > 0f)
		{
			t2 -= 0.018f;
			yield return null;
		}
		Game.OnStateChange(Game.gameState.PrevState);
		t2 = 0.5f;
		while (t2 > 0f)
		{
			t2 -= 0.018f;
			yield return null;
		}
		gui.windows_pause.Hide();
		PauseDeActivate = false;
		PauseActivate = false;
	}

	private void ShowPreRaceBoosts()
	{
		float maxValue = car.TurboModule._Barrel.MaxValue;
		float num = car.HealthModule._Barrel.MaxValue * 2f;
		int num2 = 3;
		bool turboPurchased = false;
		bool healthPurchased = false;
		int coins = Progress.shop.currency;
		int healthBost = Progress.shop.healthBost;
		int turboBoost = Progress.shop.turboBoost;
		int restoreBoost = Progress.shop.restoreBoost;
		if (PlayerPrefs.GetInt("tutorial_boosters", 0) != 2 && Progress.levels.active_level > 1)
		{
			PlayerPrefs.SetInt("tutorial_boosters", 1);
			PlayerPrefs.SetInt("tutorial_revive", 1);
			Progress.shop.turboBoost++;
			Progress.shop.healthBost++;
			healthBost++;
			turboBoost++;
			restoreBoost++;
		}
		Action action = delegate
		{
			if (turboBoost <= 0)
			{
				Audio.PlayAsync("gui_boosters_activation");
				turboPurchased = true;
				gui.interface_Boosters.RefreshPreRaceBoosts(coins, turboBoost, isTurboShow: true, healthBost, isHealthShow: true, turboPurchased, healthPurchased);
				car.AddExtraTurbo();
				gui.interface_CarInfoBars.ChangeTurbo();
			}
			else
			{
				if (turboBoost > 0)
				{
					turboBoost--;
					Progress.shop.turboBoost--;
				}
				Audio.PlayAsync("gui_boosters_activation");
				turboPurchased = true;
				gui.interface_Boosters.RefreshPreRaceBoosts(coins, turboBoost, isTurboShow: true, healthBost, isHealthShow: true, turboPurchased, healthPurchased);
				car.AddExtraTurbo();
				gui.interface_CarInfoBars.ChangeTurbo();
			}
		};
		Action action2 = delegate
		{
			if (healthBost <= 0)
			{
				Audio.PlayAsync("gui_boosters_activation");
				healthPurchased = true;
				gui.interface_Boosters.RefreshPreRaceBoosts(coins, turboBoost, isTurboShow: true, healthBost, isHealthShow: true, turboPurchased, healthPurchased);
				car.AddExtraHealth();
				gui.interface_CarInfoBars.ChangeHealth();
			}
			else
			{
				if (healthBost > 0)
				{
					Progress.shop.healthBost--;
					healthBost--;
				}
				Audio.PlayAsync("revive");
				healthPurchased = true;
				gui.interface_Boosters.RefreshPreRaceBoosts(coins, turboBoost, isTurboShow: true, healthBost, isHealthShow: true, turboPurchased, healthPurchased);
				car.AddExtraHealth();
				gui.interface_CarInfoBars.ChangeHealth();
			}
		};
		Action action3 = delegate
		{
			Progress.shop.currency = coins;
			Progress.shop.healthBost = healthBost;
			Progress.shop.turboBoost = turboBoost;
			Progress.shop.restoreBoost = restoreBoost;
			Progress.Save(Progress.SaveType.Shop);
		};
	}

	private void Sucsessss()
	{
		gui.interface_Boosters._BTN.interactable = true;
		PauseVideo = true;
		AnalyticsManager.LogEvent(EventCategoty.video, "ads_watched", "revive");
		gui.interface_Boosters.RefreshRestoreBoost(Progress.shop.currency, Progress.shop.restoreBoost, isHealthRestore: true, isHealthRestorePurchased: true);
		if (Progress.shop.endlessLevel)
		{
			if (TimeDieSpecMissionsForMee <= 1f)
			{
				TimeToDieCorut = StartCoroutine(timeToDie(30f));
				if (Convoi != null)
				{
					Car2DAIController component = Convoi.GetComponent<Car2DAIController>();
					component.AnDeathConvoi = false;
				}
			}
			else
			{
				TimeDieSpecMissions += 30f;
			}
		}
		GameContinue();
		FortuneVideo = false;
		rewiveVideo = false;
	}

	private void NoSucses()
	{
		PauseVideo = true;
		GameOver();
		gui.interface_Boosters._BTN.interactable = true;
		FortuneVideo = false;
		rewiveVideo = false;
	}

	private void NoVideo()
	{
		if (!_noVideoShowed)
		{
			_noVideoShowed = true;
			PauseVideo = true;
			GameOver();
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("noVideoBox", typeof(GameObject))) as GameObject;
			FortuneVideo = false;
			rewiveVideo = false;
		}
	}

	public void forune_video(Action sucses_true, Action NoVideo, Action NoSuccsec)
	{
		if (sucses_true != null)
		{
			Fort_video_suc = sucses_true;
		}
		if (NoVideo != null)
		{
			Fort_video_novideo = NoVideo;
		}
		if (NoSuccsec != null)
		{
			Fort_video_nosuc = NoSuccsec;
		}
		FortuneVideo = true;
		rewiveVideo = false;
		NewWrapperForAppodeal.instance.ShowVideo(delegate(bool sucess)
		{
			if (sucess)
			{
				Fort_video_suc();
			}
			else
			{
				Fort_video_nosuc();
			}
		}, delegate
		{
			Fort_video_novideo();
		}, delegate
		{
			Fort_video_nosuc();
		});
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

	private void ShowRestoreBoost()
	{
		Game.OnStateChange(Game.gameState.Revive);
		gui.interface_Controlls.topLeftBtn.SetActive(value: false);
		Progress.Shop shop = Progress.shop;
		bool healthRestorePurchased = false;
		int price = Utilities.LevelNumberGlobal(level, pack) * 100;
		Action onClick = delegate
		{
			if (shop.restoreBoost <= 0)
			{
				healthRestorePurchased = true;
				isRestoreBoostWasShowed = true;
				gui.interface_Boosters._BTN.interactable = false;
				NewWrapperForAppodeal.instance.ShowVideo(delegate(bool sucess)
				{
					if (sucess)
					{
						UnityEngine.Debug.Log("video sucsess");
						Sucsessss();
					}
					else
					{
						UnityEngine.Debug.Log("video no sucsess");
						NoSucses();
					}
				}, delegate
				{
					UnityEngine.Debug.Log("video no video");
					NoVideo();
				}, delegate
				{
					UnityEngine.Debug.Log("video no sucsess");
					NoSucses();
				});
				FortuneVideo = false;
				rewiveVideo = true;
				PauseVideo = true;
			}
			else
			{
				if (shop.restoreBoost > 0)
				{
					shop.restoreBoost--;
				}
				Audio.Play("revive");
				healthRestorePurchased = true;
				isRestoreBoostWasShowed = true;
				gui.interface_Boosters.RefreshRestoreBoost(shop.currency, shop.restoreBoost, isHealthRestore: true, healthRestorePurchased);
				Progress.shop = shop;
				GameContinue();
			}
		};
		Action onHideCallback = delegate
		{
			if (!healthRestorePurchased && !isCoinsBuyMenuShowed)
			{
				GameOver();
			}
		};
		gui.interface_Boosters.ShowRestoreBoost(shop.currency, shop.restoreBoost, price, 5f, onClick, onHideCallback);
	}

	private void OnDestroy()
	{
		if (Game.currentState != Game.gameState.Shop)
		{
			Audio.StopBackgroundMusic();
		}
		_instance = null;
	}

	private void StartRace()
	{
		BossDeath = false;
		if (preloader == null)
		{
			GameObject gameObject = GameObject.Find("Preloader");
			if (gameObject != null)
			{
				preloader = gameObject.GetComponent<PreloaderCanvas>();
			}
		}
		if (preloader.Zvantaj.text.Contains("100"))
		{
			preloader.Zvantaj.text = "0%";
		}
		Transform transform = Camera.main.transform;
		Vector3 position = Camera.main.transform.position;
		float x = position.x;
		Vector3 position2 = Camera.main.transform.position;
		transform.position = new Vector3(x, position2.y, -40f);
		gui.interface_Boosters.startRace = true;
		Pool.instance.Stop();
		SuperBonusesManager.instance.Stop();
		gui.interface_Controlls.SetTurboButtonState(enabled: false);
		gui.interface_Controlls.SetBombButtonState(enabled: false);
		if (NonInitedAI.Count > 0)
		{
			for (int i = 0; i < NonInitedAI.Count; i++)
			{
				NonInitedAI[i]();
			}
			NonInitedAI.Clear();
		}
		car.isKinematic = false;
		car.EngineModule.Break(onoff: true);
		car.Enabled = false;
		follow.StartFollow(car.transform);
		if (Dron != null)
		{
			UnityEngine.Object.Destroy(Dron);
		}
		if (Dron2 != null)
		{
			UnityEngine.Object.Destroy(Dron2);
		}
		if (Dron3 != null)
		{
			UnityEngine.Object.Destroy(Dron3);
		}
		if (Progress.shop.dronBombsActive)
		{
			Dron = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("PC_drone"));
		}
		if (Progress.shop.dronBeeActive)
		{
			Dron2 = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("PC_drone_beeranha"));
		}
		if (Progress.shop.dronFireActive)
		{
			Dron3 = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("PC_drone_firefly"));
		}
		gui.interface_CarInfoBars.SetBombIcon(Progress.shop.Car.bombActLev);
		StartCoroutine(WaitForStart());
	}

	public void OnBossFinish()
	{
		OnFinish();
	}

	private IEnumerator WaitForStart()
	{
		gui.distObj.SetActive(value: false);
		gui.interface_Boosters.HideRestoreBoost();
		if (Progress.shop.endlessLevel || Progress.shop.ArenaNew || Progress.shop.EsterLevelPlay)
		{
			gui.BadgesObj.gameObject.SetActive(value: false);
		}
		Progress.shop.ArenaBrifOpen = false;
		gui.GloabalAnim.SetTrigger(hash_NeedForExit);
		gui.GloabalAnim.SetBool(hash_IsON, value: false);
		Progress.fortune.GOGOGOGOGOGO = false;
		Progress.shop.restoreBoost = 0;
		gui.interface_R_G_C.OnEnable();
		Audio.StopBackgroundMusic();
		gui.interface_Boosters.Timer_for_revive.gameObject.SetActive(value: false);
		if (!Progress.shop.Tutorial && !Progress.shop.EsterLevelPlay)
		{
			SceneManager.LoadScene("wheelOfFortune_NEW", LoadSceneMode.Additive);
		}
		car.SuspensionModule.Hubs.Clear();
		car.wheelsrigitbodyes = null;
		car.gameObject.transform.localScale = Vector3.one * 3f;
		Vector3 position = car.transform.position;
		Coroutine corut = StartCoroutine(StopCar(position.x));
		SuperBonusesManager.instance.damageX2.time = 10f;
		SuperBonusesManager.instance.damageX2.gameObject.SetActive(value: false);
		SuperBonusesManager.instance.BcPcB.gameObject.SetActive(value: false);
		Progress.settings.x2damage = false;
		Progress.settings.ReduceDamage = false;
		car.x2damageeff.SetActive(value: false);
		car.redusedamageeff.SetActive(value: false);
		car.SuspensionModule.Init(car.GetComponent<Rigidbody2D>(), car.WheelsRigitbodies());
		car.GetModulesReferences(Progress.shop.activeCar);
		car.SetCarUpgrades(Progress.shop.Car);
		car.EngineModule.Init(car.WheelsRigitbodies());
		if (Progress.shop.activeCar != 4)
		{
			StartCoroutine(car.OnOFFLimits());
		}
		Rigidbody2D _rb = instance.car.GetComponent<Rigidbody2D>();
		if ((bool)_rb)
		{
			_rb.constraints = RigidbodyConstraints2D.None;
		}
		car.EngineModule.Break(onoff: true);
		for (int i = 0; i < gui.markersss.Count; i++)
		{
			gui.markersss[i].SetActive(value: true);
		}
		yield return 0;
		yield return 0;
		yield return 0;
		for (int j = 0; j < gui.markersss.Count; j++)
		{
			gui.markersss[j].SetActive(value: false);
		}
		if (Progress.shop.Tutorial)
		{
			car.TurboModule._barrel.Value = 0f;
			Progress.shop.TutorialBadgeNeed = true;
			Screen.sleepTimeout = -1;
		}
		if (preloader != null)
		{
			yield return new WaitForSeconds(0.5f);
			preloader.Zvantaj.text = "100 %";
			yield return new WaitForSeconds(0.5f);
			preloader.gameObject.SetActive(value: false);
		}
		else
		{
			yield return null;
		}
		if (Progress.shop.Tutorial)
		{
			gui.THUD.controlls.SetActive(value: true);
			gui.THUD.i = 0;
		}
		startsss = GameObject.Find("Start");
		yield return 0;
		follow.StartFollow(startsss.transform);
		follow.StopCam();
		follow.Stop();
		Audio.StopBackgroundMusic();
		Audio.PlayBackgroundMusic("music_fortune2");
		if (Progress.shop.EsterLevelPlay)
		{
			Progress.fortune.GOGOGOGOGOGO = true;
		}
		while (!Progress.fortune.GOGOGOGOGOGO)
		{
			Game.OnStateChange(Game.gameState.PreRace);
			Game.currentState = Game.gameState.PreRace;
			gui.interface_CarInfoBars.Hide();
			gui.interface_Controlls.topLeftBtn.SetActive(value: false);
			car.EngineModule.Break(onoff: true);
			yield return null;
		}
		follow.StartFollow(instance.car.transform);
		follow.StartCam();
		Audio.StopBackgroundMusic();
		Audio.Stop("fortune2");
		Audio.Stop("fortune3");
		Audio.Stop("fortune");
		gui.GloabalAnim.SetBool(hash_IsON, value: true);
		yield return new WaitForSeconds(0.5f);
		RectTransform rt = gui.interface_CarInfoBars.healthBar.GetComponent<RectTransform>();
		RectTransform rectTransform = rt;
		float x = 140f + Progress.fortune.SumPercentHP;
		Vector2 sizeDelta = rt.sizeDelta;
		rectTransform.sizeDelta = new Vector2(x, sizeDelta.y);
		RectTransform rtz = gui.interface_CarInfoBars.turboBar.GetComponent<RectTransform>();
		RectTransform rectTransform2 = rtz;
		float x2 = 140f + Progress.fortune.SumPercentTurbo;
		Vector2 sizeDelta2 = rtz.sizeDelta;
		rectTransform2.sizeDelta = new Vector2(x2, sizeDelta2.y);
		gui.interface_CarInfoBars.TurboText.count = Progress.fortune.SumPercentTurbo.ToString();
		gui.interface_CarInfoBars.HealthText.count = Progress.fortune.SumPercentHP.ToString();
		gui.interface_CarInfoBars.bombAnimator.SetBool("isActivate", value: false);
		gui.interface_CarInfoBars.HealthAnimator.SetBool("isActive", value: false);
		gui.interface_CarInfoBars.TurboAnimator.SetBool("isActive", value: false);
		car.BombModule.moduleEnabled = true;
		if (Progress.fortune.SumBombStart > 0)
		{
			gui.interface_CarInfoBars.bombText.count = Progress.fortune.SumBombStart.ToString();
			gui.interface_CarInfoBars.bombAnimator.SetBool("isActivate", value: true);
		}
		if (Progress.fortune.SumBombStart > 0)
		{
			car.BombModule.Increase(Progress.fortune.SumBombStart);
		}
		if (Progress.fortune.Dirka == 3)
		{
			car.BombModule._barrel.Enable = true;
			car.BombModule._barrel.MaxValue = 999f;
			car.BombModule._barrel.Value = 999f;
			car.BombModule._barrel.Restore = true;
			Progress.fortune.Dirka = 0;
		}
		else
		{
			Progress.fortune.Dirka = 0;
			car.BombModule._barrel.Restore = false;
		}
		if (Progress.fortune.SumPercentHP > 0f)
		{
			gui.interface_CarInfoBars.HealthAnimator.SetBool("isActive", value: true);
		}
		car.HealthModule._barrel.MaxValue = car.HealthModule._barrel.MaxValue + Progress.fortune.SumPercentHP / 100f * car.HealthModule._barrel.MaxValue;
		car.HealthModule._barrel.Value = car.HealthModule._barrel.MaxValue;
		if (Progress.fortune.SumPercentHP > 0f)
		{
			gui.interface_CarInfoBars.ChangeHealth();
		}
		if (Progress.fortune.SumPercentTurbo > 0f)
		{
			gui.interface_CarInfoBars.TurboAnimator.SetBool("isActive", value: true);
		}
		car.TurboModule._barrel.MaxValue = car.TurboModule._barrel.MaxValue + Progress.fortune.SumPercentTurbo / 100f * car.TurboModule._barrel.MaxValue;
		car.TurboModule._barrel.Value = car.TurboModule._barrel.MaxValue;
		if (Progress.fortune.SumPercentTurbo > 0f)
		{
			gui.interface_CarInfoBars.ChangeTurbo();
		}
		if (Progress.shop.endlessLevel)
		{
			float increment = 6f;
			GameObject folow = new GameObject("folow");
			folow.transform.position = car.transform.position;
			follow.StartFollow(folow.transform);
			Rigidbody2D rb2dfolow = folow.AddComponent<Rigidbody2D>();
			rb2dfolow.isKinematic = true;
			Car2DAIController c2dac = Convoi.GetComponent<Car2DAIController>();
			c2dac.isKinematic = false;
			car.isKinematic = true;
			while (increment > 0f)
			{
				increment -= Time.deltaTime;
				yield return null;
				if (increment > 3f)
				{
					car.gameObject.transform.position = car.gameObject.transform.position + new Vector3(0f, 6f * Time.deltaTime);
					Transform transform = folow.transform;
					Vector3 position2 = folow.transform.position;
					Vector3 position3 = Convoi.transform.position;
					float x3 = position3.x;
					Vector3 position4 = Convoi.transform.position;
					transform.position = Vector3.MoveTowards(position2, new Vector3(x3, position4.y), Time.deltaTime * 70f);
					follow.StartFollow(folow.transform);
					car.isKinematic = true;
				}
				else
				{
					Transform transform2 = folow.transform;
					Vector3 position5 = folow.transform.position;
					Vector3 position6 = car.transform.position;
					float x4 = position6.x;
					Vector3 position7 = car.transform.position;
					transform2.position = Vector3.MoveTowards(position5, new Vector3(x4, position7.y), Time.deltaTime * 70f);
					car.isKinematic = false;
				}
			}
			car.isKinematic = false;
			follow.StartFollow(car.transform);
		}
		gui.interface_Boosters.startRace = false;
		while (Game.currentState != Game.gameState.PreRace)
		{
			yield return null;
		}
		if (pack == 2)
		{
			Audio.PlayBackgroundMusic("music_gameplay2");
		}
		else
		{
			Audio.PlayBackgroundMusic("music_gameplay2");
		}
		race.StartRace();
		Audio.Play("gfx_gamestart_02_sn");
		Game.OnStateChange(Game.gameState.Race);
		if (Progress.shop.Tutorial)
		{
			car.TurboModule._barrel.Value = 0f;
		}
		AllInitedForPool = true;
		gui.interface_StartCouner.StartCounting(Utilities.LevelNumberGlobal(level, pack), 3, delegate
		{
			StopCoroutine(corut);
			follow.StartFollow(car.transform);
			race.StartRace();
			car.Enabled = true;
			car.EngineModule.Break(onoff: false);
			Time.timeScale = 1f;
			Audio.Play("gfx_gamestart_01_sn");
			GUIPositionBar interface_PositionBar = gui.interface_PositionBar;
			Transform transform3 = car.transform;
			Vector3 position8 = race.start.position;
			float x5 = position8.x;
			Vector3 position9 = race.finish.position;
			interface_PositionBar.SetProgressBar(transform3, x5, position9.x);
			car.EngineModule.Break(onoff: false);
			if (Progress.shop.endlessLevel)
			{
				TimeToDieCorut = StartCoroutine(timeToDie());
			}
			instance.gui.interface_Controlls.topLeftBtn.SetActive(value: true);
			car.BombModule._barrel.enabled = true;
		});
	}

	private IEnumerator StopCar(float tX)
	{
		car.EngineModule.Break(onoff: true);
		while (true)
		{
			Transform transform = car.transform;
			Vector3 position = car.transform.position;
			float y = position.y;
			Vector3 position2 = car.transform.position;
			transform.position = new Vector3(tX, y, position2.z);
			car.gameObject.transform.eulerAngles = Vector3.zero;
			for (int i = 0; i < car.WheelsTransforms().Count; i++)
			{
				car.WheelsTransforms()[i].transform.eulerAngles = Vector3.zero;
			}
			yield return null;
		}
	}

	private IEnumerator timeToDie(float timeDieSpecMissions = 0f)
	{
		float i = 0f;
		gui.MissionsTimeObj.gameObject.SetActive(value: true);
		gui.distObj.SetActive(value: true);
		float _count = 0f;
		string empty = string.Empty;
		TimeDieSpecMissionsForMee = timeDieSpecMissions;
		if (TimeDieSpecMissionsForMee == 0f)
		{
			TimeDieSpecMissions = TimeToDie.instance.timeToDie.TimeToDie[Progress.shop.SpecialMissionsRewardCar - 1].TimeDie;
		}
		else
		{
			TimeDieSpecMissions = TimeDieSpecMissionsForMee;
		}
		bool endSoundPlay = false;
		string zero = "0";
		string dot = ".";
		int lastSec = -1;
		while (i < TimeDieSpecMissions)
		{
			TimeDieSpecMissionsForMee = _count;
			_count = TimeDieSpecMissions - i;
			int min = (int)(_count / 60f);
			int sec = (int)(_count - (float)(min * 60));
			if (min == 0 && sec == 2 && !endSoundPlay)
			{
				endSoundPlay = true;
				if (Game.currentState != Game.gameState.Finish)
				{
					Audio.PlayAsync("airstrike_alarm", 1.5f);
				}
				if (Convoi != null)
				{
					Car2DAIController component = Convoi.GetComponent<Car2DAIController>();
					component.AnDeathConvoi = true;
				}
			}
			if (lastSec != sec)
			{
				lastSec = sec;
				string _countStr2 = (min > 9) ? (min.ToString() + dot) : (zero + min.ToString() + dot);
				_countStr2 = ((sec > 9) ? (_countStr2 + sec.ToString()) : (_countStr2 + zero + sec.ToString()));
				gui.MissionsTime.count = _countStr2;
			}
			i += Time.deltaTime;
			if (Game.currentState == Game.gameState.Finish)
			{
				gui.MissionsTimeObj.gameObject.SetActive(value: false);
			}
			yield return 0;
		}
		if (Game.currentState != Game.gameState.Finish)
		{
			car.EngineModule.Break(onoff: true);
			Pool instance = Pool.instance;
			string name = Pool.Name(Pool.Bonus.enemyairstrike);
			Vector3 position = car.gameObject.transform.position;
			float x = position.x - 40f;
			Vector3 position2 = car.gameObject.transform.position;
			instance.spawnAtPoint(name, new Vector3(x, position2.y + 8.5f, 0f));
		}
	}

	private void NextLevel()
	{
		LoadLevel(level + 1);
	}

	public void Restart()
	{
		car.HealthModule.AnDeath = false;
		if (OnDieCoroutine != null)
		{
			StopCoroutine(OnDieCoroutine);
			OnDieCoroutine = null;
		}
		CounterEmemys = 0;
		LastCounterEmemys = -1;
		CurrentDifLevel = 0;
		gui.RestartOn();
		car.gameObject.SetActive(value: false);
		car.gameObject.SetActive(value: true);
		LoadLevel(level);
		car.HealthModule.AnDeath = false;
	}

	private void LoadLevel(int _level)
	{
		if (_level > 13)
		{
			if (_level < 12 || pack != 4)
			{
				pack++;
				GameEnergyLogic.GetFuelForRace();
				level = _level;
				level = 1;
				Progress.SetActiveLevel((byte)level);
				Audio.Stop();
				follow.Stop();
				Progress.levels.active_pack = (byte)pack;
				Progress.levels.active_level = (byte)level;
				UnityEngine.SceneManagement.SceneManager.LoadScene("Race_preloader");
				GameEnergyLogic.instance.energyConfig.eachStart = 5;
				return;
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
		else
		{
			GameEnergyLogic.GetFuelForRace();
			level = _level;
			Progress.SetActiveLevel((byte)level);
			Audio.Stop();
			follow.Stop();
			CheckCar();
			follow.transform.position = race.start.position - new Vector3(0f, 0f, 10f);
			race.Reset();
			gui.interface_CarInfoBars.SetRubins(0);
			Progress.levels.active_pack = (byte)pack;
			Progress.levels.active_level = (byte)level;
			if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Race_preloader")
			{
				Progress.levels.active_pack = (byte)pack;
				Progress.levels.active_level = (byte)level;
				Game.LoadLevel("Race_preloader");
			}
			else
			{
				LevelBuilder.LoadLevel(level, pack);
			}
			car.transform.position = race.start.position;
			TutorialRace.ReInit();
			preloader.gameObject.SetActive(value: true);
			car.Restart(Progress.shop.Car);
			isRestoreBoostWasShowed = false;
			SpaceTrashGenerator.instance.RefreshTrash();
			ParallaxSystem componentInChildren = Camera.main.GetComponentInChildren<ParallaxSystem>();
			ParallaxSystem.Create(Camera.main.transform, pack);
			SpaceTrashGenerator.instance.Target = Camera.main.transform;
			UnityEngine.Object.Destroy(componentInChildren.gameObject);
			StartRace();
			GameEnergyLogic.instance.energyConfig.eachStart = 5;
		}
		gui.interface_CarInfoBars.rocketsLabel.StopCheng = false;
		gui.interface_CarInfoBars.rubiesLabel.StopCheng = false;
	}

	private void CheckCar()
	{
		if (Progress.shop.activeCar == carNum)
		{
			return;
		}
		if (!Cars.Contains(car))
		{
			Cars.Add(car);
		}
		car.gameObject.SetActive(value: false);
		UnityEngine.Object.Destroy(car.gameObject);
		if (!car.gameObject.activeSelf)
		{
			if (Progress.shop.activeCar == 0)
			{
				car = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("PC_cars_new"))).GetComponent<Car2DController>();
			}
			else if (Progress.shop.activeCar == 1)
			{
				car = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("PC_car_2"))).GetComponent<Car2DController>();
			}
			else if (Progress.shop.activeCar == 2)
			{
				car = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("PC_car_3"))).GetComponent<Car2DController>();
			}
			else if (Progress.shop.activeCar == 3)
			{
				car = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("PC_car_4"))).GetComponent<Car2DController>();
			}
			else if (Progress.shop.activeCar == 4)
			{
				car = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("PC_car_5"))).GetComponent<Car2DController>();
			}
			else if (Progress.shop.activeCar == 5)
			{
				car = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("PC_car_07"))).GetComponent<Car2DController>();
			}
			else if (Progress.shop.activeCar == 6)
			{
				car = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("PC_car_police1"))).GetComponent<Car2DController>();
			}
			else if (Progress.shop.activeCar == 7)
			{
				car = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("PC_car_police2"))).GetComponent<Car2DController>();
			}
			else if (Progress.shop.activeCar == 8)
			{
				car = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("PC_car_08"))).GetComponent<Car2DController>();
			}
			else if (Progress.shop.activeCar == 9)
			{
				car = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("PC_car_underground_02"))).GetComponent<Car2DController>();
			}
			else if (Progress.shop.activeCar == 10)
			{
				car = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("PC_car_underground_01"))).GetComponent<Car2DController>();
			}
			else if (Progress.shop.activeCar == 11)
			{
				car = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("PC_car_police3"))).GetComponent<Car2DController>();
			}
			else if (Progress.shop.activeCar == 12)
			{
				car = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("PC_car_underground_05"))).GetComponent<Car2DController>();
			}
			else if (Progress.shop.activeCar == 13)
			{
				car = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("PC_car_underground_06"))).GetComponent<Car2DController>();
			}
			Progress.shop.Car.id = Progress.shop.activeCar;
			car.Init(Progress.shop.Car);
			Cars.Add(car);
			car.OnDie += OnDie;
		}
		gui.interface_CarInfoBars.ChangeRocketCount(car.BombModule._Barrel.Value);
		car.OnHeathChanged += delegate(float v)
		{
			gui.interface_CarInfoBars.ChangeHealthBar(v);
		};
		car.OnTurboChanged += delegate(float v)
		{
			gui.interface_CarInfoBars.ChangeTurboBar(v);
		};
		car.OnWeaponChanged += delegate(float v)
		{
			gui.interface_CarInfoBars.ChangeRocketCount(v);
		};
		car.OnBoostHealthChanged += delegate(float v)
		{
			gui.interface_CarInfoBars.ChangeBoostHealthBar(v);
		};
		car.OnBoostTurboChanged += delegate(float v)
		{
			gui.interface_CarInfoBars.ChangeBoostTurboBar(v);
		};
		car.SetCallbackControll(delegate(bool b)
		{
			gui.interface_Controlls.SetTurboButtonState(b);
		}, delegate(bool b)
		{
			gui.interface_Controlls.SetBombButtonState(b);
		});
		race.Init(car.transform, race.start, race.finish);
		carNum = Progress.shop.activeCar;
	}

	private void OnDie()
	{
		chekSparkl = false;
		if (OnDieCoroutine != null)
		{
			StopCoroutine(OnDieCoroutine);
		}
		if (isRestoreBoostWasShowed)
		{
			OnDieCoroutine = Utilities.instance.RunAction(2f, GameOver);
		}
		else
		{
			Game.OnStateChange(Game.gameState.Revive);
			OnDieCoroutine = Utilities.instance.RunAction(2f, ShowRestoreBoost);
		}
		StartCoroutine(OnDieCoroutine);
	}

	private void GameContinue()
	{
		StartCoroutine(Continue());
	}

	private IEnumerator Continue()
	{
		float time = 0f;
		if (deathInMarker)
		{
			for (; time < 1.5f; time += Time.deltaTime)
			{
				Transform transform = car.gameObject.transform;
				Vector3 position = car.gameObject.transform.position;
				Vector3 position2 = car.gameObject.transform.position;
				float x = position2.x + goleftonmarker * 2f;
				Vector3 position3 = car.gameObject.transform.position;
				transform.position = Vector3.Lerp(position, new Vector3(x, position3.y + 25f), time * Time.deltaTime);
			}
		}
		else
		{
			for (; time < 1.5f; time += Time.deltaTime)
			{
				Transform transform2 = car.gameObject.transform;
				Vector3 position4 = car.gameObject.transform.position;
				Vector3 position5 = car.gameObject.transform.position;
				float x2 = position5.x;
				Vector3 position6 = car.gameObject.transform.position;
				transform2.position = Vector3.Lerp(position4, new Vector3(x2, position6.y + 4f), time * Time.deltaTime);
			}
		}
		ToGround(car.gameObject, 1);
		Game.OnStateChange(Game.gameState.Race);
		gui.interface_Controlls.topLeftBtn.SetActive(value: true);
		chekSparkl = true;
		car.gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
		Vector3 position7 = race.finish.position;
		float x3 = position7.x;
		Vector3 position8 = car.gameObject.transform.position;
		if (x3 < position8.x)
		{
			Transform transform3 = car.gameObject.transform;
			Vector3 position9 = race.finish.position;
			float x4 = position9.x - 3f;
			Vector3 position10 = car.gameObject.transform.position;
			transform3.position = new Vector3(x4, position10.y);
		}
		animatorChangeStateAfterTime[] q = UnityEngine.Object.FindObjectsOfType<animatorChangeStateAfterTime>();
		if (q.Length > 0)
		{
			animatorChangeStateAfterTime[] array = q;
			foreach (animatorChangeStateAfterTime animatorChangeStateAfterTime in array)
			{
				animatorChangeStateAfterTime.gameObject.SetActive(value: false);
			}
		}
		Vector3 position11 = car.gameObject.transform.position;
		float x5 = position11.x;
		Vector3 position12 = car.gameObject.transform.position;
		GameObject Go = Pool.GameOBJECT(Pool.Bonus.portal, new Vector3(x5, position12.y + 4f));
		StartCoroutine(destroyObjAnterTime(Go, 10f));
		ToGround(Go, 10);
		car.gameObject.transform.position = Go.transform.position;
		yield return new WaitForSeconds(2.5f);
		car.Respawn(Progress.shop.Car);
	}

	private void ToGround(GameObject GO, int nadGround)
	{
		int num = 0;
		num = ((!Progress.shop.TestFor9) ? 10 : 3);
		Vector2 origin = GO.transform.position + num * Vector3.up - Vector3.right;
		RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, -Vector2.up, float.MaxValue);
		Vector2 origin2 = GO.transform.position + num * Vector3.up + Vector3.right;
		RaycastHit2D raycastHit2D2 = Physics2D.Raycast(origin2, -Vector2.up, float.MaxValue);
		Vector2 point = raycastHit2D2.point;
		float x = point.x;
		Vector2 point2 = raycastHit2D.point;
		float x2 = x - point2.x;
		Vector2 point3 = raycastHit2D2.point;
		float y = point3.y;
		Vector2 point4 = raycastHit2D.point;
		Vector2 from = new Vector2(x2, y - point4.y);
		float num2 = Vector2.Angle(from, Vector2.right);
		if (from.y < 0f)
		{
			num2 = 360f - num2;
		}
		Vector2 point5 = raycastHit2D.point;
		float y2 = point5.y;
		Vector2 point6 = raycastHit2D2.point;
		float num3 = Mathf.Max(y2, point6.y) + (float)nadGround;
		Transform transform = GO.transform;
		Vector3 position = GO.transform.position;
		float x3 = position.x;
		float y3 = num3;
		Vector3 position2 = GO.transform.position;
		transform.position = new Vector3(x3, y3, position2.z);
	}

	private IEnumerator destroyObjAnterTime(GameObject obj, float time)
	{
		yield return new WaitForSeconds(time);
		obj.SetActive(value: false);
	}

	public void GameOver()
	{
		Game.OnStateChange(Game.gameState.FinishLose);
		race.OnLose();
		Audio.StopBackgroundMusic();
		if (Progress.shop.EsterLevelPlay)
		{
			Progress.shop.EsterEggsBalance += race.Rubins + race.RubinsAI;
			if (Progress.shop.EsterX2TimeActivate)
			{
				Progress.shop.EsterEggsBalance += race.Rubins + race.RubinsAI;
			}
		}
		else
		{
			Progress.shop.currency += race.Rubins + race.RubinsAI;
		}
		gui.interface_CarInfoBars.SetRubins(race.Rubins + race.RubinsAI);
		Progress.Save(Progress.SaveType.Shop);
		GameBase.TogglePause(pause: true);
		Time.timeScale = 1f;
		StartCoroutine(GAmeOver_finish());
		GameCenter.CheckAllAchievements();
	}

	private IEnumerator GAmeOver_finish()
	{
		if (Progress.shop.EsterLevelPlay)
		{
			StartCoroutine(res());
			yield break;
		}
		if (Progress.shop.ArenaNew)
		{
			int active_pack = Progress.levels.active_pack;
			int num = 0;
			switch (active_pack)
			{
			case 1:
				num = DifficultyConfig.instance.MetrivForARENA1;
				break;
			case 2:
				num = DifficultyConfig.instance.MetrivForARENA2;
				break;
			case 3:
				num = DifficultyConfig.instance.MetrivForARENA3;
				break;
			}
			if ((active_pack == 1 && !Progress.shop.Key1 && Progress.shop.Arena1Distance >= num) || (active_pack == 2 && !Progress.shop.Key2 && Progress.shop.Arena2Distance >= num) || (active_pack == 3 && !Progress.shop.Key3 && Progress.shop.Arena3Distance >= num) || (active_pack == 1 && Progress.shop.Key1 && Progress.shop.Arena1Distance > Progress.shop.Arena1MaxDistance) || (active_pack == 2 && Progress.shop.Key2 && Progress.shop.Arena2Distance > Progress.shop.Arena2MaxDistance) || (active_pack == 3 && Progress.shop.Key3 && Progress.shop.Arena3Distance > Progress.shop.Arena3MaxDistance))
			{
				StartCoroutine(res());
				yield break;
			}
		}
		gui.GloabalAnim.SetBool(hash_IsON, value: false);
		yield return new WaitForSeconds(1f);
		R_L_N_C = gui.interface_R_G_C.R_L_N_C;
		gui.interface_R_G_C.OnEnable();
		yield return new WaitForSeconds(0.2f);
		gui.gameObject.SetActive(value: false);
		gui.interface_R_G_C.StartWindow();
		R_L_N_C.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(1.5f);
		car.gameObject.SetActive(value: false);
		R_L_N_C.Results_Suma(race.Rubins, race.RubinsAI, Progress.fortune.SumPercentRuby);
	}

	private void OnFinish()
	{
		if (Game.currentState == Game.gameState.Race)
		{
			Audio.StopBackgroundMusic();
			Game.OnStateChange(Game.gameState.Finish);
			if (Audio.IsSoundPlaying("gfx_turbo_01_sn"))
			{
				Audio.Stop("gfx_turbo_01_sn");
			}
			CarDeathOnLevel = 0;
			follow.Stop();
			Utilities.instance.RunAfterTime(1f, Result);
			if (!Progress.levels.Pack(pack).Level(level).rewarded)
			{
				Progress.levels.Pack().Level().rewarded = true;
				Progress.OpenNextLevel();
			}
			Progress.SetNextLevel();
			Progress.Save(Progress.SaveType.Levels);
		}
	}

	private void Result()
	{
		if (Audio.IsSoundPlaying("gfx_turbo_01_sn"))
		{
			Audio.Stop("gfx_turbo_01_sn");
		}
		Progress.settings.FriendId = string.Empty;
		if (Progress.levels.Pack(pack).Level(level).oldticket < Progress.levels.Pack(pack).Level(level).ticket)
		{
			Progress.levels.Pack(pack).Level(level).oldticket = Progress.levels.Pack(pack).Level(level).ticket;
		}
		if (Game.currentState == Game.gameState.Finish)
		{
			car.HealthModule.AnDeath = true;
			if (!Progress.shop.endlessLevel && !Progress.shop.bossLevel)
			{
				car.HealthModule.AnDeath = false;
				car.gameObject.SetActive(value: false);
			}
			else if (Progress.shop.bossLevel)
			{
				StartCoroutine(DelayToOffCarBossLev());
			}
			StartCoroutine(res());
		}
	}

	private IEnumerator DelayToOffCarBossLev()
	{
		yield return new WaitForSeconds(3f);
		car.HealthModule.AnDeath = false;
		car.gameObject.SetActive(value: false);
	}

	private IEnumerator res()
	{
		if (Progress.shop.TestFor9)
		{
			gui.interface_R_G_C.BoxesShow();
			while (!gui.interface_R_G_C.BoxesController.BoxesAnimHiden)
			{
				yield return null;
			}
		}
		if (!Progress.shop.endlessLevel && !Progress.shop.bossLevel)
		{
			Progress.levels.CounterForWinForX2++;
		}
		gui.GloabalAnim.SetBool(hash_IsON, value: false);
		yield return new WaitForSeconds(1f);
		Res_New_Contr = gui.interface_R_G_C.R_N_C;
		gui.interface_R_G_C.OnEnable();
		int ruby = race.Rubins + race.RubinsAI + race.RubinsBonus;
		int amount = 0;
		if (Progress.fortune.SumPercentRuby > 0f)
		{
			float num = Progress.fortune.SumPercentRuby / 100f;
			amount = (int)((float)ruby * num);
		}
		int amountBadge = 0;
		int tix = Progress.levels.Pack(instance.pack).Level(instance.level).ticket;
		if (tix > 0)
		{
			float num2 = 0f;
			if (tix == 1)
			{
				num2 = 10f;
			}
			else if (tix == 2)
			{
				num2 = 25f;
			}
			else if (tix >= 3)
			{
				num2 = 50f;
			}
			num2 /= 100f;
			amountBadge = (int)((float)ruby * num2);
		}
		if (!Progress.shop.EsterLevelPlay)
		{
			Progress.shop.currency += ruby + amount + amountBadge;
		}
		if (this.OnRubinsBonusCollect != null)
		{
			this.OnRubinsBonusCollect(amount);
		}
		Progress.achievements.rubiesHighscore += ruby;
		GameCenter.SubmitScore(Progress.achievements.rubiesHighscore, Progress.achievements.CivilHighscore, Progress.achievements.enemiesDestroyed);
		GameCenter.CheckAllAchievements();
		yield return new WaitForSeconds(0.2f);
		gui.gameObject.SetActive(value: false);
		int realLastLevel = Utilities.LevelNumberGlobal(Progress.levels.Max_Active_Level, Progress.levels.Max_Active_Pack);
		if (FBLeaderboard.IsUserLoggedIn)
		{
			CEC3Score.GetInstance().PostScore(Convert.ToInt64(FBLeaderboard.CurrentUser.userID), realLastLevel);
		}
		gui.interface_R_G_C.StartWindow();
		Res_New_Contr.gameObject.SetActive(value: true);
		if (!Progress.shop.endlessLevel && !Progress.shop.bossLevel && !Progress.shop.ArenaNew && !Progress.shop.EsterLevelPlay)
		{
			Res_New_Contr.ChengBaidg();
		}
		yield return new WaitForSeconds(4f);
		Res_New_Contr.Results_Suma(race.Rubins, race.RubinsAI, Progress.fortune.SumPercentRuby);
	}

	private void SetRubins(int rubinsCount, bool saveShopProgress = false)
	{
		race.Rubins += rubinsCount;
		gui.interface_CarInfoBars.SetRubins(race.Rubins + race.RubinsAI + race.RubinsBonus);
		if (saveShopProgress)
		{
			if (!Progress.shop.EsterLevelPlay)
			{
				Progress.shop.currency += race.Rubins;
			}
			else
			{
				Progress.shop.EsterEggsBalance += race.Rubins;
			}
			Progress.Save(Progress.SaveType.Shop);
		}
	}

	private void SetRubinsAI(int rubinsCount, bool saveShopProgress = false)
	{
		race.RubinsAI += rubinsCount;
		gui.interface_CarInfoBars.SetRubins(race.Rubins + race.RubinsAI + race.RubinsBonus);
		if (saveShopProgress)
		{
			if (!Progress.shop.EsterLevelPlay)
			{
				Progress.shop.currency += race.RubinsAI;
			}
			else
			{
				Progress.shop.EsterEggsBalance += race.RubinsAI;
			}
			Progress.Save(Progress.SaveType.Shop);
		}
	}

	private void SetRubinsBonus(int rubinsCount, bool saveShopProgress = false)
	{
		race.RubinsBonus += rubinsCount;
		gui.interface_CarInfoBars.SetRubins(race.Rubins + race.RubinsAI + race.RubinsBonus);
		if (saveShopProgress)
		{
			if (!Progress.shop.EsterLevelPlay)
			{
				Progress.shop.currency += race.RubinsBonus;
			}
			else
			{
				Progress.shop.EsterEggsBalance += race.RubinsBonus;
			}
			Progress.Save(Progress.SaveType.Shop);
		}
	}

	public void CollectTicket(int posX)
	{
		if (Progress.levels.Pack().Level().tickets.Length == 0)
		{
			Progress.levels.Pack().Level().tickets += posX.ToString();
		}
		else
		{
			Progress.PackInfo.LevelInfo levelInfo = Progress.levels.Pack().Level();
			levelInfo.tickets = levelInfo.tickets + "|" + posX.ToString();
		}
		gui.interface_CarInfoBars.AddTicket();
		Audio.PlayAsync("bns_health_man_01_sn");
		Progress.levels.tickets++;
		Progress.Save(Progress.SaveType.Levels);
	}

	public void Collect(enItem item, int amount)
	{
		if (car == null)
		{
			return;
		}
		switch (item)
		{
		case enItem.Cloud:
		case enItem.Metor:
		case enItem.Freeze:
		case enItem.Enigma:
		case enItem.Ticket:
			break;
		case enItem.Health:
			car.HealthModule.IncreaseHealth(amount);
			Audio.PlayAsync("bns_health_pickup_sn");
			Pool.Animate(Pool.Bonus.health, car.ConectorEff.transform);
			gui.interface_CarInfoBars.ChangeHealth();
			break;
		case enItem.Nitro:
			car.TurboModule.Increase(amount);
			Audio.PlayAsync("bns_turbo_pickup_sn");
			Pool.Animate(Pool.Bonus.turbo, car.ConectorEff.transform);
			gui.interface_CarInfoBars.ChangeTurbo();
			break;
		case enItem.Rocket:
			Audio.PlayAsync("bns_bomb_pickup_sn");
			Pool.Animate(Pool.Bonus.bomb, car.ConectorEff.transform);
			car.BombModule.Increase(amount);
			break;
		case enItem.Rubin:
			if (car.gameObject.activeSelf)
			{
				Audio.PlayAsync("bns_rubi_pickup_sn", 0.5f);
				if (this.OnRubinsCollect != null)
				{
					this.OnRubinsCollect(amount);
				}
			}
			break;
		case enItem.Flip:
		{
			Audio.PlayAsync("bns_turbo_pickup_sn");
			Pool.Animate(Pool.Bonus.flip, car.transform);
			car.TurboModule.Increase(amount);
			GUIInterfaceMessage.Words[] array = new GUIInterfaceMessage.Words[1]
			{
				GUIInterfaceMessage.Words.wow
			};
			gui.interface_messanger.AnimateWithText(array[UnityEngine.Random.Range(0, array.Length)]);
			break;
		}
		case enItem.Copter:
			Audio.PlayAsync("superbonus");
			SuperBonusesManager.instance.ActiveBonus(SuperBonusesManager.enBonus.Copter, 1);
			gui.interface_messanger.AnimateWithText(GUIInterfaceMessage.Words.copter);
			break;
		case enItem.Zeppelin:
			Audio.PlayAsync("superbonus");
			SuperBonusesManager.instance.ActiveBonus(SuperBonusesManager.enBonus.Zeppelin, 1);
			gui.interface_messanger.AnimateWithText(GUIInterfaceMessage.Words.airstrike);
			break;
		case enItem.BombCar:
			Audio.PlayAsync("superbonus");
			SuperBonusesManager.instance.ActiveBonus(SuperBonusesManager.enBonus.BombCar, amount);
			gui.interface_messanger.AnimateWithText(GUIInterfaceMessage.Words.bombers);
			break;
		case enItem.Police:
			Audio.PlayAsync("superbonus");
			SuperBonusesManager.instance.ActiveBonus(SuperBonusesManager.enBonus.Police, amount);
			gui.StartPoliceLight();
			gui.interface_messanger.AnimateWithText(GUIInterfaceMessage.Words.police);
			break;
		case enItem.DamageX2:
			Audio.PlayAsync("superbonus");
			SuperBonusesManager.instance.ActiveBonus(SuperBonusesManager.enBonus.DamageX2, 1);
			gui.interface_messanger.AnimateWithText(GUIInterfaceMessage.Words.morepower);
			break;
		case enItem.ReduceDamage:
			Audio.PlayAsync("superbonus");
			SuperBonusesManager.instance.ActiveBonus(SuperBonusesManager.enBonus.ReduceDamage, 1);
			gui.interface_messanger.AnimateWithText(GUIInterfaceMessage.Words.lesspower);
			break;
		}
	}

	public void Collect(enItemRuby item, int amount)
	{
		if (car == null)
		{
			return;
		}
		if (item == enItemRuby.Rubin)
		{
			if (car.gameObject.activeSelf)
			{
				Audio.PlayAsync("bns_rubi_pickup_sn");
				if (this.OnRubinsCollect != null)
				{
					this.OnRubinsCollect(amount);
				}
			}
		}
		else
		{
			UnityEngine.Debug.LogWarning("Unknown CollectibleItem type.");
		}
	}

	public void onAIKilled(int reward, Car2DAIController controller = null)
	{
		if (controller != null)
		{
			if (controller.IsCivic)
			{
				if (this.OnRubinsAICollect != null)
				{
					this.OnRubinsAICollect(controller.civic.Reward);
				}
				if (EnemiesKilledPerDTCivic >= 2)
				{
					GUIInterfaceMessage.Words[] array = new GUIInterfaceMessage.Words[2]
					{
						GUIInterfaceMessage.Words.tasty,
						GUIInterfaceMessage.Words.yumyum
					};
					gui.interface_messanger.AnimateWithText(array[UnityEngine.Random.Range(0, array.Length)]);
					EnemiesKilledPerDTCivic = 0;
				}
				else
				{
					EnemiesKilledPerDTCivic++;
				}
			}
			else if (this.OnRubinsAICollect != null)
			{
				this.OnRubinsAICollect(reward);
			}
			onAIKilled(controller);
			EnemiesKilledPerDT++;
			if (checkingEnemies == null)
			{
				checkingEnemies = CheckEnemiesKilledPerTime(0.5f);
				StartCoroutine(checkingEnemies);
			}
			if (!controller.IsCivic)
			{
				GameCenter.OnDestroyEnemy();
			}
			else
			{
				GameCenter.OnDestroyCivil();
			}
		}
		else if (this.OnRubinsAICollect != null)
		{
			this.OnRubinsAICollect(reward);
		}
	}

	public void onAIKilled(Car2DAIController controller = null)
	{
		if (controller != null)
		{
			race.activeAIs.Remove(controller);
		}
	}

	private IEnumerator CheckEnemiesKilledPerTime(float time)
	{
		while (time >= 0f)
		{
			if (EnemiesKilledPerDT >= 2)
			{
				GUIInterfaceMessage.Words[] array = new GUIInterfaceMessage.Words[2]
				{
					GUIInterfaceMessage.Words.combo,
					GUIInterfaceMessage.Words.yourock
				};
				gui.interface_messanger.AnimateWithText(array[UnityEngine.Random.Range(0, array.Length)]);
				EnemiesKilledPerDT = 0;
				checkingEnemies = null;
				yield break;
			}
			time -= Time.deltaTime;
			yield return null;
		}
		if (car.HealthModule._Barrel.Value / car.HealthModule._Barrel.MaxValue < 0.1f)
		{
			GUIInterfaceMessage.Words[] array2 = new GUIInterfaceMessage.Words[2]
			{
				GUIInterfaceMessage.Words.wow,
				GUIInterfaceMessage.Words.yumyum
			};
			gui.interface_messanger.AnimateWithText(array2[UnityEngine.Random.Range(0, array2.Length)]);
		}
		EnemiesKilledPerDT = 0;
		checkingEnemies = null;
	}

	public void InitAI(Car2DAIController controller, int _car, bool iscivic, int coll, int Rams, int gans, string scrap1, string scrap2, string scrap3, string scrap4, string scrap5, string scrap1y, string scrap2y, string scrap3y, string scrap4y, string scrap5y, Vector3 RGB, int locat, int rubyUkus, int rubyBoom)
	{
		controller.IsCivic = iscivic;
		controller.constructor.location = locat;
		controller.gameObject.SetActive(value: true);
		controller.constructor.SetCar(_car);
		if (!iscivic)
		{
			controller.constructor.finalSetRam(Rams);
			controller.constructor.finalSetgun(gans);
			controller.constructor.rams = Rams;
			controller.constructor.guns = gans;
		}
		controller.constructor.scrap1 = scrap1;
		controller.constructor.scrap2 = scrap2;
		controller.constructor.scrap3 = scrap3;
		controller.constructor.scrap4 = scrap4;
		controller.constructor.scrap5 = scrap5;
		if (Progress.shop.EsterLevelPlay)
		{
			if (controller.constructor.carType == 1)
			{
				controller.constructor.scrap1y = "enemy_igor_easter_car01_scp01";
				controller.constructor.scrap2y = "enemy_igor_easter_car01_scp02";
				controller.constructor.scrap3y = "enemy_igor_easter_car01_scp03";
				controller.constructor.scrap4y = "enemy_igor_easter_car01_scp04";
				controller.constructor.scrap5y = "enemy_igor_easter_car01_wheel_f";
			}
			else if (controller.constructor.carType == 2)
			{
				controller.constructor.scrap1y = "enemy_igor_easter_car02_scp01";
				controller.constructor.scrap2y = "enemy_igor_easter_car02_scp02";
				controller.constructor.scrap3y = "enemy_igor_easter_car02_scp03";
				controller.constructor.scrap4y = "enemy_igor_easter_car02_scp04";
				controller.constructor.scrap5y = "enemy_igor_easter_car02_wheel";
			}
			else if (controller.constructor.carType == 3)
			{
				controller.constructor.scrap1y = "enemy_igor_easter_car03_scp01";
				controller.constructor.scrap2y = "enemy_igor_easter_car03_scp02";
				controller.constructor.scrap3y = "enemy_igor_easter_car03_scp03";
				controller.constructor.scrap4y = "enemy_igor_easter_car03_scp04";
				controller.constructor.scrap5y = "enemy_igor_easter_car03_wheel_f";
			}
			else if (controller.constructor.carType == 4)
			{
				controller.constructor.scrap1y = "enemy_igor_easter_car04_scp01";
				controller.constructor.scrap2y = "enemy_igor_easter_car04_scp02";
				controller.constructor.scrap3y = "enemy_igor_easter_car04_scp03";
				controller.constructor.scrap4y = "enemy_igor_easter_car04_scp04";
				controller.constructor.scrap5y = "enemy_igor_easter_car04_wheel";
			}
		}
		else
		{
			controller.constructor.scrap1y = scrap1y;
			controller.constructor.scrap2y = scrap2y;
			controller.constructor.scrap3y = scrap3y;
			controller.constructor.scrap4y = scrap4y;
			controller.constructor.scrap5y = scrap5y;
		}
		controller.CollRubyForVzruv = rubyBoom;
		controller.CollRubyForYkys = rubyUkus;
		controller.constructor._RGB = RGB;
		if (car == null)
		{
			NonInitedAI.Add(delegate
			{
				InitAI(controller, _car, iscivic, coll, Rams, gans, scrap1, scrap2, scrap3, scrap4, scrap5, scrap1y, scrap2y, scrap3y, scrap4y, scrap5y, RGB, locat, rubyUkus, rubyBoom);
			});
			controller.Init(iscivic);
			return;
		}
		controller.Init(iscivic, car.transform);
		if (!iscivic)
		{
			for (int i = 0; i < car.GetComponentsInChildren<Car2DWeaponModuleBase>().Length; i++)
			{
				car.GetComponentsInChildren<Car2DWeaponModuleBase>()[i].AddTarget(controller.transform);
			}
		}
		controller.constructor.colScraps = coll;
		if (!iscivic)
		{
			gui.interface_PositionBar.InitAIs(controller);
		}
		race.activeAIs.Add(controller);
	}

	public void EatMainCar(float damage)
	{
		if (car != null)
		{
			car.HealthModule.ChangeHealth(0f - damage);
		}
	}

	public void HitMainCar(float damage)
	{
		if (car != null)
		{
			car.HealthModule.ChangeHealth(0f - Math.Abs(damage));
		}
	}

	public void SetRegarger(bool active)
	{
		if (gui != null)
		{
			gui.interface_CarInfoBars.SetActiveRecharger(active, delegate
			{
				car.BombModule.Increase(1);
			});
		}
	}

	public BoostTimer GetBoostersForCloud()
	{
		return gui.interface_BoostTimer;
	}

	private void Update()
	{
		if (car != null && !car.BombModule._barrel.Enable)
		{
			car.BombModule._barrel.Enable = true;
		}
		if (Progress.shop.Tutorial && car != null && car.HealthModule._barrel.Value < 10f)
		{
			car.HealthModule._barrel.Value = 10f;
		}
		if (CounterEmemys != LastCounterEmemys && (bool)gui && gui.gameObject.activeSelf)
		{
			if (DifficultyConfig.instance.ReturnCurDifIndex(CounterEmemys) != CurrentDifLevel)
			{
				CurrentDifLevel = DifficultyConfig.instance.ReturnCurDifIndex(CounterEmemys);
				Progress.levels.Pack(pack).Level(level).ticket = CurrentDifLevel;
			}
			gui.SetToken();
			LastCounterEmemys = CounterEmemys;
		}
		if (Progress.shop.endlessLevel && Convoi != null && !Convoi.activeSelf)
		{
			OnFinish1();
		}
	}
}
