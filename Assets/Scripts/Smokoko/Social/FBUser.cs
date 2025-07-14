using System;
using UnityEngine;

namespace Smokoko.Social
{
	[Serializable]
	public class FBUser : IComparable<FBUser>
	{
		public Action<FBUser> OnImageUpdated = delegate
		{
		};

		public bool loadImage;

		public string userID;

		public string username;

		[SerializeField]
		private Texture2D _imageSquare;

		[SerializeField]
		private Texture2D _imageLarge;

		public int score;

		public bool userIsPlayer;

		public static long Tscore;

		public Texture2D image
		{
			get
			{
				if (_imageLarge == null)
				{
					return (!userIsPlayer) ? FBLeaderboardSettings.Instance.DefaultUserImage : FBLeaderboardSettings.Instance.DefaultPlayerImage;
				}
				return _imageLarge;
			}
			set
			{
				loadImage = true;
				_imageLarge = value;
				OnImageUpdated(this);
			}
		}

		public Texture2D imageSquare
		{
			get
			{
				if (_imageSquare == null)
				{
					return (!userIsPlayer) ? FBLeaderboardSettings.Instance.DefaultUserImage : FBLeaderboardSettings.Instance.DefaultPlayerImage;
				}
				return _imageSquare;
			}
			set
			{
				_imageSquare = value;
				OnImageUpdated(this);
			}
		}

		public int CompareTo(FBUser user2)
		{
			return FBLeaderboard.ScoreDistanse(user2.score).CompareTo(FBLeaderboard.ScoreDistanse(score));
		}

		public void SetScore(int newScore)
		{
			if (userIsPlayer)
			{
				Tscore = newScore;
				score = newScore;
			}
		}
	}
}
