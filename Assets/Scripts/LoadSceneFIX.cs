using System.Collections.Generic;
using UnityEngine;

public class LoadSceneFIX : MonoBehaviour
{
	public List<GameObject> Objs = new List<GameObject>();

	private int counter = -1;

	private void OnEnable()
	{
		counter = 10;
	}

	private void Update()
	{
		counter--;
		if (counter == 0)
		{
			counter = -1;
			for (int i = 0; i < Objs.Count; i++)
			{
				Objs[i].SetActive(value: true);
			}
			base.gameObject.SetActive(value: false);
		}
	}
}
