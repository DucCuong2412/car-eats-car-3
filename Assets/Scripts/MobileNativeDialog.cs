using System;
using UnionAssets.FLE;

public class MobileNativeDialog : EventDispatcherBase
{
	public Action<MNDialogResult> OnComplete = delegate
	{
	};

	public MobileNativeDialog(string title, string message)
	{
		init(title, message, "Yes", "No");
	}

	public MobileNativeDialog(string title, string message, string yes, string no)
	{
		init(title, message, yes, no);
	}

	private void init(string title, string message, string yes, string no)
	{
		MNAndroidDialog mNAndroidDialog = MNAndroidDialog.Create(title, message, yes, no);
		mNAndroidDialog.addEventListener("complete", OnCompleteListener);
	}

	private void OnCompleteListener(CEvent e)
	{
		OnComplete((MNDialogResult)e.data);
		dispatch("complete", e.data);
	}
}
