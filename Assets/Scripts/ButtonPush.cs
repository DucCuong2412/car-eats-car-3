using UnityEngine;

public class ButtonPush : MonoBehaviour
{
	private void OnClick()
	{
		Audio.PlayAsync("gui_button_02_sn");
	}
}
