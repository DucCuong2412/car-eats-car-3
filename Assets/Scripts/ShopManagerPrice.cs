using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopManagerPrice : ScriptableObject
{
	[Serializable]
	public class price
	{
		public List<Bomb> Bomb = new List<Bomb>();

		public List<Gadget> Gadget = new List<Gadget>();

		public List<Cars> Car = new List<Cars>();

		public int CarByu1;

		public int CarByu2;

		public string CarByu3;

		public string CarByu4;

		public string CarByu5;

		public string CarByu9;

		public string CarByu10;
	}

	[Serializable]
	public class Cars
	{
		public string CarNum = string.Empty;

		public List<Armor> Armor = new List<Armor>();

		public List<Speed> Speed = new List<Speed>();

		public List<Turbo> Turbo = new List<Turbo>();

		public List<Weapon> Weapon = new List<Weapon>();
	}

	[Serializable]
	public class Bomb
	{
		public string NumberBomb;

		public int price;
	}

	[Serializable]
	public class Gadget
	{
		public string NumberGadget;

		public int price;
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

	private const string ISNSettingsAssetName = "ShopManager";

	private const string ISNSettingsPath = "Resources";

	private const string ISNSettingsAssetExtension = ".asset";

	private static ShopManagerPrice _instance;

	public price Price;

	public static ShopManagerPrice instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (Resources.Load("ShopManager") as ShopManagerPrice);
				if (_instance == null)
				{
					_instance = ScriptableObject.CreateInstance<ShopManagerPrice>();
				}
			}
			return _instance;
		}
	}
}
