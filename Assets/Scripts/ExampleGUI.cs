using com.adjust.sdk;
using UnityEngine;

public class ExampleGUI : MonoBehaviour
{
	private int nr_buttons = 5;

	private static bool isEnabled;

	private void OnGUI()
	{
	}

	public void responseDelegate(ResponseData responseData)
	{
		UnityEngine.Debug.Log("Was success? " + responseData.success);
		UnityEngine.Debug.Log("Will retry? " + responseData.willRetry);
		if (!string.IsNullOrEmpty(responseData.activityKindString))
		{
			UnityEngine.Debug.Log("activityKind " + responseData.activityKindString);
		}
		if (responseData.trackerName != null)
		{
			UnityEngine.Debug.Log("trackerName " + responseData.trackerName);
		}
		if (responseData.trackerToken != null)
		{
			UnityEngine.Debug.Log("trackerToken " + responseData.trackerToken);
		}
		if (responseData.network != null)
		{
			UnityEngine.Debug.Log("network " + responseData.network);
		}
		if (responseData.campaign != null)
		{
			UnityEngine.Debug.Log("campaign " + responseData.campaign);
		}
		if (responseData.adgroup != null)
		{
			UnityEngine.Debug.Log("adgroup " + responseData.adgroup);
		}
		if (responseData.creative != null)
		{
			UnityEngine.Debug.Log("creative " + responseData.creative);
		}
		if (responseData.error != null)
		{
			UnityEngine.Debug.Log("error " + responseData.error);
		}
	}
}
