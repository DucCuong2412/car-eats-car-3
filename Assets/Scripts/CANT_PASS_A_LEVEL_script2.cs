using UnityEngine;
using UnityEngine.UI;

public class CANT_PASS_A_LEVEL_script2 : MonoBehaviour
{
	public Button restart;

	public Button close;

	public Text ruby;

	public Text fuel;

	public GameObject inf;

	public static void Init()
	{
		CANT_PASS_A_LEVEL_script2 component = (UnityEngine.Object.Instantiate(Resources.Load("GUI/CANT_PASS_A_LEVEL_script2")) as GameObject).GetComponent<CANT_PASS_A_LEVEL_script2>();
	}

	private void OnEnable()
	{
		ruby.text = Progress.shop.currency.ToString();
		inf.SetActive(Progress.gameEnergy.isInfinite);
		fuel.gameObject.SetActive(!Progress.gameEnergy.isInfinite);
		fuel.text = Progress.gameEnergy.energy.ToString();
		restart.onClick.AddListener(Restart_clic);
		close.onClick.AddListener(closeWindow);
	}

	private void OnDisable()
	{
		restart.onClick.RemoveAllListeners();
		close.onClick.RemoveAllListeners();
	}

	private void closeWindow()
	{
		base.gameObject.SetActive(value: false);
	}

	private void Restart_clic()
	{
		if (!GameEnergyLogic.isEnoughForRace)
		{
			GUI_shop.instance.ShowBuyCanvasWindow();
			return;
		}
		int activeCar = Progress.shop.activeCar;
		bool[] premiumEquipped = Progress.shop.Cars[1].premiumEquipped;
		Progress.shop.activeCar = 1;
		for (int i = 0; i < Progress.shop.Cars[1].premiumEquipped.Length; i++)
		{
			Progress.shop.Cars[1].premiumEquipped[i] = true;
		}
		Game.OnStateChange(Game.gameState.PreRace);
		RaceLogic.instance.Restart();
		Progress.shop.activeCar = activeCar;
		Progress.shop.driveOnTankominato = false;
		closeWindow();
	}
}
