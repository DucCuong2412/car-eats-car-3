using System.Collections.Generic;
using UnityEngine;

public class SelectBombs : MonoBehaviour
{
	public List<GameObject> bombIcon = new List<GameObject>();

	public List<GameObject> bombIconBack = new List<GameObject>();

	public GameObject icoBombOn;

	public GameObject icoBombOff;

	public GameObject icoTurboOff;

	public GameObject icoTurboOn;

	private void OnEnable()
	{
		SetBombIcon(Progress.shop.Car.bombActLev);
	}

	public void SetBombIcon(int bombtype)
	{
		if (bombtype > 5)
		{
			bombtype = 4;
		}
		foreach (GameObject item in bombIcon)
		{
			item.gameObject.SetActive(value: false);
		}
		switch (bombtype)
		{
		case 5:
			bombIcon[1].gameObject.SetActive(value: true);
			break;
		case 0:
			bombIcon[0].gameObject.SetActive(value: true);
			break;
		default:
			bombIcon[bombtype + 1].gameObject.SetActive(value: true);
			break;
		}
		foreach (GameObject item2 in bombIconBack)
		{
			item2.gameObject.SetActive(value: false);
		}
		switch (bombtype)
		{
		case 5:
			bombIconBack[1].gameObject.SetActive(value: true);
			break;
		case 0:
			bombIconBack[0].gameObject.SetActive(value: true);
			break;
		default:
			bombIconBack[bombtype + 1].gameObject.SetActive(value: true);
			break;
		}
	}

	private void Update()
	{
		if (RaceLogic.instance.car != null)
		{
			if (RaceLogic.instance.car.TurboModule._barrel.Value == 0f)
			{
				icoTurboOff.SetActive(value: true);
			}
			else
			{
				icoTurboOff.SetActive(value: false);
			}
			if (RaceLogic.instance.car.BombModule._barrel.Value == 0f)
			{
				icoBombOff.SetActive(value: true);
			}
			else
			{
				icoBombOff.SetActive(value: false);
			}
		}
	}
}
