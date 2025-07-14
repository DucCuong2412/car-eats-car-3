using System.Collections.Generic;
using UnityEngine;

public class IncubatorMG_ObjLayers : MonoBehaviour
{
	public List<SpriteRenderer> Sprites;

	[HideInInspector]
	public List<int> SpritesLayers = new List<int>();

	public GameObject OnObjectForDeath;

	public GameObject OffObjectForDeath;

	private void Start()
	{
		TrySetLay();
	}

	public void TrySetLay()
	{
		if (SpritesLayers.Count == 0)
		{
			SpritesLayers.Clear();
			int count = Sprites.Count;
			for (int i = 0; i < count; i++)
			{
				SpritesLayers.Add(Sprites[i].sortingOrder);
			}
		}
	}

	private void OnEnable()
	{
		if (SpritesLayers.Count == 0)
		{
			TrySetLay();
		}
		if (Sprites.Count == SpritesLayers.Count)
		{
			int count = Sprites.Count;
			for (int i = 0; i < count; i++)
			{
				Sprites[i].sortingOrder = SpritesLayers[i];
			}
		}
	}

	private void OnDisable()
	{
		if (Sprites.Count == SpritesLayers.Count)
		{
			int count = Sprites.Count;
			for (int i = 0; i < count; i++)
			{
				Sprites[i].sortingOrder = SpritesLayers[i];
			}
		}
	}
}
