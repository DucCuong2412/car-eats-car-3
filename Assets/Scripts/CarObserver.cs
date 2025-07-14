using System.Collections;
using UnityEngine;

public class CarObserver : MonoBehaviour
{
	public int NitroForBackFlip = 30;

	public int NitroForFrontFlip = 60;

	private sbyte _flipCountWithDir;

	private bool CheckFlip;

	private float rotAccum;

	private float CollCountDownTimer;

	public GameObject Gun1;

	public GameObject Gun2;

	private sbyte FlipCountWithDir
	{
		get
		{
			return _flipCountWithDir;
		}
		set
		{
			_flipCountWithDir = value;
		}
	}

	private void OnEnable()
	{
		CheckFlip = true;
		StartCoroutine(FlipCheck());
	}

	private IEnumerator FlipCheck()
	{
		while (CheckFlip)
		{
			Vector3 eulerAngles = base.transform.eulerAngles;
			float prevVector_z = eulerAngles.z;
			yield return new WaitForFixedUpdate();
			Vector3 localEulerAngles = base.transform.localEulerAngles;
			float diff = localEulerAngles.z - prevVector_z;
			if (Mathf.Abs(diff) < 180f)
			{
				rotAccum -= diff;
			}
			if (Mathf.Abs(rotAccum) >= 300f)
			{
				sbyte b = (sbyte)Mathf.Sign(rotAccum);
				if ((float)b == Mathf.Sign(FlipCountWithDir))
				{
					FlipCountWithDir = (sbyte)(FlipCountWithDir + b);
				}
				else
				{
					FlipCountWithDir = b;
				}
				RaceLogic.instance.Collect(RaceLogic.enItem.Flip, (FlipCountWithDir <= 0) ? NitroForBackFlip : NitroForFrontFlip);
				rotAccum = 0f;
			}
		}
	}
}
