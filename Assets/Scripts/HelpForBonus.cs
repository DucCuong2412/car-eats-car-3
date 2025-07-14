using UnityEngine;

public class HelpForBonus : MonoBehaviour
{
	public GameObject red;

	public GameObject green;

	public GameObject REDCIO;

	public GameObject GREENCIO;

	public CircleCollider2D CC2d;

	public CircleCollider2D CC2dG;

	public CircleCollider2D CC2dR;

	private void Update()
	{
		if (REDCIO != null && GREENCIO != null)
		{
			REDCIO.SetActive(red.activeSelf);
			GREENCIO.SetActive(green.activeSelf);
			CC2dG.enabled = green.activeSelf;
			CC2dR.enabled = red.activeSelf;
		}
		if (!CC2d.enabled)
		{
			CC2d.enabled = true;
		}
	}
}
