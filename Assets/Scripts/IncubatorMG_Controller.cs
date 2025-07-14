using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncubatorMG_Controller : MonoBehaviour
{
	public enum Tupe
	{
		none = 0,
		CarCivil = 1,
		CarCop = 4,
		Stone = 2,
		Decor = 3,
		Star = 5
	}

	public List<IncubatorMG_PlayerCar> Cars;

	[HideInInspector]
	public IncubatorMG_PlayerCar PlayerCar;

	public IncubatorMG_GroundMover Ground;

	public IncubatorMG_GroundMover Back;

	public IncubatorMG_Pool PoolThis;

	public IncubatorMG_Config Config;

	public IncubatorMG_UI Ui;

	public Animator WarningForLoseAnim;

	public GameObject WinParticls;

	[HideInInspector]
	public bool TutorialShow = true;

	[HideInInspector]
	public int CurrentStars;

	[HideInInspector]
	public float Speed;

	[HideInInspector]
	public bool PauseOn;

	public GameObject PreloaderObj;

	[Header("Marckers To System")]
	public Transform StartOnLineTop;

	public Transform StartOnLineCenter;

	public Transform StartOnLineBot;

	public Transform DestroyMarkerLeft;

	public Transform DestroyMarkerRight;

	public Transform StartUnderLine1;

	public Transform StartUnderLine2;

	public Transform StartUnderLine3;

	public Transform StartUnderLine4;

	private Coroutine StarPatternCorutine;

	private int _countOnCycle = 60;

	private int _currentStage;

	private int _currentCycleNum;

	private int _tCounter;

	private int _tCounterDecor;

	private int _countOnCycleDecor;

	private int _numLineToGnrNow = -1;

	private int is_ON = Animator.StringToHash("is_ON");

	private void Start()
	{
		Audio.StopBackgroundMusic();
		Audio.PlayBackgroundMusic("music_gameplay1");
		for (int i = 0; i < Cars.Count; i++)
		{
			Cars[i].gameObject.SetActive(value: false);
		}
		Cars[Progress.shop.Incubator_CurrentEggNum].gameObject.SetActive(value: true);
		PlayerCar = Cars[Progress.shop.Incubator_CurrentEggNum];
		StarPatternCorutine = null;
		CurrentStars = 0;
		_currentStage = 0;
		Speed = Config.GetStage()[0].Speed;
		_countOnCycle = Config.GetStage()[0].NumOnCycle;
		Ground.Speed = Speed / 7f;
		Back.Speed = Speed / 14f;
		if (CurrentStars <= 1)
		{
			WarningForLoseAnim.SetBool(is_ON, value: true);
		}
		else
		{
			WarningForLoseAnim.SetBool(is_ON, value: false);
		}
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

	public bool OutOfScope(Vector3 vec)
	{
		float x = vec.x;
		Vector3 position = DestroyMarkerLeft.position;
		if (!(x < position.x))
		{
			float x2 = vec.x;
			Vector3 position2 = DestroyMarkerRight.position;
			if (!(x2 > position2.x))
			{
				return false;
			}
		}
		return true;
	}

	public void PressMoveCar(bool up)
	{
		if (PlayerCar.gameObject.activeSelf)
		{
			PlayerCar.SetMove(up);
		}
	}

	public void OnTrigerEnter(IncubatorMG_InterectObj control)
	{
		switch (control.Tupe)
		{
		case Tupe.Star:
			CurrentStars++;
			Audio.PlayAsync("bns_rubi_pickup_sn");
			break;
		case Tupe.CarCop:
			CurrentStars -= Config.GetCopDmgStars();
			Audio.PlayAsync("crash_car_01_sn");
			break;
		case Tupe.CarCivil:
			CurrentStars -= Config.GetCivilDmgStars();
			Audio.PlayAsync("crash_car_01_sn");
			break;
		case Tupe.Stone:
			CurrentStars -= Config.GetStoneDmgStars();
			Audio.PlayAsync("crash_wood_01_sn");
			break;
		}
		if (control.Tupe != Tupe.Decor)
		{
			control.StarObj.gameObject.SetActive(value: false);
			for (int i = 0; i < control.CarsCivil.Count; i++)
			{
				control.CarsCivil[i].OnObjectForDeath.gameObject.SetActive(value: true);
				control.CarsCivil[i].OffObjectForDeath.gameObject.SetActive(value: false);
			}
			for (int j = 0; j < control.CarsCop.Count; j++)
			{
				control.CarsCop[j].OnObjectForDeath.gameObject.SetActive(value: true);
				control.CarsCop[j].OffObjectForDeath.gameObject.SetActive(value: false);
			}
		}
		for (int k = 0; k < control.Stones.Count; k++)
		{
			control.Stones[k].OnObjectForDeath.gameObject.SetActive(value: true);
			control.Stones[k].OffObjectForDeath.gameObject.SetActive(value: false);
		}
		control.SetDestroy();
		if (CurrentStars <= 1)
		{
			WarningForLoseAnim.SetBool(is_ON, value: true);
		}
		else
		{
			WarningForLoseAnim.SetBool(is_ON, value: false);
		}
		if (CurrentStars <= 0)
		{
			Ui.resPanel.SetActive(value: true);
			Ui.winText.SetActive(value: false);
			Ui.loseText.SetActive(value: true);
			PauseOn = true;
			PlayerCar.gameObject.SetActive(value: false);
			CurrentStars = 0;
		}
		if (CurrentStars == Config.GetStarsToWin())
		{
			if (Progress.shop.Incubator_EvoProgressStep != -1)
			{
				Progress.shop.Incubator_RubySetCompleat[Progress.shop.Incubator_EvoProgressStep] = true;
			}
			if (Progress.shop.Incubator_EvoProgressStep != 14)
			{
				Progress.shop.Incubator_EvoProgressStep++;
			}
			WinParticls.SetActive(value: true);
			Ui.resPanel.SetActive(value: true);
			StartCoroutine(sound());
			Ui.winText.SetActive(value: true);
			Ui.loseText.SetActive(value: false);
			PlayerCar.gameObject.SetActive(value: false);
			PauseOn = true;
		}
	}

	private IEnumerator sound()
	{
		Audio.Play("fortuneWin");
		yield return 0;
	}

	private void Update()
	{
		if (!PauseOn)
		{
			if (!TutorialShow)
			{
				_tCounter++;
			}
			if (_tCounter >= _countOnCycle)
			{
				Gnr();
				_tCounter = 0;
			}
			_tCounterDecor++;
			if (_tCounterDecor >= _countOnCycleDecor)
			{
				GnrDecor();
				_tCounterDecor = 0;
			}
		}
	}

	private void Gnr()
	{
		_currentCycleNum++;
		if (_currentStage < Config.GetStage().Count - 1 && _currentCycleNum > Config.GetStage()[_currentStage + 1].NumToStart)
		{
			_currentStage++;
			_countOnCycle = Config.GetStage()[_currentStage].NumOnCycle;
			_countOnCycle = Config.GetStage()[_currentStage].NumOnCycle;
			StartCoroutine(DelayToNewSpeed(Speed));
		}
		IncubatorMG_InterectObj incubatorMG_InterectObj = null;
		int num = UnityEngine.Random.Range(0, 100);
		if (num > Config.GetStage()[_currentStage].Empty)
		{
			if (num > Config.GetStage()[_currentStage].Empty && num <= Config.GetStage()[_currentStage].Empty + Config.GetStage()[_currentStage].CopCar)
			{
				incubatorMG_InterectObj = PoolThis.GetInterectObj();
				incubatorMG_InterectObj.Tupe = Tupe.CarCop;
				incubatorMG_InterectObj.Speed = Speed * -0.3f;
			}
			else if (num > Config.GetStage()[_currentStage].Empty + Config.GetStage()[_currentStage].CopCar && num <= Config.GetStage()[_currentStage].Empty + Config.GetStage()[_currentStage].CopCar + Config.GetStage()[_currentStage].CivilCar)
			{
				incubatorMG_InterectObj = PoolThis.GetInterectObj();
				incubatorMG_InterectObj.Tupe = Tupe.CarCivil;
				incubatorMG_InterectObj.Speed = Speed * -0.3f;
			}
			else if (num > Config.GetStage()[_currentStage].Empty + Config.GetStage()[_currentStage].CopCar + Config.GetStage()[_currentStage].CivilCar && num <= Config.GetStage()[_currentStage].Empty + Config.GetStage()[_currentStage].CopCar + Config.GetStage()[_currentStage].CivilCar + Config.GetStage()[_currentStage].StarBig)
			{
				if (StarPatternCorutine == null)
				{
					StarPatternCorutine = StartCoroutine(PatternStars());
				}
			}
			else if (num > Config.GetStage()[_currentStage].Empty + Config.GetStage()[_currentStage].CopCar + Config.GetStage()[_currentStage].CivilCar + Config.GetStage()[_currentStage].StarBig)
			{
				incubatorMG_InterectObj = PoolThis.GetInterectObj();
				incubatorMG_InterectObj.Tupe = Tupe.Stone;
				incubatorMG_InterectObj.Speed = 0f;
			}
		}
		if (incubatorMG_InterectObj != null && incubatorMG_InterectObj.Tupe != Tupe.Star)
		{
			_numLineToGnrNow = -1;
			incubatorMG_InterectObj.transform.position = GetPositionObj();
			incubatorMG_InterectObj.gameObject.SetActive(value: true);
			incubatorMG_InterectObj.ChengLayer(_numLineToGnrNow);
		}
	}

	private IEnumerator DelayToNewSpeed(float oldSpeed)
	{
		float plasedToSpeed = (Config.GetStage()[_currentStage].Speed - oldSpeed) / 20f;
		for (int tt = 0; tt < 20; tt++)
		{
			float t = 0.3f;
			while (t > 0f)
			{
				t -= Time.deltaTime;
				yield return null;
			}
			Speed += plasedToSpeed;
			Ground.Speed = Speed / 7f;
			Back.Speed = Speed / 20f;
		}
	}

	private IEnumerator PatternStars()
	{
		Vector3 lineToGnrPos = GetPositionObj();
		int numLine = _numLineToGnrNow;
		_numLineToGnrNow = -1;
		float timeBetweenStars = Config.GetStage()[_currentStage].TimeBetweenStarSec;
		int countStars = UnityEngine.Random.Range(Config.GetStage()[_currentStage].MinStarNum, Config.GetStage()[_currentStage].MaxStarNum + 1);
		while (countStars > 0)
		{
			countStars--;
			IncubatorMG_InterectObj obj = PoolThis.GetInterectObj();
			obj.Tupe = Tupe.Star;
			obj.Speed = 0f;
			obj.transform.position = lineToGnrPos;
			obj.gameObject.SetActive(value: true);
			obj.ChengLayer(numLine);
			float t = timeBetweenStars;
			while (t > 0f)
			{
				if (!PauseOn)
				{
					t -= Time.deltaTime;
				}
				yield return null;
			}
		}
		StarPatternCorutine = null;
	}

	private void GnrDecor()
	{
		if (TutorialShow)
		{
			_countOnCycleDecor = UnityEngine.Random.Range(100, 300);
		}
		else
		{
			_countOnCycleDecor = UnityEngine.Random.Range(Config.GetStage()[_currentStage].MinCycle, Config.GetStage()[_currentStage].MaxCycle);
		}
		int num = 0;
		num = ((!TutorialShow) ? Config.GetStage()[_currentStage].ChanceToGnrDecor : 50);
		int num2 = UnityEngine.Random.Range(0, 100);
		if (num2 <= num)
		{
			IncubatorMG_InterectObj decor = PoolThis.GetDecor();
			decor.Tupe = Tupe.Decor;
			decor.transform.position = GetPositionDecor();
			decor.Speed = 0f;
			decor.gameObject.SetActive(value: true);
			decor.ChengLayer(_numLineToGnrNow);
		}
	}

	private Vector3 GetPositionObj()
	{
		int num = UnityEngine.Random.Range(0, 1000);
		if (num < 333)
		{
			_numLineToGnrNow = 1;
			return StartOnLineTop.position;
		}
		if (num >= 333 && num < 667)
		{
			_numLineToGnrNow = 3;
			return StartOnLineCenter.position;
		}
		if (num >= 667)
		{
			_numLineToGnrNow = 5;
			return StartOnLineBot.position;
		}
		UnityEngine.Debug.Log("Error GetPositionObj!!!!");
		return Vector3.zero;
	}

	private Vector3 GetPositionDecor()
	{
		int num = UnityEngine.Random.Range(0, 1000);
		if (num < 250)
		{
			_numLineToGnrNow = 0;
			return StartUnderLine1.position;
		}
		if (num >= 250 && num < 500)
		{
			_numLineToGnrNow = 2;
			return StartUnderLine2.position;
		}
		if (num >= 500 && num < 750)
		{
			_numLineToGnrNow = 4;
			return StartUnderLine3.position;
		}
		if (num >= 750)
		{
			_numLineToGnrNow = 6;
			return StartUnderLine4.position;
		}
		UnityEngine.Debug.Log("Error GetPositionDecor!!!!");
		return Vector3.zero;
	}

	private void OnDisable()
	{
		Game.OnStateChange(Game.gameState.Shop);
	}
}
