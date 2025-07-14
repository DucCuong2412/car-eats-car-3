using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NEwUnderMApControllerSwap : MonoBehaviour
{
	public bool InUnderFlag;

	public GameObject ComixToUnderObj;

	public Button GotoUnderFromComix;

	public LevelGalleryCanvasView LGCV;

	public Button goUnder;

	public Animator animBtnGoUnder;

	public Animator AnimForPlayBtn;

	public GameObject ActiveRotate;

	private bool inUnder;

	public GameObject PositionForCar;

	private void OnEnable()
	{
		if (ComixToUnderObj != null)
		{
			ComixToUnderObj.SetActive(value: false);
		}
		if (Progress.levels._pack[1]._level[7].isOpen)
		{
			Progress.levels.Pack(1, createUndeground: true).Level(1).isOpen = true;
		}
		if (Progress.levels._pack[1]._level[7].isOpen)
		{
			animBtnGoUnder.SetBool("isLocked", value: false);
			if (!Progress.levels.InUndegroundComixShowed)
			{
				StartCoroutine(DelayForComix());
			}
		}
		else
		{
			animBtnGoUnder.SetBool("isLocked", value: true);
		}
		goUnder.onClick.AddListener(GoToUnder);
		if (GotoUnderFromComix != null)
		{
			GotoUnderFromComix.onClick.AddListener(GoToPress);
		}
	}

	private IEnumerator DelayForComix()
	{
		yield return new WaitForSeconds(1f);
		GoToUnder();
		yield return new WaitForSeconds(1f);
		ComixToUnderObj.SetActive(value: true);
		Progress.levels.InUndegroundComixShowed = true;
	}

	private void GoToUnder()
	{
		if (inUnder)
		{
			return;
		}
		inUnder = true;
		int count = LGCV.CarsForLevel.Count;
		if (!Progress.levels._pack[1]._level[7].isOpen)
		{
			return;
		}
		ActiveRotate.transform.position = goUnder.gameObject.transform.position;
		AnimForPlayBtn.SetBool("isOn", value: false);
		for (int i = 0; i < count; i++)
		{
			if (Progress.shop.activeCar == i)
			{
				LGCV.CarsForLevel[Progress.shop.activeCar].SetActive(value: true);
				LGCV.CarsForLevel[Progress.shop.activeCar].transform.parent = PositionForCar.transform;
				LGCV.CarsForLevel[Progress.shop.activeCar].transform.localPosition = Vector3.zero;
				LGCV.CarsForLevel[Progress.shop.activeCar].transform.localScale = new Vector3(0.65f, 0.65f, 0.65f);
			}
			else
			{
				LGCV.CarsForLevel[i].SetActive(value: false);
			}
		}
		foreach (CellContainer item in LGCV.LGCL)
		{
			if (item.state != 0)
			{
				item.SetState(CellContainer.State.Available, Progress.levels.Pack(item.Pack).Level(item.Level).oldticket);
			}
		}
		animBtnGoUnder.SetBool("isActive", value: true);
		if (Progress.levels.InUndegroundComixShowed)
		{
			StartCoroutine(Delay());
		}
	}

	private IEnumerator Delay()
	{
		yield return new WaitForSeconds(1f);
		GoToPress();
	}

	public void GoToPress()
	{
		Progress.levels.InUndegroundIn_OutPreloader = true;
		if (!InUnderFlag)
		{
			int num = 1;
			int num2 = 1;
			for (int i = 0; i < 13; i++)
			{
				if (Progress.levels._packUnderground[1]._level[i] != null && Progress.levels._packUnderground[1]._level[i].isOpen)
				{
					num2 = i;
				}
			}
			Progress.levels.active_pack_last_openned_under = num;
			Progress.levels.active_level_last_openned_under = num2;
			Progress.levels.Max_Active_Pack_under = (byte)num;
			Progress.levels.Max_Active_Level_under = (byte)num2;
			Game.LoadLevel("scene_underground_map_new");
			Progress.levels.InUndeground = true;
			Progress.shop.TestFor9 = true;
			Progress.shop.Undeground2 = false;
		}
		else
		{
			Game.LoadLevel("map_new");
			Progress.levels.InUndeground = false;
			Progress.shop.TestFor9 = false;
			Progress.shop.Undeground2 = false;
		}
	}

	private void OnDisable()
	{
		goUnder.onClick.RemoveAllListeners();
		if (GotoUnderFromComix != null)
		{
			GotoUnderFromComix.onClick.RemoveAllListeners();
		}
	}
}
