using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/Core/tk2dUILayout")]
public class tk2dUILayout : MonoBehaviour
{
	public Vector3 bMin = new Vector3(0f, -1f, 0f);

	public Vector3 bMax = new Vector3(1f, 0f, 0f);

	public List<tk2dUILayoutItem> layoutItems = new List<tk2dUILayoutItem>();

	public bool autoResizeCollider;

	public int ItemCount => layoutItems.Count;

	public event Action<Vector3, Vector3> OnReshape;

	private void Reset()
	{
		if (!(GetComponent<Collider>() != null))
		{
			return;
		}
		BoxCollider boxCollider = GetComponent<Collider>() as BoxCollider;
		if (boxCollider != null)
		{
			Bounds bounds = boxCollider.bounds;
			Matrix4x4 worldToLocalMatrix = base.transform.worldToLocalMatrix;
			Vector3 position = base.transform.position;
			Reshape(worldToLocalMatrix.MultiplyPoint(bounds.min) - bMin, worldToLocalMatrix.MultiplyPoint(bounds.max) - bMax, updateChildren: true);
			Vector3 vector = worldToLocalMatrix.MultiplyVector(base.transform.position - position);
			Transform transform = base.transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				Vector3 vector2 = child.localPosition -= vector;
			}
			boxCollider.center -= vector;
			autoResizeCollider = true;
		}
	}

	public virtual void Reshape(Vector3 dMin, Vector3 dMax, bool updateChildren)
	{
		foreach (tk2dUILayoutItem layoutItem in layoutItems)
		{
			layoutItem.oldPos = layoutItem.gameObj.transform.position;
		}
		bMin += dMin;
		bMax += dMax;
		Vector3 vector = new Vector3(bMin.x, bMax.y);
		base.transform.position += base.transform.localToWorldMatrix.MultiplyVector(vector);
		bMin -= vector;
		bMax -= vector;
		if (autoResizeCollider)
		{
			BoxCollider component = GetComponent<BoxCollider>();
			if (component != null)
			{
				component.center += (dMin + dMax) / 2f - vector;
				component.size += dMax - dMin;
			}
		}
		foreach (tk2dUILayoutItem layoutItem2 in layoutItems)
		{
			Vector3 a = base.transform.worldToLocalMatrix.MultiplyVector(layoutItem2.gameObj.transform.position - layoutItem2.oldPos);
			Vector3 vector2 = -a;
			Vector3 vector3 = -a;
			if (updateChildren)
			{
				vector2.x += (layoutItem2.snapLeft ? dMin.x : ((!layoutItem2.snapRight) ? 0f : dMax.x));
				vector2.y += (layoutItem2.snapBottom ? dMin.y : ((!layoutItem2.snapTop) ? 0f : dMax.y));
				vector3.x += (layoutItem2.snapRight ? dMax.x : ((!layoutItem2.snapLeft) ? 0f : dMin.x));
				vector3.y += (layoutItem2.snapTop ? dMax.y : ((!layoutItem2.snapBottom) ? 0f : dMin.y));
			}
			if (layoutItem2.sprite != null || layoutItem2.UIMask != null || layoutItem2.layout != null)
			{
				Matrix4x4 matrix4x = base.transform.localToWorldMatrix * layoutItem2.gameObj.transform.worldToLocalMatrix;
				vector2 = matrix4x.MultiplyVector(vector2);
				vector3 = matrix4x.MultiplyVector(vector3);
			}
			if (layoutItem2.sprite != null)
			{
				layoutItem2.sprite.ReshapeBounds(vector2, vector3);
			}
			else if (layoutItem2.UIMask != null)
			{
				layoutItem2.UIMask.ReshapeBounds(vector2, vector3);
			}
			else if (layoutItem2.layout != null)
			{
				layoutItem2.layout.Reshape(vector2, vector3, updateChildren: true);
			}
			else
			{
				Vector3 vector4 = vector2;
				if (layoutItem2.snapLeft && layoutItem2.snapRight)
				{
					vector4.x = 0.5f * (vector2.x + vector3.x);
				}
				if (layoutItem2.snapTop && layoutItem2.snapBottom)
				{
					vector4.y = 0.5f * (vector2.y + vector3.y);
				}
				layoutItem2.gameObj.transform.position += vector4;
			}
		}
		if (this.OnReshape != null)
		{
			this.OnReshape(dMin, dMax);
		}
	}

	public void SetBounds(Vector3 pMin, Vector3 pMax)
	{
		Matrix4x4 worldToLocalMatrix = base.transform.worldToLocalMatrix;
		Reshape(worldToLocalMatrix.MultiplyPoint(pMin) - bMin, worldToLocalMatrix.MultiplyPoint(pMax) - bMax, updateChildren: true);
	}

	public Vector3 GetMinBounds()
	{
		return base.transform.localToWorldMatrix.MultiplyPoint(bMin);
	}

	public Vector3 GetMaxBounds()
	{
		return base.transform.localToWorldMatrix.MultiplyPoint(bMax);
	}

	public void Refresh()
	{
		Reshape(Vector3.zero, Vector3.zero, updateChildren: true);
	}
}
