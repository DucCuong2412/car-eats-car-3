using AnimationOrTween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fortune : FortuneBase
{
	private static FortuneModel pInstance;

	public List<FortuneLamp> lamps = new List<FortuneLamp>();

	public List<FortuneSector> sectors = new List<FortuneSector>();

	public Transform arrow;

	public Text giftCostText;

	private IEnumerator arrowRotator;

	private float prevAngle;

	private float moveAngle;

	private int activeLamp;

	public static void InitNoShow()
	{
		if (pInstance == null)
		{
			Loading.LoadResource("Fortune", delegate(GameObject obj)
			{
				pInstance = UnityEngine.Object.Instantiate(obj).GetComponent<FortuneModel>();
				pInstance.ObjCont.SetActive(value: false);
				pInstance.qwrteqwr();
			});
		}
	}

	public static void ShowWheel(int tickets, Action callback)
	{
		if (pInstance == null)
		{
			Loading.LoadResource("Fortune", delegate(GameObject obj)
			{
				pInstance = UnityEngine.Object.Instantiate(obj).GetComponent<FortuneModel>();
				pInstance.ObjCont.SetActive(value: true);
				pInstance.ObjCont.transform.parent.gameObject.SetActive(value: false);
				pInstance.ObjCont.transform.parent.gameObject.SetActive(value: true);
				getPerc();
				pInstance.ShowWheel(tickets, callback);
			});
			return;
		}
		pInstance.ObjCont.SetActive(value: true);
		pInstance.ObjCont.transform.parent.gameObject.SetActive(value: false);
		pInstance.ObjCont.transform.parent.gameObject.SetActive(value: true);
		getPerc();
		pInstance.ShowWheel(tickets, callback);
	}

	private static void getPerc()
	{
		float num = 1f;
		if (Progress.levels.active_pack == 1)
		{
			pInstance.fortune.weight = pInstance.fortune.weight1;
			num = pInstance.fortune.MultiplerLoc1;
		}
		else if (Progress.levels.active_pack == 2)
		{
			pInstance.fortune.weight = pInstance.fortune.weight2;
			num = pInstance.fortune.MultiplerLoc2;
		}
		else if (Progress.levels.active_pack == 3)
		{
			pInstance.fortune.weight = pInstance.fortune.weight3;
			num = pInstance.fortune.MultiplerLoc3;
		}
		else if (Progress.levels.active_pack == 4)
		{
			pInstance.fortune.weight = pInstance.fortune.weight4;
			num = pInstance.fortune.MultiplerLoc4;
		}
		for (int i = 0; i < pInstance.fortune.sectors.Count; i++)
		{
			pInstance.fortune.sectors[i].amount = (int)((float)pInstance.fortune.Prizes[i] * num);
			if (pInstance.fortune.sectors[i].amount > 0)
			{
				pInstance.fortune.sectors[i].prizeLabel.text = pInstance.fortune.sectors[i].amount.ToString();
			}
		}
	}

	private void OnDestroy()
	{
		pInstance = null;
	}

	private void OnEnable()
	{
		OnSpinEnded = (SpinEnded)Delegate.Combine(OnSpinEnded, new SpinEnded(SpinEnd));
		OnSpinStarted = (SpinStarted)Delegate.Combine(OnSpinStarted, new SpinStarted(SpinStart));
		Vector3 eulerAngles = wheelTransform.eulerAngles;
		prevAngle = eulerAngles.z;
		giftCostText.text = PriceConfig.instance.fortuneWheel.rubiesForOneAddictionalSpin.ToString();
	}

	private void OnDisable()
	{
		OnSpinEnded = (SpinEnded)Delegate.Remove(OnSpinEnded, new SpinEnded(SpinEnd));
		OnSpinStarted = (SpinStarted)Delegate.Remove(OnSpinStarted, new SpinStarted(SpinStart));
	}

	public override void Update()
	{
		base.Update();
		float num = 360f / (float)sectors.Count / 2f;
		float num2 = prevAngle;
		Vector3 eulerAngles = wheelTransform.eulerAngles;
		moveAngle = num2 - eulerAngles.z;
		if (Mathf.Abs(moveAngle) > num)
		{
			Vector3 eulerAngles2 = wheelTransform.eulerAngles;
			prevAngle = eulerAngles2.z;
			if (arrowRotator == null)
			{
				arrowRotator = AnimateArrow();
				StartCoroutine(arrowRotator);
			}
		}
	}

	public void pressBut()
	{
		StopWheel();
	}

	private IEnumerator AnimateArrow()
	{
		float sign = Mathf.Sign((!base.isSpinning) ? moveAngle : base.Speed);
		arrow.eulerAngles = Vector3.forward * ZeroSectorAngle;
		for (int j = 0; j < 5; j++)
		{
			arrow.eulerAngles += 2f * Vector3.forward * sign;
			yield return null;
		}
		for (int i = 0; i < 5; i++)
		{
			arrow.eulerAngles -= 2f * Vector3.forward * sign;
			yield return null;
		}
		arrow.eulerAngles = Vector3.forward * ZeroSectorAngle;
		arrowRotator = null;
	}

	private void SpinEnd(int sectorNum)
	{
		StartCoroutine(WinSector(sectorNum));
	}

	private void SpinStart()
	{
		Progress.levels.tickets--;
		StartCoroutine(Lighting());
		Audio.Play("fortune", 1f, loop: true);
	}

	private IEnumerator Lighting()
	{
		while (base.isSpinning)
		{
			activeLamp += ((!(base.Speed <= 0f)) ? 1 : (-1));
			if (activeLamp < 0)
			{
				activeLamp = lamps.Count - 1;
			}
			else if (activeLamp >= lamps.Count)
			{
				activeLamp = 0;
			}
			if (lamps[activeLamp].lightAnimation.isPlaying)
			{
				lamps[activeLamp].lightAnimation.Stop();
			}
			ActiveAnimation.Play(lamps[activeLamp].lightAnimation, Direction.Forward);
			yield return Utilities.WaitForRealSeconds(0.5f / Mathf.Abs(base.Speed));
		}
		activeLamp = 0;
	}

	private IEnumerator WinSector(int sectorNum)
	{
		HSBColor hbsColor = HSBColor.FromColor(sectors[sectorNum].sprite.color);
		hbsColor.s = 1f;
		Color color = hbsColor.ToColor();
		for (int j = 0; j < lamps.Count; j++)
		{
			if (lamps[j].lightAnimation.isPlaying)
			{
				lamps[j].lightAnimation.Stop();
			}
			lamps[j].SetColor(color);
		}
		for (int i = 0; i < 2; i++)
		{
			for (int k = 0; k < lamps.Count; k++)
			{
				if (lamps[k].lightAnimation.isPlaying)
				{
					lamps[k].lightAnimation.Stop();
				}
				ActiveAnimation.Play(lamps[k].lightAnimation, Direction.Forward);
			}
			yield return Utilities.WaitForRealSeconds(0.5f);
		}
		for (int l = 0; l < lamps.Count; l++)
		{
			lamps[l].Reset();
		}
	}
}
