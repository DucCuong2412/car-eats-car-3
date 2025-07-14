using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SloderForPremium : MonoBehaviour
{
	public Button BTN;

	public ShopModel model;

	public ShopView view;

	private bool chek = true;

	private void OnEnable()
	{
		BTN.onClick.AddListener(click);
		BTN.gameObject.SetActive(Progress.shop.isFirst);
		if (Progress.shop.isFirst)
		{
			StartCoroutine(slider());
		}
	}

	private void OnDisable()
	{
		BTN.onClick.RemoveAllListeners();
	}

	private void click()
	{
		Progress.shop.isFirst = false;
		chek = false;
		BTN.gameObject.SetActive(Progress.shop.isFirst);
	}

	private IEnumerator slider()
	{
		yield return new WaitForSeconds(4f);
		while (chek)
		{
			view.ChangePremium(1);
			yield return new WaitForSeconds(2f);
		}
	}
}
