using System.Collections.Generic;
using UnityEngine;

public class starsLocCar : MonoBehaviour
{
	public List<GameObject> stars = new List<GameObject>();

	private void Update()
	{
		if (Progress.shop.activeCar == 3 || Progress.shop.activeCar == 4 || Progress.shop.activeCar == 5)
		{
			foreach (GameObject star in stars)
			{
				star.SetActive(value: true);
			}
		}
		else
		{
			foreach (GameObject star2 in stars)
			{
				star2.SetActive(value: false);
			}
		}
	}
}
