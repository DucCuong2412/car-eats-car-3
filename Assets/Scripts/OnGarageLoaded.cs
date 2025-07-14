using System.Collections;
using UnityEngine;

public class OnGarageLoaded : MonoBehaviour
{
	public void EndLoad()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private IEnumerator DestroyOnLoad()
	{
		yield return Utilities.WaitForRealSeconds(0.1f);
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
