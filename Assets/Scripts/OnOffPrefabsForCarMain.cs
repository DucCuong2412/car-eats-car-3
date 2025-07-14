using UnityEngine;

public class OnOffPrefabsForCarMain : MonoBehaviour
{
	private void Update()
	{
		Vector3 position = RaceLogic.instance.car.transform.position;
		float y = position.y;
		Vector3 position2 = base.transform.position;
		if (Mathf.Abs(y - position2.y) > 40f)
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
