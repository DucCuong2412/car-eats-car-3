using System;
using UnityEngine;

public class ConfigForPolicePedia : ScriptableObject
{
	[Serializable]
	public class PartForOpenCar1
	{
		[Header("Coll part ")]
		public int CollPart1;

		public int CollPart2;

		public int CollPart3;

		public int CollPart4;
	}

	private const string ISNSettingsAssetName = "ConfigForPolicePedia";

	private const string ISNSettingsPath = "Resources";

	private const string ISNSettingsAssetExtension = ".asset";

	private static ConfigForPolicePedia _instance;

	public PartForOpenCar1 Car1;

	public PartForOpenCar1 Car2;

	public PartForOpenCar1 Car3;

	public static ConfigForPolicePedia instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (Resources.Load("ConfigForPolicePedia") as ConfigForPolicePedia);
				if (_instance == null)
				{
					_instance = ScriptableObject.CreateInstance<ConfigForPolicePedia>();
				}
			}
			return _instance;
		}
	}
}
