using System.Collections.Generic;

namespace xmlClassTemplate.ConstructionObject
{
	public class RotateComparer : IComparer<ConstructionObjectNode>
	{
		public int Compare(ConstructionObjectNode go1, ConstructionObjectNode go2)
		{
			return go1.rotation.x.CompareTo(go2.rotation.x);
		}
	}
}
