using UnityEngine;

public class AnalyticsQuitChecker : MonoBehaviour
{
	private void OnApplicationQuit()
	{
		GoogleAnalyticsV4.instance.LogEvent("Quit", "activeLevel", "level_" + Progress.levels.active_pack.ToString() + Progress.levels.active_level.ToString(), -1L);
		GoogleAnalyticsV4.instance.StopSession();
	}
}
