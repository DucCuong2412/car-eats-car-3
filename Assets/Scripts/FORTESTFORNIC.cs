using System.Collections;
using UnityEngine;

public class FORTESTFORNIC : MonoBehaviour
{
	private void Start()
	{
		StartCoroutine(test());
	}

	private IEnumerator test()
	{
		yield return new WaitForSeconds(1f);
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
