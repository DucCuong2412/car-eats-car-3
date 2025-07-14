using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortuneBase : MonoBehaviour
{
	public delegate void SpinEnded(int sector);

	public delegate void SpinStarted();

	public SpinEnded OnSpinEnded;

	public SpinStarted OnSpinStarted;

	public float minTouchRadius = 0.1f;

	public float maxTouchRadius = 1f;

	public Camera wheelCamera;

	public AnimationCurve SpinCurve;

	public Transform wheelTransform;

	public float ZeroSectorAngle;

	public float ignoreMoveLessThan = 1f;

	[Range(0f, 0.9f)]
	public float omission;

	[HideInInspector]
	public List<int> weight = new List<int>();

	[Header("Loc1")]
	public List<int> weight1 = new List<int>();

	[Header("Loc2")]
	public List<int> weight2 = new List<int>();

	[Header("Loc3")]
	public List<int> weight3 = new List<int>();

	[Header("Loc4")]
	public List<int> weight4 = new List<int>();

	[Header("Prizes")]
	public List<int> Prizes = new List<int>();

	public float MultiplerLoc1 = 1f;

	public float MultiplerLoc2 = 1.5f;

	public float MultiplerLoc3 = 2f;

	public float MultiplerLoc4 = 2.5f;

	[Space(15f)]
	public bool spinning;

	public bool forME;

	public bool canSpin = true;

	private bool draging;

	private float pSpeed = 1f;

	private float tempAngle;

	private float LEXtime;

	public bool isSpinning => spinning;

	public bool isDraging => draging;

	public float Speed => pSpeed;

	private float CurrentAngle
	{
		get
		{
			Vector3 eulerAngles = wheelTransform.eulerAngles;
			return eulerAngles.z;
		}
		set
		{
			wheelTransform.eulerAngles = Vector3.forward * value;
		}
	}

	public virtual void Update()
	{
		if (wheelTransform == null)
		{
			UnityEngine.Debug.LogWarning("Add wheel transform to script.");
			return;
		}
		if (wheelCamera == null)
		{
			UnityEngine.Debug.LogWarning("Add wheel camera to script.");
			return;
		}
		if (weight.Count == 0)
		{
			weight.Add(1);
		}
		if (!spinning && canSpin)
		{
			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
			{
				CheckMouseTouch();
			}
			else
			{
				CheckScreenTouch();
			}
		}
	}

	private void CheckMouseTouch()
	{
		if (Input.GetMouseButtonUp(0) && draging)
		{
			draging = false;
			TouchEnd(UnityEngine.Input.mousePosition);
		}
		if (Input.GetMouseButtonDown(0))
		{
			if (!draging)
			{
				TouchBegin(UnityEngine.Input.mousePosition);
			}
		}
		else if (draging)
		{
			TouchMove(UnityEngine.Input.mousePosition);
		}
	}

	private void CheckScreenTouch()
	{
		if (UnityEngine.Input.touchCount != 1)
		{
			return;
		}
		if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Ended && draging)
		{
			draging = false;
			TouchEnd(UnityEngine.Input.GetTouch(0).position);
		}
		if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began)
		{
			if (!draging)
			{
				TouchBegin(UnityEngine.Input.GetTouch(0).position);
			}
		}
		else if (draging)
		{
			TouchMove(UnityEngine.Input.mousePosition);
		}
	}

	private bool touchOnWheel(Vector3 screenPoint)
	{
		Vector3 a = wheelCamera.ScreenToWorldPoint(screenPoint);
		float num = Vector3.Distance(a, wheelTransform.position);
		if (num >= minTouchRadius && num <= maxTouchRadius)
		{
			return true;
		}
		return false;
	}

	private void TouchBegin(Vector3 position)
	{
		if (touchOnWheel(position))
		{
			draging = true;
			tempAngle = getAngle();
		}
	}

	private void TouchMove(Vector3 position)
	{
		if (!touchOnWheel(position))
		{
			TouchEnd(position);
			return;
		}
		float angle = getAngle();
		CurrentAngle += angle - tempAngle;
		tempAngle = angle;
	}

	private void TouchEnd(Vector3 position)
	{
		if (canSpin && !spinning)
		{
			draging = false;
			int randomSector = getRandomSector();
			float angle = getAngle();
			float num = angle - tempAngle;
			if (!(Mathf.Abs(num) < ignoreMoveLessThan))
			{
				float num2 = 5f;
				float num3 = ZeroSectorAngle - CurrentAngle + 360f * ((!(num < 0f)) ? num2 : (0f - num2)) + 360f / (float)weight.Count * (float)randomSector;
				num3 += UnityEngine.Random.Range(0f - omission, omission) * 180f / (float)weight.Count;
				StartCoroutine(DoSpin(num2, num3, SpinEnd, randomSector));
			}
		}
	}

	public void StopWheel()
	{
		LEXtime = 0.5f;
	}

	private IEnumerator DoSpin(float time, float maxAngle, Action<int> action = null, int sector = 0)
	{
		float timer2 = 0f;
		float startAngle = CurrentAngle;
		float prevAngle = startAngle;
		pSpeed = prevAngle - SpinCurve.Evaluate(0f) * maxAngle;
		LEXtime = time;
		spinning = true;
		forME = true;
		if (OnSpinStarted != null)
		{
			OnSpinStarted();
		}
		while (spinning)
		{
			yield return null;
			float angle = timer2 / time * maxAngle * 1.5f;
			pSpeed = prevAngle - angle;
			prevAngle = angle;
			CurrentAngle = (angle + startAngle) % 360f;
			timer2 += Time.unscaledDeltaTime;
		}
		canSpin = false;
		timer2 = 3f;
		while (timer2 < time)
		{
			float angle2 = SpinCurve.Evaluate(timer2 / time) * maxAngle;
			pSpeed = prevAngle - angle2;
			prevAngle = angle2;
			CurrentAngle = (angle2 + startAngle) % 360f;
			timer2 += Time.unscaledDeltaTime;
			yield return null;
		}
		action?.Invoke(sector);
		spinning = false;
		forME = false;
	}

	public void DoSpin()
	{
		if (canSpin && !spinning)
		{
			int randomSector = getRandomSector();
			float angle = getAngle();
			float num = angle - tempAngle;
			if (!(Mathf.Abs(num) < ignoreMoveLessThan))
			{
				UnityEngine.Debug.Log("Fortune time!");
				float num2 = 5f;
				float num3 = ZeroSectorAngle - CurrentAngle + 360f * ((!(num < 0f)) ? num2 : (0f - num2)) + 360f / (float)weight.Count * (float)randomSector;
				num3 += UnityEngine.Random.Range(0f - omission, omission) * 180f / (float)weight.Count;
				UnityEngine.Debug.Log("sector " + randomSector + " | ZeroSectorAngle " + ZeroSectorAngle + " | CurrentAngle " + CurrentAngle + " | weight.Count " + weight.Count);
				StartCoroutine(DoSpin(num2, num3, SpinEnd, randomSector));
			}
		}
	}

	private void SpinEnd(int sectorNum)
	{
		if (OnSpinEnded != null)
		{
			OnSpinEnded(sectorNum);
		}
	}

	private float totalWeight()
	{
		float num = 0f;
		for (int i = 0; i < weight.Count; i++)
		{
			num += (float)weight[i];
		}
		return (!(num > 0f)) ? (-1f) : num;
	}

	public virtual int getRandomSector()
	{
		int num = UnityEngine.Random.Range(0, 101);
		UnityEngine.Debug.Log("perc " + num);
		int num2 = 0;
		for (int i = 0; i < weight.Count; i++)
		{
			if (num >= num2 && num < num2 + weight[i])
			{
				UnityEngine.Debug.Log("sector " + i);
				return i;
			}
			num2 += weight[i];
		}
		UnityEngine.Debug.Log("ELSE  sector " + (weight.Count - 1));
		return weight.Count - 1;
	}

	private float getAngle()
	{
		if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
		{
			Vector3 mousePosition = UnityEngine.Input.mousePosition;
			float x = mousePosition.x;
			Vector3 mousePosition2 = UnityEngine.Input.mousePosition;
			Vector2 a = new Vector2(x, mousePosition2.y);
			Vector3 vector = wheelCamera.WorldToScreenPoint(wheelTransform.position);
			float x2 = vector.x;
			Vector3 vector2 = wheelCamera.WorldToScreenPoint(wheelTransform.position);
			Vector2 b = new Vector2(x2, vector2.y);
			return Vector2.Angle(Vector2.right, a - b) * (float)((!(b.y > a.y)) ? 1 : (-1));
		}
		Vector2 position = Input.touches[0].position;
		position.y = (float)Screen.height - position.y;
		Vector3 vector3 = wheelCamera.WorldToScreenPoint(wheelTransform.position);
		float x3 = vector3.x;
		Vector3 vector4 = wheelCamera.WorldToScreenPoint(wheelTransform.position);
		Vector2 b2 = new Vector2(x3, vector4.y);
		return (0f - Vector2.Angle(Vector2.right, position - b2)) * (float)((!(b2.y > position.y)) ? 1 : (-1));
	}
}
