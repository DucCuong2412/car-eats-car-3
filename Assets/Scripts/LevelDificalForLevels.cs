using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelDificalForLevels : ScriptableObject
{
	[Serializable]
	public class Link
	{
		public List<ForLevel> leveldif = new List<ForLevel>();
	}

	[Serializable]
	public class ForLevel
	{
		public string NumberLevel;

		public float medium;

		public float hard;
	}

	private const string ISNSettingsAssetName = "LevelDiff";

	private const string ISNSettingsPath = "Resources";

	private const string ISNSettingsAssetExtension = ".asset";

	private static LevelDificalForLevels _instance;

	public Link link;

	public static LevelDificalForLevels instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (Resources.Load("LevelDiff") as LevelDificalForLevels);
				if (_instance == null)
				{
					_instance = ScriptableObject.CreateInstance<LevelDificalForLevels>();
				}
			}
			return _instance;
		}
	}
}
