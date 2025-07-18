//using GooglePlayGames.OurUtils;
using System;

namespace GooglePlayGames.BasicApi.SavedGame
{
	public struct SavedGameMetadataUpdate
	{
		public struct Builder
		{
			internal bool mDescriptionUpdated;

			internal string mNewDescription;

			internal bool mCoverImageUpdated;

			internal byte[] mNewPngCoverImage;

			internal TimeSpan? mNewPlayedTime;

			public Builder WithUpdatedDescription(string description)
			{
				//mNewDescription = Misc.CheckNotNull(description);
				mDescriptionUpdated = true;
				return this;
			}

			public Builder WithUpdatedPngCoverImage(byte[] newPngCoverImage)
			{
				mCoverImageUpdated = true;
				mNewPngCoverImage = newPngCoverImage;
				return this;
			}

			public Builder WithUpdatedPlayedTime(TimeSpan newPlayedTime)
			{
				if (newPlayedTime.TotalMilliseconds > 1.8446744073709552E+19)
				{
					throw new InvalidOperationException("Timespans longer than ulong.MaxValue milliseconds are not allowed");
				}
				mNewPlayedTime = newPlayedTime;
				return this;
			}

			public SavedGameMetadataUpdate Build()
			{
				return new SavedGameMetadataUpdate(this);
			}
		}

		private readonly bool mDescriptionUpdated;

		private readonly string mNewDescription;

		private readonly bool mCoverImageUpdated;

		private readonly byte[] mNewPngCoverImage;

		private readonly TimeSpan? mNewPlayedTime;

		public bool IsDescriptionUpdated => mDescriptionUpdated;

		public string UpdatedDescription => mNewDescription;

		public bool IsCoverImageUpdated => mCoverImageUpdated;

		public byte[] UpdatedPngCoverImage => mNewPngCoverImage;

		public bool IsPlayedTimeUpdated => mNewPlayedTime.HasValue;

		public TimeSpan? UpdatedPlayedTime => mNewPlayedTime;

		private SavedGameMetadataUpdate(Builder builder)
		{
			mDescriptionUpdated = builder.mDescriptionUpdated;
			mNewDescription = builder.mNewDescription;
			mCoverImageUpdated = builder.mCoverImageUpdated;
			mNewPngCoverImage = builder.mNewPngCoverImage;
			mNewPlayedTime = builder.mNewPlayedTime;
		}
	}
}
