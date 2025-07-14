using System.Collections.Generic;

namespace xmlClassTemplate.AIObjectCivic
{
	public class PositionComparer : IComparer<AIObjectNodeCivic>
	{
		public int Compare(AIObjectNodeCivic go1, AIObjectNodeCivic go2)
		{
			return go1.position.x.CompareTo(go2.position.x);
		}
	}
}
