using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForCoinFuelShowPremiumWindow : MonoBehaviour
{
	private PremiumShopNew _shopWindowModel;

	public LevelGalleryCanvasView LGCV;

	public void ShowBuyCanvasWindow(bool isCoins = false)
	{
		StartCoroutine(LoadBuyCanvasWindow(isCoins));
	}

	public IEnumerator LoadBuyCanvasWindow(bool isCoins = false, Action CloseShop = null)
	{
		if (LGCV != null)
		{
			LGCV.FACVOM.enabled = false;
			LGCV.FACVOM._btnFirstVideoCoin.SetActive(value: false);
			LGCV.FACVOM._btnFirstVideoFuel.SetActive(value: false);
		}
		if (_shopWindowModel == null)
		{
			yield return StartCoroutine(InitShopCanvasWindows());
		}
		if (!isCoins)
		{
			ShowGetCoinsAndFuel();
		}
		else
		{
			ShowGetCoinsAndFuel(isCoinsPanel: true);
		}
	}

	private IEnumerator InitShopCanvasWindows()
	{
		SceneManager.LoadScene("premium_shop", LoadSceneMode.Additive);
		yield return 0;
		GameObject go = UnityEngine.Object.FindObjectOfType<PremiumShopNew>().gameObject;
		PremiumShopNew view = _shopWindowModel = go.GetComponent<PremiumShopNew>();
	}

	private void ShowGetCoinsAndFuel(bool isCoinsPanel = false)
	{
		_shopWindowModel.gameObject.SetActive(value: true);
		if (!isCoinsPanel)
		{
			_shopWindowModel.openFuel();
		}
	}
}
