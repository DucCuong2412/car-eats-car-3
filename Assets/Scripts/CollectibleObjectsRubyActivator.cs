using System;
using System.Collections.Generic;
using UnityEngine;
using xmlClassTemplate.CollectibleObjectRuby;

public class CollectibleObjectsRubyActivator : MonoBehaviour
{
	[Serializable]
	private class CollectibleRubyNode
	{
		public GameObject gameObject;

		public tk2dSprite sprite;

		public Colliders collider;

		public CollectibleObjectRubyNode node;

		public CollectibleItemRuby script;

		public tk2dSpriteAnimator animator;

		public CollectibleRubyNode(GameObject go, CollectibleObjectRubyNode p_node)
		{
			node = p_node;
			gameObject = go;
			sprite = go.GetComponent<tk2dSprite>();
			collider = go.GetComponent<Colliders>();
			script = go.GetComponent<CollectibleItemRuby>();
			animator = go.GetComponent<tk2dSpriteAnimator>();
		}
	}

	private Transform followTransform;

	private Stack<CollectibleObjectRubyNode> lStack = new Stack<CollectibleObjectRubyNode>();

	private Stack<CollectibleObjectRubyNode> rStack = new Stack<CollectibleObjectRubyNode>();

	private List<CollectibleRubyNode> activeColls = new List<CollectibleRubyNode>();

	private bool isStarted;

	private Dictionary<string, tk2dSpriteCollectionData> spriteCollectionData = new Dictionary<string, tk2dSpriteCollectionData>();

	[SerializeField]
	private Stack<CollectibleRubyNode> collStack = new Stack<CollectibleRubyNode>();

	public void CreateStack(Transform follower, int stackLength = 200)
	{
		followTransform = follower;
		GameObject original = Resources.Load("Collectible_ObjectRUBY") as GameObject;
		for (int i = 0; i < stackLength; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(original);
			gameObject.transform.parent = base.transform;
			gameObject.name = gameObject.name.Replace("(Clone)", string.Empty);
			collStack.Push(new CollectibleRubyNode(gameObject, null));
		}
	}

	public void Restart(List<CollectibleObjectRubyNode> list)
	{
		isStarted = false;
		rStack.Clear();
		lStack.Clear();
		while (activeColls.Count > 0)
		{
			RemoveObject(activeColls[0]);
		}
		for (int num = list.Count - 1; num >= 0; num--)
		{
			if (!spriteCollectionData.ContainsKey(list[num].sprite.collection) && list[num].sprite.collection != string.Empty)
			{
				tk2dSpriteCollectionData tk2dSpriteCollectionData = tk2dSystem.LoadResourceByName<tk2dSpriteCollectionData>(list[num].sprite.collection);
				spriteCollectionData.Add(tk2dSpriteCollectionData.assetName, tk2dSpriteCollectionData);
			}
			if (list[num].sprite.collection != string.Empty)
			{
				rStack.Push(list[num]);
			}
		}
		isStarted = true;
	}

	private void Update()
	{
		if (RaceLogic.instance.car != null && followTransform == null)
		{
			followTransform = RaceLogic.instance.car.transform;
		}
		if (!isStarted || followTransform == null)
		{
			return;
		}
		float collectibleAD = LevelBuilder.collectibleAD;
		if (rStack.Count > 0)
		{
			do
			{
				float x = rStack.Peek().position.x;
				Vector3 position = followTransform.position;
				if (!(x <= position.x + collectibleAD))
				{
					break;
				}
				activeColls.Add(SpawnObject(rStack.Pop()));
			}
			while (rStack.Count != 0);
		}
		if (lStack.Count > 0)
		{
			do
			{
				float x2 = lStack.Peek().position.x;
				Vector3 position2 = followTransform.position;
				if (!(x2 >= position2.x - collectibleAD))
				{
					break;
				}
				activeColls.Add(SpawnObject(lStack.Pop()));
			}
			while (lStack.Count != 0);
		}
		float xJail = LevelBuilder.instance.constructionsActivator.XJail;
		int num = activeColls.Count - 1;
		for (int num2 = num; num2 >= 0; num2--)
		{
			CollectibleRubyNode collectibleRubyNode = activeColls[num2];
			Vector3 position3 = collectibleRubyNode.gameObject.transform.position;
			if (position3.x > xJail - 20f && xJail != 0f && collectibleRubyNode.gameObject.activeSelf)
			{
				collectibleRubyNode.gameObject.SetActive(value: false);
			}
			if (!collectibleRubyNode.gameObject.activeSelf)
			{
				RemoveObject(collectibleRubyNode);
			}
			else
			{
				Vector3 position4 = collectibleRubyNode.gameObject.transform.position;
				float x3 = position4.x;
				Vector3 position5 = followTransform.position;
				if (x3 < position5.x - collectibleAD)
				{
					RemoveObject(collectibleRubyNode);
					lStack.Push(collectibleRubyNode.node);
				}
				else
				{
					Vector3 position6 = collectibleRubyNode.gameObject.transform.position;
					float x4 = position6.x;
					Vector3 position7 = followTransform.position;
					if (x4 > position7.x + collectibleAD)
					{
						RemoveObject(collectibleRubyNode);
						rStack.Push(collectibleRubyNode.node);
					}
				}
			}
		}
	}

	private void RemoveObject(CollectibleRubyNode node)
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
		activeColls.Remove(node);
		collStack.Push(node);
	}

	private CollectibleRubyNode SpawnObject(CollectibleObjectRubyNode nodeInstance)
	{
		if (collStack.Count == 0)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(activeColls[0].gameObject);
			gameObject.transform.parent = activeColls[0].gameObject.gameObject.transform.parent;
			gameObject.name = gameObject.name.Replace("(Clone)", string.Empty);
			gameObject.gameObject.SetActive(value: false);
			collStack.Push(new CollectibleRubyNode(gameObject, null));
		}
		CollectibleRubyNode collectibleRubyNode = collStack.Pop();
		collectibleRubyNode.gameObject.transform.position = new Vector3(nodeInstance.position.x, nodeInstance.position.y);
		collectibleRubyNode.gameObject.transform.eulerAngles = new Vector3(0f, 0f, nodeInstance.position.z);
		collectibleRubyNode.sprite.SetSprite(spriteCollectionData[nodeInstance.sprite.collection], nodeInstance.sprite.id);
		collectibleRubyNode.sprite.GetComponent<Renderer>().sortingLayerName = nodeInstance.sprite.sortingLayer;
		collectibleRubyNode.sprite.GetComponent<Renderer>().sortingOrder = nodeInstance.sprite.sortingOrder;
		collectibleRubyNode.sprite.color = nodeInstance.sprite.color;
		collectibleRubyNode.sprite.scale = nodeInstance.sprite.scale;
		collectibleRubyNode.node = nodeInstance;
		collectibleRubyNode.script.itemType = nodeInstance.type;
		collectibleRubyNode.script.amount = nodeInstance.amount;
		switch (nodeInstance.collider.colliderType)
		{
		case "PolygonCollider2D":
			collectibleRubyNode.collider.polygon.points = nodeInstance.collider.vertices;
			collectibleRubyNode.collider.polygon.enabled = true;
			break;
		case "CircleCollider2D":
			collectibleRubyNode.collider.circle.offset = nodeInstance.collider.vertices[0];
			collectibleRubyNode.collider.circle.radius = nodeInstance.collider.vertices[1].x;
			collectibleRubyNode.collider.circle.enabled = true;
			break;
		case "EdgeCollider2D":
			collectibleRubyNode.collider.edge.points = nodeInstance.collider.vertices;
			collectibleRubyNode.collider.edge.enabled = true;
			break;
		case "BoxCollider2D":
			collectibleRubyNode.collider.box.offset = nodeInstance.collider.vertices[0];
			collectibleRubyNode.collider.box.size = nodeInstance.collider.vertices[1];
			collectibleRubyNode.collider.box.enabled = true;
			break;
		}
		collectibleRubyNode.gameObject.SetActive(value: true);
		return collectibleRubyNode;
	}
}
