using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGargageController : MonoBehaviour
{
	public GameObject shade;

	public GameObject Tut1;

	public GameObject Tut2;

	public GameObject Tut3;

	public ControllerBtnUpgrate CBU;

	public ControllerGarage CG;

	public List<GameObject> OffObj = new List<GameObject>();

	private void OnEnable()
	{
		if (Progress.shop.TutorialGarage)
		{
			Game.OnStateChange(Game.gameState.Fortune);
			shade.SetActive(value: true);
			Tut1.SetActive(value: true);
		}
	}

	public void ClicTut1()
	{
		CG.OnClicSpeed();
		Tut1.SetActive(value: false);
		Tut2.SetActive(value: true);
		foreach (GameObject item in OffObj)
		{
			item.SetActive(value: false);
		}
	}

	public void ClicTut2()
	{
		CBU.Speed();
		Tut2.SetActive(value: false);
		Tut3.SetActive(value: true);
		foreach (GameObject item in OffObj)
		{
			item.SetActive(value: true);
		}
	}

	public void clictut3()
	{
		Tut3.SetActive(value: false);
		shade.SetActive(value: false);
		Progress.shop.TutorialGarage = false;
		Progress.shop.shopinlevel = false;
		StartCoroutine(cor());
	}

	private IEnumerator cor()
	{
		yield return new WaitForSeconds(0.5f);
		if (Progress.levels.InUndeground)
		{
			Game.LoadLevel("scene_underground_map_new");
		}
		else
		{
			Game.LoadLevel("map_new");
		}
	}
}
