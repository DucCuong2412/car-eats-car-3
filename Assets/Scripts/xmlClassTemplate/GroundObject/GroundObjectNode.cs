using UnityEngine;

namespace xmlClassTemplate.GroundObject
{
	public class GroundObjectNode
	{
		public Vector2 P2;

		public Vector2 P3;

		public Vector2 P4;

		public int elementNumber;

		public float segmentHeight;

		public Vector2 position;

		public Quaternion Rotation;

		public int layer;

		public GroundObjectNode Clone()
		{
			GroundObjectNode groundObjectNode = new GroundObjectNode();
			groundObjectNode.P2 = P2;
			groundObjectNode.P3 = P3;
			groundObjectNode.P4 = P4;
			groundObjectNode.elementNumber = elementNumber;
			groundObjectNode.segmentHeight = segmentHeight;
			groundObjectNode.position = position;
			groundObjectNode.Rotation = Rotation;
			groundObjectNode.layer = layer;
			return groundObjectNode;
		}
	}
}
