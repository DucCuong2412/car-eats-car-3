using UnityEngine;

public class circCollActivateAlways : MonoBehaviour
{
	public CircleCollider2D coll;

	public bool forRuby;

	private int i;

	private void Update()
	{
		if (i > 30 && RaceLogic.instance.AllInitedForPool)
		{
			coll.enabled = true;
			coll.isTrigger = true;
			i = 0;
			if (!forRuby || !(RaceLogic.instance.car != null))
			{
				return;
			}
			Vector3 position = base.transform.position;
			float x = position.x;
			Vector3 position2 = RaceLogic.instance.car.transform.position;
			if (!(Mathf.Abs(x - position2.x) > 40f))
			{
				Vector3 position3 = base.transform.position;
				float y = position3.y;
				Vector3 position4 = RaceLogic.instance.car.transform.position;
				if (!(Mathf.Abs(y - position4.y) > 40f))
				{
					return;
				}
			}
			base.gameObject.SetActive(value: false);
		}
		else
		{
			i++;
		}
	}
}
