using SmartLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Results_new_controller : MonoBehaviour
{
	[Header("Animators")]
	public Animator Shadefar;

	public Animator Shadenear;

	public Animator Badges;

	public Animator BadgesUnderground;

	public Animator Panel;

	public Animator Gear;

	public Animator TopPanel;

	[Header("X2")]
	public Button x2;

	public GameObject x2GO;

	[Header("Delay")]
	public float DelayForStart;

	public float DelayForStart2;

	[Header("Buttons no spec")]
	public GameObject containerBtnNoSpec;

	public Button _btnNext;

	public Button _btnShop;

	public Button _btnRestart;

	[Header("Buttons spec")]
	public GameObject containerBtnSpec;

	public Button _btnNextS;

	public Button _btnShopS;

	[Header("Main Panel")]
	public GameObject panelMain;

	public GameObject backGround;

	public GameObject frontPanel;

	public GameObject backGroundlose;

	public GameObject frontPanellose;

	public Canvas canvaNeedForCamera;

	[Header("Top Panel")]
	public CounterController RubyText;

	public CounterController text;

	public GameObject icoInf;

	[Header("For Suma")]
	public CounterController Collectable;

	public Animator Coll;

	public CounterController Destroys;

	public Animator Dest;

	public CounterController Total;

	public Animator Tot;

	[Header("BonusAnim")]
	public Animator animatorForPercent;

	public Animator animValue;

	public GameObject newGameObj;

	public GameObject woff;

	public CounterController percent;

	private Camera cameratemp;

	[Header("Boss&&Missions")]
	public GameObject NormalCaption;

	public GameObject NormalBadges;

	public GameObject MissionCaption;

	public Text MissionCarName;

	public GameObject MissionCarsObj;

	public List<GameObject> MissionCars;

	public GameObject BossCaption;

	public Text BossCarName;

	public GameObject BossCarsObj;

	public List<GameObject> BossCars;

	public Animator BossUnlockCarAnim;

	public List<GameObject> BossUnlockCars;

	public Button _btnGarageUnlockCar;

	[Header("Arena")]
	public GameObject Content;

	public GameObject ContentArena;

	public CounterController BestArena;

	public Animator BstArena;

	public CounterController DistanceArena;

	public Animator DistArena;

	public CounterController CollectableArena;

	public Animator CollArena;

	public CounterController DestroysArena;

	public Animator DestArena;

	public CounterController TotalArena;

	public Animator TotArena;

	public Button _btnNextArena;

	public Button _btnShopArena;

	public Button _btnRestartArena;

	public GameObject DistObj;

	public GameObject KeyObj;

	public Animator keys;

	public Slider SliderBest;

	public Slider SliderCurrent;

	public Text DistText;

	public List<GameObject> Rubies;

	public List<GameObject> Hearts;

	public GameObject X2Valentine;

	private int _isOn = Animator.StringToHash("isOn");

	private int _victory_isOn = Animator.StringToHash("victory_isOn");

	private int _isActive = Animator.StringToHash("isActive");

	private int _isEnded = Animator.StringToHash("isEnded");

	private bool x2Show;

	public Results_Glogal_controller RGC;

	private ControllerGarage CG;

	[HideInInspector]
	public int kill;

	public int collect;

	public int tottal;

	private void Update()
	{
		text.count = Progress.gameEnergy.energy.ToString();
		if (Progress.gameEnergy.isInfinite)
		{
			text.gameObject.SetActive(value: false);
			icoInf.gameObject.SetActive(value: true);
		}
		else
		{
			text.gameObject.SetActive(value: true);
			icoInf.gameObject.SetActive(value: false);
		}
	}

	public void ShowX2()
	{
		if (Progress.shop.endlessLevel || Progress.shop.bossLevel)
		{
			return;
		}
		if (Progress.levels.CounterForWinForX2 == 3)
		{
			if (!Progress.levels.InUndeground)
			{
				x2.gameObject.SetActive(value: true);
			}
		}
		else
		{
			x2.gameObject.SetActive(value: false);
		}
	}

	private void OnEnable()
	{
		Content.SetActive(value: false);
		ContentArena.SetActive(value: false);
		if (Progress.shop.ArenaNew)
		{
			ContentArena.SetActive(value: true);
		}
		else if (Progress.shop.EsterLevelPlay)
		{
			ContentArena.SetActive(value: true);
			for (int i = 0; i < Rubies.Count; i++)
			{
				Rubies[i].SetActive(value: false);
			}
			for (int j = 0; j < Hearts.Count; j++)
			{
				Hearts[j].SetActive(value: true);
			}
		}
		else
		{
			Content.SetActive(value: true);
		}
		RubyText.count = Progress.shop.currency.ToString();
		Collectable.count = string.Empty;
		Destroys.count = string.Empty;
		Total.count = string.Empty;
		backGroundlose.SetActive(value: false);
		frontPanellose.SetActive(value: false);
		backGround.SetActive(value: true);
		frontPanel.SetActive(value: true);
		_btnGarageUnlockCar.onClick.AddListener(ShopUnlockCar);
		text.count = Progress.gameEnergy.energy.ToString();
		x2.onClick.AddListener(X2);
		if (Progress.gameEnergy.isInfinite)
		{
			text.gameObject.SetActive(value: false);
			icoInf.gameObject.SetActive(value: true);
		}
		else
		{
			text.gameObject.SetActive(value: true);
			icoInf.gameObject.SetActive(value: false);
		}
		if (Progress.shop.bossLevel)
		{
			int num = -1;
			if (Progress.levels.InUndeground)
			{
				if (Progress.levels.active_boss_pack_last_openned_undeground == 1)
				{
					Progress.shop.BossDeath1Undeground = true;
				}
				if (Progress.levels.active_boss_pack_last_openned_undeground == 1 && Progress.shop.BossDeath1Undeground)
				{
					num = 4;
				}
			}
			else
			{
				if (Progress.levels.active_boss_pack_last_openned == 1)
				{
					Progress.shop.BossDeath1 = true;
				}
				else if (Progress.levels.active_boss_pack_last_openned == 2)
				{
					Progress.shop.BossDeath2 = true;
				}
				else if (Progress.levels.active_boss_pack_last_openned == 3)
				{
					Progress.shop.BossDeath3 = true;
				}
				if (Progress.levels.active_boss_pack_last_openned == 1 && Progress.shop.BossDeath1)
				{
					num = 1;
				}
				if (Progress.levels.active_boss_pack_last_openned == 2 && Progress.shop.BossDeath2)
				{
					num = 2;
				}
				if (Progress.levels.active_boss_pack_last_openned == 3 && Progress.shop.BossDeath3)
				{
					num = 3;
				}
			}
			UnityEngine.Debug.Log("AnalyticsManager.LogEvent(EventCategoty.result_win, boss" + num.ToString());
			if (num != -1)
			{
				AnalyticsManager.LogEvent(EventCategoty.result_boss, "boss" + num.ToString(), "win");
			}
		}
		if (Progress.shop.endlessLevel && !Progress.shop.bossLevel)
		{
			UnityEngine.Debug.Log("AnalyticsManager.LogEvent(EventCategoty.result_win_special_mission , Progress.shop.SpecialMissionsRewardCar  ====>>> " + Progress.shop.SpecialMissionsRewardCar);
			AnalyticsManager.LogEvent(EventCategoty.result_special_mission, "win", Progress.shop.SpecialMissionsRewardCar.ToString());
			containerBtnSpec.SetActive(value: true);
			containerBtnNoSpec.SetActive(value: false);
			_btnNextS.onClick.AddListener(levels);
			_btnShopS.onClick.AddListener(Monstro);
		}
		else
		{
			if (Progress.levels.InUndeground)
			{
				if (Progress.levels.active_pack_last_openned_under == 1)
				{
					AnalyticsManager.LogEvent(EventCategoty.result_level, "win", RaceLogic.instance.pack.ToString() + "_" + RaceLogic.instance.level.ToString() + "_cave");
				}
				else
				{
					AnalyticsManager.LogEvent(EventCategoty.result_level, "win", RaceLogic.instance.pack.ToString() + "_" + RaceLogic.instance.level.ToString() + "_sewage");
				}
			}
			else
			{
				AnalyticsManager.LogEvent(EventCategoty.result_level, "win", RaceLogic.instance.pack.ToString() + "_" + RaceLogic.instance.level.ToString());
			}
			containerBtnSpec.SetActive(value: false);
			containerBtnNoSpec.SetActive(value: true);
			_btnNext.onClick.AddListener(levels);
			_btnRestart.onClick.AddListener(Restart);
			_btnShop.onClick.AddListener(shop);
		}
		canvaNeedForCamera.worldCamera = Camera.main;
		StartCoroutine(ForStart());
		Camera main = Camera.main;
		main.gameObject.SetActive(value: false);
		main.gameObject.SetActive(value: true);
		OpenElements();
		if (Progress.shop.ArenaNew || Progress.shop.EsterLevelPlay)
		{
			DistObj.SetActive(value: false);
			KeyObj.SetActive(value: false);
			if (Progress.shop.EsterLevelPlay)
			{
				DistObj.SetActive(value: true);
			}
			BestArena.count = string.Empty;
			DistanceArena.count = string.Empty;
			CollectableArena.count = string.Empty;
			DestroysArena.count = string.Empty;
			TotalArena.count = string.Empty;
			_btnNextArena.onClick.AddListener(levels);
			_btnRestartArena.onClick.AddListener(Restart);
			_btnShopArena.onClick.AddListener(shop);
		}
	}

	private void X2()
	{
		AnalyticsManager.LogEvent(EventCategoty.video, "ads_watched", "x2_lvl_win");
		NewWrapperForAppodeal.instance.ShowVideo(delegate(bool sucess)
		{
			if (sucess)
			{
				Sucses();
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

	public void Sucses()
	{
		StartCoroutine(suc());
	}

	private IEnumerator suc()
	{
		x2Show = true;
		x2GO.SetActive(value: true);
		x2.interactable = false;
		Progress.levels.CounterForWinForX2 = 0;
		Progress.shop.currency += tottal;
		RubyText.count = Progress.shop.currency.ToString();
		yield return 0;
		Total.count = (tottal * 2).ToString();
		animatorForPercent.SetBool(_isOn, value: false);
		x2.interactable = true;
	}

	public void NoSucses()
	{
		x2.interactable = true;
	}

	public void NOvideo()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("noVideoBox", typeof(GameObject))) as GameObject;
		x2.interactable = true;
	}

	private void OpenElements()
	{
		NormalCaption.SetActive(value: false);
		NormalBadges.SetActive(value: false);
		MissionCaption.SetActive(value: false);
		MissionCarsObj.SetActive(value: false);
		for (int i = 0; i < MissionCars.Count; i++)
		{
			MissionCars[i].SetActive(value: false);
		}
		BossCaption.SetActive(value: false);
		BossCarsObj.SetActive(value: false);
		for (int j = 0; j < BossCars.Count; j++)
		{
			BossCars[j].SetActive(value: false);
		}
		if (Progress.shop.endlessLevel || Progress.shop.bossLevel)
		{
			if (Progress.shop.endlessLevel)
			{
				MissionCaption.SetActive(value: true);
				MissionCarName.text = LanguageManager.Instance.GetTextValue(MonstropediaConfig.instance.Price.Infos[Progress.shop.SpecialMissionsRewardCar - 1].CarName);
				MissionCarsObj.SetActive(value: true);
				MissionCars[Progress.shop.SpecialMissionsRewardCar - 1].SetActive(value: true);
				Progress.shop.SpecialMissionsGated[Progress.shop.SpecialMissionsRewardCar - 1] = true;
				Progress.shop.MonstroLocks[Progress.shop.SpecialMissionsRewardCar - 1] = false;
				Progress.shop.SpecialMissionsRewardCar = -1;
			}
			if (!Progress.shop.bossLevel)
			{
				return;
			}
			BossCarsObj.SetActive(value: true);
			int num = -1;
			if (!Progress.levels.InUndeground)
			{
				if (Progress.levels.active_boss_pack_last_openned == 1 && Progress.shop.BossDeath1)
				{
					num = 0;
				}
				if (Progress.levels.active_boss_pack_last_openned == 2 && Progress.shop.BossDeath2)
				{
					num = 1;
				}
				if (Progress.levels.active_boss_pack_last_openned == 3 && Progress.shop.BossDeath3)
				{
					num = 2;
				}
			}
			else
			{
				if (Progress.levels.active_boss_pack_last_openned_undeground == 1 && Progress.shop.BossDeath1Undeground)
				{
					num = 3;
				}
				if (Progress.levels.active_boss_pack_last_openned_undeground == 2 && Progress.shop.BossDeath2Undeground)
				{
					num = 4;
				}
			}
			BossCaption.SetActive(value: true);
			string key = string.Empty;
			if (num == 0)
			{
				key = "BOSS1";
			}
			if (num == 1)
			{
				key = "BOSS2";
			}
			if (num == 2)
			{
				key = "BOSS3";
			}
			if (num == 3)
			{
				key = "COCKCHAFER";
			}
			if (num == 4)
			{
				key = "TURTLE";
			}
			BossCarName.text = LanguageManager.Instance.GetTextValue(key);
			BossCars[num].SetActive(value: true);
		}
		else
		{
			NormalCaption.SetActive(value: true);
			NormalBadges.SetActive(value: true);
		}
	}

	private void OnDisable()
	{
		_btnNext.onClick.RemoveAllListeners();
		_btnRestart.onClick.RemoveAllListeners();
		_btnShop.onClick.RemoveAllListeners();
		_btnNextS.onClick.RemoveAllListeners();
		_btnShopS.onClick.RemoveAllListeners();
		_btnGarageUnlockCar.onClick.RemoveAllListeners();
		_btnNextArena.onClick.RemoveAllListeners();
		_btnRestartArena.onClick.RemoveAllListeners();
		_btnShopArena.onClick.RemoveAllListeners();
	}

	private IEnumerator FORKEY()
	{
		while (!keys.gameObject.activeSelf)
		{
			yield return 0;
		}
		while (!keys.GetBool("isON"))
		{
			keys.SetBool("isON", value: true);
			yield return 0;
		}
	}

	private IEnumerator ForStart()
	{
		yield return new WaitForSeconds(DelayForStart);
		if (Progress.shop.EsterLevelPlay && Progress.shop.ValentineMaxDistance < Progress.shop.ValentineDistance)
		{
			Progress.shop.ValentineMaxDistance = Progress.shop.ValentineDistance;
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
			switch (active_pack)
			{
			case 1:
				AnalyticsManager.LogEvent(EventCategoty.results_arena, "arena1", "win");
				if (!Progress.shop.Key1)
				{
					if (Progress.shop.Arena1MaxDistance < Progress.shop.Arena1Distance)
					{
						Progress.shop.Arena1MaxDistance = Progress.shop.Arena1Distance;
					}
					KeyObj.SetActive(value: true);
					StartCoroutine(FORKEY());
					DistText.text = num.ToString() + " M.";
					if (Progress.shop.Arena1MaxDistance != 0)
					{
						SliderBest.gameObject.SetActive(value: true);
						SliderBest.maxValue = Progress.shop.Arena1MaxDistance;
						SliderBest.value = Progress.shop.Arena1MaxDistance;
					}
					else
					{
						SliderBest.gameObject.SetActive(value: false);
					}
					SliderCurrent.maxValue = num;
					SliderCurrent.value = Progress.shop.Arena1Distance;
					Progress.shop.Key1 = true;
				}
				else
				{
					DistObj.SetActive(value: true);
				}
				break;
			case 2:
				AnalyticsManager.LogEvent(EventCategoty.results_arena, "arena2", "win");
				if (!Progress.shop.Key2)
				{
					KeyObj.SetActive(value: true);
					if (Progress.shop.Arena2MaxDistance < Progress.shop.Arena2Distance)
					{
						Progress.shop.Arena2MaxDistance = Progress.shop.Arena2Distance;
					}
					DistText.text = num.ToString() + " M.";
					keys.SetBool("isON", value: true);
					StartCoroutine(FORKEY());
					if (Progress.shop.Arena2MaxDistance != 0)
					{
						SliderBest.gameObject.SetActive(value: true);
						SliderBest.maxValue = Progress.shop.Arena2MaxDistance;
						SliderBest.value = Progress.shop.Arena2MaxDistance;
					}
					else
					{
						SliderBest.gameObject.SetActive(value: false);
					}
					SliderCurrent.maxValue = num;
					SliderCurrent.value = Progress.shop.Arena2Distance;
					Progress.shop.Key2 = true;
				}
				else
				{
					DistObj.SetActive(value: true);
				}
				break;
			case 3:
				AnalyticsManager.LogEvent(EventCategoty.results_arena, "arena3", "win");
				if (!Progress.shop.Key3)
				{
					KeyObj.SetActive(value: true);
					if (Progress.shop.Arena3MaxDistance < Progress.shop.Arena3Distance)
					{
						Progress.shop.Arena3MaxDistance = Progress.shop.Arena3Distance;
					}
					DistText.text = num.ToString() + " M.";
					keys.SetBool("isON", value: true);
					StartCoroutine(FORKEY());
					if (Progress.shop.Arena3MaxDistance != 0)
					{
						SliderBest.gameObject.SetActive(value: true);
						SliderBest.maxValue = Progress.shop.Arena3MaxDistance;
						SliderBest.value = Progress.shop.Arena3MaxDistance;
					}
					else
					{
						SliderBest.gameObject.SetActive(value: false);
					}
					SliderCurrent.maxValue = num;
					SliderCurrent.value = Progress.shop.Arena3Distance;
					Progress.shop.Key3 = true;
				}
				else
				{
					DistObj.SetActive(value: true);
				}
				break;
			}
		}
		Panel.SetBool(_isOn, value: true);
		TopPanel.SetBool(_isOn, value: true);
		Audio.PlayAsync("gui_screen_on");
		yield return new WaitForSeconds(DelayForStart2);
		Shadenear.SetBool(_victory_isOn, value: true);
		Shadefar.SetBool(_victory_isOn, value: true);
		Gear.SetBool(_victory_isOn, value: true);
		yield return new WaitForSeconds(0.3f);
		Audio.PlayAsync("results_light");
		yield return new WaitForSeconds(DelayForStart2 - 0.3f);
	}

	private void Restart()
	{
		Audio.Play("gui_button_02_sn");
		if (Progress.shop.EsterLevelPlay)
		{
			StartCoroutine(DelayForRestart());
			AdvertWrapper.instance.ShowInterstitial(show: true);
		}
		else if (!GameEnergyLogic.isEnoughForRace)
		{
			GUI_shop.instance.ShowBuyCanvasWindow();
		}
		else
		{
			StartCoroutine(DelayForRestart());
			AdvertWrapper.instance.ShowInterstitial(show: true);
		}
	}

	private IEnumerator DelayForRestart()
	{
		Audio.Play("fuel-1");
		RGC.AnimfuelText.text = "-" + PriceConfig.instance.energy.eachStart.ToString();
		RGC.animfuel.Play();
		RGC.animfuel["bodov_PAUSE_decreasFuel"].speed = 0.2f;
		while (RGC.animfuel.isPlaying)
		{
			yield return 0;
		}
		Panel.SetBool(_isOn, value: false);
		animatorForPercent.SetBool(_isOn, value: false);
		float t = 1f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return null;
		}
		Game.OnStateChange(Game.gameState.PreRace);
		RaceLogic.instance.Restart();
		RaceLogic.instance.gui.gameObject.SetActive(value: true);
		panelMain.SetActive(value: false);
		base.gameObject.SetActive(value: false);
	}

	private void levels()
	{
		if (Progress.levels.CounterForWinForX2 == 3)
		{
			Progress.levels.CounterForWinForX2 = 0;
		}
		Audio.Play("gui_button_02_sn");
		if (!x2Show)
		{
			AdvertWrapper.instance.ShowInterstitial(show: true);
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

	private void ShopUnlockCar()
	{
		int num = 0;
		while (true)
		{
			if (num < BossUnlockCars.Count)
			{
				if (BossUnlockCars[num].activeSelf)
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		Progress.shop.activeCar = num;
		shop();
	}

	private void Monstro()
	{
		Time.timeScale = 1f;
		Progress.shop.Monstroinlevel = true;
		SceneManager.LoadScene("monstropedia_new", LoadSceneMode.Additive);
	}

	private void shop()
	{
		if (Game.currentState != Game.gameState.Shop)
		{
			Game.OnStateChange(Game.gameState.Shop);
		}
		if (!Progress.shop.showOpenGateCar2 && Progress.shop.BossDeath1)
		{
			Progress.shop.activeCar = 1;
		}
		if (!Progress.shop.showOpenGateCar3 && Progress.shop.BossDeath2)
		{
			Progress.shop.activeCar = 2;
		}
		Audio.Play("gui_button_02_sn");
		Time.timeScale = 1f;
		Progress.shop.shopinlevel = true;
		if (CG == null)
		{
			SceneManager.LoadScene("garage_new", LoadSceneMode.Additive);
			StartCoroutine(shops());
		}
		else
		{
			SceneManager.LoadScene("garage_new", LoadSceneMode.Additive);
			StartCoroutine(shops());
		}
	}

	private IEnumerator shops()
	{
		while (CG == null)
		{
			UnityEngine.Debug.Log("CG NULL");
			CG = UnityEngine.Object.FindObjectOfType<ControllerGarage>();
			yield return 0;
		}
		UnityEngine.Debug.Log("CG IS NOT NULL");
	}

	public void ChengBaidg()
	{
		BadgesUnderground.transform.parent.gameObject.SetActive(value: false);
		Badges.transform.parent.gameObject.SetActive(value: false);
		if (Progress.levels.InUndeground)
		{
			BadgesUnderground.transform.parent.gameObject.SetActive(value: true);
		}
		else
		{
			Badges.transform.parent.gameObject.SetActive(value: true);
		}
	}

	public void Results_Suma(int rubyCollectble, int rubyDestroyEnemy, float percentForWin)
	{
		percent.count = percentForWin.ToString();
		GameCenterWrapper.SaveGameSave();
		Progress.shop.foundProgress = false;
		woff.SetActive(value: true);
		StartCoroutine(Suma(rubyCollectble, rubyDestroyEnemy, percentForWin));
	}

	private IEnumerator Suma(int rubyCollectble, int rubyDestroyEnemy, float percentForWin)
	{
		if (Progress.shop.bossLevel)
		{
			for (int i = 0; i < BossUnlockCars.Count; i++)
			{
				BossUnlockCars[i].SetActive(value: false);
			}
			int num = -1;
			if (Progress.shop.BossDeath1)
			{
				num = 1;
			}
			if (Progress.shop.BossDeath2)
			{
				num = 2;
			}
			if (Progress.shop.BossDeath3)
			{
				num = -1;
			}
			if (num != -1)
			{
				if (Progress.levels.active_boss_pack_last_openned == 1 && !Progress.shop.BossDeath1Reward)
				{
					BossUnlockCars[num].SetActive(value: true);
					BossUnlockCarAnim.SetBool("isUnlocked", value: true);
					Audio.Play("lose_screen");
					Progress.shop.BossDeath1Reward = true;
				}
				if (Progress.levels.active_boss_pack_last_openned == 2 && !Progress.shop.BossDeath2Reward)
				{
					BossUnlockCars[num].SetActive(value: true);
					BossUnlockCarAnim.SetBool("isUnlocked", value: true);
					Audio.Play("lose_screen");
					Progress.shop.BossDeath2Reward = true;
				}
				if (Progress.levels.active_boss_pack_last_openned == 3 && !Progress.shop.BossDeath3Reward)
				{
					BossUnlockCars[num].SetActive(value: true);
					BossUnlockCarAnim.SetBool("isUnlocked", value: true);
					Audio.Play("lose_screen");
					Progress.shop.BossDeath3Reward = true;
				}
			}
		}
		float temp7;
		if (Progress.shop.EsterLevelPlay || (Progress.shop.ArenaNew && DistObj.activeSelf))
		{
			int _pack = Progress.levels.active_pack_last_openned;
			int _bestDist = 0;
			int _currentDist = 0;
			switch (_pack)
			{
			case 1:
				_bestDist = Progress.shop.Arena1MaxDistance;
				_currentDist = Progress.shop.Arena1Distance;
				break;
			case 2:
				_bestDist = Progress.shop.Arena2MaxDistance;
				_currentDist = Progress.shop.Arena2Distance;
				break;
			case 3:
				_bestDist = Progress.shop.Arena3MaxDistance;
				_currentDist = Progress.shop.Arena3Distance;
				break;
			}
			if (Progress.shop.EsterLevelPlay)
			{
				_bestDist = Progress.shop.ValentineMaxDistance;
				_currentDist = Progress.shop.ValentineDistance;
			}
			temp7 = (float)_bestDist / 1000f;
			if (temp7 < 0.5f)
			{
				temp7 = 0.5f;
			}
			else if (temp7 > 1f)
			{
				temp7 = 1f;
			}
			yield return StartCoroutine(RubiesLerp(BestArena, _bestDist, 0f, temp7, BstArena));
			temp7 = (float)_currentDist / 1000f;
			if (temp7 < 0.5f)
			{
				temp7 = 0.5f;
			}
			else if (temp7 > 1f)
			{
				temp7 = 1f;
			}
			yield return StartCoroutine(RubiesLerp(DistanceArena, _currentDist, 0f, temp7, DistArena));
		}
		temp7 = (float)rubyCollectble / 1000f;
		if (temp7 < 0.5f)
		{
			temp7 = 0.5f;
		}
		else if (temp7 > 1f)
		{
			temp7 = 1f;
		}
		if (Progress.shop.ArenaNew || Progress.shop.EsterLevelPlay)
		{
			yield return StartCoroutine(RubiesLerp(CollectableArena, rubyCollectble, 0f, temp7, CollArena));
		}
		else
		{
			yield return StartCoroutine(RubiesLerp(Collectable, rubyCollectble, 0f, temp7, Coll));
		}
		collect = rubyCollectble;
		temp7 = (float)rubyDestroyEnemy / 1000f;
		if (temp7 < 0.5f)
		{
			temp7 = 0.5f;
		}
		else if (temp7 > 1f)
		{
			temp7 = 1f;
		}
		if (Progress.shop.ArenaNew || Progress.shop.EsterLevelPlay)
		{
			yield return StartCoroutine(RubiesLerp(DestroysArena, rubyDestroyEnemy, 0f, temp7, DestArena));
		}
		else
		{
			yield return StartCoroutine(RubiesLerp(Destroys, rubyDestroyEnemy, 0f, temp7, Dest));
		}
		kill = rubyDestroyEnemy;
		temp7 = (float)(rubyDestroyEnemy + rubyCollectble) / 1000f;
		if (temp7 < 0.5f)
		{
			temp7 = 0.5f;
		}
		else if (temp7 > 1f)
		{
			temp7 = 1f;
		}
		if (Progress.shop.ArenaNew || Progress.shop.EsterLevelPlay)
		{
			yield return StartCoroutine(RubiesLerp(TotalArena, rubyDestroyEnemy + rubyCollectble, 0f, temp7, TotArena));
		}
		else
		{
			yield return StartCoroutine(RubiesLerp(Total, rubyDestroyEnemy + rubyCollectble, 0f, temp7, Tot));
		}
		tottal = rubyDestroyEnemy + rubyCollectble;
		yield return new WaitForSeconds(0.5f);
		if (Progress.shop.EsterLevelPlay && Progress.shop.EsterX2TimeActivate)
		{
			X2Valentine.SetActive(value: true);
			yield return StartCoroutine(RubiesLerp(TotalArena, (rubyDestroyEnemy + rubyCollectble) * 2, rubyDestroyEnemy + rubyCollectble, temp7, TotArena));
			Progress.shop.EsterX2TimeActivate = false;
		}
		float tempsBadges = 0f;
		if (!Progress.shop.endlessLevel && !Progress.shop.bossLevel && !Progress.shop.ArenaNew && !Progress.shop.EsterLevelPlay)
		{
			if (Progress.levels.InUndeground)
			{
				BadgesUnderground.SetInteger("old_difficulty", Progress.levels.Pack(RaceLogic.instance.pack).Level(RaceLogic.instance.level).ticket);
			}
			else
			{
				Badges.SetInteger("old_difficulty", Progress.levels.Pack(RaceLogic.instance.pack).Level(RaceLogic.instance.level).ticket);
			}
			yield return new WaitForSeconds(1f);
			int tix = Progress.levels.Pack(RaceLogic.instance.pack).Level(RaceLogic.instance.level).ticket;
			if (tix > 0)
			{
				float badgePerc = 0f;
				if (tix == 1)
				{
					badgePerc = 10f;
				}
				else if (tix == 2)
				{
					badgePerc = 25f;
				}
				else if (tix >= 3)
				{
					badgePerc = 50f;
				}
				tempsBadges = (float)(rubyDestroyEnemy + rubyCollectble) * (badgePerc / 100f);
				temp7 = ((float)(rubyDestroyEnemy + rubyCollectble) + tempsBadges) / 500f;
				if (temp7 < 0.5f)
				{
					temp7 = 0.5f;
				}
				else if (temp7 > 1f)
				{
					temp7 = 1f;
				}
				yield return StartCoroutine(RubiesLerp(Total, (float)(rubyDestroyEnemy + rubyCollectble) + tempsBadges, rubyDestroyEnemy + rubyCollectble, temp7, Tot));
				tottal = (int)((float)(rubyDestroyEnemy + rubyCollectble) + tempsBadges);
			}
		}
		if (percentForWin != 0f)
		{
			newGameObj.SetActive(value: true);
			animatorForPercent.SetBool(_isOn, value: true);
			animValue.SetBool(_isActive, value: true);
			yield return new WaitForSeconds(1f);
			float temps = (float)(rubyDestroyEnemy + rubyCollectble) * (percentForWin / 100f);
			temp7 = ((float)(rubyDestroyEnemy + rubyCollectble) + temps) / 500f;
			if (temp7 < 0.5f)
			{
				temp7 = 0.5f;
			}
			else if (temp7 > 1f)
			{
				temp7 = 1f;
			}
			yield return StartCoroutine(RubiesLerp(Total, (float)(rubyDestroyEnemy + rubyCollectble) + tempsBadges + temps, (float)(rubyDestroyEnemy + rubyCollectble) + tempsBadges, temp7, Tot));
			tottal = (int)((float)(rubyDestroyEnemy + rubyCollectble) + tempsBadges + temps);
		}
		if (!Progress.shop.endlessLevel && !Progress.shop.bossLevel && Progress.levels.CounterForWinForX2 == 3 && !Progress.levels.InUndeground)
		{
			newGameObj.SetActive(value: false);
			animatorForPercent.SetBool(_isOn, value: false);
			animValue.SetBool(_isActive, value: false);
			yield return new WaitForSeconds(1f);
			ShowX2();
			animatorForPercent.SetBool(_isOn, value: true);
		}
		int tempBFA = 0;
		for (int j = 0; j < 4; j++)
		{
			for (int k = 0; k <= 12; k++)
			{
				tempBFA += Progress.levels.Pack(j).Level(k).ticket;
			}
		}
		GameCenter.OnBadgesCollect(tempBFA);
	}

	private IEnumerator RubiesLerp(CounterController l, float number, float buff, float time, Animator anim, bool x2 = false)
	{
		if (!x2)
		{
			anim.SetBool(_isEnded, value: false);
		}
		if (number > 0f)
		{
			Audio.PlayAsync("gui_scoring");
		}
		float buf = buff;
		float t = 0f;
		while (t < time)
		{
			t += Time.deltaTime * 2f;
			buf = Mathf.Lerp(buf, number, t);
			l.count = ((int)buf).ToString();
			yield return null;
		}
		if (!x2)
		{
			anim.SetBool(_isEnded, value: true);
		}
		l.count = ((int)number).ToString();
	}
}
