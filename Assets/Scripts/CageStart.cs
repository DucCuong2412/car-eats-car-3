using UnityEngine;

public class CageStart : MonoBehaviour
{
	public CageMGScript scr;

	public GameObject MissTriger;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "CarMain" || other.tag == "CarMainChild")
		{
			scr.SetStart();
			base.gameObject.SetActive(value: false);
			scr.CarFol.StopCam();
			MissTriger.SetActive(value: true);
		}
	}
}
