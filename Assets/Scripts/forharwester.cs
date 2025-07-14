using System.Collections;
using UnityEngine;

public class forharwester : MonoBehaviour
{
	public GameObject mass;

	public Rigidbody2D body;

	private void OnEnable()
	{
		StartCoroutine(start());
	}

	private IEnumerator start()
	{
		while (body.isKinematic)
		{
			yield return null;
		}
		mass.SetActive(value: false);
		float t = 0f;
		while (t < 3f)
		{
			t += Time.unscaledDeltaTime;
			yield return null;
		}
		mass.SetActive(value: true);
	}
}
