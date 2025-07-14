using System;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyConfig : ScriptableObject
{
	[Serializable]
	public class LevelParamKlick
	{
		public float TimeToDethSec;

		public int ClicksToWin;
	}

	[Serializable]
	public class ColorsClass
	{
		public Vector3 Colored1 = default(Vector3);

		public Vector3 Colored2 = default(Vector3);
	}

	[Serializable]
	public class locations
	{
		public float PercentDmgPoliseCar = 5f;

		public List<Level> Loc1 = new List<Level>();

		public List<Level> Loc2 = new List<Level>();

		public List<Level> Loc3 = new List<Level>();

		public List<Level> LocUnder = new List<Level>();

		public List<Level> LocTut = new List<Level>();
	}

	[Serializable]
	public class Level
	{
		[Header("Percents to next Difficul")]
		public int PercTo1;

		public int PercTo2;

		public int PercTo3;

		[Header("Power percents")]
		public float Power0;

		public float Power1;

		public float Power2;

		public float Power3;
	}

	private const string ISNSettingsAssetName = "DifficultyConf";

	private const string ISNSettingsPath = "Resources";

	private const string ISNSettingsAssetExtension = ".asset";

	private static DifficultyConfig _instance;

	[Header("Clicks for CageMG - MOBILE")]
	public List<LevelParamKlick> ClicksCageM = new List<LevelParamKlick>();

	[Header("Clicks for CageMG - Other")]
	public List<LevelParamKlick> ClicksCage = new List<LevelParamKlick>();

	[Header("Colors for Civilians! for Bodov")]
	public List<ColorsClass> ColorsAll = new List<ColorsClass>();

	[Header("Budges For Open Bosses")]
	public int BudgesBoss1;

	public int BudgesBoss2;

	public int BudgesBoss3;

	[Header("Budges For Open ARENA")]
	public int BudgesARENA1;

	public int BudgesARENA2;

	public int BudgesARENA3;

	[Header("Metraj For Open ARENA")]
	public int MetrivForARENA1;

	public int MetrivForARENA2;

	public int MetrivForARENA3;

	[Header("Rubiniv For Start ARENA")]
	public int RubinivForStartARENA1;

	public int RubinivForStartARENA2;

	public int RubinivForStartARENA3;

	public locations Locations;

	public static DifficultyConfig instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (Resources.Load("DifficultyConf") as DifficultyConfig);
				if (_instance == null)
				{
					_instance = ScriptableObject.CreateInstance<DifficultyConfig>();
				}
			}
			return _instance;
		}
	}

	public float GetDifNumWithoutCurrent(float cur)
	{
		switch (RaceLogic.instance.CurrentDifLevel)
		{
		case 0:
			return cur / 100f * GetCurrentLevel().Power0;
		case 1:
			return cur / 100f * GetCurrentLevel().Power1;
		case 2:
			return cur / 100f * GetCurrentLevel().Power2;
		case 3:
			return cur / 100f * GetCurrentLevel().Power3;
		default:
			UnityEngine.Debug.Log("!!!! ERROR  GetDifficultyPercent -1!!");
			return -1f;
		}
	}

	public int ReturnCurDifIndex(int countEnemy)
	{
		float num = (float)RaceLogic.instance.MaxEnemysInLevel / 100f;
		int num2 = (int)(num * (float)instance.GetCurrentLevel().PercTo1);
		int num3 = (int)(num * (float)instance.GetCurrentLevel().PercTo2);
		int num4 = (int)(num * (float)instance.GetCurrentLevel().PercTo3);
		if (countEnemy < num2)
		{
			return 0;
		}
		if (countEnemy >= num2 && countEnemy < num3)
		{
			return 1;
		}
		if (countEnemy >= num3 && countEnemy < num4)
		{
			return 2;
		}
		if (countEnemy >= num4)
		{
			return 3;
		}
		UnityEngine.Debug.Log("LEX ERROR!!!!! ReturnCurDifIndex return -1!");
		return -1;
	}

	public Level GetCurrentLevel()
	{
		int num = Progress.levels.active_pack;
		int active_level = Progress.levels.active_level;
		if (Progress.shop.Tutorial)
		{
			num = 4;
		}
		if (Progress.levels.InUndeground)
		{
			num = 5;
		}
		switch (num)
		{
		case 1:
			return Locations.Loc1[active_level - 1];
		case 2:
			return Locations.Loc2[active_level - 1];
		case 3:
			return Locations.Loc3[active_level - 1];
		case 4:
			return Locations.LocTut[0];
		case 5:
			return Locations.LocUnder[active_level - 1];
		default:
			return null;
		}
	}
}
