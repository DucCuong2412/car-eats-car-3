using UnityEngine;

public class RateUs
{
	private string r_title => RateUsSettings.Instance.title;

	private string r_message => RateUsSettings.Instance.message;

	private string r_rate => RateUsSettings.Instance.rate;

	private string r_later => RateUsSettings.Instance.later;

	private string r_no => RateUsSettings.Instance.no;

	private string r_url => (Application.platform != RuntimePlatform.IPhonePlayer) ? RateUsSettings.Instance.url_android : RateUsSettings.Instance.url_ios;

	~RateUs()
	{
	}

	public static void RateApp(int everyCount = 15)
	{
	}

	public static void ShowToastNotification(string message)
	{
	}
}
