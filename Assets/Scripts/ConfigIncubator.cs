using System;
using System.Collections.Generic;
using UnityEngine;

public class ConfigIncubator : ScriptableObject
{
	[Serializable]
	public class Eegs
	{
		[Serializable]
		public class Evo
		{
			public int IncubationTime;

			public float ArrowPlasToPress;

			public float ArrowMinusPerFrame;

			[Header("Multipliers")]
			public int Multiplier1;

			public int Multiplier2;

			public int Multiplier3;

			public int Multiplier4;

			[Header("Next mult into arrow")]
			public int Multiplier1Start;

			public int Multiplier2Start;

			public int Multiplier3Start;

			public int Multiplier4Start;
		}

		public int NextPlayTime;

		public List<int> OrderRubyToUnlock;

		public List<Evo> Evos;
	}

	private const string ISNSettingsAssetName = "ConfigIncubator";

	private const string ISNSettingsPath = "Resources";

	private const string ISNSettingsAssetExtension = ".asset";

	private static ConfigIncubator _instance;

	public List<Eegs> EegsList;

	public static ConfigIncubator instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (Resources.Load("ConfigIncubator") as ConfigIncubator);
				if (_instance == null)
				{
					_instance = ScriptableObject.CreateInstance<ConfigIncubator>();
				}
			}
			return _instance;
		}
	}

	public Eegs.Evo GetCurrentIncubation()
	{
		if (Progress.shop.Incubator_EvoStage == 4)
		{
			return EegsList[Progress.shop.Incubator_CurrentEggNum].Evos[3];
		}
		if (Progress.shop.Incubator_EvoStage == -1)
		{
			return EegsList[Progress.shop.Incubator_CurrentEggNum].Evos[0];
		}
		return EegsList[Progress.shop.Incubator_CurrentEggNum].Evos[Progress.shop.Incubator_EvoStage];
	}

	public List<int> GerOrderRubyToUnlock()
	{
		return EegsList[Progress.shop.Incubator_CurrentEggNum].OrderRubyToUnlock;
	}

	public int GerNextPlayTime()
	{
		return EegsList[Progress.shop.Incubator_CurrentEggNum].NextPlayTime;
	}
}
