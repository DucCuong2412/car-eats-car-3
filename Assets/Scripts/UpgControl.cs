using UnityEngine;
using UnityEngine.UI;

public class UpgControl : MonoBehaviour
{
	public Image gradient;

	public Text priceLabel;

	public Image rubyIcon;

	public GameObject buyBtn;

	public void SetProgress(int count)
	{
		if (count < 5)
		{
			gradient.type = Image.Type.Filled;
			gradient.fillMethod = Image.FillMethod.Horizontal;
		}
		switch (count)
		{
		case 0:
			gradient.fillAmount = 0f;
			break;
		case 1:
			gradient.fillAmount = 0.17f;
			break;
		case 2:
			gradient.fillAmount = 0.4f;
			break;
		case 3:
			gradient.fillAmount = 0.63f;
			break;
		case 4:
			gradient.fillAmount = 0.85f;
			break;
		case 5:
			gradient.type = Image.Type.Sliced;
			break;
		}
	}

	public void SetUpgPriceLabel(string price, bool max = false)
	{
		if (!max)
		{
			buyBtn.GetComponentInChildren<Text>().transform.localScale = new Vector3(1f, 1f);
			buyBtn.GetComponentInChildren<Text>().transform.localPosition = new Vector3(-91.72497f, -2f);
			Image[] componentsInChildren = buyBtn.GetComponentsInChildren<Image>();
			componentsInChildren[0].color = new Color32(0, 150, byte.MaxValue, byte.MaxValue);
			componentsInChildren[1].color = new Color32(152, 220, byte.MaxValue, byte.MaxValue);
			componentsInChildren[2].color = new Color32(0, 82, 163, byte.MaxValue);
			priceLabel.text = price;
			rubyIcon.enabled = true;
		}
		else
		{
			buyBtn.GetComponentInChildren<Text>().transform.localScale = new Vector3(1.2f, 1.2f);
			buyBtn.GetComponentInChildren<Text>().transform.localPosition = new Vector3(-78.75f, -2f);
			Image[] componentsInChildren2 = buyBtn.GetComponentsInChildren<Image>();
			componentsInChildren2[0].color = new Color32(215, 102, 0, byte.MaxValue);
			componentsInChildren2[1].color = new Color32(byte.MaxValue, 191, 23, byte.MaxValue);
			componentsInChildren2[2].color = new Color32(148, 71, 0, byte.MaxValue);
			priceLabel.text = price;
			rubyIcon.enabled = false;
		}
	}
}
