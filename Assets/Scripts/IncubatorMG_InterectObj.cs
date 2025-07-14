using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncubatorMG_InterectObj : MonoBehaviour
{
	public IncubatorMG_Controller.Tupe Tupe;

	public GameObject CarContainer;

	public GameObject StoneContainer;

	public IncubatorMG_ObjLayers StarObj;

	public List<IncubatorMG_ObjLayers> Stones;

	public List<IncubatorMG_ObjLayers> CarsCivil;

	public List<IncubatorMG_ObjLayers> CarsCop;

	public float Speed = 0.1f;

	public int CurrentLine;

	private int tPlassedLayer;

	private int CurrentNum = -1;

	private Vector3 _pos;

	private IncubatorMG_Controller _controller;

	private void Start()
	{
		_controller = UnityEngine.Object.FindObjectOfType<IncubatorMG_Controller>();
		GoToPool();
	}

	private void OnEnable()
	{
		_pos = base.transform.position;
		if (Tupe == IncubatorMG_Controller.Tupe.CarCivil || Tupe == IncubatorMG_Controller.Tupe.CarCop)
		{
			CarContainer.SetActive(value: true);
			if (Tupe == IncubatorMG_Controller.Tupe.CarCivil)
			{
				CurrentNum = UnityEngine.Random.Range(0, CarsCivil.Count);
				CarsCivil[CurrentNum].gameObject.SetActive(value: true);
			}
			else if (Tupe == IncubatorMG_Controller.Tupe.CarCop)
			{
				CurrentNum = UnityEngine.Random.Range(0, CarsCop.Count);
				CarsCop[CurrentNum].gameObject.SetActive(value: true);
			}
		}
		else if (Tupe == IncubatorMG_Controller.Tupe.Stone)
		{
			StoneContainer.SetActive(value: true);
			CurrentNum = UnityEngine.Random.Range(0, Stones.Count);
			Stones[CurrentNum].gameObject.SetActive(value: true);
		}
		else if (Tupe == IncubatorMG_Controller.Tupe.Decor)
		{
			CurrentNum = UnityEngine.Random.Range(0, Stones.Count);
			Stones[CurrentNum].gameObject.SetActive(value: true);
		}
		else if (Tupe == IncubatorMG_Controller.Tupe.Star)
		{
			StoneContainer.SetActive(value: true);
			StarObj.gameObject.SetActive(value: true);
		}
	}

	public void ChengLayer(int line)
	{
		tPlassedLayer = 0;
		switch (line)
		{
		case 0:
			tPlassedLayer = 0;
			base.transform.localScale = Vector3.one * 0.8f;
			break;
		case 1:
			tPlassedLayer = 300;
			base.transform.localScale = Vector3.one * 0.8f;
			break;
		case 2:
			tPlassedLayer = 600;
			base.transform.localScale = Vector3.one * 0.9f;
			break;
		case 3:
			tPlassedLayer = 900;
			base.transform.localScale = Vector3.one * 0.9f;
			break;
		case 4:
			tPlassedLayer = 1200;
			base.transform.localScale = Vector3.one * 0.9f;
			break;
		case 5:
			tPlassedLayer = 1500;
			base.transform.localScale = Vector3.one;
			break;
		case 6:
			tPlassedLayer = 1800;
			base.transform.localScale = Vector3.one;
			break;
		}
		ChengGraphicLayer();
	}

	private void ChengGraphicLayer()
	{
		int num = 0;
		switch (Tupe)
		{
		case IncubatorMG_Controller.Tupe.CarCivil:
			num = CarsCivil[CurrentNum].Sprites.Count;
			CarsCivil[CurrentNum].TrySetLay();
			for (int l = 0; l < num; l++)
			{
				CarsCivil[CurrentNum].Sprites[l].sortingOrder = CarsCivil[CurrentNum].SpritesLayers[l] + tPlassedLayer;
			}
			break;
		case IncubatorMG_Controller.Tupe.CarCop:
			num = CarsCop[CurrentNum].Sprites.Count;
			CarsCop[CurrentNum].TrySetLay();
			for (int j = 0; j < num; j++)
			{
				CarsCop[CurrentNum].Sprites[j].sortingOrder = CarsCop[CurrentNum].SpritesLayers[j] + tPlassedLayer;
			}
			break;
		case IncubatorMG_Controller.Tupe.Stone:
			num = Stones[CurrentNum].Sprites.Count;
			Stones[CurrentNum].TrySetLay();
			for (int k = 0; k < num; k++)
			{
				Stones[CurrentNum].Sprites[k].sortingOrder = Stones[CurrentNum].SpritesLayers[k] + tPlassedLayer;
			}
			break;
		case IncubatorMG_Controller.Tupe.Decor:
			num = Stones[CurrentNum].Sprites.Count;
			Stones[CurrentNum].TrySetLay();
			for (int i = 0; i < num; i++)
			{
				Stones[CurrentNum].Sprites[i].sortingOrder = Stones[CurrentNum].SpritesLayers[i] + tPlassedLayer;
			}
			break;
		case IncubatorMG_Controller.Tupe.Star:
			StarObj.Sprites[0].sortingOrder = StarObj.SpritesLayers[0] + tPlassedLayer + 100;
			break;
		}
	}

	public void SetDestroy()
	{
		StartCoroutine(Death());
		tPlassedLayer = 0;
	}

	private void Update()
	{
		if (!_controller.PauseOn)
		{
			_pos.x -= _controller.Speed + Speed;
			base.transform.position = _pos;
			if (_controller.OutOfScope(base.transform.position))
			{
				GoToPool();
				tPlassedLayer = 0;
			}
		}
	}

	private void GoToPool()
	{
		int num = 0;
		if (Tupe != IncubatorMG_Controller.Tupe.Decor)
		{
			StarObj.gameObject.SetActive(value: false);
			num = CarsCivil.Count;
			for (int i = 0; i < num; i++)
			{
				CarsCivil[i].gameObject.SetActive(value: false);
			}
			num = CarsCop.Count;
			for (int j = 0; j < num; j++)
			{
				CarsCop[j].gameObject.SetActive(value: false);
			}
		}
		num = Stones.Count;
		for (int k = 0; k < num; k++)
		{
			Stones[k].gameObject.SetActive(value: false);
		}
		if (Tupe != IncubatorMG_Controller.Tupe.Decor)
		{
			CarContainer.SetActive(value: false);
			StoneContainer.SetActive(value: false);
		}
		base.gameObject.SetActive(value: false);
	}

	private IEnumerator Death()
	{
		yield return new WaitForSeconds(0.5f);
		int count6;
		if (Tupe != IncubatorMG_Controller.Tupe.Decor)
		{
			StarObj.gameObject.SetActive(value: false);
			count6 = CarsCivil.Count;
			for (int i = 0; i < count6; i++)
			{
				CarsCivil[i].OnObjectForDeath.gameObject.SetActive(value: false);
				CarsCivil[i].OffObjectForDeath.gameObject.SetActive(value: true);
			}
			count6 = CarsCop.Count;
			for (int j = 0; j < count6; j++)
			{
				CarsCop[j].OnObjectForDeath.gameObject.SetActive(value: false);
				CarsCop[j].OffObjectForDeath.gameObject.SetActive(value: true);
			}
		}
		count6 = Stones.Count;
		for (int k = 0; k < count6; k++)
		{
			Stones[k].OnObjectForDeath.gameObject.SetActive(value: false);
			Stones[k].OffObjectForDeath.gameObject.SetActive(value: true);
		}
		if (Tupe != IncubatorMG_Controller.Tupe.Decor)
		{
			StarObj.gameObject.SetActive(value: false);
			count6 = CarsCivil.Count;
			for (int l = 0; l < count6; l++)
			{
				CarsCivil[l].gameObject.SetActive(value: false);
			}
			count6 = CarsCop.Count;
			for (int m = 0; m < count6; m++)
			{
				CarsCop[m].gameObject.SetActive(value: false);
			}
		}
		count6 = Stones.Count;
		for (int n = 0; n < count6; n++)
		{
			Stones[n].gameObject.SetActive(value: false);
		}
		if (Tupe != IncubatorMG_Controller.Tupe.Decor)
		{
			CarContainer.SetActive(value: false);
			StoneContainer.SetActive(value: false);
		}
		base.gameObject.SetActive(value: false);
	}
}
