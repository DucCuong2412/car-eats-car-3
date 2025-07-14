using SA.Common.Pattern;
using UnityEngine;

public class TvOsCloudExample : MonoBehaviour
{
	private void Start()
	{
		UnityEngine.Debug.Log("iCloudManager.Instance.init()");
		iCloudManager.OnCloudDataReceivedAction += OnCloudDataReceivedAction;
		Singleton<iCloudManager>.Instance.setString("Test", "test");
		Singleton<iCloudManager>.Instance.requestDataForKey("Test");
	}

	private void OnCloudDataReceivedAction(iCloudData data)
	{
		UnityEngine.Debug.Log("OnCloudDataReceivedAction");
		if (data.IsEmpty)
		{
			UnityEngine.Debug.Log(data.key + " / data is empty");
		}
		else
		{
			UnityEngine.Debug.Log(data.key + " / " + data.stringValue);
		}
	}
}
