using AnimationOrTween;
using UnityEngine;

public class Car2DGadgetsModule : MonoBehaviour
{
	public enum Gadgets
	{
		shield,
		ram,
		turboregen,
		magnet,
		bombregen
	}

	private enum Premium
	{

	}

	public GameObject Shield;

	public GameObject Ram;

	public GameObject Turbo;

	public GameObject Magnet;

	private bool[] BoughtGadgets = new bool[5];

	public bool Inited;

	private bool _enabled;

	public bool Enabled
	{
		get
		{
			return _enabled;
		}
		set
		{
			_enabled = value;
			if (Inited && _enabled)
			{
				ActivateGadget();
			}
		}
	}

	public void Init(bool[] boughtGadgets)
	{
		BoughtGadgets = boughtGadgets;
		ActivateGadget();
		Inited = true;
	}

	public void ActivateGadget(Gadgets e)
	{
		switch (e)
		{
		case Gadgets.shield:
			if (Shield == null)
			{
				Shield = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("CarShield"));
				Shield.transform.position = base.transform.position;
			}
			Shield.transform.parent = base.transform;
			Shield.transform.rotation = base.transform.rotation;
			if (!Shield.activeSelf)
			{
				Shield.SetActive(value: true);
			}
			Shield.GetComponent<CarShield>().Init();
			break;
		case Gadgets.ram:
			if (Ram == null)
			{
				Ram = ((Progress.shop.NowSelectCarNeedForMe != 2) ? ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("CarRam"))) : ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("harwesterRam"))));
			}
			Ram.transform.rotation = base.transform.rotation;
			Ram.transform.parent = base.transform;
			Ram.transform.localPosition = new Vector2(0f, 0f);
			if (!Ram.activeSelf)
			{
				Ram.SetActive(value: true);
				ActiveAnimation.Play(Ram.GetComponent<Animation>(), Direction.Forward);
			}
			break;
		case Gadgets.magnet:
			if (Magnet == null)
			{
				Magnet = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Magnet"));
			}
			Magnet.transform.rotation = base.transform.rotation;
			Magnet.transform.parent = base.transform;
			Magnet.transform.localPosition = ((Progress.shop.NowSelectCarNeedForMe != 2) ? new Vector2(0f, 0f) : new Vector2(1f, -0.18f));
			if (!Magnet.activeSelf)
			{
				Magnet.SetActive(value: true);
			}
			break;
		case Gadgets.turboregen:
			if (Turbo == null)
			{
				Turbo = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("TurboRecharger"));
			}
			Turbo.transform.rotation = base.transform.rotation;
			Turbo.transform.parent = base.transform;
			if (Progress.shop.NowSelectCarNeedForMe == 0)
			{
				Turbo.transform.localPosition = new Vector2(0f, 0f);
			}
			else if (Progress.shop.NowSelectCarNeedForMe == 1)
			{
				Turbo.transform.localPosition = new Vector2(0f, -0.1f);
			}
			else
			{
				Turbo.transform.localPosition = new Vector2(0.1f, 0f);
			}
			if (!Turbo.activeSelf)
			{
				Turbo.SetActive(value: true);
				ActiveAnimation.Play(Turbo.GetComponent<Animation>(), Direction.Forward);
				GetComponent<Car2DController>().TurboModule._Barrel.Restore = true;
				GetComponent<Car2DController>().TurboModule._Barrel.RestoreTime = 25f;
			}
			break;
		case Gadgets.bombregen:
			RaceLogic.instance.SetRegarger(active: true);
			break;
		}
	}

	public void DisactivateGadget(int current)
	{
		switch (current)
		{
		case 0:
			if (Magnet != null)
			{
				Magnet.GetComponent<MagnetScript>().Disable();
			}
			BoughtGadgets[0] = false;
			break;
		case 1:
			if (Ram != null)
			{
				Utilities.RunActor(Ram.GetComponent<Animation>(), isForward: false, delegate
				{
					Ram.SetActive(value: false);
				});
			}
			BoughtGadgets[1] = false;
			break;
		case 2:
			if (Turbo != null)
			{
				Utilities.RunActor(Turbo.GetComponent<Animation>(), isForward: false, delegate
				{
					Turbo.SetActive(value: false);
				});
			}
			BoughtGadgets[2] = false;
			break;
		case 3:
			if (Shield != null)
			{
				Shield.GetComponent<CarShield>().Disable();
			}
			BoughtGadgets[3] = false;
			break;
		case 4:
			RaceLogic.instance.SetRegarger(active: false);
			BoughtGadgets[4] = false;
			break;
		}
	}

	public void ActivateGadget(bool[] _boughtGadget)
	{
		if (_boughtGadget[0])
		{
			ActivateGadget(Gadgets.magnet);
		}
		if (_boughtGadget[1])
		{
			ActivateGadget(Gadgets.ram);
		}
		if (_boughtGadget[2])
		{
			ActivateGadget(Gadgets.turboregen);
		}
		if (_boughtGadget[3])
		{
			ActivateGadget(Gadgets.shield);
		}
		if (_boughtGadget[4])
		{
			ActivateGadget(Gadgets.bombregen);
		}
	}

	public void ActivateGadget(int i, bool active)
	{
		BoughtGadgets[i] = active;
		ActivateGadget(BoughtGadgets);
	}

	public void ActivateGadget()
	{
		ActivateGadget(BoughtGadgets);
	}
}
