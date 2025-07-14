using UnityEngine;

public class MarkerDeathEnemies : MonoBehaviour
{
	public bool KillBosses;

	public bool GiveReward;

	public BoxCollider2D bc2d;

	private void OnEnable()
	{
		bc2d = GetComponent<BoxCollider2D>();
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		ScanTarget(collider.gameObject.transform);
	}

	public void ScanTarget(Transform item)
	{
		int num = 0;
		while (true)
		{
			if (num >= 10)
			{
				return;
			}
			item = item.parent;
			if (!(item != null))
			{
				return;
			}
			if (item.name.Contains("Enemy") || (KillBosses && item.name.Contains("BOS")) || item.name.Contains("enemy") || item.name.Contains("Civilians"))
			{
				Car2DAIController component = item.gameObject.GetComponent<Car2DAIController>();
				if (component == null)
				{
					item.gameObject.GetComponent<Car2DControlerForBombCar>();
				}
				if (component != null)
				{
					component.Death(GiveReward);
					return;
				}
				if (item.name.Contains("small"))
				{
					item.gameObject.SetActive(value: false);
					return;
				}
			}
			if (item.name.Contains("PC_cars_new") || item.name.Contains("PC_car_2") || item.name.Contains("PC_car_3") || item.name.Contains("PC_car_4") || item.name.Contains("PC_car_5") || item.name.Contains("PC_car_07") || item.name.Contains("PC_car_police") || item.name.Contains("PC_car_08") || item.name.Contains("PC_car_underground_01") || item.name.Contains("PC_car_underground_02") || item.name.Contains("PC_car_police3") || item.name.Contains("PC_car_underground_05") || item.name.Contains("PC_car_underground_06"))
			{
				break;
			}
			num++;
		}
		RaceLogic.instance.deathInMarker = true;
		RaceLogic instance = RaceLogic.instance;
		Vector2 size = bc2d.size;
		instance.goleftonmarker = size.y / 4f;
		Car2DController component2 = item.gameObject.GetComponent<Car2DController>();
		component2.HealthModule._barrel.Value = -1f;
	}
}
