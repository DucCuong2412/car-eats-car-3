using System.Collections.Generic;
using UnityEngine;

public class SuperBonusesManager : MonoBehaviour
{
	public enum enBonus
	{
		Cloud,
		Metor,
		Freeze,
		Enigma,
		DamageX2,
		BombCar,
		Zeppelin,
		Copter,
		Police,
		ReduceDamage
	}

	private static SuperBonusesManager _instance;

	private Transform NeutralTarget;

	public List<Car2DAIController> Targets;

	private bool IsInited;

	public BombCar_PoliceCar_bonus BcPcB;

	public DamageX2Script damageX2;

	private CloudScript Cloud;

	private MeteorShowerScript Meteor;

	private IceEffecrScript Freeze;

	public static SuperBonusesManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (SuperBonusesManager)UnityEngine.Object.FindObjectOfType(typeof(SuperBonusesManager));
				if (_instance == null)
				{
					GameObject gameObject = new GameObject();
					gameObject.name = "_SuperBonusesManager";
					_instance = gameObject.AddComponent<SuperBonusesManager>();
				}
			}
			return _instance;
		}
	}

	private void OnDestroy()
	{
		_instance = null;
	}

	public void Init(Transform _neutralTarget, List<Car2DAIController> _targets)
	{
		NeutralTarget = _neutralTarget;
		Targets = _targets;
		SpawnBoosts();
		IsInited = true;
	}

	public void ActiveBonus(enBonus _type, int amount)
	{
		if (IsInited)
		{
			switch (_type)
			{
			case enBonus.Police:
				BcPcB.gameObject.SetActive(value: true);
				BcPcB.Activate(NeutralTarget, Targets, Ground: true, police: true, copter: false, amount);
				break;
			case enBonus.BombCar:
				BcPcB.gameObject.SetActive(value: true);
				BcPcB.Activate(NeutralTarget, Targets, Ground: true, police: false, copter: false, amount);
				break;
			case enBonus.Copter:
				BcPcB.gameObject.SetActive(value: true);
				BcPcB.Activate(NeutralTarget, Targets, Ground: false, police: false, copter: true, 1);
				break;
			case enBonus.Zeppelin:
				BcPcB.gameObject.SetActive(value: true);
				BcPcB.Activate(NeutralTarget, Targets, Ground: false, police: false, copter: false, 1);
				break;
			case enBonus.DamageX2:
				damageX2.gameObject.SetActive(value: true);
				damageX2.Activate(NeutralTarget, Targets, x2damage: true);
				break;
			case enBonus.ReduceDamage:
				damageX2.gameObject.SetActive(value: true);
				damageX2.Activate(NeutralTarget, Targets, x2damage: false);
				break;
			}
		}
	}

	private void SpawnBoosts()
	{
		BcPcB = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Bomb_Police_bonus"))).GetComponent<BombCar_PoliceCar_bonus>();
		BcPcB.transform.parent = instance.transform;
		damageX2 = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("DamageX2"))).GetComponent<DamageX2Script>();
		damageX2.transform.parent = instance.transform;
	}

	public void Stop()
	{
		RaceLogic.instance.GetBoostersForCloud().Stop();
	}
}
