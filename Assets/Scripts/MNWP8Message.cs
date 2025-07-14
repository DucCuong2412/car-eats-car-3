using UnityEngine;

public class MNWP8Message : MNPopup
{
	public static MNWP8Message Create(string title, string message)
	{
		MNWP8Message mNWP8Message = new GameObject("WP8Message").AddComponent<MNWP8Message>();
		mNWP8Message.title = title;
		mNWP8Message.message = message;
		mNWP8Message.init();
		return mNWP8Message;
	}

	public void init()
	{
	}

	public void onPopUpCallBack()
	{
		dispatch("complete");
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
