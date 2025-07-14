using System;
using UnityEngine;
using UnityEngine.Events;

namespace Smokoko.DebugModule
{
	public class DebugFacade : MonoBehaviour
	{
		private static DebugFacade p_instance;

		private bool pIsDebug;

		private LogHandler logHandler;

		private Console console;

		private SwipeHandler swipe;

		private UnityAction customAction = delegate
		{
		};

		private int counletter;

		private string s = string.Empty;

		private bool isFirstOpen;

		private bool showFPS;

		private static DebugFacade instance
		{
			get
			{
				if (p_instance == null)
				{
					GameObject gameObject = new GameObject("_debug panel");
					p_instance = gameObject.AddComponent<DebugFacade>();
					UnityEngine.Object.DontDestroyOnLoad(gameObject);
				}
				return p_instance;
			}
		}

		public static bool isDebug
		{
			get
			{
				if (p_instance == null)
				{
					return false;
				}
				return p_instance.pIsDebug;
			}
		}

		public static UnityAction CustomAction
		{
			get
			{
				return instance.customAction;
			}
			set
			{
				instance.customAction = value;
			}
		}

		public static bool Init()
		{
			return instance;
		}

		private void OnEnable()
		{
			swipe = base.gameObject.AddComponent<SwipeHandler>();
			swipe.SetTouchCount(DebugSettings.Instance.touchesToSwipe);
			swipe.onShow += OnShowPanelEvent;
			swipe.onHide += OnHidePanelEvent;
		}

		private void Update()
		{
		}

		private void OnReceiveLog(LogHandler.Log log)
		{
			counletter += log.message.Length;
			if (counletter > 10000)
			{
				s = string.Empty;
				counletter = 0;
			}
			string text = s;
			s = text + "\n" + log.type.ToString() + ": " + log.message;
			if (console != null)
			{
				console.ClearText();
				console.AddText(s);
			}
		}

		private void OnFirstOpen()
		{
			logHandler = base.gameObject.AddComponent<LogHandler>();
			if ((bool)logHandler)
			{
			}
			LogHandler obj = logHandler;
			obj.OnAssert = (LogHandler.OnLogReceive)Delegate.Combine(obj.OnAssert, new LogHandler.OnLogReceive(OnReceiveLog));
			LogHandler obj2 = logHandler;
			obj2.OnError = (LogHandler.OnLogReceive)Delegate.Combine(obj2.OnError, new LogHandler.OnLogReceive(OnReceiveLog));
			LogHandler obj3 = logHandler;
			obj3.OnException = (LogHandler.OnLogReceive)Delegate.Combine(obj3.OnException, new LogHandler.OnLogReceive(OnReceiveLog));
			LogHandler obj4 = logHandler;
			obj4.OnLog = (LogHandler.OnLogReceive)Delegate.Combine(obj4.OnLog, new LogHandler.OnLogReceive(OnReceiveLog));
			LogHandler obj5 = logHandler;
			obj5.OnWarning = (LogHandler.OnLogReceive)Delegate.Combine(obj5.OnWarning, new LogHandler.OnLogReceive(OnReceiveLog));
		}

		private void OnShow()
		{
			pIsDebug = true;
			if (!isFirstOpen)
			{
				isFirstOpen = true;
				OnFirstOpen();
			}
		}

		private void OnHide()
		{
			pIsDebug = false;
			if (console != null && !console.gameObject.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(console.gameObject);
			}
		}

		private void OnShowPanelEvent()
		{
			OnShow();
		}

		private void OnHidePanelEvent()
		{
			OnHide();
		}

		private void OnFPS()
		{
			showFPS = !showFPS;
			if (showFPS)
			{
				base.gameObject.AddComponent<HUDFPS>();
			}
			else
			{
				UnityEngine.Object.Destroy(base.gameObject.GetComponent<HUDFPS>());
			}
		}

		private void OnCustom()
		{
			if (customAction != null)
			{
				customAction();
			}
		}

		private void OnConsole()
		{
			if (console == null)
			{
				console = (UnityEngine.Object.Instantiate(Resources.Load("ConsolePrefab")) as GameObject).GetComponent<Console>();
			}
			else
			{
				console.Toggle();
			}
			console.ClearText();
			console.AddText(s);
		}

		private void OnGUI()
		{
			if (pIsDebug)
			{
				float num = (float)Screen.width * 0.5f;
				float num2 = (float)Screen.height * 0.075f;
				Rect rect = new Rect(num / 2f, 0f, num, num2);
				GUIStyle button = GUI.skin.button;
				button.fixedWidth = num * 0.325f;
				button.fixedHeight = num2 * 0.8f;
				button.fontSize = (int)(num * 0.05f);
				GUI.Box(rect, string.Empty);
				GUILayout.BeginArea(rect);
				GUILayout.BeginHorizontal(GUILayout.Width(num));
				if (GUILayout.Button("FPS", button))
				{
					OnFPS();
				}
				if (GUILayout.Button("CONSOLE", button))
				{
					OnConsole();
				}
				if (GUILayout.Button("CUSTOM", button))
				{
					OnCustom();
				}
				GUILayout.EndHorizontal();
				GUILayout.EndArea();
			}
		}
	}
}
