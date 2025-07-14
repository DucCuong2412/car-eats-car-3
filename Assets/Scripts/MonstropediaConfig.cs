using System;
using System.Collections.Generic;
using UnityEngine;

public class MonstropediaConfig : ScriptableObject
{
	[Serializable]
	public class price
	{
		public List<Cars> Infos = new List<Cars>();
	}

	[Serializable]
	public class Cars
	{
		public string CarName = string.Empty;

		public int Reward;
	}

	private const string ISNSettingsAssetName = "MonstropediaConf";

	private const string ISNSettingsPath = "Resources";

	private const string ISNSettingsAssetExtension = ".asset";

	private static MonstropediaConfig _instance;

	public price Price;

	public static MonstropediaConfig instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (Resources.Load("MonstropediaConf") as MonstropediaConfig);
				if (_instance == null)
				{
					_instance = ScriptableObject.CreateInstance<MonstropediaConfig>();
				}
			}
			return _instance;
		}
	}
}
