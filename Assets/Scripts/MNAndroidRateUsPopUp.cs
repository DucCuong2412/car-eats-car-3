using System;
using UnityEngine;

public class MNAndroidRateUsPopUp : MNPopup
{
	public string yes;

	public string later;

	public string no;

	public string url;

	public static MNAndroidRateUsPopUp Create(string title, string message, string url)
	{
		return Create(title, message, url, "Rate app", "Later", "No, thanks");
	}

	public static MNAndroidRateUsPopUp Create(string title, string message, string url, string yes, string later, string no)
	{
		MNAndroidRateUsPopUp mNAndroidRateUsPopUp = new GameObject("AndroidRateUsPopUp").AddComponent<MNAndroidRateUsPopUp>();
		mNAndroidRateUsPopUp.title = title;
		mNAndroidRateUsPopUp.message = message;
		mNAndroidRateUsPopUp.url = url;
		mNAndroidRateUsPopUp.yes = yes;
		mNAndroidRateUsPopUp.later = later;
		mNAndroidRateUsPopUp.no = no;
		mNAndroidRateUsPopUp.init();
		return mNAndroidRateUsPopUp;
	}

	public void init()
	{
		MNAndroidNative.showRateDialog(title, message, yes, later, no);
	}

	public void onPopUpCallBack(string buttonIndex)
	{
		switch (Convert.ToInt16(buttonIndex))
		{
		case 0:
			MNAndroidNative.RedirectStoreRatingPage(url);
			dispatch("complete", MNDialogResult.RATED);
			break;
		case 1:
			dispatch("complete", MNDialogResult.REMIND);
			break;
		case 2:
			dispatch("complete", MNDialogResult.DECLINED);
			break;
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
