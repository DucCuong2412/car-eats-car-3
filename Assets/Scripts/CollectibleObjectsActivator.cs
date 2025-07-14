using System;
using System.Collections.Generic;
using UnityEngine;
using xmlClassTemplate.CollectibleObject;

public class CollectibleObjectsActivator : MonoBehaviour
{
	[Serializable]
	private class CollectibleNode
	{
		public GameObject gameObject;

		public tk2dSprite sprite;

		public Colliders collider;

		public CollectibleObjectNode node;

		public CollectibleItem script;

		public tk2dSpriteAnimator animator;

		public CollectibleNode(GameObject go, CollectibleObjectNode p_node)
		{
			node = p_node;
			gameObject = go;
			sprite = go.GetComponent<tk2dSprite>();
			collider = go.GetComponent<Colliders>();
			script = go.GetComponent<CollectibleItem>();
			animator = go.GetComponent<tk2dSpriteAnimator>();
		}
	}

	private Transform followTransform;

	private Stack<CollectibleObjectNode> lStack = new Stack<CollectibleObjectNode>();

	private Stack<CollectibleObjectNode> rStack = new Stack<CollectibleObjectNode>();

	private List<CollectibleNode> activeColls = new List<CollectibleNode>();

	private bool isStarted;

	private Dictionary<string, tk2dSpriteCollectionData> spriteCollectionData = new Dictionary<string, tk2dSpriteCollectionData>();

	[SerializeField]
	private Stack<CollectibleNode> collStack = new Stack<CollectibleNode>();

	private List<CollectibleNode> tList = new List<CollectibleNode>();

	private int tTimeToff = 10;

	private float distanceToActivate => LevelBuilder.collectibleAD;

	public void CreateStack(Transform follower, int stackLength = 200)
	{
		followTransform = follower;
		GameObject original = Resources.Load("Collectible_Object") as GameObject;
		tList.Clear();
		for (int i = 0; i < stackLength; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(original);
			gameObject.transform.parent = base.transform;
			gameObject.name = gameObject.name.Replace("(Clone)", string.Empty);
			CollectibleNode collectibleNode = new CollectibleNode(gameObject, null);
			collStack.Push(collectibleNode);
			tList.Add(collectibleNode);
		}
		enabledAll(enable: true);
		tTimeToff = 10;
	}

	private void enabledAll(bool enable = false)
	{
		for (int i = 0; i < tList.Count; i++)
		{
			if (enable)
			{
				tList[i].gameObject.SetActive(value: true);
			}
			BoostAnimator component = tList[i].gameObject.GetComponent<BoostAnimator>();
			if (component != null)
			{
				component.EnabledAll(enable);
			}
			if (!enable)
			{
				tList[i].gameObject.SetActive(value: false);
			}
			component = null;
		}
	}

	public void Restart(List<CollectibleObjectNode> list)
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
		if (tTimeToff >= 0)
		{
			tTimeToff--;
			if (tTimeToff == 0)
			{
				enabledAll();
				tTimeToff = -1;
			}
		}
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
				if (!(x2 >= position2.x - distanceToActivate))
				{
					break;
				}
				activeColls.Add(SpawnObject(lStack.Pop()));
			}
			while (lStack.Count != 0);
		}
		for (int num = activeColls.Count - 1; num >= 0; num--)
		{
			CollectibleNode collectibleNode = activeColls[num];
			Vector3 position3 = collectibleNode.gameObject.transform.position;
			if (position3.x > LevelBuilder.instance.constructionsActivator.XJail - 20f && LevelBuilder.instance.constructionsActivator.XJail != 0f && collectibleNode.gameObject.activeSelf)
			{
				collectibleNode.gameObject.SetActive(value: false);
				activeColls.Remove(collectibleNode);
			}
			else if (!collectibleNode.gameObject.activeSelf)
			{
				RemoveObject(collectibleNode);
			}
			else
			{
				Vector3 position4 = collectibleNode.gameObject.transform.position;
				float x3 = position4.x;
				Vector3 position5 = followTransform.position;
				if (x3 < position5.x - distanceToActivate)
				{
					RemoveObject(collectibleNode);
					lStack.Push(collectibleNode.node);
				}
				else
				{
					Vector3 position6 = collectibleNode.gameObject.transform.position;
					float x4 = position6.x;
					Vector3 position7 = followTransform.position;
					if (x4 > position7.x + distanceToActivate)
					{
						RemoveObject(collectibleNode);
						rStack.Push(collectibleNode.node);
					}
				}
			}
		}
	}

	private void RemoveObject(CollectibleNode node)
	{
		node.gameObject.SetActive(value: false);
		activeColls.Remove(node);
		collStack.Push(node);
	}

	private CollectibleNode SpawnObject(CollectibleObjectNode nodeInstance)
	{
		if (collStack.Count == 0)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(activeColls[0].gameObject);
			gameObject.transform.parent = activeColls[0].gameObject.gameObject.transform.parent;
			gameObject.name = gameObject.name.Replace("(Clone)", string.Empty);
			gameObject.gameObject.SetActive(value: false);
			collStack.Push(new CollectibleNode(gameObject, null));
		}
		CollectibleNode collectibleNode = collStack.Pop();
		collectibleNode.gameObject.transform.position = new Vector3(nodeInstance.position.x, nodeInstance.position.y);
		collectibleNode.gameObject.transform.eulerAngles = new Vector3(0f, 0f, nodeInstance.position.z);
		collectibleNode.sprite.SetSprite(spriteCollectionData[nodeInstance.sprite.collection], nodeInstance.sprite.id);
		collectibleNode.sprite.GetComponent<Renderer>().sortingLayerName = nodeInstance.sprite.sortingLayer;
		collectibleNode.sprite.GetComponent<Renderer>().sortingOrder = nodeInstance.sprite.sortingOrder;
		collectibleNode.sprite.color = new Color(nodeInstance.sprite.color.r, nodeInstance.sprite.color.g, nodeInstance.sprite.color.b, 0f);
		collectibleNode.sprite.scale = nodeInstance.sprite.scale;
		collectibleNode.node = nodeInstance;
		collectibleNode.script.itemType = nodeInstance.type;
		collectibleNode.script.amount = nodeInstance.amount;
		collectibleNode.gameObject.SetActive(value: true);
		return collectibleNode;
	}
}
