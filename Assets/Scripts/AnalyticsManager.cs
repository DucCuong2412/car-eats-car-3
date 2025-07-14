using Analytics;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AnalyticsManager
{
	private static int[] daysToLogRetention = new int[11]
	{
		1,
		2,
		3,
		4,
		5,
		6,
		7,
		8,
		9,
		14,
		30
	};

	private static string CurrentLevelName;

	public static string LoadedLevelName => SceneManager.GetActiveScene().name;

	private static void LogEventString(string category, string action, string label = "")
	{
		LogEvent(category, action, label);
	}

	public static void LogRetention(int dayNum)
	{
		LogEventString("retention", " classic retention", "day_" + dayNum.ToString());
	}

	public static void LogRollingRetention(int dayNum)
	{
		LogEventString("retention", "rolling retention", "day_" + dayNum.ToString());
	}

	private static void TrackNewLevel()
	{
		if (CurrentLevelName == null)
		{
			CurrentLevelName = LoadedLevelName;
		}
		if (!CurrentLevelName.Equals(LoadedLevelName))
		{
			CurrentLevelName = LoadedLevelName;
			GoogleAnalyticsV4.instance.LogScreen(CurrentLevelName);
		}
	}

	public static void StartSession()
	{
		GoogleAnalyticsV4.instance.StartSession();
		GoogleAnalyticsV4.instance.LogEvent("StartSession", "Test", "Test", -1L);
		SceneManager.sceneLoaded += delegate
		{
			TrackNewLevel();
		};
		IAnalytics instance = MonoSingleton<Flurry>.Instance;
		instance.StartSession("QTR9JQ36WZCZFNW7N86Q", "MQPNTZTC82Y8STHSR29N");
	}

	public static void LogEvent(EventCategoty category, EventAction action, string label = "", int val = -1)
	{
		LogEvent(category.ToString(), action.ToString(), label, val);
	}

	public static void LogEvent(EventCategoty category, string action, string label = "", int val = -1)
	{
		LogEvent(category.ToString(), action, label, val);
	}

	public static void EndSession()
	{
		GoogleAnalyticsV4.instance.StopSession();
	}

	private static void LogEvent(string category, string action, string label = "", int val = -1)
	{
		MonoSingleton<Flurry>.Instance.LogEvent(category, new Dictionary<string, string>
		{
			{
				action,
				label
			}
		});
	}
}
