using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageMGScript : MonoBehaviour
{
	public GameObject pos;

	private float TimeToDethNum;

	private int ClicksToWinStart;

	private int ClicksToWin;

	public Collider2D coll;

	public Rigidbody2D Rb;

	public List<GameObject> BrokenIronLeft = new List<GameObject>();

	public List<GameObject> NormalIronLeft = new List<GameObject>();

	public List<GameObject> BrokenIronRight = new List<GameObject>();

	public List<GameObject> NormalIronRight = new List<GameObject>();

	public GameObject TrigerEnemyDeth;

	public GameObject TrigerScraps;

	private bool brokeSide;

	[Header("Camera Follow")]
	public GameObject CamFollowObj;

	private float Speed = 1f;

	private Vector3 CamFollowStartPos;

	private Vector3 CarFollowStartPos;

	public CarFollow CarFol;

	private float fraction;

	private bool firstIn;

	private static string str_CarMain = "CarMain";

	private static string str_CarMainChild = "CarMainChild";

	private Coroutine TimeToDeath;

	private Coroutine TutCage;

	private Coroutine tt;

	private Coroutine tMove;

	private int TriggerForME = Animator.StringToHash("is_pulse");

	private float iter;

	private void OnEnable()
	{
		Rb.isKinematic = true;
		CarFol = CarFollow.Instance;
		CamFollowStartPos = CamFollowObj.transform.position;
		fraction = 0f;
		firstIn = false;
		TrigerEnemyDeth.SetActive(value: true);
		coll.enabled = true;
		brokeSide = false;
		TrigerScraps.SetActive(value: true);
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == str_CarMain || other.tag == str_CarMainChild)
		{
			RaceLogic.instance.car.gameObject.transform.eulerAngles = Vector3.Lerp(RaceLogic.instance.car.gameObject.transform.rotation.eulerAngles, Vector3.zero, Time.deltaTime);
		}
	}

	public void SetStart()
	{
		Rb.isKinematic = false;
		TrigerEnemyDeth.SetActive(value: true);
		if (Progress.levels.InUndeground)
		{
			TimeToDethNum = DifficultyConfig.instance.ClicksCageM[12 * (Progress.levels.active_pack_last_openned_under - 1) + Progress.levels.active_level_last_openned_under].TimeToDethSec;
			ClicksToWinStart = DifficultyConfig.instance.ClicksCageM[12 * (Progress.levels.active_pack_last_openned_under - 1) + Progress.levels.active_level_last_openned_under].ClicksToWin;
		}
		else
		{
			TimeToDethNum = DifficultyConfig.instance.ClicksCageM[12 * (Progress.levels.active_pack_last_openned - 1) + Progress.levels.active_level_last_openned].TimeToDethSec;
			ClicksToWinStart = DifficultyConfig.instance.ClicksCageM[12 * (Progress.levels.active_pack_last_openned - 1) + Progress.levels.active_level_last_openned].ClicksToWin;
		}
	}

	private IEnumerator TimeToDeathCorut()
	{
		float t = TimeToDethNum;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			RaceLogic.instance.gui.SetCageTime(TimeToDethNum, t);
			yield return null;
		}
		RaceLogic.instance.GameOver();
		RaceLogic.instance.gui.CageBut.gameObject.SetActive(value: false);
	}

	private IEnumerator tutorials()
	{
		yield return new WaitForSeconds(2f);
		RaceLogic.instance.gui.FCT.showShade();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!brokeSide && (other.tag == "CarMain" || other.tag == "CarMainChild") && !firstIn && coll.enabled)
		{
			ClicksToWin = ClicksToWinStart;
			RaceLogic.instance.gui.CageBut.gameObject.SetActive(value: true);
			RaceLogic.instance.gui.StartCage(this);
			TimeToDeath = StartCoroutine(TimeToDeathCorut());
			if (!Progress.shop.TutorialsInCage)
			{
				TutCage = StartCoroutine(tutorials());
			}
			firstIn = true;
			tMove = StartCoroutine(pressForMoveBackDelay());
			Rigidbody2D component = RaceLogic.instance.car.GetComponent<Rigidbody2D>();
			if ((bool)component)
			{
				component.constraints = RigidbodyConstraints2D.FreezeRotation;
			}
		}
	}

	public void PressBut()
	{
		if (!Progress.shop.TutorialsInCage)
		{
			RaceLogic.instance.gui.FCT.animClicProgress.SetTrigger(TriggerForME);
			if (TutCage != null)
			{
				StopCoroutine(TutCage);
			}
			RaceLogic.instance.gui.FCT.hideShade();
			TutCage = StartCoroutine(tutorials());
		}
		if (tt == null)
		{
			tt = StartCoroutine(pressForCam());
		}
		else if (tt != null)
		{
			iter -= 0.5f;
			if (iter < 0f)
			{
				iter = 0f;
			}
		}
		if (tMove == null && ClicksToWin >= 1)
		{
			tMove = StartCoroutine(pressForMoveBackDelay());
		}
		ClicksToWin--;
		if (ClicksToWin <= 0)
		{
			BrokeCage(left: false);
			Progress.shop.TutorialsInCage = true;
			if (TutCage != null)
			{
				StopCoroutine(TutCage);
			}
		}
		RaceLogic.instance.gui.SetHPFunc((float)ClicksToWin / (float)ClicksToWinStart);
	}

	private IEnumerator pressForCam()
	{
		float pressCamSpeed = 1f;
		iter = 0f;
		while (iter < 1f)
		{
			iter += Time.deltaTime * pressCamSpeed;
			CarFol.zoom.MainCamera.orthographicSize = Mathf.Lerp(10f, 12f, iter);
			yield return null;
		}
		tt = null;
	}

	private IEnumerator pressForMoveBackDelay()
	{
		RaceLogic.instance.car.EngineModule.Break(onoff: false);
		float t2 = 0.6f;
		while (t2 > 0f)
		{
			t2 -= Time.deltaTime;
			RaceLogic.instance.car.Accelerate(5f);
			yield return null;
		}
		t2 = 0.3f;
		while (t2 > 0f)
		{
			t2 -= Time.deltaTime;
			if (coll.enabled)
			{
				RaceLogic.instance.car.Accelerate(-5f);
			}
			else
			{
				RaceLogic.instance.car.Accelerate(5f);
			}
			yield return null;
		}
		tMove = null;
		if (coll.enabled)
		{
			RaceLogic.instance.car.EngineModule.Break(onoff: true);
		}
		else
		{
			RaceLogic.instance.car.EngineModule.Break(onoff: false);
		}
	}

	public void BrakeCage()
	{
		if (TimeToDeath != null)
		{
			StopCoroutine(TimeToDeath);
			TimeToDeath = null;
		}
		CarFol.StartFollow(RaceLogic.instance.car.transform);
		CarFol.offset.startCam();
		Rigidbody2D component = RaceLogic.instance.car.GetComponent<Rigidbody2D>();
		if ((bool)component)
		{
			component.constraints = RigidbodyConstraints2D.None;
		}
	}

	public void BrokeCage(bool left)
	{
		RaceLogic.instance.gui.EndCage(callBrake: false);
		if (TimeToDeath != null)
		{
			StopCoroutine(TimeToDeath);
			TimeToDeath = null;
		}
		TrigerEnemyDeth.SetActive(value: false);
		if (!left)
		{
			TrigerScraps.SetActive(value: false);
			if (!brokeSide)
			{
				Rigidbody2D component = RaceLogic.instance.car.GetComponent<Rigidbody2D>();
				if ((bool)component)
				{
					component.constraints = RigidbodyConstraints2D.None;
				}
				coll.enabled = false;
			}
		}
		RaceLogic.instance.car.EngineModule.Break(onoff: false);
		brokeSide = true;
		base.transform.parent.gameObject.layer = 13;
		setBroken(left);
	}

	private IEnumerator camStoped()
	{
		if (!brokeSide)
		{
			int t = 5;
			while (t > 0)
			{
				t--;
				CarFollowStartPos = RaceLogic.instance.car.transform.position;
				CamFollowObj.transform.position = CarFollowStartPos;
				yield return null;
			}
			CarFol.StartFollow(CamFollowObj.transform);
			CarFol.offset.stabiliz = 0f;
			while (fraction < 1f)
			{
				fraction += Time.deltaTime * Speed;
				CamFollowObj.transform.position = Vector3.Lerp(CarFollowStartPos, CamFollowStartPos, fraction);
				yield return null;
			}
			CamFollowObj.transform.position = CamFollowStartPos;
		}
	}

	private IEnumerator CamToCar()
	{
		CarFollowStartPos = CamFollowObj.transform.position;
		CarFol.offset.stabiliz = 0f;
		fraction = 0f;
		while (fraction < 1f)
		{
			fraction += Time.deltaTime * 0.5f;
			CamFollowObj.transform.position = Vector3.Lerp(CarFollowStartPos, RaceLogic.instance.car.transform.position, fraction);
			yield return null;
		}
		CarFol.StartFollow(RaceLogic.instance.car.transform);
		CarFol.offset.startCam();
		yield return new WaitForSeconds(1f);
		coll.enabled = false;
	}

	private void setBroken(bool left)
	{
		if (left)
		{
			for (int i = 0; i < BrokenIronLeft.Count; i++)
			{
				BrokenIronLeft[i].SetActive(value: true);
			}
			for (int j = 0; j < NormalIronLeft.Count; j++)
			{
				NormalIronLeft[j].SetActive(value: false);
			}
			return;
		}
		int num = 0;
		if (Progress.levels.InUndeground)
		{
			num = UnityEngine.Random.Range(1, 3);
			for (int k = 0; k < num; k++)
			{
				Pool.Scrap(Pool.Scraps.SpiderScr1, pos.transform.position, UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(3, 7));
			}
			num = UnityEngine.Random.Range(1, 3);
			for (int l = 0; l < num; l++)
			{
				Pool.Scrap(Pool.Scraps.SpiderScr1, pos.transform.position, UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(3, 7));
			}
			num = UnityEngine.Random.Range(1, 3);
			for (int m = 0; m < num; m++)
			{
				Pool.Scrap(Pool.Scraps.SpiderScr1, pos.transform.position, UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(3, 7));
			}
		}
		for (int n = 0; n < BrokenIronRight.Count; n++)
		{
			BrokenIronRight[n].SetActive(value: true);
		}
		for (int num2 = 0; num2 < NormalIronRight.Count; num2++)
		{
			NormalIronRight[num2].SetActive(value: false);
		}
	}
}
