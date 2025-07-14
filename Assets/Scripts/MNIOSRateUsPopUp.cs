using System;
using UnityEngine;

public class MNIOSRateUsPopUp : MNPopup
{
	public string rate;

	public string remind;

	public string declined;

	public string appleId;

	public static MNIOSRateUsPopUp Create()
	{
		return Create("Like the Game?", "Rate US");
	}

	public static MNIOSRateUsPopUp Create(string title, string message)
	{
		return Create(title, message, "Rate Now", "Ask me later", "No, thanks");
	}

	public static MNIOSRateUsPopUp Create(string title, string message, string rate, string remind, string declined)
	{
		MNIOSRateUsPopUp mNIOSRateUsPopUp = new GameObject("IOSRateUsPopUp").AddComponent<MNIOSRateUsPopUp>();
		mNIOSRateUsPopUp.title = title;
		mNIOSRateUsPopUp.message = message;
		mNIOSRateUsPopUp.rate = rate;
		mNIOSRateUsPopUp.remind = remind;
		mNIOSRateUsPopUp.declined = declined;
		mNIOSRateUsPopUp.init();
		return mNIOSRateUsPopUp;
	}

	public void init()
	{
		MNIOSNative.showRateUsPopUP(title, message, rate, remind, declined);
	}

	public void onPopUpCallBack(string buttonIndex)
	{
		switch (Convert.ToInt16(buttonIndex))
		{
		case 0:
			MNIOSNative.RedirectToAppStoreRatingPage(appleId);
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
