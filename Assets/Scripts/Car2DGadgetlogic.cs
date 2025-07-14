using UnityEngine;

public class Car2DGadgetlogic : MonoBehaviour
{
	public Animator Magnet;

	public Animator EMP;

	public Animator Missle;

	public Animator LedoLuch;

	public Animator Rechrger;

	public GameObject MagnetGO;

	public GameObject EMPGO;

	public GameObject MissleGO;

	public GameObject LedoLuchGO;

	public GameObject RechrgerGO;

	public bool garage;

	private int _magnet_isOn = Animator.StringToHash("magnet_isOn");

	private int _bombGenerator_isOn = Animator.StringToHash("bombGenerator_isOn");

	private void OnEnable()
	{
		if (!garage)
		{
			MagnetGO.SetActive(Progress.shop.Car.Gadget_Magnet_bounght);
			EMPGO.SetActive(Progress.shop.Car.Gadget_EMP_bounght);
			MissleGO.SetActive(Progress.shop.Car.Gadget_MISSLLE_bounght);
			LedoLuchGO.SetActive(Progress.shop.Car.Gadget_LEDOLUCH_bounght);
			RechrgerGO.SetActive(Progress.shop.Car.Gadget_RECHARGER_bounght);
			if (Progress.shop.Car.Gadget_Magnet_bounght)
			{
				Magnet.SetBool(_magnet_isOn, value: true);
			}
			if (Progress.shop.Car.Gadget_RECHARGER_bounght)
			{
				Rechrger.SetBool(_bombGenerator_isOn, value: true);
			}
		}
		else
		{
			MagnetGO.SetActive(value: true);
			EMPGO.SetActive(value: true);
			MissleGO.SetActive(value: true);
			LedoLuchGO.SetActive(value: true);
			RechrgerGO.SetActive(value: true);
		}
	}

	private void Update()
	{
		if (!garage)
		{
			MagnetGO.SetActive(Progress.shop.Car.Gadget_Magnet_bounght);
			EMPGO.SetActive(Progress.shop.Car.Gadget_EMP_bounght);
			MissleGO.SetActive(Progress.shop.Car.Gadget_MISSLLE_bounght);
			LedoLuchGO.SetActive(Progress.shop.Car.Gadget_LEDOLUCH_bounght);
			RechrgerGO.SetActive(Progress.shop.Car.Gadget_RECHARGER_bounght);
		}
		else
		{
			MagnetGO.SetActive(value: true);
			EMPGO.SetActive(value: true);
			MissleGO.SetActive(value: true);
			LedoLuchGO.SetActive(value: true);
			RechrgerGO.SetActive(value: true);
		}
	}
}
