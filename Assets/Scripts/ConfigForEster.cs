using System;
using System.Collections.Generic;
using UnityEngine;

public class ConfigForEster : ScriptableObject
{
	[Serializable]
	public class TOU
	{
		public int TimeToOpensVideoX2Mins = 60;

		public int TimeToNextPlayMins = 15;

		public int price1 = 10000;

		public int price2 = 20000;

		public int price3 = 30000;

		public int price4 = 40000;

		public int priceForVideo = 400;

		public int TimeToOpensVideoFreeRubiesMins = 60;

		public int DAMAGE_BOMBER = 200;
	}

	[Serializable]
	public class Car
	{
		public List<Diction> Cars = new List<Diction>();
	}

	[Serializable]
	public class Diction
	{
		public string CarNumber;

		public int CarPercent;
	}

	private const string ISNSettingsAssetName = "ConfigForEster";

	private const string ISNSettingsPath = "Resources";

	private const string ISNSettingsAssetExtension = ".asset";

	private static ConfigForEster _instance;

	public TOU Config;

	public Car CARS;

	public static ConfigForEster instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (Resources.Load("ConfigForEster") as ConfigForEster);
				if (_instance == null)
				{
					_instance = ScriptableObject.CreateInstance<ConfigForEster>();
				}
			}
			return _instance;
		}
	}
}
