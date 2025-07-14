using System.Collections.Generic;
using UnityEngine;
using xmlClassTemplate.DynamicObject;

public class DynamicObjectsActivator : MonoBehaviour
{
	public class DynamicNode
	{
		public GameObject gameObject;

		public tk2dSprite sprite;

		public Colliders collider;

		public DynamicObjectNode node;

		public Rigidbody2D rigidBody;

		public ObjectActor objectActor;

		public Shadow shadow;

		public DynamicNode(GameObject go, DynamicObjectNode p_node)
		{
			node = p_node;
			gameObject = go;
			sprite = go.GetComponent<tk2dSprite>();
			collider = go.GetComponent<Colliders>();
			rigidBody = go.GetComponent<Rigidbody2D>();
			objectActor = go.GetComponent<ObjectActor>();
			shadow = go.GetComponent<Shadow>();
		}
	}

	public DynamicObjectsActivatorAi DOAA;

	private Transform followTransform;

	private bool isStarted;

	private Dictionary<string, tk2dSpriteCollectionData> spriteCollectionData = new Dictionary<string, tk2dSpriteCollectionData>();

	private Stack<DynamicObjectNode> lStack = new Stack<DynamicObjectNode>();

	private Stack<DynamicObjectNode> rStack = new Stack<DynamicObjectNode>();

	private List<DynamicObjectNode> waitingList = new List<DynamicObjectNode>();

	private List<DynamicNode> activeObjects = new List<DynamicNode>();

	private Stack<DynamicNode> objectsStack = new Stack<DynamicNode>();

	private float distanceToActivate => LevelBuilder.dynamicAD;

	public void CreateStack(Transform follower, int stackLength = 50)
	{
		followTransform = follower;
		GameObject original = Resources.Load("Dynamic_Object") as GameObject;
		for (int i = 0; i < stackLength; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(original);
			gameObject.transform.parent = base.transform;
			gameObject.name = gameObject.name.Replace("(Clone)", string.Empty);
			objectsStack.Push(new DynamicNode(gameObject, null));
		}
	}

	public void Restart(List<DynamicObjectNode> list)
	{
		isStarted = false;
		rStack.Clear();
		lStack.Clear();
		waitingList.Clear();
		while (activeObjects.Count > 0)
		{
			RemoveObject(activeObjects[0]);
		}
		for (int num = list.Count - 1; num >= 0; num--)
		{
			if (!spriteCollectionData.ContainsKey(list[num].sprite.collection) && list[num].sprite.collection != string.Empty)
			{
				tk2dSpriteCollectionData tk2dSpriteCollectionData = tk2dSystem.LoadResourceByName<tk2dSpriteCollectionData>(list[num].sprite.collection);
				spriteCollectionData.Add(tk2dSpriteCollectionData.spriteCollectionName, tk2dSpriteCollectionData);
			}
			if (list[num].sprite.collection != string.Empty)
			{
				rStack.Push(list[num]);
			}
			else
			{
				UnityEngine.Debug.LogError("Some sprite in dynamics collection is not loadable!");
			}
		}
		isStarted = true;
	}

	public void Update()
	{
		if (RaceLogic.instance.car != null && followTransform == null)
		{
			followTransform = RaceLogic.instance.car.transform;
		}
		if (!isStarted || followTransform == null)
		{
			return;
		}
		if (rStack.Count > 0)
		{
			do
			{
				float x = rStack.Peek().position.x;
				Vector3 position = followTransform.position;
				if (!(x <= position.x + distanceToActivate))
				{
					break;
				}
				waitingList.Add(rStack.Pop());
			}
			while (rStack.Count != 0);
		}
		if (waitingList.Count > 0)
		{
			if (rStack.Count > 0)
			{
				float x2 = waitingList[0].position.x;
				Vector3 position2 = followTransform.position;
				if (x2 < position2.x + distanceToActivate || waitingList[waitingList.Count - 1].position.x + distanceToActivate < rStack.Peek().position.x)
				{
					for (int i = 0; i < waitingList.Count; i++)
					{
						activeObjects.Add(SpawnObject(waitingList[i]));
					}
					waitingList.Clear();
				}
			}
			else
			{
				for (int j = 0; j < waitingList.Count; j++)
				{
					activeObjects.Add(SpawnObject(waitingList[j]));
				}
				waitingList.Clear();
			}
		}
		if (lStack.Count > 0)
		{
			do
			{
				float x3 = lStack.Peek().position.x;
				Vector3 position3 = followTransform.position;
				if (!(x3 >= position3.x - distanceToActivate))
				{
					break;
				}
				activeObjects.Add(SpawnObject(lStack.Pop()));
			}
			while (lStack.Count != 0);
		}
		for (int k = 0; k < activeObjects.Count; k++)
		{
			DynamicNode dynamicNode = activeObjects[k];
			DynamicObjectNode node = dynamicNode.node;
			Vector3 position4 = dynamicNode.gameObject.transform.position;
			float x4 = position4.x;
			Vector3 position5 = dynamicNode.gameObject.transform.position;
			float y = position5.y;
			Vector3 eulerAngles = dynamicNode.gameObject.transform.eulerAngles;
			node.position = new Vector3(x4, y, eulerAngles.z);
			dynamicNode.node.health = dynamicNode.objectActor.Health;
			Vector3 position6 = dynamicNode.gameObject.transform.position;
			if (position6.x > LevelBuilder.instance.constructionsActivator.XJail - 20f && LevelBuilder.instance.constructionsActivator.XJail != 0f && dynamicNode.gameObject.activeSelf)
			{
				dynamicNode.gameObject.SetActive(value: false);
			}
			if (!dynamicNode.gameObject.activeSelf)
			{
				RemoveObject(dynamicNode);
				k--;
				continue;
			}
			Vector3 position7 = dynamicNode.gameObject.transform.position;
			float x5 = position7.x;
			Vector3 position8 = followTransform.position;
			if (x5 < position8.x - distanceToActivate)
			{
				RemoveObject(dynamicNode);
				lStack.Push(dynamicNode.node);
				k--;
				continue;
			}
			Vector3 position9 = dynamicNode.gameObject.transform.position;
			float x6 = position9.x;
			Vector3 position10 = followTransform.position;
			if (x6 > position10.x + distanceToActivate)
			{
				RemoveObject(dynamicNode);
				rStack.Push(dynamicNode.node);
				k--;
			}
		}
		if (DOAA != null)
		{
			for (int l = 0; l < activeObjects.Count; l++)
			{
				Vector3 position11 = activeObjects[l].gameObject.transform.position;
				foreach (DynamicObjectsActivatorAi.DynamicNode activeObject in DOAA.activeObjects)
				{
					if (position11 == activeObject.gameObject.transform.position)
					{
						activeObject.gameObject.SetActive(value: false);
					}
				}
			}
		}
	}

	private DynamicNode SpawnObject(DynamicObjectNode nodeInstance)
	{
		if (objectsStack.Count == 0)
		{
			UnityEngine.Debug.LogWarning("No free dynamics in stack!!! Create new one.");
			GameObject gameObject = UnityEngine.Object.Instantiate(activeObjects[0].gameObject);
			gameObject.transform.parent = activeObjects[0].gameObject.transform.parent;
			gameObject.name = gameObject.name.Replace("(Clone)", string.Empty);
			gameObject.gameObject.SetActive(value: false);
			objectsStack.Push(new DynamicNode(gameObject, null));
		}
		DynamicNode dynamicNode = objectsStack.Pop();
		dynamicNode.gameObject.transform.position = new Vector3(nodeInstance.position.x, nodeInstance.position.y);
		dynamicNode.gameObject.transform.eulerAngles = new Vector3(0f, 0f, nodeInstance.position.z);
		dynamicNode.gameObject.transform.localScale = nodeInstance.scale;
		dynamicNode.sprite.SetSprite(spriteCollectionData[nodeInstance.sprite.collection], nodeInstance.sprite.id);
		dynamicNode.sprite.GetComponent<Renderer>().sortingLayerName = nodeInstance.sprite.sortingLayer;
		dynamicNode.sprite.GetComponent<Renderer>().sortingOrder = nodeInstance.sprite.sortingOrder;
		dynamicNode.gameObject.layer = nodeInstance.layer;
		dynamicNode.sprite.scale = nodeInstance.sprite.scale;
		dynamicNode.sprite.color = nodeInstance.sprite.color;
		dynamicNode.node = nodeInstance;
		if (nodeInstance.collider != null)
		{
			switch (nodeInstance.collider.colliderType)
			{
			case "PolygonCollider2D":
				dynamicNode.collider.polygon.points = nodeInstance.collider.vertices;
				dynamicNode.collider.polygon.enabled = true;
				dynamicNode.collider.polygon.isTrigger = nodeInstance.collider.isTrigger;
				break;
			case "CircleCollider2D":
				dynamicNode.collider.circle.offset = nodeInstance.collider.vertices[0];
				dynamicNode.collider.circle.radius = nodeInstance.collider.vertices[1].x;
				dynamicNode.collider.circle.enabled = true;
				dynamicNode.collider.circle.isTrigger = nodeInstance.collider.isTrigger;
				break;
			case "EdgeCollider2D":
				dynamicNode.collider.edge.points = nodeInstance.collider.vertices;
				dynamicNode.collider.edge.enabled = true;
				dynamicNode.collider.edge.isTrigger = nodeInstance.collider.isTrigger;
				break;
			case "BoxCollider2D":
				dynamicNode.collider.box.offset = nodeInstance.collider.vertices[0];
				dynamicNode.collider.box.size = nodeInstance.collider.vertices[1];
				dynamicNode.collider.box.isTrigger = nodeInstance.collider.isTrigger;
				dynamicNode.collider.box.enabled = true;
				break;
			}
		}
		dynamicNode.rigidBody.mass = nodeInstance.rigidBodyMass;
		if (nodeInstance.rigidBodyMass <= 0.5f)
		{
			dynamicNode.rigidBody.isKinematic = true;
		}
		else
		{
			dynamicNode.rigidBody.isKinematic = false;
		}
		dynamicNode.objectActor.Health = nodeInstance.health;
		if (nodeInstance.health >= 1000)
		{
			dynamicNode.objectActor._immortal = true;
		}
		else
		{
			dynamicNode.objectActor._immortal = false;
		}
		dynamicNode.objectActor.scrapsDynamicObject = nodeInstance.scrapList;
		dynamicNode.objectActor.shadowType = nodeInstance.shadowType;
		dynamicNode.objectActor.shadowSize = nodeInstance.shadowSize;
		dynamicNode.objectActor.CollisionSound = nodeInstance.snd_collision;
		dynamicNode.objectActor.CrashSound = nodeInstance.snd_crash;
		if (nodeInstance.health <= 0)
		{
			dynamicNode.objectActor.enabled = false;
		}
		else
		{
			dynamicNode.objectActor.enabled = true;
		}
		dynamicNode.gameObject.SetActive(value: true);
		return dynamicNode;
	}

	private void RemoveObject(DynamicNode node)
	{
		node.gameObject.SetActive(value: false);
		BoxCollider2D box = node.collider.box;
		bool flag = false;
		node.collider.polygon.enabled = flag;
		flag = flag;
		node.collider.circle.enabled = flag;
		flag = flag;
		node.collider.edge.enabled = flag;
		box.enabled = flag;
		activeObjects.Remove(node);
		objectsStack.Push(node);
	}
}
