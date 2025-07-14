using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiContainer : MonoBehaviour
{
	public List<GameObject> markersss = new List<GameObject>();

	public GUIStartCouner interface_StartCouner;

	public GUIControllsMessenger interface_Controlls;

	public GUIPositionBar interface_PositionBar;

	public GUICarInfoFields interface_CarInfoBars;

	public GUIBoosters interface_Boosters;

	public GUIColorTint interface_ColorTint;

	public BoostTimer interface_BoostTimer;

	public GUIPause windows_pause;

	public TutorialsHUD THUD;

	public ForCageTutorial FCT;

	public GUIInterfaceMessage interface_messanger;

	public Results_Glogal_controller interface_R_G_C;

	public Animator GloabalAnim;

	public Animator PoliceLights;

	public Animator UndegroundLights;

	[Header("Badge")]
	public GameObject Badges;

	public Image Token1;

	public Image Token2;

	public Image Token3;

	public Animator Token1Anim;

	public Animator Token2Anim;

	public Animator Token3Anim;

	[Header("Badge Undeground")]
	public GameObject BadgesUndeground;

	public Image Token1Undeground;

	public Image Token2Undeground;

	public Image Token3Undeground;

	public Animator Token1AnimUndeground;

	public Animator Token2AnimUndeground;

	public Animator Token3AnimUndeground;

	[Header("CageMG")]
	public Button CageBut;

	public CounterController CageTimeToDeath;

	public Slider CageHP;

	private CageMGScript cageControl;

	public Text dist;

	public GameObject distObj;

	public RectTransform RTDistOBj;

	public GameObject distArrow;

	public RectTransform RTDistArrow;

	[Header("MissionsTime")]
	public GameObject MissionsTimeObj;

	public GameObject BadgesObj;

	public CounterController MissionsTime;

	private bool onceIn3Badge;

	private int DistConvoiAndCar;

	private int hash_cageIsON = Animator.StringToHash("cageIsON");

	private string tTime = string.Empty;

	private string tTimeNull = "0";

	private string tTimeNNN = "00.00";

	private int hash_IsON = Animator.StringToHash("isON");

	private int hash_IsOn = Animator.StringToHash("isOn");

	private void Update()
	{
		if (RaceLogic.instance.Convoi != null && MissionsTimeObj.activeSelf)
		{
			Vector3 position = RaceLogic.instance.Convoi.transform.position;
			float x = position.x;
			Vector3 position2 = RaceLogic.instance.car.transform.position;
			if (x - position2.x > 20f)
			{
				float z = 0f;
				distObj.SetActive(value: true);
				Vector3 position3 = RaceLogic.instance.Convoi.transform.position;
				float x2 = position3.x;
				Vector3 position4 = RaceLogic.instance.car.transform.position;
				DistConvoiAndCar = (int)(x2 - position4.x);
				dist.text = DistConvoiAndCar.ToString();
				Vector2 anchoredPosition = RTDistOBj.anchoredPosition;
				if (anchoredPosition.y > 0f)
				{
					Vector2 anchoredPosition2 = RTDistOBj.anchoredPosition;
					z = anchoredPosition2.y / 250f * 55f;
				}
				else
				{
					Vector2 anchoredPosition3 = RTDistOBj.anchoredPosition;
					if (anchoredPosition3.y < 0f)
					{
						Vector2 anchoredPosition4 = RTDistOBj.anchoredPosition;
						z = 360f - anchoredPosition4.y / -250f * 55f;
					}
				}
				Vector3 vector = distObj.transform.parent.InverseTransformDirection(RaceLogic.instance.Convoi.transform.position - RaceLogic.instance.car.transform.position);
				if (vector.y >= 50f)
				{
					vector.y = 50f;
				}
				else if (vector.y <= -50f)
				{
					vector.y = -50f;
				}
				Transform transform = distObj.transform;
				Vector3 localPosition = distObj.transform.localPosition;
				float x3 = localPosition.x;
				float y = vector.y * 5f;
				Vector3 localPosition2 = distObj.transform.localPosition;
				transform.localPosition = new Vector3(x3, y, localPosition2.z);
				RTDistArrow.localRotation = Quaternion.Euler(0f, 0f, z);
			}
			else if (distObj.activeSelf)
			{
				distObj.SetActive(value: false);
			}
		}
		else if (distObj.activeSelf)
		{
			distObj.SetActive(value: false);
		}
	}

	private void OnEnable()
	{
		onceIn3Badge = false;
		Badges.SetActive(value: false);
		BadgesUndeground.SetActive(value: false);
		if (!Progress.shop.bossLevel)
		{
			Badges.SetActive(value: false);
			BadgesUndeground.SetActive(value: false);
			if (Progress.levels.InUndeground)
			{
				BadgesUndeground.SetActive(value: true);
			}
			else
			{
				Badges.SetActive(value: true);
			}
		}
	}

	public void StartCage(CageMGScript _cageControl)
	{
		CageBut.onClick.RemoveAllListeners();
		CageBut.onClick.AddListener(PressCageBtn);
		cageControl = _cageControl;
		GloabalAnim.SetBool(hash_cageIsON, value: true);
		CageHP.value = 1f;
	}

	public void EndCage(bool callBrake = true)
	{
		if (!(cageControl == null))
		{
			GloabalAnim.SetBool(hash_cageIsON, value: false);
			if (callBrake)
			{
				cageControl.BrakeCage();
			}
			cageControl = null;
		}
	}

	private void PressCageBtn()
	{
		if (cageControl != null)
		{
			cageControl.PressBut();
		}
	}

	public void SetCageTime(float max, float time)
	{
		tTime = time.ToString();
		if ((int)time < 10)
		{
			tTime = tTimeNull + tTime;
		}
		if (time <= 0f)
		{
			tTime = tTimeNNN;
		}
		CageTimeToDeath.count = tTime;
	}

	public void SetHPFunc(float _time)
	{
		CageHP.value = _time;
	}

	public void StrtCorutFunc()
	{
		StartCoroutine(TimePoliceLightPlayBoss());
	}

	private IEnumerator TimePoliceLightPlayBoss()
	{
		if (Progress.levels.InUndeground)
		{
			UndegroundLights.SetBool(hash_IsON, value: true);
		}
		else
		{
			PoliceLights.SetBool(hash_IsON, value: true);
		}
		int iter = 10;
		while (iter > 0)
		{
			float t = 1f;
			while (t > 0f)
			{
				t -= Time.deltaTime;
				yield return null;
			}
			if (Progress.levels.InUndeground)
			{
				UndegroundLights.SetBool(hash_IsON, value: false);
			}
			else
			{
				PoliceLights.SetBool(hash_IsON, value: false);
			}
			iter--;
			yield return null;
		}
	}

	public void RestartOn()
	{
		onceIn3Badge = false;
		Badges.SetActive(value: false);
		BadgesUndeground.SetActive(value: false);
		if (!Progress.shop.bossLevel)
		{
			Badges.SetActive(value: false);
			BadgesUndeground.SetActive(value: false);
			if (Progress.levels.InUndeground)
			{
				BadgesUndeground.SetActive(value: true);
			}
			else
			{
				Badges.SetActive(value: true);
			}
		}
		if (Progress.shop.bossLevel)
		{
			StopCoroutine(TimePoliceLightPlayBoss());
			if (Progress.levels.InUndeground)
			{
				UndegroundLights.SetBool(hash_IsON, value: false);
			}
			else
			{
				PoliceLights.SetBool(hash_IsON, value: false);
			}
		}
		Token1.fillAmount = 0f;
		Token2.fillAmount = 0f;
		Token3.fillAmount = 0f;
		Token1Undeground.fillAmount = 0f;
		Token2Undeground.fillAmount = 0f;
		Token3Undeground.fillAmount = 0f;
		CageBut.onClick.RemoveAllListeners();
	}

	public void SetToken()
	{
		int counterEmemys = RaceLogic.instance.CounterEmemys;
		float num = (float)RaceLogic.instance.MaxEnemysInLevel / 100f;
		int num2 = (int)(num * (float)DifficultyConfig.instance.GetCurrentLevel().PercTo1);
		int num3 = (int)(num * (float)DifficultyConfig.instance.GetCurrentLevel().PercTo2);
		int num4 = (int)(num * (float)DifficultyConfig.instance.GetCurrentLevel().PercTo3);
		if (Progress.shop.Tutorial && RaceLogic.instance.CounterEmemys > 0)
		{
			if (!Progress.levels.InUndeground)
			{
				Token1.fillAmount = 1f;
				if (Token1.fillAmount == 1f)
				{
					Token1Anim.SetBool(hash_IsOn, value: true);
					StartCoroutine(TimePoliceLightPlay());
				}
				Token2.fillAmount = 1f;
				if (Token2.fillAmount == 1f)
				{
					Token2Anim.SetBool(hash_IsOn, value: true);
					StartCoroutine(TimePoliceLightPlay());
				}
				Token3.fillAmount = 1f;
				if (Token3.fillAmount == 1f)
				{
					Token3Anim.SetBool(hash_IsOn, value: true);
					StartCoroutine(TimePoliceLightPlay());
				}
			}
			else
			{
				Token1Undeground.fillAmount = 1f;
				if (Token1Undeground.fillAmount == 1f)
				{
					Token1AnimUndeground.SetBool(hash_IsOn, value: true);
					StartCoroutine(TimePoliceLightPlay());
				}
				Token2Undeground.fillAmount = 1f;
				if (Token2Undeground.fillAmount == 1f)
				{
					Token2AnimUndeground.SetBool(hash_IsOn, value: true);
					StartCoroutine(TimePoliceLightPlay());
				}
				Token3Undeground.fillAmount = 1f;
				if (Token3Undeground.fillAmount == 1f)
				{
					Token3AnimUndeground.SetBool(hash_IsOn, value: true);
					StartCoroutine(TimePoliceLightPlay());
				}
			}
		}
		if (counterEmemys <= num2)
		{
			if (!Progress.levels.InUndeground)
			{
				Token1.fillAmount = 100f / (float)num2 * (float)counterEmemys / 100f;
				if (Token1.fillAmount == 1f)
				{
					Token1Anim.SetBool(hash_IsOn, value: true);
					StartCoroutine(TimePoliceLightPlay());
				}
			}
			else
			{
				Token1Undeground.fillAmount = 100f / (float)num2 * (float)counterEmemys / 100f;
				if (Token1Undeground.fillAmount == 1f)
				{
					Token1AnimUndeground.SetBool(hash_IsOn, value: true);
					StartCoroutine(TimePoliceLightPlay());
				}
			}
		}
		else if (counterEmemys <= num3)
		{
			if (!Progress.levels.InUndeground)
			{
				if (Token1.fillAmount != 1f)
				{
					Token1.fillAmount = 1f;
					Token1Anim.SetBool(hash_IsOn, value: true);
					StartCoroutine(TimePoliceLightPlay());
				}
				Token2.fillAmount = 100f / (float)(num3 - num2) * (float)(counterEmemys - num2) / 100f;
				if (Token2.fillAmount == 1f)
				{
					Token2Anim.SetBool(hash_IsOn, value: true);
					StartCoroutine(TimePoliceLightPlay());
				}
			}
			else
			{
				if (Token1Undeground.fillAmount != 1f)
				{
					Token1Undeground.fillAmount = 1f;
					Token1AnimUndeground.SetBool(hash_IsOn, value: true);
					StartCoroutine(TimePoliceLightPlay());
				}
				Token2Undeground.fillAmount = 100f / (float)(num3 - num2) * (float)(counterEmemys - num2) / 100f;
				if (Token2Undeground.fillAmount == 1f)
				{
					Token2AnimUndeground.SetBool(hash_IsOn, value: true);
					StartCoroutine(TimePoliceLightPlay());
				}
			}
		}
		else
		{
			if (onceIn3Badge)
			{
				return;
			}
			if (!Progress.levels.InUndeground)
			{
				if (Token2.fillAmount != 1f)
				{
					Token2.fillAmount = 1f;
					Token2Anim.SetBool(hash_IsOn, value: true);
					StartCoroutine(TimePoliceLightPlay());
				}
				Token3.fillAmount = 100f / (float)(num4 - num3) * (float)(counterEmemys - num3) / 100f;
				if (Token3.fillAmount >= 1f)
				{
					onceIn3Badge = true;
					Token3Anim.SetBool(hash_IsOn, value: true);
					StartCoroutine(TimePoliceLightPlay());
				}
			}
			else
			{
				if (Token2Undeground.fillAmount != 1f)
				{
					Token2Undeground.fillAmount = 1f;
					Token2AnimUndeground.SetBool(hash_IsOn, value: true);
					StartCoroutine(TimePoliceLightPlay());
				}
				Token3Undeground.fillAmount = 100f / (float)(num4 - num3) * (float)(counterEmemys - num3) / 100f;
				if (Token3Undeground.fillAmount >= 1f)
				{
					onceIn3Badge = true;
					Token3AnimUndeground.SetBool(hash_IsOn, value: true);
					StartCoroutine(TimePoliceLightPlay());
				}
			}
		}
	}

	public void StartPoliceLight()
	{
		StartCoroutine(TimePoliceLightPlay());
	}

	private IEnumerator TimePoliceLightPlay()
	{
		if (Progress.levels.InUndeground)
		{
			UndegroundLights.gameObject.SetActive(value: true);
			UndegroundLights.SetBool(hash_IsON, value: true);
			PoliceLights.gameObject.SetActive(value: false);
		}
		else
		{
			PoliceLights.gameObject.SetActive(value: true);
			PoliceLights.SetBool(hash_IsON, value: true);
			UndegroundLights.gameObject.SetActive(value: false);
			Audio.Play("siren_police");
		}
		float t = 3f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return null;
		}
		if (Progress.levels.InUndeground)
		{
			UndegroundLights.SetBool(hash_IsON, value: false);
		}
		else
		{
			PoliceLights.SetBool(hash_IsON, value: false);
		}
	}

	private void OnDisable()
	{
		onceIn3Badge = false;
	}
}
