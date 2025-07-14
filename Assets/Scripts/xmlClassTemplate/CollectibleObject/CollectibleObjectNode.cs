using UnityEngine;

namespace xmlClassTemplate.CollectibleObject
{
	public class CollectibleObjectNode
	{
		public Sprite sprite;

		public string clipName;

		public RaceLogic.enItem type;

		public int amount;

		public Vector3 position;

		public CollectibleObjectNode()
		{
		}

		public CollectibleObjectNode(GameObject go)
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
			CollectibleItem component2 = go.GetComponent<CollectibleItem>();
			if (component2 != null)
			{
				type = component2.itemType;
				amount = component2.amount;
			}
			else
			{
				component2 = go.AddComponent<CollectibleItem>();
				type = component2.itemType;
				amount = component2.amount;
			}
		}

		public CollectibleObjectNode Clone()
		{
			return (CollectibleObjectNode)MemberwiseClone();
		}
	}
}
