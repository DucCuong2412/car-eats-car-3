using SmartLocalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCars : MonoBehaviour
{
	[Serializable]
	public class Mission
	{
		public int LocForOpen;

		public int LavelForOpen;

		public GameObject Obj;
	}

	[Serializable]
	public class Decor
	{
		public int LocForOpen;

		public int LavelForOpen;

		public float TimeToOpen;

		public Animator Anim;
	}

	public bool Underground;

	[Header("Arena Brif Openner")]
	public CellContainer Arena1;

	public CellContainer Arena2;

	public CellContainer Arena3;

	[Header("BossWindowBlock")]
	public GameObject BossWind;

	public Button BossClose;

	public Text BossName;

	public Text BossBadgesNeed;

	public CounterController BossBadgesBalance;

	public Animator BossAnim;

	[Header("Missions")]
	public GameObject CarCombain;

	public GameObject CarKusKus;

	public GameObject MissionTimeMarker;

	public Animator MissionTimeMarkerAnim;

	public Text MissionTime;

	private Coroutine timerCorut;

	public LevelGalleryCanvasLogic galery;

	public List<Mission> Missions = new List<Mission>();

	public List<Decor> DecorsAnim = new List<Decor>();

	public GameObject Boss1;

	public GameObject Boss2;

	public GameObject Boss3;

	private int hash_isON = Animator.StringToHash("isON");

	private int hash_isOn = Animator.StringToHash("isOn");

	private bool setAlert;

	public MapCentralization MC;

	public void BossLockOpen(int pack)
	{
		BossWind.SetActive(value: true);
		BossClose.onClick.AddListener(bossClosePress);
		int num = -1;
		switch (pack)
		{
		case 1:
			num = 0;
			break;
		case 2:
			num = 1;
			break;
		case 3:
			num = 2;
			break;
		}
		int num2 = -1;
		string text = "-1";
		switch (num)
		{
		case 0:
			text = ((!Progress.levels.InUndeground) ? "BOSS1" : "COCKCHAFER");
			num2 = DifficultyConfig.instance.BudgesBoss1;
			break;
		case 1:
			text = ((!Progress.levels.InUndeground) ? "BOSS2" : "TURTLE");
			num2 = DifficultyConfig.instance.BudgesBoss2;
			break;
		case 2:
			text = "BOSS3";
			num2 = DifficultyConfig.instance.BudgesBoss3;
			break;
		}
		BossBadgesNeed.text = num2.ToString();
		int num3 = 0;
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j <= 12; j++)
			{
				num3 += Progress.levels.Pack(i).Level(j).oldticket;
			}
		}
		BossBadgesBalance.count = num3.ToString();
		StartCoroutine(TrySetName(text));
		BossName.text = LanguageManager.Instance.GetTextValue(text);
	}

	private IEnumerator TrySetName(string name)
	{
		float t = 0.5f;
		while (t > 0f)
		{
			BossName.text = LanguageManager.Instance.GetTextValue(name);
			t -= Time.deltaTime;
			yield return null;
		}
	}

	private void bossClosePress()
	{
		BossAnim.SetBool(hash_isON, value: false);
		BossClose.onClick.RemoveAllListeners();
		StartCoroutine(bossDelay());
	}

	private IEnumerator bossDelay()
	{
		float t = 1f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return null;
		}
		BossWind.SetActive(value: false);
	}

	private void Update()
	{
		if (!Underground)
		{
			CarCombain.SetActive(!Progress.shop.BossDeath1);
			CarKusKus.SetActive(!Progress.shop.BossDeath2);
			Boss1.SetActive(!Progress.shop.BossDeath1);
			Boss2.SetActive(!Progress.shop.BossDeath2);
			Boss3.SetActive(!Progress.shop.BossDeath3);
		}
	}

	private void OnEnable()
	{
		TryAnimateDecors();
		if (Underground)
		{
			return;
		}
		if (Progress.shop.ArenaBrifOpenFromGarage)
		{
			StartCoroutine(DelayArena());
		}
		CarCombain.SetActive(!Progress.shop.BossDeath1);
		CarKusKus.SetActive(!Progress.shop.BossDeath2);
		if (Progress.shop.BossDeath1)
		{
			Boss1.SetActive(value: false);
		}
		else if (Progress.shop.BossDeath2)
		{
			Boss2.SetActive(value: false);
		}
		else if (Progress.shop.BossDeath3)
		{
			Boss3.SetActive(value: false);
		}
		int num = -1;
		List<Mission> list = new List<Mission>();
		Mission mission = Missions[0];
		for (int i = 0; i < Missions.Count; i++)
		{
			Missions[i].Obj.SetActive(value: false);
		}
		if (!SpecialMissionsConf.instance.TimeMisEnd())
		{
			if (Progress.shop.ActivCellNum <= Missions.Count && Progress.shop.ActivCellNum >= 0)
			{
				mission = Missions[Progress.shop.ActivCellNum];
			}
		}
		else
		{
			num = SpecialMissionsConf.instance.OpenMission();
			if (num == -1)
			{
				return;
			}
			Progress.shop.SpecialMissionsRewardCar = num;
			for (int j = 0; j < Missions.Count; j++)
			{
				if (Progress.levels.Pack(Missions[j].LocForOpen).Level(Missions[j].LavelForOpen).isOpen)
				{
					list.Add(Missions[j]);
				}
			}
			List<int> list2 = new List<int>();
			List<int> list3 = new List<int>();
			float num2 = 1.4f;
			int num3 = 1000;
			for (int k = 0; k < list.Count; k++)
			{
				if (list.Count - k <= 3)
				{
					num3 = (int)((float)num3 * num2);
				}
				for (int l = 0; l < num3; l++)
				{
					list2.Add(k);
				}
			}
			int num4 = 0;
			while (list2.Count > 0)
			{
				num4 = UnityEngine.Random.Range(0, list2.Count);
				list3.Add(list2[num4]);
				list2.RemoveAt(num4);
			}
			num4 = UnityEngine.Random.Range(0, list3.Count);
			Progress.shop.ActivCellNum = list3[num4];
			mission = list[Progress.shop.ActivCellNum];
			Progress.shop.SpecialMissionsOpenTime = DateTime.UtcNow;
			Progress.shop.SpecialMissionsLastPlay = DateTime.UtcNow;
		}
		mission.Obj.SetActive(value: true);
		mission.Obj.GetComponent<CellContainer>().SetState(CellContainer.State.Available);
		galery.MissionsLEX(mission.Obj.GetComponent<CellContainer>());
		setAlert = false;
		MissionTimeMarker.transform.position = mission.Obj.transform.position;
		MissionTimeMarkerAnim.SetBool(hash_isOn, value: true);
		timerCorut = StartCoroutine(TimerCorutine());
		StartCoroutine(SetPosCorutine(mission));
	}

	private IEnumerator DelayArena()
	{
		Progress.shop.ArenaBrifOpenFromGarage = false;
		yield return new WaitForSeconds(0.5f);
		int temp = 0;
		for (int i = 1; i <= 3; i++)
		{
			for (int j = 1; j <= 12; j++)
			{
				temp += Progress.levels.Pack(i).Level(j).oldticket;
			}
		}
		if (temp >= DifficultyConfig.instance.BudgesARENA3)
		{
			Arena3.OnButtonClickGlog();
		}
		else if (temp >= DifficultyConfig.instance.BudgesARENA2)
		{
			Arena2.OnButtonClickGlog();
		}
		else if (temp >= DifficultyConfig.instance.BudgesARENA1)
		{
			Arena1.OnButtonClickGlog();
		}
	}

	private IEnumerator SetPosCorutine(Mission mis)
	{
		float t = 1f;
		while (t > 0f)
		{
			MissionTimeMarker.transform.position = mis.Obj.transform.position;
			t -= Time.deltaTime;
			yield return null;
		}
		Transform transform = MissionTimeMarker.transform;
		Vector3 position = mis.Obj.transform.position;
		float x = position.x;
		Vector3 position2 = mis.Obj.transform.position;
		transform.position = new Vector3(x, position2.y + 0.5f);
	}

	private IEnumerator TimerCorutine()
	{
		int min2 = SpecialMissionsConf.instance.GetTimeMisEndMinssss() / 60;
		int sec2 = SpecialMissionsConf.instance.GetTimeMisEndMinssss() - min2 * 60;
		if (sec2 <= 9)
		{
			MissionTime.text = min2.ToString() + ":0" + sec2.ToString();
		}
		else
		{
			MissionTime.text = min2.ToString() + ":" + sec2.ToString();
		}
		while (true)
		{
			float t = 1f;
			while (t > 0f)
			{
				t -= Time.deltaTime;
				yield return null;
			}
			min2 = SpecialMissionsConf.instance.GetTimeMisEndMinssss() / 60;
			sec2 = SpecialMissionsConf.instance.GetTimeMisEndMinssss() - min2 * 60;
			if (min2 <= 0 && sec2 <= 0)
			{
				break;
			}
			if (min2 <= 0 && !setAlert)
			{
				MissionTimeMarkerAnim.SetBool("isHurry", value: true);
				setAlert = true;
			}
			if (sec2 <= 9)
			{
				MissionTime.text = min2.ToString() + ":0" + sec2.ToString();
			}
			else
			{
				MissionTime.text = min2.ToString() + ":" + sec2.ToString();
			}
			yield return null;
		}
		MissionTimeMarkerAnim.SetBool(hash_isOn, value: false);
		OnEnable();
		Progress.shop.ActivCellNum = -1;
		foreach (CellContainer item in MC.CC)
		{
			if (item.Pack == Progress.levels.active_pack_last_openned && item.Level == Progress.levels.active_level_last_openned)
			{
				item.SetState(CellContainer.State.Active, Progress.levels.Pack(item.Pack).Level(item.Level).oldticket);
				item.ActiveRotate.anchoredPosition = item.gameObject.GetComponent<RectTransform>().anchoredPosition;
				UnityEngine.Debug.Log("!#&^$@!!! zapus action for play buttons");
				item.ButtonPlay.Act = item.Gogogogo;
			}
		}
	}

	private void TryAnimateDecors()
	{
		if (Progress.shop.MapDecorsOpenned == null || Progress.shop.MapDecorsOpenned.Count != DecorsAnim.Count)
		{
			Progress.shop.MapDecorsOpenned.Clear();
			for (int i = 0; i < DecorsAnim.Count; i++)
			{
				Progress.shop.MapDecorsOpenned.Add(item: false);
			}
		}
		for (int j = 0; j < DecorsAnim.Count; j++)
		{
			if (!Progress.shop.MapDecorsOpenned[j] && Progress.levels.Pack(DecorsAnim[j].LocForOpen).Level(DecorsAnim[j].LavelForOpen).isOpen)
			{
				StartCoroutine(DelayPlayAnim(DecorsAnim[j].TimeToOpen, DecorsAnim[j].Anim));
				Progress.shop.MapDecorsOpenned[j] = true;
			}
			else if (Progress.shop.MapDecorsOpenned[j] && Progress.levels.Pack(DecorsAnim[j].LocForOpen).Level(DecorsAnim[j].LavelForOpen).isOpen)
			{
				DecorsAnim[j].Anim.SetBool("isOpened", value: true);
			}
		}
	}

	private IEnumerator DelayPlayAnim(float t, Animator anim)
	{
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return null;
		}
		anim.SetBool("isOpened", value: true);
	}

	private void OnDisable()
	{
		if (timerCorut != null)
		{
			StopCoroutine(timerCorut);
		}
	}
}
