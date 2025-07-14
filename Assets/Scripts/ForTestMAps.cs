using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForTestMAps : MonoBehaviour
{
	private string text = string.Empty;

	public List<GameObject> SELLECTS = new List<GameObject>();

	public GameObject BUTTON;

	public GameObject SEA;

	public GameObject CARS;

	public GameObject DECOR_B;

	public GameObject DECOR;

	public GameObject BOSSES;

	public GameObject ALLMAP;

	public GameObject ALLLEVELSSS;

	private void OnGUI()
	{
		float num = Screen.width;
		float num2 = Screen.height;
		Rect rect = new Rect(0f, 0f, num, num2);
		GUIStyle button = GUI.skin.button;
		button.fixedWidth = num * 0.1f;
		button.fixedHeight = num2 * 0.1f;
		button.fontSize = (int)(num * 0.015f);
		GUI.Box(rect, string.Empty);
		GUILayout.BeginArea(rect);
		GUILayout.BeginHorizontal(GUILayout.Width(num));
		if (GUILayout.Button("TEST ALL", button))
		{
			StartCoroutine(fjdkshfkdjs());
		}
		if (GUILayout.Button("TEST SELLECTS", button))
		{
			StartCoroutine(qweqr());
		}
		if (GUILayout.Button("TEST BUTTON", button))
		{
			BUTTON.SetActive(!BUTTON.activeSelf);
		}
		if (GUILayout.Button("TEST CARS", button))
		{
			CARS.SetActive(!CARS.activeSelf);
		}
		if (GUILayout.Button("TEST DECOR", button))
		{
			DECOR_B.SetActive(!DECOR_B.activeSelf);
			DECOR.SetActive(!DECOR.activeSelf);
		}
		if (GUILayout.Button("TEST BOSSES", button))
		{
			BOSSES.SetActive(!BOSSES.activeSelf);
		}
		if (GUILayout.Button("TEST MAP", button))
		{
			ALLMAP.SetActive(!ALLMAP.activeSelf);
		}
		if (GUILayout.Button("TEST ALL", button))
		{
			ALLLEVELSSS.SetActive(!ALLLEVELSSS.activeSelf);
		}
		if (GUILayout.Button("TEST SEA", button))
		{
			SEA.SetActive(!SEA.activeSelf);
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}

	private IEnumerator fjdkshfkdjs()
	{
		Object[] renderers = UnityEngine.Object.FindObjectsOfType(typeof(Image));
		UnityEngine.Debug.Log(renderers.Length);
		Object[] array = renderers;
		foreach (UnityEngine.Object e in array)
		{
			(e as Image).gameObject.SetActive(value: false);
			UnityEngine.Debug.Log((e as Image).gameObject.name);
			text = text + (e as Image).gameObject.name + "\n";
			yield return new WaitForSeconds(0.2f);
		}
	}

	private IEnumerator qweqr()
	{
		foreach (GameObject e in SELLECTS)
		{
			e.SetActive(value: false);
			UnityEngine.Debug.Log(e.name);
			text = text + e.name + "\n";
			yield return new WaitForSeconds(1f);
		}
	}
}
