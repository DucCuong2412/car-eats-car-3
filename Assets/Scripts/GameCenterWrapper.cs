using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using Smokoko.Progress;
using System;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using xmlClassTemplate;

public static class GameCenterWrapper
{
	public delegate void DelAvailableSaveFound();

	[Serializable]
	public class CloudProgress
	{
		public Progress.Levels levels;

		public Progress.Review review;

		public Progress.Settings settings;

		public Progress.Shop shop;

		public Progress.PackInfo packInfo;

		public Progress.Notification notification;

		public Progress.AchievementsProgress achievementsProgress;

		public Progress.Achievements achievements;

		public Progress.GameEnergy gameEnergy;

		public Progress.Notifications notifications;

		public CloudProgress()
		{
		}

		public CloudProgress(bool fromProgress)
		{
			if (fromProgress)
			{
				shop = Progress.shop;
				levels = Progress.levels;
				review = Progress.review;
				settings = Progress.settings;
				packInfo = Progress.packInfo;
				notification = Progress.notification;
				achievementsProgress = Progress.achievementsProgress;
				achievements = Progress.achievements;
				gameEnergy = Progress.gameEnergy;
				notifications = Progress.notifications;
			}
		}

		public CloudProgress(CloudProgress other)
		{
			achievementsProgress = other.achievementsProgress;
			shop = other.shop;
			notification = other.notification;
			levels = other.levels;
			review = other.review;
			settings = other.settings;
			packInfo = other.packInfo;
			achievements = other.achievements;
			gameEnergy = other.gameEnergy;
			notifications = other.notifications;
		}
	}

	private const string GP_CONNENTED = "GP_CONNENTED";

	public static DelAvailableSaveFound OnAvailableSaveFound = delegate
	{
	};

	private static bool isInited = false;

	public static bool _isLoggedIn = false;

	private static CloudProgress loadedCloudProgress = null;

	private static string loadedLevelNum = "Not loaded";

	private static int loadedLevelCoins = -1;

	public static ISavedGameMetadata temp;

	[CompilerGenerated]
	private static Action<SavedGameRequestStatus, ISavedGameMetadata> _003C_003Ef__mg_0024cache0;

	[CompilerGenerated]
	private static Action<SavedGameRequestStatus, ISavedGameMetadata> _003C_003Ef__mg_0024cache1;

	[CompilerGenerated]
	private static Action<SavedGameRequestStatus, byte[]> _003C_003Ef__mg_0024cache2;

	[CompilerGenerated]
	private static Action<SavedGameRequestStatus, ISavedGameMetadata> _003C_003Ef__mg_0024cache3;

	public static bool isLoggedIn
	{
		get
		{
			return _isLoggedIn;
		}
		set
		{
			value = _isLoggedIn;
		}
	}

	public static void Init()
	{
		if (!isInited)
		{
			isInited = true;
			if (PlayerPrefs.GetInt("GP_CONNENTED", 1) > 0)
			{
				Connect();
			}
		}
	}

	public static void Connect()
	{
	}

	public static void LoadGameSaves()
	{
		if (Progress.settings.LoginToGP)
		{
			UnityEngine.Debug.Log("#@$!#!@#!@# LOADING PROGRESS ");
			OpenSavedGame("md");
		}
		else
		{
			UnityEngine.Debug.Log("#@$!#!@#!@# LOADING PROGRESS FALSE (NOT LOGIN TO GP)");
		}
	}

	public static void ReplaceLocalSaveFromCloud()
	{
		if (loadedCloudProgress != null)
		{
			UnityEngine.Debug.Log("REPLACING" + loadedCloudProgress);
			Progress.levels = loadedCloudProgress.levels;
			Progress.review = loadedCloudProgress.review;
			Progress.settings = loadedCloudProgress.settings;
			Progress.packInfo = loadedCloudProgress.packInfo;
			Progress.shop = loadedCloudProgress.shop;
			Progress.notification = loadedCloudProgress.notification;
			Progress.gameEnergy = loadedCloudProgress.gameEnergy;
			Progress.notifications = loadedCloudProgress.notifications;
			Progress.shop.currency = loadedCloudProgress.shop.currency;
		}
	}

	public static void SaveGameSave()
	{
		if (Progress.settings.LoginToGP)
		{
			UnityEngine.Debug.Log("SaveGameSave");
			TimeSpan totalPlaytime = new TimeSpan(20000L);
			CloudProgress details = new CloudProgress(fromProgress: true);
			string s = XML.Serialize<CloudProgress>(details);
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			SaveGame(temp, bytes, totalPlaytime);
		}
	}

	public static void ShowGAmeSavesUI()
	{
	}

	public static void ShowCloudProgress()
	{
		if (Progress.shop.foundProgress)
		{
			ProgressBase<Progress>.GetInstance().ForSave(loadedLevelNum, loadedLevelCoins);
		}
	}

	private static void OpenSavedGame(string filename)
	{
		UnityEngine.Debug.Log("#@$!#!@#!@# OpenSavedGame");
		ISavedGameClient savedGame = PlayGamesPlatform.Instance.SavedGame;
		savedGame.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
	}

	public static void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
	{
		if (status == SavedGameRequestStatus.Success)
		{
			UnityEngine.Debug.Log("#@$!#!@#!@# OnSavedGameOpened");
			LoadGameData(game);
			temp = game;
		}
	}

	public static void SaveGame(ISavedGameMetadata game, byte[] savedData, TimeSpan totalPlaytime)
	{
		UnityEngine.Debug.Log("#@$!#!@#!@# SaveGame");
		ISavedGameClient savedGame = PlayGamesPlatform.Instance.SavedGame;
		Texture2D screenshot = getScreenshot();
		SavedGameMetadataUpdate.Builder builder = default(SavedGameMetadataUpdate.Builder).WithUpdatedPlayedTime(totalPlaytime).WithUpdatedDescription("Saved game at " + DateTime.Now);
		if (screenshot != null)
		{
			byte[] newPngCoverImage = screenshot.EncodeToPNG();
			builder = builder.WithUpdatedPngCoverImage(newPngCoverImage);
		}
		SavedGameMetadataUpdate updateForMetadata = builder.Build();
		savedGame.CommitUpdate(game, updateForMetadata, savedData, OnSavedGameWritten);
	}

	public static void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
	{
		UnityEngine.Debug.Log("#@$!#!@#!@# OnSavedGameWritten");
		if (status == SavedGameRequestStatus.Success)
		{
			UnityEngine.Debug.Log("#@$!#!@#!@# OnSavedGameWrittenSuccess");
		}
	}

	public static Texture2D getScreenshot()
	{
		Texture2D texture2D = new Texture2D(1024, 700);
		texture2D.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.width / 1024 * 700), 0, 0);
		return texture2D;
	}

	private static void LoadGameData(ISavedGameMetadata game)
	{
		UnityEngine.Debug.Log("#@$!#!@#!@# LoadGameData");
		ISavedGameClient savedGame = PlayGamesPlatform.Instance.SavedGame;
		savedGame.ReadBinaryData(game, OnSavedGameDataRead);
	}

	public static void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
	{
		if (status == SavedGameRequestStatus.Success)
		{
			UnityEngine.Debug.Log("#@$!#!@#!@# OnSavedGameDataReadSuccess");
			string @string = Encoding.UTF8.GetString(data);
			loadedCloudProgress = XML.Deserialize<CloudProgress>(@string);
			loadedLevelCoins = loadedCloudProgress.shop.currency;
			loadedLevelNum = Utilities.LevelNumberGlobal(loadedCloudProgress.levels.Max_Active_Level, loadedCloudProgress.levels.Max_Active_Pack).ToString();
			int num = Utilities.LevelNumberGlobal(loadedCloudProgress.levels.Max_Active_Level, loadedCloudProgress.levels.Max_Active_Pack);
			int num2 = Utilities.LevelNumberGlobal(Progress.levels.Max_Active_Level, Progress.levels.Max_Active_Pack);
			UnityEngine.Debug.Log("CURENCY ====>>> " + loadedCloudProgress.shop.currency + " : " + Progress.shop.currency);
			UnityEngine.Debug.Log("LEVELS ====>>> " + num + " : " + num2);
			if (loadedCloudProgress.shop.currency >= Progress.shop.currency || num >= num2)
			{
				UnityEngine.Debug.Log("Invoking Saves Found Event");
				ShowCloudProgress();
			}
		}
		else
		{
			Progress.shop.foundProgress = false;
		}
	}

	private static void DeleteGameData(string filename)
	{
		ISavedGameClient savedGame = PlayGamesPlatform.Instance.SavedGame;
		savedGame.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, DeleteSavedGame);
	}

	public static void DeleteSavedGame(SavedGameRequestStatus status, ISavedGameMetadata game)
	{
		if (status == SavedGameRequestStatus.Success)
		{
			ISavedGameClient savedGame = PlayGamesPlatform.Instance.SavedGame;
			savedGame.Delete(game);
		}
	}
}
