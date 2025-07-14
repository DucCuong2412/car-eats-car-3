using System.Runtime.InteropServices;

public class MNIOSNative
{
	[DllImport("__Internal")]
	private static extern void _MNP_ShowRateUsPopUp(string title, string message, string rate, string remind, string declined);

	[DllImport("__Internal")]
	private static extern void _MNP_ShowDialog(string title, string message, string yes, string no);

	[DllImport("__Internal")]
	private static extern void _MNP_ShowMessage(string title, string message, string ok);

	[DllImport("__Internal")]
	private static extern void _MNP_DismissCurrentAlert();

	[DllImport("__Internal")]
	private static extern void _MNP_RedirectToAppStoreRatingPage(string appId);

	[DllImport("__Internal")]
	private static extern void _MNP_ShowPreloader();

	[DllImport("__Internal")]
	private static extern void _MNP_HidePreloader();

	public static void dismissCurrentAlert()
	{
		_MNP_DismissCurrentAlert();
	}

	public static void showRateUsPopUP(string title, string message, string rate, string remind, string declined)
	{
		_MNP_ShowRateUsPopUp(title, message, rate, remind, declined);
	}

	public static void showDialog(string title, string message)
	{
		showDialog(title, message, "Yes", "No");
	}

	public static void showDialog(string title, string message, string yes, string no)
	{
		_MNP_ShowDialog(title, message, yes, no);
	}

	public static void showMessage(string title, string message)
	{
		showMessage(title, message, "Ok");
	}

	public static void showMessage(string title, string message, string ok)
	{
		_MNP_ShowMessage(title, message, ok);
	}

	public static void RedirectToAppStoreRatingPage(string appleId)
	{
		_MNP_RedirectToAppStoreRatingPage(appleId);
	}

	public static void ShowPreloader()
	{
		_MNP_ShowPreloader();
	}

	public static void HidePreloader()
	{
		_MNP_HidePreloader();
	}
}
