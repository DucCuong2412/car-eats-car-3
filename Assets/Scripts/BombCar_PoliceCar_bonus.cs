using System.Collections.Generic;
using UnityEngine;

public class BombCar_PoliceCar_bonus : MonoBehaviour
{
	public float _Time = 5f;

	private Transform Car;

	public void Activate(Transform _car, List<Car2DAIController> _targets, bool Ground, bool police, bool copter, int amount)
	{
		Car = _car;
		if (Ground)
		{
			if (police)
			{
				SpawnPolice(amount);
			}
			else
			{
				SpawnBombCar(amount);
			}
		}
		else if (copter)
		{
			SpawnCopter();
		}
		else
		{
			SpawnZeppelin();
		}
	}

	private void SpawnPolice(int amount)
	{
		if (Progress.levels.active_pack == 1)
		{
			amount = 2;
		}
		else if (Progress.levels.active_pack == 2)
		{
			amount = 3;
		}
		else if (Progress.levels.active_pack == 3)
		{
			amount = 4;
		}
		for (int i = 0; i < amount; i++)
		{
			Vector3 position = Car.position;
			float x = position.x + 45f + (float)i;
			Vector3 position2 = Car.position;
			GameObject gameObject = Pool.GameOBJECT(Pool.Bonus.policecar, new Vector2(x, position2.y + 20f + (float)i));
			RaceLogic.instance.gui.interface_PositionBar.InitAIs(gameObject.transform);
			tk2dSprite[] componentsInChildren = gameObject.GetComponentsInChildren<tk2dSprite>(includeInactive: true);
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				componentsInChildren[j].SortingOrder += i * 1200;
			}
		}
	}

	private void SpawnBombCar(int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			Vector3 position = Car.position;
			float x = position.x + 45f + (float)i;
			Vector3 position2 = Car.position;
			GameObject gameObject = Pool.GameOBJECT(Pool.Bonus.bombcar, new Vector2(x, position2.y + 15f + (float)i));
			tk2dSprite[] componentsInChildren = gameObject.GetComponentsInChildren<tk2dSprite>(includeInactive: true);
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				componentsInChildren[j].SortingOrder += i * 1200;
			}
		}
	}

	private void SpawnCopter()
	{
		Vector3 position = Car.position;
		float x = position.x - 30f;
		Vector3 position2 = Car.position;
		Pool.GameOBJECT(Pool.Bonus.copter, new Vector2(x, position2.y + 10f));
	}

	private void SpawnZeppelin()
	{
		Vector3 position = Car.position;
		float x = position.x - 50f;
		Vector3 position2 = Car.position;
		Pool.GameOBJECT(Pool.Bonus.cepel, new Vector2(x, position2.y + 13f));
	}
}
