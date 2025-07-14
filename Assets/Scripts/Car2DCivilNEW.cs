using System.Collections.Generic;
using UnityEngine;

public class Car2DCivilNEW : MonoBehaviour
{
	public List<tk2dSprite> SpritesColored1 = new List<tk2dSprite>();

	public List<tk2dSprite> SpritesColored2 = new List<tk2dSprite>();

	public void SetColors(int index)
	{
		Vector3 colored = DifficultyConfig.instance.ColorsAll[index].Colored1;
		Vector3 colored2 = DifficultyConfig.instance.ColorsAll[index].Colored2;
		for (int i = 0; i < SpritesColored1.Count; i++)
		{
			SpritesColored1[i].color = new Color(colored.x, colored.y, colored.z);
		}
		for (int j = 0; j < SpritesColored2.Count; j++)
		{
			SpritesColored2[j].color = new Color(colored2.x, colored2.y, colored2.z);
		}
	}
}
