using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndegroundMapBoxController : MonoBehaviour
{
	[Serializable]
	public class Egg
	{
		public List<GameObject> Eggs;
	}

	public List<Animator> Boxes;

	public List<Egg> Eggs;

	public int TimeBetweenBoxes;

	private int yellow = Animator.StringToHash("yellow");

	private int green = Animator.StringToHash("green");

	private int blue = Animator.StringToHash("blue");

	private int white = Animator.StringToHash("white");

	private void OnEnable()
	{
		if (Progress.levels.ResultBoxRev_Undeground1 == null || Progress.levels.ResultBoxRev_Undeground1.Count == 0)
		{
			Progress.levels.ResultBoxRev_Undeground1 = ResultBoxesConfig.instance.LevelsBox;
		}
		int num = ResultBoxesConfig.instance.LevelsBox.Count - 13;
		if (Progress.levels.ResultBoxRev_Undeground1.Count == 13)
		{
			for (int i = 0; i < num; i++)
			{
				Progress.levels.ResultBoxRev_Undeground1.Add(ResultBoxesConfig.instance.LevelsBox[i + 13]);
			}
		}
		num = 0;
		if (Progress.shop.Cars[9].equipped)
		{
			num = Boxes.Count;
			for (int j = 0; j < num; j++)
			{
				int count = Progress.levels.ResultBoxRev_Undeground1[j].Revards.Count;
				for (int k = 0; k < count; k++)
				{
					if (Progress.levels.ResultBoxRev_Undeground1[j].Revards[k] == ResultBoxesConfig.Revard.Egg_1)
					{
						Progress.levels.ResultBoxRev_Undeground1[j].Revards.Remove(ResultBoxesConfig.Revard.Egg_1);
					}
				}
			}
		}
		if (Progress.shop.Cars[10].equipped)
		{
			num = Boxes.Count;
			for (int l = 0; l < num; l++)
			{
				int count2 = Progress.levels.ResultBoxRev_Undeground1[l].Revards.Count;
				for (int m = 0; m < count2; m++)
				{
					if (Progress.levels.ResultBoxRev_Undeground1[l].Revards[m] == ResultBoxesConfig.Revard.Egg_2)
					{
						Progress.levels.ResultBoxRev_Undeground1[l].Revards.Remove(ResultBoxesConfig.Revard.Egg_2);
					}
				}
			}
		}
		if (TimeEnd())
		{
			bool flag = true;
			num = 4;
			for (int n = 0; n < num; n++)
			{
				if (Progress.levels.ResultBoxRev_Undeground1[n].Revards.Count != 0)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				if (Progress.levels.ResultBoxRev_Undeground1_Adds == null || Progress.levels.ResultBoxRev_Undeground1_Adds.Count == 0)
				{
					num = ResultBoxesConfig.instance.LevelsBox.Count;
					for (int num2 = 0; num2 < num; num2++)
					{
						Progress.levels.ResultBoxRev_Undeground1_Adds.Add(item: false);
					}
				}
				if (Progress.levels.ResultBoxRev_Undeground1_Adds.Count == 13)
				{
					num = ResultBoxesConfig.instance.LevelsBox.Count - 13;
					for (int num3 = 0; num3 < num; num3++)
					{
						Progress.levels.ResultBoxRev_Undeground1_Adds.Add(item: false);
					}
				}
				bool flag2 = true;
				num = Progress.levels.ResultBoxRev_Undeground1_Adds.Count;
				for (int num4 = 0; num4 < num; num4++)
				{
					if (Progress.levels.ResultBoxRev_Undeground1_Adds[num4])
					{
						flag2 = false;
						break;
					}
				}
				if (flag2)
				{
					int num5 = 0;
					num = Progress.levels.ResultBoxRev_Undeground1.Count;
					for (int num6 = 0; num6 < num && Progress.levels.ResultBoxRev_Undeground1[num6].Revards.Count == 0; num6++)
					{
						num5++;
					}
					num5 = UnityEngine.Random.Range(0, num5);
					Progress.levels.ResultBoxRev_Undeground1_Adds[num5] = true;
					Progress.levels.ResultBoxRev_Undeground1[num5].Revards.Add(ResultBoxesConfig.Revard.Blue);
					Progress.levels.ResultBoxRev_Undeground1[num5].Revards.Add(ResultBoxesConfig.Revard.Green);
					Progress.levels.ResultBoxRev_Undeground1[num5].Revards.Add(ResultBoxesConfig.Revard.White);
					Progress.levels.ResultBoxRev_Undeground1[num5].Revards.Add(ResultBoxesConfig.Revard.Yellow);
				}
			}
		}
		StartCoroutine(Delay());
	}

	public bool TimeEnd()
	{
		if ((DateTime.UtcNow - Progress.levels.ResultBoxRev_Undeground1_LastGetTime).TotalMinutes < (double)TimeBetweenBoxes)
		{
			return false;
		}
		return true;
	}

	private IEnumerator Delay()
	{
		int t = 1;
		while (t > 0)
		{
			t--;
			yield return null;
		}
		int count4 = Boxes.Count;
		for (int i = 0; i < count4; i++)
		{
			int count3 = Eggs[i].Eggs.Count;
			for (int j = 0; j < count3; j++)
			{
				Eggs[i].Eggs[j].SetActive(value: false);
			}
			count3 = Progress.levels.ResultBoxRev_Undeground1[i].Revards.Count;
			for (int k = 0; k < count3; k++)
			{
				if (Progress.levels.ResultBoxRev_Undeground1[i].Revards[k] == ResultBoxesConfig.Revard.Yellow)
				{
					Boxes[i].SetBool(yellow, value: true);
				}
				if (Progress.levels.ResultBoxRev_Undeground1[i].Revards[k] == ResultBoxesConfig.Revard.Green)
				{
					Boxes[i].SetBool(green, value: true);
				}
				if (Progress.levels.ResultBoxRev_Undeground1[i].Revards[k] == ResultBoxesConfig.Revard.Blue)
				{
					Boxes[i].SetBool(blue, value: true);
				}
				if (Progress.levels.ResultBoxRev_Undeground1[i].Revards[k] == ResultBoxesConfig.Revard.White)
				{
					Boxes[i].SetBool(white, value: true);
				}
				if (Progress.levels.ResultBoxRev_Undeground1[i].Revards[k] == ResultBoxesConfig.Revard.Egg_1)
				{
					Eggs[i].Eggs[0].SetActive(value: true);
				}
				if (Progress.levels.ResultBoxRev_Undeground1[i].Revards[k] == ResultBoxesConfig.Revard.Egg_2)
				{
					Eggs[i].Eggs[1].SetActive(value: true);
				}
				if (Progress.levels.ResultBoxRev_Undeground1[i].Revards[k] == ResultBoxesConfig.Revard.Egg_3)
				{
					Eggs[i].Eggs[2].SetActive(value: true);
				}
				if (Progress.levels.ResultBoxRev_Undeground1[i].Revards[k] == ResultBoxesConfig.Revard.Egg_5)
				{
					Eggs[i].Eggs[3].SetActive(value: true);
				}
				if (Progress.levels.ResultBoxRev_Undeground1[i].Revards[k] == ResultBoxesConfig.Revard.Egg_6)
				{
					Eggs[i].Eggs[4].SetActive(value: true);
				}
			}
		}
	}
}
