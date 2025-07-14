using System.Collections.Generic;
using UnityEngine;

public static class GameCenter
{
	public enum Ach
	{
		Hungry,
		Biting,
		Glutton,
		Megakill,
		Ultrakill,
		Monsterkill,
		Strong,
		Muscle,
		Iinvincible,
		GoodSoul,
		Hero,
		BraveHeart,
		Demolisher,
		Engineer,
		CarCollector,
		Deputy,
		Sheriff,
		Marshal,
		SOCIAL,
		Judge
	}

	public class Achievement
	{
		public Ach Name;

		public int Value;

		private string Id_android;

		private string Id_ios;

		public string Id => Id_android;

		public Achievement(Ach Name, int Value, string Id_android, string Id_ios)
		{
			this.Name = Name;
			this.Value = Value;
			this.Id_android = Id_android;
			this.Id_ios = Id_ios;
		}
	}

	private static List<Achievement> achievements = new List<Achievement>
	{
		new Achievement(Ach.Hungry, 100, "CgkIrLuEnrkTEAIQBA", "CgkIrLuEnrkTEAIQBA"),
		new Achievement(Ach.Biting, 500, "CgkIrLuEnrkTEAIQBQ", "CgkIrLuEnrkTEAIQBQ"),
		new Achievement(Ach.Glutton, 1000, "CgkIrLuEnrkTEAIQBg", "CgkIrLuEnrkTEAIQBg"),
		new Achievement(Ach.Megakill, 50, "CgkIrLuEnrkTEAIQBw", "CgkIrLuEnrkTEAIQBw"),
		new Achievement(Ach.Ultrakill, 300, "CgkIrLuEnrkTEAIQCA", "CgkIrLuEnrkTEAIQCA"),
		new Achievement(Ach.Monsterkill, 700, "CgkIrLuEnrkTEAIQCQ", "CgkIrLuEnrkTEAIQCQ"),
		new Achievement(Ach.Strong, 1, "CgkIrLuEnrkTEAIQCg", "CgkIrLuEnrkTEAIQCg"),
		new Achievement(Ach.Muscle, 1, "CgkIrLuEnrkTEAIQCw", "CgkIrLuEnrkTEAIQCw"),
		new Achievement(Ach.Iinvincible, 1, "CgkIrLuEnrkTEAIQDA", "CgkIrLuEnrkTEAIQDA"),
		new Achievement(Ach.GoodSoul, 5, "CgkIrLuEnrkTEAIQDQ", "CgkIrLuEnrkTEAIQDQ"),
		new Achievement(Ach.Hero, 10, "CgkIrLuEnrkTEAIQDg", "CgkIrLuEnrkTEAIQDg"),
		new Achievement(Ach.BraveHeart, 15, "CgkIrLuEnrkTEAIQDw", "CgkIrLuEnrkTEAIQDw"),
		new Achievement(Ach.Demolisher, 1, "CgkIrLuEnrkTEAIQEA", "CgkIrLuEnrkTEAIQEA"),
		new Achievement(Ach.Engineer, 1, "CgkIrLuEnrkTEAIQEQ", "CgkIrLuEnrkTEAIQEQ"),
		new Achievement(Ach.CarCollector, 1, "CgkIrLuEnrkTEAIQEg", "CgkIrLuEnrkTEAIQEg"),
		new Achievement(Ach.Deputy, 36, "CgkIrLuEnrkTEAIQEw", "CgkIrLuEnrkTEAIQEw"),
		new Achievement(Ach.Sheriff, 72, "CgkIrLuEnrkTEAIQFA", "CgkIrLuEnrkTEAIQFA"),
		new Achievement(Ach.Marshal, 108, "CgkIrLuEnrkTEAIQFQ", "CgkIrLuEnrkTEAIQFQ"),
		new Achievement(Ach.SOCIAL, 1, "CgkIrLuEnrkTEAIQFg", "CgkIrLuEnrkTEAIQFg"),
		new Achievement(Ach.Judge, 1, "CgkIrLuEnrkTEAIQFw", "CgkIrLuEnrkTEAIQFw")
	};

	private const string LEADERBOARD_HIGHSCORES_ID_RUBY = "CgkIrLuEnrkTEAIQAw";

	private const string LEADERBOARD_HIGHSCORES_ID_POLICE = "CgkIrLuEnrkTEAIQAg";

	private const string LEADERBOARD_HIGHSCORES_ID_CIVIL = "CgkIrLuEnrkTEAIQAQ";

	private static bool isInited = false;

	public static void Init()
	{
		isInited = true;
		ScoreManager.Init();
		AchievementsManager.Instance.Connect();
	}

	public static void ShowAchievements()
	{
		if (!isInited)
		{
			Init();
		}
		Social.ShowAchievementsUI();
	}

	public static void ShowLeaderBoards()
	{
		if (!isInited)
		{
			Init();
		}
		Social.ShowLeaderboardUI();
	}

	public static void SubmitScore(int scoreRuby, int scoreCivil, int scorePolice)
	{
		Social.ReportScore(scoreRuby, "CgkIrLuEnrkTEAIQAw", delegate
		{
		});
		Social.ReportScore(scoreCivil, "CgkIrLuEnrkTEAIQAQ", delegate
		{
		});
		Social.ReportScore(scorePolice, "CgkIrLuEnrkTEAIQAg", delegate
		{
		});
	}

	public static void ResetAllAchievements()
	{
		AchievementsManager.Instance.ResetAllAchievements();
	}

	public static void ResetAllLeaderboards()
	{
		Social.ReportScore(0L, "CgkIrLuEnrkTEAIQAw", delegate
		{
		});
		Social.ReportScore(0L, "CgkIrLuEnrkTEAIQAQ", delegate
		{
		});
		Social.ReportScore(0L, "CgkIrLuEnrkTEAIQAg", delegate
		{
		});
	}

	private static void CheckAchievement(Ach ach, int Value)
	{
		Achievement achievement = achievements.Find((Achievement a) => a.Name == ach);
		if (achievement != null && Value >= achievement.Value)
		{
			Social.ReportProgress(achievement.Id, 100.0, delegate
			{
			});
		}
	}

	public static void OnBossDestroy(int bossNumber)
	{
		if (bossNumber > 0 && bossNumber <= Progress.achievements.bossesDestroyed.Length)
		{
			Progress.achievements.bossesDestroyed[bossNumber - 1]++;
		}
		CheckAchievement(Ach.Strong, Progress.achievements.bossesDestroyed[0]);
		CheckAchievement(Ach.Muscle, Progress.achievements.bossesDestroyed[1]);
		CheckAchievement(Ach.Iinvincible, Progress.achievements.bossesDestroyed[2]);
	}

	public static void OnDestroyEnemy()
	{
		Progress.achievements.enemiesDestroyed++;
		CheckAchievement(Ach.Megakill, Progress.achievements.enemiesDestroyed);
		CheckAchievement(Ach.Ultrakill, Progress.achievements.enemiesDestroyed);
		CheckAchievement(Ach.Monsterkill, Progress.achievements.enemiesDestroyed);
	}

	public static void OnDestroyCivil()
	{
		Progress.achievements.CivilHighscore++;
		CheckAchievement(Ach.Hungry, Progress.achievements.CivilHighscore);
		CheckAchievement(Ach.Biting, Progress.achievements.CivilHighscore);
		CheckAchievement(Ach.Glutton, Progress.achievements.CivilHighscore);
	}

	public static void OnSaveFriend()
	{
		Progress.achievements.SaveFriends++;
		CheckAchievement(Ach.GoodSoul, Progress.achievements.SaveFriends);
		CheckAchievement(Ach.Hero, Progress.achievements.SaveFriends);
		CheckAchievement(Ach.BraveHeart, Progress.achievements.SaveFriends);
	}

	public static void OnPurchaseBombs()
	{
		if (Progress.shop.Cars[0].bomb_0_bounght && Progress.shop.Cars[0].bomb_1_bounght && Progress.shop.Cars[0].bomb_2_bounght && Progress.shop.Cars[0].bomb_3_bounght && Progress.shop.Cars[0].bomb_4_bounght)
		{
			CheckAchievement(Ach.Demolisher, 1);
		}
		else
		{
			CheckAchievement(Ach.Demolisher, 0);
		}
	}

	public static void OnPurchaseGadgets()
	{
		if (Progress.shop.Cars[0].Gadget_EMP_bounght && Progress.shop.Cars[0].Gadget_LEDOLUCH_bounght && Progress.shop.Cars[0].Gadget_Magnet_bounght && Progress.shop.Cars[0].Gadget_MISSLLE_bounght && Progress.shop.Cars[0].Gadget_RECHARGER_bounght)
		{
			CheckAchievement(Ach.Engineer, 1);
		}
		else
		{
			CheckAchievement(Ach.Engineer, 0);
		}
	}

	public static void OnPurchaseCars()
	{
		if (Progress.shop.Cars[0].equipped && Progress.shop.Cars[1].equipped && Progress.shop.Cars[2].equipped)
		{
			CheckAchievement(Ach.CarCollector, 1);
		}
		else
		{
			CheckAchievement(Ach.CarCollector, 0);
		}
	}

	public static void OnBadgesCollect(int value)
	{
		CheckAchievement(Ach.Deputy, value);
		CheckAchievement(Ach.Sheriff, value);
		CheckAchievement(Ach.Marshal, value);
	}

	public static void OnConnectFB(int value)
	{
		CheckAchievement(Ach.SOCIAL, value);
	}

	public static void OnRateGame(int value)
	{
		CheckAchievement(Ach.Judge, value);
	}

	public static void CheckAllAchievements()
	{
		if (Progress.shop.Cars[0].equipped && Progress.shop.Cars[1].equipped && Progress.shop.Cars[2].equipped)
		{
			CheckAchievement(Ach.CarCollector, 1);
		}
		else
		{
			CheckAchievement(Ach.CarCollector, 0);
		}
		if (Progress.shop.Cars[0].Gadget_EMP_bounght && Progress.shop.Cars[0].Gadget_LEDOLUCH_bounght && Progress.shop.Cars[0].Gadget_Magnet_bounght && Progress.shop.Cars[0].Gadget_MISSLLE_bounght && Progress.shop.Cars[0].Gadget_RECHARGER_bounght)
		{
			CheckAchievement(Ach.Engineer, 1);
		}
		else
		{
			CheckAchievement(Ach.Engineer, 0);
		}
		if (Progress.shop.Cars[0].bomb_0_bounght && Progress.shop.Cars[0].bomb_1_bounght && Progress.shop.Cars[0].bomb_2_bounght && Progress.shop.Cars[0].bomb_3_bounght && Progress.shop.Cars[0].bomb_4_bounght)
		{
			CheckAchievement(Ach.Demolisher, 1);
		}
		else
		{
			CheckAchievement(Ach.Demolisher, 0);
		}
		CheckAchievement(Ach.GoodSoul, Progress.achievements.SaveFriends);
		CheckAchievement(Ach.Hero, Progress.achievements.SaveFriends);
		CheckAchievement(Ach.BraveHeart, Progress.achievements.SaveFriends);
		CheckAchievement(Ach.Hungry, Progress.achievements.CivilHighscore);
		CheckAchievement(Ach.Biting, Progress.achievements.CivilHighscore);
		CheckAchievement(Ach.Glutton, Progress.achievements.CivilHighscore);
		CheckAchievement(Ach.Megakill, Progress.achievements.enemiesDestroyed);
		CheckAchievement(Ach.Ultrakill, Progress.achievements.enemiesDestroyed);
		CheckAchievement(Ach.Monsterkill, Progress.achievements.enemiesDestroyed);
		CheckAchievement(Ach.Strong, Progress.achievements.bossesDestroyed[0]);
		CheckAchievement(Ach.Muscle, Progress.achievements.bossesDestroyed[1]);
		CheckAchievement(Ach.Iinvincible, Progress.achievements.bossesDestroyed[2]);
	}
}
