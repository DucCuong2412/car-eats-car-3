using System.Collections.Generic;
using UnityEngine;

public class InAppSettings : ScriptableObject
{
	public enum Purchases
	{
		Rubies1,
		Rubies2,
		Rubies3,
		Rubies4,
		UnlockNext,
		UnlockWorld1,
		UnlockWorld2,
		UnlockWorld3,
		UnlimitedFuel,
		FuelUpTank,
		FuelAddMore,
		DroneBee,
		DroneBomber,
		PremiumCar,
		PremiumCar2,
		Megapack
	}

	public string Base64KeyAndroid = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAjruRBjzqIsAj/Y/lVwosm4vpz/JtgUe0zV2VG5ZLdTL/svcgckwfHuSSVg2fFFL8QnQdAHoR227OiBLu9+jLNjwFjVYSucjXYLMhX74EN7Z5JSwNGXz7dM+nP8pOBqXov/YpeGkUU19+MEgBRLC00zrqo4+yeBUCZPOARr7/VgIBdPSCu23yNtYA46QJo9CIl3UmP40Vt0bjybeV/yareWpG3UVfTgoL3fjDcVlhJq2xml4bRYeWZsy06xodX50Ambjb+O6u0vnHVKUVrZOb0xWHvvXchXa9v33xe4N/D4HrwStItW+lsjLnz4qlMTLxdujMprNXQfJbXyjWf1HgtQIDAQAB";

	private const string ISNSettingsAssetName = "InAppSettings";

	private const string ISNSettingsPath = "Extensions/In-App/Resources";

	private const string ISNSettingsAssetExtension = ".asset";

	private static InAppSettings _instance;

	public string Rubies1 = "com.smokoko.careatscar3.rubies1";

	public string Rubies2 = "com.smokoko.careatscar3.rubies2";

	public string Rubies3 = "com.smokoko.careatscar3.rubies3";

	public string Rubies4 = "com.smokoko.careatscar3.rubies4";

	public string UnlockNext = "com.smokoko.careatscar3.alllevels";

	public string UnlockWorld1 = "com.smokoko.careatscar3.world1";

	public string UnlockWorld2 = "com.smokoko.careatscar3.world2";

	public string UnlockWorld3 = "com.smokoko.careatscar3.world3";

	public string UnlimitedFuel = "com.smokoko.careatscar3.unlimited_fuel";

	public string FuelUpTank = "com.smokoko.careatscar3.tankupgrade";

	public string FuelAddMore = "com.smokoko.careatscar3.fueltruck";

	public string DroneBee = "com.smokoko.careatscar3.premiumdrone";

	public string DroneBomber = "com.smokoko.careatscar3.drone";

	public string PremiumCar = "com.smokoko.careatscar3.premiumcar";

	public string PremiumCar2 = string.Empty;

	public string Megapack = "com.smokoko.careatscar3.megapack";

	public List<string> nonConsumable = new List<string>();

	public static InAppSettings instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (Resources.Load("InAppSettings") as InAppSettings);
				if (_instance == null)
				{
					_instance = ScriptableObject.CreateInstance<InAppSettings>();
				}
			}
			return _instance;
		}
	}

	public string GetDefaultPrice(Purchases purch)
	{
		string sKU = GetSKU(purch);
		if (PlayerPrefs.HasKey(sKU))
		{
			return PlayerPrefs.GetString(sKU);
		}
		return null;
	}

	public string GetSKU(Purchases purch)
	{
		switch (purch)
		{
		case Purchases.Rubies1:
			return Rubies1;
		case Purchases.Rubies2:
			return Rubies2;
		case Purchases.Rubies3:
			return Rubies3;
		case Purchases.Rubies4:
			return Rubies4;
		case Purchases.UnlockNext:
			return UnlockNext;
		case Purchases.UnlockWorld1:
			return UnlockWorld1;
		case Purchases.UnlockWorld2:
			return UnlockWorld2;
		case Purchases.UnlockWorld3:
			return UnlockWorld3;
		case Purchases.UnlimitedFuel:
			return UnlimitedFuel;
		case Purchases.DroneBee:
			return DroneBee;
		case Purchases.DroneBomber:
			return DroneBomber;
		case Purchases.FuelAddMore:
			return FuelAddMore;
		case Purchases.FuelUpTank:
			return FuelUpTank;
		case Purchases.PremiumCar:
			return PremiumCar;
		case Purchases.PremiumCar2:
			return PremiumCar2;
		case Purchases.Megapack:
			return Megapack;
		default:
			return null;
		}
	}

	public List<string> GetAllSKU()
	{
		List<string> list = new List<string>();
		list.Add(Rubies1);
		list.Add(Rubies2);
		list.Add(Rubies3);
		list.Add(Rubies4);
		list.Add(UnlockNext);
		list.Add(UnlockWorld1);
		list.Add(UnlockWorld2);
		list.Add(UnlockWorld3);
		list.Add(UnlimitedFuel);
		list.Add(DroneBee);
		list.Add(DroneBomber);
		list.Add(FuelAddMore);
		list.Add(FuelUpTank);
		list.Add(PremiumCar);
		list.Add(PremiumCar2);
		list.Add(Megapack);
		return list;
	}
}
