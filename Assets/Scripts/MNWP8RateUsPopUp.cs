using UnityEngine;

public class MNWP8RateUsPopUp : MNPopup
{
	public static MNWP8RateUsPopUp Create(string title, string message)
	{
		MNWP8RateUsPopUp mNWP8RateUsPopUp = new GameObject("WP8RateUsPopUp").AddComponent<MNWP8RateUsPopUp>();
		mNWP8RateUsPopUp.title = title;
		mNWP8RateUsPopUp.message = message;
		mNWP8RateUsPopUp.init();
		return mNWP8RateUsPopUp;
	}

	public void init()
	{
	}

	public void OnOkDel()
	{
		dispatch("complete", MNDialogResult.RATED);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void OnCancelDel()
	{
		dispatch("complete", MNDialogResult.DECLINED);
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
