using System;
using System.Collections.Generic;
using UnityEngine;

namespace Analytics
{
	public static class FlurryAndroid
	{
		private static readonly string s_FlurryAgentClassName = "com.flurry.android.FlurryAgent";

		private static readonly string s_UnityPlayerClassName = "com.unity3d.player.UnityPlayer";

		private static readonly string s_UnityPlayerActivityName = "currentActivity";

		private static AndroidJavaClass s_FlurryAgent;

		private static AndroidJavaClass FlurryAgent
		{
			get
			{
				if (Application.platform != RuntimePlatform.Android)
				{
					return null;
				}
				if (s_FlurryAgent == null)
				{
					s_FlurryAgent = new AndroidJavaClass(s_FlurryAgentClassName);
				}
				return s_FlurryAgent;
			}
		}

		public static void Dispose()
		{
			if (s_FlurryAgent != null)
			{
				s_FlurryAgent.Dispose();
			}
			s_FlurryAgent = null;
		}

		public static void Init(string apiKey)
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass(s_UnityPlayerClassName))
			{
				using (AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>(s_UnityPlayerActivityName))
				{
					FlurryAgent.CallStatic("init", androidJavaObject, apiKey);
				}
			}
		}

		public static void OnStartSession()
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass(s_UnityPlayerClassName))
			{
				using (AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>(s_UnityPlayerActivityName))
				{
					FlurryAgent.CallStatic("onStartSession", androidJavaObject);
				}
			}
		}

		public static void OnEndSession()
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass(s_UnityPlayerClassName))
			{
				using (AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>(s_UnityPlayerActivityName))
				{
					FlurryAgent.CallStatic("onEndSession", androidJavaObject);
				}
			}
		}

		public static bool IsSessionActive()
		{
			return FlurryAgent.CallStatic<bool>("isSessionActive", new object[0]);
		}

		public static string GetSessionId()
		{
			return FlurryAgent.CallStatic<string>("getSessionId", new object[0]);
		}

		public static int GetAgentVersion()
		{
			return FlurryAgent.CallStatic<int>("getAgentVersion", new object[0]);
		}

		public static string GetReleaseVersion()
		{
			return FlurryAgent.CallStatic<string>("getReleaseVersion", new object[0]);
		}

		public static void SetLogEnabled(bool isEnabled)
		{
			FlurryAgent.CallStatic("setLogEnabled", isEnabled);
		}

		public static void SetLogLevel(LogLevel logLevel)
		{
			FlurryAgent.CallStatic("setLogLevel", (int)logLevel);
		}

		public static void SetVersionName(string versionName)
		{
			FlurryAgent.CallStatic("setVersionName", versionName);
		}

		public static void SetReportLocation(bool reportLocation)
		{
			FlurryAgent.CallStatic("setReportLocation", reportLocation);
		}

		public static void SetLocation(float lat, float lon)
		{
			FlurryAgent.CallStatic("setLocation", lat, lon);
		}

		public static void ClearLocation()
		{
			FlurryAgent.CallStatic("clearLocation");
		}

		public static void SetContinueSessionMillis(long millis)
		{
			FlurryAgent.CallStatic("setContinueSessionMillis", millis);
		}

		public static void SetLogEvents(bool logEvents)
		{
			FlurryAgent.CallStatic("setLogEvents", logEvents);
		}

		public static void SetCaptureUncaughtExceptions(bool isEnabled)
		{
			FlurryAgent.CallStatic("setCaptureUncaughtExceptions", isEnabled);
		}

		public static void AddOrigin(string originName, string originVersion)
		{
			FlurryAgent.CallStatic("addOrigin", originName, originVersion);
		}

		public static void AddOrigin(string originName, string originVersion, Dictionary<string, string> originParameters)
		{
			using (AndroidJavaObject androidJavaObject = DictionaryToJavaHashMap(originParameters))
			{
				FlurryAgent.CallStatic("addOrigin", originName, originVersion, androidJavaObject);
			}
		}

		public static void SetPulseEnabled(bool isEnabled)
		{
			FlurryAgent.CallStatic("setPulseEnabled", isEnabled);
		}

		public static EventRecordStatus LogEvent(string eventId)
		{
			return JavaObjectToEventRecordStatus(FlurryAgent.CallStatic<AndroidJavaObject>("logEvent", new object[1]
			{
				eventId
			}));
		}

		public static EventRecordStatus LogEvent(string eventId, Dictionary<string, string> parameters)
		{
			using (AndroidJavaObject androidJavaObject = DictionaryToJavaHashMap(parameters))
			{
				return JavaObjectToEventRecordStatus(FlurryAgent.CallStatic<AndroidJavaObject>("logEvent", new object[3]
				{
					eventId,
					androidJavaObject,
					false
				}));
			}
		}

		public static EventRecordStatus LogEvent(string eventId, bool timed)
		{
			return JavaObjectToEventRecordStatus(FlurryAgent.CallStatic<AndroidJavaObject>("logEvent", new object[2]
			{
				eventId,
				timed
			}));
		}

		public static EventRecordStatus LogEvent(string eventId, Dictionary<string, string> parameters, bool timed)
		{
			using (AndroidJavaObject androidJavaObject = DictionaryToJavaHashMap(parameters))
			{
				return JavaObjectToEventRecordStatus(FlurryAgent.CallStatic<AndroidJavaObject>("logEvent", new object[3]
				{
					eventId,
					androidJavaObject,
					timed
				}));
			}
		}

		public static void EndTimedEvent(string eventId)
		{
			FlurryAgent.CallStatic("endTimedEvent", eventId);
		}

		public static void EndTimedEvent(string eventId, Dictionary<string, string> parameters)
		{
			using (AndroidJavaObject androidJavaObject = DictionaryToJavaHashMap(parameters))
			{
				FlurryAgent.CallStatic("endTimedEvent", eventId, androidJavaObject);
			}
		}

		public static void OnError(string errorId, string message, string errorClass)
		{
			FlurryAgent.CallStatic("onError", errorId, message, errorClass);
		}

		public static void OnPageView()
		{
			FlurryAgent.CallStatic("onPageView");
		}

		public static void SetAge(int age)
		{
			FlurryAgent.CallStatic("setAge", age);
		}

		public static void SetGender(byte gender)
		{
			FlurryAgent.CallStatic("setGender", gender);
		}

		public static void SetUserId(string userId)
		{
			FlurryAgent.CallStatic("setUserId", userId);
		}

		private static AndroidJavaObject DictionaryToJavaHashMap(Dictionary<string, string> dictionary)
		{
			AndroidJavaObject androidJavaObject = new AndroidJavaObject("java.util.HashMap");
			IntPtr methodID = AndroidJNIHelper.GetMethodID(androidJavaObject.GetRawClass(), "put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
			foreach (KeyValuePair<string, string> item in dictionary)
			{
				using (AndroidJavaObject androidJavaObject2 = new AndroidJavaObject("java.lang.String", item.Key))
				{
					using (AndroidJavaObject androidJavaObject3 = new AndroidJavaObject("java.lang.String", item.Value))
					{
						AndroidJNI.CallObjectMethod(androidJavaObject.GetRawObject(), methodID, AndroidJNIHelper.CreateJNIArgArray(new object[2]
						{
							androidJavaObject2,
							androidJavaObject3
						}));
					}
				}
			}
			return androidJavaObject;
		}

		private static EventRecordStatus JavaObjectToEventRecordStatus(AndroidJavaObject javaObject)
		{
			return (EventRecordStatus)javaObject.Call<int>("ordinal", new object[0]);
		}
	}
}
