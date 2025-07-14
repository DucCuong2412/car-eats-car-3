using System.Collections.Generic;

namespace xmlClassTemplate.ConstructionObject
{
	public class PositionComparer : IComparer<ConstructionObjectNode>
	{
		public int Compare(ConstructionObjectNode go1, ConstructionObjectNode go2)
		{
			return go1.position.x.CompareTo(go2.position.x);
		}
	}
}
