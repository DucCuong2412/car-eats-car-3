using UnityEngine;

public class GarageParalaxAndGodrays : MonoBehaviour
{
	public RectTransform Content;

	public float speedPar;

	public float rotationPar;

	public RectTransform Paralax;

	public float speedGR;

	public float rotationGR;

	public RectTransform GodRays;

	private void Update()
	{
		Vector2 anchoredPosition = Content.anchoredPosition;
		if (!(anchoredPosition.x < 110f))
		{
			return;
		}
		Vector2 anchoredPosition2 = Content.anchoredPosition;
		if (anchoredPosition2.x > -2700f)
		{
			RectTransform paralax = Paralax;
			Vector2 anchoredPosition3 = Content.anchoredPosition;
			paralax.anchoredPosition = new Vector2(anchoredPosition3.x * speedPar, 0f);
			if (GodRays != null)
			{
				RectTransform godRays = GodRays;
				Vector2 anchoredPosition4 = Content.anchoredPosition;
				godRays.anchoredPosition = new Vector2(anchoredPosition4.x * speedGR, 0f);
			}
		}
	}
}
