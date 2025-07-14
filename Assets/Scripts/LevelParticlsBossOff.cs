using UnityEngine;

public class LevelParticlsBossOff : MonoBehaviour
{
	public GameObject Boss9lvl;

	public GameObject Boss18lvl;

	public GameObject Boss27lvl;

	public GameObject Boss36lvl;

	public GameObject marker;

	private void Start()
	{
		Boss36lvl.SetActive(!Progress.levels.Win_36_Lvl);
		if (Utilities.LevelNumberGlobal(Progress.levels.Max_Active_Level, Progress.levels.Max_Active_Pack) > 27)
		{
			Boss9lvl.SetActive(value: false);
			Boss18lvl.SetActive(value: false);
			Boss27lvl.SetActive(value: false);
		}
		else if (Utilities.LevelNumberGlobal(Progress.levels.Max_Active_Level, Progress.levels.Max_Active_Pack) > 18)
		{
			Boss9lvl.SetActive(value: false);
			Boss18lvl.SetActive(value: false);
			Boss27lvl.SetActive(value: true);
		}
		else if (Utilities.LevelNumberGlobal(Progress.levels.Max_Active_Level, Progress.levels.Max_Active_Pack) > 9)
		{
			Boss9lvl.SetActive(value: false);
			Boss18lvl.SetActive(value: true);
			Boss27lvl.SetActive(value: true);
		}
	}

	private void Update()
	{
		if (Progress.shop.premiumShopforFirst)
		{
			marker.SetActive(value: true);
			return;
		}
		marker.SetActive(value: false);
		Progress.shop.premiumShopforFirst = false;
	}
}
