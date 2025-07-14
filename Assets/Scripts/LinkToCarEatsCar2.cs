using System;
using UnityEngine;

public class LinkToCarEatsCar2 : ScriptableObject
{
	[Serializable]
	public class Link
	{
		public string Link_Android;

		public string Link_IOS;
	}

	private const string ISNSettingsAssetName = "LinkToCarEatsCar2Settings";

	private const string ISNSettingsPath = "Resources";

	private const string ISNSettingsAssetExtension = ".asset";

	private static LinkToCarEatsCar2 _instance;

	public Link link;

	public static LinkToCarEatsCar2 instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (Resources.Load("LinkToCarEatsCar2Settings") as LinkToCarEatsCar2);
				if (_instance == null)
				{
					_instance = ScriptableObject.CreateInstance<LinkToCarEatsCar2>();
				}
			}
			return _instance;
		}
	}
}
