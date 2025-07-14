using UnityEngine;

public class DeathCarOnUp : MonoBehaviour
{
	public Car2DAIController C2DAIC;

	public Car2DControlerForBombCar C2DCFBC;

	private void OnTriggerEnter2D(Collider2D c)
	{
		if (c.tag == "CarMain" || c.tag == "CarMainChild" || c.tag == "Wheels")
		{
			if (C2DAIC != null)
			{
				C2DAIC.Death();
			}
			if (C2DCFBC != null)
			{
				C2DCFBC.Death();
			}
		}
	}
}
