using System.Collections.Generic;

namespace xmlClassTemplate.AIObject
{
	public class PositionComparer : IComparer<AIObjectNode>
	{
		public int Compare(AIObjectNode go1, AIObjectNode go2)
		{
			return go1.position.x.CompareTo(go2.position.x);
		}
	}
}
