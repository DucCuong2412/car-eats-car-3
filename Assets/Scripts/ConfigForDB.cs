using System;
using System.Collections.Generic;
using UnityEngine;

public class ConfigForDB : ScriptableObject
{
	public enum Icons
	{
		icon_1,
		icon_2,
		icon_3,
		icon_4,
		icon_5,
		icon_6,
		icon_7,
		icon_8,
		icon_9,
		icon_10
	}

	[Serializable]
	public class price
	{
		public List<Day> Days = new List<Day>();
	}

	[Serializable]
	public class Day
	{
		public int fuel;

		public int coin;

		public Icons Icon;

		public bool Drone;
	}

	private const string ISNSettingsAssetName = "DailyBonusManager";

	private const string ISNSettingsPath = "Resources";

	private const string ISNSettingsAssetExtension = ".asset";

	private static ConfigForDB _instance;

	public price RevardOfFirstDay;

	[Header("After the cycle")]
	public Icons iconNextDay;

	public int fuelNextDay;

	public int coinNextDay;

	public static ConfigForDB instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (Resources.Load("DailyBonusManager") as ConfigForDB);
				if (_instance == null)
				{
					_instance = ScriptableObject.CreateInstance<ConfigForDB>();
				}
			}
			return _instance;
		}
	}
}
