using ArabicSupport;
using SmartLocalization;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MarkerNewCar : MonoBehaviour
{
	public GameObject left;

	public GameObject right;

	public ScrollRectSnapLEXTry SRSLT;

	public Text discr;

	private void Update()
	{
		string currentlyLanguageCode = LocalizationManager.instance.currentlyLanguageCode;
		if (Progress.shop.activeCar == 1)
		{
			discr.text = LanguageManager.Instance.GetTextValue("WIN BOSS 1 TO UNLOCK");
			if (currentlyLanguageCode.Contains("fa-IR"))
			{
				discr.text = ArabicFixer.Fix(discr.text);
			}
		}
		else if (Progress.shop.activeCar == 2)
		{
			discr.text = LanguageManager.Instance.GetTextValue("WIN BOSS 2 TO UNLOCK");
			if (currentlyLanguageCode.Contains("fa-IR"))
			{
				discr.text = ArabicFixer.Fix(discr.text);
			}
		}
	}

	private void OnEnable()
	{
		StartCoroutine(CH());
	}

	private IEnumerator CH()
	{
		yield return 0;
		yield return 0;
		yield return 0;
		change();
	}

	public void change()
	{
		switch (SRSLT.GetMinButtonNum() - 2)
		{
		case 0:
			if ((!Progress.shop.Cars[1].equipped && Progress.shop.Cars[1].bought) || (!Progress.shop.Cars[2].equipped && Progress.shop.Cars[2].bought) || (Progress.shop.Key1 && Progress.shop.Key2 && Progress.shop.Key3 && !Progress.shop.Cars[4].equipped))
			{
				right.SetActive(value: true);
				left.SetActive(value: false);
			}
			else
			{
				right.SetActive(value: false);
				left.SetActive(value: false);
			}
			break;
		case 1:
			if ((!Progress.shop.Cars[2].equipped && Progress.shop.Cars[2].bought) || (Progress.shop.Key1 && Progress.shop.Key2 && Progress.shop.Key3 && !Progress.shop.Cars[4].equipped))
			{
				right.SetActive(value: true);
				left.SetActive(value: false);
			}
			else
			{
				right.SetActive(value: false);
				left.SetActive(value: false);
			}
			break;
		case 2:
			if (Progress.shop.BossDeath1)
			{
				if (!Progress.shop.Cars[1].equipped && Progress.shop.Cars[1].bought)
				{
					right.SetActive(value: false);
					left.SetActive(value: true);
				}
				else
				{
					right.SetActive(value: false);
					left.SetActive(value: false);
				}
				if (Progress.shop.Key1 && Progress.shop.Key2 && Progress.shop.Key3 && !Progress.shop.Cars[4].equipped)
				{
					right.SetActive(value: true);
				}
				else
				{
					right.SetActive(value: false);
				}
			}
			break;
		case 3:
			if ((!Progress.shop.Cars[1].equipped && Progress.shop.Cars[1].bought) || (!Progress.shop.Cars[2].equipped && Progress.shop.Cars[2].bought))
			{
				right.SetActive(value: false);
				left.SetActive(value: true);
			}
			else
			{
				right.SetActive(value: false);
				left.SetActive(value: false);
			}
			if (Progress.shop.Key1 && Progress.shop.Key2 && Progress.shop.Key3 && !Progress.shop.Cars[4].equipped)
			{
				right.SetActive(value: true);
			}
			else
			{
				right.SetActive(value: false);
			}
			break;
		case 4:
			if ((!Progress.shop.Cars[1].equipped && Progress.shop.Cars[1].bought) || (!Progress.shop.Cars[2].equipped && Progress.shop.Cars[2].bought))
			{
				right.SetActive(value: false);
				left.SetActive(value: true);
			}
			else
			{
				right.SetActive(value: false);
				left.SetActive(value: false);
			}
			break;
		}
	}
}
