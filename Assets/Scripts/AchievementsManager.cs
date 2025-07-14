using System;

public class AchievementsManager
{
	private static AchievementsManager _instance;

	public static Action OnGPConnected = delegate
	{
	};

	public static Action OnAchievementsLoaded = delegate
	{
	};

	private bool isConnected;

	private bool isAchsLoaded;

	public static AchievementsManager Instance => _instance ?? (_instance = new AchievementsManager());

	public void Connect()
	{
	}

	public void ShowAchievements()
	{
	}

	public void ExitAccount()
	{
	}

	public void OpenAchievement(string id)
	{
	}

	public void ResetAllAchievements()
	{
	}

	public void RegisterAchievement(string id)
	{
	}
}
