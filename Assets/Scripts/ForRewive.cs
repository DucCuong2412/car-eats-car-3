using UnityEngine;

public class ForRewive : MonoBehaviour
{
	public GameObject video;

	public GameObject use;

	private void Update()
	{
		if (Progress.shop.restoreBoost > 0)
		{
			use.SetActive(value: true);
			video.SetActive(value: false);
		}
		else if (Progress.shop.restoreBoost <= 0)
		{
			use.SetActive(value: false);
			video.SetActive(value: true);
		}
	}
}
