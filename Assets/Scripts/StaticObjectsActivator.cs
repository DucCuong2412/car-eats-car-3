using System.Collections.Generic;
using UnityEngine;
using xmlClassTemplate.StaticObject;

public class StaticObjectsActivator : MonoBehaviour
{
	private class StaticNode
	{
		public GameObject gameObject;

		public tk2dSprite sprite;

		public StaticObjectNode node;

		public ObjectType.Type type;

		public StaticNode(GameObject go, StaticObjectNode p_node)
		{
			node = p_node;
			gameObject = go;
			sprite = go.GetComponent<tk2dSprite>();
		}
	}

	private Transform followTransform;

	private Stack<StaticObjectNode> lStack = new Stack<StaticObjectNode>();

	private Stack<StaticObjectNode> rStack = new Stack<StaticObjectNode>();

	private List<StaticNode> activeDecors = new List<StaticNode>();

	private bool isStarted;

	private Dictionary<string, tk2dSpriteCollectionData> spriteCollectionData = new Dictionary<string, tk2dSpriteCollectionData>();

	private Stack<StaticNode> decorStack = new Stack<StaticNode>();

	public void CreateStack(Transform follower, int stackLength = 50)
	{
		followTransform = follower;
		GameObject original = Resources.Load("Decor_Static") as GameObject;
		for (int i = 0; i < stackLength; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(original);
			gameObject.transform.parent = base.transform;
			gameObject.name = gameObject.name.Replace("(Clone)", string.Empty);
			decorStack.Push(new StaticNode(gameObject, null));
		}
	}

	public void Restart(List<StaticObjectNode> list)
	{
		isStarted = false;
		rStack.Clear();
		lStack.Clear();
		while (activeDecors.Count > 0)
		{
			RemoveObject(activeDecors[0]);
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
				UnityEngine.Debug.LogError("Some sprite in static/decor collection is not loadable!");
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
		float staticAD = LevelBuilder.staticAD;
		Vector3 position = followTransform.position;
		float x = position.x;
		if (rStack.Count > 0)
		{
			while (rStack.Peek().position.x <= x + staticAD)
			{
				StaticNode item = SpawnObject(rStack.Pop());
				activeDecors.Add(item);
				if (rStack.Count == 0)
				{
					break;
				}
			}
		}
		if (lStack.Count > 0)
		{
			while (lStack.Peek().position.x >= x - staticAD)
			{
				activeDecors.Add(SpawnObject(lStack.Pop()));
				if (lStack.Count == 0)
				{
					break;
				}
			}
		}
		int num = activeDecors.Count;
		float xJail = LevelBuilder.instance.constructionsActivator.XJail;
		for (int i = 0; i < num; i++)
		{
			StaticNode staticNode = activeDecors[i];
			Vector3 position2 = staticNode.gameObject.transform.position;
			if (position2.x < x - staticAD)
			{
				RemoveObject(staticNode);
				lStack.Push(staticNode.node);
				i--;
				num--;
			}
			else if (position2.x > x + staticAD)
			{
				RemoveObject(staticNode);
				rStack.Push(staticNode.node);
				i--;
				num--;
			}
			else if (position2.x > xJail - 20f && xJail != 0f && staticNode.gameObject.activeSelf)
			{
				staticNode.gameObject.SetActive(value: false);
				i--;
				num--;
			}
		}
	}

	private void RemoveObject(StaticNode node)
	{
		node.gameObject.SetActive(value: false);
		activeDecors.Remove(node);
		decorStack.Push(node);
	}

	private StaticNode SpawnObject(StaticObjectNode nodeInstance)
	{
		if (decorStack.Count == 0)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(activeDecors[0].gameObject);
			gameObject.transform.parent = activeDecors[0].gameObject.gameObject.transform.parent;
			gameObject.name = gameObject.name.Replace("(Clone)", string.Empty);
			gameObject.gameObject.SetActive(value: false);
			decorStack.Push(new StaticNode(gameObject, null));
		}
		StaticNode staticNode = decorStack.Pop();
		staticNode.gameObject.transform.position = new Vector3(nodeInstance.position.x, nodeInstance.position.y);
		staticNode.gameObject.transform.eulerAngles = new Vector3(0f, 0f, nodeInstance.position.z);
		staticNode.gameObject.transform.localScale = nodeInstance.scale;
		staticNode.sprite.SetSprite(spriteCollectionData[nodeInstance.sprite.collection], nodeInstance.sprite.id);
		staticNode.sprite.GetComponent<Renderer>().sortingLayerName = nodeInstance.sprite.sortingLayer;
		staticNode.sprite.SortingOrder = nodeInstance.sprite.sortingOrder;
		staticNode.sprite.scale = nodeInstance.sprite.scale;
		staticNode.sprite.color = nodeInstance.sprite.color;
		staticNode.type = nodeInstance.type;
		staticNode.node = nodeInstance;
		staticNode.gameObject.SetActive(value: true);
		return staticNode;
	}
}
