using UnityEngine;

public class scaler : MonoBehaviour
{
	public RectTransform scalers;

	public GameObject objektForScale;

	private void Update()
	{
		Vector2 sizeDelta = scalers.sizeDelta;
		objektForScale.transform.localScale = new Vector3(sizeDelta.y / 100f, sizeDelta.y / 100f);
	}
}
