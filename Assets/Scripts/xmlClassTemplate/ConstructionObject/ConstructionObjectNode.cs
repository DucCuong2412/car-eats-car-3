using UnityEngine;

namespace xmlClassTemplate.ConstructionObject
{
	public class ConstructionObjectNode
	{
		public string name;

		public Vector3 position;

		public Quaternion rotation;

		public string path;

		public ConstructionObjectNode Clone()
		{
			return (ConstructionObjectNode)MemberwiseClone();
		}
	}
}
