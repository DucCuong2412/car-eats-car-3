using SmartLocalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreloaderCanvas : MonoBehaviour
{
	public Text levelText;

	public Text discriptions;

	public Text Zvantaj;

	public List<GameObject> GarageAndMap = new List<GameObject>();

	public List<GameObject> Loc1 = new List<GameObject>();

	public List<GameObject> Loc2 = new List<GameObject>();

	public List<GameObject> Loc3 = new List<GameObject>();

	public List<GameObject> SpecialMission = new List<GameObject>();

	public List<GameObject> Boss = new List<GameObject>();

	public GameObject arenaImage;

	public List<GameObject> valentineImage;

	public GameObject EsterImage;

	public GameObject Underworld1Img;

	public GameObject UnderworldGoTo;

	public bool GarageAndMaps;

	public bool ArenaNew;

	public bool Monstro;

	public bool Levels;

	public bool SpecialMisions;

	public bool BossMission;

	public bool Tutorial;

	public bool Valentine;

	public bool Ester;

	public bool UnderWorld1;

	private string originText;

	private static string strPerc = " %";

	public void Awake()
	{
		if (Progress.levels.InUndegroundIn_OutPreloader)
		{
			Progress.levels.InUndegroundIn_OutPreloader = false;
			UnderworldGoTo.SetActive(value: true);
			discriptions.gameObject.SetActive(value: false);
			return;
		}
		if (Progress.levels.InUndegroundPreloader)
		{
			UnderWorld1 = true;
			Levels = true;
			Progress.levels.InUndegroundPreloader = false;
		}
		else
		{
			if (Ester && GetTimeNextPlayMinutes() <= 0 && GetTimeNextPlayHours() <= 0 && GetTimeNextPlayDay() <= 0 && GetTimeNextPlaySeconds() <= 0)
			{
				Ester = false;
				GarageAndMaps = true;
			}
			if (!Ester && !Tutorial && !Valentine && !GarageAndMaps && !Monstro)
			{
				if (!Progress.shop.endlessLevel && !Progress.shop.bossLevel && !Progress.shop.ArenaNew && !Progress.shop.EsterLevelPlay)
				{
					Levels = true;
				}
				else if (!Progress.shop.LoadPolicePedia)
				{
					if (Progress.shop.bossLevel)
					{
						BossMission = true;
					}
					else if (!Progress.shop.bossLevel && !Progress.shop.ArenaNew && !Progress.shop.EsterLevelPlay)
					{
						SpecialMisions = true;
					}
					else if (Progress.shop.ArenaNew)
					{
						ArenaNew = true;
					}
					else if (Progress.shop.EsterLevelPlay)
					{
						Ester = true;
					}
				}
				else
				{
					Valentine = true;
				}
			}
		}
		StartCoroutine(StartCorut());
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

	public IEnumerator StartCorut()
	{
		yield return 0;
		SetRandomImages();
		AdvertWrapper.instance.ShowBaner();
	}

	private void OnDisable()
	{
		Hide();
	}

	private void SetLevelText()
	{
		if (!Tutorial)
		{
			if (!Progress.shop.endlessLevel && !Progress.shop.bossLevel && !Progress.shop.ArenaNew && !Progress.shop.LoadPolicePedia && !Progress.shop.EsterLevelPlay)
			{
				if (originText == null)
				{
					originText = levelText.text;
				}
				string newValue = Utilities.LevelNumberGlobal(Progress.levels.active_level, Progress.levels.active_pack).ToString();
				levelText.text = LanguageManager.Instance.GetTextValue("LEVEL *").Replace("*", newValue);
			}
			else if (Progress.shop.bossLevel)
			{
				if (originText == null)
				{
					originText = levelText.text;
				}
				levelText.text = LanguageManager.Instance.GetTextValue("BOSS LEVEL").Replace("*", string.Empty);
			}
			else if (!Progress.shop.bossLevel && !Progress.shop.ArenaNew)
			{
				if (originText == null)
				{
					originText = levelText.text;
				}
				levelText.text = LanguageManager.Instance.GetTextValue("SPECIAL MISSION").Replace("*", string.Empty);
			}
			else if (Progress.shop.ArenaNew)
			{
				if (originText == null)
				{
					originText = levelText.text;
				}
				levelText.text = LanguageManager.Instance.GetTextValue("ARENA *").Replace("*", Progress.levels.active_pack.ToString());
			}
		}
		else
		{
			if (originText == null)
			{
				originText = levelText.text;
			}
			levelText.text = LanguageManager.Instance.GetTextValue("TUTORIAL");
		}
	}

	private void SetRandomImages()
	{
		if (Monstro)
		{
			BoolFalse();
			SetImageMonstro();
		}
		if (Levels)
		{
			BoolFalse();
			SetImageLevels();
		}
		if (SpecialMisions)
		{
			BoolFalse();
			SetImageSpecMission();
		}
		if (BossMission)
		{
			BoolFalse();
			SetImageBoss();
		}
		if (GarageAndMaps)
		{
			BoolFalse();
			SetImageGarage();
		}
		if (Tutorial)
		{
			SetImageGarage();
			SetLevelText();
		}
		if (ArenaNew)
		{
			SetLevelText();
			arenaImage.SetActive(value: true);
			discriptions.text = LanguageManager.Instance.GetTextValue("preloader_description_arena");
		}
		if (Valentine)
		{
			int num = UnityEngine.Random.Range(0, valentineImage.Count);
			valentineImage[num].SetActive(value: true);
			levelText.text = string.Empty;
			if (num == 0)
			{
				discriptions.text = LanguageManager.Instance.GetTextValue("FRANCOPSTEIN - IT'S ALIVE!");
			}
			if (num == 1)
			{
				discriptions.text = LanguageManager.Instance.GetTextValue("CAROCOP - BUNKER ON WHEELS");
			}
			if (num == 2)
			{
				string textValue = LanguageManager.Instance.GetTextValue("A MONSTROUS MIXTURE OF DRILL AND TANK GUN");
				discriptions.text = textValue;
			}
		}
		if (Ester)
		{
			EsterImage.SetActive(value: true);
			levelText.text = LanguageManager.Instance.GetTextValue("EASTER EGG HUNT").Replace("*", string.Empty);
			discriptions.text = LanguageManager.Instance.GetTextValue("RABBITSTER DESCRIPTION");
		}
	}

	private void BoolFalse()
	{
		GarageAndMaps = false;
		Monstro = false;
		Levels = false;
		SpecialMisions = false;
		BossMission = false;
		ArenaNew = false;
		Valentine = false;
		Ester = false;
	}

	private void SetImageGarage()
	{
		int num = UnityEngine.Random.Range(0, GarageAndMap.Count);
		if (num == 9)
		{
			if (!Progress.shop.Cars[8].equipped)
			{
				SetImageGarage();
				return;
			}
			GarageAndMap[num].SetActive(value: true);
		}
		else
		{
			GarageAndMap[num].SetActive(value: true);
		}
		string key = string.Empty;
		switch (num)
		{
		case 0:
			key = "preloader_description_car1";
			break;
		case 1:
			key = "preloader_description_car2";
			break;
		case 2:
			key = "preloader_description_car3";
			break;
		case 3:
			key = "preloader_description_drones";
			break;
		case 4:
			key = "NOTHING WILL STOP HIM";
			break;
		case 5:
			key = "preloader_description_arena";
			break;
		case 6:
			key = "Beetlee description";
			break;
		case 7:
			key = "FRANCOPSTEIN - IT'S ALIVE!";
			break;
		case 8:
			key = "CAROCOP - BUNKER ON WHEELS";
			break;
		case 9:
			key = "RABBITSTER DESCRIPTION";
			break;
		case 10:
		{
			string textValue = LanguageManager.Instance.GetTextValue("A MONSTROUS MIXTURE OF DRILL AND TANK GUN");
			discriptions.text = textValue;
			break;
		}
		}
		if (num != 10)
		{
			discriptions.text = LanguageManager.Instance.GetTextValue(key);
		}
		levelText.text = string.Empty;
	}

	private void SetImageMonstro()
	{
		int index = 0;
		for (int i = 0; i < Progress.shop.SpecialMissionsGated.Count; i++)
		{
			if (!Progress.shop.SpecialMissionsGated[i])
			{
				index = i;
				break;
			}
		}
		SpecialMission[index].SetActive(value: true);
		levelText.text = string.Empty;
		discriptions.text = LanguageManager.Instance.GetTextValue("preloader_description_convoy");
	}

	private void SetImageLevels()
	{
		SetLevelText();
		if (UnderWorld1)
		{
			Underworld1Img.SetActive(value: true);
			discriptions.text = LanguageManager.Instance.GetTextValue("COLLECT EGGS AND GEMS IN THE CAVE");
		}
		else if (Progress.levels.active_pack == 1)
		{
			int num = UnityEngine.Random.Range(0, Loc1.Count);
			Loc1[num].SetActive(value: true);
			switch (num)
			{
			case 4:
				discriptions.text = LanguageManager.Instance.GetTextValue("preloader_description_copter1");
				break;
			case 5:
				discriptions.text = LanguageManager.Instance.GetTextValue("preloader_description_fbi");
				break;
			default:
				discriptions.text = LanguageManager.Instance.GetTextValue("preloader_description_police1_" + (num + 1).ToString());
				break;
			}
		}
		else if (Progress.levels.active_pack == 2)
		{
			int num2 = UnityEngine.Random.Range(0, Loc2.Count);
			Loc2[num2].SetActive(value: true);
			switch (num2)
			{
			case 0:
				discriptions.text = LanguageManager.Instance.GetTextValue("preloader_description_copter2");
				break;
			case 5:
				discriptions.text = LanguageManager.Instance.GetTextValue("preloader_description_fire");
				break;
			case 6:
				discriptions.text = LanguageManager.Instance.GetTextValue("preloader_description_copter1");
				break;
			case 7:
				discriptions.text = LanguageManager.Instance.GetTextValue("preloader_description_fbi");
				break;
			default:
				discriptions.text = LanguageManager.Instance.GetTextValue("preloader_description_police2_" + num2.ToString());
				break;
			}
		}
		else if (Progress.levels.active_pack == 3)
		{
			int num3 = UnityEngine.Random.Range(0, Loc3.Count);
			Loc3[num3].SetActive(value: true);
			if (num3 == 0)
			{
				discriptions.text = LanguageManager.Instance.GetTextValue("preloader_description_copter3");
			}
			else
			{
				discriptions.text = LanguageManager.Instance.GetTextValue("preloader_description_police3_" + num3.ToString());
			}
		}
	}

	private void SetImageBoss()
	{
		SetLevelText();
		Boss[Progress.levels.active_boss_pack_last_openned - 1].SetActive(value: true);
		discriptions.text = LanguageManager.Instance.GetTextValue("preloader_description_boss" + Progress.levels.active_boss_pack_last_openned.ToString());
	}

	private void SetImageSpecMission()
	{
		SetLevelText();
		if (Progress.shop.SpecialMissionsRewardCar - 1 <= SpecialMission.Count)
		{
			SpecialMission[Progress.shop.SpecialMissionsRewardCar - 1].SetActive(value: true);
		}
		else
		{
			SpecialMission[SpecialMission.Count - 1].SetActive(value: true);
		}
		discriptions.text = LanguageManager.Instance.GetTextValue("preloader_description_convoy");
	}

	public IEnumerator MoveToBy(float number, float buff, float time)
	{
		float buf = buff;
		float t = 0f;
		while (t < time)
		{
			t += 0.5f * Time.deltaTime;
			buf = Mathf.Lerp(buf, number, t);
			Zvantaj.text = ((int)buf).ToString() + strPerc;
			yield return null;
		}
		yield return 0;
	}

	public void Hide()
	{
		base.gameObject.SetActive(value: false);
	}
}
