using System.Collections.Generic;
using UnityEngine;

namespace xmlClassTemplate.DynamicObject
{
	public class DynamicObjectNode
	{
		public int health;

		public Vector3 position = Vector3.zero;

		public Vector3 scale = Vector3.one;

		public int layer;

		public Sprite sprite;

		public Collider collider;

		public float rigidBodyMass;

		public Shadow.ShadowType shadowType;

		public float shadowSize;

		public List<ObjectActor.ScrapDynamicObject> scrapList = new List<ObjectActor.ScrapDynamicObject>();

		public ObjectActor.enCollSound snd_collision;

		public ObjectActor.enCrashSound snd_crash;

		public DynamicObjectNode()
		{
		}

		public DynamicObjectNode(GameObject go)
		{
			Vector3 vector = go.transform.position;
			float x = vector.x;
			Vector3 vector2 = go.transform.position;
			float y = vector2.y;
			Vector3 eulerAngles = go.transform.eulerAngles;
			position = new Vector3(x, y, eulerAngles.z);
			scale = go.transform.localScale;
			layer = go.layer;
			ObjectActor component = go.GetComponent<ObjectActor>();
			if (component != null)
			{
				health = component.Health;
				scrapList = component.scrapsDynamicObject;
				shadowType = component.shadowType;
				shadowSize = component.shadowSize;
				snd_collision = component.CollisionSound;
				snd_crash = component.CrashSound;
			}
			if ((bool)go.GetComponent<CircleCollider2D>())
			{
				CircleCollider2D component2 = go.GetComponent<CircleCollider2D>();
				collider = new Collider("CircleCollider2D", new List<Vector2>
				{
					component2.offset,
					new Vector2(component2.radius, 0f)
				}.ToArray(), component2.isTrigger);
			}
			else if ((bool)go.GetComponent<EdgeCollider2D>())
			{
				EdgeCollider2D component3 = go.GetComponent<EdgeCollider2D>();
				collider = new Collider("EdgeCollider2D", component3.points, component3.isTrigger);
			}
			else if ((bool)go.GetComponent<BoxCollider2D>())
			{
				BoxCollider2D component4 = go.GetComponent<BoxCollider2D>();
				collider = new Collider("BoxCollider2D", new List<Vector2>
				{
					component4.offset,
					component4.size
				}.ToArray(), component4.isTrigger);
			}
			else if ((bool)go.GetComponent<PolygonCollider2D>())
			{
				PolygonCollider2D component5 = go.GetComponent<PolygonCollider2D>();
				collider = new Collider("PolygonCollider2D", component5.points, component5.isTrigger);
			}
			tk2dSprite component6 = go.GetComponent<tk2dSprite>();
			if (component6 != null)
			{
				sprite = new Sprite(component6);
			}
			if ((bool)go.GetComponent<Rigidbody2D>())
			{
				rigidBodyMass = go.GetComponent<Rigidbody2D>().mass;
			}
		}

		public DynamicObjectNode Clone()
		{
			DynamicObjectNode dynamicObjectNode = new DynamicObjectNode();
			dynamicObjectNode.health = health;
			dynamicObjectNode.position = position;
			dynamicObjectNode.scale = scale;
			dynamicObjectNode.layer = layer;
			dynamicObjectNode.sprite = sprite;
			dynamicObjectNode.collider = collider;
			dynamicObjectNode.rigidBodyMass = rigidBodyMass;
			dynamicObjectNode.shadowType = shadowType;
			dynamicObjectNode.shadowSize = shadowSize;
			dynamicObjectNode.snd_collision = snd_collision;
			dynamicObjectNode.snd_crash = snd_crash;
			for (int i = 0; i < scrapList.Count; i++)
			{
				dynamicObjectNode.scrapList.Add(scrapList[i].Clone());
			}
			return dynamicObjectNode;
		}
	}
}
