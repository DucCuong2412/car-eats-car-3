using UnityEngine;

public class BombCommonLogic : MonoBehaviour
{
	private const int maxCount = 50;

	private int layerExplosion;

	private RaycastHit2D[] hits = new RaycastHit2D[50];

	private static string str_Enemy = "Enemy";

	private static string str_Enemy2 = "enemy";

	private static string str_Civilians = "Civilians";

	private static string str_small = "small";

	private static string str_CarMain = "CarMain";

	private static string str_CarMainChild = "CarMainChild";

	private static string str_Wheels = "Wheels";

	public void Awake()
	{
		layerExplosion = LayerMask.GetMask("Objects", "ObjectsBox", "EnemyCar", "Default", "Civilians", "ForConvoi");
	}

	public void Explosion(Vector3 pos, float radius, float power, float damage, BombLogic.types types = BombLogic.types.DEFAULT)
	{
		int num = Physics2D.CircleCastNonAlloc(pos, radius, Vector2.zero, hits, 0f, layerExplosion);
		for (int i = 0; i < num; i++)
		{
			Rigidbody2D component = hits[i].collider.GetComponent<Rigidbody2D>();
			float value = Vector2.Distance(hits[i].transform.position, pos);
			float num2 = Get1_0FromDistance(value, radius);
			Vector2 force = -hits[i].normal * power * num2;
			if (types == BombLogic.types.REDUSER && (hits[i].transform.name.Contains(str_Enemy2) || hits[i].transform.name.Contains(str_Enemy) || hits[i].transform.name.Contains(str_Civilians)) && !hits[i].transform.name.Contains(str_small))
			{
				GameObject gameObject = null;
				if (hits[i].transform.name.Contains(str_Enemy) || hits[i].transform.name.Contains(str_Enemy2))
				{
					gameObject = Pool.GetSmaller(civil: false);
				}
				else if (hits[i].transform.name.Contains(str_Civilians))
				{
					gameObject = Pool.GetSmaller(civil: true);
				}
				if (gameObject != null)
				{
					gameObject.transform.position = hits[i].transform.gameObject.transform.position;
					Transform transform = gameObject.transform;
					Vector3 position = gameObject.transform.position;
					float x = position.x;
					Vector3 position2 = gameObject.transform.position;
					float y = position2.y + 2f;
					Vector3 position3 = gameObject.transform.position;
					transform.position = new Vector3(x, y, position3.z);
					Car2DSmallerBombConstructor component2 = gameObject.GetComponent<Car2DSmallerBombConstructor>();
					Car2DAIController component3 = hits[i].transform.gameObject.GetComponent<Car2DAIController>();
					if (component3 != null)
					{
						if (!component3.BigToSmall)
						{
							component3.BigToSmall = true;
							component2.CarType = component3.constructor.carType;
							component2.inCarType = component3.inCarType;
							component2.Colorindex = component3.Colorindex;
							component2.IsAhead = component3.constructor.isAhead;
							if (hits[i].transform.name.Contains(str_Enemy))
							{
								component2.IsCivil = false;
							}
							else if (hits[i].transform.name.Contains(str_Civilians))
							{
								component2.IsCivil = true;
							}
							RaceLogic.instance.CounterEmemys++;
							gameObject.SetActive(value: true);
							if ((bool)component)
							{
								component.BroadcastMessage("OnHit", 10000, SendMessageOptions.DontRequireReceiver);
							}
							else
							{
								hits[i].transform.gameObject.SetActive(value: false);
							}
						}
						else
						{
							gameObject.SetActive(value: false);
						}
					}
				}
			}
			if ((bool)component && component.tag != str_CarMain && component.tag != str_CarMainChild && component.tag != str_Wheels)
			{
				component.AddForce(force);
				component.BroadcastMessage("OnHit", damage * num2, SendMessageOptions.DontRequireReceiver);
				component.SendMessageUpwards("ChangeHealth", 0f - damage * num2, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	private float Get1_0FromDistance(float value, float maxDistance)
	{
		float num = Mathf.Clamp(value, 0f, maxDistance);
		return 1f - num / maxDistance;
	}
}
