using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FortuneNEw : MonoBehaviour
{
	public delegate void SpinEnded1(int sector);

	public delegate void SpinEnded2(int sector);

	public delegate void SpinEnded3(int sector);

	public Button Clic;

	public Button btn_exit;

	public Button Yes;

	public Button No;

	public GameObject reclama;

	public RewardAnimator RA;

	public Animator globalAnimator;

	public GameObject fortune_window;

	public SpinEnded1 OnSpinEnded1;

	public SpinEnded2 OnSpinEnded2;

	public SpinEnded3 OnSpinEnded3;

	public Transform wheelTransform1;

	public Transform wheelTransform2;

	public Transform wheelTransform3;

	public AnimationCurve SpinCurve;

	public List<SectorFortuneNew> sectorsFor1Circle = new List<SectorFortuneNew>();

	public List<SectorFortuneNew> sectorsFor2Circle = new List<SectorFortuneNew>();

	public List<SectorFortuneNew> sectorsFor3Circle = new List<SectorFortuneNew>();

	public List<float> sectorsFor1CircleRotation = new List<float>();

	public List<float> sectorsFor2CircleRotation = new List<float>();

	public List<float> sectorsFor3CircleRotation = new List<float>();

	private float pSpeed = 1f;

	private SectorFortuneNew.SectorType win1;

	private SectorFortuneNew.SectorType win2;

	private SectorFortuneNew.SectorType win3;

	public float win1Amount;

	public float win2Amount;

	public float win3Amount;

	[HideInInspector]
	public List<int> weight1 = new List<int>();

	[HideInInspector]
	public List<int> weight2 = new List<int>();

	[HideInInspector]
	public List<int> weight3 = new List<int>();

	[HideInInspector]
	public float ZeroSectorAngle;

	public List<GameObject> cogs = new List<GameObject>();

	[Range(0f, 0.9f)]
	public float omission;

	private float tempAngle;

	private int _isOn = Animator.StringToHash("isOn");

	private int _isTaken = Animator.StringToHash("isTaken");

	private int _isSpinBtnPressed = Animator.StringToHash("isSpinBtnPressed");

	private int _isSpined = Animator.StringToHash("isSpined");

	public GameObject doubleNeed;

	public GameObject doubleCheked;

	public GameObject TAKES;

	[Header("Time Rotate Circle")]
	public float timeRotate1Circle = 11f;

	public float timeRotate2Circle = 8f;

	public float timeRotate3Circle = 6f;

	private bool ActivWindow;

	private bool forunaReclama;

	public Animation animationScpins;

	public List<Image> animArrow = new List<Image>();

	[Header("Amount")]
	public float Level1;

	public float Level2;

	public float Level3;

	[Header("AmountBomb")]
	public int Level1Bomb;

	public int Level2Bomb;

	public int Level3Bomb;

	private float CurrentAngle1
	{
		get
		{
			Vector3 eulerAngles = wheelTransform1.eulerAngles;
			return eulerAngles.z;
		}
		set
		{
			wheelTransform1.eulerAngles = Vector3.forward * value;
		}
	}

	private float CurrentAngle2
	{
		get
		{
			Vector3 eulerAngles = wheelTransform2.eulerAngles;
			return eulerAngles.z;
		}
		set
		{
			wheelTransform2.eulerAngles = Vector3.forward * value;
		}
	}

	private float CurrentAngle3
	{
		get
		{
			Vector3 eulerAngles = wheelTransform3.eulerAngles;
			return eulerAngles.z;
		}
		set
		{
			wheelTransform3.eulerAngles = Vector3.forward * value;
		}
	}

	public void TryExit()
	{
		if (ActivWindow)
		{
			if (doubleNeed.activeSelf || doubleCheked.activeSelf || TAKES.activeSelf)
			{
				NoReclama();
			}
			else
			{
				ExtraExit();
			}
		}
	}

	public void ExtraExit()
	{
		ActivWindow = false;
		if (Audio.IsSoundPlaying("fortune2"))
		{
			Audio.Stop("fortune2");
		}
		Audio.PlayAsync("gui_button_02_sn");
		Progress.fortune.SumPercentRuby = 0f;
		Progress.fortune.SumPercentTurbo = 0f;
		Progress.fortune.SumPercentDamage = 0f;
		Progress.fortune.SumPercentHP = 0f;
		Progress.fortune.SumBombStart = 0;
		StartCoroutine(exitextra());
	}

	private IEnumerator exitextra()
	{
		globalAnimator.SetBool(_isOn, value: false);
		yield return new WaitForSeconds(0.2f);
		Progress.fortune.GOGOGOGOGOGO = true;
		UnityEngine.Object.Destroy(fortune_window);
	}

	private void OnEnable()
	{
		Progress.fortune.GOGOGOGOGOGO = false;
		OnSpinEnded1 = (SpinEnded1)Delegate.Combine(OnSpinEnded1, new SpinEnded1(SpinEnd1reward));
		OnSpinEnded2 = (SpinEnded2)Delegate.Combine(OnSpinEnded2, new SpinEnded2(SpinEnd2reward));
		OnSpinEnded3 = (SpinEnded3)Delegate.Combine(OnSpinEnded3, new SpinEnded3(SpinEnd3reward));
		int index = UnityEngine.Random.Range(0, 12);
		CurrentAngle1 = sectorsFor1CircleRotation[index];
		index = UnityEngine.Random.Range(0, 10);
		CurrentAngle2 = sectorsFor2CircleRotation[index];
		index = UnityEngine.Random.Range(0, 8);
		CurrentAngle3 = sectorsFor3CircleRotation[index];
		Clic.onClick.AddListener(Spin);
		Yes.onClick.AddListener(YesReclama);
		No.onClick.AddListener(NoReclama);
		for (int i = 0; i < 12; i++)
		{
			weight1.Add(1);
		}
		for (int j = 0; j < 10; j++)
		{
			weight2.Add(1);
		}
		for (int k = 0; k < 8; k++)
		{
			weight3.Add(1);
		}
		ActivWindow = true;
		Transform transform = fortune_window.transform;
		Vector3 position = fortune_window.transform.position;
		float x = position.x;
		Vector3 position2 = fortune_window.transform.position;
		transform.position = new Vector3(x, position2.y, 100f);
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

	public void YesReclama()
	{
		Audio.PlayAsync("gui_button_02_sn");
		Yes.interactable = false;
		StartCoroutine(checkInternetConnection(delegate(bool isConnected)
		{
			if (isConnected)
			{
				RaceLogic.instance.forune_video(delegate
				{
					Sucses();
				}, delegate
				{
					NOvideo();
				}, delegate
				{
					NoSucses();
				});
			}
			else
			{
				NOvideo();
			}
		}));
	}

	public void Sucses()
	{
		AnalyticsManager.LogEvent(EventCategoty.video, "ads_watched", "x2_fortune");
		Progress.fortune.SumPercentRuby = Progress.fortune.SumPercentRuby * 2f;
		Progress.fortune.SumPercentTurbo = Progress.fortune.SumPercentTurbo * 2f;
		Progress.fortune.SumPercentDamage = Progress.fortune.SumPercentDamage * 2f;
		Progress.fortune.SumPercentHP = Progress.fortune.SumPercentHP * 2f;
		Progress.fortune.SumBombStart = Progress.fortune.SumBombStart * 2;
		Yes.interactable = true;
		RA.animator_Res.SetBool("isDoubled", value: true);
		if (win1 != SectorFortuneNew.SectorType.AddOneBomb)
		{
			float num = win1Amount * 2f;
			RA.amount1.count = num.ToString();
		}
		else
		{
			float num2 = win1Amount * 2f;
			RA.amount1.count = num2.ToString();
		}
		if (win2 != SectorFortuneNew.SectorType.AddOneBomb)
		{
			float num3 = win2Amount * 2f;
			RA.amount2.count = num3.ToString();
		}
		else
		{
			float num4 = win2Amount * 2f;
			RA.amount2.count = num4.ToString();
		}
		if (win3 != SectorFortuneNew.SectorType.AddOneBomb)
		{
			float num5 = win3Amount * 2f;
			RA.amount3.count = num5.ToString();
		}
		else
		{
			float num6 = win3Amount * 2f;
			RA.amount3.count = num6.ToString();
		}
		RaceLogic.instance.FortuneVideo = false;
		RaceLogic.instance.rewiveVideo = false;
	}

	public void NoSucses()
	{
		RaceLogic.instance.FortuneVideo = false;
		Yes.interactable = true;
		RA.animator_Res.SetBool("video_isFailed", value: true);
		RaceLogic.instance.rewiveVideo = false;
	}

	public void NOvideo()
	{
		if (RA != null && RA.animator_Res != null)
		{
			RA.animator_Res.SetBool("video_isAvaliable", value: false);
		}
		Yes.interactable = true;
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("noVideoBox", typeof(GameObject))) as GameObject;
		RaceLogic.instance.FortuneVideo = false;
		RaceLogic.instance.rewiveVideo = false;
	}

	public void NoReclama()
	{
		Audio.PlayAsync("gui_button_02_sn");
		StartCoroutine(hideNoReclama());
	}

	private IEnumerator hideNoReclama()
	{
		while (!RA.animator_Res.isInitialized)
		{
			yield return 0;
		}
		RA.animator_Res.SetBool(_isTaken, value: true);
		globalAnimator.SetBool(_isOn, value: false);
		yield return new WaitForSeconds(0.2f);
		Progress.fortune.GOGOGOGOGOGO = true;
		UnityEngine.Object.Destroy(fortune_window);
	}

	private void OnDisable()
	{
		ActivWindow = false;
		OnSpinEnded1 = (SpinEnded1)Delegate.Remove(OnSpinEnded1, new SpinEnded1(SpinEnd1reward));
		OnSpinEnded2 = (SpinEnded2)Delegate.Remove(OnSpinEnded2, new SpinEnded2(SpinEnd2reward));
		OnSpinEnded3 = (SpinEnded3)Delegate.Remove(OnSpinEnded3, new SpinEnded3(SpinEnd3reward));
		Clic.onClick.RemoveAllListeners();
		Yes.onClick.RemoveAllListeners();
		No.onClick.RemoveAllListeners();
	}

	public void Spin()
	{
		Audio.PlayAsync("fortune2", 1f, loop: true);
		Audio.PlayAsync("gui_button_02_sn");
		animationScpins.enabled = false;
		foreach (Image item in animArrow)
		{
			Image image = item;
			Color color = item.color;
			float r = color.r;
			Color color2 = item.color;
			float g = color2.g;
			Color color3 = item.color;
			image.color = new Color(r, g, color3.b, 1f);
		}
		Progress.fortune.GOGOGOGOGOGO = false;
		Progress.fortune.SumPercentRuby = 0f;
		Progress.fortune.SumPercentTurbo = 0f;
		Progress.fortune.SumPercentDamage = 0f;
		Progress.fortune.SumPercentHP = 0f;
		Progress.fortune.SumBombStart = 0;
		StartCoroutine(startSpins());
		globalAnimator.SetBool(_isSpinBtnPressed, value: true);
		Clic.interactable = false;
		ButtonPressTween component = Clic.GetComponent<ButtonPressTween>();
		component.enabled = false;
	}

	private IEnumerator startSpins()
	{
		StartCoroutine(DoSpin3circle());
		yield return new WaitForSeconds(0.5f);
		StartCoroutine(DoSpin2circle());
		yield return new WaitForSeconds(0.5f);
		StartCoroutine(DoSpin1circle());
	}

	public virtual int getRandomSector(List<int> weight)
	{
		int num = UnityEngine.Random.Range(0, weight.Count + 1);
		int num2 = 0;
		for (int i = 0; i < weight.Count; i++)
		{
			if (num >= num2 && num < num2 + weight[i])
			{
				return i;
			}
			num2 += weight[i];
		}
		return weight.Count - 1;
	}

	public IEnumerator DoSpin3circle()
	{
		float deltaAngle = tempAngle;
		int sectorrand = getRandomSector(weight3);
		float time = timeRotate3Circle;
		int sector = 0;
		float pMaxAngle2 = ZeroSectorAngle - CurrentAngle3 + 360f * ((!(deltaAngle < 0f)) ? time : (0f - time)) + 360f / (float)weight3.Count * (float)sectorrand;
		pMaxAngle2 += UnityEngine.Random.Range(0f - omission, omission) * 180f / (float)weight3.Count;
		StartCoroutine(DoSpin3circle(time, pMaxAngle2, SpinEnd3, sector));
		yield return 0;
	}

	private IEnumerator DoSpin3circle(float time, float maxAngle, Action<int> action = null, int sector = 0)
	{
		float startAngle = CurrentAngle3;
		float prevAngle = startAngle;
		pSpeed = prevAngle - SpinCurve.Evaluate(0f) * maxAngle;
		float timer = 0f;
		while (timer < time)
		{
			yield return null;
			float angle = SpinCurve.Evaluate(timer / time) * maxAngle * -1.5f;
			pSpeed = prevAngle - angle;
			prevAngle = angle;
			CurrentAngle3 = (angle + startAngle) % 360f;
			if (timer <= time - 2.5f)
			{
				foreach (GameObject cog in cogs)
				{
					cog.transform.eulerAngles = Vector3.forward * CurrentAngle3;
				}
			}
			timer += Time.unscaledDeltaTime;
			if (!(timer >= time - 2f))
			{
				continue;
			}
			Vector3 eulerAngles = wheelTransform3.eulerAngles;
			int num = (int)eulerAngles.z;
			for (int i = 0; i < sectorsFor3CircleRotation.Count; i++)
			{
				if (!((float)num >= sectorsFor3CircleRotation[i] - 10f) || !((float)num <= sectorsFor3CircleRotation[i] + 10f))
				{
					continue;
				}
				Transform transform = wheelTransform3;
				Vector3 eulerAngles2 = wheelTransform3.eulerAngles;
				float x = eulerAngles2.x;
				Vector3 eulerAngles3 = wheelTransform3.eulerAngles;
				transform.eulerAngles = new Vector3(x, eulerAngles3.y, sectorsFor3CircleRotation[i]);
				sector = i;
				win3 = sectorsFor3Circle[sector].sectorType;
				if (sectorsFor3Circle[sector].sectorType == SectorFortuneNew.SectorType.AddOneBomb)
				{
					switch (sectorsFor3Circle[sector].amount)
					{
					case SectorFortuneNew.ClasAmount.Level1:
						win3Amount = Level1Bomb;
						break;
					case SectorFortuneNew.ClasAmount.Level2:
						win3Amount = Level2Bomb;
						break;
					case SectorFortuneNew.ClasAmount.Level3:
						win3Amount = Level3Bomb;
						break;
					}
				}
				else
				{
					switch (sectorsFor3Circle[sector].amount)
					{
					case SectorFortuneNew.ClasAmount.Level1:
						win3Amount = Level1;
						break;
					case SectorFortuneNew.ClasAmount.Level2:
						win3Amount = Level2;
						break;
					case SectorFortuneNew.ClasAmount.Level3:
						win3Amount = Level3;
						break;
					}
				}
				StartCoroutine(LerpForCentr(sectorsFor3CircleRotation[i], wheelTransform3));
				action?.Invoke(sector);
				sectorsFor3Circle[sector].anim.SetActive(value: true);
				yield break;
			}
		}
	}

	private void SpinEnd3(int sectorNum)
	{
		if (OnSpinEnded3 != null)
		{
			OnSpinEnded3(sectorNum);
		}
	}

	private void SpinEnd3reward(int sectorNum)
	{
		switch (sectorsFor3Circle[sectorNum].sectorType)
		{
		case SectorFortuneNew.SectorType.PersentRubins:
			switch (sectorsFor3Circle[sectorNum].amount)
			{
			case SectorFortuneNew.ClasAmount.Level1:
				Progress.fortune.SumPercentRuby += Level1;
				break;
			case SectorFortuneNew.ClasAmount.Level2:
				Progress.fortune.SumPercentRuby += Level2;
				break;
			case SectorFortuneNew.ClasAmount.Level3:
				Progress.fortune.SumPercentRuby += Level3;
				break;
			}
			break;
		case SectorFortuneNew.SectorType.PersentTurbo:
			switch (sectorsFor3Circle[sectorNum].amount)
			{
			case SectorFortuneNew.ClasAmount.Level1:
				Progress.fortune.SumPercentTurbo += Level1;
				break;
			case SectorFortuneNew.ClasAmount.Level2:
				Progress.fortune.SumPercentTurbo += Level2;
				break;
			case SectorFortuneNew.ClasAmount.Level3:
				Progress.fortune.SumPercentTurbo += Level3;
				break;
			}
			break;
		case SectorFortuneNew.SectorType.PersentDamage:
			switch (sectorsFor3Circle[sectorNum].amount)
			{
			case SectorFortuneNew.ClasAmount.Level1:
				Progress.fortune.SumPercentDamage += Level1;
				break;
			case SectorFortuneNew.ClasAmount.Level2:
				Progress.fortune.SumPercentDamage += Level2;
				break;
			case SectorFortuneNew.ClasAmount.Level3:
				Progress.fortune.SumPercentDamage += Level3;
				break;
			}
			break;
		case SectorFortuneNew.SectorType.PersentHealth:
			switch (sectorsFor3Circle[sectorNum].amount)
			{
			case SectorFortuneNew.ClasAmount.Level1:
				Progress.fortune.SumPercentHP += Level1;
				break;
			case SectorFortuneNew.ClasAmount.Level2:
				Progress.fortune.SumPercentHP += Level2;
				break;
			case SectorFortuneNew.ClasAmount.Level3:
				Progress.fortune.SumPercentHP += Level3;
				break;
			}
			break;
		case SectorFortuneNew.SectorType.None:
			Progress.fortune.Dirka++;
			break;
		case SectorFortuneNew.SectorType.AddOneBomb:
			switch (sectorsFor3Circle[sectorNum].amount)
			{
			case SectorFortuneNew.ClasAmount.Level1:
				Progress.fortune.SumBombStart += Level1Bomb;
				break;
			case SectorFortuneNew.ClasAmount.Level2:
				Progress.fortune.SumBombStart += Level2Bomb;
				break;
			case SectorFortuneNew.ClasAmount.Level3:
				Progress.fortune.SumBombStart += Level3Bomb;
				break;
			}
			break;
		}
		Progress.Save(Progress.SaveType.Shop);
	}

	public IEnumerator DoSpin2circle()
	{
		float deltaAngle = tempAngle;
		int sectorrand = getRandomSector(weight2);
		float time = timeRotate2Circle;
		int sector = 0;
		float pMaxAngle2 = ZeroSectorAngle - CurrentAngle2 + 360f * ((!(deltaAngle < 0f)) ? time : (0f - time)) + 360f / (float)weight2.Count * (float)sectorrand;
		pMaxAngle2 += UnityEngine.Random.Range(0f - omission, omission) * 180f / (float)weight2.Count;
		StartCoroutine(DoSpin2circle(time, pMaxAngle2, SpinEnd2, sector));
		yield return 0;
	}

	private IEnumerator DoSpin2circle(float time, float maxAngle, Action<int> action = null, int sector = 0)
	{
		float startAngle = CurrentAngle2;
		float prevAngle = startAngle;
		pSpeed = prevAngle - SpinCurve.Evaluate(0f) * maxAngle;
		float timer = 0f;
		while (timer < time)
		{
			yield return null;
			float angle = SpinCurve.Evaluate(timer / time) * maxAngle * 1.5f;
			pSpeed = prevAngle - angle;
			prevAngle = angle;
			CurrentAngle2 = (angle + startAngle) % 360f;
			timer += Time.unscaledDeltaTime;
			if (!(timer >= time - 4f))
			{
				continue;
			}
			Vector3 eulerAngles = wheelTransform2.eulerAngles;
			int num = (int)eulerAngles.z;
			for (int i = 0; i < sectorsFor2CircleRotation.Count; i++)
			{
				if (!((float)num >= sectorsFor2CircleRotation[i] - 10f) || !((float)num <= sectorsFor2CircleRotation[i] + 10f))
				{
					continue;
				}
				Transform transform = wheelTransform2;
				Vector3 eulerAngles2 = wheelTransform2.eulerAngles;
				float x = eulerAngles2.x;
				Vector3 eulerAngles3 = wheelTransform2.eulerAngles;
				transform.eulerAngles = new Vector3(x, eulerAngles3.y, sectorsFor2CircleRotation[i]);
				sector = i;
				win2 = sectorsFor2Circle[sector].sectorType;
				if (sectorsFor2Circle[sector].sectorType == SectorFortuneNew.SectorType.AddOneBomb)
				{
					switch (sectorsFor2Circle[sector].amount)
					{
					case SectorFortuneNew.ClasAmount.Level1:
						win2Amount = Level1Bomb;
						break;
					case SectorFortuneNew.ClasAmount.Level2:
						win2Amount = Level2Bomb;
						break;
					case SectorFortuneNew.ClasAmount.Level3:
						win2Amount = Level3Bomb;
						break;
					}
				}
				else
				{
					switch (sectorsFor2Circle[sector].amount)
					{
					case SectorFortuneNew.ClasAmount.Level1:
						win2Amount = Level1;
						break;
					case SectorFortuneNew.ClasAmount.Level2:
						win2Amount = Level2;
						break;
					case SectorFortuneNew.ClasAmount.Level3:
						win2Amount = Level3;
						break;
					}
				}
				StartCoroutine(LerpForCentr(sectorsFor2CircleRotation[i], wheelTransform2));
				action?.Invoke(sector);
				sectorsFor2Circle[sector].anim.SetActive(value: true);
				yield break;
			}
		}
	}

	private void SpinEnd2(int sectorNum)
	{
		if (OnSpinEnded2 != null)
		{
			OnSpinEnded2(sectorNum);
		}
	}

	private void SpinEnd2reward(int sectorNum)
	{
		switch (sectorsFor2Circle[sectorNum].sectorType)
		{
		case SectorFortuneNew.SectorType.PersentRubins:
			switch (sectorsFor2Circle[sectorNum].amount)
			{
			case SectorFortuneNew.ClasAmount.Level1:
				Progress.fortune.SumPercentRuby += Level1;
				break;
			case SectorFortuneNew.ClasAmount.Level2:
				Progress.fortune.SumPercentRuby += Level2;
				break;
			case SectorFortuneNew.ClasAmount.Level3:
				Progress.fortune.SumPercentRuby += Level3;
				break;
			}
			break;
		case SectorFortuneNew.SectorType.PersentTurbo:
			switch (sectorsFor2Circle[sectorNum].amount)
			{
			case SectorFortuneNew.ClasAmount.Level1:
				Progress.fortune.SumPercentTurbo += Level1;
				break;
			case SectorFortuneNew.ClasAmount.Level2:
				Progress.fortune.SumPercentTurbo += Level2;
				break;
			case SectorFortuneNew.ClasAmount.Level3:
				Progress.fortune.SumPercentTurbo += Level3;
				break;
			}
			break;
		case SectorFortuneNew.SectorType.PersentDamage:
			switch (sectorsFor2Circle[sectorNum].amount)
			{
			case SectorFortuneNew.ClasAmount.Level1:
				Progress.fortune.SumPercentDamage += Level1;
				break;
			case SectorFortuneNew.ClasAmount.Level2:
				Progress.fortune.SumPercentDamage += Level2;
				break;
			case SectorFortuneNew.ClasAmount.Level3:
				Progress.fortune.SumPercentDamage += Level3;
				break;
			}
			break;
		case SectorFortuneNew.SectorType.PersentHealth:
			switch (sectorsFor2Circle[sectorNum].amount)
			{
			case SectorFortuneNew.ClasAmount.Level1:
				Progress.fortune.SumPercentHP += Level1;
				break;
			case SectorFortuneNew.ClasAmount.Level2:
				Progress.fortune.SumPercentHP += Level2;
				break;
			case SectorFortuneNew.ClasAmount.Level3:
				Progress.fortune.SumPercentHP += Level3;
				break;
			}
			break;
		case SectorFortuneNew.SectorType.None:
			Progress.fortune.Dirka++;
			break;
		case SectorFortuneNew.SectorType.AddOneBomb:
			switch (sectorsFor2Circle[sectorNum].amount)
			{
			case SectorFortuneNew.ClasAmount.Level1:
				Progress.fortune.SumBombStart += Level1Bomb;
				break;
			case SectorFortuneNew.ClasAmount.Level2:
				Progress.fortune.SumBombStart += Level2Bomb;
				break;
			case SectorFortuneNew.ClasAmount.Level3:
				Progress.fortune.SumBombStart += Level3Bomb;
				break;
			}
			break;
		}
		Progress.Save(Progress.SaveType.Shop);
	}

	public IEnumerator DoSpin1circle()
	{
		float deltaAngle = tempAngle;
		int sectorrand = getRandomSector(weight1);
		float time = timeRotate1Circle;
		int sector = 0;
		float pMaxAngle2 = ZeroSectorAngle - CurrentAngle1 + 360f * ((!(deltaAngle < 0f)) ? time : (0f - time)) + 360f / (float)weight1.Count * (float)sectorrand;
		pMaxAngle2 += UnityEngine.Random.Range(0f - omission, omission) * 180f / (float)weight1.Count;
		StartCoroutine(DoSpin1circle(time, pMaxAngle2, SpinEnd1, sector));
		yield return 0;
	}

	private IEnumerator DoSpin1circle(float time, float maxAngle, Action<int> action = null, int sector = 0)
	{
		float startAngle = CurrentAngle1;
		float prevAngle = startAngle;
		pSpeed = prevAngle - SpinCurve.Evaluate(0f) * maxAngle;
		float timer = 0f;
		while (timer < time)
		{
			yield return null;
			float angle = SpinCurve.Evaluate(timer / time) * maxAngle * -1.5f;
			pSpeed = prevAngle - angle;
			prevAngle = angle;
			CurrentAngle1 = (angle + startAngle) % 360f;
			if (timer >= 1.5f)
			{
				foreach (GameObject cog in cogs)
				{
					cog.transform.eulerAngles = Vector3.forward * CurrentAngle1;
				}
			}
			if (timer >= time - 4f)
			{
				Audio.Stop("fortune2");
				Audio.Play("fortune3");
			}
			timer += Time.unscaledDeltaTime;
			if (!(timer >= time - 3f))
			{
				continue;
			}
			Vector3 eulerAngles = wheelTransform1.eulerAngles;
			int num = (int)eulerAngles.z;
			for (int i = 0; i < sectorsFor1CircleRotation.Count; i++)
			{
				if (!((float)num >= sectorsFor1CircleRotation[i] - 10f) || !((float)num <= sectorsFor1CircleRotation[i] + 10f))
				{
					continue;
				}
				sector = i;
				win1 = sectorsFor1Circle[sector].sectorType;
				if (sectorsFor1Circle[sector].sectorType == SectorFortuneNew.SectorType.AddOneBomb)
				{
					switch (sectorsFor1Circle[sector].amount)
					{
					case SectorFortuneNew.ClasAmount.Level1:
						win1Amount = Level1Bomb;
						break;
					case SectorFortuneNew.ClasAmount.Level2:
						win1Amount = Level2Bomb;
						break;
					case SectorFortuneNew.ClasAmount.Level3:
						win1Amount = Level3Bomb;
						break;
					}
				}
				else
				{
					switch (sectorsFor1Circle[sector].amount)
					{
					case SectorFortuneNew.ClasAmount.Level1:
						win1Amount = Level1;
						break;
					case SectorFortuneNew.ClasAmount.Level2:
						win1Amount = Level2;
						break;
					case SectorFortuneNew.ClasAmount.Level3:
						win1Amount = Level3;
						break;
					}
				}
				StartCoroutine(LerpForCentr(sectorsFor1CircleRotation[i], wheelTransform1));
				action?.Invoke(sector);
				sectorsFor1Circle[sector].anim.SetActive(value: true);
				yield break;
			}
		}
	}

	private void SpinEnd1(int sectorNum)
	{
		if (OnSpinEnded1 != null)
		{
			OnSpinEnded1(sectorNum);
		}
	}

	private void SpinEnd1reward(int sectorNum)
	{
		switch (sectorsFor1Circle[sectorNum].sectorType)
		{
		case SectorFortuneNew.SectorType.PersentRubins:
			switch (sectorsFor1Circle[sectorNum].amount)
			{
			case SectorFortuneNew.ClasAmount.Level1:
				Progress.fortune.SumPercentRuby += Level1;
				break;
			case SectorFortuneNew.ClasAmount.Level2:
				Progress.fortune.SumPercentRuby += Level2;
				break;
			case SectorFortuneNew.ClasAmount.Level3:
				Progress.fortune.SumPercentRuby += Level3;
				break;
			}
			break;
		case SectorFortuneNew.SectorType.PersentTurbo:
			switch (sectorsFor1Circle[sectorNum].amount)
			{
			case SectorFortuneNew.ClasAmount.Level1:
				Progress.fortune.SumPercentTurbo += Level1;
				break;
			case SectorFortuneNew.ClasAmount.Level2:
				Progress.fortune.SumPercentTurbo += Level2;
				break;
			case SectorFortuneNew.ClasAmount.Level3:
				Progress.fortune.SumPercentTurbo += Level3;
				break;
			}
			break;
		case SectorFortuneNew.SectorType.PersentDamage:
			switch (sectorsFor1Circle[sectorNum].amount)
			{
			case SectorFortuneNew.ClasAmount.Level1:
				Progress.fortune.SumPercentDamage += Level1;
				break;
			case SectorFortuneNew.ClasAmount.Level2:
				Progress.fortune.SumPercentDamage += Level2;
				break;
			case SectorFortuneNew.ClasAmount.Level3:
				Progress.fortune.SumPercentDamage += Level3;
				break;
			}
			break;
		case SectorFortuneNew.SectorType.PersentHealth:
			switch (sectorsFor1Circle[sectorNum].amount)
			{
			case SectorFortuneNew.ClasAmount.Level1:
				Progress.fortune.SumPercentHP += Level1;
				break;
			case SectorFortuneNew.ClasAmount.Level2:
				Progress.fortune.SumPercentHP += Level2;
				break;
			case SectorFortuneNew.ClasAmount.Level3:
				Progress.fortune.SumPercentHP += Level3;
				break;
			}
			break;
		case SectorFortuneNew.SectorType.None:
			Progress.fortune.Dirka++;
			break;
		case SectorFortuneNew.SectorType.AddOneBomb:
			switch (sectorsFor1Circle[sectorNum].amount)
			{
			case SectorFortuneNew.ClasAmount.Level1:
				Progress.fortune.SumBombStart += Level1Bomb;
				break;
			case SectorFortuneNew.ClasAmount.Level2:
				Progress.fortune.SumBombStart += Level2Bomb;
				break;
			case SectorFortuneNew.ClasAmount.Level3:
				Progress.fortune.SumBombStart += Level3Bomb;
				break;
			}
			break;
		}
		Progress.Save(Progress.SaveType.Shop);
		Audio.PlayAsync("fortuneWin");
		StartCoroutine(Reclama());
	}

	private IEnumerator LerpForCentr(float FinRotate, Transform Transf)
	{
		for (int i = 0; i < 15; i++)
		{
			Vector3 eulerAngles = Transf.eulerAngles;
			Vector3 eulerAngles2 = Transf.eulerAngles;
			float x = eulerAngles2.x;
			Vector3 eulerAngles3 = Transf.eulerAngles;
			Transf.eulerAngles = Vector3.Lerp(eulerAngles, new Vector3(x, eulerAngles3.y, FinRotate), 0.1f);
			yield return 0;
		}
		Vector3 eulerAngles4 = Transf.eulerAngles;
		float x2 = eulerAngles4.x;
		Vector3 eulerAngles5 = Transf.eulerAngles;
		Transf.eulerAngles = new Vector3(x2, eulerAngles5.y, FinRotate);
	}

	private IEnumerator Reclama()
	{
		yield return new WaitForSeconds(1.5f);
		foreach (SectorFortuneNew item in sectorsFor3Circle)
		{
			item.anim.SetActive(value: false);
		}
		foreach (SectorFortuneNew item2 in sectorsFor2Circle)
		{
			item2.anim.SetActive(value: false);
		}
		foreach (SectorFortuneNew item3 in sectorsFor1Circle)
		{
			item3.anim.SetActive(value: false);
		}
		globalAnimator.SetBool(_isSpined, value: true);
		yield return new WaitForSeconds(0.1f);
		RA.ChangeImage(win1, RA.imgWheel12, win1Amount, RA.amount1, RA.percent1);
		RA.ChangeImage(win2, RA.imgWheel10, win2Amount, RA.amount2, RA.percent2);
		RA.ChangeImage(win3, RA.imgWheel8, win3Amount, RA.amount3, RA.percent3);
		if (win1 != win2 && win1 != win3 && win2 != win3)
		{
			yield return new WaitForSeconds(0.2f);
			RA.Init(0, win1, win2, win3, win1Amount, RA.amount1, win2Amount, RA.amount2, win3Amount, RA.amount3, RA.percent1, RA.percent2, RA.percent3);
		}
		else if (win1 != win2 && win1 == win3 && win2 != win3)
		{
			yield return new WaitForSeconds(0.2f);
			RA.Init(5, win1, win2, win3, win1Amount, RA.amount1, win2Amount, RA.amount2, win3Amount, RA.amount3, RA.percent1, RA.percent2, RA.percent3);
		}
		else if (win1 == win2 && win1 != win3 && win2 != win3)
		{
			yield return new WaitForSeconds(0.2f);
			RA.Init(3, win1, win2, win3, win1Amount, RA.amount1, win2Amount, RA.amount2, win3Amount, RA.amount3, RA.percent1, RA.percent2, RA.percent3);
		}
		else if (win1 != win2 && win1 != win3 && win2 == win3)
		{
			yield return new WaitForSeconds(0.2f);
			RA.Init(6, win1, win2, win3, win1Amount, RA.amount1, win2Amount, RA.amount2, win3Amount, RA.amount3, RA.percent1, RA.percent2, RA.percent3);
		}
		else if (win1 == win2 && win1 == win3 && win2 == win3)
		{
			yield return new WaitForSeconds(0.2f);
			if (win1 != SectorFortuneNew.SectorType.None && win3 != SectorFortuneNew.SectorType.None && win3 != SectorFortuneNew.SectorType.None)
			{
				RA.Init(7, win1, win2, win3, win1Amount, RA.amount1, win2Amount, RA.amount2, win3Amount, RA.amount3, RA.percent1, RA.percent2, RA.percent3);
			}
			else
			{
				RA.Init(99, win1, win2, win3, win1Amount, RA.amount1, win2Amount, RA.amount2, win3Amount, RA.amount3, RA.percent1, RA.percent2, RA.percent3);
			}
		}
	}
}
