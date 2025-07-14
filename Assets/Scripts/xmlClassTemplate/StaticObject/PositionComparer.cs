using System.Collections.Generic;
using UnityEngine;

namespace xmlClassTemplate.StaticObject
{
	public class PositionComparer : IComparer<tk2dSprite>
	{
		public int Compare(tk2dSprite go1, tk2dSprite go2)
		{
			Vector3 position = go1.transform.position;
			ref float x = ref position.x;
			Vector3 position2 = go2.transform.position;
			return x.CompareTo(position2.x);
		}
	}
}
