using UnityEngine;

public class FortuneSector : MonoBehaviour
{
	public enum SectorType
	{
		rubins,
		health,
		turbo,
		resurrection
	}

	public enum SectorColor
	{
		color_0,
		color_1,
		color_2,
		color_3,
		color_4,
		color_5,
		color_6
	}

	public GameObject staticPart;

	public UISprite sprite;

	public UISprite prizeSprite;

	public UILabel prizeLabel;

	public SectorType sectorType;

	public int amount;

	private void Awake()
	{
		if (sectorType == SectorType.rubins)
		{
			prizeLabel.text = amount.ToString();
		}
		else
		{
			prizeLabel.text = string.Empty;
		}
	}

	private Color getColor(SectorColor color)
	{
		switch (color)
		{
		case SectorColor.color_1:
			return new Color(14f / 15f, 0.882352948f, 88f / 255f);
		case SectorColor.color_2:
			return new Color(223f / 255f, 127f / 255f, 19f / 85f);
		case SectorColor.color_3:
			return new Color(11f / 15f, 11f / 51f, 0.294117659f);
		case SectorColor.color_4:
			return new Color(0.4117647f, 91f / 255f, 167f / 255f);
		case SectorColor.color_5:
			return new Color(19f / 85f, 152f / 255f, 40f / 51f);
		case SectorColor.color_6:
			return new Color(62f / 255f, 59f / 85f, 157f / 255f);
		default:
			return Color.white;
		}
	}

	public void SetColor(Color color)
	{
		sprite.color = color;
	}

	public void SetColor(SectorColor color)
	{
		SetColor(getColor(color));
	}

	public void SetType(SectorType type)
	{
		if (type == SectorType.rubins)
		{
		}
	}
}
