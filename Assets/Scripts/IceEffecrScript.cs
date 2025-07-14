using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEffecrScript : MonoBehaviour
{
	public float _Time = 5f;

	public float Damage = 400f;

	private Vector2 _target;

	private GlacierScript g;

	private List<Car2DAIController> ActiveTargets;

	private List<GameObject> Targets = new List<GameObject>();

	[HideInInspector]
	public float t;

	private string str = "gfx_freeze_01_sn";

	public void Activate(Transform _car, List<Car2DAIController> _targets)
	{
		Audio.Play(str, Audio.soundVolume, loop: true);
		Targets.Clear();
		ActiveTargets = null;
		ActiveTargets = _targets;
		t = _Time;
		StartCoroutine(Freeze());
	}

	private IEnumerator Freeze()
	{
		while (t > 0f)
		{
			for (int i = 0; i < ActiveTargets.Count; i++)
			{
				if (!Targets.Contains(ActiveTargets[i].gameObject))
				{
					Targets.Add(ActiveTargets[i].gameObject);
					g = Pool.instance.spawnAtPoint(Pool.Name(Pool.Bombs.ice), Targets[Targets.Count - 1].transform).GetComponent<GlacierScript>();
					g.Init(t, Damage, ActiveTargets[i].gameObject, ActiveTargets[i]);
				}
			}
			for (int j = 0; j < Targets.Count; j++)
			{
				if (!Targets[j].activeSelf || Targets[j] == null)
				{
					Targets.Remove(Targets[j]);
				}
			}
			t -= Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}
		base.gameObject.SetActive(value: false);
	}

	private void OnDisable()
	{
		Audio.Stop(str);
	}
}
