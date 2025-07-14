using System.Collections.Generic;
using UnityEngine;

public class InAppIDsExample
{
	public enum Purchases
	{
		Moneytier1,
		Moneytier2,
		Moneytier3,
		Moneytier4,
		OpenAllLevels,
		CarForRealMoney
	}

	private const string Moneytier1 = "com.smokoko.mtc.coinstier1";

	private const string Moneytier2 = "com.smokoko.mtc.coinstier2";

	private const string Moneytier3 = "com.smokoko.mtc.coinstier3";

	private const string Moneytier4 = "com.smokoko.mtc.coinstier4";

	private const string OpenAllLevels = "com.smokoko.mtc.openalllevels";

	private const string CarForRealMoney = "com.smokoko.mtc.car";

	public static string GetDefaultPrice(Purchases purch)
	{
		if (PlayerPrefs.HasKey(GetSKU(purch)))
		{
			return PlayerPrefs.GetString(GetSKU(purch));
		}
		switch (purch)
		{
		case Purchases.Moneytier1:
			return "$0.99";
		case Purchases.Moneytier2:
			return "$1.99";
		case Purchases.Moneytier3:
			return "$2.99";
		case Purchases.Moneytier4:
			return "$4.99";
		case Purchases.OpenAllLevels:
			return "$4.99";
		case Purchases.CarForRealMoney:
			return "$4.99";
		default:
			return null;
		}
	}

	public static string GetSKU(Purchases purch)
	{
		switch (purch)
		{
		case Purchases.Moneytier1:
			return "com.smokoko.mtc.coinstier1";
		case Purchases.Moneytier2:
			return "com.smokoko.mtc.coinstier2";
		case Purchases.Moneytier3:
			return "com.smokoko.mtc.coinstier3";
		case Purchases.Moneytier4:
			return "com.smokoko.mtc.coinstier4";
		case Purchases.OpenAllLevels:
			return "com.smokoko.mtc.openalllevels";
		case Purchases.CarForRealMoney:
			return "com.smokoko.mtc.car";
		default:
			return null;
		}
	}

	public static List<string> GetAllSKU()
	{
		List<string> list = new List<string>();
		list.Add("com.smokoko.mtc.coinstier1");
		list.Add("com.smokoko.mtc.coinstier2");
		list.Add("com.smokoko.mtc.coinstier3");
		list.Add("com.smokoko.mtc.coinstier4");
		list.Add("com.smokoko.mtc.openalllevels");
		list.Add("com.smokoko.mtc.car");
		return list;
	}
}
