using UnityEngine;

public class TutorialsForGamePlay : MonoBehaviour
{
	public bool Heals;

	public bool Turbo;

	public bool FlipB;

	public bool FlipF;

	public bool Bomb;

	public bool Bonus;

	public bool Badge;

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Wheels" || coll.tag == "CarMainChild" || coll.tag == "CarMain")
		{
			if (Heals)
			{
				H();
			}
			if (Turbo)
			{
				T();
			}
			if (FlipB || FlipF)
			{
				FB();
			}
			if (Bomb)
			{
				BOMBS();
			}
			if (Bonus)
			{
				BONUSES();
			}
		}
	}

	private void H()
	{
		RaceLogic.instance.gui.THUD.Shade.SetActive(value: true);
		RaceLogic.instance.gui.THUD.i = 0;
		RaceLogic.instance.gui.THUD.badges.interactable = false;
		RaceLogic.instance.gui.THUD.bonuses.interactable = false;
		Time.timeScale = 0f;
		base.gameObject.SetActive(value: false);
	}

	private void T()
	{
		RaceLogic.instance.gui.THUD.Turbo.SetActive(value: true);
		RaceLogic.instance.gui.THUD.TurboAnim.enabled = true;
		RaceLogic.instance.gui.THUD.i = 0;
		RaceLogic.instance.gui.THUD.bombs.enabled = false;
		RaceLogic.instance.gui.THUD.turbos.enabled = false;
		RaceLogic.instance.gui.THUD.flips.enabled = false;
		RaceLogic.instance.gui.THUD.flipsb.enabled = false;
		foreach (GameObject item in RaceLogic.instance.gui.THUD.offTurbo)
		{
			item.SetActive(value: false);
		}
		Time.timeScale = 0f;
		base.gameObject.SetActive(value: false);
	}

	private void FB()
	{
		RaceLogic.instance.gui.THUD.i = 0;
		RaceLogic.instance.gui.THUD.FlipB.SetActive(value: true);
		RaceLogic.instance.gui.THUD.FlipBAnim.enabled = true;
		RaceLogic.instance.gui.THUD.FlipFAnim.enabled = true;
		RaceLogic.instance.gui.THUD.bombs.enabled = false;
		RaceLogic.instance.gui.THUD.turbos.enabled = false;
		RaceLogic.instance.gui.THUD.flips.enabled = false;
		RaceLogic.instance.gui.THUD.flipsb.enabled = false;
		foreach (GameObject item in RaceLogic.instance.gui.THUD.offFlip)
		{
			item.SetActive(value: false);
		}
		Time.timeScale = 0f;
		base.gameObject.SetActive(value: false);
	}

	private void BOMBS()
	{
		RaceLogic.instance.gui.THUD.i = 0;
		RaceLogic.instance.gui.THUD.Bomb.SetActive(value: true);
		RaceLogic.instance.gui.THUD.BombsAnim.enabled = true;
		RaceLogic.instance.gui.THUD.bombs.enabled = false;
		RaceLogic.instance.gui.THUD.turbos.enabled = false;
		RaceLogic.instance.gui.THUD.flips.enabled = false;
		RaceLogic.instance.gui.THUD.flipsb.enabled = false;
		foreach (GameObject item in RaceLogic.instance.gui.THUD.offBomb)
		{
			item.SetActive(value: false);
		}
		Time.timeScale = 0f;
		base.gameObject.SetActive(value: false);
	}

	private void BONUSES()
	{
		RaceLogic.instance.gui.THUD.Shade.SetActive(value: true);
		RaceLogic.instance.gui.THUD.Bonus.SetActive(value: true);
		RaceLogic.instance.gui.THUD.i = 0;
		RaceLogic.instance.gui.THUD.badges.interactable = false;
		RaceLogic.instance.gui.THUD.bonuses.interactable = false;
		RaceLogic.instance.gui.THUD.bombs.enabled = false;
		RaceLogic.instance.gui.THUD.turbos.enabled = false;
		RaceLogic.instance.gui.THUD.flips.enabled = false;
		RaceLogic.instance.gui.THUD.flipsb.enabled = false;
		Time.timeScale = 0f;
		base.gameObject.SetActive(value: false);
	}
}
