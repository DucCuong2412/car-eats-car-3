using System;
using UnionAssets.FLE;

public class MobileNativeRateUs : EventDispatcherBase
{
	public string title;

	public string message;

	public string yes;

	public string later;

	public string no;

	public string url;

	public string appleId;

	public Action<MNDialogResult> OnComplete = delegate
	{
	};

	public MobileNativeRateUs(string title, string message)
	{
		this.title = title;
		this.message = message;
		yes = "Rate app";
		later = "Later";
		no = "No, thanks";
	}

	public MobileNativeRateUs(string title, string message, string yes, string later, string no)
	{
		this.title = title;
		this.message = message;
		this.yes = yes;
		this.later = later;
		this.no = no;
	}

	public void SetAndroidAppUrl(string _url)
	{
		url = _url;
	}

	public void SetAppleId(string _appleId)
	{
		appleId = _appleId;
	}

	public void Start()
	{
		MNAndroidRateUsPopUp mNAndroidRateUsPopUp = MNAndroidRateUsPopUp.Create(title, message, url, yes, later, no);
		mNAndroidRateUsPopUp.addEventListener("complete", OnCompleteListener);
	}

	private void OnCompleteListener(CEvent e)
	{
		OnComplete((MNDialogResult)e.data);
		dispatch("complete", e.data);
	}
}
