using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopManagerStats : ScriptableObject
{
	[Serializable]
	public class price
	{
		public Cars[] Car = new Cars[4];
	}

	[Serializable]
	public class Cars
	{
		public string CarNum = string.Empty;

		public Stock Stock;

		public List<Armor> Armor = new List<Armor>();

		public List<Speed> Speed = new List<Speed>();

		public List<Turbo> Turbo = new List<Turbo>();

		public List<Weapon> Weapon = new List<Weapon>();
	}

	[Serializable]
	public class Stock
	{
		public int ArmorStats;

		public int SpeedStats;

		public int TurboStats;

		public int WeaponStats;
	}

	[Serializable]
	public class Armor
	{
		public string NumberArmor;

		public int price;
	}

	[Serializable]
	public class Speed
	{
		public string NumberSpeed;

		public int price;
	}

	[Serializable]
	public class Weapon
	{
		public string NumberWeapon;

		public int price;
	}

	[Serializable]
	public class Turbo
	{
		public string NumberTurbo;

		public int price;
	}

	private const string ISNSettingsAssetName = "ShopManagerStats";

	private const string ISNSettingsPath = "Resources";

	private const string ISNSettingsAssetExtension = ".asset";

	private static ShopManagerStats _instance;

	public price Price;

	public static ShopManagerStats instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (Resources.Load("ShopManagerStats") as ShopManagerStats);
				if (_instance == null)
				{
					_instance = ScriptableObject.CreateInstance<ShopManagerStats>();
				}
			}
			return _instance;
		}
	}
}
