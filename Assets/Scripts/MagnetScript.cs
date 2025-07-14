using System.Collections.Generic;
using UnityEngine;

public class MagnetScript : MonoBehaviour
{
	private Animation _anim;

	private List<CollectItemOther> Colectebles = new List<CollectItemOther>();

	private List<CollectibleItemRuby> ColecteblesR = new List<CollectibleItemRuby>();

	private Collider2D[] colliders = new Collider2D[20];

	private int layerCast;

	public float Radius = 15f;

	public Animation Anim
	{
		get
		{
			if (_anim == null)
			{
				_anim = base.gameObject.GetComponentInParent<Animation>();
			}
			return _anim;
		}
	}

	private void OnEnable()
	{
		layerCast = LayerMask.GetMask("Boost");
	}

	public void Disable()
	{
		Utilities.RunActor(Anim, isForward: false, delegate
		{
			base.gameObject.SetActive(value: false);
		});
	}

	private void Start()
	{
		SetParticleSystemSortingLayer();
	}

	private void SetParticleSystemSortingLayer()
	{
		ParticleSystem[] componentsInChildren = base.gameObject.GetComponentsInChildren<ParticleSystem>(includeInactive: true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].GetComponent<Renderer>().sortingLayerName = "Particles";
		}
	}

	private void FixedUpdate()
	{
		FindObjects();
		MoveObjects();
		MoveObjectsR();
	}

	private void FindObjects()
	{
		if (Physics2D.OverlapCircleNonAlloc(base.transform.position, Radius, colliders, layerCast) <= 0)
		{
			return;
		}
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i] == null)
			{
				continue;
			}
			CollectItemOther component = colliders[i].gameObject.GetComponent<CollectItemOther>();
			CollectibleItemRuby component2 = colliders[i].gameObject.GetComponent<CollectibleItemRuby>();
			if (component != null)
			{
				CollectItemOther collectItemOther = component;
				if (collectItemOther != null && collectItemOther.itemType == RaceLogic.enItem.Rubin && !Colectebles.Contains(collectItemOther) && collectItemOther != null)
				{
					Colectebles.Add(collectItemOther);
				}
			}
			if (component2 != null)
			{
				CollectibleItemRuby collectibleItemRuby = component2;
				if (collectibleItemRuby != null && collectibleItemRuby.itemType == RaceLogic.enItemRuby.Rubin && !ColecteblesR.Contains(collectibleItemRuby) && collectibleItemRuby != null)
				{
					ColecteblesR.Add(collectibleItemRuby);
				}
			}
		}
	}

	private void MoveObjects()
	{
		for (int i = 0; i < Colectebles.Count; i++)
		{
			if (Vector2.Distance(Colectebles[i].transform.position, base.transform.position) > Radius || Colectebles[i].itemType != 0)
			{
				Colectebles.Remove(Colectebles[i]);
			}
			else
			{
				Colectebles[i].transform.position = Vector2.Lerp(Colectebles[i].transform.position, base.transform.position, Time.deltaTime * 40f / Vector2.Distance(Colectebles[i].transform.position, base.transform.position));
			}
		}
	}

	private void MoveObjectsR()
	{
		for (int i = 0; i < ColecteblesR.Count; i++)
		{
			if (Vector2.Distance(ColecteblesR[i].transform.position, base.transform.position) > Radius || ColecteblesR[i].itemType != 0)
			{
				ColecteblesR.Remove(ColecteblesR[i]);
			}
			else
			{
				ColecteblesR[i].transform.position = Vector2.Lerp(ColecteblesR[i].transform.position, base.transform.position, Time.deltaTime * 40f / Vector2.Distance(ColecteblesR[i].transform.position, base.transform.position));
			}
		}
	}
}
