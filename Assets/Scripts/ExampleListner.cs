using UnionAssets.FLE;
using UnityEngine;

public class ExampleListner : MonoBehaviour
{
	public GUIStyle style;

	private string label = "Click's: ";

	private int count;

	private void Start()
	{
		EventButtonExample.instance.addEventListener("click", onButtonClick);
		EventButtonExample.instance.addEventListener("click", onButtonClickData);
	}

	private void onButtonClick()
	{
		count++;
	}

	private void onButtonClickData(CEvent e)
	{
		UnityEngine.Debug.Log("================================");
		UnityEngine.Debug.Log("onButtonClickData");
		UnityEngine.Debug.Log("dispatcher: " + e.dispatcher.ToString());
		UnityEngine.Debug.Log("event data: " + e.data.ToString());
		UnityEngine.Debug.Log("event name: " + e.name.ToString());
		UnityEngine.Debug.Log("================================");
	}

	private void OnGUI()
	{
		GUI.Label(new Rect(0f, 0f, 200f, 200f), label + count.ToString(), style);
	}
}
