using UnityEngine;

namespace Smokoko.Social
{
	public class FBLeaderboardSettings : ScriptableObject
	{
		public string DefaultPlayerName = "Player";

		public Texture2D DefaultPlayerImage;

		public Texture2D DefaultUserImage;

		private const string ISNSettingsAssetName = "FBLeaderboard";

		private const string ISNSettingsPath = "Smokoko/FBLeaderboard/Resources";

		private const string ISNSettingsAssetExtension = ".asset";

		private static FBLeaderboardSettings instance;

		public static FBLeaderboardSettings Instance
		{
			get
			{
				if (instance == null)
				{
					instance = (Resources.Load("FBLeaderboard") as FBLeaderboardSettings);
					if (instance == null)
					{
						instance = ScriptableObject.CreateInstance<FBLeaderboardSettings>();
					}
				}
				return instance;
			}
		}
	}
}
