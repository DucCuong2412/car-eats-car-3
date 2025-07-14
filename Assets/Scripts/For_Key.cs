using System.Collections.Generic;
using UnityEngine;

public class For_Key : MonoBehaviour
{
	public List<GameObject> key = new List<GameObject>();

	private void OnEnable()
	{
		int num = 0;
		if (Progress.shop.Key1)
		{
			num++;
		}
		if (Progress.shop.Key2)
		{
			num++;
		}
		if (Progress.shop.Key3)
		{
			num++;
		}
		foreach (GameObject item in key)
		{
			item.SetActive(value: false);
		}
		for (int i = 0; i < num; i++)
		{
			key[i].SetActive(value: true);
		}
	}
}
