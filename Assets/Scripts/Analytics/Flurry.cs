using System.Collections.Generic;
using UnityEngine;

namespace Analytics
{
	public sealed class Flurry : MonoSingleton<Flurry>, IAnalytics
	{
		private void Awake()
		{
			Application.logMessageReceived += ErrorHandler;
		}

		protected override void OnDestroy()
		{
			//FlurryAndroid.Dispose();
			base.OnDestroy();
		}

		private void ErrorHandler(string condition, string stackTrace, LogType type)
		{
			if (type == LogType.Error)
			{
				LogError("Uncaught Unity Exception", condition, this);
			}
		}

		public void StartSession(string apiKeyIOS, string apiKeyAndroid)
		{
			//FlurryAndroid.Init(apiKeyAndroid);
			//FlurryAndroid.OnStartSession();
		}

		public void LogAppVersion(string version)
		{
			//FlurryAndroid.SetVersionName(version);
		}

		public void SetLogLevel(LogLevel level)
		{
			//FlurryAndroid.SetLogLevel(level);
		}

		public EventRecordStatus LogEvent(string eventName)
		{
			return default;// FlurryAndroid.LogEvent(eventName);
		}

		public EventRecordStatus LogEvent(string eventName, Dictionary<string, string> parameters)
		{
			return default;// FlurryAndroid.LogEvent(eventName, parameters);
		}

		//public EventRecordStatus LogEvent(string eventName, bool timed)
		//{
		//	return FlurryAndroid.LogEvent(eventName, timed);
		//}

		public EventRecordStatus BeginLogEvent(string eventName)
		{
			return  default;// FlurryAndroid.LogEvent(eventName, timed: true);
        }

		public EventRecordStatus BeginLogEvent(string eventName, Dictionary<string, string> parameters)
		{
            return default;//return FlurryAndroid.LogEvent(eventName, parameters, timed: true);
        }

		public void EndLogEvent(string eventName)
		{
            //return default;//FlurryAndroid.EndTimedEvent(eventName);
        }

		public void EndLogEvent(string eventName, Dictionary<string, string> parameters)
		{
            //return default;//FlurryAndroid.EndTimedEvent(eventName, parameters);
        }

		public void LogError(string errorID, string message, object target)
		{
            //return default;//	FlurryAndroid.OnError(errorID, message, target.GetType().Name);
        }

		public void LogUserID(string userID)
		{
            /// default;//FlurryAndroid.SetUserId(userID);
        }

		public void LogUserAge(int age)
		{
            //return default;//	FlurryAndroid.SetAge(age);
        }

		public void LogUserGender(UserGender gender)
		{
			int num;
			switch (gender)
			{
			case UserGender.Male:
				num = 1;
				break;
			case UserGender.Female:
				num = 0;
				break;
			default:
				num = -1;
				break;
			}
			//FlurryAndroid.SetGender((byte)num);
		}
	}
}
