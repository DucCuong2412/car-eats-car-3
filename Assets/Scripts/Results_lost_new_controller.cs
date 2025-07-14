using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Results_lost_new_controller : MonoBehaviour
{
	[Header("Animators")]
	public Animator Panel;

	public Animator PanelArena;

	public Animator TopPanel;

	[Header("Delay")]
	public float DelayForStart;

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

	public GameObject frontPanelArena;

	public GameObject backGroundwin;

	public GameObject frontPanelwin;

	public Canvas canvaNeedForCamera;

	[Header("Top Panel")]
	public CounterController text;

	public CounterController textRuby;

	public GameObject icoInf;

	[Header("For Suma")]
	public CounterController Collectable;

	public Animator Coll;

	public CounterController Destroys;

	public Animator Dest;

	public CounterController Total;

	public Animator Tot;

	[Header("For Suma Arena")]
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

	public GameObject DistObj;

	public GameObject KeyObj;

	public Slider SliderBest;

	public Slider SliderCurrent;

	public Text DistText;

	public Button _btnNextArena;

	public Button _btnShopArena;

	public Button _btnRestartArena;

	[Header("BonusAnim")]
	public Animator animatorForPercent;

	public Animator animValue;

	public GameObject woof;

	public CounterController percent;

	public Results_Glogal_controller RGC;

	private ControllerGarage CG;

	private int _isEnded = Animator.StringToHash("isEnded");

	private int _isOn = Animator.StringToHash("isOn");

	private int _isActive = Animator.StringToHash("isActive");

	private void OnEnable()
	{
		canvaNeedForCamera.worldCamera = Camera.main;
		Camera main = Camera.main;
		main.gameObject.SetActive(value: false);
		main.gameObject.SetActive(value: true);
		Collectable.count = string.Empty;
		Destroys.count = string.Empty;
		Total.count = string.Empty;
		CollectableArena.count = string.Empty;
		DestroysArena.count = string.Empty;
		TotalArena.count = string.Empty;
		BestArena.count = string.Empty;
		DistanceArena.count = string.Empty;
		backGroundwin.SetActive(value: false);
		frontPanelwin.SetActive(value: false);
		backGround.SetActive(value: true);
		if (Progress.shop.ArenaNew)
		{
			frontPanelArena.SetActive(value: true);
		}
		else
		{
			frontPanel.SetActive(value: true);
		}
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
		if (Progress.shop.bossLevel)
		{
			int num = -1;
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
			if (num != -1)
			{
				AnalyticsManager.LogEvent(EventCategoty.result_boss, "boss" + num.ToString(), "lose");
			}
		}
		if (Progress.shop.endlessLevel && !Progress.shop.bossLevel)
		{
			containerBtnSpec.SetActive(value: true);
			containerBtnNoSpec.SetActive(value: false);
			_btnNextS.onClick.AddListener(levels);
			_btnShopS.onClick.AddListener(monstro);
			AnalyticsManager.LogEvent(EventCategoty.result_special_mission, "lose", Progress.shop.SpecialMissionsRewardCar.ToString());
		}
		else
		{
			if (Progress.levels.InUndeground)
			{
				if (Progress.levels.active_pack_last_openned_under == 1)
				{
					AnalyticsManager.LogEvent(EventCategoty.result_level, "lose", RaceLogic.instance.pack.ToString() + "_" + RaceLogic.instance.level.ToString() + "_cave");
				}
				else
				{
					AnalyticsManager.LogEvent(EventCategoty.result_level, "lose", RaceLogic.instance.pack.ToString() + "_" + RaceLogic.instance.level.ToString() + "_sewage");
				}
			}
			else
			{
				AnalyticsManager.LogEvent(EventCategoty.result_level, "lose", RaceLogic.instance.pack.ToString() + "_" + RaceLogic.instance.level.ToString());
			}
			containerBtnSpec.SetActive(value: false);
			containerBtnNoSpec.SetActive(value: true);
			_btnNext.onClick.AddListener(levels);
			_btnRestart.onClick.AddListener(Restart);
			_btnShop.onClick.AddListener(shop);
			if (Progress.shop.ArenaNew)
			{
				_btnNextArena.onClick.AddListener(levels);
				_btnRestartArena.onClick.AddListener(Restart);
				_btnShopArena.onClick.AddListener(shop);
			}
		}
		StartCoroutine(ForStart());
	}

	private void Update()
	{
		text.count = Progress.gameEnergy.energy.ToString();
		textRuby.count = Progress.shop.currency.ToString();
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

	private void OnDisable()
	{
		_btnNext.onClick.RemoveAllListeners();
		_btnRestart.onClick.RemoveAllListeners();
		_btnShop.onClick.RemoveAllListeners();
		_btnNextS.onClick.RemoveAllListeners();
		_btnShopS.onClick.RemoveAllListeners();
		if (Progress.shop.ArenaNew)
		{
			_btnNextArena.onClick.RemoveAllListeners();
			_btnRestartArena.onClick.RemoveAllListeners();
			_btnShopArena.onClick.RemoveAllListeners();
		}
	}

	private IEnumerator ForStart()
	{
		Audio.Play("lose_screen");
		Audio.Play("siren_police");
		yield return new WaitForSeconds(DelayForStart);
		if (Progress.shop.EsterLevelPlay && Progress.shop.ValentineMaxDistance < Progress.shop.ValentineDistance)
		{
			Progress.shop.ValentineMaxDistance = Progress.shop.ValentineDistance;
		}
		if (Progress.shop.ArenaNew)
		{
			PanelArena.SetBool(_isOn, value: true);
			DistObj.SetActive(value: false);
			KeyObj.SetActive(value: false);
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
				AnalyticsManager.LogEvent(EventCategoty.results_arena, "arena1", "lose");
				if (!Progress.shop.Key1)
				{
					KeyObj.SetActive(value: true);
					if (Progress.shop.Arena1MaxDistance < Progress.shop.Arena1Distance)
					{
						Progress.shop.Arena1MaxDistance = Progress.shop.Arena1Distance;
					}
					DistText.text = num.ToString() + " M.";
					if (Progress.shop.Arena1MaxDistance != 0)
					{
						SliderBest.gameObject.SetActive(value: true);
						SliderBest.maxValue = DifficultyConfig.instance.MetrivForARENA1;
						SliderBest.value = Progress.shop.Arena1MaxDistance;
					}
					else
					{
						SliderBest.gameObject.SetActive(value: false);
					}
					SliderCurrent.maxValue = DifficultyConfig.instance.MetrivForARENA1;
					SliderCurrent.value = Progress.shop.Arena1Distance;
				}
				else
				{
					DistObj.SetActive(value: true);
				}
				break;
			case 2:
				AnalyticsManager.LogEvent(EventCategoty.results_arena, "arena2", "lose");
				if (!Progress.shop.Key2)
				{
					KeyObj.SetActive(value: true);
					if (Progress.shop.Arena2MaxDistance < Progress.shop.Arena2Distance)
					{
						Progress.shop.Arena2MaxDistance = Progress.shop.Arena2Distance;
					}
					DistText.text = num.ToString() + " M.";
					if (Progress.shop.Arena2MaxDistance != 0)
					{
						SliderBest.gameObject.SetActive(value: true);
						SliderBest.maxValue = DifficultyConfig.instance.MetrivForARENA2;
						SliderBest.value = Progress.shop.Arena2MaxDistance;
					}
					else
					{
						SliderBest.gameObject.SetActive(value: false);
					}
					SliderCurrent.maxValue = DifficultyConfig.instance.MetrivForARENA2;
					SliderCurrent.value = Progress.shop.Arena2Distance;
				}
				else
				{
					DistObj.SetActive(value: true);
				}
				break;
			case 3:
				AnalyticsManager.LogEvent(EventCategoty.results_arena, "arena3", "lose");
				if (!Progress.shop.Key3)
				{
					KeyObj.SetActive(value: true);
					if (Progress.shop.Arena3MaxDistance < Progress.shop.Arena3Distance)
					{
						Progress.shop.Arena3MaxDistance = Progress.shop.Arena3Distance;
					}
					DistText.text = num.ToString() + " M.";
					if (Progress.shop.Arena3MaxDistance != 0)
					{
						SliderBest.gameObject.SetActive(value: true);
						SliderBest.maxValue = DifficultyConfig.instance.MetrivForARENA3;
						SliderBest.value = Progress.shop.Arena3MaxDistance;
					}
					else
					{
						SliderBest.gameObject.SetActive(value: false);
					}
					SliderCurrent.maxValue = DifficultyConfig.instance.MetrivForARENA3;
					SliderCurrent.value = Progress.shop.Arena3Distance;
				}
				else
				{
					DistObj.SetActive(value: true);
				}
				break;
			}
		}
		else
		{
			Panel.SetBool(_isOn, value: true);
		}
		TopPanel.SetBool(_isOn, value: true);
	}

	private void Restart()
	{
		Audio.Play("gui_button_02_sn");
		if (Progress.shop.EsterLevelPlay)
		{
			StartCoroutine(DelayForRestart());
		}
		else if (!GameEnergyLogic.isEnoughForRace)
		{
			GUI_shop.instance.ShowBuyCanvasWindow();
		}
		else
		{
			StartCoroutine(DelayForRestart());
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
		if (Progress.shop.ArenaNew)
		{
			PanelArena.SetBool(_isOn, value: false);
		}
		else
		{
			Panel.SetBool(_isOn, value: false);
		}
		animatorForPercent.SetBool(_isOn, value: false);
		float t = 1.5f;
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
		Audio.Play("gui_button_02_sn");
		if (Progress.levels.InUndeground)
		{
			Game.LoadLevel("scene_underground_map_new");
		}
		else
		{
			Game.LoadLevel("map_new");
		}
	}

	private void monstro()
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

	public void Results_Suma(int rubyCollectble, int rubyDestroyEnemy, float percentForWin)
	{
		percent.count = percentForWin.ToString();
		woof.SetActive(value: true);
		StartCoroutine(Suma(rubyCollectble, rubyDestroyEnemy, percentForWin));
	}

	private IEnumerator Suma(int rubyCollectble, int rubyDestroyEnemy, float percentForWin)
	{
		float temp6;
		if (Progress.shop.ArenaNew && DistObj.activeSelf)
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
			temp6 = (float)_bestDist / 1000f;
			if (temp6 < 0.5f)
			{
				temp6 = 0.5f;
			}
			else if (temp6 > 1f)
			{
				temp6 = 1f;
			}
			if (Progress.shop.ArenaNew)
			{
				yield return StartCoroutine(RubiesLerp(BestArena, _bestDist, 0f, temp6, BstArena));
			}
			temp6 = (float)_currentDist / 1000f;
			if (temp6 < 0.5f)
			{
				temp6 = 0.5f;
			}
			else if (temp6 > 1f)
			{
				temp6 = 1f;
			}
			if (Progress.shop.ArenaNew)
			{
				yield return StartCoroutine(RubiesLerp(DistanceArena, _currentDist, 0f, temp6, DistArena));
			}
			if (Progress.shop.Arena1MaxDistance < Progress.shop.Arena1Distance)
			{
				Progress.shop.Arena1MaxDistance = Progress.shop.Arena1Distance;
			}
		}
		temp6 = (float)rubyCollectble / 1000f;
		if (temp6 < 0.5f)
		{
			temp6 = 0.5f;
		}
		else if (temp6 > 1f)
		{
			temp6 = 1f;
		}
		if (Progress.shop.ArenaNew)
		{
			yield return StartCoroutine(RubiesLerp(CollectableArena, rubyCollectble, 0f, temp6, CollArena));
		}
		else
		{
			yield return StartCoroutine(RubiesLerp(Collectable, rubyCollectble, 0f, temp6, Coll));
		}
		temp6 = (float)rubyDestroyEnemy / 1000f;
		if (temp6 < 0.5f)
		{
			temp6 = 0.5f;
		}
		else if (temp6 > 1f)
		{
			temp6 = 1f;
		}
		if (Progress.shop.ArenaNew)
		{
			yield return StartCoroutine(RubiesLerp(DestroysArena, rubyDestroyEnemy, 0f, temp6, DestArena));
		}
		else
		{
			yield return StartCoroutine(RubiesLerp(Destroys, rubyDestroyEnemy, 0f, temp6, Dest));
		}
		temp6 = (float)(rubyDestroyEnemy + rubyCollectble) / 1000f;
		if (temp6 < 0.5f)
		{
			temp6 = 0.5f;
		}
		else if (temp6 > 1f)
		{
			temp6 = 1f;
		}
		if (Progress.shop.ArenaNew)
		{
			yield return StartCoroutine(RubiesLerp(TotalArena, rubyDestroyEnemy + rubyCollectble, 0f, temp6, TotArena));
		}
		else
		{
			yield return StartCoroutine(RubiesLerp(Total, rubyDestroyEnemy + rubyCollectble, 0f, temp6, Tot));
		}
		yield return new WaitForSeconds(0.5f);
		if (percentForWin != 0f)
		{
			animatorForPercent.SetBool(_isOn, value: true);
			animValue.SetBool(_isActive, value: true);
			yield return new WaitForSeconds(1f);
			float temps = (float)(rubyDestroyEnemy + rubyCollectble) * (percentForWin / 100f);
			temp6 = ((float)(rubyDestroyEnemy + rubyCollectble) + temps) / 500f;
			if (temp6 < 0.5f)
			{
				temp6 = 0.5f;
			}
			else if (temp6 > 1f)
			{
				temp6 = 1f;
			}
			yield return StartCoroutine(RubiesLerp(Total, (float)(rubyDestroyEnemy + rubyCollectble) + temps, rubyDestroyEnemy + rubyCollectble, temp6, Tot));
		}
	}

	private IEnumerator RubiesLerp(CounterController l, float number, float buff, float time, Animator anim)
	{
		anim.SetBool(_isEnded, value: false);
		if (number > 0f)
		{
			Audio.PlayAsync("gui_scoring");
		}
		float buf = buff;
		float t = 0f;
		while (t < time)
		{
			t += Time.unscaledDeltaTime * 2f;
			buf = Mathf.Lerp(buf, number, t);
			l.count = ((int)buf).ToString();
			yield return null;
		}
		l.count = ((int)number).ToString();
		anim.SetBool(_isEnded, value: true);
	}
}
