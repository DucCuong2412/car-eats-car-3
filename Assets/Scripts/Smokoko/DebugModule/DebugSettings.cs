using UnityEngine;

namespace Smokoko.DebugModule
{
	public class DebugSettings : ScriptableObject
	{
		private const string ISNSettingsAssetName = "DebugSettings";

		private const string ISNSettingsPath = "Smokoko/Debug/DebugSettings/Resources";

		private const string ISNSettingsAssetExtension = ".asset";

		private static DebugSettings instance;

		[Header("Input")]
		[UnityEngine.Tooltip("Editor keyboard keys to enable/disable debug mode")]
		public KeyCode[] editorKeys = new KeyCode[1]
		{
			KeyCode.F12
		};

		[Header("Touches")]
		[UnityEngine.Tooltip("Touch count to swipe on mobile devices")]
		public int touchesToSwipe = 3;

		public static DebugSettings Instance
		{
			get
			{
				if (instance == null)
				{
					instance = (Resources.Load("DebugSettings") as DebugSettings);
					if (instance == null)
					{
						instance = ScriptableObject.CreateInstance<DebugSettings>();
					}
				}
				return instance;
			}
		}
	}
}
