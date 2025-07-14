using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeToDie : ScriptableObject
{
	[Serializable]
	public class TimeToDies
	{
		[Header("Time To Die In Special Mission")]
		public List<Die> TimeToDie = new List<Die>();
	}

	[Serializable]
	public class Die
	{
		public string MissionNumber;

		public float TimeDie;

		public float HPConvoi;

		public float SpeedConvoi;

		public float TorqueConvoi;
	}

	private const string ISNSettingsAssetName = "TimeToDieSettings";

	private const string ISNSettingsPath = "Resources";

	private const string ISNSettingsAssetExtension = ".asset";

	private static TimeToDie _instance;

	public TimeToDies timeToDie;

	public static TimeToDie instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (Resources.Load("TimeToDieSettings") as TimeToDie);
				if (_instance == null)
				{
					_instance = ScriptableObject.CreateInstance<TimeToDie>();
				}
			}
			return _instance;
		}
	}
}
