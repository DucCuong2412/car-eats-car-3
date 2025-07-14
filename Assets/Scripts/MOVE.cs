using System.Collections;
using UnityEngine;

public class MOVE : MonoBehaviour
{
	public float Speed;

	public GameObject GO;

	public Animation Anim;

	private int i;

	private void OnEnable()
	{
		StartCoroutine(animation());
	}

	private IEnumerator animation()
	{
		while (true)
		{
			Anim.Play();
			yield return new WaitForSeconds(5f);
		}
	}

	private void Update()
	{
		if (i == 5)
		{
			Transform transform = GO.transform;
			Vector3 localPosition = GO.transform.localPosition;
			float x = localPosition.x + Speed * Time.deltaTime;
			Vector3 localPosition2 = GO.transform.localPosition;
			transform.localPosition = new Vector3(x, localPosition2.y);
			i = 0;
		}
		else
		{
			i++;
		}
	}
}
