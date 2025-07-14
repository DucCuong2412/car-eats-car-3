using System;
using UnityEngine;

public class PriceConfig : ScriptableObject
{
	[Serializable]
	public class Currency
	{
		[Header("Coin Packs")]
		public int coinsPack1;

		public int coinsPack2;

		public int coinsPack3;

		public int coinsPack4;

		public int coinsPack5;

		[Header("Default Prices")]
		public string coinsPack2DefaultPrice;

		public string coinsPack3DefaultPrice;

		public string coinsPack4DefaultPrice;

		public string coinsPack5DefaultPrice;

		[Header("Benefit Percents")]
		public string coinsPack3Benefit;

		public string coinsPack4Benefit;

		public string coinsPack5Benefit;

		[Header("time restore clic")]
		public int timeForRuby;

		public int timeForFuel;

		public int timeForFortune;

		[Header("dron")]
		public int priceDroneFly;
	}

	[Serializable]
	public class Energy
	{
		[Header("Fuel Packs")]
		public int fuelPack1;

		public int fuelPack2;

		public int fuelPack3;

		public int fuelPack4;

		[Range(float.PositiveInfinity, float.PositiveInfinity)]
		public float fuelPack5Unlimit;

		[Header("Prices")]
		public int fuelPack1Price;

		public int fuelPack2Price;

		public int fuelPack3Price;

		public int fuelPack4Price;

		public string fuelPack5DefaultPrice;

		public string fuelPackToMaxPrice;

		public string fuelPack100Price;

		[Header("Game")]
		public int eachStart;

		public int restoreTime;

		public int maxValue;
	}

	[Serializable]
	public class PremiumContent
	{
		[Header("Premium content prices")]
		public string tankominatorDefaultPrice;

		public string tankDefaultPrice;

		public string supergunDefaultPrice;

		public string antigravsDefaultPrice;

		public string megaturboDefaultPrice;

		public string harvesterDefaultPrice;

		[Header("Premium content benefits")]
		public string tankominatorBenefit;

		[Header("Drones")]
		public string DronBee;

		public string DronBomber;
	}

	[Serializable]
	public class LevelsGallery
	{
		[Header("Levels gallery")]
		public string unlockNextLevelDefaultPrice;

		public string unlockWorld1DefaultPrice;

		public string unlockWorld2DefaultPrice;

		public string unlockWorld3DefaultPrice;

		public string unlockWorld4DefaultPrice;
	}

	[Serializable]
	public class FortuneWheel
	{
		[Header("Fortune wheel")]
		public int rubiesForOneAddictionalSpin;
	}

	[Serializable]
	public class TOU
	{
		[Header("TempOfUse + PrivacyPolicy")]
		public string TempOfUse;

		public string PrivacyPolicy;
	}

	private const string ISNSettingsAssetName = "PriceConfigSettings";

	private const string ISNSettingsPath = "Resources";

	private const string ISNSettingsAssetExtension = ".asset";

	private static PriceConfig _instance;

	public Currency currency;

	public Energy energy;

	public PremiumContent premiumConten;

	public LevelsGallery levelsGallery;

	public FortuneWheel fortuneWheel;

	public TOU PrivasyPolicy;

	public static PriceConfig instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (Resources.Load("PriceConfigSettings") as PriceConfig);
				if (_instance == null)
				{
					_instance = ScriptableObject.CreateInstance<PriceConfig>();
				}
			}
			return _instance;
		}
	}
}
