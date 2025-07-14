using System;
using UnityEngine;

public class EasyHill2DNode : MonoBehaviour
{
	public enum GroundStyle
	{
		STRAIGHT,
		BEZIER,
		PIPE
	}

	public enum TextureStyle
	{
		FIXED_WIDTH,
		STRETCH,
		CONSTANT
	}

	[HideInInspector]
	public Vector2 p1 = Vector2.zero;

	public Vector2 p2;

	public Vector2 p3;

	public Vector2 p4;

	[HideInInspector]
	public Vector2 worldP1;

	[HideInInspector]
	public Vector2 worldP2;

	[HideInInspector]
	public Vector2 worldP3;

	[HideInInspector]
	public Vector2 worldP4;

	[HideInInspector]
	public int elementNumber = 10;

	[HideInInspector]
	public float textureRepeatWidth = 33f;

	[HideInInspector]
	public float textureRepeatHeight = 33f;

	[HideInInspector]
	public float segmentHeight = 100f;

	[HideInInspector]
	public bool groundCollider = true;

	[HideInInspector]
	public bool snapToParent;

	public GroundStyle groundStyle = GroundStyle.BEZIER;

	public TextureStyle textureStyle = TextureStyle.STRETCH;

	[NonSerialized]
	public Vector2[] physicsVertices;

	[NonSerialized]
	public Vector2[] groundVertices;

	[NonSerialized]
	public Vector2[] meshVertices;

	[HideInInspector]
	public MeshFilter meshFilter;

	[HideInInspector]
	public Mesh mesh;

	[HideInInspector]
	public EasyHill2DManager easyHill2DManager;

	private bool updateToggle = true;

	[HideInInspector]
	public bool destrucible;

	[HideInInspector]
	public string sortingLayer;

	[HideInInspector]
	public int sortingOrder;

	private void Start()
	{
		if (!Application.isPlaying)
		{
			if (snapToParent)
			{
				SnapToParent();
			}
			calculateMesh();
		}
		GetComponent<Renderer>().sortingLayerName = sortingLayer;
		GetComponent<Renderer>().sortingOrder = sortingOrder;
	}

	private void Awake()
	{
		if (!Application.isPlaying)
		{
			meshFilter = base.gameObject.GetComponent<MeshFilter>();
			if (meshFilter != null)
			{
				meshFilter.mesh = new Mesh();
			}
			if (groundCollider)
			{
				EdgeCollider2D component = base.gameObject.GetComponent<EdgeCollider2D>();
				if (component != null)
				{
					component.Reset();
				}
			}
			EasyHill2DManager.Instance().getAllHill2DNodes();
		}
		else
		{
			calculateMesh();
		}
	}

	private void onEnable()
	{
	}

	private void OnDestroy()
	{
		if (!Application.isPlaying)
		{
			EasyHill2DManager.Instance().getAllHill2DNodes();
		}
	}

	private void Update()
	{
		if (!Application.isPlaying && base.gameObject.transform.localScale != Vector3.one)
		{
			base.gameObject.transform.localScale = Vector3.one;
		}
	}

	public void checkDestructible()
	{
		if (groundStyle != 0)
		{
			destrucible = false;
		}
		if (destrucible && base.gameObject.isStatic)
		{
			base.gameObject.isStatic = false;
		}
		if (!destrucible && !base.gameObject.isStatic)
		{
			base.gameObject.isStatic = false;
		}
	}

	public void checkLockY()
	{
		if (EasyHill2DManager.Instance().myLockY)
		{
			Vector3 position = base.transform.position;
			if (position.y != EasyHill2DManager.Instance().myLockYValue && !snapToParent && groundStyle == GroundStyle.STRAIGHT && GUI.changed)
			{
				Transform transform = base.gameObject.transform;
				Vector3 position2 = base.gameObject.transform.position;
				float x = position2.x;
				float myLockYValue = EasyHill2DManager.Instance().myLockYValue;
				Vector3 position3 = base.gameObject.transform.position;
				transform.position = new Vector3(x, myLockYValue, position3.z);
			}
		}
	}

	public void checkDekoOffset()
	{
		if (EasyHill2DManager.Instance().myDekoOffset && snapToParent)
		{
			Vector3 localPosition = base.gameObject.transform.localPosition;
			if (localPosition.y != EasyHill2DManager.Instance().myDekoOffsetValue)
			{
				Transform transform = base.gameObject.transform;
				float myDekoOffsetValue = EasyHill2DManager.Instance().myDekoOffsetValue;
				Vector3 localPosition2 = base.gameObject.transform.localPosition;
				transform.localPosition = new Vector3(0f, myDekoOffsetValue, localPosition2.z);
			}
		}
		if (EasyHill2DManager.Instance().myDekoOffset && snapToParent)
		{
			Vector3 localPosition3 = base.gameObject.transform.localPosition;
			if (localPosition3.x != 0f)
			{
				Transform transform2 = base.gameObject.transform;
				float myDekoOffsetValue2 = EasyHill2DManager.Instance().myDekoOffsetValue;
				Vector3 localPosition4 = base.gameObject.transform.localPosition;
				transform2.localPosition = new Vector3(0f, myDekoOffsetValue2, localPosition4.z);
			}
		}
	}

	public void makeCollider()
	{
		EdgeCollider2D edgeCollider2D = GetComponent<EdgeCollider2D>();
		if (edgeCollider2D == null)
		{
			edgeCollider2D = base.gameObject.AddComponent<EdgeCollider2D>();
		}
		Vector2[] array = new Vector2[physicsVertices.Length];
		switch (groundStyle)
		{
		}
		for (int i = 0; i < physicsVertices.Length; i++)
		{
			ref Vector2 reference = ref array[i];
			Vector2 a = physicsVertices[i];
			Vector3 position = base.transform.position;
			float x = position.x;
			Vector3 position2 = base.transform.position;
			reference = a - new Vector2(x, position2.y);
		}
		edgeCollider2D.points = array;
	}

	public void UpdateMesh()
	{
		Vector3[] array = new Vector3[physicsVertices.Length * 2];
		int num = 0;
		for (int i = 0; i < physicsVertices.Length; i++)
		{
			array[num++] = (Vector3)physicsVertices[i] - base.transform.position;
			array[num++] = (Vector3)groundVertices[i] - base.transform.position;
		}
		mesh.vertices = array;
		int[] array2 = new int[array.Length * 3];
		int num4 = 0;
		for (int j = 0; j < array.Length - 2; j += 2)
		{
			array2[num4++] = j;
			array2[num4++] = j + 3;
			array2[num4++] = j + 1;
			array2[num4++] = j;
			array2[num4++] = j + 2;
			array2[num4++] = j + 3;
		}
		mesh.triangles = array2;
		CalculateUVs();
		if (groundCollider)
		{
			makeCollider();
		}
	}

	public void CalculateUVs()
	{
		float num = GetComponent<Renderer>().sharedMaterial.mainTexture.width;
		float num2 = GetComponent<Renderer>().sharedMaterial.mainTexture.height;
		float num3 = Mathf.Abs(p4.x - p1.x);
		Vector2[] array = new Vector2[mesh.vertices.Length];
		switch (textureStyle)
		{
		case TextureStyle.STRETCH:
		{
			bool flag2 = true;
			for (int j = 0; j < mesh.vertices.Length; j++)
			{
				float y2 = (!flag2) ? (0f - textureRepeatHeight) : 0f;
				float num4 = mesh.vertices[j].x - p1.x;
				float x3 = num4 / num3 * textureRepeatWidth;
				flag2 = !flag2;
				array[j] = new Vector2(x3, y2);
			}
			break;
		}
		case TextureStyle.CONSTANT:
			textureRepeatWidth = textureRepeatHeight * (num / num2);
			for (int k = 0; k < mesh.vertices.Length; k++)
			{
				float x4 = mesh.vertices[k].x;
				Vector3 position2 = base.transform.position;
				float x5 = (x4 + position2.x) / textureRepeatWidth;
				float y3 = mesh.vertices[k].y;
				Vector3 position3 = base.transform.position;
				float y4 = (y3 + position3.y) / textureRepeatHeight;
				array[k] = new Vector2(x5, y4);
			}
			break;
		case TextureStyle.FIXED_WIDTH:
		{
			bool flag = true;
			textureRepeatWidth = segmentHeight * (num / num2);
			textureRepeatHeight = 1f;
			for (int i = 0; i < mesh.vertices.Length; i++)
			{
				float y = (!flag) ? (0f - textureRepeatHeight) : 0f;
				float x = mesh.vertices[i].x;
				Vector3 position = base.transform.position;
				float x2 = (x + position.x) * (num2 / num / segmentHeight);
				flag = !flag;
				array[i] = new Vector2(x2, y);
			}
			break;
		}
		}
		mesh.uv = array;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
	}

	public void calculateMesh()
	{
		meshFilter = base.gameObject.GetComponent<MeshFilter>();
		if (meshFilter == null)
		{
			meshFilter = base.gameObject.AddComponent<MeshFilter>();
		}
		mesh = meshFilter.sharedMesh;
		if (mesh == null)
		{
			meshFilter.mesh = new Mesh();
			mesh = meshFilter.sharedMesh;
		}
		mesh.Clear();
		physicsVertices = calculatePhysicVertices(p1, p2, p3, p4, elementNumber);
		switch (groundStyle)
		{
		case GroundStyle.BEZIER:
			groundVertices = new Vector2[physicsVertices.Length];
			for (int j = 0; j < groundVertices.Length; j++)
			{
				Vector3 a = physicsVertices[j];
				a -= new Vector3(0f, segmentHeight, 0f);
				groundVertices[j] = a;
			}
			break;
		case GroundStyle.PIPE:
			groundVertices = new Vector2[physicsVertices.Length];
			for (int k = 0; k < groundVertices.Length; k++)
			{
				Vector3 a2 = physicsVertices[k];
				if (k < groundVertices.Length - 1)
				{
					Vector3 point = (physicsVertices[k] - physicsVertices[k + 1]).normalized;
					point = Quaternion.AngleAxis(90f, Vector3.forward) * point;
					a2 += point * segmentHeight;
				}
				else
				{
					Vector3 point2 = (physicsVertices[k] - physicsVertices[k - 1]).normalized;
					point2 = Quaternion.AngleAxis(-90f, Vector3.forward) * point2;
					a2 += point2 * segmentHeight;
				}
				groundVertices[k] = a2;
			}
			break;
		case GroundStyle.STRAIGHT:
			groundVertices = new Vector2[physicsVertices.Length];
			for (int i = 0; i < groundVertices.Length; i++)
			{
				float x = physicsVertices[i].x;
				Vector3 position = base.transform.position;
				float y = position.y;
				Vector3 position2 = base.transform.position;
				Vector3 v = new Vector3(x, y, position2.z) - segmentHeight * Vector3.down;
				if (physicsVertices[i].y > v.y)
				{
					groundVertices[i] = v;
					continue;
				}
				groundVertices[i] = physicsVertices[i];
				physicsVertices[i] = v;
			}
			break;
		}
		UpdateMesh();
	}

	public void SnapChildren()
	{
		if (snapToParent)
		{
			return;
		}
		EasyHill2DNode[] componentsInChildren = base.gameObject.transform.GetComponentsInChildren<EasyHill2DNode>();
		EasyHill2DNode[] array = componentsInChildren;
		foreach (EasyHill2DNode easyHill2DNode in array)
		{
			if (easyHill2DNode.snapToParent)
			{
				easyHill2DNode.SnapToParent();
			}
		}
	}

	public void SnapToParent()
	{
		GameObject gameObject = base.gameObject;
		if (!(gameObject.transform.parent != null))
		{
			return;
		}
		GameObject gameObject2 = base.gameObject.transform.parent.gameObject;
		if (gameObject2 != null)
		{
			EasyHill2DNode component = gameObject2.GetComponent<EasyHill2DNode>();
			if (component != null)
			{
				Transform transform = base.gameObject.transform;
				Vector3 localPosition = base.gameObject.transform.localPosition;
				float y = localPosition.y;
				Vector3 localPosition2 = base.gameObject.transform.localPosition;
				transform.localPosition = new Vector3(0f, y, localPosition2.z);
				p1 = component.p1;
				p2 = component.p2;
				p3 = component.p3;
				p4 = component.p4;
				elementNumber = component.elementNumber;
				calculateMesh();
			}
		}
	}

	public void checkColliderEnabled()
	{
		if (base.gameObject.GetComponent<EdgeCollider2D>() != null && !groundCollider)
		{
			UnityEngine.Object.DestroyImmediate(base.gameObject.GetComponent<EdgeCollider2D>());
		}
	}

	private void OnDrawGizmos()
	{
	}

	private void drawGizmoLines()
	{
		Gizmos.color = Color.blue;
		for (int i = 0; i < physicsVertices.Length - 1; i++)
		{
			Gizmos.DrawLine(physicsVertices[i], physicsVertices[i + 1]);
		}
		Gizmos.color = Color.cyan;
		for (int j = 0; j < groundVertices.Length - 1; j++)
		{
			Gizmos.DrawLine(groundVertices[j], groundVertices[j + 1]);
		}
	}

	private void OnDrawGizmosSelected()
	{
	}

	private Vector2[] calculatePhysicVertices(Vector2 pP1, Vector2 pP2, Vector2 pP3, Vector2 pP4, int pElementNumber)
	{
		Vector2 a = pP1;
		Vector3 position = base.transform.position;
		float x = position.x;
		Vector3 position2 = base.transform.position;
		pP1 = a + new Vector2(x, position2.y);
		Vector2 a2 = pP2;
		Vector3 position3 = base.transform.position;
		float x2 = position3.x;
		Vector3 position4 = base.transform.position;
		pP2 = a2 + new Vector2(x2, position4.y);
		Vector2 a3 = pP3;
		Vector3 position5 = base.transform.position;
		float x3 = position5.x;
		Vector3 position6 = base.transform.position;
		pP3 = a3 + new Vector2(x3, position6.y);
		Vector2 a4 = pP4;
		Vector3 position7 = base.transform.position;
		float x4 = position7.x;
		Vector3 position8 = base.transform.position;
		pP4 = a4 + new Vector2(x4, position8.y);
		Vector2[] array = new Vector2[pElementNumber + 2];
		float num = Mathf.Abs(pP4.x - pP1.x);
		float num2 = num / (float)pElementNumber;
		Vector2 vector = pP1;
		for (int i = 0; i <= pElementNumber + 1; i++)
		{
			array[i] = vector;
			vector = getBezierCoordinates(pP1, pP2, pP3, pP4, (float)i * num2 / num);
		}
		return array;
	}

	private Vector2[] CombineVector2Arrays(Vector2[] array1, Vector2[] array2)
	{
		Vector2[] array3 = new Vector2[array1.Length + array2.Length];
		Array.Copy(array1, array3, array1.Length);
		Array.Copy(array2, 0, array3, array1.Length, array2.Length);
		return array3;
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
}
