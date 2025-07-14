using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamAnimController : MonoBehaviour
{
	public Animation anim;

	public float DistansToActivation;

	private bool show = true;

	private bool hide = true;

	private string on;

	private string off;

	private string activ;

	private string deactiv;

	private List<string> states = new List<string>();

	private float timer;

	private void OnEnable()
	{
	}

	private IEnumerator animation()
	{
		while (true)
		{
			if (RaceLogic.instance.car != null)
			{
				Vector3 position = RaceLogic.instance.car.gameObject.transform.position;
				float x = position.x;
				Vector3 position2 = base.gameObject.transform.position;
				if (Mathf.Abs(x - position2.x) < DistansToActivation)
				{
					if (show)
					{
						show = false;
						hide = true;
						anim.Play(activ);
					}
					yield return new WaitForSeconds(1f);
					anim.Play(on);
				}
				else
				{
					if (hide)
					{
						hide = false;
						anim.Play(deactiv);
						yield return 0;
						anim.Play(off);
					}
					yield return 0;
					show = true;
					anim.Play(off);
				}
			}
			yield return 0;
		}
	}
}
