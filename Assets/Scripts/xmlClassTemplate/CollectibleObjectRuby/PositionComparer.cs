using System.Collections.Generic;

namespace xmlClassTemplate.CollectibleObjectRuby
{
	public class PositionComparer : IComparer<CollectibleObjectRubyNode>
	{
		public int Compare(CollectibleObjectRubyNode go1, CollectibleObjectRubyNode go2)
		{
			return go1.position.x.CompareTo(go2.position.x);
		}
	}
}
