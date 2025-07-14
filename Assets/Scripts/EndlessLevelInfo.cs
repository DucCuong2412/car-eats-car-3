using System;
using System.Collections.Generic;
using UnityEngine;

public class EndlessLevelInfo : MonoBehaviour
{
	[Serializable]
	public class CarTypesClass
	{
		public List<int> tcarTupe = new List<int>();
	}

	[Serializable]
	public class CarTypesClassScraps
	{
		public int carTupe;

		public int callScrNum;

		[Header("Death")]
		public string Scrap1;

		public string Scrap2;

		public string Scrap3;

		public string Scrap4;

		public string Scrap5;

		[Header("Beat")]
		public string Scrap1y;

		public string Scrap2y;

		public string Scrap3y;

		public string Scrap4y;

		public string Scrap5y;
	}

	private static EndlessLevelInfo _instance;

	[Header("Underground")]
	public List<CarTypesClassScraps> carTypesScrapsUnder = new List<CarTypesClassScraps>();

	public List<int> chengDateIntervalUnder = new List<int>();

	public List<int> intervalsUnder = new List<int>();

	public List<CarTypesClass> carTypesUnder = new List<CarTypesClass>();

	[Header("Pack 1")]
	public List<CarTypesClassScraps> carTypesScraps = new List<CarTypesClassScraps>();

	public List<int> chengDateInterval = new List<int>();

	public List<int> intervals = new List<int>();

	public List<CarTypesClass> carTypes = new List<CarTypesClass>();

	[Header("Pack 2")]
	public List<CarTypesClassScraps> carTypesScraps2 = new List<CarTypesClassScraps>();

	public List<int> chengDateInterval2 = new List<int>();

	public List<int> intervals2 = new List<int>();

	public List<CarTypesClass> carTypes2 = new List<CarTypesClass>();

	[Header("Pack 3")]
	public List<CarTypesClassScraps> carTypesScraps3 = new List<CarTypesClassScraps>();

	public List<int> chengDateInterval3 = new List<int>();

	public List<int> intervals3 = new List<int>();

	public List<CarTypesClass> carTypes3 = new List<CarTypesClass>();

	[Header("Arena - Pack 1")]
	public List<CarTypesClassScraps> ArenaCarTypesScraps = new List<CarTypesClassScraps>();

	public List<int> ArenaChengDateInterval = new List<int>();

	public List<int> ArenaIntervals = new List<int>();

	public List<CarTypesClass> ArenaCarTypes = new List<CarTypesClass>();

	[Header("Arena - Pack 2")]
	public List<CarTypesClassScraps> ArenaCarTypesScraps2 = new List<CarTypesClassScraps>();

	public List<int> ArenaChengDateInterval2 = new List<int>();

	public List<int> ArenaIntervals2 = new List<int>();

	public List<CarTypesClass> ArenaCarTypes2 = new List<CarTypesClass>();

	[Header("Arena - Pack 3")]
	public List<CarTypesClassScraps> ArenaCarTypesScraps3 = new List<CarTypesClassScraps>();

	public List<int> ArenaChengDateInterval3 = new List<int>();

	public List<int> ArenaIntervals3 = new List<int>();

	public List<CarTypesClass> ArenaCarTypes3 = new List<CarTypesClass>();

	[Header("Event config")]
	public List<CarTypesClassScraps> EventCarTypesScraps = new List<CarTypesClassScraps>();

	public List<int> EventChengDateInterval = new List<int>();

	public List<int> EventIntervals = new List<int>();

	public List<int> EventIntervals_forCivil = new List<int>();

	public List<CarTypesClass> EventCarTypes = new List<CarTypesClass>();

	[Header("Diatance")]
	public int Arena1;

	public int Arena2;

	public int Arena3;

	public static EndlessLevelInfo instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = UnityEngine.Object.FindObjectOfType<EndlessLevelInfo>();
				if (_instance == null)
				{
					_instance = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("EndlessLevInfo"))).GetComponent<EndlessLevelInfo>();
				}
			}
			return _instance;
		}
	}

	public int GetArenaDist(int pack)
	{
		switch (pack)
		{
		case 1:
			return Arena1;
		case 2:
			return Arena2;
		case 3:
			return Arena3;
		default:
			UnityEngine.Debug.Log("!!!!!! LEX ERROR!!!!!! chengDateInterval pack = " + pack);
			return -1;
		}
	}

	public List<int> GetChengDateInterval(int pack, bool arenaNew, bool eventEster)
	{
		if (Progress.levels.InUndeground)
		{
			pack = 4;
		}
		if (arenaNew)
		{
			switch (pack)
			{
			case 1:
				return ArenaChengDateInterval;
			case 2:
				return ArenaChengDateInterval2;
			case 3:
				return ArenaChengDateInterval3;
			}
			UnityEngine.Debug.Log("!!!!!! LEX ERROR!!!!!! chengDateInterval pack = " + pack);
		}
		else if (eventEster)
		{
			return EventChengDateInterval;
		}
		switch (pack)
		{
		case 1:
			return chengDateInterval;
		case 2:
			return chengDateInterval2;
		case 3:
			return chengDateInterval3;
		case 4:
			return chengDateIntervalUnder;
		default:
			UnityEngine.Debug.Log("!!!!!! LEX ERROR!!!!!! chengDateInterval pack = " + pack);
			return null;
		}
	}

	public List<int> GetIntervals(int pack, bool arenaNew, bool eventEster)
	{
		if (Progress.levels.InUndeground)
		{
			pack = 4;
		}
		if (arenaNew)
		{
			switch (pack)
			{
			case 1:
				return ArenaIntervals;
			case 2:
				return ArenaIntervals2;
			case 3:
				return ArenaIntervals3;
			}
			UnityEngine.Debug.Log("!!!!!! LEX ERROR!!!!!! intervals pack = " + pack);
		}
		else if (eventEster)
		{
			return EventIntervals;
		}
		switch (pack)
		{
		case 1:
			return intervals;
		case 2:
			return intervals2;
		case 3:
			return intervals3;
		case 4:
			return intervalsUnder;
		default:
			UnityEngine.Debug.Log("!!!!!! LEX ERROR!!!!!! intervals pack = " + pack);
			return null;
		}
	}

	public List<int> GetIntervals_civ(int pack, bool arenaNew, bool eventEster)
	{
		if (eventEster)
		{
			return EventIntervals_forCivil;
		}
		return null;
	}

	public List<CarTypesClass> GetCarTypes(int pack, bool arenaNew, bool eventEster)
	{
		if (Progress.levels.InUndeground)
		{
			pack = 4;
		}
		if (arenaNew)
		{
			switch (pack)
			{
			case 1:
				return ArenaCarTypes;
			case 2:
				return ArenaCarTypes2;
			case 3:
				return ArenaCarTypes3;
			}
			UnityEngine.Debug.Log("!!!!!! LEX ERROR!!!!!! carTypes pack = " + pack);
		}
		else if (eventEster)
		{
			return EventCarTypes;
		}
		switch (pack)
		{
		case 1:
			return carTypes;
		case 2:
			return carTypes2;
		case 3:
			return carTypes3;
		case 4:
			return carTypesUnder;
		default:
			UnityEngine.Debug.Log("!!!!!! LEX ERROR!!!!!! carTypes pack = " + pack);
			return null;
		}
	}

	public List<CarTypesClassScraps> GetCarTypesScr(int pack, bool arenaNew, bool eventEster)
	{
		if (Progress.levels.InUndeground)
		{
			pack = 4;
		}
		if (arenaNew)
		{
			switch (pack)
			{
			case 1:
				return ArenaCarTypesScraps;
			case 2:
				return ArenaCarTypesScraps2;
			case 3:
				return ArenaCarTypesScraps3;
			}
			UnityEngine.Debug.Log("!!!!!! LEX ERROR!!!!!! carTypes pack = " + pack);
		}
		else if (eventEster)
		{
			return EventCarTypesScraps;
		}
		switch (pack)
		{
		case 1:
			return carTypesScraps;
		case 2:
			return carTypesScraps2;
		case 3:
			return carTypesScraps3;
		case 4:
			return carTypesScrapsUnder;
		default:
			UnityEngine.Debug.Log("!!!!!! LEX ERROR!!!!!! carTypes pack = " + pack);
			return null;
		}
	}
}
