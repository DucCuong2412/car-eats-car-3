using System.Collections.Generic;

namespace xmlClassTemplate.DynamicObject
{
	public class PositionComparer : IComparer<DynamicObjectNode>
	{
		public int Compare(DynamicObjectNode go1, DynamicObjectNode go2)
		{
			return go1.position.x.CompareTo(go2.position.x);
		}
	}
}
