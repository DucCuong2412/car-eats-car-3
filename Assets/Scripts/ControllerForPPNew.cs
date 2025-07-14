using UnityEngine;

public class ControllerForPPNew : MonoBehaviour
{
	public GameObject GO;

	private void Start()
	{
		if (!Progress.shop.NeverShowPP && Progress.shop.showPP && !Progress.shop.Tutorial)
		{
			GO.SetActive(value: true);
			Game.OnStateChange(Game.gameState.OpenWindow);
		}
	}

	public void PP()
	{
		string privacyPolicy = PriceConfig.instance.PrivasyPolicy.PrivacyPolicy;
		Application.OpenURL(privacyPolicy);
	}

	public void TOU()
	{
		string tempOfUse = PriceConfig.instance.PrivasyPolicy.TempOfUse;
		Application.OpenURL(tempOfUse);
	}

	public void Acept()
	{
		Game.OnStateChange(Game.gameState.Levels);
		Progress.shop.showPP = false;
		Progress.shop.NeverShowPP = true;
		base.gameObject.SetActive(value: false);
	}
}
