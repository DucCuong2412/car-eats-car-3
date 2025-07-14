using System;
using UnityEngine;

public class MNIOSDialog : MNPopup
{
	public string yes;

	public string no;

	public static MNIOSDialog Create(string title, string message)
	{
		return Create(title, message, "Yes", "No");
	}

	public static MNIOSDialog Create(string title, string message, string yes, string no)
	{
		MNIOSDialog mNIOSDialog = new GameObject("IOSPopUp").AddComponent<MNIOSDialog>();
		mNIOSDialog.title = title;
		mNIOSDialog.message = message;
		mNIOSDialog.yes = yes;
		mNIOSDialog.no = no;
		mNIOSDialog.init();
		return mNIOSDialog;
	}

	public void init()
	{
		MNIOSNative.showDialog(title, message, yes, no);
	}

	public void onPopUpCallBack(string buttonIndex)
	{
		switch (Convert.ToInt16(buttonIndex))
		{
		case 0:
			dispatch("complete", MNDialogResult.YES);
			break;
		case 1:
			dispatch("complete", MNDialogResult.NO);
			break;
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
