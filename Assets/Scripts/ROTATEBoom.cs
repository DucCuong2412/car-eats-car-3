using UnityEngine;

public class ROTATEBoom : MonoBehaviour
{
	private float speeds;

	private void Start()
	{
		if (UnityEngine.Random.Range(0, 2) == 0)
		{
			speeds = UnityEngine.Random.Range(-100, -70);
		}
		else
		{
			speeds = UnityEngine.Random.Range(70, 100);
		}
	}

	private void Update()
	{
		base.transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * speeds * 10f));
	}
}
