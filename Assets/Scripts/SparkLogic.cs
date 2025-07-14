using System.Collections;
using UnityEngine;

public class SparkLogic : MonoBehaviour
{
	public Animation Spark;

	public float maxScale = 1f;

	public float MinScale = 1f;

	public float speeds = 1f;

	private void OnEnable()
	{
		if (RaceLogic.instance.AllInitedForPool)
		{
			StartCoroutine(SparkFly());
		}
	}

	private IEnumerator SparkFly()
	{
		float angle = UnityEngine.Random.Range(315, 360);
		float size = UnityEngine.Random.Range(MinScale, maxScale);
		Spark.transform.parent.parent.localRotation = Quaternion.Euler(0f, 0f, angle);
		Spark.transform.parent.parent.localScale = new Vector3(size, size, 1f);
		Spark["spark_an"].speed = speeds;
		Spark.Play();
		while (Spark.isPlaying)
		{
			yield return 0;
		}
		base.gameObject.SetActive(value: false);
	}
}
