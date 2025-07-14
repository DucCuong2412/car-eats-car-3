using System.Collections.Generic;
using UnityEngine;

public class OnGO : MonoBehaviour
{
	public List<GameObject> GO = new List<GameObject>();

	private void Update()
	{
		if (!GO[0].activeSelf)
		{
			foreach (GameObject item in GO)
			{
				item.SetActive(value: true);
			}
		}
	}
}
