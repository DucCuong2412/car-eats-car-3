using System;
using System.Collections.Generic;
using UnityEngine;

public class BoostAnimator : MonoBehaviour
{
	[Serializable]
	public class Boost
	{
		public RaceLogic.enItem Types;

		public GameObject BoostObject;
	}

	public List<Boost> _Boosts = new List<Boost>();

	public CollectibleItem global;

	public CollectItemOther bomb;

	public void EnabledAll(bool enable = false)
	{
		foreach (Boost boost in _Boosts)
		{
			boost.BoostObject.SetActive(enable);
		}
	}

	public void UpdateBoost(RaceLogic.enItem type)
	{
		foreach (Boost boost in _Boosts)
		{
			boost.BoostObject.SetActive(value: false);
		}
		int num = 0;
		while (true)
		{
			if (num < _Boosts.Count)
			{
				if (type == _Boosts[num].Types)
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		_Boosts[num].BoostObject.SetActive(value: true);
		if (type == RaceLogic.enItem.Rocket)
		{
			bomb.amount = global.amount;
		}
	}
}
