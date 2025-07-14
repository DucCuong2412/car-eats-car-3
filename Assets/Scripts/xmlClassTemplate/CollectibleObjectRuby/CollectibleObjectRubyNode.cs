using System.Collections.Generic;
using UnityEngine;

namespace xmlClassTemplate.CollectibleObjectRuby
{
	public class CollectibleObjectRubyNode
	{
		public Sprite sprite;

		public string clipName;

		public Collider collider;

		public RaceLogic.enItemRuby type;

		public int amount;

		public Vector3 position;

		public CollectibleObjectRubyNode()
		{
		}

		public CollectibleObjectRubyNode(GameObject go)
		{
			Vector3 vector = go.transform.position;
			float x = vector.x;
			Vector3 vector2 = go.transform.position;
			float y = vector2.y;
			Vector3 eulerAngles = go.transform.eulerAngles;
			position = new Vector3(x, y, eulerAngles.z);
			tk2dSprite component = go.GetComponent<tk2dSprite>();
			if (component != null)
			{
				sprite = new Sprite(component);
			}
			tk2dSpriteAnimator component2 = go.GetComponent<tk2dSpriteAnimator>();
			if ((bool)component2)
			{
				clipName = component2.DefaultClip.name;
			}
			if ((bool)go.GetComponent<CircleCollider2D>())
			{
				CircleCollider2D component3 = go.GetComponent<CircleCollider2D>();
				collider = new Collider("CircleCollider2D", new List<Vector2>
				{
					component3.offset,
					new Vector2(component3.radius, 0f)
				}.ToArray(), component3.isTrigger);
			}
			else if ((bool)go.GetComponent<EdgeCollider2D>())
			{
				EdgeCollider2D component4 = go.GetComponent<EdgeCollider2D>();
				collider = new Collider("EdgeCollider2D", component4.points, component4.isTrigger);
			}
			else if ((bool)go.GetComponent<BoxCollider2D>())
			{
				BoxCollider2D component5 = go.GetComponent<BoxCollider2D>();
				collider = new Collider("BoxCollider2D", new List<Vector2>
				{
					component5.offset,
					component5.size
				}.ToArray(), component5.isTrigger);
			}
			else if ((bool)go.GetComponent<PolygonCollider2D>())
			{
				PolygonCollider2D component6 = go.GetComponent<PolygonCollider2D>();
				collider = new Collider("PolygonCollider2D", component6.points, component6.isTrigger);
			}
			CollectibleItemRuby component7 = go.GetComponent<CollectibleItemRuby>();
			if ((bool)component7)
			{
				type = component7.itemType;
				amount = component7.amount;
			}
		}

		public CollectibleObjectRubyNode Clone()
		{
			return (CollectibleObjectRubyNode)MemberwiseClone();
		}
	}
}
