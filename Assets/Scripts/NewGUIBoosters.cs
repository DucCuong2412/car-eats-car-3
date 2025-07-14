using UnityEngine;

public class NewGUIBoosters : MonoBehaviour
{
	public GameObject video;

	public GameObject use;

	private void OnEnable()
	{
		if (use.transform.parent.name == "booster_health" && Progress.shop.healthBost > 0)
		{
			use.SetActive(value: true);
			video.SetActive(value: false);
		}
		else if (use.transform.parent.name == "booster_health" && Progress.shop.healthBost <= 0)
		{
			use.SetActive(value: false);
			video.SetActive(value: true);
		}
		if (use.transform.parent.name == "booster_turbo" && Progress.shop.turboBoost > 0)
		{
			use.SetActive(value: true);
			video.SetActive(value: false);
		}
		else if (use.transform.parent.name == "booster_turbo" && Progress.shop.turboBoost <= 0)
		{
			use.SetActive(value: false);
			video.SetActive(value: true);
		}
	}
}
