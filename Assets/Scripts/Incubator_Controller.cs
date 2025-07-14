using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Incubator_Controller : MonoBehaviour
{
	[Serializable]
	public class EggClass
	{
		public GameObject Obj;

		public Animator Anim;

		public List<GameObject> Icos;
	}

	[Serializable]
	public class CarClass
	{
		public GameObject CarObj;

		public GameObject CarEggEmiterObj;

		public Animator CarEvo0Animator;

		public GameObject CarObj_Evo0;

		public GameObject CarObj_Evo1;

		public GameObject CarObj_Evo2;

		public GameObject CarObj_Evo3;

		public GameObject CarObj_Evo4;

		public Animator CarObj_Evo4Animator;
	}

	public Button BackBtn;

	public GameObject ByuNow;

	public GameObject ChooseEggBtnPane;

	public Button ChooseEggBtn;

	public GameObject PreloaderObj;

	[Header("ChooseEggPane")]
	public GameObject ChooseEggPane;

	public Button ChooseEggPane_CloseBtn;

	public Button ChooseEggPane_ChooseBtn;

	public GameObject ChooseEggPane_ChooseGray;

	public GameObject ChooseEggPane_AllEggsObj;

	public GameObject ChooseEggPane_BanerObj;

	public GameObject ChooseEggPane_BanerObj_loc2;

	public List<EggClass> Eggs;

	[Header("Cars")]
	public List<CarClass> Cars;

	[Header("Tube && Lamp")]
	public Animator CarTubeAnimator;

	public Animator CarTubeEvoAnimatorFront;

	public Animator CarTubeEvoAnimatorBack;

	public Animator CarLampAnimator;

	[Header("Bot Pane")]
	public Animator BotPineAnimator;

	public GameObject BotPineHatchObj;

	public Button BotPineHatchPushBut;

	public GameObject BotPineGetCarObj;

	public Button BotPineGetCarBtn;

	public GameObject BotPineHatchArrow;

	public Image BotPineHatchProgressBar;

	public Text BotPineHatchTime;

	public Text BotPineHatchTimeMultiplier;

	[Header("Bot Evo Pane")]
	public GameObject BotPineEvolutionObj;

	public Button BotPineEvolution_PlayBut;

	public List<GameObject> BotPineEvolution_ShopBut_ruby;

	public Button BotPineEvolution_ShopBut;

	public GameObject BotPineEvolution_PlayVideoObj;

	public Text BotPineEvolution_PlayVideoTime;

	public Button BotPineEvolution_PlayVideoBut;

	public List<Incubator_RubiProgresser> RubyesProgress;

	public List<Animator> ArrowProgress;

	[Header("Gems")]
	public Text Ruby1Counter;

	public Text Ruby2Counter;

	public Text Ruby3Counter;

	public Text Ruby4Counter;

	public Animator Ruby1NoGemsAnimator;

	public Animator Ruby2NoGemsAnimator;

	public Animator Ruby3NoGemsAnimator;

	public Animator Ruby4NoGemsAnimator;

	public Animator Ruby1DecrementAnimator;

	public Animator Ruby2DecrementAnimator;

	public Animator Ruby3DecrementAnimator;

	public Animator Ruby4DecrementAnimator;

	private int is_lampON = Animator.StringToHash("is_lampON");

	private int evolution_isOn = Animator.StringToHash("evolution_isOn");

	private int is_vortexON = Animator.StringToHash("is_vortexON");

	private int rate = Animator.StringToHash("rate");

	private int isOn = Animator.StringToHash("isOn");

	private int isState_1 = Animator.StringToHash("isState_1");

	private int isState_2 = Animator.StringToHash("isState_2");

	private int isState_3 = Animator.StringToHash("isState_3");

	private int isState_4 = Animator.StringToHash("isState_4");

	private int isState_5 = Animator.StringToHash("isState_5");

	private int isActive = Animator.StringToHash("isActive");

	private int isComplete = Animator.StringToHash("isComplete");

	private int isZero = Animator.StringToHash("isZero");

	private int is_active = Animator.StringToHash("is_active");

	private const string zero = "0";

	private const string dots = ":";

	private const string textX = "x";

	private const string textNull = "";

	private Coroutine TimeIncubationCorut;

	private Coroutine TimeToNextPlayCorut;

	private int _secsLeft;

	private int _currentEddEvoStage = -1;

	private int _lastEddEvoStage = -1;

	private int _currentMultiplier = 1;

	private int _lastMultiplier = 1;

	private float _arrowOffset;

	private int _nextPlayTime = 5;

	private int _incubationTime = 2;

	private float _arrowPlasToPress = 5f;

	private float _arrowMinusPerFrame = 0.01f;

	private int _multiplier1 = 2;

	private int _multiplier2 = 4;

	private int _multiplier3 = 6;

	private int _multiplier4 = 50;

	private int _multiplier1Start = 38;

	private int _multiplier2Start;

	private int _multiplier3Start = -30;

	private int _multiplier4Start = -67;

	private List<int> OrderRubyToUnlock;

	private int _lastChossenEggPane = -1;

	public GameObject IBN;

	private void Start()
	{
		if (Progress.shop.Incubator_CurrentEggNum == 2 && Progress.shop.Cars[9].equipped)
		{
			Progress.shop.Incubator_CurrentEggNum = -1;
			Progress.shop.Incubator_EvoStage = -1;
			Progress.shop.Incubator_EvoProgressStep = -1;
		}
		if (Progress.shop.Incubator_CurrentEggNum == 1 && Progress.shop.Cars[10].equipped)
		{
			Progress.shop.Incubator_CurrentEggNum = -1;
			Progress.shop.Incubator_EvoStage = -1;
			Progress.shop.Incubator_EvoProgressStep = -1;
		}
		if (Progress.shop.Incubator_CurrentEggNum == 0 && Progress.shop.Cars[8].equipped)
		{
			Progress.shop.Incubator_CurrentEggNum = -1;
			Progress.shop.Incubator_EvoStage = -1;
			Progress.shop.Incubator_EvoProgressStep = -1;
		}
		if (Progress.shop.Incubator_CurrentEggNum == 3 && Progress.shop.dronFireBuy)
		{
			Progress.shop.Incubator_CurrentEggNum = -1;
			Progress.shop.Incubator_EvoStage = -1;
			Progress.shop.Incubator_EvoProgressStep = -1;
		}
		if (Progress.shop.Incubator_RubySetActive == null || Progress.shop.Incubator_RubySetActive.Count <= 1)
		{
			Progress.shop.Incubator_RubySetActive = new List<bool>();
			Progress.shop.Incubator_RubySetCompleat = new List<bool>();
			for (int i = 0; i < 15; i++)
			{
				Progress.shop.Incubator_RubySetActive.Add(item: false);
				Progress.shop.Incubator_RubySetCompleat.Add(item: false);
			}
		}
		_secsLeft = -1;
		if (Progress.shop.Incubator_CurrentEggNum == -1)
		{
			PanelsStart();
		}
		else
		{
			SepParams();
			ChooseEggBtnPane.SetActive(value: false);
			ChooseEggPane.SetActive(value: false);
			if ((Progress.shop.Incubator_RubySetCompleat[3] && Progress.shop.Incubator_EvoStage == 1) || (Progress.shop.Incubator_RubySetCompleat[8] && Progress.shop.Incubator_EvoStage == 2) || (Progress.shop.Incubator_RubySetCompleat[14] && Progress.shop.Incubator_EvoStage == 3))
			{
				if (Progress.shop.Incubator_Time == DateTime.MinValue)
				{
					Progress.shop.Incubator_Time = DateTime.UtcNow;
				}
				PanelsStart(kliker: true);
				TimeIncubationCorut = StartCoroutine(TimerIncubationCorutine());
			}
			else if (GetTimeIncubation() <= 0)
			{
				if (Progress.shop.Incubator_EvoStage == 0)
				{
					Progress.shop.Incubator_EvoProgressStep = 0;
					StartCoroutine(EggScarsDelay());
					Progress.shop.Incubator_EvoStage = 1;
				}
				PanelsStart();
			}
			else
			{
				PanelsStart();
				TimeIncubationCorut = StartCoroutine(TimerIncubationCorutine());
			}
			StartCar();
		}
		BackBtn.onClick.AddListener(BackPress);
		ChooseEggBtn.onClick.AddListener(ChooseEggPress);
		ChooseEggPane_CloseBtn.onClick.AddListener(ChooseEggPaneClosePress);
		ChooseEggPane_ChooseBtn.onClick.AddListener(ChooseEggPaneChoosePress);
		BotPineHatchPushBut.onClick.AddListener(BotPineHatchPushButPress);
		BotPineEvolution_PlayBut.onClick.AddListener(BotPineEvolution_PlayPress);
		BotPineEvolution_PlayVideoBut.onClick.AddListener(BotPineEvolution_PlayVideoPress);
		BotPineEvolution_ShopBut.onClick.AddListener(BotPineEvolution_ShopPress);
		BotPineGetCarBtn.onClick.AddListener(BotPine_GetCarBtnPress);
		StartCoroutine(preloderHide());
	}

	private IEnumerator preloderHide()
	{
		yield return new WaitForSeconds(1f);
		PreloaderCanvas q = PreloaderObj.GetComponent<PreloaderCanvas>();
		yield return new WaitForSeconds(1f);
		if (q != null)
		{
			q.Hide();
		}
		PreloaderObj.SetActive(value: false);
	}

	private void SepParams()
	{
		ConfigIncubator.Eegs.Evo currentIncubation = ConfigIncubator.instance.GetCurrentIncubation();
		OrderRubyToUnlock = ConfigIncubator.instance.GerOrderRubyToUnlock();
		_nextPlayTime = ConfigIncubator.instance.GerNextPlayTime();
		_incubationTime = currentIncubation.IncubationTime;
		_arrowPlasToPress = currentIncubation.ArrowPlasToPress;
		_arrowMinusPerFrame = currentIncubation.ArrowMinusPerFrame;
		_multiplier1 = currentIncubation.Multiplier1;
		_multiplier2 = currentIncubation.Multiplier2;
		_multiplier3 = currentIncubation.Multiplier3;
		_multiplier4 = currentIncubation.Multiplier4;
		_multiplier1Start = currentIncubation.Multiplier1Start;
		_multiplier2Start = currentIncubation.Multiplier2Start;
		_multiplier3Start = currentIncubation.Multiplier3Start;
		_multiplier4Start = currentIncubation.Multiplier4Start;
	}

	private IEnumerator TimerIncubationCorutine()
	{
		while (true)
		{
			int min = GetTimeIncubation() / 60;
			int sec = GetTimeIncubation() - min * 60;
			if (min <= 0 && sec <= 0)
			{
				break;
			}
			if (min <= 9)
			{
				BotPineHatchTime.text = "0" + min.ToString();
			}
			else
			{
				BotPineHatchTime.text = min.ToString();
			}
			if (sec <= 9)
			{
				Text botPineHatchTime = BotPineHatchTime;
				botPineHatchTime.text = botPineHatchTime.text + ":0" + sec.ToString();
			}
			else
			{
				Text botPineHatchTime2 = BotPineHatchTime;
				botPineHatchTime2.text = botPineHatchTime2.text + ":" + sec.ToString();
			}
			float z = (float)(min * 60 + sec) / (float)(_incubationTime * 60);
			BotPineHatchProgressBar.fillAmount = 1f - z;
			float t = 1f;
			while (t > 0f)
			{
				t -= Time.deltaTime * (float)_currentMultiplier;
				yield return null;
			}
			_secsLeft--;
		}
		if (Progress.shop.Incubator_EvoStage < 4)
		{
			if (Progress.shop.Incubator_EvoStage == 0)
			{
				Progress.shop.Incubator_EvoProgressStep = 0;
				StartCoroutine(EggScarsDelay());
			}
			Progress.shop.Incubator_EvoStage++;
			SepParams();
			StartCar();
			PanelsStart(kliker: false, wisualCheng: true);
			Progress.shop.Incubator_Time = DateTime.MinValue;
		}
	}

	private IEnumerator EggScarsDelay()
	{
		Audio.PlayAsync("baby_bug");
		Cars[Progress.shop.Incubator_CurrentEggNum].CarEggEmiterObj.SetActive(value: true);
		float t = 5f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return null;
		}
		Cars[Progress.shop.Incubator_CurrentEggNum].CarEggEmiterObj.SetActive(value: false);
	}

	public int GetTimeIncubation()
	{
		if (_secsLeft == -1)
		{
			_secsLeft = _incubationTime * 60 - (int)(DateTime.UtcNow - Progress.shop.Incubator_Time).TotalSeconds;
		}
		if (Progress.shop.Incubator_Time != DateTime.MinValue)
		{
			Progress.shop.Incubator_Time = DateTime.UtcNow.AddSeconds(-(_incubationTime * 60 - _secsLeft));
		}
		if (Progress.shop.Incubator_EvoStage == 0 && _secsLeft > 0)
		{
			int num = _secsLeft / (_incubationTime * 60 / 100);
			if (num < 80)
			{
				Cars[Progress.shop.Incubator_CurrentEggNum].CarEvo0Animator.SetBool(isState_2, value: true);
			}
			if (num < 60)
			{
				Cars[Progress.shop.Incubator_CurrentEggNum].CarEvo0Animator.SetBool(isState_3, value: true);
			}
			if (num < 40)
			{
				Cars[Progress.shop.Incubator_CurrentEggNum].CarEvo0Animator.SetBool(isState_4, value: true);
			}
			if (num < 20)
			{
				Cars[Progress.shop.Incubator_CurrentEggNum].CarEvo0Animator.SetBool(isState_5, value: true);
			}
		}
		return _secsLeft;
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

	private void PanelsStart(bool kliker = false, bool wisualCheng = false)
	{
		ChooseEggBtnPane.SetActive(value: false);
		ChooseEggPane.SetActive(value: false);
		BotPineEvolutionObj.SetActive(value: false);
		if (!wisualCheng)
		{
			BotPineHatchObj.SetActive(value: false);
		}
		BotPineGetCarObj.SetActive(value: false);
		ChooseEggBtnPane.SetActive(value: false);
		ChooseEggPane_AllEggsObj.SetActive(value: false);
		ChooseEggPane_BanerObj.SetActive(value: false);
		switch (Progress.shop.Incubator_EvoStage)
		{
		case -1:
		{
			ChooseEggBtnPane.SetActive(value: true);
			int count = Cars.Count;
			for (int i = 0; i < count; i++)
			{
				Cars[i].CarObj.SetActive(value: false);
			}
			int num = 0;
			bool flag = false;
			count = Progress.shop.Incubator_Eggs.Count;
			for (int j = 0; j < count; j++)
			{
				if (Progress.shop.Incubator_Eggs[j])
				{
					int count2 = Eggs[num].Icos.Count;
					for (int k = 0; k < count2; k++)
					{
						Eggs[num].Icos[k].SetActive(value: false);
					}
					Eggs[num].Obj.SetActive(value: true);
					Eggs[num].Icos[j].SetActive(value: true);
					flag = true;
					num++;
				}
			}
			if (!flag)
			{
				if (Progress.shop.Cars[9].equipped && Progress.shop.Cars[10].equipped)
				{
					ChooseEggPane_BanerObj_loc2.SetActive(value: true);
					ChooseEggPane_BanerObj.SetActive(value: false);
				}
				else
				{
					ChooseEggPane_BanerObj_loc2.SetActive(value: false);
					ChooseEggPane_BanerObj.SetActive(value: true);
				}
			}
			else
			{
				ChooseEggPane_AllEggsObj.SetActive(value: true);
			}
			break;
		}
		case 0:
			CarLampAnimator.SetBool(is_lampON, value: true);
			BotPineAnimator.SetBool(isOn, value: true);
			BotPineHatchObj.SetActive(value: true);
			break;
		case 1:
			if (kliker)
			{
				Utilities.instance.RunAfterTime(3f, delegate
				{
					CarTubeAnimator.SetBool(evolution_isOn, value: true);
				});
				StartCoroutine(DelayToAnimInTube());
				BotPineAnimator.SetBool(isOn, value: true);
				BotPineHatchObj.SetActive(value: true);
			}
			else
			{
				CarLampAnimator.SetBool(is_lampON, value: false);
				StartCoroutine(InitEvoPanelCorut(wisualCheng));
			}
			break;
		case 2:
			if (kliker)
			{
				Utilities.instance.RunAfterTime(3f, delegate
				{
					CarTubeAnimator.SetBool(evolution_isOn, value: true);
				});
				StartCoroutine(DelayToAnimInTube());
				BotPineAnimator.SetBool(isOn, value: true);
				BotPineHatchObj.SetActive(value: true);
			}
			else
			{
				CarTubeAnimator.SetBool(evolution_isOn, value: false);
				StartCoroutine(InitEvoPanelCorut(wisualCheng));
			}
			break;
		case 3:
			if (kliker)
			{
				Utilities.instance.RunAfterTime(3f, delegate
				{
					CarTubeAnimator.SetBool(evolution_isOn, value: true);
				});
				StartCoroutine(DelayToAnimInTube());
				BotPineAnimator.SetBool(isOn, value: true);
				BotPineHatchObj.SetActive(value: true);
			}
			else
			{
				CarTubeAnimator.SetBool(evolution_isOn, value: false);
				StartCoroutine(InitEvoPanelCorut(wisualCheng));
			}
			break;
		case 4:
			IBN.SetActive(value: false);
			StartCoroutine(DelayNew());
			break;
		}
	}

	private IEnumerator DelayNew()
	{
		switch (Progress.shop.Incubator_CurrentEggNum)
		{
		case 5:
			AnalyticsManager.LogEvent(EventCategoty.incubator, "car_crafted", "turtles");
			break;
		case 4:
			AnalyticsManager.LogEvent(EventCategoty.incubator, "car_crafted", "alligator");
			break;
		case 2:
			AnalyticsManager.LogEvent(EventCategoty.incubator, "car_crafted", "scorpion");
			break;
		case 1:
			AnalyticsManager.LogEvent(EventCategoty.incubator, "car_crafted", "cockchafer");
			break;
		case 3:
			AnalyticsManager.LogEvent(EventCategoty.incubator, "car_crafted", "firefly");
			break;
		}
		BotPineAnimator.SetBool(isOn, value: false);
		BotPineGetCarObj.SetActive(value: false);
		CarTubeAnimator.SetBool(evolution_isOn, value: false);
		float t = 0.5f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return null;
		}
		BotPineAnimator.SetBool(isOn, value: true);
		BotPineGetCarObj.SetActive(value: true);
		BotPineHatchObj.SetActive(value: false);
		BotPineEvolutionObj.SetActive(value: false);
	}

	private IEnumerator DelayToAnimInTube()
	{
		float t = 2f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			CarTubeEvoAnimatorFront.SetBool(is_vortexON, value: true);
			CarTubeEvoAnimatorBack.SetBool(is_vortexON, value: true);
			yield return null;
		}
	}

	private IEnumerator InitEvoPanelCorut(bool wisualCheng = false)
	{
		float t2;
		if (wisualCheng)
		{
			BotPineAnimator.SetBool(isOn, value: false);
			t2 = 1f;
			while (t2 > 0f)
			{
				t2 -= Time.deltaTime;
				yield return null;
			}
		}
		BotPineAnimator.SetBool(isOn, value: true);
		BotPineHatchObj.SetActive(value: false);
		BotPineEvolutionObj.SetActive(value: true);
		t2 = 0.1f;
		while (t2 > 0f)
		{
			t2 -= Time.deltaTime;
			yield return null;
		}
		int count2 = RubyesProgress.Count;
		for (int i = 0; i < count2; i++)
		{
			RubyesProgress[i].SetRuby(OrderRubyToUnlock[i]);
		}
		if (Progress.shop.Incubator_EvoStage > 1)
		{
			ArrowProgress[0].SetBool(isComplete, value: true);
		}
		if (Progress.shop.Incubator_EvoStage > 2)
		{
			ArrowProgress[1].SetBool(isComplete, value: true);
		}
		if (Progress.shop.Incubator_EvoStage > 3)
		{
			ArrowProgress[2].SetBool(isComplete, value: true);
		}
		count2 = RubyesProgress.Count;
		for (int j = 0; j < count2; j++)
		{
			if (Progress.shop.Incubator_RubySetCompleat[j])
			{
				RubyesProgress[j].CompleteAnim();
			}
		}
		if (!Progress.shop.Incubator_RubySetActive[Progress.shop.Incubator_EvoProgressStep])
		{
			RubyesProgress[Progress.shop.Incubator_EvoProgressStep].UnlockAnim();
		}
		else if (!Progress.shop.Incubator_RubySetCompleat[Progress.shop.Incubator_EvoProgressStep])
		{
			RubyesProgress[Progress.shop.Incubator_EvoProgressStep].ActiveAnim();
		}
		BotPineEvolution_PlayBut.gameObject.SetActive(value: false);
		BotPineEvolution_ShopBut.gameObject.SetActive(value: false);
		BotPineEvolution_PlayVideoObj.SetActive(value: false);
		SetRubyCounters();
		BotPineEvolution_ShopBut_ruby[OrderRubyToUnlock[Progress.shop.Incubator_EvoProgressStep] - 1].SetActive(value: true);
		if (!Progress.shop.Incubator_RubySetActive[Progress.shop.Incubator_EvoProgressStep])
		{
			switch (OrderRubyToUnlock[Progress.shop.Incubator_EvoProgressStep])
			{
			case 1:
				if (Progress.shop.Incubator_CountRuby1 > 0)
				{
					StartCoroutine(DelayToRubyMove());
					Progress.shop.Incubator_CountRuby1--;
				}
				else
				{
					BotPineEvolution_ShopBut.gameObject.SetActive(value: true);
				}
				break;
			case 2:
				if (Progress.shop.Incubator_CountRuby2 > 0)
				{
					StartCoroutine(DelayToRubyMove());
					Progress.shop.Incubator_CountRuby2--;
				}
				else
				{
					BotPineEvolution_ShopBut.gameObject.SetActive(value: true);
				}
				break;
			case 3:
				if (Progress.shop.Incubator_CountRuby3 > 0)
				{
					StartCoroutine(DelayToRubyMove());
					Progress.shop.Incubator_CountRuby3--;
				}
				else
				{
					BotPineEvolution_ShopBut.gameObject.SetActive(value: true);
				}
				break;
			case 4:
				if (Progress.shop.Incubator_CountRuby4 > 0)
				{
					StartCoroutine(DelayToRubyMove());
					Progress.shop.Incubator_CountRuby4--;
				}
				else
				{
					BotPineEvolution_ShopBut.gameObject.SetActive(value: true);
				}
				break;
			}
		}
		else
		{
			ButsPlays();
		}
		yield return null;
	}

	private void SetRubyCounters(bool dec = false)
	{
		if (dec)
		{
			StartCoroutine(DecAnimDelay());
		}
		int num = OrderRubyToUnlock[Progress.shop.Incubator_EvoProgressStep];
		bool flag = Progress.shop.Incubator_RubySetActive[Progress.shop.Incubator_EvoProgressStep];
		if (Progress.shop.Incubator_CountRuby1 > 0)
		{
			Ruby1Counter.text = "x" + Progress.shop.Incubator_CountRuby1.ToString();
		}
		else
		{
			Ruby1Counter.text = string.Empty;
			Ruby1NoGemsAnimator.SetBool(isZero, value: true);
			if (!flag && num == 1)
			{
				Ruby1NoGemsAnimator.SetBool(isActive, value: true);
			}
		}
		if (Progress.shop.Incubator_CountRuby2 > 0)
		{
			Ruby2Counter.text = "x" + Progress.shop.Incubator_CountRuby2.ToString();
		}
		else
		{
			Ruby2Counter.text = string.Empty;
			Ruby2NoGemsAnimator.SetBool(isZero, value: true);
			if (!flag && num == 2)
			{
				Ruby2NoGemsAnimator.SetBool(isActive, value: true);
			}
		}
		if (Progress.shop.Incubator_CountRuby3 > 0)
		{
			Ruby3Counter.text = "x" + Progress.shop.Incubator_CountRuby3.ToString();
		}
		else
		{
			Ruby3Counter.text = string.Empty;
			Ruby3NoGemsAnimator.SetBool(isZero, value: true);
			if (!flag && num == 3)
			{
				Ruby3NoGemsAnimator.SetBool(isActive, value: true);
			}
		}
		if (Progress.shop.Incubator_CountRuby4 > 0)
		{
			Ruby4Counter.text = "x" + Progress.shop.Incubator_CountRuby4.ToString();
			return;
		}
		Ruby4Counter.text = string.Empty;
		Ruby4NoGemsAnimator.SetBool(isZero, value: true);
		if (!flag && num == 4)
		{
			Ruby4NoGemsAnimator.SetBool(isActive, value: true);
		}
	}

	private IEnumerator DecAnimDelay()
	{
		int activeGem = OrderRubyToUnlock[Progress.shop.Incubator_EvoProgressStep];
		switch (activeGem)
		{
		case 1:
			Ruby1DecrementAnimator.SetBool(isOn, value: true);
			break;
		case 2:
			Ruby2DecrementAnimator.SetBool(isOn, value: true);
			break;
		case 3:
			Ruby3DecrementAnimator.SetBool(isOn, value: true);
			break;
		case 4:
			Ruby4DecrementAnimator.SetBool(isOn, value: true);
			break;
		}
		float t = 0.5f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return null;
		}
		switch (activeGem)
		{
		case 1:
			Ruby1DecrementAnimator.SetBool(isOn, value: false);
			break;
		case 2:
			Ruby2DecrementAnimator.SetBool(isOn, value: false);
			break;
		case 3:
			Ruby3DecrementAnimator.SetBool(isOn, value: false);
			break;
		case 4:
			Ruby4DecrementAnimator.SetBool(isOn, value: false);
			break;
		}
	}

	private IEnumerator TimerToNextPlay()
	{
		while (true)
		{
			int min = GetTimeNextPlay() / 60;
			int sec = GetTimeNextPlay() - min * 60;
			if (min <= 0 && sec <= 0)
			{
				break;
			}
			if (min <= 9)
			{
				BotPineEvolution_PlayVideoTime.text = "0" + min.ToString();
			}
			else
			{
				BotPineEvolution_PlayVideoTime.text = min.ToString();
			}
			if (sec <= 9)
			{
				Text botPineEvolution_PlayVideoTime = BotPineEvolution_PlayVideoTime;
				botPineEvolution_PlayVideoTime.text = botPineEvolution_PlayVideoTime.text + ":0" + sec.ToString();
			}
			else
			{
				Text botPineEvolution_PlayVideoTime2 = BotPineEvolution_PlayVideoTime;
				botPineEvolution_PlayVideoTime2.text = botPineEvolution_PlayVideoTime2.text + ":" + sec.ToString();
			}
			float t = 1f;
			while (t > 0f)
			{
				t -= Time.deltaTime;
				yield return null;
			}
		}
		ButsPlays();
		TimeToNextPlayCorut = null;
	}

	public int GetTimeNextPlay()
	{
		return _nextPlayTime * 60 - (int)(DateTime.UtcNow - Progress.shop.Incubator_LastPlay).TotalSeconds;
	}

	private void ButsPlays()
	{
		if (GetTimeNextPlay() <= 0)
		{
			BotPineEvolution_PlayBut.gameObject.SetActive(value: true);
			BotPineEvolution_PlayVideoObj.SetActive(value: false);
		}
		else if (TimeToNextPlayCorut == null)
		{
			BotPineEvolution_PlayBut.gameObject.SetActive(value: false);
			BotPineEvolution_PlayVideoObj.SetActive(value: true);
			TimeToNextPlayCorut = StartCoroutine(TimerToNextPlay());
		}
	}

	private IEnumerator DelayToRubyMove()
	{
		float t = 1f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return null;
		}
		Progress.shop.Incubator_RubySetActive[Progress.shop.Incubator_EvoProgressStep] = true;
		RubyesProgress[Progress.shop.Incubator_EvoProgressStep].ActiveAnim();
		SetRubyCounters(dec: true);
		ButsPlays();
		BotPineEvolution_ShopBut.gameObject.SetActive(value: false);
	}

	private void StartCar()
	{
		for (int i = 0; i < Cars.Count; i++)
		{
			Cars[i].CarObj.SetActive(value: false);
		}
		Cars[Progress.shop.Incubator_CurrentEggNum].CarObj.SetActive(value: true);
		Cars[Progress.shop.Incubator_CurrentEggNum].CarObj_Evo1.transform.parent.parent.gameObject.SetActive(value: true);
		Cars[Progress.shop.Incubator_CurrentEggNum].CarObj_Evo0.SetActive(value: false);
		Cars[Progress.shop.Incubator_CurrentEggNum].CarObj_Evo1.SetActive(value: false);
		Cars[Progress.shop.Incubator_CurrentEggNum].CarObj_Evo2.SetActive(value: false);
		Cars[Progress.shop.Incubator_CurrentEggNum].CarObj_Evo3.SetActive(value: false);
		Cars[Progress.shop.Incubator_CurrentEggNum].CarObj_Evo4.SetActive(value: false);
		switch (Progress.shop.Incubator_EvoStage)
		{
		case 0:
			Cars[Progress.shop.Incubator_CurrentEggNum].CarObj_Evo0.SetActive(value: true);
			break;
		case 1:
			Cars[Progress.shop.Incubator_CurrentEggNum].CarObj_Evo1.SetActive(value: true);
			StartCoroutine(sound());
			break;
		case 2:
			Cars[Progress.shop.Incubator_CurrentEggNum].CarObj_Evo2.SetActive(value: true);
			StartCoroutine(sound());
			break;
		case 3:
			Cars[Progress.shop.Incubator_CurrentEggNum].CarObj_Evo3.SetActive(value: true);
			StartCoroutine(sound());
			break;
		case 4:
			Cars[Progress.shop.Incubator_CurrentEggNum].CarObj_Evo4.SetActive(value: true);
			Cars[Progress.shop.Incubator_CurrentEggNum].CarObj_Evo4Animator.SetBool(isComplete, value: true);
			if (Progress.shop.Incubator_CurrentEggNum == 3)
			{
				Progress.shop.dronFireEvolFin = true;
			}
			break;
		}
	}

	private IEnumerator sound()
	{
		yield return new WaitForSecondsRealtime(3f);
		Audio.Play("baby_bug");
	}

	private void BotPineHatchPushButPress()
	{
		_arrowOffset += _arrowPlasToPress;
		if (_arrowOffset > 160f)
		{
			_arrowOffset = 160f;
		}
	}

	private void Update()
	{
		if (_arrowOffset > 0f)
		{
			_arrowOffset -= _arrowMinusPerFrame;
			if (_arrowOffset < 0f)
			{
				_arrowOffset = 0f;
			}
			BotPineHatchArrow.transform.localRotation = Quaternion.Euler(0f, 0f, 80f - _arrowOffset);
			CarLampAnimator.SetInteger(rate, (int)(0.625f * _arrowOffset));
			CarTubeEvoAnimatorFront.SetInteger(rate, (int)(0.625f * _arrowOffset));
			CarTubeEvoAnimatorBack.SetInteger(rate, (int)(0.625f * _arrowOffset));
			GetMultiplier();
		}
		if (Progress.shop.Incubator_CurrentEggNum != -1)
		{
			ByuNow.SetActive(value: true);
		}
		else
		{
			ByuNow.SetActive(value: false);
		}
	}

	private void GetMultiplier()
	{
		if (80f - _arrowOffset <= (float)_multiplier4Start)
		{
			_currentMultiplier = _multiplier4;
		}
		else if (80f - _arrowOffset <= (float)_multiplier3Start)
		{
			_currentMultiplier = _multiplier3;
		}
		else if (80f - _arrowOffset <= (float)_multiplier2Start)
		{
			_currentMultiplier = _multiplier2;
		}
		else if (80f - _arrowOffset <= (float)_multiplier1Start)
		{
			_currentMultiplier = _multiplier1;
		}
		else
		{
			_currentMultiplier = 1;
		}
		if (_currentMultiplier == _lastMultiplier)
		{
			return;
		}
		if (_currentMultiplier == 1)
		{
			if (Progress.shop.Incubator_EvoStage == 0)
			{
				Cars[Progress.shop.Incubator_CurrentEggNum].CarEvo0Animator.SetBool(isActive, value: false);
			}
			BotPineHatchTimeMultiplier.gameObject.SetActive(value: false);
		}
		if (_currentMultiplier == _multiplier1)
		{
			if (Progress.shop.Incubator_EvoStage == 0)
			{
				Cars[Progress.shop.Incubator_CurrentEggNum].CarEvo0Animator.SetBool(isActive, value: true);
			}
			BotPineHatchTimeMultiplier.gameObject.SetActive(value: true);
		}
		BotPineHatchTimeMultiplier.text = "x" + _currentMultiplier.ToString();
		_lastMultiplier = _currentMultiplier;
	}

	private void BotPineEvolution_PlayPress()
	{
		Progress.shop.Incubator_LastPlay = DateTime.UtcNow;
		SceneManager.LoadScene("scene_miniGame_incubator");
	}

	private void BotPineEvolution_PlayVideoPress()
	{
		Action Act = delegate
		{
			BotPineEvolution_PlayVideoBut.interactable = true;
			BotPineEvolution_PlayPress();
			AnalyticsManager.LogEvent(EventCategoty.video, "ads_watched", "incubator");
		};
		BotPineEvolution_PlayVideoBut.interactable = false;
		NewWrapperForAppodeal.instance.ShowVideo(delegate(bool sucsess)
		{
			if (sucsess)
			{
				Act();
				BotPineEvolution_PlayVideoBut.interactable = true;
			}
			else
			{
				BotPineEvolution_PlayVideoBut.interactable = true;
			}
		}, delegate
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("noVideoBox", typeof(GameObject))) as GameObject;
			BotPineEvolution_PlayVideoBut.interactable = true;
		}, delegate
		{
			BotPineEvolution_PlayVideoBut.interactable = true;
		});
	}

	private void BotPineEvolution_ShopPress()
	{
		Progress.shop.EsterShowOnMap = true;
		Progress.levels.InUndeground = true;
		Progress.shop.TestFor9 = true;
		if (Progress.levels.InUndeground)
		{
			Game.LoadLevel("scene_underground_map_new");
		}
		else
		{
			Game.LoadLevel("map_new");
		}
	}

	private void BackPress()
	{
		if (!Progress.shop.Cars[Progress.shop.activeCar].equipped)
		{
			Progress.shop.activeCar = 0;
		}
		if (Progress.levels.InUndeground)
		{
			Progress.shop.TestFor9 = true;
			Game.LoadLevel("scene_underground_map_new");
		}
		else
		{
			Game.LoadLevel("map_new");
		}
	}

	private void BotPine_GetCarBtnPress()
	{
		Progress.shop.Incubator_RubySetActive.Clear();
		Progress.shop.Incubator_RubySetCompleat.Clear();
		Progress.shop.Incubator_EvoStage = -1;
		Progress.shop.Incubator_EvoProgressStep = -1;
		if (Progress.shop.Incubator_CurrentEggNum == 0)
		{
			Progress.shop.Cars[8].equipped = true;
			Progress.shop.activeCar = 8;
		}
		else if (Progress.shop.Incubator_CurrentEggNum == 1)
		{
			Progress.shop.Cars[10].equipped = true;
			Progress.shop.activeCar = 10;
		}
		else if (Progress.shop.Incubator_CurrentEggNum == 2)
		{
			Progress.shop.Cars[9].equipped = true;
			Progress.shop.activeCar = 9;
		}
		else if (Progress.shop.Incubator_CurrentEggNum == 4)
		{
			Progress.shop.Cars[12].equipped = true;
			Progress.shop.Cars[12].bought = true;
			Progress.shop.activeCar = 12;
		}
		else if (Progress.shop.Incubator_CurrentEggNum == 5)
		{
			Progress.shop.Cars[13].equipped = true;
			Progress.shop.Cars[13].bought = true;
			Progress.shop.activeCar = 13;
		}
		Progress.shop.Incubator_CurrentEggNum = -1;
		if (Game.currentState != Game.gameState.Shop)
		{
			Game.OnStateChange(Game.gameState.Shop);
		}
		UnityEngine.SceneManagement.SceneManager.LoadScene("garage_new");
	}

	private void ChooseEggPress()
	{
		ChooseEggPane.SetActive(value: true);
		ChooseEggBtnPane.SetActive(value: false);
		ChooseEggPane_ChooseBtn.gameObject.SetActive(value: false);
		ChooseEggPane_ChooseGray.SetActive(value: true);
	}

	private void ChooseEggPaneClosePress()
	{
		ChooseEggPane.SetActive(value: false);
		ChooseEggBtnPane.SetActive(value: true);
	}

	private void ChooseEggPaneChoosePress()
	{
		int count = Eggs[_lastChossenEggPane].Icos.Count;
		for (int i = 0; i < count; i++)
		{
			if (Eggs[_lastChossenEggPane].Icos[i].activeSelf)
			{
				Progress.shop.Incubator_CurrentEggNum = i;
				Progress.shop.Incubator_Eggs[i] = false;
				break;
			}
		}
		Progress.shop.Incubator_EvoStage = 0;
		ChooseEggPaneClosePress();
		SepParams();
		ChooseEggBtnPane.SetActive(value: false);
		StartCar();
		PanelsStart();
		Progress.shop.Incubator_Time = DateTime.UtcNow;
		TimeIncubationCorut = StartCoroutine(TimerIncubationCorutine());
	}

	public void ChooseEggPaneEggPress(int eggNum)
	{
		for (int i = 0; i < Eggs.Count; i++)
		{
			Eggs[i].Anim.SetBool(is_active, value: false);
		}
		_lastChossenEggPane = eggNum;
		Eggs[eggNum].Anim.SetBool(is_active, value: true);
		ChooseEggPane_ChooseBtn.gameObject.SetActive(value: true);
		ChooseEggPane_ChooseGray.SetActive(value: false);
	}

	private void OnDisable()
	{
		if (TimeIncubationCorut != null)
		{
			StopCoroutine(TimeIncubationCorut);
			TimeIncubationCorut = null;
		}
		if (TimeToNextPlayCorut != null)
		{
			StopCoroutine(TimeToNextPlayCorut);
			TimeToNextPlayCorut = null;
		}
		BackBtn.onClick.RemoveAllListeners();
		ChooseEggBtn.onClick.RemoveAllListeners();
		ChooseEggPane_CloseBtn.onClick.RemoveAllListeners();
		ChooseEggPane_ChooseBtn.onClick.RemoveAllListeners();
		BotPineHatchPushBut.onClick.RemoveAllListeners();
		BotPineEvolution_PlayBut.onClick.RemoveAllListeners();
		BotPineEvolution_PlayVideoBut.onClick.RemoveAllListeners();
		BotPineEvolution_ShopBut.onClick.RemoveAllListeners();
		BotPineGetCarBtn.onClick.RemoveAllListeners();
	}
}
