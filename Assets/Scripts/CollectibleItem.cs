using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
	public RaceLogic.enItem itemType;

	public int amount;

	public bool green;

	public BoostAnimator _BoostAnimator;

	private static string strGlow = "glow";

	private void OnEnable()
	{
		if (RaceLogic.instance.AllInitedForPool)
		{
			if (_BoostAnimator != null)
			{
				_BoostAnimator.UpdateBoost(itemType);
			}
			if (_BoostAnimator != null && itemType != RaceLogic.enItem.Health && itemType != RaceLogic.enItem.Rocket && itemType != RaceLogic.enItem.Nitro)
			{
				base.enabled = false;
			}
		}
	}

	private void onActivate()
	{
		RaceLogic.instance.Collect(itemType, amount);
		if (itemType == RaceLogic.enItem.Ticket)
		{
			RaceLogic instance = RaceLogic.instance;
			Vector3 position = base.transform.position;
			instance.CollectTicket(Mathf.RoundToInt(position.x));
		}
		GetComponent<Collider2D>().enabled = false;
		base.gameObject.SetActive(value: false);
		if (base.gameObject.name.Contains(strGlow))
		{
			base.gameObject.transform.parent.parent.gameObject.SetActive(value: false);
		}
	}
}
