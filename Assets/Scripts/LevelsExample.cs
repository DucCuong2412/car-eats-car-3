using UnityEngine;

public class LevelsExample : LevelsModelBase
{
	public GameObject ButtonBack;

	public GameObject ButtonShop;

	public GameObject ButtonMoreGames;

	public GameObject ButtonLevel;

	public override void OnButtonBackClick()
	{
		UnityEngine.Debug.Log("Back");
	}

	public override void OnButtonMoreGamesClick()
	{
		Application.OpenURL("http://maxmixgames.com");
	}

	public override void OnButtonShopClick()
	{
		UnityEngine.Debug.Log("Shop");
	}

	public override void OnButtonLevelClick()
	{
		UnityEngine.Debug.Log("Levels");
	}
}
