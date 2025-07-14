using System.Collections;
using UnityEngine;

public class CageBroke : MonoBehaviour
{
	public CageMGScript scr;

	public float TimeToBroke;

	public bool leftTriger;

	public GameObject RightTriger;

	private bool firstEnter = true;

	private void Start()
	{
		if (leftTriger)
		{
			RightTriger.SetActive(value: false);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (firstEnter && (other.tag == "CarMain" || other.tag == "CarMainChild"))
		{
			firstEnter = false;
			StartCoroutine(delay());
		}
	}

	private IEnumerator delay()
	{
		float t = TimeToBroke;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return null;
		}
		if (leftTriger)
		{
			scr.BrokeCage(left: true);
			if ((bool)RightTriger)
			{
				RightTriger.SetActive(value: true);
			}
		}
		else
		{
			scr.BrokeCage(left: false);
		}
		base.gameObject.SetActive(value: false);
	}
}
