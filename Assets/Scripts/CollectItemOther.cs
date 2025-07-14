using UnityEngine;

public class CollectItemOther : MonoBehaviour
{
	public RaceLogic.enItem itemType;

	public int amount;

	public bool green;

	public BoostAnimator _BoostAnimator;

	public GameObject collObj;

	private static string CarMain = "CarMain";

	private static string CarMainChild = "CarMainChild";

	private static string Wheels = "Wheels";

	private static string Magnet = "Magnet";

	private static string ruby = "_ruby";

	private int i;

	public void OnTriggerEnter2D(Collider2D coll)
	{
		if (base.name.Contains(ruby))
		{
			if (coll.gameObject.CompareTag(CarMainChild) || coll.gameObject.CompareTag(CarMain) || coll.gameObject.CompareTag(Wheels) || coll.gameObject.CompareTag(Magnet))
			{
				onActivate();
			}
		}
		else if (coll.gameObject.CompareTag(CarMainChild) || coll.gameObject.CompareTag(CarMain) || coll.gameObject.CompareTag(Wheels))
		{
			onActivate();
		}
	}

	private void OnEnable()
	{
		i = 0;
	}

	private void onActivate()
	{
		if (i == 0)
		{
			i++;
			RaceLogic.instance.Collect(itemType, amount);
			if (itemType == RaceLogic.enItem.Ticket)
			{
				RaceLogic instance = RaceLogic.instance;
				Vector3 position = base.transform.position;
				instance.CollectTicket(Mathf.RoundToInt(position.x));
			}
			GetComponent<Collider2D>().enabled = false;
			collObj.SetActive(value: false);
		}
	}
}
