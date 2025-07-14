using UnityEngine;

public class RateUsSettings : ScriptableObject
{
	public string title;

	public string message;

	public string rate;

	public string later;

	public string no;

	public string url_android;

	public string url_ios;

	private const string ISNSettingsAssetName = "RateUsSettings";

	private const string ISNSettingsPath = "Extensions/RateUs/Resources";

	private const string ISNSettingsAssetExtension = ".asset";

	private static RateUsSettings instance;

	public static RateUsSettings Instance
	{
		get
		{
			if (instance == null)
			{
				instance = (Resources.Load("RateUsSettings") as RateUsSettings);
				if (instance == null)
				{
					instance = ScriptableObject.CreateInstance<RateUsSettings>();
				}
			}
			return instance;
		}
	}
}
