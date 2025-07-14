using UnityEngine;

public class MonstroEffect : MonoBehaviour
{
	public GameObject MonstropediaMarker;

	private void Update()
	{
		MonstropediaMarker.SetActive(value: false);
		int num = 0;
		while (true)
		{
			if (num < Progress.shop.MonstroLocks.Count)
			{
				if (!Progress.shop.MonstroLocks[num] && Progress.shop.MonstroCanGetReward[num])
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		MonstropediaMarker.SetActive(value: true);
	}
}
