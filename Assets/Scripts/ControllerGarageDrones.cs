using CompleteProject;
using Smokoko.DebugModule;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerGarageDrones : MonoBehaviour
{
	public Animator gate1Bee;

	public Animator gate2Bomb;

	public Animator gate3Fly;

	private int _isLocked = Animator.StringToHash("isLocked");

	private int _IsUnlockShop = Animator.StringToHash("IsUnlockShop");

	public Button BuyBee;

	public Button EquipBee;

	public Button UnEquipBee;

	public Button BuyBomb;

	public Button EquipBomb;

	public Button UnEquipBomb;

	public Text PriceBee;

	public Text PriceBomber;

	public Text PriceFly;

	public Button gotoIncubator;

	public Button byuFly;

	public ScrollRectSnapLEXTry scroll;

	public ControllerGarage CG;

	private void OnEnable()
	{
		byuFly.onClick.AddListener(delegate
		{
			if (PriceConfig.instance.currency.priceDroneFly <= Progress.shop.currency)
			{
				Progress.shop.currency -= PriceConfig.instance.currency.priceDroneFly;
				Progress.shop.dronFireEggBuy = true;
				Progress.shop.Incubator_Eggs[3] = true;
				gotoIncubator.gameObject.SetActive(value: true);
				EquipBomb.gameObject.SetActive(value: false);
				UnEquipBomb.gameObject.SetActive(value: false);
				byuFly.gameObject.SetActive(value: false);
			}
			else
			{
				CG.ShowBuyCanvasWindow(isCoins: true);
			}
		});
		gotoIncubator.onClick.AddListener(delegate
		{
			SceneManager.LoadScene("scene_incubator");
		});
		BuyBee.onClick.AddListener(BuyBeeClic);
		EquipBee.onClick.AddListener(EquipBeeClic);
		UnEquipBee.onClick.AddListener(UnEquipClic);
		BuyBomb.onClick.AddListener(BuyBombClic);
		EquipBomb.onClick.AddListener(EquipBombClic);
		UnEquipBomb.onClick.AddListener(UnEquipBombClic);
		PriceFly.text = PriceConfig.instance.currency.priceDroneFly.ToString();
		if (Progress.shop.TakeDrone)
		{
			gate2Bomb.SetBool(_isLocked, value: false);
			Progress.shop.dronBombsBuy = true;
			BuyBomb.gameObject.SetActive(value: false);
			EquipBomb.gameObject.SetActive(value: false);
			UnEquipBomb.gameObject.SetActive(value: true);
			EquipBombClic();
		}
		Change();
	}

	private void OnDisable()
	{
		BuyBee.onClick.RemoveAllListeners();
		EquipBee.onClick.RemoveAllListeners();
		UnEquipBee.onClick.RemoveAllListeners();
		BuyBomb.onClick.RemoveAllListeners();
		EquipBomb.onClick.RemoveAllListeners();
		UnEquipBomb.onClick.RemoveAllListeners();
	}

	private void BuyBeeClic()
	{
		ByBee();
	}

	private void ByBee()
	{
		if (DebugFacade.isDebug)
		{
			ModalWindow.instance.Show("Debug buy DroneBee", new ButtonInfo("Buy", ByDroneBee));
		}
		else
		{
			Purchaser.BuyProductID(Purchaser.DroneBee, ByDroneBee);
		}
	}

	private void ByDroneBee()
	{
		Progress.shop.BuyForRealMoney = true;
		AnalyticsManager.LogEvent(EventCategoty.premium_shop, "premium_drone1_beeranha", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
		EquipBeeClic();
		Progress.shop.dronBeeBuy = true;
		gate1Bee.SetBool(_isLocked, value: false);
		BuyBee.gameObject.SetActive(value: false);
		EquipBee.gameObject.SetActive(value: false);
		UnEquipBee.gameObject.SetActive(value: true);
	}

	private void EquipBeeClic()
	{
		Progress.shop.dronBeeActive = true;
		BuyBee.gameObject.SetActive(value: false);
		EquipBee.gameObject.SetActive(value: false);
		UnEquipBee.gameObject.SetActive(value: true);
	}

	private void UnEquipClic()
	{
		Progress.shop.dronBeeActive = false;
		BuyBee.gameObject.SetActive(value: false);
		EquipBee.gameObject.SetActive(value: true);
		UnEquipBee.gameObject.SetActive(value: false);
	}

	private void BuyBombClic()
	{
		ByBomber();
	}

	private void ByBomber()
	{
		if (DebugFacade.isDebug)
		{
			ModalWindow.instance.Show("Debug buy DroneBomber", new ButtonInfo("Buy", ByDroneBomber));
		}
		else
		{
			Purchaser.BuyProductID(Purchaser.DroneBomber, ByDroneBomber);
		}
	}

	private void ByDroneBomber()
	{
		AnalyticsManager.LogEvent(EventCategoty.premium_shop, "premium_drone1_bombastic", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
		Progress.shop.BuyForRealMoney = true;
		gate2Bomb.SetBool(_isLocked, value: false);
		Progress.shop.dronBombsBuy = true;
		BuyBomb.gameObject.SetActive(value: false);
		EquipBomb.gameObject.SetActive(value: false);
		UnEquipBomb.gameObject.SetActive(value: true);
		EquipBombClic();
	}

	private void EquipBombClic()
	{
		switch (scroll.GetMinButtonNum())
		{
		case 0:
			Progress.shop.dronFireActive = true;
			break;
		case 1:
			Progress.shop.dronBombsActive = true;
			break;
		}
		BuyBomb.gameObject.SetActive(value: false);
		EquipBomb.gameObject.SetActive(value: false);
		UnEquipBomb.gameObject.SetActive(value: true);
	}

	private void UnEquipBombClic()
	{
		switch (scroll.GetMinButtonNum())
		{
		case 0:
			Progress.shop.dronFireActive = false;
			break;
		case 1:
			Progress.shop.dronBombsActive = false;
			break;
		}
		BuyBomb.gameObject.SetActive(value: false);
		EquipBomb.gameObject.SetActive(value: true);
		UnEquipBomb.gameObject.SetActive(value: false);
	}

	private IEnumerator changeAnimator()
	{
		yield return 0;
		if (Progress.shop.dronBeeBuy)
		{
			gate1Bee.SetBool(_IsUnlockShop, value: true);
		}
		if (Progress.shop.dronBombsBuy)
		{
			gate2Bomb.SetBool(_IsUnlockShop, value: true);
		}
		if (Progress.shop.dronFireEggBuy)
		{
			gate3Fly.SetBool(_IsUnlockShop, value: true);
		}
		string BeePriceBee = Purchaser.m_StoreController.products.WithID(Purchaser.DroneBee).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.DroneBee).metadata.localizedPrice.ToString();
		string BomberPriceBomber = Purchaser.m_StoreController.products.WithID(Purchaser.DroneBomber).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.DroneBomber).metadata.localizedPrice.ToString();
		PriceBee.text = ((!string.IsNullOrEmpty(BeePriceBee)) ? BeePriceBee : PriceConfig.instance.premiumConten.DronBee.ToString());
		PriceBomber.text = ((!string.IsNullOrEmpty(BomberPriceBomber)) ? BomberPriceBomber : PriceConfig.instance.premiumConten.DronBomber.ToString());
	}

	private void Change()
	{
		StartCoroutine(changeAnimator());
		if (Progress.shop.dronBeeBuy && Progress.shop.dronBombsBuy)
		{
			BuyBee.gameObject.SetActive(value: false);
			EquipBee.gameObject.SetActive(value: false);
			UnEquipBee.gameObject.SetActive(value: true);
			BuyBomb.gameObject.SetActive(value: false);
			EquipBomb.gameObject.SetActive(value: false);
			UnEquipBomb.gameObject.SetActive(value: true);
		}
		else if (Progress.shop.dronBombsBuy)
		{
			if (Progress.shop.dronBeeBuy)
			{
				BuyBee.gameObject.SetActive(value: false);
				EquipBee.gameObject.SetActive(value: true);
				UnEquipBee.gameObject.SetActive(value: false);
			}
			else
			{
				BuyBee.gameObject.SetActive(value: true);
				EquipBee.gameObject.SetActive(value: false);
				UnEquipBee.gameObject.SetActive(value: false);
			}
			BuyBomb.gameObject.SetActive(value: false);
			EquipBomb.gameObject.SetActive(value: false);
			UnEquipBomb.gameObject.SetActive(value: true);
		}
		else if (Progress.shop.dronBeeBuy)
		{
			if (Progress.shop.dronBombsBuy)
			{
				BuyBomb.gameObject.SetActive(value: false);
				EquipBomb.gameObject.SetActive(value: true);
				UnEquipBomb.gameObject.SetActive(value: false);
			}
			else
			{
				BuyBomb.gameObject.SetActive(value: true);
				EquipBomb.gameObject.SetActive(value: false);
				UnEquipBomb.gameObject.SetActive(value: false);
			}
			BuyBee.gameObject.SetActive(value: false);
			EquipBee.gameObject.SetActive(value: false);
			UnEquipBee.gameObject.SetActive(value: true);
		}
		else
		{
			BuyBee.gameObject.SetActive(value: true);
			EquipBee.gameObject.SetActive(value: false);
			UnEquipBee.gameObject.SetActive(value: false);
			BuyBomb.gameObject.SetActive(value: true);
			EquipBomb.gameObject.SetActive(value: false);
			UnEquipBomb.gameObject.SetActive(value: false);
		}
	}

	public void Updaters()
	{
		StartCoroutine(upd());
	}

	private IEnumerator upd()
	{
		yield return new WaitForSeconds(0.5f);
		switch (scroll.GetMinButtonNum())
		{
		case 0:
			changeFly();
			break;
		case 1:
			BuyBee.gameObject.transform.parent.gameObject.SetActive(value: true);
			byuFly.gameObject.SetActive(value: false);
			gotoIncubator.gameObject.SetActive(value: false);
			Change();
			break;
		}
	}

	private void changeFly()
	{
		BuyBomb.gameObject.SetActive(value: false);
		BuyBee.gameObject.transform.parent.gameObject.SetActive(value: false);
		if (Progress.shop.dronFireEggBuy)
		{
			byuFly.gameObject.SetActive(value: false);
			if (Progress.shop.dronFireEvolFin)
			{
				gotoIncubator.gameObject.SetActive(value: false);
				if (Progress.shop.dronFireActive)
				{
					EquipBomb.gameObject.SetActive(value: false);
					UnEquipBomb.gameObject.SetActive(value: true);
				}
				else
				{
					EquipBomb.gameObject.SetActive(value: true);
					UnEquipBomb.gameObject.SetActive(value: false);
				}
			}
			else
			{
				gotoIncubator.gameObject.SetActive(value: true);
				EquipBomb.gameObject.SetActive(value: false);
				UnEquipBomb.gameObject.SetActive(value: false);
			}
		}
		else
		{
			byuFly.gameObject.SetActive(value: true);
			EquipBomb.gameObject.SetActive(value: false);
			UnEquipBomb.gameObject.SetActive(value: false);
		}
	}
}
