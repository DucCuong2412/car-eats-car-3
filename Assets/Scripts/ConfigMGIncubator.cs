using System;
using System.Collections.Generic;
using UnityEngine;

public class ConfigMGIncubator : ScriptableObject
{
	[Serializable]
	public class Evo
	{
		[Serializable]
		public class Stage
		{
			public int NumToStart;

			public int NumOnCycle;

			public float Speed;

			[Header("Chance Generating")]
			public int Empty;

			public int CopCar;

			public int CivilCar;

			public int InterDecor;

			public int StarBig;

			[Header("Stars Pattern")]
			public int MinStarNum;

			public int MaxStarNum;

			public float TimeBetweenStarSec;

			[Header("Decors")]
			public int MinCycle;

			public int MaxCycle;

			public int ChanceToGnrDecor;
		}

		public int StarsToWin;

		public int CopDmgStars;

		public int CivilDmgStars;

		public int StoneDmgStars;

		public List<Stage> Stages;
	}

	private const string ISNSettingsAssetName = "ConfigMGIncubator";

	private const string ISNSettingsPath = "Resources";

	private const string ISNSettingsAssetExtension = ".asset";

	private static ConfigMGIncubator _instance;

	public List<Evo> ForEvo;

	public static ConfigMGIncubator instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (Resources.Load("ConfigMGIncubator") as ConfigMGIncubator);
				if (_instance == null)
				{
					_instance = ScriptableObject.CreateInstance<ConfigMGIncubator>();
				}
			}
			return _instance;
		}
	}
}
