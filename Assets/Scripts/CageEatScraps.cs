using System.Collections;
using UnityEngine;

public class CageEatScraps : MonoBehaviour
{
	private bool canScr = true;

	public GameObject pos;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!canScr || (!(other.tag == "CarMain") && !(other.tag == "CarMainChild")))
		{
			return;
		}
		int num = 0;
		if (Progress.levels.InUndeground)
		{
			num = UnityEngine.Random.Range(0, 2);
			for (int i = 0; i < num; i++)
			{
				Pool.Scrap(Pool.Scraps.SpiderScr1, pos.transform.position, UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(3, 7));
			}
			num = UnityEngine.Random.Range(0, 2);
			for (int j = 0; j < num; j++)
			{
				Pool.Scrap(Pool.Scraps.SpiderScr1, pos.transform.position, UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(3, 7));
			}
			num = UnityEngine.Random.Range(0, 2);
			for (int k = 0; k < num; k++)
			{
				Pool.Scrap(Pool.Scraps.SpiderScr1, pos.transform.position, UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(3, 7));
			}
		}
		else
		{
			num = UnityEngine.Random.Range(0, 4);
			for (int l = 0; l < num; l++)
			{
				Pool.Scrap(Pool.Scraps.CageScr1, pos.transform.position, UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(3, 7));
			}
			num = UnityEngine.Random.Range(0, 3);
			for (int m = 0; m < num; m++)
			{
				Pool.Scrap(Pool.Scraps.CageScr2, pos.transform.position, UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(3, 7));
			}
			num = UnityEngine.Random.Range(0, 3);
			for (int n = 0; n < num; n++)
			{
				Pool.Scrap(Pool.Scraps.CageScr3, pos.transform.position, UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(3, 7));
			}
		}
		StartCoroutine(DelayScr());
	}

	private IEnumerator DelayScr()
	{
		canScr = false;
		yield return new WaitForSeconds(0.5f);
		canScr = true;
	}
}
