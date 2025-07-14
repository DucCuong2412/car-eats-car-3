using UnityEngine;

public class ElecticRam : MonoBehaviour
{
	private static string CarMain = "CarMain";

	private static string CarMainChild = "CarMainChild";

	private void OnTriggerStay2D(Collider2D c)
	{
		Eating(c.gameObject);
	}

	private void OnTriggerExit2D(Collider2D c)
	{
	}

	public void Eating(GameObject g)
	{
		if (!(g.tag != CarMain) || !(g.tag != CarMainChild))
		{
			RaceLogic.instance.car.electric = true;
			RaceLogic.instance.car.timerForElectric = 5f;
		}
	}
}
