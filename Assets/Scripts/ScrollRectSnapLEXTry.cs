//using CompleteProject;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollRectSnapLEXTry : MonoBehaviour
{
	public RectTransform panel;

	public Button[] bttn;

	public RectTransform center;

	public Button left;

	public Button right;

	public float Larping = 5f;

	public float DistToNext = 50f;

	private int goToCardX = -1;

	private float[] distance;

	private bool dragging;

	private int bttnDistance;

	private int minButtonNum;

	private int hash_car_isUnlocked = Animator.StringToHash("car_isUnlocked");

	private int hash_car_isActivated = Animator.StringToHash("car_isActivated");

	private int hash_isDrones = Animator.StringToHash("isDrones");

	private int hash_isScrolled = Animator.StringToHash("isScrolled");

	private int hash_categoryArmorON = Animator.StringToHash("categoryArmorON");

	private int hash_categoryTurboON = Animator.StringToHash("categoryTurboON");

	private int hash_categorySpeedON = Animator.StringToHash("categorySpeedON");

	private int hash_categoryDamageON = Animator.StringToHash("categoryDamageON");

	private int hash_categoryGadgetsON = Animator.StringToHash("categoryGadgetsON");

	private int hash_categoryBombsON = Animator.StringToHash("categoryBombsON");

	private int hash_garageON = Animator.StringToHash("garageON");

	private bool Stoped;

	public ControllerMarkerEggs CME;

	private int startCard = -1;

	private int startStartCard = -1;

	public float zaderjka;

	public EventSystem SR;

	public bool CarList;

	public ControllerGarageDrones CGD;

	public Text priceFranc;

	public Text priceKroll;

	public ControllerGarage CG;

	public MarkerNewCar MNC;

	private void Start()
	{
		int num = bttn.Length;
		distance = new float[num];
		if (Progress.shop.activeCar >= 0)
		{
			SetPanelNum(Progress.shop.activeCar + 2);
		}
		else
		{
			SetPanelNum(1);
		}
		if (Progress.shop.GotoGarageOutIncubator)
		{
			Progress.shop.GotoGarageOutIncubator = false;
			switch (Progress.shop.Incubator_CurrentEggNum)
			{
			case 0:
				SetPanelNum(10);
				break;
			case 1:
				SetPanelNum(12);
				break;
			case 2:
				SetPanelNum(11);
				break;
			case 3:
				SetPanelNum(0);
				break;
			case 5:
				SetPanelNum(15);
				break;
			case 4:
				SetPanelNum(14);
				break;
			}
		}
		Vector2 anchoredPosition = bttn[1].GetComponent<RectTransform>().anchoredPosition;
		float x = anchoredPosition.x;
		Vector2 anchoredPosition2 = bttn[0].GetComponent<RectTransform>().anchoredPosition;
		bttnDistance = (int)Mathf.Abs(x - anchoredPosition2.x);
		if (Progress.shop.activeCar == 13)
		{
			left.gameObject.SetActive(value: true);
			right.gameObject.SetActive(value: false);
		}
	}

	private void OnEnable()
	{
		if (Progress.shop.TutorialGarage)
		{
			left.gameObject.SetActive(value: false);
			right.gameObject.SetActive(value: false);
		}
		left.onClick.AddListener(delegate
		{
			SetgoToCardX(right: false);
		});
		right.onClick.AddListener(delegate
		{
			SetgoToCardX();
		});
	}

	private void OnDisable()
	{
		left.onClick.RemoveAllListeners();
		right.onClick.RemoveAllListeners();
	}

	public bool pressBut(int num)
	{
		minButtonNum = GetMinButtonNum();
		if (num == minButtonNum)
		{
			return false;
		}
		if (num > minButtonNum)
		{
			SetgoToCardX();
			return true;
		}
		if (num < minButtonNum)
		{
			SetgoToCardX(right: false);
			return true;
		}
		return false;
	}

	public void SetPanelNum(int LastLigNum)
	{
		StartCoroutine(timeOut(LastLigNum));
	}

	private IEnumerator timeOut(int LastLigNum = 0)
	{
		dragging = true;
		int t = 2;
		while (t > 0)
		{
			RectTransform rectTransform = panel;
			float x = LastLigNum * -bttnDistance;
			Vector2 anchoredPosition = panel.anchoredPosition;
			rectTransform.anchoredPosition = new Vector2(x, anchoredPosition.y);
			t--;
			yield return null;
		}
		dragging = false;
		StartCoroutine(testCarut(test: true));
		CGD.Updaters();
	}

	public void SetgoToCardX(bool right = true, int numBut = -1)
	{
		minButtonNum = GetMinButtonNum();
		if (numBut != -1)
		{
			minButtonNum = numBut - 1;
		}
		dragging = true;
		if (minButtonNum < 15 && right)
		{
			goToCardX = (minButtonNum + 1) * bttnDistance;
		}
		else if (minButtonNum > 0 && !right)
		{
			goToCardX = (minButtonNum - 1) * bttnDistance;
		}
		StartCoroutine(testCarut(test: true));
		CGD.Updaters();
	}

	private void Update()
	{
		GetMinButtonNum();
		if (!dragging)
		{
			if (goToCardX != -1)
			{
				goToCardX = -1;
			}
			LerpToBttn(minButtonNum * -bttnDistance);
		}
		if (goToCardX >= 0)
		{
			LerpToBttn(-goToCardX);
			Vector2 anchoredPosition = panel.anchoredPosition;
			if (anchoredPosition.x - 2f < (float)(-goToCardX))
			{
				Vector2 anchoredPosition2 = panel.anchoredPosition;
				if (anchoredPosition2.x + 2f > (float)(-goToCardX))
				{
					RectTransform rectTransform = panel;
					float x = -goToCardX;
					Vector2 anchoredPosition3 = panel.anchoredPosition;
					rectTransform.anchoredPosition = new Vector2(x, anchoredPosition3.y);
					goToCardX = -1;
					dragging = false;
				}
			}
		}
		Vector2 anchoredPosition4 = panel.anchoredPosition;
		if (anchoredPosition4.x >= 114f && dragging)
		{
			RectTransform rectTransform2 = panel;
			Vector2 anchoredPosition5 = panel.anchoredPosition;
			rectTransform2.anchoredPosition = new Vector2(114f, anchoredPosition5.y);
		}
		else
		{
			Vector2 anchoredPosition6 = panel.anchoredPosition;
			if (anchoredPosition6.x < -13014f && dragging)
			{
				RectTransform rectTransform3 = panel;
				Vector2 anchoredPosition7 = panel.anchoredPosition;
				rectTransform3.anchoredPosition = new Vector2(-13014f, anchoredPosition7.y);
			}
		}
		if (Progress.shop.activeCar > 1)
		{
			CG.animator.SetBool(hash_isDrones, value: false);
		}
	}

	public int GetMinButtonNum()
	{
		for (int i = 0; i < bttn.Length; i++)
		{
			float[] array = distance;
			int num = i;
			Vector3 position = center.transform.position;
			float x = position.x;
			Vector3 position2 = bttn[i].transform.position;
			array[num] = Mathf.Abs(x - position2.x);
		}
		float num2 = Mathf.Min(distance);
		for (int j = 0; j < bttn.Length; j++)
		{
			if (num2 == distance[j])
			{
				minButtonNum = j;
			}
		}
		if (minButtonNum == startCard)
		{
			startCard = -1;
		}
		if (startCard != -1)
		{
			minButtonNum = startCard;
		}
		return minButtonNum;
	}

	private void LerpToBttn(int position)
	{
		Vector2 anchoredPosition = panel.anchoredPosition;
		float num = Mathf.Lerp(anchoredPosition.x, position, Time.deltaTime * Larping);
		float x = num;
		Vector2 anchoredPosition2 = panel.anchoredPosition;
		Vector2 anchoredPosition3 = new Vector2(x, anchoredPosition2.y);
		panel.anchoredPosition = anchoredPosition3;
	}

	public void StartDrag()
	{
		goToCardX = -1;
		dragging = true;
		startCard = -1;
		startStartCard = GetMinButtonNum();
	}

	private IEnumerator EndDragCorut()
	{
		SR.enabled = false;
		yield return new WaitForSeconds(zaderjka);
		SR.enabled = true;
	}

	public void EndDrag()
	{
		startCard = -1;
		startCard = GetMinButtonNum();
		if (!CarList)
		{
			if (startCard == -1)
			{
				goto IL_01da;
			}
			Vector3 position = bttn[startCard].transform.position;
			if (!(position.x > DistToNext))
			{
				Vector3 position2 = bttn[startCard].transform.position;
				if (!(position2.x < 0f - DistToNext))
				{
					goto IL_01da;
				}
			}
			Vector3 position3 = bttn[startCard].transform.position;
			if (position3.x - 480f > DistToNext)
			{
				startCard--;
			}
			else
			{
				Vector3 position4 = bttn[startCard].transform.position;
				if (position4.x - 480f < 0f - DistToNext)
				{
					startCard++;
				}
			}
			if (startCard < 0 || startCard > 13)
			{
				startCard = -1;
			}
			if (startStartCard == startCard)
			{
				Vector3 position5 = bttn[startCard].transform.position;
				if (position5.x - 480f > DistToNext)
				{
					startCard--;
				}
				else
				{
					Vector3 position6 = bttn[startCard].transform.position;
					if (position6.x - 480f < 0f - DistToNext)
					{
						startCard++;
					}
				}
				if (startCard < 0 || startCard > 2)
				{
					startCard = -1;
				}
			}
		}
		goto IL_01e1;
		IL_01da:
		startCard = -1;
		goto IL_01e1;
		IL_01e1:
		CarList = false;
		dragging = false;
		startStartCard = -1;
		StartCoroutine(testCarut());
		CGD.Updaters();
		StartCoroutine(EndDragCorut());
		if (GetMinButtonNum() == 0)
		{
			CME.Checks();
		}
		Progress.shop.activeCar = GetMinButtonNum() - 2;
		OffOtherCars();
	}

	private IEnumerator testCarut(bool test = false)
	{
		if (test)
		{
			yield return new WaitForSeconds(0.1f);
		}
		int temp = Progress.shop.activeCar;
		int temps = GetMinButtonNum() - 2;
		switch (temps)
		{
		case -2:
			left.gameObject.SetActive(value: false);
			right.gameObject.SetActive(value: true);
			break;
		case 13:
			left.gameObject.SetActive(value: true);
			right.gameObject.SetActive(value: false);
			break;
		default:
			left.gameObject.SetActive(value: true);
			right.gameObject.SetActive(value: true);
			break;
		}
		if (temps <= 13)
		{
			Progress.shop.activeCar = temps;
		}
		OffOtherCars();
		if (Progress.shop.activeCar != temp)
		{
			CG.btn_UNLOCK_RUBY.SetActive(value: true);
			if (Progress.shop.activeCar == 1)
			{
				Progress.shop.Cars[Progress.shop.activeCar].bought = Progress.shop.BossDeath1;
				CG.unlockPrice.text = ShopManagerPrice.instance.Price.CarByu1.ToString();
			}
			if (Progress.shop.activeCar == 2)
			{
				Progress.shop.Cars[Progress.shop.activeCar].bought = Progress.shop.BossDeath2;
				CG.unlockPrice.text = ShopManagerPrice.instance.Price.CarByu2.ToString();
			}
			if (Progress.shop.activeCar == 3)
			{
				//string text = Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar).metadata.localizedPrice.ToString();
				//text = ((!string.IsNullOrEmpty(text)) ? text : ShopManagerPrice.instance.Price.CarByu3);
				//CG.unlockPricePremium.text = text;
				CG.btn_UNLOCK_RUBY.SetActive(value: false);
			}
			if (Progress.shop.activeCar == 4)
			{
				//string text2 = Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar2).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar2).metadata.localizedPrice.ToString();
				//text2 = ((!string.IsNullOrEmpty(text2)) ? text2 : ShopManagerPrice.instance.Price.CarByu4);
				//CG.unlockPricePremium2.text = text2;
				CG.btn_UNLOCK_RUBY.SetActive(value: false);
			}
			if (Progress.shop.activeCar == 5)
			{
				//string text3 = Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar3).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar3).metadata.localizedPrice.ToString();
				//text3 = ((!string.IsNullOrEmpty(text3)) ? text3 : ShopManagerPrice.instance.Price.CarByu5);
				//CG.unlockPricePremium3.text = text3;
				CG.btn_UNLOCK_RUBY.SetActive(value: false);
			}
			if (Progress.shop.activeCar == 6)
			{
				//string text4 = Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar4).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar4).metadata.localizedPrice.ToString();
				//text4 = ((!string.IsNullOrEmpty(text4)) ? text4 : ShopManagerPrice.instance.Price.CarByu5);
				//priceFranc.text = text4;
				if (Progress.shop.Get1partForPoliceCar && Progress.shop.Get2partForPoliceCar && Progress.shop.Get3partForPoliceCar && Progress.shop.Get4partForPoliceCar)
				{
					Progress.shop.Cars[Progress.shop.activeCar].bought = true;
					Progress.shop.Cars[Progress.shop.activeCar].equipped = true;
				}
				else
				{
					Progress.shop.Cars[Progress.shop.activeCar].bought = false;
					Progress.shop.Cars[Progress.shop.activeCar].equipped = false;
				}
			}
			if (Progress.shop.activeCar == 7)
			{
				//string text5 = Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar5).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar5).metadata.localizedPrice.ToString();
				//text5 = ((!string.IsNullOrEmpty(text5)) ? text5 : ShopManagerPrice.instance.Price.CarByu5);
				//priceFranc.text = text5;
				if (Progress.shop.Get1partForPoliceCar2 && Progress.shop.Get2partForPoliceCar2 && Progress.shop.Get3partForPoliceCar2 && Progress.shop.Get4partForPoliceCar2)
				{
					Progress.shop.Cars[Progress.shop.activeCar].bought = true;
					Progress.shop.Cars[Progress.shop.activeCar].equipped = true;
				}
				else
				{
					Progress.shop.Cars[Progress.shop.activeCar].bought = false;
					Progress.shop.Cars[Progress.shop.activeCar].equipped = false;
				}
			}
			if (Progress.shop.activeCar == 11)
			{
				//string text6 = Purchaser.m_StoreController.products.WithID(Purchaser.Tankominator).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.Tankominator).metadata.localizedPrice.ToString();
				//text6 = ((!string.IsNullOrEmpty(text6)) ? text6 : ShopManagerPrice.instance.Price.CarByu5);
				//priceFranc.text = text6;
				if (Progress.shop.Get1partForPoliceCar3 && Progress.shop.Get2partForPoliceCar3 && Progress.shop.Get3partForPoliceCar3 && Progress.shop.Get4partForPoliceCar3)
				{
					Progress.shop.Cars[Progress.shop.activeCar].bought = true;
					Progress.shop.Cars[Progress.shop.activeCar].equipped = true;
				}
				else
				{
					Progress.shop.Cars[Progress.shop.activeCar].bought = false;
					Progress.shop.Cars[Progress.shop.activeCar].equipped = false;
				}
			}
			if (Progress.shop.activeCar == 8)
			{
				//string text7 = Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar6).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar6).metadata.localizedPrice.ToString();
				//text7 = ((!string.IsNullOrEmpty(text7)) ? text7 : ShopManagerPrice.instance.Price.CarByu5);
				//priceKroll.text = text7;
			}
			if (Progress.shop.activeCar == 9)
			{
				//	string text8 = Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar9).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar9).metadata.localizedPrice.ToString();
				//	text8 = ((!string.IsNullOrEmpty(text8)) ? text8 : ShopManagerPrice.instance.Price.CarByu9);
				//	priceFranc.text = text8;
			}
			if (Progress.shop.activeCar == 10)
			{
				//string text9 = Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar10).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar10).metadata.localizedPrice.ToString();
				//text9 = ((!string.IsNullOrEmpty(text9)) ? text9 : ShopManagerPrice.instance.Price.CarByu10);
				//priceFranc.text = text9;
			}
			if (Progress.shop.activeCar == 12)
			{
				//string text10 = Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar10).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar10).metadata.localizedPrice.ToString();
				//text10 = ((!string.IsNullOrEmpty(text10)) ? text10 : ShopManagerPrice.instance.Price.CarByu10);
				//priceFranc.text = text10;
			}
			if (Progress.shop.activeCar == 13)
			{
				//string text11 = Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar10).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.PremiumCar10).metadata.localizedPrice.ToString();
				//text11 = ((!string.IsNullOrEmpty(text11)) ? text11 : ShopManagerPrice.instance.Price.CarByu10);
				//priceFranc.text = text11;
			}
			if (temps >= 0)
			{
				if (Progress.shop.activeCar == 6 || Progress.shop.activeCar == 7 || Progress.shop.activeCar == 9 || Progress.shop.activeCar == 10 || Progress.shop.activeCar == 11 || Progress.shop.activeCar == 12 || Progress.shop.activeCar == 13)
				{
					CG.animator.SetBool(hash_car_isUnlocked, value: true);
					CG.animator.SetBool(hash_car_isActivated, Progress.shop.Cars[Progress.shop.activeCar].equipped);
				}
				else
				{
					CG.animator.SetBool(hash_car_isUnlocked, Progress.shop.Cars[Progress.shop.activeCar].bought);
					CG.animator.SetBool(hash_car_isActivated, Progress.shop.Cars[Progress.shop.activeCar].equipped);
				}
				bool q = CG.animator.GetBool(hash_isDrones);
				if (!Progress.shop.Cars[Progress.shop.activeCar].equipped)
				{
					CG.animator.SetBool(hash_isScrolled, value: true);
					yield return new WaitForSeconds(0.2f);
					CG.animator.SetBool(hash_isScrolled, value: false);
				}
				else if (!q)
				{
					CG.animator.SetBool(hash_categoryArmorON, value: false);
					CG.animator.SetBool(hash_categoryTurboON, value: false);
					CG.animator.SetBool(hash_categorySpeedON, value: false);
					CG.animator.SetBool(hash_categoryDamageON, value: false);
					CG.animator.SetBool(hash_categoryGadgetsON, value: false);
					CG.animator.SetBool(hash_categoryBombsON, value: false);
					CG.animator.SetBool(hash_isScrolled, value: true);
					yield return new WaitForSeconds(0.2f);
					CG.animator.SetBool(hash_isScrolled, value: false);
				}
				if (q)
				{
					CG.animator.SetBool("isNextDron", value: false);
					CG.animator.SetBool(hash_isDrones, value: false);
				}
				CG.MG.up();
				MNC.change();
			}
			else
			{
				if (CG.animator.GetBool(hash_isDrones))
				{
					CG.animator.SetBool("isNextDron", value: true);
				}
				CG.animator.SetBool(hash_isDrones, value: true);
				if (CG.animator.GetBool(hash_categoryArmorON) || CG.animator.GetBool(hash_categoryTurboON) || CG.animator.GetBool(hash_categorySpeedON) || CG.animator.GetBool(hash_categoryDamageON) || CG.animator.GetBool(hash_categoryGadgetsON) || CG.animator.GetBool(hash_categoryBombsON))
				{
					CG.animator.SetBool(hash_isScrolled, value: true);
					yield return new WaitForSeconds(0.1f);
				}
				CG.animator.SetBool(hash_categoryArmorON, value: false);
				CG.animator.SetBool(hash_categoryTurboON, value: false);
				CG.animator.SetBool(hash_categorySpeedON, value: false);
				CG.animator.SetBool(hash_categoryDamageON, value: false);
				CG.animator.SetBool(hash_categoryGadgetsON, value: false);
				CG.animator.SetBool(hash_categoryBombsON, value: false);
				if (CG.animator.GetBool(hash_garageON))
				{
					yield return new WaitForSeconds(0.3f);
					CG.animator.SetBool(hash_isScrolled, value: true);
				}
				yield return new WaitForSeconds(0.2f);
				CG.animator.SetBool(hash_isScrolled, value: false);
				MNC.change();
			}
			if ((Progress.shop.activeCar == 3 || Progress.shop.activeCar == 4 || Progress.shop.activeCar == 5 || Progress.shop.activeCar == 6 || Progress.shop.activeCar == 7 || Progress.shop.activeCar == 8 || Progress.shop.activeCar == 9 || Progress.shop.activeCar == 10 || Progress.shop.activeCar == 11 || Progress.shop.activeCar == 12 || Progress.shop.activeCar == 13) && !Progress.shop.Cars[Progress.shop.activeCar].equipped)
			{
				CG.animator.CrossFade("bodov_garage_Unlocked_ON", 0f);
			}
		}
		if (temps == 0 && CG.animator.GetBool(hash_isDrones))
		{
			CG.animator.SetBool("isNextDron", value: false);
			CG.animator.SetBool(hash_isDrones, value: false);
		}
		if (GetMinButtonNum() == 0)
		{
			CME.Checks();
		}
		Stoped = true;
		yield return new WaitForSeconds(1f);
		Stoped = false;
	}

	private void OffOtherCars()
	{
		int num = bttn.Length;
		for (int i = 0; i < num; i++)
		{
			if (Progress.shop.activeCar + 1 != i && Progress.shop.activeCar + 2 != i && Progress.shop.activeCar + 3 != i)
			{
				bttn[i].gameObject.SetActive(value: false);
			}
			else
			{
				bttn[i].gameObject.SetActive(value: true);
			}
		}
		StartCoroutine(Delay());
	}

	private IEnumerator Delay()
	{
		CG.gadget.GadgetsAnimatorsOff();
		yield return null;
		yield return null;
		CG.gadget.GadgetsAnimatorsOff();
	}
}
