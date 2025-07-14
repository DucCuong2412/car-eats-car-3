using UnityEngine;

public class RateUsLinks : ScriptableObject
{
	public string url_android = "https://play.google.com/store/apps/details?id=com.smokoko.careatscar3";

	public string url_ios = "https://itunes.apple.com/us/app/car-eats-car-3/id1339559270?l=uk&ls=1&mt=8";

	public string feeedbackMail = "office@smokoko.com";

	private const string ISNSettingsAssetName = "RateUsLinks";

	private const string ISNSettingsPath = "Extensions/DoubleWindowRateUs/Resources";

	private const string ISNSettingsAssetExtension = ".asset";

	private static RateUsLinks instance;

	public static RateUsLinks Instance
	{
		get
		{
			if (instance == null)
			{
				instance = (Resources.Load("RateUsLinks") as RateUsLinks);
				if (instance == null)
				{
					instance = ScriptableObject.CreateInstance<RateUsLinks>();
				}
			}
			return instance;
		}
	}
}
