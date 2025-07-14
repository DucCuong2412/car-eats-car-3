using System;
using System.Collections.Generic;
using UnityEngine;

namespace Smokoko.Social
{
	public static class FBLeaderboard
	{
		public static Action<bool> OnUserLoggedIn = delegate
		{
		};

		public static Action OnUserLoginError = delegate
		{
		};

		public static Action<List<FBUser>> OnScoresLoaded = delegate
		{
		};

		public static Action<List<FBRequest>> OnRequestsLoaded = delegate
		{
		};

		public static Action<List<string>> OnRequestComplete = delegate
		{
		};

		public static List<FBUser> users = new List<FBUser>();

		private static FBUser _currentUser;

		private static bool initCalled = false;

		public static Action<bool, string> OnFriendInvitedAct = delegate
		{
		};

		private static string idInvite = string.Empty;

		public static FBUser CurrentUser
		{
			get
			{
				return _currentUser;
			}
			set
			{
				_currentUser = value;
			}
		}

		public static bool IsFBInited => false;

		public static bool IsUserLoggedIn => false;

		public static void Init()
		{
			if (!initCalled)
			{
				initCalled = true;
			}
		}

		public static void ShareMessage(string caption, string message, Texture2D texture)
		{
		}

		public static long FBScore(int realLastLevel)
		{
			return realLastLevel;
		}

		public static long FBScore(long level, long distanse, float percent)
		{
			UnityEngine.Debug.Log(level + "  " + distanse + "  " + percent);
			return 1000000000 * level + (long)(percent * 100f) * 1000000 + distanse;
		}

		public static long ScoreLevel(long score)
		{
			if (score < 1000000000)
			{
				return 1L;
			}
			return score / 1000000000;
		}

		public static float ScorePercent(long score)
		{
			if (score < 1000000000)
			{
				return 0f;
			}
			long num = score % 1000000000;
			return (float)(num / 1000000) * 0.01f;
		}

		public static long ScoreDistanse(long score)
		{
			if (score < 1000000000)
			{
				return 0L;
			}
			long num = score % 1000000000;
			long num2 = num / 1000000;
			return num % 1000000;
		}

		public static void PostToFacebook(string text)
		{
		}

		public static void LogIn()
		{
		}

		public static void LogOut()
		{
		}

		public static void LoadAppScores()
		{
		}

		public static void AppRequest()
		{
		}

		public static void InviteFriend(string id)
		{
		}

		public static void LoadAppRequests()
		{
		}

		public static void AskGift(string title, string message, string objectId, string data = "", string[] to = null)
		{
			for (int i = 0; i < to.Length; i++)
			{
				UnityEngine.Debug.Log(to[i]);
			}
		}

		public static void SendGift(string title, string message, string objectId, string data = "", string[] to = null)
		{
		}
	}
}
