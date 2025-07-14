using System;
using System.Collections.Generic;
using UnityEngine;

public class ResultBoxesConfig : ScriptableObject
{
	public enum Revard
	{
		none,
		Yellow,
		Green,
		Blue,
		White,
		Egg_1,
		Egg_2,
		Egg_3,
		Egg_5,
		Egg_6
	}

	[Serializable]
	public class Level
	{
		public List<Revard> Revards = new List<Revard>();
	}

	private const string ISNSettingsAssetName = "ResultBoxesConfigObj";

	private const string ISNSettingsPath = "Resources";

	private const string ISNSettingsAssetExtension = ".asset";

	private static ResultBoxesConfig _instance;

	public List<Level> LevelsBox = new List<Level>();

	public static ResultBoxesConfig instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (Resources.Load("ResultBoxesConfigObj") as ResultBoxesConfig);
				if (_instance == null)
				{
					_instance = ScriptableObject.CreateInstance<ResultBoxesConfig>();
				}
			}
			return _instance;
		}
	}
}
