using UnityEngine;

public class SpiderEnemyDamage : MonoBehaviour
{
	public SpiderEnemyController SEC;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "CarMain" || other.tag == "CarMainChild")
		{
			SEC.Dmg = RaceLogic.instance.car.HealthModule._barrel.MaxValue / 10f;
			RaceLogic.instance.HitMainCar(SEC.Dmg);
			UnityEngine.Debug.Log("Spider damege oce2d");
		}
	}
}
