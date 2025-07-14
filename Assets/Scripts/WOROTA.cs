using UnityEngine;

public class WOROTA : MonoBehaviour
{
	private string str = "crash_wood_01_sn";

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Wheels" || coll.tag == "CarMainChild" || coll.tag == "CarMain")
		{
			Audio.Play(str);
			base.gameObject.SetActive(value: false);
			int num = 4;
			for (int i = 0; i < num; i++)
			{
				Pool.Scrap(Pool.Scraps.CageScr1, base.transform.position, UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(3, 7));
			}
			num = 3;
			for (int j = 0; j < num; j++)
			{
				Pool.Scrap(Pool.Scraps.CageScr2, base.transform.position, UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(3, 7));
			}
			num = 3;
			for (int k = 0; k < num; k++)
			{
				Pool.Scrap(Pool.Scraps.CageScr3, base.transform.position, UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(3, 7));
			}
		}
	}
}
