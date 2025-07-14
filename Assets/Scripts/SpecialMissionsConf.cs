using System;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMissionsConf : ScriptableObject
{
	[Serializable]
	public class Car
	{
		public int carNum;

		public int packForOpen;

		public int lavelForOpen;
	}

	private const string ISNSettingsAssetName = "SpecialConf";

	private const string ISNSettingsPath = "Resources";

	private const string ISNSettingsAssetExtension = ".asset";

	private static SpecialMissionsConf _instance;

	public int TimeBetweenMissions;

	public List<int> timeMissions = new List<int>();

	public List<Car> Cars = new List<Car>();

	public static SpecialMissionsConf instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (Resources.Load("SpecialConf") as SpecialMissionsConf);
				if (_instance == null)
				{
					_instance = ScriptableObject.CreateInstance<SpecialMissionsConf>();
				}
			}
			return _instance;
		}
	}

	private void tryCreate()
	{
		if (Progress.shop.SpecialMissionsGated == null || Progress.shop.SpecialMissionsGated.Count == 0)
		{
			for (int i = 0; i < 15; i++)
			{
				Progress.shop.SpecialMissionsGated.Add(item: false);
				Progress.shop.SpecialMissionsFirstOpen.Add(item: false);
			}
		}
		if (Progress.shop.MonstroCanGetReward == null || Progress.shop.MonstroCanGetReward.Count == 0)
		{
			for (int j = 0; j < 15; j++)
			{
				Progress.shop.MonstroCanGetReward.Add(item: true);
				Progress.shop.MonstroLocks.Add(item: true);
			}
		}
	}

	public int OpenMission()
	{
		tryCreate();
		for (int i = 0; i < Cars.Count; i++)
		{
			if (Progress.levels.Pack(Cars[i].packForOpen).Level(Cars[i].lavelForOpen).isOpen && !Progress.shop.SpecialMissionsFirstOpen[Cars[i].carNum - 1])
			{
				Progress.shop.SpecialMissionsFirstOpen[Cars[i].carNum - 1] = true;
				return Cars[i].carNum;
			}
		}
		if (!TimeEnd())
		{
			return -1;
		}
		for (int j = 0; j < 15; j++)
		{
			if (!Progress.shop.SpecialMissionsGated[Cars[j].carNum - 1] && Progress.levels.Pack(Cars[j].packForOpen).Level(Cars[j].lavelForOpen).isOpen)
			{
				return Cars[j].carNum;
			}
		}
		return -1;
	}

	public int GetTimeMisEndMinssss()
	{
		return timeMissions[Progress.shop.SpecialMissionsRewardCar - 1] * 60 - (int)(DateTime.UtcNow - Progress.shop.SpecialMissionsOpenTime).TotalSeconds;
	}

	public bool TimeEnd()
	{
		if ((DateTime.UtcNow - Progress.shop.SpecialMissionsLastPlay).TotalMinutes < (double)TimeBetweenMissions)
		{
			return false;
		}
		return true;
	}

	public bool TimeMisEnd()
	{
		if (Progress.shop.SpecialMissionsRewardCar - 1 < 0)
		{
			return true;
		}
		if ((DateTime.UtcNow - Progress.shop.SpecialMissionsOpenTime).TotalMinutes < (double)timeMissions[Progress.shop.SpecialMissionsRewardCar - 1])
		{
			return false;
		}
		Progress.shop.ActivCellNum = -1;
		return true;
	}
}
