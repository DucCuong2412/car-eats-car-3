using UnityEngine;

public class FortuneLamp : MonoBehaviour
{
	public UISprite sprite;

	public Animation lightAnimation;

	public void Reset()
	{
		sprite.color = new Color(1f, 1f, 0f, 0.01f);
	}

	public void SetColor(Color c)
	{
		sprite.color = new Color(c.r, c.g, c.b, 1f);
	}
}
