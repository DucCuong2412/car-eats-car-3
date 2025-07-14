using System.Collections.Generic;
using UnityEngine;
using xmlClassTemplate.GroundObject;

public class GroundActivationAi : MonoBehaviour
{
	private class Ground
	{
		public EasyHill2DNode node;

		public EasyHill2DNode decor;

		public GroundObjectNode gNode;

		public Ground(EasyHill2DNode p_node, GroundObjectNode p_gNode = null)
		{
			node = p_node;
			EasyHill2DNode[] componentsInChildren = p_node.GetComponentsInChildren<EasyHill2DNode>();
			EasyHill2DNode[] array = componentsInChildren;
			foreach (EasyHill2DNode x in array)
			{
				if (x != p_node)
				{
					decor = x;
					break;
				}
			}
			gNode = p_gNode;
		}
	}

	public Transform followTransform;

	public EasyHill2DNode groundInstance;

	private static GroundActivationAi p_instance;

	private List<Ground> activeGrounds = new List<Ground>();

	private Stack<Ground> groundStack = new Stack<Ground>();

	private Stack<GroundObjectNode> lStack = new Stack<GroundObjectNode>();

	private Stack<GroundObjectNode> rStack = new Stack<GroundObjectNode>();

	private bool isStarted;

	private static List<Ground> allGrounds;

	private float distanceToActivate => LevelBuilder.groundAD;

	public GroundActivationAi()
	{
		p_instance = this;
	}

	private void OnDestroy()
	{
		groundInstance = null;
		p_instance = null;
	}

	public void CreateStack(Transform follower, int pack, int stackLength = 10)
	{
		followTransform = follower;
		groundInstance = (Resources.Load("Ground_" + pack) as GameObject).GetComponent<EasyHill2DNode>();
		groundInstance.gameObject.SetActive(value: true);
		for (int i = 0; i < stackLength; i++)
		{
			EasyHill2DNode easyHill2DNode = UnityEngine.Object.Instantiate(groundInstance);
			easyHill2DNode.transform.parent = base.transform;
			easyHill2DNode.gameObject.name = easyHill2DNode.gameObject.name.Replace("(Clone)", string.Empty);
			MeshFilter[] componentsInChildren = easyHill2DNode.gameObject.GetComponentsInChildren<MeshFilter>();
			MeshFilter[] array = componentsInChildren;
			foreach (MeshFilter meshFilter in array)
			{
				meshFilter.mesh = new Mesh();
			}
			groundStack.Push(new Ground(easyHill2DNode));
			easyHill2DNode.gameObject.SetActive(value: false);
		}
		GameObject gameObject = new GameObject("Decor");
		gameObject.transform.parent = base.gameObject.transform;
	}

	public void Restart(List<GroundObjectNode> list)
	{
		isStarted = false;
		rStack.Clear();
		lStack.Clear();
		while (activeGrounds.Count > 0)
		{
			RemoveObject(activeGrounds[0]);
		}
		for (int num = list.Count - 1; num >= 0; num--)
		{
			rStack.Push(list[num]);
		}
		isStarted = true;
		Update();
	}

	private void Update()
	{
		if (!isStarted)
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
				activeGrounds.Add(SpawnObject(rStack.Pop()));
			}
			while (rStack.Count != 0);
		}
		if (lStack.Count > 0)
		{
			do
			{
				float num = lStack.Peek().position.x + lStack.Peek().P4.x;
				Vector3 position2 = followTransform.position;
				if (!(num >= position2.x - distanceToActivate))
				{
					break;
				}
				activeGrounds.Add(SpawnObject(lStack.Pop()));
			}
			while (lStack.Count != 0);
		}
		for (int i = 0; i < activeGrounds.Count; i++)
		{
			Ground ground = activeGrounds[i];
			Vector3 position3 = ground.node.transform.position;
			float num2 = position3.x + ground.node.p4.x;
			Vector3 position4 = followTransform.position;
			if (num2 < position4.x - distanceToActivate)
			{
				RemoveObject(ground);
				lStack.Push(ground.gNode);
				i--;
				continue;
			}
			Vector3 position5 = ground.node.transform.position;
			float num3 = position5.x + ground.node.p1.x;
			Vector3 position6 = followTransform.position;
			if (num3 > position6.x + distanceToActivate)
			{
				RemoveObject(ground);
				rStack.Push(ground.gNode);
				i--;
			}
		}
	}

	private Ground SpawnObject(GroundObjectNode gNode)
	{
		if (groundStack.Count == 0)
		{
			UnityEngine.Debug.LogWarning("Stack of ground nodes is empty!!! Create new one");
			EasyHill2DNode easyHill2DNode = UnityEngine.Object.Instantiate(groundInstance);
			easyHill2DNode.transform.parent = base.transform;
			easyHill2DNode.gameObject.name = easyHill2DNode.gameObject.name.Replace("(Clone)", string.Empty);
			MeshFilter[] componentsInChildren = easyHill2DNode.gameObject.GetComponentsInChildren<MeshFilter>();
			MeshFilter[] array = componentsInChildren;
			foreach (MeshFilter meshFilter in array)
			{
				meshFilter.mesh = new Mesh();
			}
			groundStack.Push(new Ground(easyHill2DNode));
			easyHill2DNode.gameObject.SetActive(value: false);
		}
		Ground ground = groundStack.Pop();
		ground.node.transform.position = gNode.position;
		ground.node.p2 = gNode.P2;
		ground.node.p3 = gNode.P3;
		ground.node.p4 = gNode.P4;
		ground.node.elementNumber = gNode.elementNumber;
		ground.node.segmentHeight = gNode.segmentHeight;
		ground.node.gameObject.SetActive(value: true);
		ground.node.calculateMesh();
		ground.node.SnapChildren();
		ground.gNode = gNode;
		return ground;
	}

	private void RemoveObject(Ground ground)
	{
		ground.node.gameObject.SetActive(value: false);
		activeGrounds.Remove(ground);
		groundStack.Push(ground);
	}

	public static void DestructGround(Vector3 pos, float radius, float strength)
	{
		if (p_instance == null)
		{
			if (allGrounds == null)
			{
				allGrounds = new List<Ground>();
				EasyHill2DNode[] array = UnityEngine.Object.FindObjectsOfType<EasyHill2DNode>();
				EasyHill2DNode[] array2 = array;
				foreach (EasyHill2DNode easyHill2DNode in array2)
				{
					if (easyHill2DNode.groundCollider)
					{
						Ground item = new Ground(easyHill2DNode);
						allGrounds.Add(item);
					}
				}
			}
			Destruct(allGrounds, pos, radius, strength);
		}
		else
		{
			Destruct(p_instance.activeGrounds, pos, radius, strength);
		}
	}

	private static void Destruct(List<Ground> list, Vector3 pos, float radius, float strength)
	{
		for (int i = 0; i < list.Count; i++)
		{
			EasyHill2DNode node = list[i].node;
			EasyHill2DNode decor = list[i].decor;
			float num = pos.x + radius;
			float x = node.p1.x;
			Vector3 position = node.gameObject.transform.position;
			if (!(num > x + position.x))
			{
				continue;
			}
			float num2 = pos.x - radius;
			float x2 = node.p4.x;
			Vector3 position2 = node.gameObject.transform.position;
			if (!(num2 < x2 + position2.x))
			{
				continue;
			}
			for (int j = 0; j < node.physicsVertices.Length; j++)
			{
				float num3 = Vector2.Distance(new Vector3(node.physicsVertices[j].x, node.physicsVertices[j].y) + node.gameObject.transform.position, pos + node.gameObject.transform.position);
				if (num3 < radius)
				{
					float d = Mathf.Abs(radius - num3) * strength;
					node.physicsVertices[j] += -Vector2.up * d;
					decor.physicsVertices[j] += -Vector2.up * d;
					decor.groundVertices[j] += -Vector2.up * d;
				}
			}
			node.UpdateMesh();
			decor.UpdateMesh();
		}
	}
}
