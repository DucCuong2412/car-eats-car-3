using System.Collections.Generic;
using UnityEngine;

public class CarsColiders : MonoBehaviour
{
	public List<CircleCollider2D> circle = new List<CircleCollider2D>();

	public List<BoxCollider2D> box = new List<BoxCollider2D>();

	public List<PolygonCollider2D> poligon = new List<PolygonCollider2D>();

	private void OnEnable()
	{
		if (RaceLogic.instance.AllInitedForPool)
		{
			ON();
		}
	}

	private void ON()
	{
		for (int i = 0; i < circle.Count; i++)
		{
			if (circle[i] != null)
			{
				circle[i].enabled = true;
			}
		}
		for (int j = 0; j < box.Count; j++)
		{
			if (box[j] != null)
			{
				box[j].enabled = true;
			}
		}
		for (int k = 0; k < poligon.Count; k++)
		{
			if (poligon[k] != null)
			{
				poligon[k].enabled = true;
			}
		}
	}
}
