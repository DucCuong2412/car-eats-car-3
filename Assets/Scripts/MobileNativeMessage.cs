using System;
using UnionAssets.FLE;

public class MobileNativeMessage : EventDispatcherBase
{
	public Action OnComplete = delegate
	{
	};

	public MobileNativeMessage(string title, string message)
	{
		init(title, message, "Ok");
	}

	public MobileNativeMessage(string title, string message, string ok)
	{
		init(title, message, ok);
	}

	private void init(string title, string message, string ok)
	{
		MNAndroidMessage mNAndroidMessage = MNAndroidMessage.Create(title, message, ok);
		mNAndroidMessage.addEventListener("complete", OnCompleteListener);
	}

	private void OnCompleteListener(CEvent e)
	{
		OnComplete();
		dispatch("complete", e.data);
	}
}
