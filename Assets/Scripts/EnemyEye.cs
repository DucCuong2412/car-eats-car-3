using System;
using System.Collections;
using UnityEngine;

public class EnemyEye : MonoBehaviour
{
	public class MyRect
	{
		public float xMin;

		public float xMax;

		public float yMin;

		public float yMax;

		public Vector3 a = Vector3.zero;

		public Vector3 b = Vector3.zero;

		public Vector3 c = Vector3.zero;

		public Vector3 d = Vector3.zero;

		public MyRect(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
		{
			xMin = a.x;
			xMax = b.x;
			yMin = a.y;
			yMax = d.y;
			this.a = a;
			this.b = b;
			this.c = c;
			this.d = d;
		}

		public void RoundAll()
		{
			a = new Vector3((float)Math.Round(a.x, 2), (float)Math.Round(a.y, 2));
			b = new Vector3((float)Math.Round(b.x, 2), (float)Math.Round(b.y, 2));
			c = new Vector3((float)Math.Round(c.x, 2), (float)Math.Round(c.y, 2));
			d = new Vector3((float)Math.Round(d.x, 2), (float)Math.Round(d.y, 2));
		}

		public bool ContainsPoint(Vector3 point)
		{
			bool result = false;
			float num = AreaOfTriangle(a, point, d);
			float num2 = AreaOfTriangle(d, point, c);
			float num3 = AreaOfTriangle(c, point, b);
			float num4 = AreaOfTriangle(point, b, a);
			float magnitude = (b - a).magnitude;
			float magnitude2 = (d - a).magnitude;
			float num5 = magnitude * magnitude2;
			if (Math.Abs(num + num2 + num3 + num4 - num5) < 0.0001f)
			{
				result = true;
			}
			if (Mathf.Abs(num) < 0.1f || Mathf.Abs(num2) < 0.1f || Mathf.Abs(num3) < 0.1f || Mathf.Abs(num4) < 0.1f)
			{
				result = true;
			}
			return result;
		}

		public float AreaOfTriangle(Vector3 a, Vector3 b, Vector3 c)
		{
			float num = Vector3.Distance(a, b);
			float num2 = Vector3.Distance(b, c);
			float num3 = Vector3.Distance(c, a);
			float num4 = (num + num2 + num3) / 2f;
			return (float)Math.Sqrt(num4 * (num4 - num) * (num4 - num2) * (num4 - num3));
		}
	}

	public float maxLength = 1f;

	[Range(0f, 1f)]
	public float minPercent;

	[Range(0f, 1f)]
	public float maxPercent = 1f;

	public SpriteRenderer rectSpriteRenderer;

	public Transform eye;

	public Transform target;

	private bool isInited;

	private Rect rect;

	private MyRect rct;

	private void OnEnable()
	{
	}

	private IEnumerator WaitForTarget()
	{
		if (!(target != null))
		{
			while (!RaceLogic.instance || !RaceLogic.instance.car)
			{
				yield return 0;
			}
			if (!(target != null))
			{
				target = RaceLogic.instance.car.transform;
			}
		}
	}

	public void Init(Transform target)
	{
		this.target = target;
	}

	private void Update()
	{
		if (!(target == null) && !(eye == null) && !(rectSpriteRenderer == null))
		{
			Bounds bounds = rectSpriteRenderer.sprite.bounds;
			Vector3 vector = target.position - base.transform.position;
			Vector3 position = base.transform.position;
			float x = position.x;
			Vector3 min = bounds.min;
			float x2 = min.x;
			Vector3 lossyScale = base.transform.lossyScale;
			float x3 = x + x2 * lossyScale.x;
			Vector3 position2 = base.transform.position;
			float x4 = position2.x;
			Vector3 max = bounds.max;
			float x5 = max.x;
			Vector3 lossyScale2 = base.transform.lossyScale;
			float x6 = x4 + x5 * lossyScale2.x;
			Vector3 position3 = base.transform.position;
			float x7 = position3.x;
			Vector3 max2 = bounds.max;
			float x8 = max2.x;
			Vector3 lossyScale3 = base.transform.lossyScale;
			float x9 = x7 + x8 * lossyScale3.x;
			Vector3 position4 = base.transform.position;
			float x10 = position4.x;
			Vector3 min2 = bounds.min;
			float x11 = min2.x;
			Vector3 lossyScale4 = base.transform.lossyScale;
			float x12 = x10 + x11 * lossyScale4.x;
			Vector3 position5 = base.transform.position;
			float y = position5.y;
			Vector3 min3 = bounds.min;
			float y2 = min3.y;
			Vector3 lossyScale5 = base.transform.lossyScale;
			float y3 = y + y2 * lossyScale5.y;
			Vector3 position6 = base.transform.position;
			float y4 = position6.y;
			Vector3 min4 = bounds.min;
			float y5 = min4.y;
			Vector3 lossyScale6 = base.transform.lossyScale;
			float y6 = y4 + y5 * lossyScale6.y;
			Vector3 position7 = base.transform.position;
			float y7 = position7.y;
			Vector3 max3 = bounds.max;
			float y8 = max3.y;
			Vector3 lossyScale7 = base.transform.lossyScale;
			float y9 = y7 + y8 * lossyScale7.y;
			Vector3 position8 = base.transform.position;
			float y10 = position8.y;
			Vector3 max4 = bounds.max;
			float y11 = max4.y;
			Vector3 lossyScale8 = base.transform.lossyScale;
			float y12 = y10 + y11 * lossyScale8.y;
			Vector3 eulerAngles = base.transform.rotation.eulerAngles;
			float angle = eulerAngles.z * ((float)Math.PI / 180f);
			Vector3 vector2 = RotatePoint(new Vector3(x3, y3), angle);
			Vector3 vector3 = RotatePoint(new Vector3(x6, y6), angle);
			Vector3 vector4 = RotatePoint(new Vector3(x9, y9), angle);
			Vector3 vector5 = RotatePoint(new Vector3(x12, y12), angle);
			float x13 = vector2.x;
			float x14 = vector3.x;
			float x15 = vector4.x;
			float x16 = vector5.x;
			float y13 = vector2.y;
			float y14 = vector3.y;
			float y15 = vector4.y;
			float y16 = vector5.y;
			rct = new MyRect(new Vector3(x13, y13), new Vector3(x14, y14), new Vector3(x15, y15), new Vector3(x16, y16));
			Vector3 lineIntersectsRect = GetLineIntersectsRect(base.transform.position, target.transform.position, rct);
			if (lineIntersectsRect == Vector3.zero)
			{
				eye.position = base.transform.position;
				return;
			}
			float magnitude = vector.magnitude;
			float value = magnitude / maxLength;
			value = Mathf.Clamp(value, minPercent, maxPercent);
			Vector3 position9 = Vector3.Lerp(base.transform.position, lineIntersectsRect, value);
			eye.position = position9;
		}
	}

	public Vector3 RotatePoint(Vector3 point, float angle)
	{
		Vector3 position = base.transform.position;
		point -= position;
		float x = (float)((double)(point.x * Mathf.Cos(angle)) - (double)point.y * Math.Sin(angle));
		float y = (float)((double)(point.x * Mathf.Sin(angle)) + (double)point.y * Math.Cos(angle));
		return new Vector3(x, y) + position;
	}

	public void OnDrawGizmos()
	{
	}

	public Vector3 GetLineIntersectsRect(Vector3 p1, Vector3 p2, MyRect rect)
	{
		Vector3 lineIntersectsLine = GetLineIntersectsLine(p1, p2, rect.a, rect.b);
		Vector3 lineIntersectsLine2 = GetLineIntersectsLine(p1, p2, rect.b, rect.c);
		Vector3 lineIntersectsLine3 = GetLineIntersectsLine(p1, p2, rect.c, rect.d);
		Vector3 lineIntersectsLine4 = GetLineIntersectsLine(p1, p2, rect.d, rect.a);
		return (lineIntersectsLine != Vector3.zero) ? lineIntersectsLine : ((lineIntersectsLine2 != Vector3.zero) ? lineIntersectsLine2 : ((lineIntersectsLine3 != Vector3.zero) ? lineIntersectsLine3 : ((!(lineIntersectsLine4 != Vector3.zero)) ? Vector3.zero : lineIntersectsLine4)));
	}

	public Vector3 GetLineIntersectsLine(Vector3 ps1, Vector3 pe1, Vector3 ps2, Vector3 pe2)
	{
		float num = pe1.y - ps1.y;
		float num2 = ps1.x - pe1.x;
		float num3 = num * ps1.x + num2 * ps1.y;
		float num4 = pe2.y - ps2.y;
		float num5 = ps2.x - pe2.x;
		float num6 = num4 * ps2.x + num5 * ps2.y;
		float num7 = num * num5 - num4 * num2;
		Vector3 vector = new Vector3((num5 * num3 - num2 * num6) / num7, (num * num6 - num4 * num3) / num7);
		rct.RoundAll();
		vector = new Vector3((float)Math.Round(vector.x, 2), (float)Math.Round(vector.y, 2));
		if (!rct.ContainsPoint(vector))
		{
			return Vector3.zero;
		}
		if (!PointOnLineSegment(base.transform.position, target.transform.position, vector))
		{
			return Vector3.zero;
		}
		float x = (ps2.x + pe2.x) / 2f;
		float y = (ps2.y + pe2.y) / 2f;
		Vector3 a = new Vector3(x, y);
		float num8 = Vector3.Distance(a, pe2);
		float num9 = Vector3.Distance(a, vector);
		if (num8 >= num9)
		{
			return vector;
		}
		return Vector3.zero;
	}

	public static bool PointOnLineSegment(Vector3 pt1, Vector3 pt2, Vector3 pt, double epsilon = 0.01)
	{
		if ((double)(pt.x - Math.Max(pt1.x, pt2.x)) > epsilon || (double)(Math.Min(pt1.x, pt2.x) - pt.x) > epsilon || (double)(pt.y - Math.Max(pt1.y, pt2.y)) > epsilon || (double)(Math.Min(pt1.y, pt2.y) - pt.y) > epsilon)
		{
			return false;
		}
		if ((double)Math.Abs(pt2.x - pt1.x) < epsilon)
		{
			return (double)Math.Abs(pt1.x - pt.x) < epsilon || (double)Math.Abs(pt2.x - pt.x) < epsilon;
		}
		if ((double)Math.Abs(pt2.y - pt1.y) < epsilon)
		{
			return (double)Math.Abs(pt1.y - pt.y) < epsilon || (double)Math.Abs(pt2.y - pt.y) < epsilon;
		}
		double value = pt1.x + (pt.y - pt1.y) * (pt2.x - pt1.x) / (pt2.y - pt1.y);
		double value2 = pt1.y + (pt.x - pt1.x) * (pt2.y - pt1.y) / (pt2.x - pt1.x);
		value = Math.Round(value, 4);
		value2 = Math.Round(value2, 4);
		return Math.Abs((double)pt.x - value) < epsilon || Math.Abs((double)pt.y - value2) < epsilon;
	}

	public bool IsOnLine(Vector3 endPoint1, Vector3 endPoint2, Vector3 checkPoint)
	{
		float num = (checkPoint.y - endPoint1.y) / (checkPoint.x - endPoint1.x);
		float num2 = (endPoint2.y - endPoint1.y) / (endPoint2.x - endPoint1.x);
		return num == num2;
	}
}
