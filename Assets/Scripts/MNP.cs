public class MNP
{
	public static void ShowPreloader(string title, string message)
	{
		MNAndroidNative.ShowPreloader(title, message);
	}

	public static void HidePreloader()
	{
		MNAndroidNative.HidePreloader();
	}
}
