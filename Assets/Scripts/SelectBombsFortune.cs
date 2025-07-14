using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBombsFortune : MonoBehaviour
{
	public List<GameObject> bombIcon = new List<GameObject>();

	private void OnEnable()
	{
		StartCoroutine(forStart());
	}

	public IEnumerator forStart()
	{
		yield return new WaitForSeconds(0.5f);
		SetBombIcon(Progress.shop.Car.bombActLev);
	}

	public void SetBombIcon(int bombtype)
	{
		foreach (GameObject item in bombIcon)
		{
			item.gameObject.SetActive(value: false);
		}
		foreach (GameObject item2 in bombIcon)
		{
			switch (bombtype)
			{
			case 5:
				if (item2.name.Contains("01"))
				{
					item2.gameObject.SetActive(value: true);
				}
				break;
			case 0:
				if (item2.name.Contains("00"))
				{
					item2.gameObject.SetActive(value: true);
				}
				break;
			case 1:
				if (item2.name.Contains("02"))
				{
					item2.gameObject.SetActive(value: true);
				}
				break;
			case 2:
				if (item2.name.Contains("03"))
				{
					item2.gameObject.SetActive(value: true);
				}
				break;
			case 3:
				if (item2.name.Contains("04"))
				{
					item2.gameObject.SetActive(value: true);
				}
				break;
			case 4:
				if (item2.name.Contains("05"))
				{
					item2.gameObject.SetActive(value: true);
				}
				break;
			}
		}
	}
}
