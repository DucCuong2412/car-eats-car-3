using UnityEngine;

namespace xmlClassTemplate.AIObjectCivic
{
	public class AIObjectNodeCivic
	{
		public bool isAhead;

		public int type;

		public int coll;

		public bool IsCivic;

		public int Location;

		public bool IsConvoi;

		public bool InitAiOnStart;

		public int Ram;

		public int guns;

		public Vector3 position;

		public string scrap1;

		public string scrap2;

		public string scrap3;

		public string scrap4;

		public string scrap5;

		public string scrap1y;

		public string scrap2y;

		public string scrap3y;

		public string scrap4y;

		public string scrap5y;

		public Vector3 RGB;

		public int CollRubyForYkys;

		public int CollRubyForVzruv;

		public AIObjectNodeCivic Clone()
		{
			return (AIObjectNodeCivic)MemberwiseClone();
		}
	}
}
