using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityADSManager : MonoBehaviour
{
	private string gameID = "1801084";

	private static Action<bool> Rewards;

	[CompilerGenerated]
	//private static Action<ShowResult> _003C_003Ef__mg_0024cache0;

	//[CompilerGenerated]
	//private static Action<bool> _003C_003Ef__mg_0024cache1;

	private void Start()
	{
		gameID = "1801084";
		//Advertisement.Initialize(gameID, testMode: false);
		//UnityEngine.Debug.Log(" unity ads version ===>" + Advertisement.version.ToString());
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public static bool ShowAd(Action<bool> rewards = null, bool video = false)
	{
		//ShowOptions showOptions = new ShowOptions();
		//showOptions.resultCallback = HandleShowResult;
		//string placementId = (!video) ? "video" : "rewardedVideo";
		//if (Advertisement.IsReady(placementId))
		//{
		//	Rewards = rewards;
		//	Advertisement.Show(placementId, showOptions);
		//	return true;
		//}
		return false;
	}

	//private static void HandleShowResult(ShowResult result)
	//{
	//	if (Rewards != null)
	//	{
	//		Rewards((result == ShowResult.Finished) ? true : false);
	//	}
	//	Rewards = Empty;
	//}

	private static void Empty(bool check)
	{
	}
}
