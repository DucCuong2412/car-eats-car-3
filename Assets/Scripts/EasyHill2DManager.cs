using System.Collections.Generic;
using UnityEngine;

public class EasyHill2DManager
{
	public List<EasyHill2DNode> easyHill2DNodes;

	public List<EasyHill2DNode> easyHill2DDeko;

	private static EasyHill2DManager _instance;

	public bool mySnap;

	public bool myLockY;

	public float mySnapDistance;

	public float myLockYValue;

	public float myDekoOffsetValue;

	public bool myDekoOffset;

	private Camera cam;

	private float height;

	private float width;

	public static EasyHill2DManager Instance()
	{
		if (_instance == null)
		{
			_instance = new EasyHill2DManager();
			_instance.easyHill2DNodes = new List<EasyHill2DNode>();
			_instance.easyHill2DDeko = new List<EasyHill2DNode>();
			_instance.getAllHill2DNodes();
			_instance.updateEditorWindowVariables();
		}
		return _instance;
	}

	public void getAllHill2DNodes()
	{
		easyHill2DNodes = new List<EasyHill2DNode>();
		easyHill2DDeko = new List<EasyHill2DNode>();
		EasyHill2DNode[] array = Object.FindObjectsOfType<EasyHill2DNode>();
		EasyHill2DNode[] array2 = array;
		foreach (EasyHill2DNode easyHill2DNode in array2)
		{
			if (easyHill2DNode.groundCollider)
			{
				easyHill2DNodes.Add(easyHill2DNode);
			}
			else
			{
				easyHill2DDeko.Add(easyHill2DNode);
			}
		}
	}

	public void InitCulling()
	{
		cam = Camera.main;
		height = 2f * cam.orthographicSize;
		width = height * cam.aspect;
		getAllHill2DNodes();
	}

	public void CullSegments()
	{
		foreach (EasyHill2DNode easyHill2DNode in easyHill2DNodes)
		{
			float x = easyHill2DNode.p1.x;
			Vector3 position = easyHill2DNode.gameObject.transform.position;
			float num = x + position.x;
			Vector3 position2 = cam.transform.position;
			if (num < position2.x + width / 2f)
			{
				float x2 = easyHill2DNode.p4.x;
				Vector3 position3 = easyHill2DNode.gameObject.transform.position;
				float num2 = x2 + position3.x;
				Vector3 position4 = cam.transform.position;
				if (num2 > position4.x - width / 2f)
				{
					easyHill2DNode.gameObject.SetActive(value: true);
					continue;
				}
			}
			easyHill2DNode.gameObject.SetActive(value: false);
		}
	}

	public void DestructSegmentCircle(float pX, float pY, float pRadius, float pStrength)
	{
		foreach (EasyHill2DNode easyHill2DNode in easyHill2DNodes)
		{
			if (easyHill2DNode.destrucible)
			{
				float num = pX + pRadius;
				float x = easyHill2DNode.p1.x;
				Vector3 position = easyHill2DNode.gameObject.transform.position;
				if (num > x + position.x)
				{
					float num2 = pX - pRadius;
					float x2 = easyHill2DNode.p4.x;
					Vector3 position2 = easyHill2DNode.gameObject.transform.position;
					if (num2 < x2 + position2.x)
					{
						for (int i = 0; i < easyHill2DNode.physicsVertices.Length; i++)
						{
							float num3 = Vector2.Distance(new Vector3(easyHill2DNode.physicsVertices[i].x, easyHill2DNode.physicsVertices[i].y) + easyHill2DNode.gameObject.transform.position, new Vector3(pX, pY, 0f) + easyHill2DNode.gameObject.transform.position);
							if (num3 < pRadius)
							{
								float num4 = Mathf.Abs(pRadius - num3) * pStrength;
								if (easyHill2DNode.physicsVertices[i].y - num4 < 0f)
								{
									num4 = easyHill2DNode.physicsVertices[i].y;
								}
								easyHill2DNode.physicsVertices[i] = new Vector3(easyHill2DNode.physicsVertices[i].x, easyHill2DNode.physicsVertices[i].y - num4);
							}
						}
						easyHill2DNode.UpdateMesh();
					}
				}
			}
		}
	}

	public void selectAllSegments()
	{
		GameObject[] array = new GameObject[easyHill2DNodes.Count];
		for (int i = 0; i < easyHill2DNodes.Count; i++)
		{
			array[i] = easyHill2DNodes[i].gameObject;
		}
	}

	public void selectAllDeko()
	{
		GameObject[] array = new GameObject[easyHill2DDeko.Count];
		for (int i = 0; i < easyHill2DDeko.Count; i++)
		{
			array[i] = easyHill2DDeko[i].gameObject;
		}
	}

	public void selectAllSegmentsAndDeko()
	{
		GameObject[] array = new GameObject[easyHill2DDeko.Count + easyHill2DNodes.Count];
		for (int i = 0; i < easyHill2DNodes.Count; i++)
		{
			array[i] = easyHill2DNodes[i].gameObject;
		}
		for (int j = easyHill2DNodes.Count; j < easyHill2DDeko.Count + easyHill2DNodes.Count; j++)
		{
			array[j] = easyHill2DDeko[j - easyHill2DNodes.Count].gameObject;
		}
	}

	public Vector2 getSnapPoint(Vector2 pPosition)
	{
		Vector3 v = pPosition;
		foreach (EasyHill2DNode easyHill2DNode in easyHill2DNodes)
		{
			if (easyHill2DNode != null)
			{
				float num = Vector3.Distance(pPosition, new Vector3(easyHill2DNode.p4.x, easyHill2DNode.p4.y) + easyHill2DNode.transform.position);
				float num2 = Vector3.Distance(pPosition, new Vector3(easyHill2DNode.p1.x, easyHill2DNode.p1.y) + easyHill2DNode.transform.position);
				if (num < mySnapDistance && num != 0f)
				{
					v = new Vector3(easyHill2DNode.p4.x, easyHill2DNode.p4.y) + easyHill2DNode.transform.position;
				}
				else if (num2 < mySnapDistance && num2 != 0f)
				{
					v = new Vector3(easyHill2DNode.p1.x, easyHill2DNode.p1.y) + easyHill2DNode.transform.position;
				}
			}
		}
		return v;
	}

	public float getMinX()
	{
		getAllHill2DNodes();
		float x = easyHill2DNodes[0].p1.x;
		Vector3 position = easyHill2DNodes[0].gameObject.transform.position;
		float num = x + position.x;
		for (int i = 0; i < easyHill2DNodes.Count; i++)
		{
			float x2 = easyHill2DNodes[i].p1.x;
			Vector3 position2 = easyHill2DNodes[i].gameObject.transform.position;
			if (x2 + position2.x < num)
			{
				float x3 = easyHill2DNodes[i].p1.x;
				Vector3 position3 = easyHill2DNodes[i].gameObject.transform.position;
				num = x3 + position3.x;
			}
		}
		return num;
	}

	public float getMaxX()
	{
		getAllHill2DNodes();
		float x = easyHill2DNodes[0].p4.x;
		Vector3 position = easyHill2DNodes[0].gameObject.transform.position;
		float num = x + position.x;
		for (int i = 0; i < easyHill2DNodes.Count; i++)
		{
			float x2 = easyHill2DNodes[i].p4.x;
			Vector3 position2 = easyHill2DNodes[i].gameObject.transform.position;
			if (x2 + position2.x > num)
			{
				float x3 = easyHill2DNodes[i].p4.x;
				Vector3 position3 = easyHill2DNodes[i].gameObject.transform.position;
				num = x3 + position3.x;
			}
		}
		return num;
	}

	public void createSegment(Material mat)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = "EasyHill2DSegment";
		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();
		gameObject.GetComponent<Renderer>().sharedMaterial = mat;
		EasyHill2DNode easyHill2DNode = gameObject.AddComponent<EasyHill2DNode>();
		easyHill2DNode.p2 = new Vector2(5f, 0f);
		easyHill2DNode.p3 = new Vector2(10f, 0f);
		easyHill2DNode.p4 = new Vector2(15f, 0f);
		easyHill2DNode.segmentHeight = -5f;
		easyHill2DNode.groundStyle = EasyHill2DNode.GroundStyle.STRAIGHT;
		easyHill2DNode.textureStyle = EasyHill2DNode.TextureStyle.CONSTANT;
		easyHill2DNode.calculateMesh();
		gameObject.isStatic = false;
	}

	public void createSegmentRight(EasyHill2DNode p)
	{
		GameObject gameObject = Object.Instantiate(p.gameObject, p.gameObject.transform.position, Quaternion.identity);
		gameObject.name = p.gameObject.name;
		EasyHill2DNode component = gameObject.GetComponent<EasyHill2DNode>();
		gameObject.transform.position = p.transform.position + new Vector3(p.p4.x, p.p4.y);
		component.calculateMesh();
	}

	public void createSegmentLeft(EasyHill2DNode p)
	{
		GameObject gameObject = Object.Instantiate(p.gameObject, p.gameObject.transform.position, Quaternion.identity);
		gameObject.name = p.gameObject.name;
		EasyHill2DNode component = gameObject.GetComponent<EasyHill2DNode>();
		gameObject.transform.position = p.transform.position - new Vector3(p.p4.x, p.p4.y);
		component.calculateMesh();
	}

	public void createDeko(Material mat, EasyHill2DNode p)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = p.gameObject.name + "Deko";
		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();
		gameObject.GetComponent<Renderer>().sharedMaterial = mat;
		EasyHill2DNode easyHill2DNode = gameObject.AddComponent<EasyHill2DNode>();
		easyHill2DNode.p2 = new Vector2(5f, 10f);
		easyHill2DNode.p3 = new Vector2(10f, 10f);
		easyHill2DNode.p4 = new Vector2(15f, 10f);
		easyHill2DNode.groundStyle = EasyHill2DNode.GroundStyle.BEZIER;
		easyHill2DNode.textureStyle = EasyHill2DNode.TextureStyle.FIXED_WIDTH;
		easyHill2DNode.sortingOrder = 1;
		easyHill2DNode.snapToParent = true;
		easyHill2DNode.segmentHeight = 3f;
		easyHill2DNode.groundCollider = false;
		easyHill2DNode.textureRepeatHeight = 0.95f;
		gameObject.transform.parent = p.gameObject.transform;
		easyHill2DNode.checkColliderEnabled();
		easyHill2DNode.SnapToParent();
		easyHill2DNode.calculateMesh();
		gameObject.isStatic = false;
	}

	public void setSegementsStatic()
	{
		foreach (EasyHill2DNode item in easyHill2DDeko)
		{
			item.gameObject.isStatic = false;
		}
		foreach (EasyHill2DNode easyHill2DNode in easyHill2DNodes)
		{
			easyHill2DNode.gameObject.isStatic = false;
		}
	}

	public void CleanForSaving()
	{
		getAllHill2DNodes();
		foreach (EasyHill2DNode easyHill2DNode in easyHill2DNodes)
		{
			UnityEngine.Object.DestroyImmediate(easyHill2DNode.mesh);
			UnityEngine.Object.DestroyImmediate(easyHill2DNode.meshFilter);
			UnityEngine.Object.DestroyImmediate(easyHill2DNode.gameObject.GetComponent<EdgeCollider2D>());
			easyHill2DNode.physicsVertices = null;
			easyHill2DNode.groundVertices = null;
			easyHill2DNode.meshVertices = null;
			easyHill2DNode.gameObject.isStatic = false;
		}
		foreach (EasyHill2DNode item in easyHill2DDeko)
		{
			UnityEngine.Object.DestroyImmediate(item.mesh);
			UnityEngine.Object.DestroyImmediate(item.meshFilter);
			UnityEngine.Object.DestroyImmediate(item.gameObject.GetComponent<EdgeCollider2D>());
			item.physicsVertices = null;
			item.groundVertices = null;
			item.meshVertices = null;
			item.gameObject.isStatic = false;
		}
	}

	public void restoreSegments()
	{
		getAllHill2DNodes();
		foreach (EasyHill2DNode item in easyHill2DDeko)
		{
			item.calculateMesh();
		}
		foreach (EasyHill2DNode easyHill2DNode in easyHill2DNodes)
		{
			easyHill2DNode.calculateMesh();
		}
	}

	public void ConvertToIdentity()
	{
		foreach (EasyHill2DNode easyHill2DNode in easyHill2DNodes)
		{
			Vector2 p = easyHill2DNode.p1;
			easyHill2DNode.p1 -= p;
			easyHill2DNode.p3 -= p;
			easyHill2DNode.p2 -= p;
			easyHill2DNode.p4 -= p;
			easyHill2DNode.gameObject.transform.position += new Vector3(p.x, p.y);
			float num = easyHill2DNode.segmentHeight = Mathf.Min(easyHill2DNode.p1.y, easyHill2DNode.p2.y, easyHill2DNode.p3.y, easyHill2DNode.p4.y) - 30f;
			easyHill2DNode.calculateMesh();
		}
		foreach (EasyHill2DNode item in easyHill2DDeko)
		{
			Vector2 p2 = item.p1;
			item.p1 -= p2;
			item.p3 -= p2;
			item.p2 -= p2;
			item.p4 -= p2;
			item.calculateMesh();
		}
	}

	public void AlignHeight()
	{
		float num = float.MaxValue;
		foreach (EasyHill2DNode easyHill2DNode2 in easyHill2DNodes)
		{
			Vector3 position = easyHill2DNode2.gameObject.transform.position;
			float num2 = position.y + Mathf.Min(easyHill2DNode2.p1.y, easyHill2DNode2.p2.y, easyHill2DNode2.p3.y, easyHill2DNode2.p4.y);
			if (num2 < num)
			{
				num = num2;
			}
		}
		foreach (EasyHill2DNode easyHill2DNode3 in easyHill2DNodes)
		{
			EasyHill2DNode easyHill2DNode = easyHill2DNode3;
			Vector3 position2 = easyHill2DNode3.transform.position;
			easyHill2DNode.segmentHeight = -30f - position2.y + num;
			easyHill2DNode3.calculateMesh();
		}
	}

	private Vector2 lerpPoint(Vector2 v1, Vector2 v2, float t)
	{
		return new Vector2(v1.x * (1f - t) + v2.x * t, v1.y * (1f - t) + v2.y * t);
	}

	public void DivideSegment(EasyHill2DNode node, float t = 0.5f)
	{
		Vector2 p = node.p1;
		Vector2 p2 = node.p2;
		Vector2 p3 = node.p3;
		Vector2 p4 = node.p4;
		Vector2 vector = lerpPoint(p, p2, t);
		Vector2 vector2 = lerpPoint(p2, p3, t);
		Vector2 vector3 = lerpPoint(p3, p4, t);
		Vector2 vector4 = lerpPoint(vector, vector2, t);
		Vector2 vector5 = lerpPoint(vector2, vector3, t);
		Vector2 vector6 = lerpPoint(vector4, vector5, t);
		Vector2[] array = new Vector2[4]
		{
			p,
			vector,
			vector4,
			vector6
		};
		Vector2[] array2 = new Vector2[4]
		{
			vector6,
			vector5,
			vector3,
			p4
		};
		EasyHill2DNode component = Object.Instantiate(node.gameObject).GetComponent<EasyHill2DNode>();
		component.gameObject.name = component.gameObject.name.Replace("(Clone)", string.Empty);
		component.p1 = array[0];
		component.p2 = array[1];
		component.p3 = array[2];
		component.p4 = array[3];
		float num = Mathf.Min(component.p1.y, component.p2.y, component.p3.y, component.p4.y);
		component.segmentHeight = num - 30f;
		component.calculateMesh();
		component.UpdateMesh();
		EasyHill2DNode component2 = Object.Instantiate(node.gameObject).GetComponent<EasyHill2DNode>();
		component2.gameObject.name = component2.gameObject.name.Replace("(Clone)", string.Empty);
		component2.p1 = array2[0];
		component2.p2 = array2[1];
		component2.p3 = array2[2];
		component2.p4 = array2[3];
		num = Mathf.Min(component2.p1.y, component2.p2.y, component2.p3.y, component2.p4.y);
		component2.segmentHeight = num - 30f;
		component2.calculateMesh();
		component2.UpdateMesh();
	}

	private Vector2 getBezierCoordinates(Vector2 pP1, Vector2 pP2, Vector2 pP3, Vector2 pP4, float pPercentages)
	{
		return getBezierCoordinates(pP1.x, pP1.y, pP2.x, pP2.y, pP3.x, pP3.y, pP4.x, pP4.y, pPercentages);
	}

	private Vector2 getBezierCoordinates(float pX1, float pY1, float pX2, float pY2, float pX3, float pY3, float pX4, float pY4, float pPercentages)
	{
		float num = 1f - pPercentages;
		float num2 = pPercentages * pPercentages;
		float num3 = num * num;
		float num4 = num3 * num;
		float num5 = num2 * pPercentages;
		float num6 = 3f * num3 * pPercentages;
		float num7 = 3f * num * num2;
		float x = num4 * pX1 + num6 * pX2 + num7 * pX3 + num5 * pX4;
		float y = num4 * pY1 + num6 * pY2 + num7 * pY3 + num5 * pY4;
		return new Vector2(x, y);
	}

	public void checkDekoOffset()
	{
		foreach (EasyHill2DNode item in easyHill2DDeko)
		{
			item.checkDekoOffset();
		}
	}

	public void checkLockY()
	{
		foreach (EasyHill2DNode easyHill2DNode in easyHill2DNodes)
		{
			easyHill2DNode.checkLockY();
		}
	}

	public void updateEditorWindowVariables()
	{
		mySnap = getSnapping();
		myLockY = getLockY();
		mySnapDistance = getSnappingDistance();
		myLockYValue = getLockYValue();
		myDekoOffset = getDekoOffset();
		myDekoOffsetValue = getDekoOffsetValue();
	}

	public float getSnappingDistance()
	{
		return 0f;
	}

	public float getLockYValue()
	{
		return 0f;
	}

	public bool getLockY()
	{
		return true;
	}

	public bool getSnapping()
	{
		return true;
	}

	public float getDekoOffsetValue()
	{
		return 0f;
	}

	public bool getDekoOffset()
	{
		return true;
	}
}
