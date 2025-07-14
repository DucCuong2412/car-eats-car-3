using System;
using System.Collections;
using UnityEngine;

public class Loading : MonoBehaviour
{
	public static void LoadResource(string resouceName, Action<GameObject> OnLoad)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("GUI/Loading")) as GameObject;
		Loading component = gameObject.GetComponent<Loading>();
		component.StartCoroutine(component.StartToLoad(resouceName, OnLoad));
	}

	private IEnumerator StartToLoad(string resouceName, Action<GameObject> OnLoad)
	{
		yield return null;
		ResourceRequest request = Resources.LoadAsync(resouceName);
		while (!request.isDone)
		{
			yield return null;
		}
		OnLoad(request.asset as GameObject);
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
