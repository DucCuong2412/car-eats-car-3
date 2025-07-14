using System.Collections.Generic;
using UnityEngine;

public class GroundDecor : MonoBehaviour
{
	public tk2dSprite sprite;

	public List<string> DecorNames = new List<string>();

	public int minPerSegment = 5;

	public int maxPerSegment = 10;

	[Range(0f, 1f)]
	public float upperCoef = 0.25f;

	[Range(-1f, 0f)]
	public float lowerCoef = -0.3f;

	public void setRandomSprite()
	{
		sprite.SetSprite(DecorNames[Random.Range(0, DecorNames.Count)]);
	}
}
