using System;
using System.Collections.Generic;
using UnityEngine;

public class CrossPromo : ScriptableObject
{
	[Serializable]
	public class Cros
	{
		[Header("Decor_cross_promo2")]
		public List<Baner1> Decor_cross_promo2 = new List<Baner1>();

		[Space(10f)]
		[Header("Decor_cross_promo")]
		public List<Baner1> Decor_cross_promo = new List<Baner1>();
	}

	[Serializable]
	public class Baner1
	{
		public int LVL;

		public int PUCK;

		public Vector3 postion;
	}

	private const string ISNSettingsAssetName = "CrossPromoSettings";

	private const string ISNSettingsPath = "Resources";

	private const string ISNSettingsAssetExtension = ".asset";

	private static CrossPromo _instance;

	public Cros cros;

	public static CrossPromo instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (Resources.Load("CrossPromoSettings") as CrossPromo);
				if (_instance == null)
				{
					_instance = ScriptableObject.CreateInstance<CrossPromo>();
				}
			}
			return _instance;
		}
	}
}
