using System;

namespace Smokoko.Social
{
	public class FBRequest
	{
		public enum FBRequestActionType
		{
			Send,
			AskFor,
			Turn,
			Undefined
		}

		public enum FBRequestState
		{
			Pending,
			Deleted
		}

		public string Id;

		public string ApplicationId;

		public string Message = string.Empty;

		public FBRequestActionType ActionType = FBRequestActionType.Undefined;

		public FBRequestState State;

		public string FromId;

		public string FromName;

		public DateTime CreatedTime;

		public string CreatedTimeString;

		public string Data = string.Empty;

		public Action<FBRequest> OnDeleteRequestFinished = delegate
		{
		};

		public void Delete()
		{
		}
	}
}
