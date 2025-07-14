using System;
using System.Collections.Generic;
using UnityEngine;

namespace Smokoko.DebugModule
{
	public class LogHandler : MonoBehaviour
	{
		[Serializable]
		public class Log
		{
			public string message;

			public string stackTrace;

			public LogType type;
		}

		public delegate void OnLogReceive(Log log);

		public OnLogReceive OnError;

		public OnLogReceive OnAssert;

		public OnLogReceive OnWarning;

		public OnLogReceive OnLog;

		public OnLogReceive OnException;

		[SerializeField]
		private List<Log> allLogs = new List<Log>();

		public List<Log> GetAllLogs()
		{
			List<Log> list = new List<Log>();
			list.AddRange(allLogs.ToArray());
			return list;
		}

		public List<Log> GetAllLogs(LogType logType)
		{
			List<Log> list = new List<Log>();
			list.AddRange(allLogs.ToArray());
			List<Log> retList = new List<Log>();
			list.ForEach(delegate(Log log)
			{
				if (log.type == logType)
				{
					retList.Add(log);
				}
			});
			return retList;
		}

		private void OnEnable()
		{
			Application.logMessageReceived += HandleLog;
		}

		private void OnDisable()
		{
			Application.logMessageReceived -= HandleLog;
		}

		private void HandleLog(string message, string stackTrace, LogType type)
		{
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			{
				string[] array = Environment.StackTrace.Split(new string[1]
				{
					Environment.NewLine
				}, StringSplitOptions.None);
				for (int i = 6; i < array.Length; i++)
				{
					stackTrace = stackTrace + array[i] + "\n";
				}
			}
			Log log = new Log();
			log.message = message;
			log.stackTrace = stackTrace;
			log.type = type;
			Log log2 = log;
			allLogs.Add(log2);
			InvokeEvent(log2);
		}

		private void InvokeEvent(Log log)
		{
			switch (log.type)
			{
			case LogType.Error:
				if (OnError != null)
				{
					OnError(log);
				}
				break;
			case LogType.Assert:
				if (OnAssert != null)
				{
					OnAssert(log);
				}
				break;
			case LogType.Warning:
				if (OnWarning != null)
				{
					OnWarning(log);
				}
				break;
			case LogType.Log:
				if (OnLog != null)
				{
					OnLog(log);
				}
				break;
			case LogType.Exception:
				if (OnException != null)
				{
					OnException(log);
				}
				break;
			}
		}
	}
}
