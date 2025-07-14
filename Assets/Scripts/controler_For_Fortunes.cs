using UnityEngine;
using UnityEngine.UI;

public class controler_For_Fortunes : MonoBehaviour
{
	public Text yesCoin;

	public Text noCoin;

	public Animation wheels;

	public GameObject GlowForSpin;

	public Button btnClickFortune;

	private void OnEnable()
	{
		btnClickFortune.onClick.AddListener(ButtonClickFortune);
	}

	private void OnDisable()
	{
		btnClickFortune.onClick.RemoveAllListeners();
	}

	private void ButtonClickFortune()
	{
		Fortune.ShowWheel(Progress.levels.tickets, delegate
		{
		});
	}

	private void Update()
	{
		if (Progress.levels.tickets > 0)
		{
			IfSpinYes(enable: true);
		}
		else
		{
			IfSpinYes(enable: false);
		}
	}

	private void IfSpinYes(bool enable)
	{
		noCoin.gameObject.SetActive(!enable);
		yesCoin.gameObject.SetActive(enable);
		GlowForSpin.SetActive(enable);
		if (enable)
		{
			wheels.Play("wheel");
			return;
		}
		wheels["wheel"].time = 0f;
		wheels.Stop();
	}
}
