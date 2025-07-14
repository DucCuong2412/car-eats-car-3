using UnityEngine;
using UnityEngine.UI;

public class GUIColorTint : MonoBehaviour
{
	public Image GameWorldColor;

	public Color getGameWorldColor()
	{
		return GameWorldColor.color;
	}

	public void SetGameWorldColor(Color a, Color b, float t)
	{
		GameWorldColor.color = Color.Lerp(a, b, t);
	}
}
