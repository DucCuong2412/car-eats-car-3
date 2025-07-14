using SmartLocalization;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartArenaForButton : MonoBehaviour
{
	public Button btnBrifingGo;

	public Text txtEnergy;

	public Text arenanamer1;

	public Text ForLocal;

	private bool started;

	private bool IsEnoughFuel => GameEnergyLogic.isEnoughForRace;

	private void Update()
	{
		arenanamer1.text = LanguageManager.Instance.GetTextValue("PART */4").Replace("*", Progress.levels.arena_pack.ToString());
		ForLocal.text = LanguageManager.Instance.GetTextValue("REACH * M. TO GET TANKOMINATOR PART */4").Replace("*", "3000");
	}

	private void OnEnable()
	{
		Game.OnStateChange(Game.gameState.OpenWindow);
		started = false;
		btnBrifingGo.onClick.AddListener(OnButtonClick);
	}

	public void OnButtonClick()
	{
		OnAvailableClick();
		Audio.PlayAsync("gui_button_02_sn");
	}

	private void OnAvailableClick()
	{
		if (!started)
		{
			TakeFuelAndGo();
		}
	}

	private void TakeFuelAndGo()
	{
		int eachStart = GameEnergyLogic.instance.energyConfig.eachStart;
		if (!Progress.shop.endlessLevel)
		{
			PriceConfig.instance.energy.eachStart = 5;
			GameEnergyLogic.instance.energyConfig.eachStart = PriceConfig.instance.energy.eachStart;
		}
		else
		{
			PriceConfig.instance.energy.eachStart = 10;
			GameEnergyLogic.instance.energyConfig.eachStart = PriceConfig.instance.energy.eachStart;
		}
		if (IsEnoughFuel)
		{
			started = true;
			StartCoroutine(AnimateFuel());
		}
		else
		{
			NotEnoughFuel();
		}
		GameEnergyLogic.instance.energyConfig.eachStart = eachStart;
		PriceConfig.instance.energy.eachStart = eachStart;
	}

	private int GetFuelForRace()
	{
		GameEnergyLogic.GetFuelForRace();
		return PriceConfig.instance.energy.eachStart;
	}

	private IEnumerator AnimateFuel()
	{
		Audio.Play("fuel-1");
		GetFuelForRace();
		txtEnergy.text = GameEnergyLogic.GetEnergy.ToString();
		GameObject anim = UnityEngine.Object.Instantiate(txtEnergy.gameObject);
		Text text = anim.GetComponent<Text>();
		text.text = $"-{PriceConfig.instance.energy.eachStart}";
		text.rectTransform.SetParent(txtEnergy.rectTransform.parent);
		text.rectTransform.localScale = txtEnergy.rectTransform.localScale;
		text.transform.position = txtEnergy.transform.position;
		float dx = 0f;
		while (dx < 50f)
		{
			text.rectTransform.anchoredPosition = txtEnergy.rectTransform.anchoredPosition - Vector2.up * dx;
			dx += 0.5f;
			yield return null;
		}
		UnityEngine.SceneManagement.SceneManager.LoadScene("Race");
	}

	private void NotEnoughFuel()
	{
		GUI_shop.instance.ShowBuyCanvasWindow();
	}
}
