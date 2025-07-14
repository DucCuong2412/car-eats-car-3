using UnityEngine;

namespace xmlClassTemplate
{
	public class Sprite
	{
		public string collection;

		public int id;

		public string sortingLayer;

		public int sortingOrder;

		public Vector2 scale = Vector2.one;

		public Color color = Color.white;

		public Sprite()
		{
		}

		public Sprite(tk2dSprite sp)
		{
			id = sp.spriteId;
			color = sp.color;
			collection = sp.Collection.spriteCollectionName;
			sortingLayer = sp.GetComponent<Renderer>().sortingLayerName;
			sortingOrder = sp.GetComponent<Renderer>().sortingOrder;
			scale = sp.scale;
		}

		public Sprite Clone()
		{
			return (Sprite)MemberwiseClone();
		}
	}
}
