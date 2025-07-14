using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAch : MonoBehaviour
{
	public enum Ach
	{
		CRUSHER,
		DESTROYER,
		TORNADO,
		STRONGMAN,
		VALIANT,
		UNSTOPPABLE,
		DECUMAN,
		ROADROLLER,
		BULLDOZER,
		EXTERMINATOR,
		MAGNAT,
		TYCOON,
		RICHMAN,
		EXPLORER,
		PRUDENT,
		MECHANIC1,
		MECHANIC2,
		MECHANIC3,
		NIMBLE,
		FLIPPER,
		ACE,
		SPRINTER,
		DRIVER,
		MARATHONER,
		IMPREGNABLE,
		TIMEKILLER
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

	public Button ConnectButton;

	public Button ShowButton;

	public Button ExitButton;

	public Button ResetButton;

	public Button AchButton;

	public Button LeaderButton;

	public List<Button> AchButtons = new List<Button>(30);

	public GameObject AchPanel;

	public GameObject LeaderPanel;

	public Button ShowLeaderboardsButton;

	public Button SubmitScoreButton;

	public Button SubmitDistanceButton;

	public Button ResetScoreButton;

	public Button ResetDistanceButton;

	private static List<Achievement> achievements = new List<Achievement>
	{
		new Achievement(Ach.CRUSHER, 10, "CgkIvMHK-dsbEAIQAA", "CgkIvMHK-dsbEAIQAA"),
		new Achievement(Ach.DESTROYER, 100, "CgkIvMHK-dsbEAIQAQ", "CgkIvMHK-dsbEAIQAQ"),
		new Achievement(Ach.TORNADO, 1000, "CgkIvMHK-dsbEAIQAg", "CgkIvMHK-dsbEAIQAg"),
		new Achievement(Ach.STRONGMAN, 1, "CgkIvMHK-dsbEAIQAw", "CgkIvMHK-dsbEAIQAw"),
		new Achievement(Ach.VALIANT, 1, "CgkIvMHK-dsbEAIQBA", "CgkIvMHK-dsbEAIQBA"),
		new Achievement(Ach.UNSTOPPABLE, 1, "CgkIvMHK-dsbEAIQBQ", "CgkIvMHK-dsbEAIQBQ"),
		new Achievement(Ach.DECUMAN, 1, "CgkIvMHK-dsbEAIQBg", "CgkIvMHK-dsbEAIQBg"),
		new Achievement(Ach.ROADROLLER, 10, "CgkIvMHK-dsbEAIQBw", "CgkIvMHK-dsbEAIQBw"),
		new Achievement(Ach.BULLDOZER, 100, "CgkIvMHK-dsbEAIQCA", "CgkIvMHK-dsbEAIQCA"),
		new Achievement(Ach.EXTERMINATOR, 1000, "CgkIvMHK-dsbEAIQCQ", "CgkIvMHK-dsbEAIQCQ"),
		new Achievement(Ach.MAGNAT, 100, "CgkIvMHK-dsbEAIQCg", "CgkIvMHK-dsbEAIQCg"),
		new Achievement(Ach.TYCOON, 1000, "CgkIvMHK-dsbEAIQCw", "CgkIvMHK-dsbEAIQCw"),
		new Achievement(Ach.RICHMAN, 10000, "CgkIvMHK-dsbEAIQDA", "CgkIvMHK-dsbEAIQDA"),
		new Achievement(Ach.EXPLORER, 100, "CgkIvMHK-dsbEAIQDQ", "CgkIvMHK-dsbEAIQDQ"),
		new Achievement(Ach.PRUDENT, 20, "CgkIvMHK-dsbEAIQDg", "CgkIvMHK-dsbEAIQDg"),
		new Achievement(Ach.MECHANIC1, 10, "CgkIvMHK-dsbEAIQDw", "CgkIvMHK-dsbEAIQDw"),
		new Achievement(Ach.MECHANIC2, 20, "CgkIvMHK-dsbEAIQEA", "CgkIvMHK-dsbEAIQEA"),
		new Achievement(Ach.MECHANIC3, 30, "CgkIvMHK-dsbEAIQEQ", "CgkIvMHK-dsbEAIQEQ"),
		new Achievement(Ach.NIMBLE, 5, "CgkIvMHK-dsbEAIQEg", "CgkIvMHK-dsbEAIQEg"),
		new Achievement(Ach.FLIPPER, 50, "CgkIvMHK-dsbEAIQEw", "CgkIvMHK-dsbEAIQEw"),
		new Achievement(Ach.ACE, 500, "CgkIvMHK-dsbEAIQFA", "CgkIvMHK-dsbEAIQFA"),
		new Achievement(Ach.SPRINTER, 50, "CgkIvMHK-dsbEAIQFQ", "CgkIvMHK-dsbEAIQFQ"),
		new Achievement(Ach.DRIVER, 500, "CgkIvMHK-dsbEAIQFg", "CgkIvMHK-dsbEAIQFg"),
		new Achievement(Ach.MARATHONER, 5000, "CgkIvMHK-dsbEAIQFw", "CgkIvMHK-dsbEAIQFw"),
		new Achievement(Ach.IMPREGNABLE, 20, "CgkIvMHK-dsbEAIQGA", "CgkIvMHK-dsbEAIQGA"),
		new Achievement(Ach.TIMEKILLER, 3, "CgkIvMHK-dsbEAIQGQ", "CgkIvMHK-dsbEAIQGQ")
	};

	private const string LEADERBOARD_HIGHSCORES_ID = "CgkIvMHK-dsbEAIQGg";

	private int _score = 100;

	private int _distance = 500;

	private void Start()
	{
		AttachEvents();
	}

	private void AttachEvents()
	{
		ConnectButton.onClick.AddListener(delegate
		{
			AchievementsManager.Instance.Connect();
		});
		ShowButton.onClick.AddListener(delegate
		{
			AchievementsManager.Instance.ShowAchievements();
		});
		ExitButton.onClick.AddListener(delegate
		{
			AchievementsManager.Instance.ExitAccount();
		});
		ResetButton.onClick.AddListener(delegate
		{
			AchievementsManager.Instance.ResetAllAchievements();
		});
		AchButton.onClick.AddListener(delegate
		{
			SetPanels();
		});
		LeaderButton.onClick.AddListener(delegate
		{
			SetPanels(ach: false);
		});
		ShowLeaderboardsButton.onClick.AddListener(delegate
		{
			ScoreManager.Instance.ShowLeaderBoardsUI();
		});
		SubmitScoreButton.onClick.AddListener(delegate
		{
			_score += 100;
			ScoreManager.Instance.SubmitScore("CgkIvMHK-dsbEAIQGg", _score);
		});
		ResetScoreButton.onClick.AddListener(delegate
		{
			ScoreManager.Instance.ResetScore("CgkIvMHK-dsbEAIQGg");
		});
		for (int i = 0; i < achievements.Count; i++)
		{
			string ach_id = achievements[i].Id;
			AchButtons[i].GetComponentInChildren<Text>().text = achievements[i].Name.ToString();
			AchButtons[i].onClick.AddListener(delegate
			{
				AchievementsManager.Instance.OpenAchievement(ach_id);
			});
		}
	}

	private void SetPanels(bool ach = true)
	{
		if (ach)
		{
			AchPanel.SetActive(value: true);
			LeaderPanel.SetActive(value: false);
		}
		else
		{
			AchPanel.SetActive(value: false);
			LeaderPanel.SetActive(value: true);
		}
	}

	public void Back()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
}
