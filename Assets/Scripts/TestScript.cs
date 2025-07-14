using Analytics;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TestScript : MonoBehaviour
{
	[Header("Flurry Settings")]
	[SerializeField]
	private string _iosApiKey = string.Empty;

	[SerializeField]
	private string _androidApiKey = string.Empty;

	private void Awake()
	{
		IAnalytics instance = MonoSingleton<Flurry>.Instance;
		instance.SetLogLevel(LogLevel.All);
		instance.StartSession(_iosApiKey, _androidApiKey);
	}

	private void OnGUI()
	{
		int num = 0;
		IAnalytics instance = MonoSingleton<Flurry>.Instance;
		if (Button("Log User Name", num++))
		{
			instance.LogUserID("Github User");
		}
		if (Button("Log User Age", num++))
		{
			instance.LogUserAge(24);
		}
		if (Button("Log User Gender", num++))
		{
			instance.LogUserGender(UserGender.Male);
		}
		if (Button("Log User Location", num++))
		{
		}
		if (Button("Log Event", num++))
		{
			instance.LogEvent("event", new Dictionary<string, string>
			{
				{
					"AppVersion",
					Application.version
				},
				{
					"UnityVersion",
					Application.unityVersion
				}
			});
		}
		if (Button("Begin Timed Event", num++))
		{
			instance.BeginLogEvent("timed-event");
		}
		if (Button("End Timed Event", num++))
		{
			instance.EndLogEvent("timed-event");
		}
		if (Button("Log Page View", num++))
		{
		}
		if (Button("Log Error", num))
		{
			instance.LogError("test-script-error", "Test Error", this);
		}
	}

	private bool Button(string label, int index)
	{
		float num = (float)Screen.width * 0.7f;
		float num2 = (float)Screen.height * 0.1f;
		Rect position = new Rect((float)Screen.width * 0.5f - num * 0.5f, num2 * (float)index * 1.1f, num, num2);
		return GUI.Button(position, label);
	}

	[Conditional("UNITY_EDITOR")]
	private void Assert(bool condition, string message, UnityEngine.Object context)
	{
		if (!condition)
		{
			UnityEngine.Debug.LogError(message, context);
		}
	}

	[Conditional("UNITY_EDITOR")]
	private void AssertNotNull(object target, string message, UnityEngine.Object context)
	{
	}
}
