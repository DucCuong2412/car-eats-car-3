using UnityEngine;

public class CollectibleItemRuby : MonoBehaviour
{
	public RaceLogic.enItemRuby itemType;

	public int amount;

	public bool green;

	public BoostAnimator _BoostAnimator;

	private static string strCarMainChild = "CarMainChild";

	private static string strCarMain = "CarMain";

	private static string strWheels = "Wheels";

	private static string strMagnet = "Magnet";

	private static string glowStr = "glow";

	public void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.CompareTag(strCarMainChild) || coll.gameObject.CompareTag(strCarMain) || coll.gameObject.CompareTag(strWheels) || coll.gameObject.CompareTag(strMagnet))
		{
			onActivate();
		}
	}

	private void OnEnable()
	{
	}

	private void onActivate()
	{
		RaceLogic.instance.Collect(itemType, amount);
		GetComponent<Collider2D>().enabled = false;
		base.gameObject.SetActive(value: false);
		if (base.gameObject.name.Contains(glowStr))
		{
			base.gameObject.transform.parent.parent.gameObject.SetActive(value: false);
		}
	}
}
