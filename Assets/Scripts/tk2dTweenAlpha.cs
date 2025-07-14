using UnityEngine;

[RequireComponent(typeof(tk2dBaseSprite))]
public class tk2dTweenAlpha : UITweener
{
	public float from;

	public float to = 1f;

	private tk2dBaseSprite _sprite;

	private tk2dBaseSprite Sprite
	{
		get
		{
			if (_sprite == null)
			{
				_sprite = GetComponent<tk2dBaseSprite>();
			}
			return _sprite;
		}
	}

	private float alpha
	{
		get
		{
			Color color = Sprite.color;
			return color.a;
		}
		set
		{
			tk2dBaseSprite sprite = Sprite;
			Color color = Sprite.color;
			float r = color.r;
			Color color2 = Sprite.color;
			float g = color2.g;
			Color color3 = Sprite.color;
			sprite.color = new Color(r, g, color3.b, value);
		}
	}

	protected override void OnUpdate(float factor, bool isFinished)
	{
		alpha = Mathf.Lerp(from, to, factor);
	}

	public static tk2dTweenAlpha Begin(GameObject go, float duration, float alpha)
	{
		tk2dTweenAlpha tk2dTweenAlpha = UITweener.Begin<tk2dTweenAlpha>(go, duration);
		tk2dTweenAlpha.from = tk2dTweenAlpha.alpha;
		tk2dTweenAlpha.to = alpha;
		return tk2dTweenAlpha;
	}
}
