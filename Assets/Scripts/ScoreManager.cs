using System;

public class ScoreManager
{
	private static ScoreManager _instance;

	private bool isConnected;

	public static Action<bool> OnConnection = delegate
	{
	};

	public static ScoreManager Instance => _instance ?? (_instance = new ScoreManager());

	public static bool Init()
	{
		return Instance != null;
	}

	public void ShowLeaderBoardsUI()
	{
	}

	public void SubmitScore(string id, int score)
	{
		if (isConnected)
		{
		}
	}

	public void SubmitDistance(string id, int distance)
	{
		if (isConnected)
		{
		}
	}

	public void ResetScore(string id)
	{
	}

	public void ResetDistance(string id)
	{
	}
}
