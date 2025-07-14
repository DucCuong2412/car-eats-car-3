using System;
using UnityEngine;

public class MNAndroidDialog : MNPopup
{
	public string yes;

	public string no;

	public static MNAndroidDialog Create(string title, string message)
	{
		return Create(title, message, "Yes", "No");
	}

	public static MNAndroidDialog Create(string title, string message, string yes, string no)
	{
		MNAndroidDialog mNAndroidDialog = new GameObject("AndroidPopUp").AddComponent<MNAndroidDialog>();
		mNAndroidDialog.title = title;
		mNAndroidDialog.message = message;
		mNAndroidDialog.yes = yes;
		mNAndroidDialog.no = no;
		mNAndroidDialog.init();
		return mNAndroidDialog;
	}

	public void init()
	{
		MNAndroidNative.showDialog(title, message, yes, no);
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
