using UnityEngine;

public class CageEnemyDeath : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "CarEnemy")
		{
			Car2DAIController componentInParent = other.gameObject.transform.parent.parent.GetComponentInParent<Car2DAIController>();
			if (componentInParent != null)
			{
				componentInParent.Death();
			}
			Car2DControlerForBombCar componentInParent2 = other.gameObject.transform.parent.parent.GetComponentInParent<Car2DControlerForBombCar>();
			if (componentInParent2 != null)
			{
				componentInParent2.Death();
			}
			FuelCarBomb componentInChildren = other.gameObject.GetComponentInChildren<FuelCarBomb>();
			if (componentInChildren != null)
			{
				componentInChildren.gameObject.transform.parent.gameObject.SetActive(value: false);
			}
		}
	}
}
