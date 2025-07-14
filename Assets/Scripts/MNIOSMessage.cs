using UnityEngine;

public class MNIOSMessage : MNPopup
{
	public string ok;

	public static MNIOSMessage Create(string title, string message)
	{
		return Create(title, message, "Ok");
	}

	public static MNIOSMessage Create(string title, string message, string ok)
	{
		MNIOSMessage mNIOSMessage = new GameObject("IOSPopUp").AddComponent<MNIOSMessage>();
		mNIOSMessage.title = title;
		mNIOSMessage.message = message;
		mNIOSMessage.ok = ok;
		mNIOSMessage.init();
		return mNIOSMessage;
	}

	public void init()
	{
		MNIOSNative.showMessage(title, message, ok);
	}

	public void onPopUpCallBack(string buttonIndex)
	{
		dispatch("complete");
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
