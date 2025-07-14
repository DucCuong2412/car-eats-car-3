using UnityEngine;

namespace xmlClassTemplate
{
	public class Collider
	{
		public string colliderType;

		public Vector2[] vertices;

		public bool isTrigger;

		public Collider()
		{
		}

		public Collider(string collType, Vector2[] verts, bool isTriger)
		{
			colliderType = collType;
			vertices = verts;
			isTrigger = isTriger;
		}

		public Collider Clone()
		{
			return (Collider)MemberwiseClone();
		}
	}
}
