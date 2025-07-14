using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalComics : MonoBehaviour
{
	public List<GameObject> ListObjForOnEnableOn = new List<GameObject>();

	public List<GameObject> ListObjForOnEnableOff = new List<GameObject>();

	public Vector2 Position;

	public GameObject ForPosition;

	public float TimeForDelayUpTo1Anim;

	public float TimeForDelayAfter1Anim;

	public float TimeForDelayAfter2Anim;

	public float TimeForDelayAfter3Anim;

	public Animation GloabalAnimatin;

	public GameObject GameObjectForEyesForOn;

	public GameObject GameObjectForEyesForOff;

	public string Name2anim;

	public GameObject Bloker;

	private int temps;

	private void OnEnable()
	{
		StartCoroutine(ForComix());
	}

	private IEnumerator test()
	{
		while (Progress.shop.TutorialFin)
		{
			Progress.shop.ActivCellNum = -1;
			yield return 0;
		}
	}

	private IEnumerator ForComix()
	{
		foreach (GameObject item in ListObjForOnEnableOff)
		{
			item.SetActive(value: false);
		}
		foreach (GameObject item2 in ListObjForOnEnableOn)
		{
			item2.SetActive(value: true);
		}
		temps = Progress.shop.ActivCellNum;
		Progress.shop.ActivCellNum = -1;
		StartCoroutine(test());
		yield return new WaitForSeconds(0.1f);
		ForPosition.GetComponent<RectTransform>().anchoredPosition = Position;
		yield return new WaitForSeconds(TimeForDelayUpTo1Anim);
		GloabalAnimatin.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(TimeForDelayAfter1Anim);
		GameObjectForEyesForOn.SetActive(value: true);
		GameObjectForEyesForOff.SetActive(value: false);
		yield return new WaitForSeconds(TimeForDelayAfter2Anim);
		GloabalAnimatin.Play(Name2anim);
		yield return new WaitForSeconds(TimeForDelayAfter3Anim);
		foreach (GameObject item3 in ListObjForOnEnableOff)
		{
			item3.SetActive(value: true);
		}
		GameObjectForEyesForOff.SetActive(value: true);
		Progress.shop.TutorialFin = false;
		Bloker.SetActive(value: false);
		GloabalAnimatin.gameObject.SetActive(value: false);
		Progress.shop.ActivCellNum = temps;
		Game.LoadLevel("map_new");
	}
}
