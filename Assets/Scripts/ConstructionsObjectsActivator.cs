using System.Collections.Generic;
using UnityEngine;
using xmlClassTemplate.ConstructionObject;

public class ConstructionsObjectsActivator : MonoBehaviour
{
	public class ConstructionNode
	{
		public GameObject gameObject;

		public ConstructionObjectNode node;

		public bool isAlive = true;

		public ConstructionNode(GameObject gameObject, ConstructionObjectNode node)
		{
			this.gameObject = gameObject;
			this.node = node;
			this.gameObject.transform.position = new Vector3(node.position.x, node.position.y);
			this.gameObject.transform.rotation = node.rotation;
			this.gameObject.transform.eulerAngles = new Vector3(0f, 0f, node.position.z);
		}
	}

	private Transform followTransform;

	private Stack<ConstructionNode> lStack = new Stack<ConstructionNode>();

	private Stack<ConstructionNode> rStack = new Stack<ConstructionNode>();

	private List<ConstructionNode> activeConstructions = new List<ConstructionNode>();

	public List<ConstructionNode> TEstList = new List<ConstructionNode>();

	private bool isStarted;

	public ConstructionsObjectsActivatorAi COAA;

	public float XJail;

	public void CreateStack(List<ConstructionObjectNode> allConstructions, Transform follower)
	{
		XJail = 0f;
		followTransform = follower;
		List<ConstructionNode> list = new List<ConstructionNode>();
		Dictionary<string, ConstructionNode> dictionary = new Dictionary<string, ConstructionNode>();
		for (int i = 0; i < allConstructions.Count; i++)
		{
			if (!dictionary.ContainsKey(allConstructions[i].name))
			{
				GameObject gameObject = Resources.Load(allConstructions[i].path + allConstructions[i].name) as GameObject;
				if (gameObject == null)
				{
					UnityEngine.Debug.LogError("No " + allConstructions[i].path + allConstructions[i].name + " in Resources. Ignore this");
					continue;
				}
				GameObject gameObject2 = new GameObject(allConstructions[i].name);
				gameObject2.transform.parent = base.gameObject.transform;
				GameObject gameObject3 = UnityEngine.Object.Instantiate(gameObject);
				gameObject3.transform.parent = gameObject2.transform;
				gameObject3.name = gameObject3.name.Replace("(Clone)", string.Empty);
				dictionary.Add(allConstructions[i].name, new ConstructionNode(gameObject3, allConstructions[i]));
				gameObject3.SetActive(value: false);
				list.Add(new ConstructionNode(gameObject3, allConstructions[i]));
			}
			else
			{
				GameObject gameObject4 = UnityEngine.Object.Instantiate(dictionary[allConstructions[i].name].gameObject);
				gameObject4.transform.parent = dictionary[allConstructions[i].name].gameObject.transform.parent.transform;
				gameObject4.name = gameObject4.name.Replace("(Clone)", string.Empty);
				gameObject4.SetActive(value: false);
				list.Add(new ConstructionNode(gameObject4, allConstructions[i]));
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			ConstructionNode item = list[j];
			TEstList.Add(item);
		}
		for (int num = list.Count - 1; num >= 0; num--)
		{
			rStack.Push(list[num]);
		}
		isStarted = true;
	}

	public void Restart(List<ConstructionObjectNode> allConstructions)
	{
		XJail = 0f;
		isStarted = false;
		lStack.Clear();
		rStack.Clear();
		activeConstructions.Clear();
		Transform[] componentsInChildren = base.transform.GetComponentsInChildren<Transform>();
		for (int num = componentsInChildren.Length - 1; num >= 0; num--)
		{
			if (componentsInChildren[num] != base.transform)
			{
				UnityEngine.Object.Destroy(componentsInChildren[num].gameObject);
			}
		}
		CreateStack(allConstructions, followTransform);
		isStarted = true;
	}

	private void Update()
	{
		if (!isStarted)
		{
			return;
		}
		if (Progress.shop.bossLevel && RaceLogic.instance.BossDeath && XJail == 0f && rStack.Peek().node.name == "finish_jail")
		{
			XJail = rStack.Peek().node.position.x;
		}
		float constructionsAD = LevelBuilder.constructionsAD;
		Vector3 position = followTransform.position;
		float x = position.x;
		if (rStack.Count > 0)
		{
			while (rStack.Peek().node.position.x <= x + constructionsAD)
			{
				ConstructionNode constructionNode = rStack.Pop();
				constructionNode.gameObject.SetActive(value: true);
				activeConstructions.Add(constructionNode);
				if (rStack.Count == 0)
				{
					break;
				}
			}
		}
		if (lStack.Count > 0)
		{
			while (lStack.Peek().node.position.x >= x - constructionsAD)
			{
				ConstructionNode constructionNode2 = lStack.Pop();
				constructionNode2.gameObject.SetActive(value: true);
				activeConstructions.Add(constructionNode2);
				if (lStack.Count == 0)
				{
					break;
				}
			}
		}
		int num = activeConstructions.Count;
		for (int i = 0; i < num; i++)
		{
			ConstructionNode constructionNode3 = activeConstructions[i];
			Vector3 position2 = constructionNode3.gameObject.transform.position;
			float x2 = position2.x;
			if (x2 < x - constructionsAD)
			{
				if (constructionNode3.node.name != "Enemy_Spider")
				{
					constructionNode3.gameObject.SetActive(value: false);
				}
				activeConstructions.Remove(constructionNode3);
				lStack.Push(constructionNode3);
				i--;
				num--;
				continue;
			}
			if (x2 > x + constructionsAD)
			{
				if (constructionNode3.node.name != "Enemy_Spider")
				{
					constructionNode3.gameObject.SetActive(value: false);
				}
				activeConstructions.Remove(constructionNode3);
				rStack.Push(constructionNode3);
				i--;
				num--;
				continue;
			}
			if (x2 > XJail - 20f && XJail != 0f && constructionNode3.gameObject.name != "finish_jail" && constructionNode3.node.name != "Enemy_Spider")
			{
				constructionNode3.gameObject.SetActive(value: false);
			}
			if (!constructionNode3.gameObject.activeSelf)
			{
				constructionNode3.isAlive = false;
				activeConstructions.Remove(constructionNode3);
				i--;
				num--;
			}
		}
		if (COAA != null)
		{
			for (int j = 0; j < activeConstructions.Count; j++)
			{
				Vector3 position3 = activeConstructions[j].gameObject.transform.position;
				foreach (ConstructionsObjectsActivatorAi.ConstructionNode activeConstruction in COAA.activeConstructions)
				{
					if (position3 == activeConstruction.gameObject.transform.position && activeConstruction.node.name != "Enemy_Spider")
					{
						activeConstruction.gameObject.SetActive(value: false);
					}
				}
			}
		}
	}
}
