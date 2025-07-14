using System.Collections.Generic;
using UnityEngine;

namespace xmlClassTemplate.GroundObject
{
	public class PositionComparer : IComparer<EasyHill2DNode>
	{
		public int Compare(EasyHill2DNode t1, EasyHill2DNode t2)
		{
			Vector3 position = t1.transform.position;
			ref float x = ref position.x;
			Vector3 position2 = t2.transform.position;
			return x.CompareTo(position2.x);
		}
	}
}
