public interface IShopWindowView
{
	void Init(IShopWindowController controller);

	void ShowCoins();

	void ShowFuel(bool isFull);

	void ShowMainPanel(bool isEnergyFull, int currency, bool isCoinsPanel = true);

	void HideMainPanel();

	void SetFuelText(int count);

	void SetCurrencyText(int count);

	void SetFuelPricesAndValues();

	void SetVideoWindow(bool turn);

	void SetInfinityIcon(bool turn);
}
