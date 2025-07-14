using System;
using System.Collections.Generic;
using UnityEngine;

public class Car2DSmallerBombConstructor : MonoBehaviour
{
	[Serializable]
	public class Body
	{
		public GameObject BodyObj;

		public List<GameObject> Specifics = new List<GameObject>();
	}

	public int CarType = 1;

	public int inCarType = -1;

	public int Colorindex = -1;

	public bool IsCivil;

	public bool IsAhead;

	public List<GameObject> RearWhells = new List<GameObject>();

	public List<GameObject> FrontWheels = new List<GameObject>();

	public List<Body> Bodies = new List<Body>();

	public List<Car2DCivilNEW> BodySprites = new List<Car2DCivilNEW>();

	public void setActiveCont()
	{
		for (int i = 0; i < RearWhells.Count; i++)
		{
			RearWhells[i].SetActive(value: false);
		}
		for (int j = 0; j < FrontWheels.Count; j++)
		{
			FrontWheels[j].SetActive(value: false);
		}
		for (int k = 0; k < Bodies.Count; k++)
		{
			Bodies[k].BodyObj.SetActive(value: false);
			if (IsCivil)
			{
				for (int l = 0; l < Bodies[k].Specifics.Count; l++)
				{
					Bodies[k].Specifics[l].SetActive(value: false);
				}
			}
		}
		string value = "0" + CarType.ToString();
		for (int m = 0; m < RearWhells.Count; m++)
		{
			if (m == CarType - 1 && RearWhells[m].transform.name.Contains(value))
			{
				RearWhells[m].SetActive(value: true);
			}
		}
		for (int n = 0; n < FrontWheels.Count; n++)
		{
			if (FrontWheels[n].transform.name.Contains(value))
			{
				FrontWheels[n].SetActive(value: true);
			}
		}
		if (IsAhead)
		{
			Bodies[CarType - 1].BodyObj.SetActive(value: true);
		}
		else
		{
			Bodies[Bodies.Count / 2 + (CarType - 1)].BodyObj.SetActive(value: true);
		}
		if (IsCivil)
		{
			Bodies[CarType - 1].Specifics[inCarType].SetActive(value: true);
			Bodies[Bodies.Count / 2 + (CarType - 1)].Specifics[inCarType].SetActive(value: true);
			for (int num = 0; num < BodySprites.Count; num++)
			{
				BodySprites[num].SetColors(Colorindex);
			}
		}
	}
}
