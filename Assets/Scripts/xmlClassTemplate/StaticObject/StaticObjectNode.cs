using UnityEngine;

namespace xmlClassTemplate.StaticObject
{
	public class StaticObjectNode
	{
		public Vector3 position = Vector3.zero;

		public ObjectType.Type type;

		public Sprite sprite;

		public Collider collider;

		public Vector3 scale = Vector3.one;

		public StaticObjectNode Clone()
		{
			return (StaticObjectNode)MemberwiseClone();
		}
	}
}
