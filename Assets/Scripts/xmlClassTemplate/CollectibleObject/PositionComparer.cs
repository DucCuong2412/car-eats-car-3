using System.Collections.Generic;

namespace xmlClassTemplate.CollectibleObject
{
	public class PositionComparer : IComparer<CollectibleObjectNode>
	{
		public int Compare(CollectibleObjectNode go1, CollectibleObjectNode go2)
		{
			return go1.position.x.CompareTo(go2.position.x);
		}
	}
}
