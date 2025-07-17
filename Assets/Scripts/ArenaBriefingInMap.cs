//using CompleteProject;
using Smokoko.DebugModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaBriefingInMap : MonoBehaviour
{
	public Animator AnimShowHide;

	public Button Exit;

	public GameObject Play;

	public GameObject PlayVideo;

	public GameObject ClosePlay;

	public CounterController Badge;

	public List<GameObject> Keys = new List<GameObject>();

	public Button ForRealMoney;

	public Text MoneyForByuNow;

	public Text best;

	public Text best2;

	public Text price;

	private int temp;

	public Slider slider;

	public NewControllerForButtonPlayOnMap ControllerForPlayButton;

	private int isON = Animator.StringToHash("isON");

	private void OnEnable()
	{
		ForRealMoney.gameObject.SetActive(!Progress.shop.Cars[4].equipped);
		Progress.shop.ArenaBrifOpen = true;
		temp = 0;
		//string text = Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar2).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar2).metadata.localizedPrice.ToString();
		//text = ((!string.IsNullOrEmpty(text)) ? text : ShopManagerPrice.instance.Price.CarByu4);
		//MoneyForByuNow.text = text;
		Exit.onClick.AddListener(ClicExit);
		ForRealMoney.onClick.AddListener(ClicPremium);
		int num = 0;
		if (Progress.shop.Key1)
		{
			num++;
		}
		if (Progress.shop.Key2)
		{
			num++;
		}
		if (Progress.shop.Key3)
		{
			num++;
		}
		for (int i = 0; i < num; i++)
		{
			Keys[i].SetActive(value: true);
		}
		for (int j = 1; j <= 3; j++)
		{
			for (int k = 1; k <= 12; k++)
			{
				temp += Progress.levels.Pack(j).Level(k).oldticket;
			}
		}
		if (Progress.levels.active_pack_last_openned == 1)
		{
			slider.maxValue = DifficultyConfig.instance.MetrivForARENA1;
			slider.value = Progress.shop.Arena1MaxDistance;
			price.text = "-" + DifficultyConfig.instance.RubinivForStartARENA1.ToString();
			if (Progress.shop.Arena1MaxDistance > 0)
			{
				best.gameObject.transform.parent.gameObject.SetActive(value: true);
			}
			else
			{
				best.gameObject.transform.parent.gameObject.SetActive(value: false);
			}
			best.text = Progress.shop.Arena1MaxDistance.ToString();
			best2.text = DifficultyConfig.instance.MetrivForARENA1.ToString() + "m.";
		}
		else if (Progress.levels.active_pack_last_openned == 2)
		{
			slider.maxValue = DifficultyConfig.instance.MetrivForARENA2;
			price.text = "-" + DifficultyConfig.instance.RubinivForStartARENA2.ToString();
			slider.value = Progress.shop.Arena2MaxDistance;
			if (Progress.shop.Arena2MaxDistance > 0)
			{
				best.gameObject.transform.parent.gameObject.SetActive(value: true);
			}
			else
			{
				best.gameObject.transform.parent.gameObject.SetActive(value: false);
			}
			best.text = Progress.shop.Arena2MaxDistance.ToString();
			best2.text = DifficultyConfig.instance.MetrivForARENA2.ToString() + "m.";
		}
		else if (Progress.levels.active_pack_last_openned == 3)
		{
			slider.maxValue = DifficultyConfig.instance.MetrivForARENA3;
			price.text = "-" + DifficultyConfig.instance.RubinivForStartARENA3.ToString();
			slider.value = Progress.shop.Arena3MaxDistance;
			if (Progress.shop.Arena3MaxDistance > 0)
			{
				best.gameObject.transform.parent.gameObject.SetActive(value: true);
			}
			else
			{
				best.gameObject.transform.parent.gameObject.SetActive(value: false);
			}
			best.text = Progress.shop.Arena3MaxDistance.ToString();
			best2.text = DifficultyConfig.instance.MetrivForARENA3.ToString() + "m.";
		}
		AnimShowHide.SetBool(isON, value: true);
		StartCoroutine(TEst());
	}

	private void OnDisable()
	{
		Exit.onClick.RemoveAllListeners();
		ForRealMoney.onClick.RemoveAllListeners();
		if (Progress.levels.InUndeground)
		{
			Progress.levels.active_level_last_openned_under = Progress.levels.Max_Active_Level_under;
			Progress.levels.active_pack_last_openned_under = Progress.levels.Max_Active_Pack_under;
		}
		else
		{
			Progress.levels.active_level_last_openned = Progress.levels.Max_Active_Level;
			Progress.levels.active_pack_last_openned = Progress.levels.Max_Active_Pack;
		}
	}

	private void ClicExit()
	{
		PlayVideo.gameObject.SetActive(value: false);
		Progress.shop.ArenaBrifOpen = false;
		ControllerForPlayButton.Act = ControllerForPlayButton.ActTemp;
		Progress.levels.active_level_last_openned = Progress.levels.Max_Active_Level;
		Progress.levels.active_pack_last_openned = Progress.levels.Max_Active_Pack;
		AnimShowHide.SetBool(isON, value: false);
		StartCoroutine(DelayToClose());
	}

	private IEnumerator DelayToClose()
	{
		yield return new WaitForSeconds(0.5f);
		base.gameObject.SetActive(value: false);
	}

	private void ClicPremium()
	{
		ByPremiumCar2();
	}

	private void ByPremiumCar2()
	{
		if (DebugFacade.isDebug)
		{
			ModalWindow.instance.Show("Debug buy Premium Car2", new ButtonInfo("Buy", ByPC2));
		}
		else
		{
			//Purchaser.BuyProductID(Purchaser.PremiumCar2, ByPC2);
		}
	}

	private void ByPC2()
	{
		AnalyticsManager.LogEvent(EventCategoty.premium_shop, "jason", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
		Progress.shop.BuyForRealMoney = true;
		Progress.shop.Cars[4].equipped = true;
	}

	private IEnumerator TEst()
	{
		ContentSizeFitter q = Play.GetComponentInChildren<ContentSizeFitter>();
		ContentSizeFitter z = PlayVideo.GetComponentInChildren<ContentSizeFitter>();
		q.enabled = false;
		z.enabled = false;
		yield return 0;
		q.enabled = true;
		z.enabled = true;
		Play.gameObject.SetActive(value: true);
		if (Progress.levels.active_pack_last_openned == 1)
		{
			Badge.count = DifficultyConfig.instance.BudgesARENA1.ToString();
			if (temp >= DifficultyConfig.instance.BudgesARENA1)
			{
				ClosePlay.SetActive(value: false);
				Play.gameObject.SetActive(value: true);
				PlayVideo.gameObject.SetActive(value: true);
			}
			else
			{
				ClosePlay.SetActive(value: true);
				Play.gameObject.SetActive(value: false);
				PlayVideo.gameObject.SetActive(value: false);
			}
		}
		else if (Progress.levels.active_pack_last_openned == 2)
		{
			Badge.count = DifficultyConfig.instance.BudgesARENA2.ToString();
			if (temp >= DifficultyConfig.instance.BudgesARENA2)
			{
				ClosePlay.SetActive(value: false);
				Play.gameObject.SetActive(value: true);
				PlayVideo.gameObject.SetActive(value: false);
			}
			else
			{
				ClosePlay.SetActive(value: true);
				Play.gameObject.SetActive(value: false);
				PlayVideo.gameObject.SetActive(value: false);
			}
		}
		else if (Progress.levels.active_pack_last_openned == 3)
		{
			Badge.count = DifficultyConfig.instance.BudgesARENA3.ToString();
			if (temp >= DifficultyConfig.instance.BudgesARENA3)
			{
				ClosePlay.SetActive(value: false);
				Play.gameObject.SetActive(value: true);
				PlayVideo.gameObject.SetActive(value: false);
			}
			else
			{
				ClosePlay.SetActive(value: true);
				Play.gameObject.SetActive(value: false);
				PlayVideo.gameObject.SetActive(value: false);
			}
		}
	}

	private void Update()
	{
	}
}
