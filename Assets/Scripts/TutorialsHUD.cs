using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialsHUD : MonoBehaviour
{
	public GameObject Shade;

	public GameObject Turbo;

	public GameObject FlipB;

	public GameObject Bomb;

	public GameObject Bonus;

	public GameObject Badge;

	public GameObject controlls;

	public GameObject tapToCont;

	public Button clicForContrls;

	public Button badges;

	public Button bonuses;

	public GUIControllsButton turbos;

	public GUIControllsButton bombs;

	public GUIControllsButton flips;

	public GUIControllsButton flipsb;

	[Header("LISTS")]
	public List<GameObject> onTurbo = new List<GameObject>();

	public List<GameObject> offTurbo = new List<GameObject>();

	public List<GameObject> onBomb = new List<GameObject>();

	public List<GameObject> offBomb = new List<GameObject>();

	public List<GameObject> onFlip = new List<GameObject>();

	public List<GameObject> offFlip = new List<GameObject>();

	public Animator FlipBAnim;

	public Animator FlipFAnim;

	public Animator TurboAnim;

	public Animator BombsAnim;

	[HideInInspector]
	public int i;

	private void OnEnable()
	{
		badges.onClick.AddListener(clices);
		bonuses.onClick.AddListener(clices);
	}

	private void OnDisable()
	{
		badges.onClick.RemoveAllListeners();
		bonuses.onClick.RemoveAllListeners();
	}

	public void clic()
	{
		Shade.SetActive(value: false);
		Turbo.SetActive(value: false);
		FlipB.SetActive(value: false);
		Bomb.SetActive(value: false);
		Bonus.SetActive(value: false);
		Badge.SetActive(value: false);
		foreach (GameObject item in onBomb)
		{
			item.SetActive(value: false);
		}
		foreach (GameObject item2 in offBomb)
		{
			item2.SetActive(value: true);
		}
		foreach (GameObject item3 in onTurbo)
		{
			item3.SetActive(value: false);
		}
		foreach (GameObject item4 in offTurbo)
		{
			item4.SetActive(value: true);
		}
		foreach (GameObject item5 in onFlip)
		{
			item5.SetActive(value: false);
		}
		foreach (GameObject item6 in offFlip)
		{
			item6.SetActive(value: true);
		}
		FlipBAnim.enabled = false;
		FlipFAnim.enabled = false;
		TurboAnim.enabled = false;
		BombsAnim.enabled = false;
		Time.timeScale = 1f;
	}

	public void clices()
	{
		Shade.SetActive(value: false);
		Turbo.SetActive(value: false);
		FlipB.SetActive(value: false);
		Bomb.SetActive(value: false);
		Bonus.SetActive(value: false);
		Badge.SetActive(value: false);
		controlls.SetActive(value: false);
		Progress.fortune.GOGOGOGOGOGO = true;
		Time.timeScale = 1f;
	}

	private void Update()
	{
		i++;
		if (i == 60)
		{
			badges.interactable = true;
			bonuses.interactable = true;
			turbos.enabled = true;
			bombs.enabled = true;
			flips.enabled = true;
			flipsb.enabled = true;
			clicForContrls.interactable = true;
			tapToCont.SetActive(value: true);
		}
	}
}
