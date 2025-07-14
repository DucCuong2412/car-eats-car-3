using UnityEngine;

public class MNAndroidMessage : MNPopup
{
	public string ok;

	public static MNAndroidMessage Create(string title, string message)
	{
		return Create(title, message, "Ok");
	}

	public static MNAndroidMessage Create(string title, string message, string ok)
	{
		MNAndroidMessage mNAndroidMessage = new GameObject("AndroidPopUp").AddComponent<MNAndroidMessage>();
		mNAndroidMessage.title = title;
		mNAndroidMessage.message = message;
		mNAndroidMessage.ok = ok;
		mNAndroidMessage.init();
		return mNAndroidMessage;
	}

	public void init()
	{
		MNAndroidNative.showMessage(title, message, ok);
	}

	public void onPopUpCallBack(string buttonIndex)
	{
		dispatch("complete");
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
