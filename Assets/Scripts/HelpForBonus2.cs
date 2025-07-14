using UnityEngine;

public class HelpForBonus2 : MonoBehaviour
{
	public CollectibleItem CI;

	private void OnEnable()
	{
		if (CI != null)
		{
			CI.enabled = true;
		}
	}
}
