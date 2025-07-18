using UnityEngine;

[AddComponentMenu("2D Toolkit/Sprite/tk2dTiledSprite")]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class tk2dTiledSprite : tk2dBaseSprite
{
	private Mesh mesh;

	private Vector2[] meshUvs;

	private Vector3[] meshVertices;

	private Color32[] meshColors;

	private Vector3[] meshNormals;

	private Vector4[] meshTangents;

	private int[] meshIndices;

	[SerializeField]
	private Vector2 _dimensions = new Vector2(50f, 50f);

	[SerializeField]
	private Anchor _anchor;

	[SerializeField]
	protected bool _createBoxCollider;

	private Vector3 boundsCenter = Vector3.zero;

	private Vector3 boundsExtents = Vector3.zero;

	public Vector2 dimensions
	{
		get
		{
			return _dimensions;
		}
		set
		{
			if (value != _dimensions)
			{
				_dimensions = value;
				UpdateVertices();
				UpdateCollider();
			}
		}
	}

	public Anchor anchor
	{
		get
		{
			return _anchor;
		}
		set
		{
			if (value != _anchor)
			{
				_anchor = value;
				UpdateVertices();
				UpdateCollider();
			}
		}
	}

	public bool CreateBoxCollider
	{
		get
		{
			return _createBoxCollider;
		}
		set
		{
			if (_createBoxCollider != value)
			{
				_createBoxCollider = value;
				UpdateCollider();
			}
		}
	}

	private new void Awake()
	{
		base.Awake();
		mesh = new Mesh();
		mesh.hideFlags = HideFlags.DontSave;
		GetComponent<MeshFilter>().mesh = mesh;
		if ((bool)base.Collection)
		{
			if (_spriteId < 0 || _spriteId >= base.Collection.Count)
			{
				_spriteId = 0;
			}
			Build();
			if (boxCollider == null)
			{
				boxCollider = GetComponent<BoxCollider>();
			}
			if (boxCollider2D == null)
			{
				boxCollider2D = GetComponent<BoxCollider2D>();
			}
		}
	}

	protected void OnDestroy()
	{
		if ((bool)mesh)
		{
			UnityEngine.Object.Destroy(mesh);
		}
	}

	protected new void SetColors(Color32[] dest)
	{
		tk2dSpriteGeomGen.GetTiledSpriteGeomDesc(out int numVertices, out int _, base.CurrentSprite, dimensions);
		tk2dSpriteGeomGen.SetSpriteColors(dest, 0, numVertices, _color, collectionInst.premultipliedAlpha);
	}

	public override void Build()
	{
		tk2dSpriteDefinition currentSprite = base.CurrentSprite;
		tk2dSpriteGeomGen.GetTiledSpriteGeomDesc(out int numVertices, out int numIndices, currentSprite, this.dimensions);
		if (meshUvs == null || meshUvs.Length != numVertices)
		{
			meshUvs = new Vector2[numVertices];
			meshVertices = new Vector3[numVertices];
			meshColors = new Color32[numVertices];
		}
		if (meshIndices == null || meshIndices.Length != numIndices)
		{
			meshIndices = new int[numIndices];
		}
		meshNormals = new Vector3[0];
		meshTangents = new Vector4[0];
		if (currentSprite.normals != null && currentSprite.normals.Length > 0)
		{
			meshNormals = new Vector3[numVertices];
		}
		if (currentSprite.tangents != null && currentSprite.tangents.Length > 0)
		{
			meshTangents = new Vector4[numVertices];
		}
		float num;
		if (boxCollider != null)
		{
			Vector3 center = boxCollider.center;
			num = center.z;
		}
		else
		{
			num = 0f;
		}
		float colliderOffsetZ = num;
		float num2;
		if (boxCollider != null)
		{
			Vector3 size = boxCollider.size;
			num2 = size.z * 0.5f;
		}
		else
		{
			num2 = 0.5f;
		}
		float colliderExtentZ = num2;
		tk2dSpriteGeomGen.SetTiledSpriteGeom(meshVertices, meshUvs, 0, out boundsCenter, out boundsExtents, currentSprite, _scale, this.dimensions, anchor, colliderOffsetZ, colliderExtentZ);
		tk2dSpriteGeomGen.SetTiledSpriteIndices(meshIndices, 0, 0, currentSprite, this.dimensions);
		if (meshNormals.Length > 0 || meshTangents.Length > 0)
		{
			float x = currentSprite.positions[0].x;
			Vector2 dimensions = this.dimensions;
			float num3 = x * dimensions.x * currentSprite.texelSize.x;
			Vector3 scale = base.scale;
			float x2 = num3 * scale.x;
			float y = currentSprite.positions[0].y;
			Vector2 dimensions2 = this.dimensions;
			float num4 = y * dimensions2.y * currentSprite.texelSize.y;
			Vector3 scale2 = base.scale;
			Vector3 pMin = new Vector3(x2, num4 * scale2.y);
			float x3 = currentSprite.positions[3].x;
			Vector2 dimensions3 = this.dimensions;
			float num5 = x3 * dimensions3.x * currentSprite.texelSize.x;
			Vector3 scale3 = base.scale;
			float x4 = num5 * scale3.x;
			float y2 = currentSprite.positions[3].y;
			Vector2 dimensions4 = this.dimensions;
			float num6 = y2 * dimensions4.y * currentSprite.texelSize.y;
			Vector3 scale4 = base.scale;
			Vector3 pMax = new Vector3(x4, num6 * scale4.y);
			tk2dSpriteGeomGen.SetSpriteVertexNormals(meshVertices, pMin, pMax, currentSprite.normals, currentSprite.tangents, meshNormals, meshTangents);
		}
		SetColors(meshColors);
		if (mesh == null)
		{
			mesh = new Mesh();
			mesh.hideFlags = HideFlags.DontSave;
		}
		else
		{
			mesh.Clear();
		}
		mesh.vertices = meshVertices;
		mesh.colors32 = meshColors;
		mesh.uv = meshUvs;
		mesh.normals = meshNormals;
		mesh.tangents = meshTangents;
		mesh.triangles = meshIndices;
		mesh.RecalculateBounds();
		mesh.bounds = tk2dBaseSprite.AdjustedMeshBounds(mesh.bounds, renderLayer);
		GetComponent<MeshFilter>().mesh = mesh;
		UpdateCollider();
		UpdateMaterial();
	}

	protected override void UpdateGeometry()
	{
		UpdateGeometryImpl();
	}

	protected override void UpdateColors()
	{
		UpdateColorsImpl();
	}

	protected override void UpdateVertices()
	{
		UpdateGeometryImpl();
	}

	protected void UpdateColorsImpl()
	{
		if (meshColors == null || meshColors.Length == 0)
		{
			Build();
			return;
		}
		SetColors(meshColors);
		mesh.colors32 = meshColors;
	}

	protected void UpdateGeometryImpl()
	{
		Build();
	}

	protected override void UpdateCollider()
	{
		if (!CreateBoxCollider)
		{
			return;
		}
		if (base.CurrentSprite.physicsEngine == tk2dSpriteDefinition.PhysicsEngine.Physics3D)
		{
			if (boxCollider != null)
			{
				boxCollider.size = 2f * boundsExtents;
				boxCollider.center = boundsCenter;
			}
		}
		else if (base.CurrentSprite.physicsEngine == tk2dSpriteDefinition.PhysicsEngine.Physics2D && boxCollider2D != null)
		{
			boxCollider2D.size = 2f * boundsExtents;
			boxCollider2D.offset = boundsCenter;
		}
	}

	protected override void CreateCollider()
	{
		UpdateCollider();
	}

	protected override void UpdateMaterial()
	{
		if (GetComponent<Renderer>().sharedMaterial != collectionInst.spriteDefinitions[base.spriteId].materialInst)
		{
			GetComponent<Renderer>().material = collectionInst.spriteDefinitions[base.spriteId].materialInst;
		}
	}

	protected override int GetCurrentVertexCount()
	{
		return 16;
	}

	public override void ReshapeBounds(Vector3 dMin, Vector3 dMax)
	{
		float num = 0.1f;
		tk2dSpriteDefinition currentSprite = base.CurrentSprite;
		Vector2 vector = new Vector2(_dimensions.x * currentSprite.texelSize.x, _dimensions.y * currentSprite.texelSize.y);
		Vector3 vector2 = new Vector3(vector.x * _scale.x, vector.y * _scale.y);
		Vector3 zero = Vector3.zero;
		switch (_anchor)
		{
		case Anchor.LowerLeft:
			zero.Set(0f, 0f, 0f);
			break;
		case Anchor.LowerCenter:
			zero.Set(0.5f, 0f, 0f);
			break;
		case Anchor.LowerRight:
			zero.Set(1f, 0f, 0f);
			break;
		case Anchor.MiddleLeft:
			zero.Set(0f, 0.5f, 0f);
			break;
		case Anchor.MiddleCenter:
			zero.Set(0.5f, 0.5f, 0f);
			break;
		case Anchor.MiddleRight:
			zero.Set(1f, 0.5f, 0f);
			break;
		case Anchor.UpperLeft:
			zero.Set(0f, 1f, 0f);
			break;
		case Anchor.UpperCenter:
			zero.Set(0.5f, 1f, 0f);
			break;
		case Anchor.UpperRight:
			zero.Set(1f, 1f, 0f);
			break;
		}
		zero = Vector3.Scale(zero, vector2) * -1f;
		Vector3 vector3 = vector2 + dMax - dMin;
		vector3.x /= vector.x;
		vector3.y /= vector.y;
		if (Mathf.Abs(vector.x * vector3.x) < currentSprite.texelSize.x * num && Mathf.Abs(vector3.x) < Mathf.Abs(_scale.x))
		{
			dMin.x = 0f;
			vector3.x = _scale.x;
		}
		if (Mathf.Abs(vector.y * vector3.y) < currentSprite.texelSize.y * num && Mathf.Abs(vector3.y) < Mathf.Abs(_scale.y))
		{
			dMin.y = 0f;
			vector3.y = _scale.y;
		}
		Vector2 vector4 = new Vector3((!Mathf.Approximately(_scale.x, 0f)) ? (vector3.x / _scale.x) : 0f, (!Mathf.Approximately(_scale.y, 0f)) ? (vector3.y / _scale.y) : 0f);
		Vector3 b = new Vector3(zero.x * vector4.x, zero.y * vector4.y);
		Vector3 position = dMin + zero - b;
		position.z = 0f;
		base.transform.position = base.transform.TransformPoint(position);
		dimensions = new Vector2(_dimensions.x * vector4.x, _dimensions.y * vector4.y);
	}
}
