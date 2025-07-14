using Smokoko.Social;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FBLeaderboardExample : MonoBehaviour
{
	private List<FBUser> allScores = new List<FBUser>();

	private void OnEnable()
	{
		FBLeaderboard.OnScoresLoaded = (Action<List<FBUser>>)Delegate.Combine(FBLeaderboard.OnScoresLoaded, new Action<List<FBUser>>(OnUsersLoaded));
		FBLeaderboard.OnRequestsLoaded = (Action<List<FBRequest>>)Delegate.Combine(FBLeaderboard.OnRequestsLoaded, new Action<List<FBRequest>>(OnRequestsLoaded));
	}

	private void OnDisable()
	{
		FBLeaderboard.OnScoresLoaded = (Action<List<FBUser>>)Delegate.Remove(FBLeaderboard.OnScoresLoaded, new Action<List<FBUser>>(OnUsersLoaded));
		FBLeaderboard.OnRequestsLoaded = (Action<List<FBRequest>>)Delegate.Remove(FBLeaderboard.OnRequestsLoaded, new Action<List<FBRequest>>(OnRequestsLoaded));
	}

	public void OnRequestsLoaded(List<FBRequest> list)
	{
		foreach (FBRequest item in list)
		{
		}
	}

	private void OnGUI()
	{
		float num = (float)Screen.width / 8f;
		float num2 = (float)Screen.height / 8f;
		if (GUI.Button(new Rect(0f, (float)Screen.height - num2, num, num2), "<-- Back"))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}
		if (!FBLeaderboard.IsFBInited)
		{
			if (GUI.Button(new Rect(0f, 0f, num, num2), "Init first"))
			{
				FBLeaderboard.Init();
			}
			return;
		}
		if (!FBLeaderboard.IsUserLoggedIn)
		{
			if (GUI.Button(new Rect(0f, 0f, num, num2), "Login first"))
			{
				FBLeaderboard.OnUserLoggedIn = (Action<bool>)Delegate.Combine(FBLeaderboard.OnUserLoggedIn, new Action<bool>(OnUserLoggedIn));
				FBLeaderboard.LogIn();
			}
			return;
		}
		List<string> usersIDs = new List<string>();
		allScores.ForEach(delegate(FBUser s)
		{
			usersIDs.Add(s.userID);
		});
		if (GUI.Button(new Rect(4f * num, 0f, num, num2), "Load Requests"))
		{
			FBLeaderboard.LoadAppRequests();
		}
		if (GUI.Button(new Rect(5f * num, 0f, num, num2), "Ask Gift"))
		{
			FBLeaderboard.AskGift("Ask title", "ask message", "882178618526289", string.Empty, usersIDs.ToArray());
		}
		if (GUI.Button(new Rect(6f * num, 0f, num, num2), "Send Gift"))
		{
			FBLeaderboard.SendGift("Send title", "send message", "882178618526289", string.Empty, usersIDs.ToArray());
		}
		if (GUI.Button(new Rect((float)Screen.width - num, 0f, num, num2), "Log Out"))
		{
			FBLeaderboard.LogOut();
		}
		if (GUI.Button(new Rect(0f, 0f, num, num2), "Load Scores"))
		{
			FBLeaderboard.LoadAppScores();
		}
		if (FBLeaderboard.CurrentUser == null || GUI.Button(new Rect(2f * num, 0f, num, num2), "Set Score"))
		{
		}
		for (int i = 0; i < allScores.Count; i++)
		{
			if (allScores[i].image != null)
			{
				GUI.DrawTexture(new Rect(num, num2 * (float)(i + 1), num2, num2), allScores[i].image);
			}
			GUI.Label(new Rect(3f * num, num2 * (float)(i + 1), 2f * num, num2), allScores[i].score.ToString());
			GUI.Label(new Rect(5f * num, num2 * (float)(i + 1), 2f * num, num2), allScores[i].username);
			if (FBLeaderboard.CurrentUser == allScores[i])
			{
				GUI.Label(new Rect(0f, num2 * (float)(i + 1), 2f * num, num2), "Me:");
			}
		}
	}

	private void OnUserLoggedIn(bool b)
	{
		FBLeaderboard.OnUserLoggedIn = (Action<bool>)Delegate.Remove(FBLeaderboard.OnUserLoggedIn, new Action<bool>(OnUserLoggedIn));
	}

	private void OnUsersLoaded(List<FBUser> list)
	{
		lock (allScores)
		{
			allScores.Clear();
			allScores.AddRange(list);
		}
	}
}
