using System.Collections.Generic;
using UnityEngine;

public class WingsAndHalo : MonoBehaviour
{
	public List<GameObject> Lists = new List<GameObject>();

	private void OnEnable()
	{
		foreach (GameObject list in Lists)
		{
			list.SetActive(value: false);
		}
	}
}
