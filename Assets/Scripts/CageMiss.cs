using UnityEngine;

public class CageMiss : MonoBehaviour
{
	public CageMGScript scr;

	private void Start()
	{
		base.gameObject.SetActive(value: false);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "CarMain" || other.tag == "CarMainChild")
		{
			scr.CarFol.StartCam();
			RaceLogic.instance.follow.StartFollow(RaceLogic.instance.car.transform);
			scr.coll.enabled = false;
			base.gameObject.SetActive(value: false);
		}
	}
}
